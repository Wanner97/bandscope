namespace BandScope.Common.DTOs
{
    public class ArtistDetailDto
    {
        public int? Id { get; set; }
        public string ArtistName { get; set; }
        public string Biography { get; set; }
        public int? TheAudioDbId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string? StyleName { get; set; }
        public int? StyleId { get; set; }
        public string? GenreName { get; set; }
        public int? GenreId { get; set; }
        public int AverageRating { get; set; }
        public int? FavorizedCount { get; set; }
        public string? Origin { get; set; }
        public string? LastFmUrl { get; set; }
        public List<ReviewOnArtistDetailDto>? Reviews { get; set; }
        public bool IsFromExternalApi { get; set; }
    }
}
