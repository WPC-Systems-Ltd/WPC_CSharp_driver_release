/// set_and_get_RTC.cs with synchronous mode.
///
/// This example demonstrates how to set and get RTC from USBDAQF1TD.
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_set_and_get_RTC
{
    static public void Main()
    {
        // Parameters setting
        int timeout = 3000; // ms

        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

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
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Set RTC
            int err = dev.Sys_setRTC(2023, 5, 8, 15, 8, 7, timeout);
            Console.WriteLine($"Set RTC to 2023-05-08, 15:08:07 , status: {err}");

            for(int i=0; i<10; i++){
                string rtc_time = dev.Sys_getRTC(timeout);
                Console.WriteLine($"Get RTC: {rtc_time}");
                Thread.Sleep(1000); // delay [ms]
            }
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