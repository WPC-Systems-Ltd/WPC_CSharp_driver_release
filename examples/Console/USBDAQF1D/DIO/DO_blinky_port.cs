/// DO_blinky_port.cs
/// 
/// This example demonstrates how to write DO high or low in port from USBDAQF1D.
/// 
/// First, it shows how to open DO in port.
/// 
/// Second, each loop has different voltage output so it will look like blinking. 
/// 
/// Last, close DO in port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
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
        dev.connect("21JA1200");
         
        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            List<int> DO_odd_state = new List<int> { 0, 1, 0, 1, 0, 1, 0, 1 };
            List<int> DO_even_state = new List<int> { 1, 0, 1, 0, 1, 0, 1, 0 };
            
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins and set it to digital output.
            err = dev.DO_openPort(port);
            Console.WriteLine($"openPort: {err}");

            // Toggle digital state for 30 times. Each times delay for 0.1 second
            for (int i = 0; i < 30; i++)
            {
                if (i % 2 == 0)
                {
                    err = dev.DO_writePort(port, DO_even_state);
                }
                else
                {
                    err = dev.DO_writePort(port, DO_odd_state);
                }

                Console.WriteLine($"writePort: {err}"); 
                Thread.Sleep(100);// delay [ms]
            }

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Close all pins with digital output
            err = dev.DO_closePort(port);
            Console.WriteLine($"closePort: {err}"); 
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