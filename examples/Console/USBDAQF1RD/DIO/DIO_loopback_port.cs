/// <summary>
/// DIO_loopback_port.cs
/// 
/// This example demonstrates how to write DIO loopback in port from USBDAQF1RD.
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
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC_Product;

class USBDAQF1RD_DIO_loopback_port
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

        // Connect to device
        dev.connect("21JA1245");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port_DO = 0;
            int port_DI = 1; 

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins with digital output 
            status = dev.DO_openPort(port_DO);
            Console.WriteLine($"DO_openPort status: {status}");

            // Open all pins with digital input
            status = dev.DI_openPort(port_DI);
            Console.WriteLine($"DI_openPort status: {status}");

            // Set pin0, pin1 and pin2 to high, others to low
            status = dev.DO_writePort(port_DO, new List<int> { 0, 0, 0, 1, 0, 0, 0, 0 });
            Console.WriteLine($"DO_writePort status: {status}");

            // Read all pins state
            List<int> pin_s = dev.DI_readPort(port_DI);
            Console.WriteLine($"DI_readPort: {pin_s[0]},{pin_s[1]},{pin_s[2]},{pin_s[3]},{pin_s[4]},{pin_s[5]}");

            // Close all pins with digital output
            status = dev.DO_closePort(port_DO);
            Console.WriteLine($"DO_closePort status: {status}");

            // Close all pins with digital input
            status = dev.DI_closePort(port_DI);
            Console.WriteLine($"DI_closePort status: {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}