using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class ArtistDataAccess : IArtistDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public ArtistDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Artist CreateArtist(Artist artist)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Artists.Add(artist);
                context.SaveChanges();

                return artist;
            }
        }

        public Artist GetArtistByIdForDetail(int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Artists.Where(a => a.Id == artistId)
                    .Include(a => a.ThumbnailPicture)
                    .Include(a => a.LogoPicture)
                    .Include(a => a.Genre)
                    .Include(a => a.Style)
                    .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                    .Include(a => a.FavorizedByUsers)
                    .FirstOrDefault();

            }
        }

        public List<Artist> GetAllArtistsForIndexList()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Artists
                    .OrderBy(a => a.Id)
                    .Take(500)
                    .Include(a => a.ThumbnailPicture)
                    .Include(a => a.Style)
                    .Include(a => a.Genre)
                    .Include(a => a.Reviews)
                    .Include(a => a.FavorizedByUsers)
                    .ToList();
            }
        }

        public List<Artist> GetSearchResultsForIndexList(string query)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Artists
                    .Where(a => a.Name != null && EF.Functions.Like(a.Name, $"%{query}%"))
                    .Include(a => a.ThumbnailPicture)
                    .Include(a => a.Style)
                    .Include(a => a.Genre)
                    .Include(a => a.Reviews)
                    .Include(a => a.FavorizedByUsers)
                    .ToList();
            }
        }

        public List<Artist> GetArtistsByUserFavorites(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.FavoriteArtists)
                    .ToList();
            }
        }

        public bool AddArtistToFavorites(int userId, int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users
                    .Include(u => u.FavoriteArtists)
                    .SingleOrDefault(u => u.Id == userId);

                var artist = context.Artists
                    .SingleOrDefault(a => a.Id == artistId);

                if (user == null || artist == null)
                {
                    return false;
                }

                if (user.FavoriteArtists == null)
                {
                    user.FavoriteArtists = new List<Artist>();
                }

                if (user.FavoriteArtists.Any(a => a.Id == artistId))
                {
                    return true;
                }

                user.FavoriteArtists.Add(artist);
                context.SaveChanges();

                return true;
            }
        }

        public bool RemoveArtistFromFavorites(int userId, int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users
                    .Include(u => u.FavoriteArtists)
                    .SingleOrDefault(u => u.Id == userId);

                if (user == null || user.FavoriteArtists == null)
                {
                    return false;
                }

                var artistToRemove = user.FavoriteArtists
                    .FirstOrDefault(a => a.Id == artistId);

                if (artistToRemove == null)
                {
                    return false;
                }

                user.FavoriteArtists.Remove(artistToRemove);
                context.SaveChanges();

                return true;
            }
        }

        public bool ArtistIsInUsersFavorites(int userId, int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Artists
                    .Any(a => a.Id == artistId
                              && a.FavorizedByUsers.Any(u => u.Id == userId));
            }
        }
    }
}
