/// Motion_1axis_move.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_1axis_move
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
             
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");
 
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis: {err}");
  
            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit: {err}");

            err = dev.Motion_cfgHome(port, Const.MOT_AXIS1, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgHome: {err}");
             
            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_position: 5000);
            Console.WriteLine($"cfgAxisMove: {err}");
  
            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");

            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1);
            Console.WriteLine($"startSingleAxisMove: {err}");

            int move_status = 0; 
            while (move_status == 0) 
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                if (move_status == 0){Console.WriteLine($"Moving...");} 
            } 
            
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");
            
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