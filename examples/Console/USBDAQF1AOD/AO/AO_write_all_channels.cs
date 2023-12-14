/// AO_write_all_channels.cs with synchronous mode.
///
/// This example demonstrates the process of writing AO signal of USBDAQF1AOD.
/// To begin with, it demonstrates the steps to open the AO.
/// Next, it outlines the procedure for writing digital signals simultaneously to the AO pins.
/// Finally, it concludes by explaining how to close the AO.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AO_write_all_channels
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        try
        {
            dev.connect("default"); // Depend on your device
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
            int port = 0;
            int timeout = 3000; // ms
            List<double> ao_value_list = new List<double>() {0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5};

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO
            err = dev.AO_open(port, timeout);
            Console.WriteLine($"AO_open in port {port}: {err}");

            // Write AO value simultaneously
            err = dev.AO_writeAllChannels(port, ao_value_list, timeout);
            Console.WriteLine($"AO_writeAllChannels in port {port}: {err}");

            // Close AO
            err = dev.AO_close(port, timeout);
            Console.WriteLine($"AO_close in port {port}: {err}");
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
