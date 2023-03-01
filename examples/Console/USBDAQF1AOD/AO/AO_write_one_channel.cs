/// AO_write_one_channel.cs with synchronous mode.
///
/// This example demonstrates how to write AO in specific channels from USBDAQF1AOD.
///
/// First, it shows how to open AO in port.
///
/// Second, write digital signals in specific channels.
///
/// Last, close AO in port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AO_write_one_channel
{
    static public void Main()
    {
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
            int timeout = 3000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO port
            err = dev.AO_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Set AO port and write data 1.5(V) in channel 4
            err = dev.AO_writeOneChannel(port, 4, 1.5, timeout);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AO port and write data 2.5(V) in channel 5
            err = dev.AO_writeOneChannel(port, 5, 2.5, timeout);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AO port and write data 3.5(V) in channel 6
            err = dev.AO_writeOneChannel(port, 6, 3.5, timeout);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AO port and write data 4.5(V) in channel 7
            err = dev.AO_writeOneChannel(port, 7, 4.5, timeout);
            Console.WriteLine($"writeOneChannel: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close AO port
            err = dev.AO_close(port, timeout);
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