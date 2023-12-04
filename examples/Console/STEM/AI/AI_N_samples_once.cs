/// AI_N_samples_once.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in N-sample mode.
/// Additionally, it gets AI data with points in once from STEM.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the streaming AI data.
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
            int slot = 1; // Connect AIO module to slot
            int mode = Const.AI_MODE_N_SAMPLE;
            float sampling_rate = 200;
            int samples = 200;
            int read_points = 200;
            int delay = 200;   // ms
            int timeout = 3000; // ms
            List<int> chip_select = new List<int>() {0, 1};

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
            err = dev.AI_enableCS(slot, chip_select, timeout:timeout);
            Console.WriteLine($"AI_enableCS in slot {slot}: {err}");

            // Set AI acquisition mode to N-sample mode
            err = dev.AI_setMode(slot, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode} in slot {slot}: {err}");

            // Set AI sampling rate
            err = dev.AI_setSamplingRate(slot, sampling_rate, timeout:timeout);
            Console.WriteLine($"AI_setNumSamples {sampling_rate} in slot {slot}: {err}");

            // Set AI # of samples
            err = dev.AI_setNumSamples(slot, samples, timeout:timeout);
            Console.WriteLine($"AI_setNumSamples {samples} in slot {slot}: {err}");

            // Start AI
            err = dev.AI_start(slot, timeout:timeout);
            Console.WriteLine($"AI_start in slot {slot}: {err}");

            // Wait a while for data acquisition
            Thread.Sleep(1000); // delay [ms]

            // Read data acquisition
            List<List<double>> ai_2Dlist = dev.AI_readStreaming(slot, read_points, delay:delay);
            Console.WriteLine($"number of samples = {ai_2Dlist.Count}");

            bool ok = true;
            foreach (List<double> ai_list in ai_2Dlist)
            {
                if (ai_list.Count != chip_select.Count*8){
                    Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));
                    ok = false;
                }
            }

            if (ok)
                Console.WriteLine("OK");
            else
                Console.WriteLine("NG");

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
