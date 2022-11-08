using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @example DIO_loopback_pins.cs
/// 
/// This example demonstrates how to write DIO loopback in pins from USBDAQF1AD.
/// 
/// Use DO pins to send signals and use DI pins to receive signals on single device also called "loopback".
/// 
/// First, it shows how to open DO and DI in pins.
/// 
/// Second, write DO pin and read DI pin
/// 
/// Last, close DO and DI in pins.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary> 
/// 

class WPC_DIO_loopback_pins
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to device
        dev.connect("21JA1245");
         

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port_DO = 0; // For DO
            int port_DI = 1; // For DI
            List<int> DO_pins = new List<int> { 0, 1, 2, 3 };
            List<int> DI_pins = new List<int> { 0, 1, 2, 3 };

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pin0, pin1, pin2 and pin3 in port 0 with digital output
            status = dev.DO_openPins(port_DO, DO_pins);
            Console.WriteLine($"DO_openPins status: {status}");

            // Open pin0, pin1, pin2 and pin3 in port 1 with digital input
            status = dev.DI_openPins(port_DI, DI_pins);
            Console.WriteLine($"DI_openPins status: {status}");

            // Set pin0 and pin2 to high, others to low in port 0
            status = dev.DO_writePins(port_DO, DO_pins, new List<int> { 1, 0, 1, 0 });
            Console.WriteLine($"DO_writePins status: {status}");

            // Read pin0, pin1, pin2 and pin3 state in port 1
            List<int> pin_s = dev.DI_readPins(port_DI, DI_pins);
            Console.WriteLine($"DI_readPins: {pin_s[0]}, {pin_s[1]}, {pin_s[2]}, {pin_s[3]}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close pin0, pin1, pin2 and pin3 in port 0 with digital output
            status = dev.DO_closePins(port_DO, DO_pins);
            Console.WriteLine($"DO_closePins status: {status}");

            // Close pin0, pin1, pin2 and pin3 in port 1 with digital input 
            status = dev.DI_closePins(port_DI, DI_pins);
            Console.WriteLine($"DI_closePins status: {status}");
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
