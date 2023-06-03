/// DO_write_port.cs with synchronous mode.
///
/// This example illustrates the process of writing a high or low signal to a DO port from STEM.
///
/// To begin with, it demonstrates the steps required to open the DO port.
/// Next, voltage output is written to the DO port.
/// Lastly, it concludes by closing the DO port.

/// If your product is "STEM", please invoke the function `Sys_setPortDIOMode`.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_DO_write_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        STEM dev = new STEM();

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
            int port = 1; // Depend on your device
            int DO_port = 1;
            int timeout = 3000;  // ms

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

            // Write DO port to high or low
            err = dev.DO_writePort(DO_port, new List<int> { 1, 0, 1, 0 }, timeout:timeout);
            Console.WriteLine($"DO_writePort in port{DO_port}: {err}");

            
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