using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void SetKeyFromString(string method, string key)
        {

        }
    }
}
