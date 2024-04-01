/// DPOT_readByChannel.cs with synchronous mode.
///
/// This example demonstrates how to read digital potentiometer resistance by channel from EthanP.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanP_DPOT_readByChannel
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanP dev = new EthanP();

        // Connect to device
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
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
            int port = 0;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open DPOT
            err = dev.DPOT_open(port, timeout);
            Console.WriteLine($"DPOT_open in port {port}, status: {err}");

            // Read all channels by for loop
            for (int i=0; i<4; i++)
            {
                float resistance_ratio = dev.DPOT_readByChannel(i, timeout);
                Console.WriteLine($"resistance_ratio in channel {i}: {resistance_ratio}");
            }

            // Close DPOT
            err = dev.DPOT_close(port, timeout);
            Console.WriteLine($"DPOT_close in port {port}, status: {err}");
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