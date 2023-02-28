/// Motion_2axis_circular_interpolation.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_2axis_circular_interpolation
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
            int center_point_x = 2000;
            int center_point_y = 2000;
            int finish_point_x = 0;
            int finish_point_y = 0;
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
            err = dev.Motion_cfgCircularInterpo(port, Const.MOT_AXIS1, Const.MOT_AXIS2, center_point_x, center_point_y, finish_point_x, finish_point_y, Const.MOT_DIR_CW, speed:1000, timeout: timeout);
            Console.WriteLine($"cfgCircularInterpo: {err}");

            for (int i = 0; i < 4; i++)
            {
                err = dev.Motion_enableServoOn(port, i, Const.MOT_TRUE, timeout);
                Console.WriteLine($"enableServoOn: {err}");
            }

            // Motion start
            err = dev.Motion_startCircularInterpo(port, timeout);
            Console.WriteLine($"startCircularInterpo: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                int axis1_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1, timeout);
                int axis2_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS2, timeout);
                move_status = axis1_move_status & axis2_move_status;
                if (move_status == 0) { Console.WriteLine($"Moving......"); }
                else { Console.WriteLine($"Move completed"); }
            }

            for (int i = 0; i < 4; i++)
            {
                // Motion stop
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION, timeout);
                Console.WriteLine($"stop: {err}");

                err = dev.Motion_enableServoOn(port, i, Const.MOT_FALSE, timeout);
                Console.WriteLine($"enableServoOn: {err}");
            }
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