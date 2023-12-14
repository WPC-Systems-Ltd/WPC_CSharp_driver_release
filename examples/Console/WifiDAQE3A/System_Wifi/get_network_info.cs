/// get_network_info.cs with synchronous mode.
///
/// This example demonstrates how to get hardware & network information from WifiDAQE3A.
///
/// First, get hardware information such as firmware model & version.
///
/// Last, get network information such as IP & submask & MAC.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_get_network_info
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Connect to device
        try
        {
            dev.connect("192.168.5.35"); // Depend on your device
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
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get serial number & RTC Time
            string serial_num = dev.Sys_getSerialNumber(timeout);
            string RTC = dev.Sys_getRTC(timeout);
            Console.WriteLine($"Serial number: {serial_num}");
            Console.WriteLine($"RTC data time: {RTC}");

            // Get IP & submask
            List<string> info = dev.Sys_getIPAddrAndSubmask(timeout);
            Console.WriteLine($"IP: {info[0]}");
            Console.WriteLine($"Submask: {info[1]}");

            // Get MAC
            string mac = dev.Sys_getMACAddr(timeout);
            Console.WriteLine($"MAC: {mac}");
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