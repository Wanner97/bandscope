using System.Text.Json.Serialization;

namespace BandScope.Common.DTOs.External
{
    public class TheAudioDbArtistSearchResponse
    {
        [JsonPropertyName("artists")]
        public List<TheAudioDbArtist>? Artists { get; set; }
    }
}
