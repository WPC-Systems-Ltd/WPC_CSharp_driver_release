/// DO_blinky_pins.cs with synchronous mode.
///
/// This example illustrates the process of writing a high or low signal to a DO pin from EthanEXD.
///
/// To begin with, it demonstrates the steps required to open the DO pin.
/// Next, in each loop, a different voltage output is applied, resulting in a blinking effect.
/// Lastly, it concludes by closing the DO pin.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanEXD_DO_blinky_pins
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanEXD dev = new EthanEXD();

        // Connect to device
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
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
            int port = 0; // Depend on your device
            List<int> pinindex = new List<int> {1, 3, 5, 7};
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pins with digital output
            err = dev.DO_openPins(port, pinindex, timeout);
            Console.WriteLine($"DO_openPins in port {port}, status: {err}");

            // Toggle digital state for 10 times. Each times delay for 500 ms
            for (int i=0; i<10; i++)
            {
                List<byte> state = dev.DO_togglePins(port, pinindex, timeout);
                Console.WriteLine(string.Format("[{0}]", string.Join(", ", state)));

                // Wait for 500 ms to see led status
                Thread.Sleep(500); // delay [ms]
            }

            // Close pins with digital output
            err = dev.DO_closePins(port, pinindex, timeout);
            Console.WriteLine($"DO_closePins in port {port}, status: {err}");
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
