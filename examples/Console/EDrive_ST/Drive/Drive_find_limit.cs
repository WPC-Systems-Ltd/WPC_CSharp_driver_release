/// Drive_find_limit.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EDrive_ST_Drive_find_limit
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EDrive_ST dev = new EDrive_ST();

        // Connect to device
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
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
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // EDrive-ST open
            err = dev.Drive_open(port, timeout:timeout);
            Console.WriteLine($"Drive_open: {err}");

            // EDrive-ST configure
            err = dev.Drive_cfgAxisDirection(port, Const.MOT_ORIGINAL_DIRECTION, timeout:timeout);
            Console.WriteLine($"Drive_cfgAxisDirection: {err}");

            err = dev.Drive_cfgEncoderDirection(port, Const.MOT_ORIGINAL_DIRECTION, timeout:timeout);
            Console.WriteLine($"Drive_cfgEncoderDirection: {err}");

            err = dev.Drive_cfgLimit(port, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout:timeout);
            Console.WriteLine($"Drive_cfgLimit {err}");

            err = dev.Drive_cfgFindRef(port, Const.MOT_DIR_REVERSE, timeout:timeout);
            Console.WriteLine($"Drive_cfgFindRef {err}");

            err = dev.Drive_rstEncoderPosi(port, timeout:timeout);
            Console.WriteLine($"Drive_rstEncoderPosi {err}");

            // EDrive-ST servo on
            err = dev.Drive_enableServoOn(port, timeout:timeout);
            Console.WriteLine($"Drive_enableServoOn {err}");

            // EDrive-ST find reference
            err = dev.Drive_findReference(port, timeout:timeout);
            Console.WriteLine($"Drive_findReference {err}");

            // Get limit status
            List<int> hit = dev.Drive_getLimitStatus(port, timeout:timeout);
            Console.WriteLine($"reverse_hit: {hit[0]}, forward_hit: {hit[1]}");

            // Wait for 3 seconds for moving
            Thread.Sleep(3000);

            // EDrive-ST stop
            err = dev.Drive_stop(port, Const.MOT_STOP_TYPE_DECELERATION, timeout:timeout);
            Console.WriteLine($"Drive_stop: {err}");

            // EDrive-ST servo off
            err = dev.Drive_enableServoOff(port, timeout:timeout);
            Console.WriteLine($"Drive_enableServoOff: {err}");

            // EDrive-ST close
            err = dev.Drive_close(port, timeout:timeout);
            Console.WriteLine($"Drive_close: {err}");
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