/// Motion_1axis_move_with_breakpoint.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
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

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
             
            //// Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\Emotion.ini");

            err = dev.Motion_openCfgFile("Emotion.ini");
            Console.WriteLine($"openCfgFile: {err}");

            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"loadCfgFile: {err}");
 
            err = dev.Motion_cfgBreakPoint(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, Const.MOT_ACTIVE_HIGH, 100, 100, 100, 100);
            Console.WriteLine($"cfgBreakPoint: {err}");

            err = dev.Motion_enableBreakPoint(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"setBreakPointEnable: {err}");
             
            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_position: 10000);
            Console.WriteLine($"cfgAxisMove: {err}");
             
            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1);
            Console.WriteLine($"startSingleAxisMove: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                Console.WriteLine($"Motion_getMoveStatus: {move_status}");
            }

            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");

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