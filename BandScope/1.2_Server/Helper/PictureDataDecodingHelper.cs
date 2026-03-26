namespace BandScope.Server.Helper
{
    public class PictureDataDecodingHelper
    {
        public static byte[] DecodePictureData(string? data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return Array.Empty<byte>();

            if (data.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                const string marker = "base64,";
                var index = data.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    var base64 = data[(index + marker.Length)..];
                    return Convert.FromBase64String(base64);
                }
            }

            return Convert.FromBase64String(data);
        }
    }
}
