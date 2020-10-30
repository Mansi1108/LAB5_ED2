﻿using Encryptions.Encryptors;
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
            using var saver = new FileStream($"{path}/Uploads/{file.FileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);
            saver.Close();
            return $"{path}/Uploads/{file.FileName}";
        }

        public static FileProperties Cipher(string filePath, string method, string key)
        {
            var encryptedFileProperties = new FileProperties();
            switch (method.ToLower())
            {
                case "cesar":
                    var cesarEncryptor = new CesarEncryptor<KeyHolder>();
                    var cesarKeyHolder = new KeyHolder();
                    cesarKeyHolder.SetCesarKey(key);
                    encryptedFileProperties.Path = cesarEncryptor.EncryptFile(filePath, cesarKeyHolder);
                    encryptedFileProperties.FileType = ".csr";
                    break;
                case "zigzag":
                    var zigzagEncryptor = new ZigZagEncryptor<KeyHolder>();
                    var zigzagKeyHolder = new KeyHolder();
                    zigzagKeyHolder.SetZigZagKey(Convert.ToInt32(key));
                    encryptedFileProperties.Path = zigzagEncryptor.EncryptFile(filePath, zigzagKeyHolder);
                    encryptedFileProperties.FileType = ".zz";
                    break;
                case "ruta":
                    var routeEncryptor = new CesarEncryptor<KeyHolder>();
                    var routeKeyHolder = new KeyHolder();
                    var list = new List<int>();
                    key = key.ToLower();
                    var values = key.Split('-');
                    var dimensions = values[0].Split('x');
                    list.Add(Convert.ToInt32(dimensions[0]));
                    list.Add(Convert.ToInt32(dimensions[1]));
                    if (values[1] == "v")
                    {
                        list.Add(0);
                    }
                    else
                    {
                        list.Add(1);
                    }
                    routeKeyHolder.SetRouteKey(list);
                    encryptedFileProperties.Path = routeEncryptor.EncryptFile(filePath, routeKeyHolder);
                    encryptedFileProperties.FileType = ".rt";
                    break;
            }
            return encryptedFileProperties;
        }

        public static string Decipher(string filePath, string key)
        {
            var filetype = Path.GetExtension(filePath);
            switch (filetype)
            {
                case ".csr":
                    var cesarDecryptor = new CesarEncryptor<KeyHolder>();
                    var cesarKeyHolder = new KeyHolder();
                    cesarKeyHolder.SetCesarKey(key);
                    return cesarDecryptor.DecryptFile(filePath, cesarKeyHolder);
                case ".zz":
                    var zigzagDecryptor = new ZigZagEncryptor<KeyHolder>();
                    var zigzagKeyHolder = new KeyHolder();
                    zigzagKeyHolder.SetZigZagKey(Convert.ToInt32(key));
                    return zigzagDecryptor.DecryptFile(filePath, zigzagKeyHolder);
                case ".rt":
                    var routeDecryptor = new RouteEncryptor<KeyHolder>();
                    var routeKeyHolder = new KeyHolder();
                    var list = new List<int>();
                    var values = key.Split('-');
                    var dimensions = values[0].Split('x');
                    list.Add(Convert.ToInt32(dimensions[0]));
                    list.Add(Convert.ToInt32(dimensions[1]));
                    if (values[1] == "v")
                    {
                        list.Add(0);
                    }
                    else
                    {
                        list.Add(1);
                    }
                    routeKeyHolder.SetRouteKey(list);
                    return routeDecryptor.DecryptFile(filePath, routeKeyHolder);
            }
        }
    }
}
