using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHook;
using System.Diagnostics;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = Process.GetProcessesByName("EliteDangerous").FirstOrDefault();
            if (process == null)
            {
                Console.WriteLine("Couldn't find Elite Process");
                Console.ReadLine();
            }
            else
            {
                RemoteHooking.Inject(process.Id, "EliteMarketScraper.dll", "EliteMarketScraper.dll");
                Console.WriteLine("Injected into process: " + process.ProcessName + ", ID: " + process.Id);
                Console.ReadLine();
            }
        }
    }
}
