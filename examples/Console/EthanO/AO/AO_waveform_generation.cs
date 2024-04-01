/// AO_waveform_generation.cs with synchronous mode.
///
/// This example demonstrates the process of writing AO signal of EthanO.
/// To begin with, it demonstrates the steps to open the AO and configure the AO parameters.
/// Next, it outlines the procedure for AO streaming.
/// Finally, it concludes by explaining how to close the AO.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
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
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Release device handle
            dev.close();
            return;
        }

        try
        {
            // Parameters setting
            int err;
            int port = 0;
            List<int> AO_pins_enabled = new List<int> { 0, 1 };
            double sampling_rate = 10000;
            int number_of_sample = 10000;
            double amplitude = 1;
            double offset = 0.5;
            double freq_0 = 10;
            double freq_1 = 20;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AO
            err = dev.AO_open(port, timeout);
            Console.WriteLine($"AO_open in port {port}, status: {err}");

            // Set AO generation mode
            err = dev.AO_setMode(port, Const.AO_MODE_CONTINOUS, timeout);
            Console.WriteLine($"AO_setMode in port {port}, status: {err}");

            // Set AO sampling rate to 10k (Hz)
            err = dev.AO_setSamplingRate(port, sampling_rate, timeout);
            Console.WriteLine($"AO_setSamplingRate in port {port}, status: {err}");

            // Set AO NumSamples to 10000
            err = dev.AO_setNumSamples(port, number_of_sample, timeout);
            Console.WriteLine($"AO_setNumSamples in port {port}, status: {err}");

            // Set AO enabled channels
            err = dev.AO_setEnableChannels(port, AO_pins_enabled, timeout);
            Console.WriteLine($"AO_setEnableChannels in port {port}, status: {err}");

            // Set AO form in channel 0
            err = dev.AO_setForm(port, 0, Const.AO_FORM_SINE, timeout);
            Console.WriteLine($"AO_setForm in channel 0 in port {port}, status: {err}");

            // Set AO form in channel 1
            err = dev.AO_setForm(port, 1, Const.AO_FORM_TRIANGULAR, timeout);
            Console.WriteLine($"AO_setForm in channel 1 in port {port}, status: {err}");

            // Set Channel 0 form parameters
            err = dev.AO_setFormParam(port, 0, amplitude, offset, freq_0, timeout);
            Console.WriteLine($"AO_setFormParam in channel 0 in port {port}, status: {err}");

            // Set Channel 1 form parameters
            err = dev.AO_setFormParam(port, 1, amplitude, offset, freq_1, timeout);
            Console.WriteLine($"AO_setFormParam in channel 1 in port {port}, status: {err}");

            // Open AO streaming
            List<int> AO_info = dev.AO_openStreaming(port, timeout);
            Console.WriteLine($"AO mode {AO_info[0]}, sampling rate {AO_info[1]}");

            // Start AO streaming
            err = dev.AO_startStreaming(port);
            Console.WriteLine($"AO_startStreaming in port {port}, status: {err}");

            // Wait for 10000 ms to generate form
            Thread.Sleep(10000); // delay [ms]

            // Close AO streaming
            err = dev.AO_closeStreaming(port, timeout);
            Console.WriteLine($"AO_closeStreaming in port {port}, status: {err}");

            // Close AO
            err = dev.AO_close(port);
            Console.WriteLine($"AO_close in port {port}, status: {err}");
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
