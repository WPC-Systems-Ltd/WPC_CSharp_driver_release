using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @example TC_read_channel_data.cs
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
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary> 

class WPC_TC_read_channel_data
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

        // Connect to device
        dev.connect("21JA1239");


        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 1;
            int channel = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open thermo port
            status = dev.Thermal_open(port);
            Console.WriteLine($"Thermal_open status: {status}");

            // Set thermo port and set K type in channel 1 
            status = dev.Thermal_setOverSampling(port, channel, WPC.THERMAL_OVERSAMPLING_NONE);
            Console.WriteLine($"Thermal_setOverSampling status: {status}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set thermo port and set K type in channel 1 
            status = dev.Thermal_setType(port, channel, WPC.THERMAL_COUPLE_TYPE_K);
            Console.WriteLine($"Thermal_setType status: {status}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set thermo port and read thermo in channel 1
            float data = dev.Thermal_readSensor(port, channel);
            Console.WriteLine($"Read channel 1 data: {data} °C ");

            // Close thermo port
            status = dev.Thermal_close(port);
            Console.WriteLine($"Thermal_close status: {status}");

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
