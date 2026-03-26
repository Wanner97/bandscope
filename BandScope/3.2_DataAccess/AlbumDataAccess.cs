using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class AlbumDataAccess : IAlbumDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public AlbumDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Album CreateAlbum(Album album)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Albums.Add(album);
                context.SaveChanges();

                return album;
            }
        }

        public List<Album> GetAlbumsByArtist(int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Albums
                    .Where(a => a.ArtistId == artistId)
                    .Include(a => a.Artist)
                    .ToList();
            }
        }
    }
}
