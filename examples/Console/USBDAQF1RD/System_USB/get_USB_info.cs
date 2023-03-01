/// get_USB_info.cs with synchronous mode.
///
/// This example demonstrates how to get hardware information from USBDAQF1RD.
///
/// First, get hardware information such as firmware model & version.
///
/// Last, get serial number and RTC.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1RD_get_USB_info
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

        // Connect to device
        dev.connect("21JA1385");

        // Perform DAQ basic information
        try
        {
            // Parameters setting
            int timeout = 3000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get serial number
            string serial_num = dev.Sys_getSerialNumber(timeout);
            Console.WriteLine($"Serial number: {serial_num}");

            // Get RTC Time
            string RTC = dev.Sys_getRTC(timeout);
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
    }
}