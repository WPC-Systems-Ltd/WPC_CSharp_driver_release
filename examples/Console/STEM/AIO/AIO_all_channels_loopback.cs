/// AIO_all_channels_loopback.cs  with synchronous mode.
///
/// This example demonstrates the process of AIO loopback across all channels of STEM.
/// It involves using AO pins to send signals and AI pins to receive signals on a single device, commonly referred to as "loopback".
/// The AI and AO pins are connected using a wire.
///
/// Initially, the example demonstrates the steps required to open the AI and AO.
/// Next, it reads AI data and displays its corresponding values.
/// Following that, it writes digital signals to the AO pins and reads AI on-demand data once again.
/// Lastly, it closes the AO and AI ports.

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

class STEM_AIO_all_channels_loopback
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
            int timeout = 3000; // ms

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

            // Open AO
            err = dev.AO_open(slot, timeout:timeout);
            Console.WriteLine($"AO_open in slot {slot}: {err}");

            // Enable CS
            err = dev.AI_enableCS(slot, new List<int> {0, 1}, timeout:timeout);
            Console.WriteLine($"AI_enableCS in slot {slot}: {err}");

            // Read data acquisition acquisition
            List<double> sample = dev.AI_readOnDemand(slot, timeout:timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Write AO value simultaneously
            // CH0~CH1 5V, CH2~CH3 3V, CH4~CH5 2V, CH6~CH7 0V
            List<double> AO_values = new List<double> { 5, 5, 3, 3, 2, 2, 0, 0 };
            err = dev.AO_writeAllChannels(slot, AO_values, timeout:timeout);
            Console.WriteLine($"AO_writeAllChannels in slot {slot}: {err}");

            // Read data acquisition acquisition
            sample = dev.AI_readOnDemand(slot, timeout:timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Close AI
            err = dev.AI_close(slot, timeout:timeout);
            Console.WriteLine($"AI_close in slot {slot}: {err}");

            // Close AO
            err = dev.AO_close(slot, timeout:timeout);
            Console.WriteLine($"AO_close in slot {slot}: {err}");
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
