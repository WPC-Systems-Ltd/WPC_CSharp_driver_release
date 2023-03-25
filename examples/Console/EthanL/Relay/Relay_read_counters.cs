/// Relay_read_counters.cs with synchronous mode.
///
/// This example demonstrates how to read counters from EthanL.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanL_Relay_read_counters
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanL dev = new EthanL();

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
            int index = -1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open Relay open
            err = dev.Relay_open(timeout:timeout);
            Console.WriteLine($"Relay_open: {err}");

            // Read counters
            List<int> counter = dev.Relay_read(index:index, timeout:timeout);

            foreach (int c in counter)
            {
                Console.WriteLine($"Relay_read: {c}");
            }
            err = dev.Relay_close(timeout:timeout);
            Console.WriteLine($"Relay_close: {err}");
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