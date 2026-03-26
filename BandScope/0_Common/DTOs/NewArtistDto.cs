namespace BandScope.Common.DTOs
{
    public class NewArtistDto
    {
        public string Name { get; set; }
        public int? StyleId { get; set; }
        public int? GenreId { get; set; }
        public string? Origin { get; set; }
        public string? Biography { get; set; }
        public string? LastFmUrl { get; set; }
        public int? ThumbnailId { get; set; }
        public int? LogoId { get; set; }
    }
}
