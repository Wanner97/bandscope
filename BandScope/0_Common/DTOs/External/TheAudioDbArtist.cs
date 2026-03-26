using System.Text.Json.Serialization;

namespace BandScope.Common.DTOs.External
{
    public class TheAudioDbArtist
    {
        [JsonPropertyName("strArtist")]
        public string Name { get; set; } = default;

        [JsonPropertyName("idArtist")]
        public string AudioDbId { get; set; } = default;

        [JsonPropertyName("strBiographyEN")]
        public string Biography { get; set; } = default;

        [JsonPropertyName("strCountry")]
        public string Origin { get; set; } = default;

        [JsonPropertyName("strLastFMChart")]
        public string LastFmUrl { get; set; } = default;

        [JsonPropertyName("strArtistThumb")]
        public string ThumbUrl { get; set; } = default;

        [JsonPropertyName("strArtistLogo")]
        public string LogoUrl { get; set; } = default;

        [JsonPropertyName("strGenre")]
        public string GenreName { get; set; } = default;

        [JsonPropertyName("strStyle")]
        public string StyleName { get; set; } = default;
    }
}
