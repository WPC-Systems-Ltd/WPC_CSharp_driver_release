using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @example AIO_one_channel_loopback.cs
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
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>

class WPC_AIO_one_channel_loopback
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        dev.connect("21JA1439");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 0;
            List<double> sample;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Open AO port
            status = dev.AO_open(port);
            Console.WriteLine($"AO_open status: {status}");

            // Set AI port and data acquisition
            sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            //Set AO port and write data 1.5(V) in channel 4
            status = dev.AO_writeOneChannel(port, 4, 1.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port and write data 2.5(V) in channel 5
            status = dev.AO_writeOneChannel(port, 5, 2.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port and write data 3.5(V) in channel 6
            status = dev.AO_writeOneChannel(port, 6, 3.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port and write data 4.5(V) in channel 7
            status = dev.AO_writeOneChannel(port, 7, 4.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            // Set AI port and data acquisition
            sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close AI port
            status = dev.AI_close(port);
            Console.WriteLine($"AI_close status: {status}");

            // Close AO port
            status = dev.AO_close(port);
            Console.WriteLine($"AO_close status: {status}");
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
