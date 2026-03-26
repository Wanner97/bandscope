using BandScope.Common.Enums;

namespace BandScope.Common.Models
{
    public class Role
    {
        public RoleEnum Id { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
