/// AI_on_demand_in_loop.cs with synchronous mode.
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
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanA_AI_on_demand_in_loop
{ 
    static void loop_func(EthanA handle, int port, int delay , int timeout =3) 
    {
        int t = 0;
        while (t < timeout) 
        {
            // Data acquisition
            List<double> s = handle.AI_readOnDemand(port);

            // Read acquisition data
            Console.WriteLine($"data: {s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");
            
            // Wait for 0.01 sec
            Thread.Sleep(10); // delay [ms]
            
            t += delay;
        }
        Console.WriteLine("loop_func end");
    }

    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanA dev = new EthanA();

        // Connect to device
        dev.connect("192.168.1.110");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            int timeout = 3000;
                    
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Set AI port and acquisition mode to on demand
            err = dev.AI_setMode(port, Const.AI_MODE_ON_DEMAND, timeout);
            Console.WriteLine($"setMode: {err}");

            // Data acquisition
            List<double> s = dev.AI_readOnDemand(port, timeout);

            // Start loop
            loop_func(dev, port, 1, 3);

            // Close AI port
            err = dev.AI_close(port, timeout);
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