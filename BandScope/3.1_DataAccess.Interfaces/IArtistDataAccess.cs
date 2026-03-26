using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IArtistDataAccess
    {
        Artist CreateArtist(Artist artist);
        Artist GetArtistByIdForDetail(int artistId);
        List<Artist> GetAllArtistsForIndexList();
        List<Artist> GetSearchResultsForIndexList(string query);
        List<Artist> GetArtistsByUserFavorites(int userId);
        bool AddArtistToFavorites(int userId, int artistId);
        bool RemoveArtistFromFavorites(int userId, int artistId);
        bool ArtistIsInUsersFavorites(int userId, int artistId);
    }
}
