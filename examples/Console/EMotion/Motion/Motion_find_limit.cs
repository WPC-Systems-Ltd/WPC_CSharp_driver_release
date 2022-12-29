/// Motion_find_limit.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_find_limit
{
    static public void Main()
    { 
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        EMotion dev = new EMotion();

        // Connect to device
        dev.connect("192.168.1.110");
   
        try
        {   
            // Parameters setting
            int status;
            int port = 0;
            
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
             
            status = dev.Motion_open(port);
            Console.WriteLine($"Motion_open status: {status}");
             
            status = dev.Motion_cfgAxisParam(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_DIRECTION_CW, Constant.MOTION_DIRECTION_CW, Constant.MOTION_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_cfgAxisParam status: {status}");

            status = dev.Motion_cfgLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_cfgLimit status: {status}");

            status = dev.Motion_cfgFindRef(port, Constant.MOTION_AXIS_1, Constant.MOTION_FIND_FORWARD_AND_REVERSE_LIMIT, Constant.MOTION_DIRECTION_REVERSE);
            Console.WriteLine($"Motion_cfgFindRef status: {status}");

            status = dev.Motion_cfgHome(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENABLE_FALSE, Constant.MOTION_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_cfgHome status: {status}");

            status = dev.Motion_rstEncoderPosi(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_rstEncoderPosi status: {status}");
             
            status = dev.Motion_findRef(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_findRef status: {status}");

            int finding_referece = 1;
            int found_reference = 0;
            while (found_reference == 0)
            {
                // read forward and reverse limit
                List<int> hit_status = dev.Motion_readLimitStatus(port, Constant.MOTION_AXIS_1); 
                int forward_hit = hit_status[0];
                int reverse_hit = hit_status[1];
                if (forward_hit == 1) { Console.WriteLine($"Forward hit"); }
                if (reverse_hit == 1) { Console.WriteLine($"Reverse hit"); }

                int home_status = dev.Motion_readHomeStatus(port, Constant.MOTION_AXIS_1);
                if (home_status == 1) { Console.WriteLine($"Home hit"); }

                List<int>  driving_status = dev.Motion_checkRef(port, Constant.MOTION_AXIS_1);
                finding_referece = driving_status[0];
                found_reference = driving_status[1];  
                if (found_reference == 1) { Console.WriteLine($"Found reference"); }
                //if (finding_referece == 1) { Console.WriteLine($"Finding reference"); }
            } 

            status = dev.Motion_stop(port, Constant.MOTION_AXIS_1, Constant.MOTION_STOP_TYPE_DECELERATION);
            Console.WriteLine($"Motion_stop status: {status}");
 
            status = dev.Motion_close(port);
            Console.WriteLine($"Motion_close status: {status}");
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