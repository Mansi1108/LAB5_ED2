using Encryptions.Encryptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FileManager
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string path)
        {
            using var saver = new FileStream($"{path}/Uploads/{file.FileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);
            saver.Close();
            return $"{path}/Uploads/{file.FileName}";
        }

        public static FileProperties Cipher(string filePath, string method, string key)
        {
            var encryptedFileProperties = new FileProperties();
            switch (method)
            {
                case "Cesar":
                    var cesarEncryptor = new CesarEncryptor<KeyHolder>();
                    var cesarKeyHolder = new KeyHolder();
                    cesarKeyHolder.SetCesarKey(key);
                    encryptedFileProperties.Path = cesarEncryptor.EncryptFile(filePath, cesarKeyHolder);
                    encryptedFileProperties.FileType = ".csr";
                    break;
                case "ZigZag":
                    var zigzagEncryptor = new ZigZagEncryptor<KeyHolder>();
                    var zigzagKeyHolder = new KeyHolder();
                    zigzagKeyHolder.SetZigZagKey(Convert.ToInt32(key));
                    encryptedFileProperties.Path = zigzagEncryptor.EncryptFile(filePath, zigzagKeyHolder);
                    encryptedFileProperties.FileType = ".zz";
                    break;
                case "Ruta":
                    var routeEncryptor = new CesarEncryptor<KeyHolder>();
                    var routeKeyHolder = new KeyHolder();
                    var list = new List<int>();
                    foreach (var character in key)
                    {
                        list.Add(Convert.ToInt32(key));
                    }
                    routeKeyHolder.SetRouteKey(list);
                    encryptedFileProperties.Path = routeEncryptor.EncryptFile(filePath, routeKeyHolder);
                    encryptedFileProperties.FileType = ".rt";
                    break;
            }
            return encryptedFileProperties;
        }
    }
}
