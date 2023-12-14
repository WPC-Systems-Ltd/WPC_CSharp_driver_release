/// AIO_one_channel_loopback.cs with synchronous mode.
///
/// This example demonstrates the process of AIO loopback with specific channels of USBDAQF1AOD.
/// It involves using AO pins to send signals and AI pins to receive signals on a single device, commonly referred to as "loopback".
/// The AI and AO pins are connected using a wire.
///
/// Initially, the example demonstrates the steps required to open the AI and AO.
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

class USBDAQF1AOD_AIO_one_channel_loopback
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
            int channel = 8;
            int timeout = 3000; // ms
            List<double> ao_value_list = new List<double>() {0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5};

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port {port}: {err}");
            
            // Set AI channel
            err = dev.AI_enableChannel(port, channel, timeout);
            Console.WriteLine($"AI_enableChannel in port {port}: {err}");

            // Open AO
            err = dev.AO_open(port, timeout);
            Console.WriteLine($"AO_open in port {port}: {err}");

            // Read data acquisition
            List<double> ai_list = dev.AI_readOnDemand(port, timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));

            // Write AO vaule in channel 0
            err = dev.AO_writeOneChannel(port, 0, ao_value_list[0], timeout);
            Console.WriteLine($"In port {port} channel 0, the AO value is {ao_value_list[0]}: {err}");

            // Write AO vaule in channel 1
            err = dev.AO_writeOneChannel(port, 1, ao_value_list[1], timeout);
            Console.WriteLine($"In port {port} channel 1, the AO value is {ao_value_list[1]}: {err}");

            // Write AO vaule in channel 2
            err = dev.AO_writeOneChannel(port, 2, ao_value_list[2], timeout);
            Console.WriteLine($"In port {port} channel 2, the AO value is {ao_value_list[2]}: {err}");

            // Write AO vaule in channel 3
            err = dev.AO_writeOneChannel(port, 3, ao_value_list[3], timeout);
            Console.WriteLine($"In port {port} channel 3, the AO value is {ao_value_list[3]}: {err}");

            // Read data acquisition
            ai_list = dev.AI_readOnDemand(port, timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", ai_list)));

            // Close AI
            err = dev.AI_close(port, timeout);
            Console.WriteLine($"AI_close in port {port}: {err}");

            // Close AO
            err = dev.AO_close(port, timeout);
            Console.WriteLine($"AO_close in port {port}: {err}");
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
