/// DIO_loopback_port.cs with synchronous mode.
///
/// This example demonstrates the process of DIO loopback using port from STEM.
/// It involves using DO port to send signals and DI port to receive signals on a single device, commonly known as "loopback".
///
/// To begin with, it illustrates the steps required to open the DO and DI port.
/// Next, it performs the operation of writing to a DO pin and reading from a DI pin.
/// Lastly, it concludes by closing the DO and DI port.

/// If your product is "STEM", please invoke the function Sys_setDIOMode.
///
/// The DIO ports 0 to 1 are assigned to slot 1, while ports 2 to 3 are assigned to slot 2.
/// ---------------------------
/// |  Slot 1    port 1 & 0   |
/// |  Slot 2    port 3 & 2   |
/// |  Slot 3    port 5 & 4   |
/// |  Slot 4    port 7 & 6   |
/// ---------------------------

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_DIO_loopback_port
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
            int slot = 1; // Connect DIO module to slot
            int DO_port = 0;
            int DI_port = 1;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get slot mode
            string slot_mode = dev.Sys_getMode(slot, timeout:timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // If the slot mode is not set to "DIO", set the slot mode to "DIO"
            if (slot_mode != "DIO"){
                err = dev.Sys_setDIOMode(slot, timeout:timeout);
                Console.WriteLine($"Sys_setDIOMode: {err}");
            }

            // Get slot mode
            slot_mode = dev.Sys_getMode(slot, timeout:timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            // Get DIO start up information
            List<List<byte>> pinstate_list = dev.DIO_loadStartup(DO_port, timeout:timeout);
            Console.WriteLine($"Slot mode: {slot_mode}");

            Console.WriteLine($"enable_list");
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[0])));

            Console.WriteLine($"direction_list");
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[1])));

            Console.WriteLine($"state_list");
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", pinstate_list[2])));

            // Write DO port to high or low
            err = dev.DO_writePort(DO_port, new List<int> { 1, 0, 1, 0 }, timeout:timeout);
            Console.WriteLine($"DO_writePort in DO_port {DO_port}: {err}");

            // Read DI port state
            List<int> state = dev.DI_readPort(DI_port, timeout:timeout);
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
