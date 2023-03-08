/// DO_blinky_port.cs with synchronous mode.
///
/// This example demonstrates how to write DO high or low in port from USBDAQF1DSNK.
///
/// First, it shows how to open DO in port.
///
/// Second, each loop has different voltage output so it will look like blinking.
///
/// Last, close DO in port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1DSNK_DO_blinky_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1DSNK dev = new USBDAQF1DSNK();

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

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            List<int> DO_odd_state = new List<int> { 0, 1, 0, 1, 0, 1, 0, 1 };
            List<int> DO_even_state = new List<int> { 1, 0, 1, 0, 1, 0, 1, 0 };
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins and set it to digital output.
            err = dev.DO_openPort(port, timeout);
            Console.WriteLine($"DO_openPort in port{port}: {err}");

            // Toggle digital state for 10 times. Each times delay for 0.5 second
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    err = dev.DO_writePort(port, DO_even_state, timeout);
                }
                else
                {
                    err = dev.DO_writePort(port, DO_odd_state, timeout);
                }
                Console.WriteLine($"DO_writePort in port{port}: {err}");
                // Wait for 0.5 second to see led status
                Thread.Sleep(500); // delay [ms]
            }

            // Close all pins with digital output
            err = dev.DO_closePort(port, timeout);
            Console.WriteLine($"DO_closePort in port{port}: {err}");
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