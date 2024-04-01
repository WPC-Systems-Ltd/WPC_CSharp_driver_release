/// Motion_3axis_helical_interpolation.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
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
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Release device handle
            dev.close();
            return;
        }

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
            int timeout = 3000; // ms
            int axis = Const.MOT_AXIS0;

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open in port {port}, status: {err}");

            // Motion open configuration file
            err = dev.Motion_openCfgFile(file_name:@"C:\Users\user\Desktop\3AxisStage_2P.ini");
            Console.WriteLine($"Motion_openCfgFile, status: {err}");

            // Motion load configuration file
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"Motion_loadCfgFile, status: {err}");

            // Motion configure
            err = dev.Motion_cfgHelicalInterpo(port, center_x, center_y, finish_x, finish_y, Const.MOT_FALSE, pitch_axis3, Const.MOT_FALSE, pitch_axis4,
            rotation_num, speed, Const.MOT_DIR_CW, cal_timeout, timeout);
            Console.WriteLine($"Motion_cfgHelicalInterpo in port {port}, status: {err}");

            // Motion start
            err = dev.Motion_startHelicalInterpo(port, timeout);
            Console.WriteLine($"Motion_startHelicalInterpo in port {port}, status: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, axis, timeout);
                Console.WriteLine($"Motion_getMoveStatus in axis{axis}: {move_status}");
            }

            // Motion stop
            err = dev.Motion_stop(port, axis, Const.MOT_STOP_TYPE_DECELERATION, timeout);
            Console.WriteLine($"Motion_stop in axis{axis}, status: {err}");

            // Motion close
            err = dev.Motion_close(port, timeout);
            Console.WriteLine($"Motion_close in port {port}, status: {err}");
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