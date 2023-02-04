/// TC_read_channel_data.cs with synchronous mode.
/// 
/// This example demonstrates how to read thermocouple from USBDAQF1TD.
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
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_TC_read_channel_data
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

        // Connect to device
        dev.connect("21JA1239");
 
        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 1;
            int channel = 1;
            int timeout = 3000;
            
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open thermo port
            err = dev.Thermal_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Set thermo port and set K type in channel 1 
            err = dev.Thermal_setOverSampling(port, channel, Const.THERMAL_OVERSAMPLING_NONE, timeout);
            Console.WriteLine($"setOverSampling: {err}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set thermo port and set K type in channel 1 
            err = dev.Thermal_setType(port, channel, Const.THERMAL_COUPLE_TYPE_K, timeout);
            Console.WriteLine($"setType: {err}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set thermo port and read thermo in channel 1
            float data = dev.Thermal_readSensor(port, channel, timeout);
            Console.WriteLine($"Read channel 1 data: {data} °C ");

            // Close thermo port
            err = dev.Thermal_close(port, timeout);
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
    }
}