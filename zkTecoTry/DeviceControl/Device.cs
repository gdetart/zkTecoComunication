using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace zkTecoTry.DeviceControl
{
    class Device
    {

        IntPtr h = IntPtr.Zero;
        private readonly string IpAddress;
        private readonly string MacAddress;
        private readonly string SerialNumber;
        private readonly string Model;
        private readonly string SoftwareVersion;
        private readonly int DoorNumber;
        private readonly int ReaderNumber;
        private readonly int AuxInNumber;
        private readonly int AuxOutNumber;




        public Device(string ip, string mac, string sn, string model, string version)
        {
            this.IpAddress = ip;
            this.MacAddress = mac;
            this.SerialNumber = sn;
            this.Model = model;
            this.SoftwareVersion = version;
            try
            {
                this.TcpConnectDevice(ip);
                if (h == IntPtr.Zero)
                {
                    Exception err = new("Device Could not Connect");
                    throw err;

                }

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);

            }


        }


        public static List<ZkAccess> SerchDevices()
        {
            int i = 0, j = 0;
            byte[] buffer = new byte[64 * 1024];
            string udp = "UDP";
            string adr = "255.255.255.255";

            List<ZkAccess> foundDevices = new();
            int ret = HelperSdk.SearchDevice(udp, adr, ref buffer[0]);
            if (ret >= 0)
            {
                string str = Encoding.Default.GetString(buffer);
                str = str.Replace("\r\n", "\t");
                string[] tmp = str.Split('\t');


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
            int ret = HelperSdk.ModifyIPAddress("UDP", "255.255.255.255", buffer);
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
                h = HelperSdk.Connect($"protocol=TCP,ipaddress={ip},port={port},timeout=2000,passwd={password}");
                return "Sucessfull";
            }
            return "UnSucessfull";
        }

        public void DissconnectDevice()
        {
            HelperSdk.Disconnect(h);
            h = IntPtr.Zero;
        }
        //Operations for LockOut control
        public string LockOut(int LockNumber, [Range(0, 255)] int Delay)
        {
            var ret = HelperSdk.ControlDevice(h, 1, LockNumber, 1, Delay, 1, "");

            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";

        }

        //Operations for AuxOut control
        public string AuxOut(int AuxNumber, [Range(0, 255)] int Delay)
        {
            var ret = HelperSdk.ControlDevice(h, 1, AuxNumber, 2, Delay, 1, "");

            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";

        }

        //Cancel Alarm
        public string CancelAlarm()
        {
            var ret = HelperSdk.ControlDevice(h, 2, 0, 0, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }

        //Restart Device
        public string RestartDevice()
        {
            var ret = HelperSdk.ControlDevice(h, 3, 0, 0, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }


        //Set Door state 1 == always Normal Open, 2== Always Normal Close
        public string DoorState(int LockNumber, [Range(0, 1)] int State)
        {
            var ret = HelperSdk.ControlDevice(h, 4, LockNumber, State, 0, 1, null);
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }

        public string SynchronizeTime()
        {

            DateTimeOffset now = DateTimeOffset.Now;
            var today = ((now.Year - 2000) * 12 * 31 + (now.Month - 1) * 31 + (now.Day - 1)) * (24 * 60 * 60) + now.Hour * 60 * 60 + now.Minute * 60 + now.Second;
            int ret = HelperSdk.SetDeviceParam(h, $"DateTime={today}");
            if (ret >= 0)
            {
                return "Sucessfull";
            }
            return "UnSucessfull";
        }


        public void SetDeviceParams(string param, string value)
        {
            int ret = HelperSdk.SetDeviceParam(h, $"{param}={value}");
            if (ret >= 0)
            {

            }
            else
            {
                var error = HelperSdk.PullLastError();
            }
        }

        public string GetDeviceParam(string param)
        {
            int BUFFERSIZE = 10 * 1024 * 1024;
            byte[] buffer = new byte[BUFFERSIZE];
            int ret = HelperSdk.GetDeviceParam(h, ref buffer[0], BUFFERSIZE, param);
            if (ret >= 0)
            {
                var bufferToString = Encoding.Default.GetString(buffer);
                var value = bufferToString.Split(',');
                string[] sub_str = value[0].Split('=');
                if (sub_str.Length >= 2)
                {
                    return sub_str[1].ToString();
                }
                else
                {
                    return "";
                }

            }
            else
            {
                return $"Faild with error {HelperSdk.PullLastError()}";
            }
        }
        
        public void getData()
        {
            Console.WriteLine(DbDevice.GetTableData(h,"user", ""));
        }
        
    }

}
