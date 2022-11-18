/// <summary>
/// AI_continuous.cs
///
/// This example demonstrates how to get AI data with loop in continuous mode from EthanA.
/// 
/// First, it shows how to open AI port and configure AI parameters.
/// 
/// Second, read AI streaming data.
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

class EthanA_AI_continuous
{
    static void loop_func(EthanA handle, int port, int num_of_samples, int delay = 1, int timeout = 3)
    {
        int t = 0;
        while (t < timeout)
        {
            // Data acquisition
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> sample in streaming_list)
            {
                // Read acquisition data
                Console.WriteLine($"data : {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

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
            float sampling_rate = 1000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port and acquisition mode to continuous
            status = dev.AI_setMode(port, Constant.AI_MODE_CONTINOUS);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port and sampling rate to 1k (Hz)
            status = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"AI_setSamplingRate status: {status}");

            // Set AI port and start acquisition
            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Start loop
            loop_func(dev, port, 600, 1, 3);

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