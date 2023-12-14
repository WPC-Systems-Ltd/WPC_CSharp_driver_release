/// AO_write_all_channels.cs with synchronous mode.
///
/// This example demonstrates the process of writing AO signal of STEM.
/// To begin with, it demonstrates the steps to open the AO.
/// Next, it outlines the procedure for writing digital signals simultaneously to the AO pins.
/// Finally, it concludes by explaining how to close the AO.

/// If your product is "STEM", please invoke the function `Sys_setAIOMode`.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AO_write_all_channels
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
            List<double> ao_value_list = new List<double>() {0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5};

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
                Console.WriteLine($"Sys_setAIOMode: {err}");
            }

            // Get slot mode
            slot_mode = dev.Sys_getMode(slot, timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // Open AO
            err = dev.AO_open(slot, timeout);
            Console.WriteLine($"AO_open in slot {slot}: {err}");

            // Write AO value simultaneously
            err = dev.AO_writeAllChannels(slot, ao_value_list, timeout);
            Console.WriteLine($"AO_writeAllChannels in slot {slot}: {err}");

            // Close AO
            err = dev.AO_close(slot, timeout);
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
