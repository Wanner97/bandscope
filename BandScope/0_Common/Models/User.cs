using BandScope.Common.Enums;

namespace BandScope.Common.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public RoleEnum RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Artist> FavoriteArtists { get; set; }
    }
}
