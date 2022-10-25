﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class example_AI_on_demand_in_loop
{ 
    static void loop_func(USBDAQF1AD handle, byte port, int delay , int timeout =3) 
    {
        int t = 0;
        while (t < timeout) 
        {
            // Data acquisition
            List<double> sample = handle.AI_readOnDemand(port);
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            Thread.Sleep(10);
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

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int status;
            byte port = 0;
            byte mode = 0;
             
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open port 0
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 0 and acquisition mode to on demand mode
            status = dev.AI_setMode(port, WPC.AI_MODE_ON_DEMAND);
            Console.WriteLine($"AI_setMode status: {status}");

            // Start thread
            loop_func(dev, port, 1, 10);
             
            // Close port 0
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
