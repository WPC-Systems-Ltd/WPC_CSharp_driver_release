/// DO_write_pins.cs with synchronous mode.
///
/// This example illustrates the process of writing a high or low signal to a DO pin from USBDAQF1TD.
///
/// To begin with, it demonstrates the steps required to open the DO pin.
/// Next, voltage output is written to the DO pin.
/// Lastly, it concludes by closing the DO pin.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_DO_write_pins
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

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
            List<int> pinindex = new List<int> {1, 3, 5, 7};
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pins with digital output
            err = dev.DO_openPins(port, pinindex, timeout:timeout);
            Console.WriteLine($"DO_openPins in port{port}: {err}");

            // Write pins to high or low
            err = dev.DO_writePins(port, pinindex, new List<int> {1, 1, 0, 0}, timeout:timeout);
            Console.WriteLine($"DO_writePins in port{port}: {err}");

            // Close pins with digital output
            err = dev.DO_closePins(port, pinindex, timeout:timeout);
            Console.WriteLine($"DO_closePins in port{port}: {err}");
            
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