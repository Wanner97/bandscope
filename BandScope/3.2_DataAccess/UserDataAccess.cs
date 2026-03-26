using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UserDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public User CreateUser(User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();

                return user;
            }
        }

        public User GetUserById(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users.Find(userId);
            }
        }

        public User GetUserByEmail(string userEmail)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            }
        }

        public List<User> GetAllUsers(int? requesterId = null)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                if (requesterId == null)
                {
                    return context.Users.ToList();
                }
                else
                {
                    return context.Users
                        .Where(u => u.Id != requesterId)
                        .ToList();
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var existingUser = context.Users.Find(user.Id);
                if (existingUser == null)
                {
                    return false;
                }

                existingUser.Email = user.Email;
                existingUser.Nickname = user.Nickname;
                existingUser.PasswordHash = user.PasswordHash;

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var userToDelete = context.Users.Find(userId);
                if (userToDelete == null)
                {
                    return false;
                }

                context.Users.Remove(userToDelete);
                context.SaveChanges();

                return true;
            }
        }

        public bool EMailAlreadyExists(string email)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users.Any(u => u.Email == email);
            }
        }

        public bool NicknameAlreadyExists(string nickname)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users.Any(u => u.Nickname == nickname);
            }
        }
    }
}
