/// <summary>
/// Find_all_devices.cs 
/// 
/// This example demonstrates how to find all available USB and ethernet devices.
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

class DeviceFinder_find_all_devices
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        DeviceFinder dev = new DeviceFinder();

        // Connect to device
        dev.connect();
 
        // Perform device information 
        try
        {
            List<List<string>> usb_list = dev.Bcst_enumerateUSBDevices();
            foreach (List<string> usb in usb_list)
            {
                foreach (string s in usb)
                {
                    Console.Write(s);
                    Console.Write("  ");
                }
                Console.WriteLine();
            }

            List<List<string>> net_list = dev.Bcst_enumerateNetworkDevices(2000);
            foreach (List<string> net in net_list)
            {
                foreach (string s in net)
                {
                    Console.Write(s);
                    Console.Write("  ");
                }
                Console.WriteLine();
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

        Console.WriteLine("End example code...");
    }
}