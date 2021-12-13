using System;
using System.Runtime.InteropServices;

namespace zkTecoTry.DeviceControl
{
    class HelperSdk
    {
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);

        //4.4 call GetDeviceParam function
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceParam")]
        public static extern int GetDeviceParam(IntPtr h, ref byte buffer, int buffersize, string itemvalues);

        //4.3 call SetDeviceParam function
        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceParam")]
        public static extern int SetDeviceParam(IntPtr h, string itemvalues);
        //4.5 call ControlDevice function

        [DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
        public static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);

        //4.8 call GetDeviceDataCount function
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceDataCount")]
        public static extern int GetDeviceDataCount(IntPtr h, string tablename, string filter, string options);

        //4.7 call GetDeviceData function
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceData")]
        public static extern int GetDeviceData(IntPtr h, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options);

        //4.6 call SetDeviceData function
        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceData")]
        public static extern int SetDeviceData(IntPtr h, string tablename, string data, string options);

        //4.10 call GetRTLog function
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);

        //4.11 call SearchDevice function
        [DllImport("plcommpro.dll", EntryPoint = "SearchDevice")]
        //public static extern int SearchDevice( ref byte commtype, ref byte address, ref byte buffer);
        public static extern int SearchDevice(string commtype, string address, ref byte buffer);

        //4.12 call ModifyIPAddress function

        [DllImport("plcommpro.dll", EntryPoint = "ModifyIPAddress")]
        public static extern int ModifyIPAddress(string commtype, string address, string buffer);


        //4.15 cal GetDeviceFileData function
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceFileData")]
        public static extern int GetDeviceFileData(IntPtr h, ref byte buffer, ref int buffersize, string filename, string options);

        //4.14 call SetDeviceFileData function
        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceFileData")]
        public static extern int SetDeviceFileData(IntPtr h, string filename, ref byte buffer, int buffersize, string options);

        //4.9 call DeleteDeviceData function
        [DllImport("plcommpro.dll", EntryPoint = "DeleteDeviceData")]
        public static extern int DeleteDeviceData(IntPtr h, string tablename, string data, string options);

        //Backup Data
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "ProcessBackupData")]
        public static extern int ProcessBackupData(byte[] data, int fileLen, ref byte Buffer, int BufferSize);




    }
}
