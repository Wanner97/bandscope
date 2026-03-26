using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IUserDataAccess
    {
        User CreateUser(User user);
        User GetUserById(int userId);
        User GetUserByEmail(string userEmail);
        List<User> GetAllUsers(int? requesterId = null);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool EMailAlreadyExists(string email);
        bool NicknameAlreadyExists(string nickname);
    }
}
