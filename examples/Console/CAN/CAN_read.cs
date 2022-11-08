using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @example CAN_read.cs
/// 
/// This example demonstrates how to read data from another device with CAN interface from USBDAQF1CD.
/// 
/// First, it shows how to open CAN port and configure CAN parameters.
/// 
/// SSecond, read bytes from another device.
/// 
/// Last, stop and close CAN port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary> 

class WPC_CAN_read
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

        // Connect to device
        dev.connect("21JA1312");


        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open CAN
            status = dev.CAN_open(port);
            Console.WriteLine($"CAN_open status: {status}");

            // Set CAN port and set speed to 125K
            status = dev.CAN_setSpeed(port, WPC.CAN_SPEED_125K);
            Console.WriteLine($"CAN_setSpeed status: {status}");

            // Set CAN port and start CAN
            status = dev.CAN_start(port);
            Console.WriteLine($"CAN_start status: {status}");

            for (int i = 0; i < 1000; i++)
            {
                List<CANFrame> frame_list = dev.CAN_read(port, 5);
                if (frame_list.Count() > 0)
                {
                    foreach (CANFrame frame in frame_list)
                    { 
                        WPC_utilities.printByteList(frame.frame_in_list); 
                    }
                } 
                else 
                { 
                    // Wait for 10ms sec
                    Thread.Sleep(10); // delay [ms]  
                } 
            } 

            // Stop CAN
            status = dev.CAN_stop(port);
            Console.WriteLine($"CAN_stop status: {status}");

            // Close CAN
            status = dev.CAN_close(port);
            Console.WriteLine($"CAN_close status: {status}");

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
