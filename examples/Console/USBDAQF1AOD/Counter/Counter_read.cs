/// Counter_read.cs with synchronous mode.
///
/// This example demonstrates how to read counter with USBDAQF1AOD.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_Counter_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        try
        {
            dev.connect("default"); // Depend on your device
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Release device handle
            dev.close();
            return;
        }

        try
        {
            // Parameters setting
            int err;
            int channel = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open counter
            err = dev.Counter_open(channel, timeout:timeout);
            Console.WriteLine($"Counter_open in channel {channel}: {err}");

            // Start counter
            err = dev.Counter_start(channel, timeout:timeout);
            Console.WriteLine($"Counter_start in channel {channel}: {err}");

            // Read counter
            for (int i=0; i<10; i++)
            {
                // Read data acquisition acquisition
                int counter = dev.Counter_read(channel, timeout:timeout);
                Console.WriteLine($"Read counter in channel {channel}: {counter}");
            }

            // Stop counter
            err = dev.Counter_stop(channel, timeout:timeout);
            Console.WriteLine($"Counter_stop in channel {channel}: {err}");

            // Close counter
            err = dev.Counter_close(channel, timeout:timeout);
            Console.WriteLine($"Counter_close in channel {channel}: {err}");
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