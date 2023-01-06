/// RTD_read_channel_data.cs
/// 
/// This example demonstrates how to read RTD data in two channels from USBDAQF1RD.
/// 
/// First, it shows how to open thermal port
/// 
/// Second, read channel 0 and channel 1 RTD data
/// 
/// Last, close thermal port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1RD_RTD_read_channel_data
{
    static public void Main()
    { 
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

        // Connect to device
        dev.connect("21JA1385");

        // Execute
        try
        {
            // Parameters setting
            int err;
            float data;
            int port = 1;
            int channel_0 = 0;
            int channel_1 = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open RTD port
            err = dev.Thermal_open(port);
            Console.WriteLine($"open: {err}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set RTD port and read RTD in channel 0
            data = dev.Thermal_readSensor(port, channel_0);
            Console.WriteLine($"Read channel 0 data: {data} °C ");

            // Set RTD port and read RTD in channel 1
            data = dev.Thermal_readSensor(port, channel_1);
            Console.WriteLine($"Read channel 1 data: {data} °C ");

            // Close RTD port
            err = dev.Thermal_close(port);
            Console.WriteLine($"close: {err}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}