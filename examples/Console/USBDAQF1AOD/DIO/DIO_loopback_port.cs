/// DIO_loopback_port.cs
/// 
/// This example demonstrates how to write DIO loopback in port from USBDAQF1AOD.
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
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_DIO_loopback_port
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        dev.connect("21JA1245");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port_DO = 0;
            int port_DI = 1; 

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins with digital output 
            err = dev.DO_openPort(port_DO);
            Console.WriteLine($"open DO Port: {err}"); 

            // Open all pins with digital input
            err = dev.DI_openPort(port_DI);
            Console.WriteLine($"open DI Port: {err}"); 
 
            // Set pin0, pin1 and pin2 to high, others to low
            err = dev.DO_writePort(port_DO, new List<int> { 0, 0, 0, 1, 0, 0, 0, 0 });
            Console.WriteLine($"writePort: {err}"); 

            // Read all pins state
            List<int> pin_s = dev.DI_readPort(port_DI);
            Console.WriteLine($"DI_readPort: {pin_s[0]},{pin_s[1]},{pin_s[2]},{pin_s[3]},{pin_s[4]},{pin_s[5]}");

            // Close all pins with digital output
            err = dev.DO_closePort(port_DO);
            Console.WriteLine($"close DO Port: {err}"); 

            // Close all pins with digital input
            err = dev.DI_closePort(port_DI);
            Console.WriteLine($"close DI Port : {err}"); 
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