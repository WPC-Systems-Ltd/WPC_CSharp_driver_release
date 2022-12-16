/// <summary>
/// Motion_velocity_blending.cs
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

class EMotion_velocity_blending
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

            status = dev.Motion_configAxisModeAndDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_AXIS_DIR_CW);
            Console.WriteLine($"Motion_configAxisModeAndDirection status: {status}");
             
            status = dev.Motion_configAxisMove(port, Constant.MOTION_AXIS_1, Constant.MOTION_VELOCITY_MODE, target_position: 10000, velocity: 1000);
            Console.WriteLine($"Motion_configAxisMove status: {status}");

            status = dev.Motion_setEnableLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_FORWARD_ENABLE, Constant.MOTION_REVERSE_ENABLE);
            Console.WriteLine($"Motion_writeEnableLimit status: {status}");

            status = dev.Motion_setLimitPolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_LIMIT_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_writeLimitPolarity status: {status}");

            status = dev.Motion_setEncoderPolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENCODER_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_setEncoderPolarity status: {status}");
  
            status = dev.Motion_resetEncoderPosition(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_resetEncoderPosition status: {status}");

            status = dev.Motion_start(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_start status: {status}");
             
            Thread.Sleep(3000);
            status = dev.Motion_overrideAxisVelocity(port, Constant.MOTION_AXIS_1, 5000);
            Console.WriteLine($"Motion_overrideAxisVelocity status: {status}");
            
            Thread.Sleep(3000);
            status = dev.Motion_overrideAxisVelocity(port, Constant.MOTION_AXIS_1, -3000);
            Console.WriteLine($"Motion_overrideAxisVelocity status: {status}");

            Thread.Sleep(3000); 

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