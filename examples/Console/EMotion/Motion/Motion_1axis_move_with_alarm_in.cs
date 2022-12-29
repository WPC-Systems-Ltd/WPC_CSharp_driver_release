/// Motion_1axis_move_with_alarm_in.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_1axis_move_with_alarm_in
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

            status = dev.Motion_cfgAlarm(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENABLE_FALSE, Constant.MOTION_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_cfgAlarm status: {status}");

            status = dev.Motion_cfgAxisMove(port, Constant.MOTION_AXIS_1, Constant.MOTION_RELATIVE_POSITION_MODE, target_position: 10000);
            Console.WriteLine($"Motion_cfgAxisMove status: {status}");

            status = dev.Motion_rstEncoderPosi(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_rstEncoderPosi status: {status}");

            status = dev.Motion_startSingleAxisMove(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Motion_startSingleAxisMove status: {status}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_readMoveStatus(port, Constant.MOTION_AXIS_1);
                Console.WriteLine($"Motion_readMoveStatus : {move_status}");
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