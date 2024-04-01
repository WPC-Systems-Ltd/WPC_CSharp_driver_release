/// AHRS_getAngularVelocity.cs with synchronous mode.
///
/// This example demonstrates the process of getting AHRS three axis angular velocity data.
/// Additionally, it retrieve AHRS data from WifiDAQE3AH.
///
/// To begin with, it demonstrates the steps to open the AHRS and configure the AHRS parameters.
/// Next, it outlines the procedure for the AHRS angular velocity data.
/// Finally, it concludes by explaining how to close the AHRS.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3AH_AHRS_getAngularVelocity
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
            int port = 0;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AHRS and update rate is 333 HZ
            err = dev.AHRS_open(port, timeout);
            Console.WriteLine($"AHRS_open in port {port}, status: {err}");

            // Start AHRS
            err = dev.AHRS_start(port, timeout);
            Console.WriteLine($"AHRS_start in port {port}, status: {err}");

            // Get three axis angular velocity
            int counter = 0;
            while (counter < 100){
                counter++;
                List<float> data = dev.AHRS_getAngularVelocity(port, timeout);
                for (int i=0; i<3; i++)
                    Console.WriteLine($"V_x: {data[0]}, V_y: {data[1]}, V_z: {data[2]}");
            }

            // Stop AHRS
            err = dev.AHRS_stop(port, timeout);
            Console.WriteLine($"AHRS_stop in port {port}, status: {err}");

            // Close AHRS
            err = dev.AHRS_close(port, timeout);
            Console.WriteLine($"AHRS_close in port {port}, status: {err}");
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