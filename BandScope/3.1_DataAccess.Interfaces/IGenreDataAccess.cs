using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IGenreDataAccess
    {
        Genre CreateGenre(Genre genre);
        Genre GetGenreById(int genreId);
        Genre GetGenreByName(string genreName);
        List<Genre> GetAllGenres();
        bool GenreWithThisNameExists(string genreName);
    }
}
