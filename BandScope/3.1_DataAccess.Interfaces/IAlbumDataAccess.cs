using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IAlbumDataAccess
    {
        Album CreateAlbum(Album album);
        List<Album> GetAlbumsByArtist(int artistId);
    }
}
