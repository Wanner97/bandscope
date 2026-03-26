using System.Text.Json.Serialization;

namespace BandScope.Common.DTOs.External
{
    public class TheAudioDbAlbum
    {
        [JsonPropertyName("strAlbum")]
        public string Name { get; set; } = default;

        [JsonPropertyName("intYearReleased")]
        public string ReleaseYear { get; set; } = default;

        [JsonPropertyName("strAlbumThumb")]
        public string ImageUrl { get; set; } = default;
    }
}
