/// get_WifiDAQ_status.cs
/// 
/// This example demonstrates how to get basic information from WifiDAQE3A such as RSSI & battery & thermo.
/// 
//// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_get_WifiDAQ_status
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Connect to device
        dev.connect("192.168.5.79");

        // Perform DAQ basic information 
        try
        {
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Get RSSI, battery and thermo

            int data1 = dev.Wifi_readRSSI();

            int data2 = dev.Wifi_readBattery();

            float data3 = dev.Wifi_readThermo();

            Console.WriteLine($"RSSI: {data1} dBm");

            Console.WriteLine($"Battery: {data2} mV");

            Console.WriteLine($"Thermo: {data3} °C"); 
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