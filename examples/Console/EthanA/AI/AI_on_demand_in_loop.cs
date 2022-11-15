/// <summary>
/// AI_on_demand_in_loop.cs
/// 
/// This example demonstrates how to get AI data with loop in ondemand mode from EthanA.
/// 
/// First, it shows how to open AI port and configure AI parameters.
/// 
/// Second, read AI data
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

using WPC_Product;

class EthanA_AI_on_demand_in_loop
{ 
    static void loop_func(EthanA handle, int port, int delay , int timeout =3) 
    {
        int t = 0;
        while (t < timeout) 
        {
            // Data acquisition
            List<double> sample = handle.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            
            // Wait for 0.01 sec
            Thread.Sleep(10); // delay [ms]
            
            t += delay;
        }
        Console.WriteLine("loop_func end");
    }

    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

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
            status = dev.AI_setMode(port, WPC.AI_MODE_ON_DEMAND);
            Console.WriteLine($"AI_setMode status: {status}");

            // Data acquisition
            List<double> sample = dev.AI_readOnDemand(port);

            // Start loop
            loop_func(dev, port, 1, 3);

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