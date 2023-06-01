/// AIO_all_channels_loopback.cs  with synchronous mode.
///
/// This example demonstrates how to write AIO loopback in all channels from USBDAQF1AOD.
///
/// Use AO pins to send signals and use AI pins to receive signals on single device also called "loopback".
///
/// First, it shows how to open AO and AI in port.
///
/// Second, write all digital signals to AO and read AI ondemand data.
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

class USBDAQF1AOD_AIO_all_channels_loopback
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

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Open AO port
            err = dev.AO_open(port, timeout:timeout);
            Console.WriteLine($"AO_open in port{port}: {err}");
            
            // Data acquisition
            List<double> sample = dev.AI_readOnDemand(port, timeout:timeout);

            // Read acquisition data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Set AO port and write data simultaneously
            List<double> AO_values = new List<double> { 0, 1, 2, 3, 4, 5, 4, 3 };
            err = dev.AO_writeAllChannels(port, AO_values, timeout:timeout);
            Console.WriteLine($"AO_writeAllChannels in port{port}: {err}");

            // Set AI port and data acquisition
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