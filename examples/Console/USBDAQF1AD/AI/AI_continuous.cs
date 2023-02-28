/// AI_continuous.cs with synchronous mode.
///
/// This example demonstrates how to get AI data with loop in continuous mode from USBDAQF1AD.
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

class USBDAQF1AD_AI_continuous
{
    static void loop_func(USBDAQF1AD handle, int port, int num_of_samples, int delay = 1, int timeout = 3)
    {
        int t = 0;
        while (t < timeout)
        {
            // Data acquisition
            List<List<double>> streaming_list = handle.AI_readStreaming(port, num_of_samples, delay);

            foreach (List<double> s in streaming_list)
            {
                // Read acquisition data
                Console.WriteLine($"data : {s[0]}, {s[1]}, {s[2]}, {s[3]}, {s[4]}, {s[5]}, {s[6]}, {s[7]}");
            }

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
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to device
        dev.connect("21JA1245");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            float sampling_rate = 1000;
            int timeout = 3000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI port
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Set AI port and acquisition mode to continuous
            err = dev.AI_setMode(port, Const.AI_MODE_CONTINUOUS, timeout);
            Console.WriteLine($"setMode: {err}");

            // Set AI port and sampling rate to 1k (Hz)
            err = dev.AI_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"setSamplingRate: {err}");

            // Set AI port and start acquisition
            err = dev.AI_start(port, timeout);
            Console.WriteLine($"start: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // Start loop
            loop_func(dev, port, 600, 1, 3);

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