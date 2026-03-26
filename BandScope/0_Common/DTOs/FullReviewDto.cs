namespace BandScope.Common.DTOs
{
    public class FullReviewDto
    {
        public int? Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
    }
}
