/// AI_continuous_with_logger.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in continuous mode and saving it into a CSV file.
/// Additionally, it utilizes a loop to retrieve AI data with 8 channels from STEM.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading and saving the streaming AI data.
/// Finally, it concludes by explaining how to close the AI.

/// If your product is "STEM", please invoke the function Sys_setAIOMode and AI_enableCS.
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

class STEM_DataLogger_AI_continuous
{
    static void loop_func(STEM handle, int slot, int samples, int delay, int exit_time)
    {
        List<List<double>> streaming_list;
        int time_cal = 0;
        while (time_cal < exit_time)
        {
            // Read data acquisition
            streaming_list = handle.AI_readStreaming(slot, samples, delay);

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
            int slot = 1; // Connect AIO module to slot
            int mode = Const.AI_MODE_CONTINUOUS;
            float sampling_rate = 1000;
            int timeout = 3000; // ms

            // Open file with CSV file
            err = dev.Logger_openFile("WPC_tester_STEM_AI.csv");
            Console.WriteLine($"Logger_openFile: {err}");

            // Write header into CSV file
            var header = $"CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7";
            err = dev.Logger_writeValue(header);
            Console.WriteLine($"Logger_writeValue: {err}");

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get slot mode
            string slot_mode = dev.Sys_getMode(slot, timeout:timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // If the slot mode is not set to "AIO", set the slot mode to "AIO"
            if (slot_mode != "AIO"){
                err = dev.Sys_setAIOMode(slot, timeout:timeout);
                Console.WriteLine($"Sys_setAIOMode: {err}");
            }

            // Get slot mode
            slot_mode = dev.Sys_getMode(slot, timeout:timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // Open AI
            err = dev.AI_open(slot, timeout:timeout);
            Console.WriteLine($"AI_open in slot {slot}: {err}");

            // Enable CS
            err = dev.AI_enableCS(slot, new List<int> {0, 1}, timeout:timeout);
            Console.WriteLine($"AI_enableCS in slot {slot}: {err}");

            // Set AI acquisition mode to continuous
            err = dev.AI_setMode(slot, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode} in slot {slot}: {err}");

            // Set AI sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(slot, sampling_rate, timeout:timeout);
            Console.WriteLine($"AI_setSamplingRate {sampling_rate} in slot {slot}: {err}");

            // Start AI acquisition
            err = dev.AI_start(slot, timeout:timeout);
            Console.WriteLine($"AI_start in slot {slot}: {err}");

            // loop parameters
            int get_samples = 200;
            int delay = 50;
            int exit_time = 100;

            // Start loop
            loop_func(dev, slot, get_samples, delay, exit_time);

            // Stop AI
            err = dev.AI_stop(slot, timeout:timeout);
            Console.WriteLine($"AI_stop in slot {slot}: {err}");

            // Close AI
            err = dev.AI_close(slot, timeout:timeout);
            Console.WriteLine($"AI_close in slot {slot}: {err}");
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
