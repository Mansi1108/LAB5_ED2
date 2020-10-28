using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptions.Interfaces
{
    interface IKeyHolder
    {
        string CesarKey { get; set; } 
        int ZigZagKey { get; set; }
        List<int> RouteKey { get; set; }
    }
}
