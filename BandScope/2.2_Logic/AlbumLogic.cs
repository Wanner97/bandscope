using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class AlbumLogic : IAlbumLogic
    {
        private readonly IAlbumDataAccess _albumDataAccess;

        public AlbumLogic(IAlbumDataAccess albumDataAccess)
        {
            _albumDataAccess = albumDataAccess;
        }

        public Album CreateAlbum(Album album)
        {
            new AlbumValidator(false).ValidateAndThrow(album);

            return _albumDataAccess.CreateAlbum(album);
        }

        public List<Album> GetAlbumsByArtist(int artistId)
        {
            if (artistId <= 0)
            {
                throw new ValidationException("artistId must be greater than 0.");
            }

            return _albumDataAccess.GetAlbumsByArtist(artistId);
        }
    }
}
