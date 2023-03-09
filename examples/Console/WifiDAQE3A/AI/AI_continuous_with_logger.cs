/// AI_continuous_with_logger.cs with synchronous mode.
///
/// This example demonstrates how to get AI data and write to CSV file in continuous mode from WifiDAQE3A.
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

class WifiDAQE3A_DataLogger_AI_continuous
{
    static void loop_func(WifiDAQE3A handle, Datalogger handle2, int port, int num_of_samples, int delay = 50, int exit_loop_time = 300)
    {
        int time_cal = 0;
        while (time_cal < exit_loop_time)
        {
            // Data acquisition
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> s in streaming_list)
            {
                // Read acquisition data
                Console.WriteLine($"{s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");

                // Write data into CSV
                handle2.Logger_writeList(s);
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

        // Create datalogger handle
        Datalogger dev_logger = new Datalogger();

        // Open file with WPC_test.csv
        dev_logger.Logger_openFile("WPC_tester_WifiDAQE3A_AI.csv");

        // Write header into CSV file
        var data = $"CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7";
        dev_logger.Logger_writeValue(data);

        // Perform DAQ basic information
        try
        {
            // Parameters setting
            int err;
            int port = 1;
            float sampling_rate = 1000;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Set AI port and acquisition mode to continuous
            err = dev.AI_setMode(port, Const.AI_MODE_CONTINUOUS, timeout);
            Console.WriteLine($"AI_setMode in port{port}: {err}");

            // Set AI port and sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"AI_setSamplingRate in port{port}: {err}");

            // Set AI port and start acquisition
            err = dev.AI_start(port, timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");

            int num_of_samples = 600;
            int delay = 50;
            int exit_loop_time = 300;

            // Start loop
            loop_func(dev, dev_logger, port, num_of_samples, delay, exit_loop_time);

            // Stop AI
            err = dev.AI_stop(port, timeout);
            Console.WriteLine($"AI_stop in port{port}: {err}");

            // Close AI port
            err = dev.AI_close(port, timeout);
            Console.WriteLine($"AI_close in port{port}: {err}");

            // Close File
            dev_logger.Logger_closeFile();
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