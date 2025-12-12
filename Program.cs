using System;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Threading;

namespace LCDResetTool
{
    class Program
    {
        const int VID = 0x38C1;
        const int PID = 0x0004;

        static void Main(string[] args)
        {
            Console.WriteLine("LCD Reset Tool Starting...");

            UsbDeviceFinder myUsbFinder = new UsbDeviceFinder(VID, PID);
            UsbDevice usbDevice = UsbDevice.OpenUsbDevice(myUsbFinder);

            if (usbDevice == null)
            {
                Console.WriteLine("LCD device not found. Make sure it's connected.");
                return;
            }

            Console.WriteLine("LCD device found. Performing soft reset...");

            try
            {
                usbDevice.ResetDevice();
                Console.WriteLine("Device reset successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error resetting device: " + ex.Message);
            }

            usbDevice.Close();
            UsbDevice.Exit();

            Console.WriteLine("Done. You can close this window.");
            Thread.Sleep(2000);
        }
    }
}
