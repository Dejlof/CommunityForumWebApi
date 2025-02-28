namespace CommunityForumApi.Interface
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtension);
    }
}
