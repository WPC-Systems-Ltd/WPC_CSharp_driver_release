
/// CAN_write.cs with synchronous mode.
///
/// This example demonstrates how to write data to another device with CAN interface from USBDAQF1CD.
///
/// First, it shows how to open CAN port and configure CAN parameters.
///
/// Second, write bytes to another device.
///
/// Last, stop and close CAN port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1CD_CAN_write
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

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open CAN
            err = dev.CAN_open(port, timeout:timeout);
            Console.WriteLine($"CAN_open in port{port}: {err}");

            // Set CAN port and set speed to 125K
            err = dev.CAN_setSpeed(port, Const.CAN_SPEED_125K, timeout:timeout);
            Console.WriteLine($"CAN_setSpeed in port{port}: {err}");

            // Set CAN port and start CAN
            err = dev.CAN_start(port);
            Console.WriteLine($"CAN_start in port{port}: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // ID: 10, data with 8 bytes, Standard & Data
            err = dev.CAN_write(port, 10, new List<byte> { 33, 22, 11, 88, 77, 55, 66, 22 }, Const.CAN_FRAME_TYPE_DATA, Const.CAN_ID_STANDARD, timeout:timeout);
            Console.WriteLine($"CAN_write in port{port}: {err}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms]

            // ID: 20, data with 8 bytes, Standard & Data
            err = dev.CAN_write(port, 20, new List<byte> { 1, 2, 3 }, Const.CAN_FRAME_TYPE_DATA, Const.CAN_ID_STANDARD, timeout:timeout);
            Console.WriteLine($"CAN_write in port{port}: {err}");

            // Stop CAN
            err = dev.CAN_stop(port, timeout:timeout);
            Console.WriteLine($"CAN_stop in port{port}: {err}");

            // Close CAN
            err = dev.CAN_close(port, timeout:timeout);
            Console.WriteLine($"CAN_close in port{port}: {err}");
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