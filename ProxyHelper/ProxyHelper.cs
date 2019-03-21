using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KaedithSpammer.ProxyHelper
{
    public static class ProxyHelper
    {
        public static List<string> Proxies { get; set; }
       
        public static void Setup()
        {
            Proxies = new List<string>();

            Proxies = File.ReadAllLines("Proxies.txt").ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Proxies.Count() > 0 ? $"[Kaedith RaidClient] ({Proxies.Count()}) Proxies have Loaded." : $"[Kaedith RaidClient] ({Proxies.Count()}) Proxy has Loaded.");
            Console.ForegroundColor = ConsoleColor.White;
            System.Timers.Timer proxyUpdate = new System.Timers.Timer();
            proxyUpdate.Elapsed += ProxyUpdate_Elapsed;
            proxyUpdate.Interval = TimeSpan.FromMinutes(10).TotalMilliseconds;
            proxyUpdate.AutoReset = true;
            proxyUpdate.Enabled = true;
            Console.WriteLine("Proxy check timer has been setup.");
        }

        private static void ProxyUpdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var proxies = new WebClient().DownloadString("https://api.proxyscrape.com/?request=displayproxies&proxytype=http");

                File.WriteAllText("Proxies.txt", proxies);

                Proxies = File.ReadAllLines("Proxies.txt").ToList();

                Console.WriteLine("Auto restocked proxies!");
            }
            catch(Exception f)
            {
                Console.WriteLine("An exception has occurred! Details: " + f);
                return;
            }
          


        }

        public static string GetRandomProxy()
        {
            return Proxies[new Random().Next(1, Proxies.Count())];
        }
    }
}
