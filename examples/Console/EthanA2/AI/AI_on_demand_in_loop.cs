/// AI_on_demand_in_loop.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in on demand mode.
/// Additionally, it utilizes a loop to retrieve AI data with 5 times from EthanA2.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the AI on demand data.
/// Finally, it concludes by explaining how to close the AI.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanA2_AI_on_demand_in_loop
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanA2 dev = new EthanA2();

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
            int channel = 8;
            int mode = Const.AI_MODE_ON_DEMAND;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port {port}, status: {err}");
            

            // Set AI acquisition mode to on demand
            err = dev.AI_setMode(port, mode, timeout);
            Console.WriteLine($"AI_setMode {mode} in port {port}, status: {err}");

            // Read AI data with 5 times
            for (int i=0; i<5; i++)
            {
                // Read data acquisition
                List<double> ai_list = dev.AI_readOnDemand(port);
                Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));
            }

            // Close AI
            err = dev.AI_close(port, timeout);
            Console.WriteLine($"AI_close in port {port}, status: {err}");
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
