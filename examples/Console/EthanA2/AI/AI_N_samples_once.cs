/// AI_N_samples_once.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in N-sample mode.
/// Additionally, it gets AI data with points in once from EthanA2.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the streaming AI data.
/// Finally, it concludes by explaining how to close the AI.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanA2_AI_N_samples_once
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
            int mode = Const.AI_MODE_N_SAMPLE;
            float sampling_rate = 200;
            int samples = 200;
            int read_points = 200;
            int read_delay = 200; // ms
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port {port}, status: {err}");
            

            // Set AI acquisition mode to N-sample mode
            err = dev.AI_setMode(port, mode, timeout);
            Console.WriteLine($"AI_setMode {mode} in port {port}, status: {err}");

            // Set AI sampling rate
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"AI_setSamplingRate {sampling_rate} in port {port}, status: {err}");

            // Set AI # of samples
            err = dev.AI_setNumSamples(port, samples, timeout);
            Console.WriteLine($"AI_setNumSamples {samples} in port {port}, status: {err}");

            // Start AI
            err = dev.AI_start(port, timeout);
            Console.WriteLine($"AI_start in port {port}, status: {err}");

            // Wait a while for data acquisition
            Thread.Sleep(1000); // delay [ms]

            // Read data acquisition
            List<List<double>> ai_2Dlist = dev.AI_readStreaming(port, read_points, read_delay);
            Console.WriteLine($"Number of samples = {ai_2Dlist.Count}");

            bool ok = true;
            foreach (List<double> ai_list in ai_2Dlist)
            {
                if (ai_list.Count != channel){
                    Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));
                    ok = false;
                }
            }

            if (ok)
                Console.WriteLine("OK");
            else
                Console.WriteLine("NG");

            // Stop AI
            err = dev.AI_stop(port, timeout);
            Console.WriteLine($"AI_stop in port {port}, status: {err}");

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
