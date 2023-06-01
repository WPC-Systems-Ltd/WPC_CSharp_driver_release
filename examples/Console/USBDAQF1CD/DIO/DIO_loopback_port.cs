/// DIO_loopback_port.cs with synchronous mode.
///
/// This example demonstrates how to write DIO loopback in port from USBDAQF1CD.
///
/// Use DO pins to send signals and use DI pins to receive signals on single device also called "loopback".
///
/// First, it shows how to open DO and DI in port.
///
/// Second, write DO in port and read DI in port
///
/// Last, close DO and DI in port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1CD_DIO_loopback_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

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
            int port = 0; // Depend on your device
            int DO_port = 0;
            int DI_port = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Open DO port with digital output
            err = dev.DO_openPort(DO_port, timeout:timeout);
            Console.WriteLine($"DO_openPort in port{DO_port}: {err}");

            // Open DI port with digital input
            err = dev.DI_openPort(DI_port, timeout:timeout);
            Console.WriteLine($"DO_openPort in port{DI_port}: {err}");

            // Write DO port to high or low
            err = dev.DO_writePort(DO_port, new List<int> { 1, 0, 1, 0 }, timeout:timeout);
            Console.WriteLine($"DO_writePort in port{DO_port}: {err}");

            // Read DI port state
            List<int> p = dev.DI_readPort(DI_port, timeout:timeout);
            Console.WriteLine($"DI_readPort: {p[0]}, {p[1]}, {p[2]}, {p[3]}, {p[4]}, {p[5]}, {p[6]}, {p[7]}");

            // Close DO port with digital output
            err = dev.DO_closePort(DO_port, timeout:timeout);
            Console.WriteLine($"DO_closePort in port{DO_port}: {err}");

            // Close DI port with digital input
            err = dev.DI_closePort(DI_port, timeout:timeout);
            Console.WriteLine($"DI_closePort in port{DI_port}: {err}");
            
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