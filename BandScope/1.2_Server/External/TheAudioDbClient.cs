using BandScope.Common.DTOs.External;
using Serilog;
using System.Text.Json;

namespace BandScope.Server.External
{
    public class TheAudioDbClient : ITheAudioDbClient
    {
        private readonly HttpClient _http;
        private readonly Serilog.ILogger _log;

        public TheAudioDbClient(HttpClient http)
        {
            _http = http;
            _log = Log.ForContext<TheAudioDbClient>();
        }

        public async Task<List<TheAudioDbArtist>> SearchArtistsAsync(string artistName, CancellationToken cancellationToken = default)
        {
            string searchValue;

            if (string.IsNullOrWhiteSpace(artistName))
            {
                searchValue = "%";
            }
            else
            {
                var encodedName = Uri.EscapeDataString(artistName.Trim());
                searchValue = encodedName + "%";
            }

            var url = $"search.php?s={searchValue}";

            var response = await GetAsync<TheAudioDbArtistSearchResponse>(url, cancellationToken);

            if (response == null || response.Artists == null || !response.Artists.Any())
            {
                return new List<TheAudioDbArtist>();
            }

            return response.Artists;
        }

        public async Task<TheAudioDbArtist?> GetArtistByAudioDbIdAsync(int audioDbId, CancellationToken cancellationToken = default)
        {
            if (audioDbId <= 0)
            {
                return null;
            }

            var url = $"artist.php?i={audioDbId}";

            var response = await GetAsync<TheAudioDbArtistSearchResponse>(url, cancellationToken);

            if (response == null || response.Artists == null || !response.Artists.Any())
            {
                return null;
            }

            return response.Artists.FirstOrDefault();
        }

        public async Task<List<TheAudioDbAlbum>> SearchAlbumsByArtistAsync(int audioDbId, CancellationToken cancellationToken = default)
        {
            if (audioDbId <= 0)
            {
                return null;
            }

            var url = $"album.php?i={audioDbId}";

            var response = await GetAsync<TheAudioDbAlbumSearchResponse>(url, cancellationToken);

            if (response == null || response.Albums == null || !response.Albums.Any())
            {
                return new List<TheAudioDbAlbum>();
            }

            return response.Albums;
        }

        private async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            try
            {
                using var response = await _http.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _log.Warning("TheAudioDB call failed: {Url} -> {StatusCode}", url, response.StatusCode);

                    return default;
                }

                var json = await response.Content.ReadAsStreamAsync(cancellationToken);

                return await JsonSerializer.DeserializeAsync<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                _log.Warning(ex, "TheAudioDB network error for {Url}", url);

                return default;
            }
            catch (TaskCanceledException ex)
            {
                _log.Warning(ex, "TheAudioDB request timed out for {Url}", url);

                return default;
            }
            catch (JsonException ex)
            {
                _log.Warning(ex, "TheAudioDB returned invalid JSON for {Url}", url);

                return default;
            }
        }
    }
}
