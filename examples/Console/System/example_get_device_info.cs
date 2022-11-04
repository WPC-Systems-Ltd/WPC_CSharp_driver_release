using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// System - example_get_device_info.cs
/// 
/// This example demonstrates how to get hardware & network information from WPC-Wifi-DAQ-E3-A.
/// 
/// First, get hardware information such as firmware model & version & serial number.
/// Last, get network information such as IP & submask & mac.
/// 
/// For other examples please check:
///  https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/Examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>


class example_get_device_info
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Connect to network device
        try
        {
            dev.connect("192.168.5.79");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Perform DAQ basic information 
        try
        {
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");


            // Get serial number & RTC Time
            string serial_num = dev.Sys_getSerialNumber();
            string RTC = dev.Sys_getRTC();
            Console.WriteLine($"Serial number: {serial_num}"); 
            Console.WriteLine($"RTC data time: {RTC}");
             
            // Get IP & submask
            List<string> info = dev.Sys_getIPAddrAndSubmask();
            Console.WriteLine($"IP: {info[0]}");
            Console.WriteLine($"Submask: {info[1]}");

            // Get MAC
            string mac = dev.Sys_getMACAddr();
            Console.WriteLine($"MAC: {mac}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect network device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}