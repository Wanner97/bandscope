using BandScope.Common.DTOs;
using BandScope.Logic.Interfaces;
using BandScope.Server.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BandScope.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginDto loginDto)
        {
            var user = _userLogic.CheckCredentials(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            var token = _userLogic.GenerateToken(user);

            var returnValue = new LoginReturnDto()
            {
                Token = token
            };

            return Ok(returnValue);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(RegisterDto registerDto)
        {
            var newUserModel = DtoToModelMapper.MapToNewUserModel(registerDto);

            var returnValue = new BoolFlagReturnDto();

            var createdUser = _userLogic.CreateUser(newUserModel);

            if (createdUser != null)
            {
                returnValue.Flag = true;
            }

            return Created(string.Empty, returnValue);
        }

        [Authorize]
        [HttpGet("byid")]
        public ActionResult GetOwnProfileData()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var user = _userLogic.GetUserById(userId);

            var returnValue = ModelToDtoMapper.MapToUserUpdateDto(user);

            return Ok(returnValue);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("byid/{userId:int}")]
        public ActionResult GetUserById(int userId)
        {
            var user = _userLogic.GetUserById(userId);

            var returnValue = ModelToDtoMapper.MapToUserProfileDto(user);

            return Ok(returnValue);
        }

        [Authorize]
        [HttpGet("profilebyid")]
        public ActionResult GetOwnProfileDataByIdForProfilePage()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var user = _userLogic.GetUserById(userId);

            var returnValue = ModelToDtoMapper.MapToUserProfileDto(user);

            return Ok(returnValue);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult GetAllUsersExcludingRequestingAdmin()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var userModelList = _userLogic.GetAllUsers(userId);

            var dtoList = new List<UserProfileDto>();

            foreach (var user in userModelList)
            {
                dtoList.Add(ModelToDtoMapper.MapToUserProfileDto(user));
            }

            return Ok(dtoList);
        }

        [AllowAnonymous]
        [HttpGet("email/{email}")]
        public ActionResult EMailAlreadyExists(string email)
        {
            var returnValue = new BoolFlagReturnDto { Flag = _userLogic.EMailAlreadyExists(email) };

            return Ok(returnValue);
        }

        [AllowAnonymous]
        [HttpGet("nickname/{nickname}")]
        public ActionResult NicknameAlreadyExists(string nickname)
        {
            var returnValue = new BoolFlagReturnDto { Flag = _userLogic.NicknameAlreadyExists(nickname) };

            return Ok(returnValue);
        }

        [Authorize]
        [HttpPut("update")]
        public ActionResult UpdateUserProfile(UserUpdateDto userUpdateDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId) || userId != userUpdateDto.Id)
            {
                return Forbid();
            }

            var userOldPassword = userUpdateDto.UserPassword;

            if (!_userLogic.ValidateOldUserPassword(userId, userOldPassword))
            {
                return BadRequest();
            }

            var userModel = DtoToModelMapper.MapToUpdatedUserModel(userUpdateDto);

            var returnValue = new BoolFlagReturnDto { Flag = _userLogic.UpdateUser(userModel) };

            return Ok(returnValue);
        }

        [Authorize]
        [HttpDelete("delete/self")]
        public ActionResult DeleteOwnUserProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var returnValue = new BoolFlagReturnDto { Flag = _userLogic.DeleteUser(userId) };

            return Ok(returnValue);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{userId:int}")]
        public ActionResult DeleteOtherUser(int userId)
        {
            var returnValue = new BoolFlagReturnDto { Flag = _userLogic.DeleteUser(userId) };

            return Ok(returnValue);
        }
    }
}
