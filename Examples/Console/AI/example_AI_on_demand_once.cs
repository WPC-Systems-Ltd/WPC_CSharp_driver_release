﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// AI - example_AI_on_demand_once.cs
/// 
/// This example demonstrates how to get AI data in on demand mode.
/// Also, it gets AI data in once with 8 channels from WPC-USB-DAQ-F1-AD.
/// 
/// First, it shows how to open AI port.
/// Second, read AI ondemand data.
/// Last, close AI port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/Examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>

public class example_AI_on_demand_once
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to USB device
        dev.connect("21JA1245");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int status;
            byte port = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port to 0
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 0 and acquisition mode to on demand
            status = dev.AI_setMode(port, WPC.AI_MODE_ON_DEMAND);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 0 and data acquisition
            List<double> sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Close AI port to 0
            status = dev.AI_close(port);
            Console.WriteLine($"AI_close status: {status}");
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