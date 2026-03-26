namespace BandScope.Common.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string? UserNewPassword { get; set; }
    }
}
