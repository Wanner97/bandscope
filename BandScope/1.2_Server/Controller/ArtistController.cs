using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.External;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistLogic _artistLogic;
        private readonly ITheAudioDbClient _theAudioDbClient;

        public ArtistController(IArtistLogic artistLogic, ITheAudioDbClient theAudioDbClient)
        {
            _artistLogic = artistLogic;
            _theAudioDbClient = theAudioDbClient;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult> GetAllArtistsForIndexList()
        {
            var dbArtistModels = _artistLogic.GetAllArtistsForIndexList();

            var audioDbArtists = await _theAudioDbClient.SearchArtistsAsync(null);

            var artistDtos = new List<ArtistListDto>();

            foreach (var artist in dbArtistModels)
            {
                artistDtos.Add(ModelToDtoMapper.MapToArtistListDto(artist));
            }

            foreach (var theAudioDbArtist in audioDbArtists)
            {
                if (!artistDtos.Any(a => a.ArtistName.Equals(theAudioDbArtist.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    artistDtos.Add(AudioDbResultToDtoMapper.MapToArtistListDto(theAudioDbArtist));
                }
            }

            return Ok(artistDtos);
        }

        [AllowAnonymous]
        [HttpGet("search/{query}")]
        public async Task<ActionResult> GetSearchResultsForIndexList(string query)
        {
            var artistDtos = new List<ArtistListDto>();

            var dbArtistModels = _artistLogic.GetSearchResultsForIndexList(query);

            var audioDbArtists = await _theAudioDbClient.SearchArtistsAsync(query);

            foreach (var dbArtistModel in dbArtistModels)
            {
                artistDtos.Add(ModelToDtoMapper.MapToArtistListDto(dbArtistModel));
            }

            foreach (var theAudioDbArtist in audioDbArtists)
            {
                if (!artistDtos.Any(a => a.ArtistName.Equals(theAudioDbArtist.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    artistDtos.Add(AudioDbResultToDtoMapper.MapToArtistListDto(theAudioDbArtist));
                }
            }

            return Ok(artistDtos);
        }

        [AllowAnonymous]
        [HttpGet("{artistId:int}")]
        public ActionResult GetArtistByIdForDetail(int artistId)
        {
            var artistModel = _artistLogic.GetArtistByIdForDetail(artistId);

            if (artistModel == null)
            {
                return NotFound();
            }

            var artistDto = ModelToDtoMapper.MapToArtistDetailDto(artistModel);

            return Ok(artistDto);
        }

        [AllowAnonymous]
        [HttpGet("audiodb/{audioDbId:int}")]
        public async Task<ActionResult> GetArtistFromAudioDbForDetail(int audioDbId)
        {
            var audioDbArtist = await _theAudioDbClient.GetArtistByAudioDbIdAsync(audioDbId);

            if (audioDbArtist == null)
            {
                return NotFound();
            }

            var artistDto = AudioDbResultToDtoMapper.MapToArtistDetailDto(audioDbArtist);

            return Ok(artistDto);
        }

        [Authorize]
        [HttpGet("favorites")]
        public ActionResult GetFavoriteArtistsOfUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var artistList = _artistLogic.GetArtistsByUserFavorites(userId);

            var artistDtos = new List<ArtistOnUserProfileDto>();

            foreach (var artist in artistList)
            {
                artistDtos.Add(ModelToDtoMapper.MapToArtistOnUserProfileDto(artist));
            }

            return Ok(artistDtos);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("favorites/{userId:int}")]
        public ActionResult GetFavoriteArtistsByUserId(int userId)
        {
            var artistList = _artistLogic.GetArtistsByUserFavorites(userId);

            var artistDtos = new List<ArtistOnUserProfileDto>();

            foreach (var artist in artistList)
            {
                artistDtos.Add(ModelToDtoMapper.MapToArtistOnUserProfileDto(artist));
            }

            return Ok(artistDtos);
        }

        [Authorize]
        [HttpGet("{artistId:int}/favorite")]
        public ActionResult ArtistIsInUsersFavorites(int artistId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var returnValue = new BoolFlagReturnDto
            {
                Flag = _artistLogic.ArtistIsInUsersFavorites(userId, artistId)
            };

            return Ok(returnValue);
        }

        [Authorize]
        [HttpPost("newfromuser")]
        public ActionResult CreateArtistFromUser(NewArtistDto newArtistDto)
        {
            var artistModel = DtoToModelMapper.MapToNewArtistModel(newArtistDto);

            var returnValue = new BoolFlagReturnDto();

            var createdArtist = _artistLogic.CreateArtist(artistModel);

            if (createdArtist != null)
            {
                returnValue.Flag = true;
            }

            return Created(string.Empty, returnValue);
        }

        [Authorize]
        [HttpPost("newfromaudiodb")]
        public ActionResult CreateArtistFromAudioDb(ArtistDetailDto artistDetailDto)
        {
            var artistModel = DtoToModelMapper.MapAudioDbArtistToArtistModel(artistDetailDto);

            var createdArtist = _artistLogic.CreateArtist(artistModel);

            var returnValue = ModelToDtoMapper.MapToArtistDetailDto(createdArtist);

            return Created(string.Empty, returnValue);
        }

        [Authorize]
        [HttpPost("favorite/{artistId:int}")]
        public ActionResult AddArtistToFavorites(int artistId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var returnValue = new BoolFlagReturnDto { Flag = _artistLogic.AddArtistToFavorites(userId, artistId) };

            if (returnValue.Flag)
            {
                return Ok(returnValue);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("favorite/{artistId:int}")]
        public ActionResult RemoveArtistFromFavorites(int artistId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var returnValue = new BoolFlagReturnDto { Flag = _artistLogic.RemoveArtistFromFavorites(userId, artistId) };

            if (returnValue.Flag)
            {
                return Ok(returnValue);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("favorite/{userId:int}/{artistId:int}")]
        public ActionResult RemoveArtistFromOtherUsersFavorites(int userId, int artistId)
        {
            var returnValue = new BoolFlagReturnDto { Flag = _artistLogic.RemoveArtistFromFavorites(userId, artistId) };

            if (returnValue.Flag)
            {
                return Ok(returnValue);
            }

            return BadRequest();
        }
    }
}
