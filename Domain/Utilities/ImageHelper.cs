using Domain.Exceptions;

namespace Domain.Utilities
{
    public static class ImageHelper
    {
        public static async Task<string> SaveBase64ImageAsync(string base64Image, string directory, string fileName)
        {
            if (string.IsNullOrWhiteSpace(base64Image))
            {
                throw new ArgumentNullException(nameof(base64Image), "Uploaded Image Can't be null");
            }

            var mimeType = Base64Utils.GetMimeTypeFromBase64(base64Image);
            var fileExtension = MimeTypes.GetExtension(mimeType);

            var imageBytes = Convert.FromBase64String(base64Image);
            var filePath = Path.Combine("wwwroot", directory, fileName + fileExtension);

            // Remove any existing image with the same name
            if (File.Exists(filePath))
            {
                throw new AlreadyExistException("Image");
            }

            await File.WriteAllBytesAsync(filePath, imageBytes);

            return $"{directory}/{fileName}{fileExtension}";
        }
        public static async Task DeleteImage(string url)
        {
            if (File.Exists(url))
            {
                File.Delete(url);
            }
        }
    }
}