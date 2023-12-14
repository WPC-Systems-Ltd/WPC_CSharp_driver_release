/// DIO_loopback_port.cs with synchronous mode.
///
/// This example demonstrates the process of DIO loopback using port from USBDAQF1AD.
/// It involves using DO port to send signals and DI port to receive signals on a single device, commonly known as "loopback".
///
/// To begin with, it illustrates the steps required to open the DO and DI port.
/// Next, it performs the operation of writing to a DO pin and reading from a DI pin.
/// Lastly, it concludes by closing the DO and DI port.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AD_DIO_loopback_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

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
            int DO_port = 0;
            int DI_port = 1;
            List<int> DO_value = new List<int> {0, 1, 0, 1};
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open DO port with digital output
            err = dev.DO_openPort(DO_port, timeout);
            Console.WriteLine($"DO_openPort in DO_port {DO_port}: {err}");

            // Open DI port with digital input
            err = dev.DI_openPort(DI_port, timeout);
            Console.WriteLine($"DO_openPort in DI_port {DI_port}: {err}");

            // Write DO port to high or low
            err = dev.DO_writePort(DO_port, DO_value, timeout);
            Console.WriteLine($"DO_writePort in DO_port {DO_port}: {err}");

            // Read DI port state
            List<int> state = dev.DI_readPort(DI_port, timeout);
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", state)));

            // Close DO port with digital output
            err = dev.DO_closePort(DO_port, timeout);
            Console.WriteLine($"DO_closePort in DO_port {DO_port}: {err}");

            // Close DI port with digital input
            err = dev.DI_closePort(DI_port, timeout);
            Console.WriteLine($"DI_closePort in DI_port {DI_port}: {err}");
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
