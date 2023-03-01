/// AO_waveform_generation.cs with synchronous mode.
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
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanO_AO_waveform_generation
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanO dev = new EthanO();

        // Connect to device
        dev.connect("192.168.1.110");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 0;
            List<int> AO_pins_enabled = new List<int> { 0, 0, 0, 0, 0, 0, 1, 1 };
            double sampling_rate = 1000;
            double amplitude = 1;
            double offset = 0.5;
            double period_0 = 0.2;
            double period_1 = 0.1;
            int timeout = 3000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO port
            err = dev.AO_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Set AO enabled channels
            err = dev.AO_setEnableChannels(port, AO_pins_enabled, timeout);
            Console.WriteLine($"setEnableChannels: {err}");

            // Set AO form in channel 0
            err = dev.AO_setForm(port, 0, Const.AO_FORM_TRIANGULAR, timeout);
            Console.WriteLine($"setForm: {err}");

            // Set AO form in channel 1
            err = dev.AO_setForm(port, 1, Const.AO_FORM_TRIANGULAR, timeout);
            Console.WriteLine($"setForm: {err}");

            // Set Channel 0 form parameters
            err = dev.AO_setFormParam(port, 0, amplitude, offset, period_0, timeout);
            Console.WriteLine($"setFormParam: {err}");

            // Set Channel 1 form parameters
            err = dev.AO_setFormParam(port, 1, amplitude, offset, period_1, timeout);
            Console.WriteLine($"setFormParam: {err}");

            // Set AO port and generation mode
            err = dev.AO_setMode(port, Const.AO_MODE_CONTINOUS, timeout);
            Console.WriteLine($"setMode: {err}");

            // Set AO port and sampling rate to 1k (Hz)
            err = dev.AO_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"setSamplingRate: {err}");

            // Open AO streaming
            List<int> AO_info = dev.AO_openStreaming(port, timeout);
            Console.WriteLine($"AO mode {AO_info[0]}, sampling rate {AO_info[1]}");

            // Start AO streaming
            err = dev.AO_startStreaming(port, timeout);
            Console.WriteLine($"startStreaming: {err}");

            // Wait for 10 sec
            Thread.Sleep(10000); // delay [ms]

            // Close AO streaming
            err = dev.AO_closeStreaming(port, timeout);
            Console.WriteLine($"closeStreaming: {err}");

            // Close AO port
            err = dev.AO_close(port);
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