using System;
using zkTecoTry.DeviceControl;

namespace zkTecoTry
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = new();

            string ok = "20211221";
            date = Convert.ToDateTime($"{ok.Substring(0, 4)}-{ok.Substring(4, 2)}-{ok[6..]}");

            Console.WriteLine($"{ok[6..]}/{ok.Substring(4, 2)}/{ok.Substring(0, 4)}");
            Console.WriteLine(date.Year);

        }
    }
}
