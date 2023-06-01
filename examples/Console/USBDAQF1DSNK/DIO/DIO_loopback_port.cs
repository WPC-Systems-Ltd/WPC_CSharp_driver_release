/// DIO_loopback_port.cs with synchronous mode.
///
/// This example demonstrates how to write DIO loopback in port from USBDAQF1DSNK.
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

class USBDAQF1DSNK_DIO_loopback_port
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
            
            // Get port mode
            string port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // If the port mode is not set to "DIO", set the port mode to "DIO"
            if (port_mode != "DIO"){
                err = dev.Sys_setPortDIOMode(port, timeout:timeout);
                Console.WriteLine($"Sys_setPortDIOMode: {err}");
            }

            // Get port mode
            port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // Get port DIO start up information
            List<List<byte>> pinstate_list = dev.DIO_loadStartup(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            Console.WriteLine($"enable_list");
            WPC_utilities.printByteList(pinstate_list[0]);

            Console.WriteLine($"direction_list");
            WPC_utilities.printByteList(pinstate_list[1]);

            Console.WriteLine($"state_list");
            WPC_utilities.printByteList(pinstate_list[2]);

            // Write DO port to high or low
            err = dev.DO_writePort(DO_port, new List<int> { 1, 0, 1, 0 }, timeout:timeout);
            Console.WriteLine($"DO_writePort in port{DO_port}: {err}");

            // Read DI port state
            List<int> p = dev.DI_readPort(DI_port, timeout:timeout);
            Console.WriteLine($"DI_readPort: {p[0]}, {p[1]}, {p[2]}, {p[3]}, {p[4]}, {p[5]}, {p[6]}, {p[7]}");
            
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