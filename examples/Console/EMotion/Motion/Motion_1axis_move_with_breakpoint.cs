/// Motion_1axis_move_with_breakpoint.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_1axis_move_with_breakpoint
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
            int start_position = 100;
            ushort pulse_width = 100;
            ushort pulse_period = 100;
            ushort pulse_number = 100;
 
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            // Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\3AxisStage_2P.ini"); 

            // Motion open configuration file 
            err = dev.Motion_openCfgFile("3AxisStage_2P.ini");
            Console.WriteLine($"openCfgFile: {err}");

            // Motion load configuration file
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"loadCfgFile: {err}");

            // Motion configure
            err = dev.Motion_cfgBreakPoint(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, Const.MOT_ACTIVE_HIGH, start_position, pulse_width, pulse_period, pulse_number);
            Console.WriteLine($"cfgBreakPoint: {err}");

            err = dev.Motion_enableBreakPoint(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"setBreakPointEnable: {err}");
             
            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_posi: 10000);
            Console.WriteLine($"cfgAxisMove: {err}");
             
            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");
            
            // Motion start
            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1);
            Console.WriteLine($"startSingleAxisMove: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                Console.WriteLine($"Motion_getMoveStatus: {move_status}");
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