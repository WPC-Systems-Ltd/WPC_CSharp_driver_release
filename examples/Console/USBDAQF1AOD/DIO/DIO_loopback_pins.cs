/// DIO_loopback_pins.cs with synchronous mode.
///
/// This example demonstrates the process of DIO loopback using pins from USBDAQF1AOD.
/// It involves using DO pins to send signals and DI pins to receive signals on a single device, commonly known as "loopback".
///
/// To begin with, it illustrates the steps required to open the DO and DI pins.
/// Next, it performs the operation of writing to a DO pin and reading from a DI pin.
/// Lastly, it concludes by closing the DO and DI pins.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_DIO_loopback_pins
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        try
        {
            dev.connect("default"); // Depend on your device
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Release device handle
            dev.close();
            return;
        }

        try
        {
            
            // Parameters setting
            int err;
            int port = 0;
            List<int> DO_pins = new List<int> {0, 1, 2, 3};
            List<int> DI_pins = new List<int> {4, 5, 6, 7};
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pins with digital output
            err = dev.DO_openPins(port, DO_pins, timeout:timeout);
            Console.WriteLine($"DO_openPins in port{port}: {err}");

            // Open pins with digital iutput
            err = dev.DI_openPins(port, DI_pins, timeout:timeout);
            Console.WriteLine($"DI_openPins in port{port}: {err}");

            // Write pins to high or low
            err = dev.DO_writePins(port, DO_pins, new List<int> {1, 1, 0, 0}, timeout:timeout);
            Console.WriteLine($"writePins: {err}");

            // Read pins state
            List<int> pin_s = dev.DI_readPins(port, DI_pins, timeout:timeout);
            Console.WriteLine($"DI_readPins: {pin_s[4]}, {pin_s[5]}, {pin_s[6]}, {pin_s[7]}");

            // Close pins with digital output
            err = dev.DO_closePins(port, DO_pins, timeout:timeout);
            Console.WriteLine($"DO_closePins in port{port}: {err}");

            // Close pins with digital input
            err = dev.DI_closePins(port, DI_pins, timeout:timeout);
            Console.WriteLine($"DI_closePins in port{port}: {err}");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();
    }
}