using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IGenreLogic
    {
        Genre CreateGenre(Genre genre);
        List<Genre> GetAllGenres();
    }
}
