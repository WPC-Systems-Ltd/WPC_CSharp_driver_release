/// AI_on_demand_once.cs
/// 
/// This example demonstrates how to get AI data in once in on demand mode from USBDAQF1AD.
/// 
/// First, it shows how to open AI port.
/// 
/// Second, read AI ondemand data.
/// 
/// Last, close AI port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AD_AI_on_demand_once
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to device
        dev.connect("21JA1245");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int err;
            int port = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port);
            Console.WriteLine($"open: {err}");

            // Set AI port and acquisition mode to on demand
            err = dev.AI_setMode(port, Const.AI_MODE_ON_DEMAND);
            Console.WriteLine($"setMode: {err}");

            // Set AI port and data acquisition
            List<double> s = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");

            // Close AI port
            err = dev.AI_close(port);
            Console.WriteLine($"close: {err}");
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