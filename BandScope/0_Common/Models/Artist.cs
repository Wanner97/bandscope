namespace BandScope.Common.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int? TheAudioDbId { get; set; }
        public string? Biography { get; set; }
        public string? Origin { get; set; }
        public string? LastFmUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? LogoUrl { get; set; }

        public int? ThumbnailPictureId { get; set; }
        public Picture? ThumbnailPicture { get; set; }

        public int? LogoPictureId { get; set; }
        public Picture? LogoPicture { get; set; }

        public int? StyleId { get; set; }
        public Style? Style { get; set; }

        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<User> FavorizedByUsers { get; set; }
    }
}
