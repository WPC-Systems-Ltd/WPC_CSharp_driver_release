﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// AI - example_AI_N_samples_in_loop.cs
/// 
/// This example demonstrates how to get AI data in N samples mode.
/// Also, it uses loop to get AI data with 3 seconds timeout with 8 channels from WPC-USB-DAQ-F1-AD.
/// 
/// First, it shows how to open AI port and configure AI parameters.
/// Second, read AI streaming data.
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

public class example_AI_N_samples_in_loop
{
    static void loop_func(USBDAQF1AD handle, int port, int num_of_samples, int delay = 1, int timeout = 3)
    {
        int t = 0;
        while (t < timeout)
        {
            // Data acquisition
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> sample in streaming_list)
            {
                // Read acquisition data
                Console.WriteLine($"data : {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

            // Wait for 0.01 sec
            Thread.Sleep(10); // delay [ms]

            t += delay;
        }
        Console.WriteLine("loop_func end");
    }

    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to USB device
        dev.connect("21JA1245");
         
        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 0;
            int samples = 3000;
            float sampling_rate = 1000;
             
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port to 0
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 0 and acquisition mode to continuous
            status = dev.AI_setMode(port, WPC.AI_MODE_CONTINOUS);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 0 and sampling rate to 1k (Hz)
            status = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"AI_setSamplingRate status : {status}");

            // Set AI port to 0 and # of samples to 3000 (pts)
            status = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"AI_setNumSamples status: {status}");

            // Set AI port to 0 and start acquisition
            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Start loop
            loop_func(dev, port, 1, 3);

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