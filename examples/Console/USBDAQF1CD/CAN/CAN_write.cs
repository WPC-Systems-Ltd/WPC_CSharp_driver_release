
/// CAN_write.cs
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
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1CD_CAN_write
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

        // Connect to device
        dev.connect("21JA1312");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open CAN
            status = dev.CAN_open(port);
            Console.WriteLine($"CAN_open status: {status}");

            // Set CAN port and set speed to 125K
            status = dev.CAN_setSpeed(port, Constant.CAN_SPEED_125K);
            Console.WriteLine($"CAN_setSpeed status: {status}");

            // Set CAN port and start CAN
            status = dev.CAN_start(port);
            Console.WriteLine($"CAN_start status: {status}");

            // ID: 10, data with 8 bytes, Standard & Data 
            status = dev.CAN_write(port, 10, new List<byte> { 33, 22, 11, 88, 77, 55, 66, 22 }, Constant.CAN_FRAME_TYPE_DATA, Constant.CAN_ID_STANDARD);
            Console.WriteLine($"CAN_write status: {status}");

            // Wait for 1 sec
            Thread.Sleep(1000); // delay [ms] 

            // ID: 20, data with 8 bytes, Standard & Data 
            status = dev.CAN_write(port, 20, new List<byte> { 1, 2, 3 }, Constant.CAN_FRAME_TYPE_DATA, Constant.CAN_ID_STANDARD);
            Console.WriteLine($"CAN_write status: {status}");

            // Stop CAN
            status = dev.CAN_stop(port);
            Console.WriteLine($"CAN_stop status: {status}");

            // Close CAN
            status = dev.CAN_close(port);
            Console.WriteLine($"CAN_close status: {status}"); 
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