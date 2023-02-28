/// RTD_read_channel_status.cs with synchronous mode.
///
/// This example demonstrates how to get status in two channels from USBDAQF1RD.
///
/// First, it shows how to open thermal port
///
/// Second, get status from channel 0 and channel 1
///
/// Last, close thermal port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1RD_RTD_read_channel_status
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

        // Connect to device
        dev.connect("21JA1385");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int err;
            int port = 1;
            int channel_0 = 0;
            int channel_1 = 1;
            int timeout = 3000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open RTD port
            err = dev.Thermal_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set RTD port and get status in channel 0
            status = dev.Thermal_getStatus(port, channel_0, timeout);
            Console.WriteLine($"Thermal_getStatus in chaannel 0: {status}");

            // Set RTD port and get status in channel 1
            status = dev.Thermal_getStatus(port, channel_1, timeout);
            Console.WriteLine($"Thermal_getStatus in chaannel 1: {status}");

            // Close RTD port
            err = dev.Thermal_close(port, timeout);
            Console.WriteLine($"close: {err}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();
    }
}