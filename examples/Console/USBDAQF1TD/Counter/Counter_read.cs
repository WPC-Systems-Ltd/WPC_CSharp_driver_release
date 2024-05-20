/// Counter_read.cs with synchronous mode.
///
/// This example demonstrates how to read counter with USBDAQF1TD.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_Counter_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

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
            ulong position = 0;
            int edge = 0;  // 0: Falling edge, 1: Rising edge
            float window_size = 100;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open counter
            err = dev.Counter_open(channel, timeout);
            Console.WriteLine($"Counter_open in channel {channel}, status: {err}");

            // Set counter edge
            err = dev.Counter_setEdge(channel, edge, timeout);
            Console.WriteLine($"Counter_setEdge in channel {channel}, status: {err}");

            // Set counter position
            err = dev.Counter_setPosition(channel, position, timeout);
            Console.WriteLine($"Counter_setPosition in channel {channel}, status: {err}");

            // Set counter frequency window size
            err = dev.Counter_setFreqWindow(channel, window_size, timeout);
            Console.WriteLine($"Counter_setFreqWindow in channel {channel}, status: {err}");

            // Start counter
            err = dev.Counter_start(channel, timeout);
            Console.WriteLine($"Counter_start in channel {channel}, status: {err}");

            // Read counter position
            for (int i=0; i<10; i++)
            {
                ulong posi = dev.Counter_readPosition(channel, timeout);
                Console.WriteLine($"Read counter in channel {channel}: {posi}");
            }

            // Stop counter
            err = dev.Counter_stop(channel, timeout);
            Console.WriteLine($"Counter_stop in channel {channel}, status: {err}");

            // Close counter
            err = dev.Counter_close(channel, timeout);
            Console.WriteLine($"Counter_close in channel {channel}, status: {err}");
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