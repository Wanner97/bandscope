using System.Text.Json.Serialization;

namespace BandScope.Common.DTOs.External
{
    public class TheAudioDbAlbumSearchResponse
    {
        [JsonPropertyName("album")]
        public List<TheAudioDbAlbum>? Albums { get; set; }
    }
}
