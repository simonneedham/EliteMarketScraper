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
    public class LocationHook : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int LocationDelegate(int a, int dataOffset);

        private string _name;
        private LocalHook _hook;
        private LocationDelegate _locationOriginal;
        public static Location currentLocation = null;

        public LocationHook()
        {
            var mainAddress = Utility.GetModuleHandle("EliteDangerous.exe");
            var imageBase = 4194304; 

            var locationAddress = IntPtr.Subtract(mainAddress, imageBase) + 0xBC3850;
            _locationOriginal = (LocationDelegate)Marshal.GetDelegateForFunctionPointer(locationAddress, typeof(LocationDelegate));


            _name = string.Format("LocationHook");
            _hook = LocalHook.Create(locationAddress, new LocationDelegate(LocationDetour), this);
            _hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
        }

        private int LocationDetour(int a, int dataOffset)
        {
            var result = _locationOriginal(a, dataOffset);

            var locationName = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 4) + 12);
            var systemName = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 8) + 12);
            var jurisdiction = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 12) + 12);
            var government = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 16) + 12);
            var economy = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 20) + 12);
            var distance = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr((IntPtr)dataOffset + 24) + 12);
            currentLocation = new Location(locationName, systemName, jurisdiction, government, economy, distance);
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
