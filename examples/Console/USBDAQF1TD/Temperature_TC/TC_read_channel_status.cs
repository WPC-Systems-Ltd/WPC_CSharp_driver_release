/// TC_read_channel_status.cs
/// 
/// This example demonstrates how to get status from USBDAQF1TD.
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
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_TC_read_channel_status
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

        // Connect to device
        dev.connect("21JA1239");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int status;
            int port = 1;
            int channel_0 = 0;
            int channel_1 = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open thermo port
            err = dev.Thermal_open(port);
            Console.WriteLine($"open: {err}");

            // Set thermo port and get status in channel 0 
            status = dev.Thermal_getStatus(port, channel_0);
            Console.WriteLine($"Thermal_getStatus in channel 0: {status}");
            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set thermo port and get status in channel 1 
            status = dev.Thermal_getStatus(port, channel_1);
            Console.WriteLine($"Thermal_getStatus in channel 1: {status}");

            // Close thermo port
            err = dev.Thermal_close(port);
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