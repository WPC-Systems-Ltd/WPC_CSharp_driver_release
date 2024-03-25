/// TC_read_channel_data.cs with synchronous mode.
///
/// This example demonstrates how to read thermocouple from EthanT.
///
/// First, it shows how to open thermal port and configure thermal parameters.
///
/// Second, read channel 1 thermocouple data.
///
/// Last, close thermal port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanT_TC_read_channel_data
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanT dev = new EthanT();

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
            int ch = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open thermo port
            err = dev.Thermal_open(port, timeout);
            Console.WriteLine($"Thermal_open in port {port}: {err}");

            // Set thermo port and set K type in channel 1
            err = dev.Thermal_setOverSampling(port, ch, Const.THERMAL_OVERSAMPLING_NONE, timeout);
            Console.WriteLine($"Thermal_setOverSampling in channel {ch} in port {port}: {err}");

            // Set thermo port and set K type in channel 1
            err = dev.Thermal_setType(port, ch, Const.THERMAL_COUPLE_TYPE_K, timeout);
            Console.WriteLine($"Thermal_setType in channel {ch} in port {port}: {err}");

            // Wait for at least 500 ms after setting type or oversampling
            Thread.Sleep(500); // delay [ms]

            // Set thermo port and read thermo in channel 1
            float data = dev.Thermal_readSensor(port, ch, timeout);
            Console.WriteLine($"Read sensor in channel {ch} in port {port}: {data}°C");

            // Close thermo port
            err = dev.Thermal_close(port, timeout);
            Console.WriteLine($"Thermal_close in port {port}: {err}");
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