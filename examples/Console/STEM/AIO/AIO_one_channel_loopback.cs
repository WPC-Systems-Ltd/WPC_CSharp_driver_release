/// AIO_one_channel_loopback.cs with synchronous mode.
///
/// This example demonstrates how to write AIO loopback in specific channel from STEM.
///
/// Use AO pins to send signals and use AI pins to receive signals on single device also called "loopback".
///
/// First, it shows how to open AO and AI in port.
///
/// Second, write digital signals to AO in specific channel and read AI ondemand data.
///
/// Last, close AO and AI in port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AIO_one_channel_loopback
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

            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Open AO port
            err = dev.AO_open(port, timeout:timeout);
            Console.WriteLine($"AO_open in port{port}: {err}");

            // Enable CS
            err = dev.AI_enableCS(port, new List<int> {0, 1}, timeout:timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");
            
            // Data acquisition
            List<double> sample = dev.AI_readOnDemand(port, timeout:timeout);

            // Read acquisition data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Set AO port and write data 1.5(V) in channel 4
            err = dev.AO_writeOneChannel(port, 4, 1.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch4 in port{port}: {err}");

            // Set AO port and write data 2.5(V) in channel 5
            err = dev.AO_writeOneChannel(port, 5, 2.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch5 in port{port}: {err}");

            // Set AO port and write data 3.5(V) in channel 6
            err = dev.AO_writeOneChannel(port, 6, 3.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch6 in port{port}: {err}");

            // Set AO port and write data 4.5(V) in channel 7
            err = dev.AO_writeOneChannel(port, 7, 4.5, timeout:timeout);
            Console.WriteLine($"AO_writeOneChannel in ch7 in port{port}: {err}");

            // Data acquisition
            sample = dev.AI_readOnDemand(port, timeout:timeout);

            // Read acquisition data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Close AI port
            err = dev.AI_close(port, timeout:timeout);
            Console.WriteLine($"AI_close in port{port}: {err}");

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