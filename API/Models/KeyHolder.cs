using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
