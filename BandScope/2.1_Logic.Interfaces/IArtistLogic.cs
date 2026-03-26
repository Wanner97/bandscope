using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IArtistLogic
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
