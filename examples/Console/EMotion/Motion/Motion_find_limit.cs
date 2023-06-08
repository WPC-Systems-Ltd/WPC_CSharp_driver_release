/// Motion_find_limit.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_find_limit
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EMotion dev = new EMotion();

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
            int axis = Const.MOT_AXIS0;

            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout:timeout);
            Console.WriteLine($"Motion_open in port {port}: {err}");

            /*
            // Motion open configuration file
            err = dev.Motion_openCfgFile(file_name:@"C:\Users\user\Desktop\3AxisStage_2P.ini");
            Console.WriteLine($"Motion_openCfgFile: {err}");

            // Motion load configuration file
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"Motion_loadCfgFile: {err}");
            */

            // Motion configure
            err = dev.Motion_cfgAxis(port, axis, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout:timeout);
            Console.WriteLine($"Motion_cfgAxis in axis{axis}: {err}");

            err = dev.Motion_cfgLimit(port, axis, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_LOW, timeout:timeout);
            Console.WriteLine($"Motion_cfgLimit in axis{axis}: {err}");

            err = dev.Motion_cfgFindRef(port, axis, Const.MOT_FIND_LIMIT, Const.MOT_DIR_REVERSE, search_velo:10000, search_accle:100000, approach_velo_percent:20, en_reset_posi:0, offset_posi:200, timeout:timeout);
            Console.WriteLine($"Motion_cfgFindRef in axis{axis}: {err}");

            err = dev.Motion_cfgHome(port, axis, Const.MOT_FALSE, Const.MOT_ACTIVE_HIGH, timeout:timeout);
            Console.WriteLine($"Motion_cfgHome in axis{axis}: {err}");

            // Servo on
            err = dev.Motion_enableServoOn(port, axis, timeout:timeout);
            Console.WriteLine($"Motion_enableServoOn in axis{axis}: {err}");

            err = dev.Motion_rstEncoderPosi(port, axis, encoder_posi:0, timeout:timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis{axis}: {err}");

            // Motion start
            err = dev.Motion_findRef(port, axis, timeout:timeout);
            Console.WriteLine($"Motion_findRef in axis{axis}: {err}");

            int driving_status = 0;
            while (driving_status == 0)
            {
                // Get forward and reverse limit
                List<int> hit_status = dev.Motion_getLimitStatus(port, axis, timeout:timeout);
                int forward_hit = hit_status[0];
                int reverse_hit = hit_status[1];
                if (forward_hit == 1) { Console.WriteLine($"Forward hit"); }
                if (reverse_hit == 1) { Console.WriteLine($"Reverse hit"); }

                // Get home status
                int home_status = dev.Motion_getHomeStatus(port, axis, timeout:timeout);
                if (home_status == 1) { Console.WriteLine($"Home hit"); }

                // Check reference status
                driving_status = dev.Motion_checkRef(port, axis, timeout:timeout);
                Console.WriteLine($"driving_status: {driving_status}");
            }

            // Motion stop
            err = dev.Motion_stop(port, axis, Const.MOT_STOP_TYPE_DECELERATION, timeout:timeout);
            Console.WriteLine($"Motion_stop in axis{axis}: {err}");

            // Servo off
            err = dev.Motion_enableServoOff(port, axis, timeout:timeout);
            Console.WriteLine($"Motion_enableServoOff in axis{axis}: {err}");

            // Motion close
            err = dev.Motion_close(port, timeout:timeout);
            Console.WriteLine($"Motion_close in port {port}: {err}");
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