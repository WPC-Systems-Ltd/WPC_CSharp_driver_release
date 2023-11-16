/// AHRS_read.cs with synchronous mode.
///
/// This example demonstrates the process of obtaining AHRS three axis estimation data.
/// Additionally, it retrieve AI data from WifiDAQE3A.
///
/// To begin with, it demonstrates the steps to open the AHRS and configure the AHRS parameters.
/// Next, it outlines the procedure for reading the streaming AHRS data.
/// Finally, it concludes by explaining how to close the AHRS.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class WifiDAQE3A_AHRS_read
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
            int err;
            int port = 0;
            byte mask = 0x01; // data mask
            double theo_grav = 9.81;
            double dt = 0.003;
            double offset_z = 0.003;
            int delay = 100;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AHRS
            err = dev.AHRS_open(port, timeout:timeout);
            Console.WriteLine($"AHRS_open in port {port}: {err}");

            // Set Set general setting
            err = dev.AHRS_setGeneral(port, theo_grav, dt, offset_z, timeout:timeout);
            Console.WriteLine($"AHRS_setGeneral {port}: {err}");

            // Start AHRS
            err = dev.AHRS_start(port, mask, timeout:timeout);
            Console.WriteLine($"AHRS_start in port {port}: {err}");

            // Read AHRS estimation
            for (int i = 0; i < 5; i++) {
                List<float> ahrs_list = dev.AHRS_readStreaming(port, delay:delay);
                Console.WriteLine($"x_esti {ahrs_list[0]}, y_esti {ahrs_list[1]}, z_esti {ahrs_list[2]}");
            }

            // Stop AHRS
            err = dev.AHRS_stop(port, timeout:timeout);
            Console.WriteLine($"AHRS_stop in port {port}: {err}");

            // Close AHRS
            err = dev.AHRS_close(port, timeout:timeout);
            Console.WriteLine($"AHRS_close in port {port}: {err}");
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