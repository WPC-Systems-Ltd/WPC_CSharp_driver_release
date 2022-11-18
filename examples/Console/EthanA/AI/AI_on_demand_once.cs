/// <summary>
/// AI_on_demand_once.cs
/// 
/// This example demonstrates how to get AI data in once in on demand mode from EthanA.
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
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC.Product;

class EthanA_AI_on_demand_once
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        EthanA dev = new EthanA();

        // Connect to device
        dev.connect("192.168.1.110");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int status;
            int port = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port and acquisition mode to on demand
            status = dev.AI_setMode(port, Constant.AI_MODE_ON_DEMAND);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port and data acquisition
            List<double> sample = dev.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");

            // Close AI port
            status = dev.AI_close(port);
            Console.WriteLine($"AI_close status: {status}");
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