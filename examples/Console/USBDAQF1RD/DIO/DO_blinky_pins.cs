/// DO_blinky_pins.cs with synchronous mode.
///
/// This example demonstrates how to write DO high or low in pins from USBDAQF1RD.
///
/// First, it shows how to open DO in pins.
///
/// Second, each loop has different voltage output so it will look like blinking.
///
/// Last, close DO in pins.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1RD_DO_blinky_pins
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

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

            // Toggle digital state for 10 times. Each times delay for 0.5 second
            for (int i=0; i<10; i++)
            {
                List<byte> state = dev.DO_togglePins(port, pinindex, timeout:timeout);
                Console.WriteLine(string.Format("[{0}]", string.Join(", ", state)));
                // Wait for 0.5 second to see led status
                Thread.Sleep(500); // delay [ms]
            }

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