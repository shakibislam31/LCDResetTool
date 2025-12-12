using System;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Threading;

namespace LCDResetTool
{
    class Program
    {
        // Your LCD's Vendor ID and Product ID
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
                // ResetDevice is implemented on IUsbDevice; cast and call there.
                var wholeUsbDevice = usbDevice as IUsbDevice;
                if (wholeUsbDevice != null)
                {
                    wholeUsbDevice.ResetDevice();
                    Console.WriteLine("Device reset successfully.");
                }
                else
                {
                    Console.WriteLine("Device does not implement IUsbDevice. Cannot call ResetDevice.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error resetting device: " + ex.Message);
            }
            finally
            {
                // Always clean up
                try
                {
                    usbDevice.Close();
                }
                catch { /* ignore close errors */ }
                UsbDevice.Exit();
            }

            Console.WriteLine("Done. You can close this window.");
            Thread.Sleep(2000); // Optional pause to see messages
        }
    }
}
