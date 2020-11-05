using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace API.Models
{
    public class KeyHolder : IKeyHolder
    {
        public string Word { get; set; }
        public int Levels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public string GetCesarKey() { return Word; }
        public int GetZigZagKey() { return Levels; }
        public List<int> GetRouteKey() { return new List<int>() { Rows, Columns }; }

        public static bool CheckKeyValidness(string method, KeyHolder key)
        {
            switch (method.ToLower())
            {
                case "cesar":
                    if (key.Word == null || key.Word == string.Empty)
                    {
                        return false;
                    }
                    foreach (var item in key.Word)
                    {
                        if ((byte)item < 65 || (byte)item > 90 && (byte)item < 97 || (byte)item > 122)
                        {
                            return false;
                        }
                    }
                    break;
                case "zigzag":
                    if (key.Levels <= 0)
                    {
                        return false;
                    }
                    break;
                case "ruta":
                    if (key.Rows <= 0 || key.Columns <= 0)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        public static bool CheckKeyFromFileType(string path, KeyHolder key)
        {
            switch (Path.GetExtension(path))
            {
                case ".csr":
                    if (key.Word == null || key.Word == string.Empty)
                    {
                        return false;
                    }
                    foreach (var item in key.Word)
                    {
                        if ((byte)item < 65 || (byte)item > 90 && (byte)item < 97 || (byte)item > 122)
                        {
                            return false;
                        }
                    }
                    break;
                case ".zz":
                    if (key.Levels <= 0)
                    {
                        return false;
                    }
                    break;
                case ".rt":
                    if (key.Rows <= 0 || key.Columns <= 0)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        public bool SetKeyFromString(string method, string key)
        {
            switch (method.ToLower())
            {
                case "cesar":
                    Word = key;
                    return CheckKeyValidness(method.ToLower(), this);
                case "zigzag":
                    Levels = Convert.ToInt32(key);
                    return CheckKeyValidness(method.ToLower(), this);
                case "ruta":
                    var keyvalues = key.Split('x');
                    Rows = Convert.ToInt32(keyvalues[0]);
                    Columns = Convert.ToInt32(keyvalues[1]);
                    return CheckKeyValidness(method.ToLower(), this);
                default:
                    return false;
            }
        }
    }
}
