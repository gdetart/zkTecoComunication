using System;
using System.Text;

namespace zkTecoTry.DeviceControl
{
    class DbDevice
    {
        public static string GetTableData(IntPtr h, string TableName, string FilterOpt)
        {
            int BUFFERSIZE = 10 * 1024 * 1024;
            byte[] Buffer = new byte[BUFFERSIZE];
            var ret = HelperSdk.GetDeviceData(h, ref Buffer[0], BUFFERSIZE, TableName, "*", FilterOpt, "");
            if (ret >= 0)
            {
                return Encoding.Default.GetString(Buffer);

            }
            else
            {
                return "error";
            }
        }
        public static string WriteUserData(IntPtr h,User user)
        {
            int ret = 0; 
            string devtablename = "user";
            string data = "Pin=1999\tCardNo=135401\tPassword=1\tGroup=0\tStartTime=20211215\tEndTime=20221231";
            string options = "";
            ret = HelperSdk.SetDeviceData(h, devtablename, data, options);
            if (ret >= 0)
            {
                return "success";
            }
            return "error";
        }

        public static string DataCount(IntPtr h,string TableName)
        {
            int ret = 0; 
            string devtablename = "user"; 
            string devdatfilter = ""; 
            string options = ""; 
            ret = HelperSdk.GetDeviceDataCount(h, devtablename, devdatfilter, options);
            return Convert.ToString(ret);
        }

    }

    class User
    {
        public string Pin { get; set; }

        public int CardNumber { get; set; }
        public int Password { get; set; }
        public int Group { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SuperAuthorize { get; set; }

        public User(int card, DateTime startTime, DateTime endTime, int pass = 1234, int group = 0, int authorize=0) 
        {
            this.CardNumber = card;
            this.EndTime = endTime;
            this.Password = pass;
            this.Group = group;
            this.StartTime = startTime;
            this.SuperAuthorize = authorize;

        }
    }
}
