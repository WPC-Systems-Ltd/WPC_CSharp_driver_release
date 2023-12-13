/// AI_continuous.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in continuous mode with 8 channels from EthanA.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the streaming AI data.
/// Finally, it concludes by explaining how to close the AI.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanA_AI_continuous
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanA dev = new EthanA();

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
            int mode = Const.AI_MODE_CONTINUOUS;
            float sampling_rate = 200;
            int read_points = 200;
            int delay = 200;   // ms
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port {port}: {err}");
            

            // Set AI acquisition mode to continuous mode
            err = dev.AI_setMode(port, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode} in port {port}: {err}");

            // Set AI sampling rate
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout:timeout);
            Console.WriteLine($"AI_setSamplingRate {sampling_rate} in port {port}: {err}");

            // Start AI
            err = dev.AI_start(port, timeout:timeout);
            Console.WriteLine($"AI_start in port {port}: {err}");

            // Wait a while for data acquisition
            Thread.Sleep(1000); // delay [ms]

            // Stop AI
            err = dev.AI_stop(port, timeout:timeout);
            Console.WriteLine($"AI_stop in port {port}: {err}");

            int data_len = 1;
            while (data_len > 0){
                // Read data acquisition
                List<List<double>> ai_2Dlist = dev.AI_readStreaming(port, read_points, delay:delay);
                Console.WriteLine($"number of samples = {ai_2Dlist.Count}");

                // Update data len
                data_len = ai_2Dlist.Count;
            }

            // Close AI
            err = dev.AI_close(port, timeout:timeout);
            Console.WriteLine($"AI_close in port {port}: {err}");
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
