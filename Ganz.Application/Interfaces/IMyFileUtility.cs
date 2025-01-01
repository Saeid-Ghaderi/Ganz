using Microsoft.AspNetCore.Http;

namespace Ganz.Application.Interfaces
{
    public interface IMyFileUtility
    {
        string ConvertToBase64(byte[] data);
        byte[] ConvertToByteArray(IFormFile file);
        byte[] DecryptFile(byte[] fileContent);
        byte[] EncryptFile(byte[] fileContent);
        string GetEncryptedFileActionUrl(string thumbnailFileName, string entityName);
        string GetFileExtension(string fileName);
        string GetFileFullPath(string fileName, string enityName);
        string GetFileUrl(string thumbnailFileName, string entityName);
        string SaveFileInFolder(IFormFile file, string enityName, bool isEncrypt = false);
    }
}
