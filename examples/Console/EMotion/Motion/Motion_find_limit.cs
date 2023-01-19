/// Motion_find_limit.cs
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
        dev.connect("192.168.1.110");
   
        try
        {   
            // Parameters setting
            int err;
            int port = 0;
            
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Motion open
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            // Motion configure
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit: {err}");

            err = dev.Motion_cfgFindRef(port, Const.MOT_AXIS1, Const.MOT_FIND_LIMIT, Const.MOT_DIR_REVERSE);
            Console.WriteLine($"cfgFindRef: {err}");

            err = dev.Motion_cfgHome(port, Const.MOT_AXIS1, Const.MOT_FALSE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgHome: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");
            
            // Motion start 
            err = dev.Motion_findRef(port, Const.MOT_AXIS1);
            Console.WriteLine($"findRef: {err}");

            int finding_referece = 1;
            int found_reference = 0;
            while (found_reference == 0)
            {
                // read forward and reverse limit
                List<int> hit_status = dev.Motion_getLimitStatus(port, Const.MOT_AXIS1); 
                int forward_hit = hit_status[0];
                int reverse_hit = hit_status[1];
                if (forward_hit == 1) { Console.WriteLine($"Forward hit"); }
                if (reverse_hit == 1) { Console.WriteLine($"Reverse hit"); }

                int home_status = dev.Motion_getHomeStatus(port, Const.MOT_AXIS1);
                if (home_status == 1) { Console.WriteLine($"Home hit"); }

                List<int>  driving_status = dev.Motion_checkRef(port, Const.MOT_AXIS1);
                finding_referece = driving_status[0];
                found_reference = driving_status[1];  
                if (found_reference == 1) { Console.WriteLine($"Found reference"); }
                //if (finding_referece == 1) { Console.WriteLine($"Finding reference"); }
            }

            // Motion stop
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");
 
            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");
            
            // Motion close
            err = dev.Motion_close(port);
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