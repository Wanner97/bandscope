namespace BandScope.Common.DTOs
{
    public class ArtistListDto
    {
        public int? Id { get; set; }
        public string ArtistName { get; set; }
        public int? TheAudioDbId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? StyleName { get; set; }
        public string? GenreName { get; set; }
        public int AverageRating { get; set; }
        public int? FavorizedCount { get; set; }
        public bool IsFromExternalApi { get; set; }
    }
}
