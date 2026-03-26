using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IAlbumLogic
    {
        Album CreateAlbum(Album album);
        List<Album> GetAlbumsByArtist(int artistId);
    }
}
