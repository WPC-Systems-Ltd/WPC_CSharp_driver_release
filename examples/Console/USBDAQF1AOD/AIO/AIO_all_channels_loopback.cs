/// AIO_all_channels_loopback.cs  with synchronous mode.
///
/// This example demonstrates the process of AIO loopback across all channels of USBDAQF1AOD.
/// It involves using AO pins to send signals and AI pins to receive signals on a single device, commonly referred to as "loopback".
/// The AI and AO pins are connected using a wire.
///
/// Initially, the example demonstrates the steps required to open the AI and AO port
/// Next, it reads AI data and displays its corresponding values.
/// Following that, it writes digital signals to the AO pins and reads AI on-demand data once again.
/// Lastly, it closes the AO and AI ports.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1AOD_AIO_all_channels_loopback
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1AOD dev = new USBDAQF1AOD();

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
            int port = 0;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Open AO port
            err = dev.AO_open(port, timeout:timeout);
            Console.WriteLine($"AO_open in port{port}: {err}");
            
            // Read data
            List<double> sample = dev.AI_readOnDemand(port, timeout:timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Set AO port and write data simultaneously
            // CH0~CH1 5V, CH2~CH3 3V, CH4~CH5 2V, CH6~CH7 0V
            List<double> AO_values = new List<double> { 5, 5, 3, 3, 2, 2, 0, 0 };
            err = dev.AO_writeAllChannels(port, AO_values, timeout:timeout);
            Console.WriteLine($"AO_writeAllChannels in port{port}: {err}");

            // Read data
            sample = dev.AI_readOnDemand(port, timeout:timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Close AI port
            err = dev.AI_close(port, timeout:timeout);
            Console.WriteLine($"AI_close in port{port}: {err}");

            // Close AO port
            err = dev.AO_close(port, timeout:timeout);
            Console.WriteLine($"AO_close in port{port}: {err}");
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