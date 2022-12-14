/// Motion_3axis_helical_interpolation.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_helical_interpolation
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
            int center_x = 0;
            int center_y = 0;
            int finish_x = 100;
            int finish_y = 100;
            int pitch_axis3 = 0;
            int pitch_axis4 = 0;
            int rotation_num = 0;
            int speed = 0;
            int cal_timeout = 1000;

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
              
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            //// Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\Emotion.ini");

            err = dev.Motion_openCfgFile("3AxisStage_2P.ini");
            Console.WriteLine($"openCfgFile: {err}");

            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"loadCfgFile: {err}");

            err = dev.Motion_cfgHelicalInterpo(port, center_x, center_y, finish_x, finish_y, Const.MOT_FALSE, pitch_axis3, Const.MOT_FALSE, pitch_axis4,
            rotation_num, speed, Const.MOT_DIR_CW, cal_timeout);

            Console.WriteLine($"cfgHelicalInterpo: {err}");
             
            err = dev.Motion_startHelicalInterpo(port);
            Console.WriteLine($"startHelicalInterpo: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                Console.WriteLine($"move_status: {move_status}"); 
            } 
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_releaseInterpoAxis(port);
            Console.WriteLine($"releaseInterpoAxis: {err}");  

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