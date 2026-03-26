using BandScope.Common.DTOs;
using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IUserLogic
    {
        User CreateUser(User user);
        User GetUserById(int userId);
        List<User> GetAllUsers(int? requesterId = null);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool EMailAlreadyExists(string email);
        bool NicknameAlreadyExists(string nickname);
        bool ValidateOldUserPassword(int userId, string oldPassword);
        User CheckCredentials(LoginDto loginDto);
        string GenerateToken(User user);
    }
}
