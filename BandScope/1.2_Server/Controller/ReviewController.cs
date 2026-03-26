using BandScope.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BandScope.Common.DTOs;
using BandScope.Server.Mapper;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewLogic _reviewLogic;

        public ReviewController(IReviewLogic reviewLogic)
        {
            _reviewLogic = reviewLogic;
        }

        [Authorize]
        [HttpGet("byuserandartist/{artistId:int}")]
        public ActionResult GetReviewByUserAndArtist(int artistId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var reviewModel = _reviewLogic.GetReviewByUserAndArtist(userId, artistId);

            if (reviewModel.Id != null)
            {
                return Ok(ModelToDtoMapper.MapToFullReviewDto(reviewModel, null));
            }

            return Ok(new FullReviewDto());
        }

        [Authorize]
        [HttpGet("byuser")]
        public ActionResult GetReviewsByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var reviewModels = _reviewLogic.GetReviewsByUserId(userId);

            var reviewDtos = new List<ReviewOnUserProfileDto>();

            foreach (var reviewModel in reviewModels)
            {
                reviewDtos.Add(ModelToDtoMapper.MapToReviewOnUserProfileDto(reviewModel));
            }

            return Ok(reviewDtos);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("byuserId/{userId:int}")]
        public ActionResult GetReviewsFromOtherUserByUserId(int userId)
        {
            var reviewModels = _reviewLogic.GetReviewsByUserId(userId);

            var reviewDtos = new List<ReviewOnUserProfileDto>();

            foreach (var reviewModel in reviewModels)
            {
                reviewDtos.Add(ModelToDtoMapper.MapToReviewOnUserProfileDto(reviewModel));
            }

            return Ok(reviewDtos);
        }

        [Authorize]
        [HttpGet("{reviewId:int}")]
        public ActionResult GetReviewById(int reviewId)
        {
            var reviewModel = _reviewLogic.GetReviewById(reviewId);

            var reviewDto = ModelToDtoMapper.MapToReviewOnArtistDetailDto(reviewModel);

            return Ok(reviewDto);
        }

        [Authorize]
        [HttpPost("add/{artistId:int}")]
        public ActionResult CreateReview(int artistId, FullReviewDto fullReviewDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            fullReviewDto.ArtistId = artistId;
            fullReviewDto.UserId = userId;

            var reviewModel = DtoToModelMapper.MapToNewReviewModel(fullReviewDto);

            var createdReview = _reviewLogic.CreateReview(reviewModel);

            var returnValue = ModelToDtoMapper.MapToFullReviewDto(createdReview, null);

            return Created(string.Empty, returnValue);
        }

        [Authorize]
        [HttpPut("update")]
        public ActionResult UpdateReview(FullReviewDto fullReviewDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var reviewModel = DtoToModelMapper.MapToUpdatedReviewModel(fullReviewDto);

            var returnValue = new BoolFlagReturnDto { Flag = _reviewLogic.UpdateReview(reviewModel) };

            if (returnValue.Flag)
            {
                return Ok(returnValue);
            }

            return BadRequest(returnValue);
        }

        [Authorize]
        [HttpDelete("delete/{reviewId:int}")]
        public ActionResult DeleteReview(int reviewId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var returnValue = new BoolFlagReturnDto { Flag = _reviewLogic.DeleteReview(reviewId) };

            if (returnValue.Flag)
            {
                return Ok(returnValue);
            }

            return BadRequest(returnValue);
        }
    }
}
