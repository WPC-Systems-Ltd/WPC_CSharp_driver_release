/// <summary>
/// Get_USB_info.cs
/// 
/// This example demonstrates how to get hardware information from USBDAQF1CD.
/// 
/// First, get hardware information such as firmware model & version.
/// 
/// Last, get serial number and RTC.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC_Product;

class USBDAQF1CD_get_USB_info
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

        // Connect to device
        dev.connect("21JA1312");

        // Perform DAQ basic information 
        try
        {
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get serial number 
            string serial_num = dev.Sys_getSerialNumber();
            Console.WriteLine($"Serial number: {serial_num}");

            // Get RTC Time
            string RTC = dev.Sys_getRTC(); 
            Console.WriteLine($"RTC data time: {RTC}");
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