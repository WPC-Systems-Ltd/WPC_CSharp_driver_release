/// AO_write_one_channel.cs with synchronous mode.
///
/// This example demonstrates the process of writing AO signal of STEM.
/// To begin with, it demonstrates the steps to open the AO port.
/// Next, it outlines the procedure for writing digital signals with channel to the AO pins.
/// Finally, it concludes by explaining how to close the AO port.

/// If your product is "STEM", please invoke the function `Sys_setPortAIOMode`.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AO_write_one_channel
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
            int port = 1;
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
            
            // Open AO port
            err = dev.AO_open(port, timeout:timeout);
            Console.WriteLine($"AO_open in port{port}: {err}");

            // Set AO port and write data 1.5(V) in channel 0
            err = dev.AO_writeOneChannel(port, 0, 1.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch0 in port{port}: {err}");

            // Set AO port and write data 2.5(V) in channel 1
            err = dev.AO_writeOneChannel(port, 1, 2.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch1 in port{port}: {err}");

            // Set AO port and write data 3.5(V) in channel 2
            err = dev.AO_writeOneChannel(port, 2, 3.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch2 in port{port}: {err}");

            // Set AO port and write data 4.5(V) in channel 3
            err = dev.AO_writeOneChannel(port, 3, 4.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch3 in port{port}: {err}");

            // Close AO port
            err = dev.AO_close(port, timeout:timeout);
            Console.WriteLine($"AO_close in port{port}: {err}");
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