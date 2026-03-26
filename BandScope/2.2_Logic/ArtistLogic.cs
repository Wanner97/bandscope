using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class ArtistLogic : IArtistLogic
    {
        private readonly IArtistDataAccess _artistDataAccess;

        public ArtistLogic(IArtistDataAccess artistDataAccess)
        {
            _artistDataAccess = artistDataAccess;
        }

        public Artist CreateArtist(Artist artist)
        {
            new ArtistValidator(false).ValidateAndThrow(artist);

            return _artistDataAccess.CreateArtist(artist);
        }

        public Artist GetArtistByIdForDetail(int artistId)
        {
            if (artistId <= 0)
            {
                throw new ValidationException("artistId must be greater than 0.");
            }

            return _artistDataAccess.GetArtistByIdForDetail(artistId);
        }

        public List<Artist> GetAllArtistsForIndexList()
        {
            return _artistDataAccess.GetAllArtistsForIndexList();
        }

        public List<Artist> GetSearchResultsForIndexList(string query)
        {
            return _artistDataAccess.GetSearchResultsForIndexList(query);
        }

        public List<Artist> GetArtistsByUserFavorites(int userId)
        {
            if (userId <= 0)
            {
                throw new ValidationException("userId must be greater than 0.");
            }

            return _artistDataAccess.GetArtistsByUserFavorites(userId);
        }

        public bool AddArtistToFavorites(int userId, int artistId)
        {
            if (userId <= 0 || artistId <= 0)
            {
                throw new ValidationException("userId and artistId must be greater than 0.");
            }

            return _artistDataAccess.AddArtistToFavorites(userId, artistId);
        }

        public bool RemoveArtistFromFavorites(int userId, int artistId)
        {
            if (userId <= 0 || artistId <= 0)
            {
                throw new ValidationException("userId and artistId must be greater than 0.");
            }

            return _artistDataAccess.RemoveArtistFromFavorites(userId, artistId);
        }

        public bool ArtistIsInUsersFavorites(int userId, int artistId)
        {
            if (userId <= 0 || artistId <= 0)
            {
                throw new ValidationException("userId and artistId must be greater than 0.");
            }

            return _artistDataAccess.ArtistIsInUsersFavorites(userId, artistId);
        }
    }
}
