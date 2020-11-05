using Encryptions.Encryptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FileManager
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string path)
        {
            if (!Directory.Exists($"{path}/Uploads"))
            {
                Directory.CreateDirectory($"{path}/Uploads");
            }
            using var saver = new FileStream($"{path}/Uploads/{file.FileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);
            saver.Close();
            return $"{path}/Uploads/{file.FileName}";
        }

        public static FileProperties Cipher(string directory, string filePath, string method, KeyHolder key)
        {
            var encryptedFileProperties = new FileProperties();
            var savingPath = $"{directory}/Ciphers";
            if (!Directory.Exists(savingPath))
            {
                Directory.CreateDirectory(savingPath);
            }
            switch (method.ToLower())
            {
                case "cesar":
                    var cesarEncryptor = new CesarEncryptor<KeyHolder>();
                    encryptedFileProperties.Path = cesarEncryptor.EncryptFile(savingPath, filePath, key);
                    encryptedFileProperties.FileType = ".csr";
                    break;
                case "zigzag":
                    var zigzagEncryptor = new ZigZagEncryptor<KeyHolder>();
                    encryptedFileProperties.Path = zigzagEncryptor.EncryptFile(savingPath, filePath, key);
                    encryptedFileProperties.FileType = ".zz";
                    break;
                case "ruta":
                    var routeEncryptor = new RouteEncryptor<KeyHolder>();
                    encryptedFileProperties.Path = routeEncryptor.EncryptFile(savingPath, filePath, key);
                    encryptedFileProperties.FileType = ".rt";
                    break;
            }
            return encryptedFileProperties;
        }

        public static string Decipher(string folderPath, string filePath, KeyHolder key)
        {
            var filetype = Path.GetExtension(filePath);
            var savingPath = $"{folderPath}/Deciphers";
            if (!Directory.Exists(savingPath))
            {
                Directory.CreateDirectory(savingPath);
            }
            switch (filetype)
            {
                case ".csr":
                    var cesarDecryptor = new CesarEncryptor<KeyHolder>();
                    return cesarDecryptor.DecryptFile(savingPath, filePath, key);
                case ".zz":
                    var zigzagDecryptor = new ZigZagEncryptor<KeyHolder>();
                    return zigzagDecryptor.DecryptFile(savingPath, filePath, key);
                case ".rt":
                    var routeDecryptor = new RouteEncryptor<KeyHolder>();
                    return routeDecryptor.DecryptFile(savingPath, filePath, key);
            }
            return string.Empty;
        }
    }
}
