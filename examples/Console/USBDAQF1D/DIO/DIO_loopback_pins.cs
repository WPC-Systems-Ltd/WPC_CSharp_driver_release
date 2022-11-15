/// <summary>
/// DIO_loopback_pins.cs
/// 
/// This example demonstrates how to write DIO loopback in pins from USBDAQF1D.
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
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC_Product;

class USBDAQF1D_DIO_loopback_pins
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

        // Connect to device
        dev.connect("21JA1200");
          
        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 0;
            List<int> DO_pins = new List<int> { 0, 1, 2, 3, 4 };
            List<int> DI_pins = new List<int> { 5, 6, 7 };

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pin0, pin1, pin2, pin3 and pin4 with digital output
            status = dev.DO_openPins(port, DO_pins);
            Console.WriteLine($"DO_openPins status: {status}");

            // Open pin5, pin6 and pin7 with digital output
            status = dev.DI_openPins(port, DI_pins);
            Console.WriteLine($"DI_openPins status: {status}");

            // Set pin0 and pin1 to high, others to low
            status = dev.DO_writePins(port, DO_pins, new List<int> { 1, 1, 0, 0, 0 });
            Console.WriteLine($"DO_writePins status: {status}");

            // Read pin5, pin6 and pin7 state
            List<int> pin_s = dev.DI_readPins(port, DI_pins);
            Console.WriteLine($"DI_readPins: {pin_s[0]}, {pin_s[1]}, {pin_s[2]}");
            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close pin0, pin1, pin2, pin3 and pin4 with digital output
            status = dev.DO_closePins(port, DO_pins);
            Console.WriteLine($"DO_closePins status: {status}");

            // Close pin5, pin6 and pin7 with digital input 
            status = dev.DI_closePins(port, DI_pins);
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