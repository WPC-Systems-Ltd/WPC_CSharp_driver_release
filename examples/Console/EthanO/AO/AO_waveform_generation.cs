/// <summary>
/// AO_waveform_generation.cs
/// 
/// This example demonstrates how to use AO waveform generation in specific channels from EthanO.
/// 
/// First, it shows how to open AO in port.
/// 
/// Second, set AO streaming parameters
///
/// Last, close AO in port.
///
/// This example demonstrates how to write AO in all channels from EthanO.
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

class EthanO_AO_waveform_generation
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        EthanO dev = new EthanO();

        // Connect to device
        dev.connect("192.168.1.110");

        // Execute
        try
        { 
            // Parameters setting
            int status; 
            int port = 0; 
            List<int> AO_pins_enabled = new List<int> { 0, 0, 0, 0, 0, 0, 1, 1 };
            double sampling_rate = 1000; 
            double amplitude = 1;
            double offset = 0.5;
            double period_0 = 0.2;
            double period_1 = 0.1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO port
            status = dev.AO_open(port);
            Console.WriteLine($"AO_open status: {status}");
            
            // Set AO enabled channels
            status = dev.AO_setEnableChannels(port, AO_pins_enabled);
            Console.WriteLine($"AO_setEnableChannels status: {status}");

            // Set AO form in channel 0
            status = dev.AO_setForm(port, 0, Constant.AO_FORM_TRIANGULAR);
            Console.WriteLine($"AO_setForm_async in channel 0 status : {status}");

            // Set AO form in channel 1
            status = dev.AO_setForm(port, 1, Constant.AO_FORM_TRIANGULAR);
            Console.WriteLine($"AO_setForm_async in channel 1 status : {status}");

            // Set Channel 0 form parameters
            status = dev.AO_setFormParam(port, 0, amplitude, offset, period_0);
            Console.WriteLine($"AO_setFormParam in channel 0 status : {status}");

            // Set Channel 1 form parameters
            status = dev.AO_setFormParam(port, 1, amplitude, offset, period_1);
            Console.WriteLine($"AO_setFormParam in channel 1 status : {status}");

            // Set AO port and generation mode
            status = dev.AO_setMode(port, Constant.AO_MODE_CONTINOUS);
            Console.WriteLine($"AO_setMode status : {status}");

            // Set AO port and sampling rate to 1k (Hz)
            status = dev.AO_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"AO_setSamplingRate status : {status}");

            // Open AO streaming
            List<int> AO_info = dev.AO_openStreaming(port);
            Console.WriteLine($"AO mode {AO_info[0]}, sampling rate {AO_info[1]}");

            // Start AO streaming
            status = dev.AO_startStreaming(port);
            Console.WriteLine($"AO_startStreaming status: {status}");

            // Wait for 10 sec
            Thread.Sleep(10000); // delay [ms]

            // Close AO streaming
            status = dev.AO_closeStreaming(port);
            Console.WriteLine($"AO_closeStreaming status: {status}");

            // Close AO port
            status = dev.AO_close(port);
            Console.WriteLine($"AO_close status: {status}");
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