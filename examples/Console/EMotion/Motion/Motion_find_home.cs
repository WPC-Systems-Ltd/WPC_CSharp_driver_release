/// <summary>
/// Motion_find_home.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC.Product;

class EMotion_find_home
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

            status = dev.Motion_setEnableLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_FORWARD_ENABLE, Constant.MOTION_REVERSE_ENABLE);
            Console.WriteLine($"Motion_writeEnableLimit status: {status}"); 

            status = dev.Motion_setLimitPolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_LIMIT_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_writeLimitPolarity status: {status}");

            status = dev.Motion_setHomePolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_HOME_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_writeHomePolarity status: {status}");
             
            status = dev.Motion_configAxisModeAndDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_AXIS_DIR_CW);
            Console.WriteLine($"Motion_configAxisModeAndDirection status: {status}");
             
            status = dev.Motion_resetEncoderPosition(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_resetEncoderPosition status: {status}");

            status = dev.Motion_findReference(port, Constant.MOTION_AXIS_1, Constant.MOTION_FIND_HOME, Constant.MOTION_FIND_REFERENCE_DIR_REVERSE);
            Console.WriteLine($"Motion_findReference status: {status}");

            int finding_referece = 1;
            int found_reference = 0;
            while (found_reference == 0)
            {
                // read forward and reverse limit status
                List<int> hit_status = dev.Motion_readLimitStatus(port, Constant.MOTION_AXIS_1); 
                int forward_hit = hit_status[0];
                int reverse_hit = hit_status[1];
                if (forward_hit == 1) { Console.WriteLine($"Forward hit"); }
                if (reverse_hit == 1) { Console.WriteLine($"Reverse hit"); }

                // read home status
                int home_status = dev.Motion_readHomeStatus(port, Constant.MOTION_AXIS_1);
                if (home_status == 1) { Console.WriteLine($"Home hit"); }
                
                // Check finding and found status
                List<int>  driving_status = dev.Motion_checkReference(port, Constant.MOTION_AXIS_1);
                finding_referece = driving_status[0];
                found_reference = driving_status[1];  
                if (found_reference == 1) { Console.WriteLine($"Found refernce"); }
                if (finding_referece == 1) { Console.WriteLine($"Finding refernce"); }
            } 
            status = dev.Motion_stop(port, Constant.MOTION_STOP_TYPE_DECELERATION, Constant.MOTION_AXIS_1);
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