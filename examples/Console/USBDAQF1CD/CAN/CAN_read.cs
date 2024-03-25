/// CAN_read.cs with synchronous mode.
///
/// This example demonstrates how to read data from another device with CAN interface from USBDAQF1CD.
///
/// First, it shows how to open CAN port and configure CAN parameters.
///
/// SSecond, read bytes from another device.
///
/// Last, stop and close CAN port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;
using WPC.Module;

class USBDAQF1CD_CAN_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

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
            int port = 1;
            int timeout = 3000; // ms
            int read_delay = 200; // ms
            int num_frames = 5;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open CAN
            err = dev.CAN_open(port, timeout);
            Console.WriteLine($"CAN_open in port {port}: {err}");

            // Set CAN port and set speed to 125K
            err = dev.CAN_setSpeed(port, Const.CAN_SPEED_125K, timeout);
            Console.WriteLine($"CAN_setSpeed in port {port}: {err}");

            // Set CAN port and start CAN
            err = dev.CAN_start(port);
            Console.WriteLine($"CAN_start in port {port}: {err}");

            for (int i = 0; i < 1000; i++)
            {
                List<CANFrame> frame_list = dev.CAN_read(port, num_frames, read_delay);
                if (frame_list.Count() > 0)
                {
                    foreach (CANFrame frame in frame_list)
                    {
                        WPC_utilities.printByteList(frame.frame_in_list);
                    }
                }
                else
                {
                    // Wait for 10 ms
                    Thread.Sleep(10); // delay [ms]
                }
            }

            // Stop CAN
            err = dev.CAN_stop(port, timeout);
            Console.WriteLine($"CAN_stop in port {port}: {err}");

            // Close CAN
            err = dev.CAN_close(port, timeout);
            Console.WriteLine($"CAN_close in port {port}: {err}");
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