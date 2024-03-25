/// set_LED_status.cs with synchronous mode.
///
/// This example demonstrates how to set LED status from WifiDAQE3A.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_set_LED_status
{
    static public void Main()
    {
        // Parameters setting
        int timeout = 3000; // ms
        int value = 1;
        int err;

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
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            for (int i=3; i<=0; i--){
                // Reset LED status
                err = dev.Wifi_resetLED(timeout);
                Console.WriteLine($"Wifi_resetLED: {err}");

                // Set green LED status
                err = dev.Wifi_setGreenLED(value, timeout);
                Console.WriteLine($"Wifi_setGreenLED: {err}");
                Thread.Sleep(1000); // delay [ms]

                // Reset LED status
                err = dev.Wifi_resetLED(timeout);
                Console.WriteLine($"Wifi_resetLED: {err}");

                // Set blue LED status
                err = dev.Wifi_setBlueLED(value, timeout);
                Console.WriteLine($"Wifi_setBlueLED: {err}");
                Thread.Sleep(1000); // delay [ms]

                // Reset LED status
                err = dev.Wifi_resetLED(timeout);
                Console.WriteLine($"Wifi_resetLED: {err}");

                // Set red LED status
                err = dev.Wifi_setRedLED(value, timeout);
                Console.WriteLine($"Wifi_setRedLED: {err}");
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