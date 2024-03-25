/// Relay_set_channel.cs with synchronous mode.
///
/// This example demonstrates how to read counters from EthanL.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanL_Relay_set_channel
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
            int DO_port = 0;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open Relay open
            err = dev.Relay_open(timeout);
            Console.WriteLine($"Relay_open: {err}");

            // Toggle digital state for 10 times. Each times delay for 0.5 second
            for (int i=0; i<10; i++)
            {
                if (i % 2 == 0)
                {
                    err = dev.DO_writePort(DO_port, new List<int> { 0, 0, 0, 0, 0, 0 }, timeout);
                }
                else
                {
                    err = dev.DO_writePort(DO_port, new List<int> { 1, 1, 1, 1, 1, 1 }, timeout);
                }
                Console.WriteLine($"DO_writePort in port {DO_port}: {err}");

                // Wait
                Thread.Sleep(500); // delay [ms]
            }
            err = dev.Relay_close(timeout);
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