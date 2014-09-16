using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EasyHook;
using System.IO;
using System.Diagnostics;

namespace EliteMarketScraper
{
    public class MarketHook : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MarketDelegate(int a, int b, int itemDatabaseId, double buyPrice, double sellPrice, int demand, int demandLevel, int cargoStock, double cargoItemBoughtFor, int stationStock, int stationStockLevel, int categoryName, int itemName, int itemInfo, double galacticAverage);

        private string _name;
        private LocalHook _hook;
        private MarketDelegate _marketOriginal;

        public MarketHook()
        {
            var mainAddress = Utility.GetModuleHandle("EliteDangerous.exe"); 
            var imageBase = 4194304;

            var marketAddress = IntPtr.Subtract(mainAddress, imageBase) + 0xBE2260;
            _marketOriginal = (MarketDelegate)Marshal.GetDelegateForFunctionPointer(marketAddress, typeof(MarketDelegate));


            _name = string.Format("MarketHook");
            _hook = LocalHook.Create(marketAddress, new MarketDelegate(MarketDetour), this);
            _hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
        }

        private int MarketDetour(int a, int b, int itemDatabaseId, double buyPrice, double sellPrice, int demand, int demandLevel, int cargoStock, double cargoItemBoughtFor, int stationStock, int stationStockLevel, int categoryName, int itemName, int itemInfo, double galacticAverage)
        {
            var result = _marketOriginal(a, b, itemDatabaseId, buyPrice, sellPrice, demand, demandLevel, cargoStock, cargoItemBoughtFor, stationStock, stationStockLevel, categoryName, itemName, itemInfo, galacticAverage);
            var sItemName = Marshal.PtrToStringAnsi((IntPtr)itemName);
            var sCategoryName = Marshal.PtrToStringAnsi((IntPtr)categoryName);
            var sItemInfo = Marshal.PtrToStringAnsi((IntPtr)itemInfo);

            Item newItem = new Item(itemDatabaseId, buyPrice, sellPrice, demand, demandLevel, cargoStock, cargoItemBoughtFor, stationStock, stationStockLevel, sCategoryName, sItemName, sItemInfo, galacticAverage, LocationHook.currentLocation);
            newItem.Dump();
            return result;
        }

        public void Dispose()
        {
            if (_hook == null)
                return;

            _hook.Dispose();
            _hook = null;
        }
    }
}
