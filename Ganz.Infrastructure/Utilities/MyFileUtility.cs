using Ganz.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Ganz.Infrastructure.Utilities
{
    public class MyFileUtility : IMyFileUtility
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyFileUtility(IWebHostEnvironment enviroment, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
        {
            _enviroment = enviroment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetFileFullPath(string fileName, string enityName)
        {
            
            var appRootPath = _enviroment.WebRootPath;
            var mediaRootPath = _configuration.GetValue<string>("MediaPath");

            return Path.Combine(appRootPath, mediaRootPath, enityName, fileName);
        }


        public string SaveFileInFolder(IFormFile file, string enityName, bool isEncrypt = false)
        {
            //var appRootPath = Path.Combine(_enviroment.ContentRootPath);
            var appRootPath = _enviroment.ContentRootPath;
            var mediaRootPath = _configuration.GetValue<string>("MediaPath");

            CheckAndCreatePathDirectory(appRootPath, mediaRootPath, enityName);

            var newFileName = $"{DateTime.Now.Ticks.ToString()}{GetFileExtension(file.FileName)}";

            var newFilePath = Path.Combine(appRootPath, mediaRootPath, enityName, newFileName);

            var byteArray = ConvertToByteArray(file);
            if (isEncrypt)
            {
                byteArray = EncryptFile(byteArray);
            }
            using var writer = new BinaryWriter(System.IO.File.OpenWrite(newFilePath));
            writer.Write(byteArray);
            return newFileName;
        }

        private string GetEntityFolderUrl(string host, string entityName, bool isHttps)
        {
            var mediaRootPath = _configuration.GetValue<string>("MediaPath").Replace("\\", "/");
            var httpMode = isHttps ? "https" : "http";
            return $"{httpMode}://{host}/{mediaRootPath}/{entityName}";
        }

        private void CheckAndCreatePathDirectory(string appRootPath, string mediaRootPath, string entityFolderName)
        {
            var mediaFullPath = Path.Combine(appRootPath, mediaRootPath);
            if (!Directory.Exists(mediaFullPath))
            {
                Directory.CreateDirectory(mediaFullPath);
            }

            var entityFolderFullPath = Path.Combine(mediaFullPath, entityFolderName);
            if (!Directory.Exists(entityFolderFullPath))
            {
                Directory.CreateDirectory(entityFolderFullPath);
            }
        }

        public string GetEncryptedFileActionUrl(string thumbnailFileName, string entityName)
        {
            var hostUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            var isHttps = _httpContextAccessor.HttpContext.Request.IsHttps;
            var httpMode = isHttps ? "https" : "http";
            return $"{httpMode}://{hostUrl}/Media/{entityName}/{thumbnailFileName}";
        }

        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public string GetFileUrl(string thumbnailFileName, string entityName)
        {
            var hostUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            var isHttps = _httpContextAccessor.HttpContext.Request.IsHttps;
            var folderPath = GetEntityFolderUrl(hostUrl, entityName, isHttps);
            return $"{folderPath}/{thumbnailFileName}";
        }

        public string GetFileExtension(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }

        public string ConvertToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public byte[] EncryptFile(byte[] fileContent)
        {
            string EncryptionKey = _configuration.GetValue<string>("FileEncryptionKey");
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public byte[] DecryptFile(byte[] fileContent)
        {
            string EncryptionKey = _configuration.GetValue<string>("FileEncryptionKey");
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
