/// Encoder_read.cs with synchronous mode.
///
/// This example demonstrates how to read encoder with USBDAQF1AD.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AD_Encoder_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

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
            int channel = 0;
            int timeout = 3000; // ms 
            long position = 0; 
            int direction = 1;  // 1: Forward, -1: Reverse
            float window_size = 100;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open encoder
            err = dev.Encoder_open(channel, timeout);
            Console.WriteLine($"Encoder_open in channel {channel}, status: {err}");

            // Set encoder direction
            err = dev.Encoder_setDirection(channel, direction, timeout);
            Console.WriteLine($"Encoder_setDirection in channel {channel}, status: {err}");
 
            // Set encoder position
            err = dev.Encoder_setPosition(channel, position, timeout);
            Console.WriteLine($"Encoder_setPosition in channel {channel}, status: {err}");

 
            // Set encoder frequency window size
            err = dev.Encoder_setFreqWindow(channel, window_size, timeout);
            Console.WriteLine($"Encoder_setFreqWindow in channel {channel}, status: {err}");

            // Start encoder
            err = dev.Encoder_start(channel, timeout);
            Console.WriteLine($"Encoder_start in channel {channel}, status: {err}");
            
            // Read encoder position
            for (int i=0; i<10; i++)
            {
                long posi = dev.Encoder_readPosition(channel, timeout);
                Console.WriteLine($"Encoder position in channel {channel}: {posi}");
            }

            // Stop encoder
            err = dev.Encoder_stop(channel, timeout);
            Console.WriteLine($"Encoder_stop in channel {channel}, status: {err}");

            // Close encoder
            err = dev.Encoder_close(channel, timeout);
            Console.WriteLine($"Encoder_close in channel {channel}, status: {err}");
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