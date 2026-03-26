using BandScope.Common.DTOs;
using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IConfiguration _configuration;

        public UserLogic(IUserDataAccess userDataAccess, IConfiguration configuration)
        {
            _userDataAccess = userDataAccess;
            _configuration = configuration;
        }

        public User CreateUser(User user)
        {
            new UserValidator(false).ValidateAndThrow(user);

            if (_userDataAccess.EMailAlreadyExists(user.Email.Trim()))
            {
                throw new ValidationException("E-Mail already exists.");
            }

            if (_userDataAccess.NicknameAlreadyExists(user.Nickname.Trim()))
            {
                throw new ValidationException("Nickname already exists.");
            }

            return _userDataAccess.CreateUser(user);
        }

        public User GetUserById(int userId)
        {
            return _userDataAccess.GetUserById(userId);
        }

        public List<User> GetAllUsers(int? requesterId = null)
        {
            return _userDataAccess.GetAllUsers(requesterId);
        }

        public bool UpdateUser(User user)
        {
            new UserValidator(true).ValidateAndThrow(user);

            return _userDataAccess.UpdateUser(user);
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ValidationException("userId must be greater than 0.");
            }

            return _userDataAccess.DeleteUser(userId);
        }

        public bool EMailAlreadyExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return _userDataAccess.EMailAlreadyExists(email.Trim());
        }

        public bool NicknameAlreadyExists(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                return false;
            }

            return _userDataAccess.NicknameAlreadyExists(nickname.Trim());
        }

        #region Update

        public bool ValidateOldUserPassword(int userId, string oldPassword)
        {
            var foundUser = _userDataAccess.GetUserById(userId);
            if (foundUser == null)
            {
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(oldPassword, foundUser.PasswordHash))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Login

        public User CheckCredentials(LoginDto loginDto)
        {
            var foundUser = _userDataAccess.GetUserByEmail(loginDto.Email);

            if (foundUser == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, foundUser.PasswordHash))
            {
                return null;
            }

            return foundUser;
        }

        #endregion

        #region Token

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nickname),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
