/// DO_blinky_port.cs with synchronous mode.
///
/// This example illustrates the process of writing a high or low signal to a DO port from USBDAQF1D.
///
/// To begin with, it demonstrates the steps required to open the DO port.
/// Next, in each loop, a different voltage output is applied, resulting in a blinking effect.
/// Lastly, it concludes by closing the DO port.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_DO_blinky_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

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
            int err;
            int port = 0; // Depend on your device
            int timeout = 3000;  // ms

            // Open port with digital output
            err = dev.DO_openPort(port, timeout);
            Console.WriteLine($"DO_openPort in port {port}, status: {err}");

            // Toggle digital state for 10 times. Each times delay for 500 ms
            for (int i=0; i<10; i++)
            {
                dev.DO_togglePort(port, timeout);

                // Wait for 500 ms to see led status
                Thread.Sleep(500); // delay [ms]
            }

            // Close port with digital output
            err = dev.DO_closePort(port, timeout);
            Console.WriteLine($"DO_closePort in port {port}, status: {err}");
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
