using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaedithSpammer.Usefulstuff
{
    public static class Utils
    {
        public static void Start()
        {
            ProxyHelper.ProxyHelper.Setup();
            TokenHelper.TokenHelper.Setup();
        }
    }
}
