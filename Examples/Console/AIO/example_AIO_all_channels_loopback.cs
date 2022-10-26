﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// AIO - example_AIO_all_channels_loopback.cs
/// 
/// This example demonstrates how to write AIO loopback in all channels from WPC-USB-DAQ-F1-AOD.
/// Use AO pins to send signals and use AI pins to receive signals on single device also called "loopback".
/// 
/// First, it shows how to open AO and AI in port.
/// Second, write all digital signals to AO and read AI ondemand data.
/// Last, close AO and AI in port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/Examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>


class example_AIO_all_channels_loopback
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to USB device
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

            // Open AI port to 0
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Open AO port0
            status = dev.AO_open(port);
            Console.WriteLine($"AO_open status: {status}");

            // Set AI port to 0 and data acquisition
            sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Set AO port to 0 and write data simultaneously
            List<double> AO_values = new List<double> { 0, 1, 2, 3, 4, 5, 4, 3 };
            status = dev.AO_writeAllChannels(port, AO_values);
            Console.WriteLine($"AO_writeAllChannels status: {status}");

            // Set AI port to 0 and data acquisition
            sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close AI port to 0
            status = dev.AI_close(port);
            Console.WriteLine($"AI_close status: {status}");

            // Close AO port0
            status = dev.AO_close(port);
            Console.WriteLine($"AO_close status: {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect network device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}
