/// AIO_one_channel_loopback.cs
///
/// This example demonstrates how to write AIO loopback in specific channel from USBDAQF1AOD.
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
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AIO_one_channel_loopback
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        dev.connect("21JA1439");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port);
            Console.WriteLine($"AI_open: {err}");

            // Open AO port
            err = dev.AO_open(port);
            Console.WriteLine($"AO_open: {err}");

            // Set AI port and data acquisition
            List<double> s = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Set AO port and write data 1.5(V) in channel 4
            err = dev.AO_writeOneChannel(port, 4, 1.5);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AO port and write data 2.5(V) in channel 5
            err = dev.AO_writeOneChannel(port, 5, 2.5);
            Console.WriteLine($"writeOneChannel: {err}");
 
            // Set AO port and write data 3.5(V) in channel 6
            err = dev.AO_writeOneChannel(port, 6, 3.5);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AO port and write data 4.5(V) in channel 7
            err = dev.AO_writeOneChannel(port, 7, 4.5);
            Console.WriteLine($"writeOneChannel: {err}");

            // Set AI port and data acquisition
            s = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close AI port
            err = dev.AI_close(port);
            Console.WriteLine($"AI_close: {err}");

            // Close AO port
            err = dev.AO_close(port);
            Console.WriteLine($"AO_close: {err}");
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