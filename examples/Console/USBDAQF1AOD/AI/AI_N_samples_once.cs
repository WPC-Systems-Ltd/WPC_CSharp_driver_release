/// AI_N_samples_once.cs
/// 
/// This example demonstrates how to get AI data in once in N sample mode from USBDAQF1AOD.
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
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AI_N_samples_once
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

        // Connect to device
        dev.connect("21JA1439");
         
        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            int samples = 50;
            float sampling_rate = 1000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port);
            Console.WriteLine($"open: {err}");

            // Set AI port and acquisition mode to N-sample mode
            err = dev.AI_setMode(port, Const.AI_MODE_N_SAMPLE);
            Console.WriteLine($"setMode: {err}");

            // Set AI port and # of samples to 5 (pts)
            err = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"setNumSamples: {err}");

            // Set AI port and sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"setSamplingRate: {err}");

            // Set AI port and start acquisition
            err = dev.AI_start(port);
            Console.WriteLine($"start: {err}");

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