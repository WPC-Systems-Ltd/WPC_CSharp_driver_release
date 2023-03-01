/// Motion_3axis_helical_interpolation.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
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
            int timeout = 3000;

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout);
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
            err = dev.Motion_cfgHelicalInterpo(port, center_x, center_y, finish_x, finish_y, Const.MOT_FALSE, pitch_axis3, Const.MOT_FALSE, pitch_axis4,
            rotation_num, speed, Const.MOT_DIR_CW, cal_timeout, timeout);
            Console.WriteLine($"cfgHelicalInterpo: {err}");

            // Motion start
            err = dev.Motion_startHelicalInterpo(port, timeout);
            Console.WriteLine($"startHelicalInterpo: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1, timeout);
                Console.WriteLine($"move_status: {move_status}");
            }

            // Motion stop
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION, timeout);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_releaseInterpoAxis(port, timeout);
            Console.WriteLine($"releaseInterpoAxis: {err}");

            // Motion close
            err = dev.Motion_close(port, timeout);
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