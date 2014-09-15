using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHook;

namespace EliteMarketScraper
{
    public class Main : IEntryPoint
    {
        public Main(EasyHook.RemoteHooking.IContext InContext)
        {
        }

        public void Run(EasyHook.RemoteHooking.IContext InContext)
        {
            MarketHook marketHook = new MarketHook();
            LocationHook locationHook = new LocationHook();
            RemoteHooking.WakeUpProcess();

            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}