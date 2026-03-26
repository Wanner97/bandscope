namespace BandScope.Server.Configuration
{
    public class TheAudioDbSettings
    {
        public const string SectionName = "TheAudioDb";

        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
