

/// AI_continuous_multi_slot.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in continuous mode with multi slot from STEM.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the streaming AI data.
/// Finally, it concludes by explaining how to close the AI.
///
/// Please invoke the function `Sys_setAIOMode_async` and `AI_enableCS_async`.
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
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AI_continuous_multi_slot
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

        // Parameters setting
        int err;
        List<int> slot_list = new List<int>() {1, 2};
        int mode = Const.AI_MODE_CONTINUOUS;
        float sampling_rate = 200;
        int read_points = 200;
        int read_delay = 200; // ms
        int timeout = 3000; // ms
        List<int> chip_select = new List<int>() {0, 1};

        try
        {
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            foreach (var slot in slot_list){
                // Get slot mode
                string slot_mode = dev.Sys_getMode(slot, timeout);
                Console.WriteLine($"Slot mode: {slot_mode}");

                // If the slot mode is not set to "AIO", set the slot mode to "AIO"
                if (slot_mode != "AIO"){
                    err = dev.Sys_setAIOMode(slot, timeout);
                    Console.WriteLine($"Sys_setAIOMode: {err}");
                }

                // Get slot mode
                slot_mode = dev.Sys_getMode(slot, timeout);
                Console.WriteLine($"Slot mode: {slot_mode}");

                // Open AI
                err = dev.AI_open(slot, timeout);
                Console.WriteLine($"AI_open in slot {slot}: {err}");

                // Enable CS
                err = dev.AI_enableCS(slot, chip_select, timeout);
                Console.WriteLine($"AI_enableCS in slot {slot}: {err}");

                // Set AI acquisition mode to continuous mode
                err = dev.AI_setMode(slot, mode, timeout);
                Console.WriteLine($"AI_setMode {mode} in slot {slot}: {err}");

                // Set AI sampling rate
                err = dev.AI_setSamplingRate(slot, sampling_rate, timeout);
                Console.WriteLine($"AI_setSamplingRate {sampling_rate} in slot {slot}: {err}");

                // Start AI
                err = dev.AI_start(slot, timeout);
                Console.WriteLine($"AI_start in slot {slot}: {err}");
            }

            // Wait a while for data acquisition
            Thread.Sleep(1000); // delay [ms]

            foreach (var slot in slot_list){
                // Stop AI
                err = dev.AI_stop(slot, timeout);
                Console.WriteLine($"AI_stop in slot {slot}: {err}");
            }

            int data_len = 1;
            while (data_len > 0){
                // Read data acquisition
                foreach (var slot in slot_list){
                    List<List<double>> ai_2Dlist = dev.AI_readStreaming(slot, read_points, read_delay);
                    Console.WriteLine($"In slot {slot}, number of samples = {ai_2Dlist.Count}");

                    // Update data len
                    data_len = ai_2Dlist.Count;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            foreach (var slot in slot_list){
                // Close AI
                err = dev.AI_close(slot, timeout);
                Console.WriteLine($"AI_close in slot {slot}: {err}");
            }
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();
    }
}

