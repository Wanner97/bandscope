using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class GenreDataAccess : IGenreDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public GenreDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Genre CreateGenre(Genre genre)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Genres.Add(genre);
                context.SaveChanges();

                return genre;
            }
        }

        public Genre GetGenreById(int genreId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Genres.Find(genreId);
            }
        }

        public Genre GetGenreByName(string genreName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Genres
                    .Where(g => g.Name.Equals(genreName))
                    .ToList()
                    .FirstOrDefault();
            }
        }

        public List<Genre> GetAllGenres()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Genres.ToList();
            }
        }

        public bool GenreWithThisNameExists(string genreName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var foundGenre = context.Genres
                    .Where(g => g.Name.Equals(genreName))
                    .ToList()
                    .FirstOrDefault();

                if (foundGenre == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
