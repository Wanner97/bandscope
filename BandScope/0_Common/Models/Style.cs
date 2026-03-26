namespace BandScope.Common.Models
{
    public class Style
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Artist> Artists { get; set; }
    }
}
