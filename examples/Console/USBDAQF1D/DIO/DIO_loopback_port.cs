/// DIO_loopback_port.cs with synchronous mode.
///
/// This example demonstrates how to write DIO loopback in port from USBDAQF1D.
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

class USBDAQF1D_DIO_loopback_port
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
            // Parameters setting
            int err;
            int port_DO = 0;
            int port_DI = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins with digital output
            err = dev.DO_openPort(port_DO, timeout:timeout);
            Console.WriteLine($"DO_openPort in port{port_DO}: {err}");

            // Open all pins with digital input
            err = dev.DI_openPort(port_DI, timeout:timeout);
            Console.WriteLine($"DO_openPort in port{port_DI}: {err}");

            // Set pin0, pin1 and pin2 to high, others to low
            err = dev.DO_writePort(port_DO, new List<int> { 0, 0, 0, 1, 0, 0, 0, 0 }, timeout:timeout);
            Console.WriteLine($"DO_writePort in port{port_DO}: {err}");

            // Read all pins state
            List<int> p = dev.DI_readPort(port_DI, timeout:timeout);
            Console.WriteLine($"DI_readPort: {p[0]}, {p[1]}, {p[2]}, {p[3]}, {p[4]}, {p[5]}, {p[6]}, {p[7]}");

            // Close all pins with digital output
            err = dev.DO_closePort(port_DO, timeout:timeout);
            Console.WriteLine($"DO_closePort in port{port_DO}: {err}");

            // Close all pins with digital input
            err = dev.DI_closePort(port_DI, timeout:timeout);
            Console.WriteLine($"DI_closePort in port{port_DI}: {err}");
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