using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptions.Interfaces
{
    public interface IKeyHolder
    {
        string GetCesarKey();
        int GetZigZagKey();
        List<int> GetRouteKey();
    }
}
