using BandScope.Common;
using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class GenreLogic : IGenreLogic
    {
        private readonly IGenreDataAccess _genreDataAccess;

        public GenreLogic(IGenreDataAccess genreDataAccess)
        {
            _genreDataAccess = genreDataAccess;
        }

        public Genre CreateGenre(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
            {
                return _genreDataAccess.GetGenreById(Const.DefaultUnknownId);
            }

            new GenreValidator(false).ValidateAndThrow(genre);

            if (GenreWithThisNameExists(genre.Name))
            {
                return _genreDataAccess.GetGenreByName(genre.Name.Trim());
            }

            return _genreDataAccess.CreateGenre(genre);
        }

        public List<Genre> GetAllGenres()
        {
            return _genreDataAccess.GetAllGenres();
        }

        public bool GenreWithThisNameExists(string genreName)
        {
            return _genreDataAccess.GenreWithThisNameExists(genreName.Trim());
        }
    }
}
