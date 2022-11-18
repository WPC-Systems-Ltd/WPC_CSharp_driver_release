/// <summary>
/// AI_N_samples_once.cs
/// 
/// This example demonstrates how to get AI data in once in N sample mode from USBDAQF1AD.
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

class USBDAQF1AD_AI_N_samples_once
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to device
        dev.connect("21JA1245");
         
        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 0;
            int samples = 50;
            float sampling_rate = 1000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port and acquisition mode to N-sample mode
            status = dev.AI_setMode(port, Constant.AI_MODE_N_SAMPLE);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port and # of samples to 5 (pts)
            status = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"AI_setNumSamples status: {status}");

            // Set AI port and sampling rate to 1k (Hz)
            status = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"AI_setSamplingRate status: {status}");

            // Set AI port and start acquisition
            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Set AI port and data acquisition
            List<List<double>> streaming_list = dev.AI_readStreaming(port, samples, 10);

            // Read acquisition data
            foreach (List<double> sample in streaming_list)
            {
                Console.WriteLine($"data : {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

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