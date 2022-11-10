namespace IntegraCTE.Core.Services
{
    public interface IFileService
    {
         Task UploadFile(string xml);
         Task<string> DownloadFile(Guid nome);
    }
}