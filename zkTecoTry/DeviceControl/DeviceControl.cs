using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace zkTecoTry.DeviceControl
{
    class DeviceControl : HelperSdk
    {

        IntPtr h = IntPtr.Zero;


        public static List<ZkAccess> SerchDevices()
        {
            int i = 0, j = 0;
            byte[] buffer = new byte[64 * 1024];

            string str = "";
            string[] tmp = null;
            string udp = "UDP";
            string adr = "255.255.255.255";

            List<ZkAccess> foundDevices = new();
            int ret = SearchDevice(udp, adr, ref buffer[0]);
            if (ret >= 0)
            {
                str = Encoding.Default.GetString(buffer);
                str = str.Replace("\r\n", "\t");
                tmp = str.Split('\t');


                //int p = this.lsvseardev.Items.Count;
                while (j < tmp.Length - 1)
                {

                    string[] sub_str = tmp[j].Split(',');
                    ZkAccess access = new(sub_str[1].Split('=')[1], sub_str[0].Split('=')[1], sub_str[2].Split('=')[1], sub_str[3].Split('=')[1], sub_str[4].Split('=')[1]);
                    foundDevices.Add(access);

                    i++;        //The next line of the list box
                    j++;        //The next column of each row
                }
            }
            return foundDevices;
        }

        public static bool AssignNewIpToController(string MacAddress, string IpToAssign)
        {

            string buffer = $"MAC={MacAddress},IPAddress={IpToAssign}";
            int ret = ModifyIPAddress("UDP", "255.255.255.255", buffer);
            if (ret >= 0)
            {
                return true;
            }
            return false;
        }


        public string TcpConnectDevice(string ip, string password = "", string port = "4370")
        {
            if (IntPtr.Zero == h)
            {
                h = Connect($"protocol=TCP,ipaddress={ip},port={port},timeout=2000,passwd={password}");
                return $"connected to {ip}";
            }
            return "not working";
        }


        //Operations for LockOut control
        public string LockOut(int LockNumber, [Range(0, 255)] int Delay)
        {
            var ret = ControlDevice(h, 1, LockNumber, 1, Delay, 1, "");

            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";

        }

        //Operations for AuxOut control
        public string AuxOut(int AuxNumber, [Range(0, 255)] int Delay)
        {
            var ret = ControlDevice(h, 1, AuxNumber, 2, Delay, 1, "");

            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";

        }

        //Cancel Alarm
        public string CancelAlarm()
        {
            var ret = ControlDevice(h, 2, 0, 0, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }

        //Restart Device
        public string RestartDevice()
        {
            var ret = ControlDevice(h, 3, 0, 0, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }
        

        //Set Door state 1 == always Normal Open, 2== Always Normal Close
        public string DoorState(int LockNumber,[Range(0,1)] int State)
        {
            var ret = ControlDevice(h,4, LockNumber, State, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }
    
    }
}
