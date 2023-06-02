/// AI_continuous_with_logger.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in continuous mode and saving it into a CSV file.
/// Additionally, it utilizes a loop to retrieve AI data with 8 channels from WifiDAQE3A with a timeout of 100 ms.
///
/// To begin with, it demonstrates the steps to open the AI port and configure the AI parameters.
/// Next, it outlines the procedure for reading and saving the streaming AI data.
/// Finally, it concludes by explaining how to close the AI port.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_DataLogger_AI_continuous
{
    static void loop_func(WifiDAQE3A handle, int port, int num_of_samples=600, int delay=50, int exit_loop_time=300)
    {
        int time_cal = 0;
        while (time_cal < exit_loop_time)
        {
            // Read data
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> sample in streaming_list)
            {
                Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

                // Write data into CSV
                handle.Logger_writeList(sample);
            }

            // Wait
            Thread.Sleep(delay); // delay [ms]
            time_cal += delay;
        }
    }

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

        try
        {
            // Parameters setting
            int err;
            int port = 1;
            int mode = Const.AI_MODE_CONTINUOUS;
            float sampling_rate = 1000;
            int timeout = 3000; // ms

            // Open file with CSV file
            err = dev.Logger_openFile("WPC_tester_WifiDAQE3A_AI.csv");
            Console.WriteLine($"Logger_openFile: {err}");

            // Write header into CSV file
            var header = $"CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7";
            err = dev.Logger_writeValue(header);
            Console.WriteLine($"Logger_writeValue: {err}");

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");
            
            // Set AI acquisition mode to continuous
            err = dev.AI_setMode(port, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode} in port{port}: {err}");

            // Set AI sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout:timeout);
            Console.WriteLine($"AI_setSamplingRate {sampling_rate} in port{port}: {err}");

            // Start AI acquisition
            err = dev.AI_start(port, timeout:timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");

            // loop parameters
            int num_of_samples = 200;
            int delay = 50;
            int exit_loop_time = 100;

            // Start loop
            loop_func(dev, port, num_of_samples:num_of_samples, delay:delay, exit_loop_time:exit_loop_time);

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