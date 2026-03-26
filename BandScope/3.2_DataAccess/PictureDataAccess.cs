using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class PictureDataAccess : IPictureDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public PictureDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Picture CreatePicture(Picture picture)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Pictures.Add(picture);
                context.SaveChanges();

                return picture;
            }
        }

        public Picture GetPictureById(int pictureId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Pictures.Find(pictureId);
            }
        }
    }
}
