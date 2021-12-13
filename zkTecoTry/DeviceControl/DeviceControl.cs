using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace zkTecoTry.DeviceControl
{
    class DeviceControl:HelperSdk
    {

        IntPtr h = IntPtr.Zero;
        public string TcpConnectDevice(string ip, string password="", string port = "4370")
        {
            if (IntPtr.Zero == h)
            {
                h = Connect($"protocol=TCP,ipaddress={ip},port={port},timeout=2000,passwd={password}");
                return $"connected to {ip}";
            }
            return "not working";
        }

        public void SerchDevices()
        {
            int ret = 0, i = 0, j = 0, k = 0;
            byte[] buffer = new byte[64 * 1024];
            string str = "";
            string[] filed = null;
            string[] tmp = null;
            string udp = "UDP";
            string adr = "255.255.255.255";

            Console.WriteLine("Start to SearchDevice!");

            ret = SearchDevice(udp, adr, ref buffer[0]);
            Console.WriteLine("ret searchdevice=" + ret);
            if (ret >= 0)
            {
                str = Encoding.Default.GetString(buffer);
                str = str.Replace("\r\n", "\t");
                tmp = str.Split('\t');

                //int p = this.lsvseardev.Items.Count;
                while (j < tmp.Length - 1)
                {

                    k = 0;
                    string[] sub_str = tmp[j].Split(',');
                    //MessageBox.Show(tmp[0]);

                    filed = sub_str[k++].Split('=');
                    string MacAddress = filed[1];

                    filed = sub_str[k++].Split('=');
                    string ipAddress = (filed[1]);

                    filed = sub_str[k++].Split('=');
                    string sNumber =(filed[1]);

                    filed = sub_str[k++].Split('=');
                    string model=(filed[1]);

                    filed = sub_str[k++].Split('=');
                    string softwareVersion = (filed[1]);

                    Console.WriteLine($"Ip Address : {ipAddress}, Mac Address : {MacAddress}, Serial Number : {sNumber}, Device Model : {model}, Software Version : {softwareVersion}");

                    i++;        //The next line of the list box
                    j++;        //The next column of each row
                }
            }
        }
    }
}
