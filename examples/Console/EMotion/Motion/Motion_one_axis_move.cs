/// <summary>
/// Motion_one_axis_move.cs
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

class EMotion_one_axis_move
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
 
        // Perform DAQ basic information 
        try
        {   // Parameters setting
            int status;
            int port = 0;

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
             
            status = dev.Motion_open(port);
            Console.WriteLine($"Motion_open status: {status}");

            status = dev.Motion_setEnableLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_FORWARD_DISABLE, Constant.MOTION_REVERSE_DISABLE);
            Console.WriteLine($"Motion_writeEnableLimit status: {status}");

            status = dev.Motion_setHomeLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_HOME_DISABLE);
            Console.WriteLine($"Motion_writeHomeLimit status: {status}");

            status = dev.Motion_setLimitPolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_LIMIT_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_writeLimitPolarity status: {status}");

            status = dev.Motion_setHomePolarity(port, Constant.MOTION_AXIS_1, Constant.MOTION_HOME_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_writeHomePolarity status: {status}");
             
            status = dev.Motion_configAxisModeAndDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_AXIS_DIRECTION_CW);
            Console.WriteLine($"Motion_configAxisModeAndDirection status: {status}");

            status = dev.Motion_configEncoderDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENCODER_DIRECTION_CW);
            Console.WriteLine($"Motion_configEncoderDirection status: {status}"); 

            status = dev.Motion_configTargetPosition(port, Constant.MOTION_AXIS_1, 1000);
            Console.WriteLine($"Motion_configTargetPosition status: {status}");

            status = dev.Motion_configVelocity(port, Constant.MOTION_AXIS_1, 10000);
            Console.WriteLine($"Motion_configVelocity status: {status}");
             
            status = dev.Motion_configAxisAccelDeccel(port, Constant.MOTION_AXIS_1, 100000, 100000);
            Console.WriteLine($"Motion_configAxisAccelDeccel status: {status}");
             
            status = dev.Motion_configInstantLimitStopMode(port, Constant.MOTION_AXIS_1, Constant.MOTION_STOP_DECELERATING);
            Console.WriteLine($"Motion_configInstantLimitStopMode status: {status}");
             
            status = dev.Motion_configOperationMode(port, Constant.MOTION_AXIS_1, Constant.MOTION_RELATIVE_POSITION_MODE);
            Console.WriteLine($"Motion_configOperationMode status: {status}");

            status = dev.Motion_resetEncoderPosition(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_resetEncoderPosition status: {status}");

            status = dev.Motion_start(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_start status: {status}");

            int move_status;
            move_status = dev.Motion_readMoveStatus(port, Constant.MOTION_AXIS_1); 
           
            while (move_status == 0) 
            {
                move_status = dev.Motion_readMoveStatus(port, Constant.MOTION_AXIS_1);
                Console.WriteLine($"move_status status: {move_status}");
            }

            status = dev.Motion_stop(port, Constant.MOTION_STOP_TYPE_DECELERATION, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_stop status: {status}"); 
 
            status = dev.Motion_close(port);
            Console.WriteLine($"Motion_open status: {status}");
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