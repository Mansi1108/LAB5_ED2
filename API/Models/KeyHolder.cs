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
        private string CesarKey;
        private int ZigZagKey;
        private List<int> RouteKey;

        public void SetCesarKey(string key) { CesarKey = key; } 
        public void SetZigZagKey(int key) { ZigZagKey = key; } 
        public void SetRouteKey(List<int> key) { RouteKey = key; } 

        public string GetCesarKey() { return CesarKey; }
        public List<int> GetRouteKey()  { return RouteKey; }
        public int GetZigZagKey() { return ZigZagKey; }

        public static bool CheckKeyValidness(string method, string key)
        {
            switch (method)
            {
                case "cesar":
                    foreach (var item in key)
                    {
                        if ((byte)item < 65 || (byte)item > 90 && (byte)item < 97 || (byte)item > 122)
                        {
                            return false;
                        }
                    }
                    break;
                case "zigzag":
                    break;
                case "ruta":
                    break;
            }
            return true;
        }

        public static bool CheckKeyFromFileType(string path, string key)
        {
            switch (Path.GetExtension(path))
            {
                case ".csr":
                    foreach (var item in key)
                    {
                        if ((byte)item < 65 || (byte)item > 90 && (byte)item < 97 || (byte)item > 122)
                        {
                            return false;
                        }
                    }
                    break;
                case ".zz":
                    break;
                case ".rt":
                    break;
            }
            return true;
        }
    }
}
