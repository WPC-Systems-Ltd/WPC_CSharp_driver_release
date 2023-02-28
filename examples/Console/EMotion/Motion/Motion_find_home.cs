/// Motion_find_home.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_find_home
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EMotion dev = new EMotion();

        // Connect to device
        dev.connect("192.168.1.110");

        try
        {
            // Parameters setting
            int err;
            int port = 0;
            int timeout = 3000;

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"open: {err}");

            // Motion configure
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"cfgAxis: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH, timeout);
            Console.WriteLine($"cfgLimit: {err}");

            err = dev.Motion_cfgFindRef(port, Const.MOT_AXIS1, Const.MOT_FIND_HOME, Const.MOT_DIR_REVERSE, timeout);
            Console.WriteLine($"cfgFindRef: {err}");

            err = dev.Motion_cfgHome(port, Const.MOT_AXIS1, Const.MOT_FALSE, Const.MOT_ACTIVE_HIGH, timeout);
            Console.WriteLine($"cfgHome: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE, timeout);
            Console.WriteLine($"enableServoOn: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1, timeout);
            Console.WriteLine($"rstEncoderPosi: {err}");

            // Motion start
            err = dev.Motion_findRef(port, Const.MOT_AXIS1, timeout);
            Console.WriteLine($"findRef: {err}");

            int finding = 1;
            int found = 0;
            while (found == 0)
            {
                // read forward and reverse limit status
                List<int> hit_status = dev.Motion_getLimitStatus(port, Const.MOT_AXIS1, timeout);
                int forward_hit = hit_status[0];
                int reverse_hit = hit_status[1];
                if (forward_hit == 1) { Console.WriteLine($"Forward hit"); }
                if (reverse_hit == 1) { Console.WriteLine($"Reverse hit"); }

                // read home status
                int home_status = dev.Motion_getHomeStatus(port, Const.MOT_AXIS1, timeout);
                if (home_status == 1) { Console.WriteLine($"Home hit"); }

                // Check finding and found status
                List<int> driving_status = dev.Motion_checkRef(port, Const.MOT_AXIS1, timeout);
                finding = driving_status[0];
                found = driving_status[1];
                if (found == 1) { Console.WriteLine($"Found refernce"); }
                //if (finding == 1) { Console.WriteLine($"Finding refernce"); }
            }

            // Motion stop
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION, timeout);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE, timeout);
            Console.WriteLine($"enableServoOn: {err}");

            // Motion close
            err = dev.Motion_close(port, timeout);
            Console.WriteLine($"close: {err}");
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