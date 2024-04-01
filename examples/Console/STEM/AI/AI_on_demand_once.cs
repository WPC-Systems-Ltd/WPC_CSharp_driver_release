/// AI_on_demand_once.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AI data in on demand mode.
/// Additionally, it retrieve AI data from STEM.
///
/// To begin with, it demonstrates the steps to open the AI and configure the AI parameters.
/// Next, it outlines the procedure for reading the AI on demand data.
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
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AI_on_demand_once
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
            int mode = Const.AI_MODE_ON_DEMAND;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get slot mode
            string slot_mode = dev.Sys_getMode(slot, timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // If the slot mode is not set to "AIO", set the slot mode to "AIO"
            if (slot_mode != "AIO"){
                err = dev.Sys_setAIOMode(slot, timeout);
                Console.WriteLine($"Sys_setAIOMode in slot {slot}, status: {err}");
            }

            // Get slot mode
            slot_mode = dev.Sys_getMode(slot, timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // Open AI
            err = dev.AI_open(slot, timeout);
            Console.WriteLine($"AI_open in slot {slot}, status: {err}");

            // Enable CS
            err = dev.AI_enableCS(slot, new List<int> {0, 1}, timeout);
            Console.WriteLine($"AI_enableCS in slot {slot}, status: {err}");

            // Set AI acquisition mode to on demand
            err = dev.AI_setMode(slot, mode, timeout);
            Console.WriteLine($"AI_setMode {mode} in slot {slot}, status: {err}");

            // Read data acquisition acquisition
            List<double> ai_list = dev.AI_readOnDemand(slot, timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));

            // Close AI
            err = dev.AI_close(slot, timeout);
            Console.WriteLine($"AI_close in slot {slot}, status: {err}");

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
