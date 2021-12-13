using System;
using System.Runtime.InteropServices;

namespace zkTecoTry
{
    class Program
    {
        static void Main(string[] args)
        {
            DeviceControl.DeviceControl device = new();
            device.SerchDevices();


        }
    }
}
