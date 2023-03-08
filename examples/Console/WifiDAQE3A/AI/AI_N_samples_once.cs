/// AI_N_samples_once.cs with synchronous mode.
///
/// This example demonstrates how to get AI data in once in N sample mode from WifiDAQE3A.
///
/// First, it shows how to open AI port and configure AI parameters.
///
/// Second, read AI streaming data.
///
/// Last, close AI port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_AI_N_samples_once
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Connect to device
        try
        {
            dev.connect("192.168.5.79"); // Depend on your device
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Release device handle
            dev.close();
            return;
        }

        // Execute
        try
        {
            // Parameters setting
            int err;
            int delay = 10;
            int port = 1;
            int samples = 50;
            float sampling_rate = 1000;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Set AI port and acquisition mode to N-sample mode
            err = dev.AI_setMode(port, Const.AI_MODE_N_SAMPLE, timeout);
            Console.WriteLine($"AI_setMode in port{port}: {err}");

            // Set AI port and # of samples to 5 (pts)
            err = dev.AI_setNumSamples(port, samples, timeout);
            Console.WriteLine($"AI_setNumSamples in port{port}: {err}");

            // Set AI port and sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"AI_setNumSamples in port{port}: {err}");

            // Set AI port and start acquisition
            err = dev.AI_start(port, timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Set AI port and data acquisition
            List<List<double>> streaming_list = dev.AI_readStreaming(port, samples, delay);

            // Read acquisition data
            foreach (List<double> sample in streaming_list)
            {
                Console.WriteLine($"{sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

            // Stop AI
            err = dev.AI_stop(port, timeout);
            Console.WriteLine($"AI_stop in port{port}: {err}");

            // Close AI port
            err = dev.AI_close(port, timeout);
            Console.WriteLine($"AI_close in port{port}: {err}");
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