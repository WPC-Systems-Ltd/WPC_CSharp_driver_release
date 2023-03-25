/// RTD_read_channel_data.cs with synchronous mode.
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
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1RD_RTD_read_channel_data
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

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
            float data;
            int port = 1;
            int ch0 = 0;
            int ch1 = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open RTD port
            err = dev.Thermal_open(port, timeout:timeout);
            Console.WriteLine($"Thermal_open in port{port}: {err}");

            // Wait for at least 250 ms after setting type or oversampling
            Thread.Sleep(250); // delay [ms]

            // Set RTD port and read RTD in channel 0
            data = dev.Thermal_readSensor(port, ch0, timeout:timeout);
            Console.WriteLine($"Read sensor in channel {ch0} in port{port}: {data}°C");

            // Set RTD port and read RTD in channel 1
            data = dev.Thermal_readSensor(port, ch1, timeout:timeout);
            Console.WriteLine($"Read sensor in channel {ch1} in port{port}: {data}°C");

            // Close RTD port
            err = dev.Thermal_close(port, timeout:timeout);
            Console.WriteLine($"Thermal_close in port{port}: {err}");
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