/// DIO_loopback_pins.cs with synchronous mode.
///
/// This example demonstrates the process of DIO loopback using pins from EthanD.
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

class EthanD_DIO_loopback_pins
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanD dev = new EthanD();

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
            int port = 0;
            int DO_port = 0;
            int DI_port = 1;
            List<int> DO_pins = new List<int> {0, 1, 2, 3};
            List<int> DI_pins = new List<int> {4, 5, 6, 7};
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
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[0])));

            Console.WriteLine($"direction_list");
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[1])));

            Console.WriteLine($"state_list");
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[2])));

            // Get port mode
            port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // Write pins to high or low
            err = dev.DO_writePins(DO_port, DO_pins, new List<int> {1, 1, 0, 0}, timeout:timeout);
            Console.WriteLine($"writePins: {err}");

            // Read pins state
            List<int> state = dev.DI_readPins(DI_port, DI_pins, timeout:timeout);
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", state)));
            
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