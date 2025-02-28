using CommunityForumApi.Interface;

namespace CommunityForumApi.Service
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtension)
        {
             if (imageFile == null)
            {
                throw new ArgumentException (nameof(imageFile));
            }

             var ext = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedFileExtension.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtension)} are allowed");
            }

            using var memorystream = new MemoryStream();
            await imageFile.CopyToAsync( memorystream );
            byte[] imageByte = memorystream.ToArray();

            string ImageBase64String = Convert.ToBase64String( imageByte ); 

            return ImageBase64String;
        }
    }
}
