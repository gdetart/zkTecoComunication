using System;
using System.Runtime.InteropServices;

namespace zkTecoTry
{
    class Program
    {
        static void Main(string[] args)
        {
            DeviceControl.DeviceControl device = new();
            DeviceControl.DeviceControl.AssignNewIpToController("00:17:61:C8:CB:9C", "192.168.0.202");


        }
    }
}
