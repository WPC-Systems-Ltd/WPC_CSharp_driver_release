/// PWM_generate.cs with synchronous mode.
///
/// This example demonstrates how to generate PWM with USBDAQF1D.
///
/// First, you should set frequency and duty cycle so that it can generate proper signal.
/// By the way, if you want to check PWM signal, you could connect DI pin with PWM pin.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_PWM_generate
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

        // Connect to device
        try
        {
            dev.connect("default"); // Depend on your device
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
            int channel = 0;
            double frequency = 100;
            double duty_cycle = 100;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open PWM
            err = dev.PWM_open(channel, timeout);
            Console.WriteLine($"PWM_open in channel {channel}: {err}");

            // Set frequency and duty_cycle
            err = dev.PWM_setFrequency(channel, frequency, timeout);
            Console.WriteLine($"PWM_setFrequency in channel {channel}: {err}");

            err = dev.PWM_setDutyCycle(channel, duty_cycle, timeout);
            Console.WriteLine($"PWM_setDutyCycle in channel {channel}: {err}");

            // Start PWM
            err = dev.PWM_start(channel, timeout);
            Console.WriteLine($"PWM_start in channel {channel}: {err}");

            // Wait for data [ms]
            Thread.Sleep(5000); // delay [ms]

            // Stop PWM
            err = dev.PWM_stop(channel, timeout);
            Console.WriteLine($"PWM_stop in channel {channel}: {err}");

            // Close PWM
            err = dev.PWM_close(channel, timeout);
            Console.WriteLine($"PWM_close in channel {channel}: {err}");
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