/// SD_write.cs with synchronous mode.
///
/// This example demonstrates how to write a message in SD card from WifiDAQE3AH.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3AH_SD_write
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        WifiDAQE3AH dev = new WifiDAQE3AH();

        // Connect to device
        try
        {
            dev.connect("192.168.5.38"); // Depend on your device
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
            uint storage;
            string filename = "WPC_test.txt";
            string write_data = "12345";
            int mode = 0; // 0 : write, 1 : read
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open file in sdcard
            err = dev.SD_openFile(filename, mode, timeout);
            Console.WriteLine($"SD_openFile, status: {err}");

            // Get sdcard storage
            storage = dev.SD_getStorage(timeout);
            Console.WriteLine($"SD_getStorage: {storage} MB");

            // Write data in sdcard
            err = dev.SD_writeFile(write_data, timeout);
            Console.WriteLine($"SD_writeFile, status: {err}");

            // Close file in sdcard
            err = dev.SD_closeFile(timeout);
            Console.WriteLine($"SD_closeFile, status: {err}");

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