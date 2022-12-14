/// AO_write_all_channels.cs
/// 
/// This example demonstrates how to write AO in all channels from USBDAQF1AOD.
/// 
/// First, it shows how to open AO in port.
/// 
/// Second, write all digital signals
/// 
/// Last, close AO in port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AO_write_all_channels
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        dev.connect("21JA1439");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO port
            err = dev.AO_open(port);
            Console.WriteLine($"open: {err}");

            // Set AO port and write data simultaneously
            List<double> AO_values = new List<double> { 3, 1, 2, 3, 4, 5, 4, 3 };

            err = dev.AO_writeAllChannels(port, AO_values);
            Console.WriteLine($"writeAllChannels: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close AO port
            err = dev.AO_close(port);
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