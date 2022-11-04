using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// AO - example_AO_write_one_channel.cs
/// 
/// This example demonstrates how to write AO in specific channels from WPC-USB-DAQ-F1-AOD.
/// 
/// First, it shows how to open AO in port.
/// Second, write digital signals in specific channels.
/// Last, close AO in port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/Examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>


class example_AO_write_one_channel
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

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO port0
            status = dev.AO_open(port);
            Console.WriteLine($"AO_open status: {status}");

            //Set AO port to 0 and write data 1.5(V) in channel 4
            status = dev.AO_writeOneChannel(port, 4, 1.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port to 0 and write data 2.5(V) in channel 5
            status = dev.AO_writeOneChannel(port, 5, 2.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port to 0 and write data 3.5(V) in channel 6
            status = dev.AO_writeOneChannel(port, 6, 3.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            //Set AO port to 0 and write data 4.5(V) in channel 7
            status = dev.AO_writeOneChannel(port, 7, 4.5);
            Console.WriteLine($"AO_writeOneChanne status: {status}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

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
