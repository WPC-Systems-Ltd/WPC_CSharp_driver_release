/// AI_continuous_with_logger.cs
///
/// This example demonstrates how to get AI data and write to CSV file in continuous mode from USBDAQF1AD.
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
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AD_DataLogger_AI_continuous
{
    static void loop_func(USBDAQF1AD handle, Datalogger handle2, int port, int num_of_samples, int delay = 1, int timeout = 3)
    {
        int t = 0;
        while (t < timeout)
        {
            // Data acquisition
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> s in streaming_list)
            {
                // Read acquisition data
                Console.WriteLine($"{s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");

                // Write data into CSV
                handle2.Logger_writeList(s); 
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
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");
 
        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to device
        dev.connect("21JA1245");

        // Create datalogger handle
        Datalogger dev_logger = new Datalogger();
        
        // Open file with WPC_test.csv
        dev_logger.Logger_openFile("WPC_tester_USBDAQF1AD_AI.csv");
        
        // Write header into CSV file
        var data = $"CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7"; 
        dev_logger.Logger_writeValue(data);

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            float sampling_rate = 1000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port);
            Console.WriteLine($"open: {err}");

            // Set AI port and acquisition mode to continuous
            err = dev.AI_setMode(port, Const.AI_MODE_CONTINOUS);
            Console.WriteLine($"setMode: {err}");

            // Set AI port and sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"setSamplingRate: {err}");

            // Set AI port and start acquisition
            err = dev.AI_start(port);
            Console.WriteLine($"start: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Start loop
            loop_func(dev, dev_logger, port, 600, 1, 3);

            // Close AI port
            err = dev.AI_close(port);
            Console.WriteLine($"close: {err}");
            
            // Close File
            dev_logger.Logger_closeFile();
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