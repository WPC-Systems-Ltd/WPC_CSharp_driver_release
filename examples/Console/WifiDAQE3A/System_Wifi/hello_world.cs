/// hello_world.cs with synchronous mode.
///
/// This example code is in the public domain from WifiDAQE3A.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_hello_world
{
    static public void Main()
    {
        // Parameters setting
        int timeout = 3000; // ms

        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

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
            for (int i=10; i<=0; i--){
                Console.WriteLine($"Restarting in {i} seconds...");
                Thread.Sleep(1000); // delay [ms]
            }

            Console.WriteLine($"Restarting now");
            dev.Sys_reboot(timeout);
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