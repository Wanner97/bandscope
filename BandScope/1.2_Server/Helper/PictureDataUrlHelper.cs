using BandScope.Common.Models;

namespace BandScope.Server.Helper
{
    public class PictureDataUrlHelper
    {
        public static string GeneratePictureDataUrl(Picture picture)
        {
            var data = picture.Data;

            if (data == null || data.Length <= 0)
            {
                return "";
            }

            var base64 = Convert.ToBase64String(data);
            var contentType = GetImageContentType(picture.Filename);

            return $"data:{contentType};base64,{base64}";
        }

        private static string GetImageContentType(string? filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                return "image/jpeg";
            }

            var extension = Path.GetExtension(filename).ToLowerInvariant();

            if (extension.StartsWith('.'))
            {
                extension = extension[1..];
            }

            if (extension.Equals("jpg"))
            {
                return "image/jpeg";
            }

            return $"image/{extension}";
        }
    }
}
