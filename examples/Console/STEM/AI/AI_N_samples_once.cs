/// AI_N_samples_once.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in N-sample mode.
/// Additionally, it gets AI data with 50 points in once from STEM.
///
/// To begin with, it demonstrates the steps to open the AI port and configure the AI parameters.
/// Next, it outlines the procedure for reading the streaming AI data.
/// Finally, it concludes by explaining how to close the AI port.

/// If your product is "STEM", please invoke the function `Sys_setPortAIOMode` and `AI_enableCS`.
/// Example: AI_enableCS is {0, 2}
/// Subsequently, the returned value of AI_readOnDemand and AI_readStreaming will be displayed as follows.
/// data:
///           CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7, CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7
///           |                                     |                                      |
///           |---------------- CS0-----------------|---------------- CS2------------------|
/// [sample0]
/// [sample1]
///    .
///    .
///    .
/// [sampleN]

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AI_N_samples_once
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        STEM dev = new STEM();

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
            int delay = 10;
            int port = 1;
            int mode = Const.AI_MODE_N_SAMPLE;
            int samples = 50;
            float sampling_rate = 1000;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Get port mode
            string port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // If the port mode is not set to "AIO", set the port mode to "AIO"
            if (port_mode != "AIO"){
                err = dev.Sys_setPortAIOMode(port, timeout:timeout);
                Console.WriteLine($"Sys_setPortAIOMode: {err}");
            }
            // Get port mode
            port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Enable CS
            err = dev.AI_enableCS(port, new List<int> {0, 1}, timeout:timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");
            
            // Set AI acquisition mode to N-sample mode
            err = dev.AI_setMode(port, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode} in port{port}: {err}");

            // Set AI # of samples to 50 (pts)
            err = dev.AI_setNumSamples(port, samples, timeout:timeout);
            Console.WriteLine($"AI_setNumSamples {samples} in port{port}: {err}");

            // Set AI sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout:timeout);
            Console.WriteLine($"AI_setNumSamples {sampling_rate} in port{port}: {err}");

            // Start AI acquisition
            err = dev.AI_start(port, timeout:timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");

            // Read data
            List<List<double>> streaming_list = dev.AI_readStreaming(port, samples, delay);

            // Print data
            foreach (List<double> sample in streaming_list)
            {
                Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));
            }

            // Stop AI
            err = dev.AI_stop(port, timeout:timeout);
            Console.WriteLine($"AI_stop in port{port}: {err}");

            // Close AI port
            err = dev.AI_close(port, timeout:timeout);
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