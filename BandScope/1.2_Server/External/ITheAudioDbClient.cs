using BandScope.Common.DTOs.External;

namespace BandScope.Server.External
{
    public interface ITheAudioDbClient
    {
        Task<List<TheAudioDbArtist>> SearchArtistsAsync(string artistName, CancellationToken cancellationToken = default);
        Task<TheAudioDbArtist?> GetArtistByAudioDbIdAsync(int audioDbId, CancellationToken cancellationToken = default);
        Task<List<TheAudioDbAlbum>> SearchAlbumsByArtistAsync(int audioDbId, CancellationToken cancellationToken = default);
    }
}
