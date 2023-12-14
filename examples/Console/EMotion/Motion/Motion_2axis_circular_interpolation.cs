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
            int center_point_x = 2000;
            int center_point_y = 2000;
            int finish_point_x = 0;
            int finish_point_y = 0;
            int timeout = 3000; // ms
            int axis_0 = Const.MOT_AXIS0;
            int axis_1 = Const.MOT_AXIS1;

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open in port {port}: {err}");

            // Motion open configuration file
            err = dev.Motion_openCfgFile(file_name:@"C:\Users\user\Desktop\3AxisStage_2P.ini");
            Console.WriteLine($"Motion_openCfgFile: {err}");

            // Motion load configuration file
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"Motion_loadCfgFile: {err}");

            // Motion configure
            err = dev.Motion_cfgCircularInterpo(port, axis_0, axis_1, center_point_x, center_point_y, finish_point_x, finish_point_y, Const.MOT_DIR_CW, speed:1000, accel:10000, decel:10000, timeout:timeout);
            Console.WriteLine($"Motion_cfgCircularInterpo in axis{axis_0} and {axis_1}: {err}");

            for (int i=0; i<4; i++)
            {
                err = dev.Motion_enableServoOn(port, i, timeout);
                Console.WriteLine($"Motion_enableServoOn in axis{i}: {err}");
            }

            // Motion start
            err = dev.Motion_startCircularInterpo(port, timeout);
            Console.WriteLine($"Motion_startCircularInterpo in port {port}: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                int axis1_move_status = dev.Motion_getMoveStatus(port, axis_0, timeout);
                int axis2_move_status = dev.Motion_getMoveStatus(port, axis_1, timeout);
                move_status = axis1_move_status & axis2_move_status;
                if (move_status == 0) { Console.WriteLine($"Moving......"); }
                else { Console.WriteLine($"Move completed"); }
            }

            for (int i=0; i<4; i++)
            {
                // Motion stop
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION, timeout);
                Console.WriteLine($"Motion_stop in axis{i}: {err}");

                err = dev.Motion_enableServoOff(port, i, timeout);
                Console.WriteLine($"Motion_enableServoOff in axis{i}: {err}");
            }

            // Motion close
            err = dev.Motion_close(port, timeout);
            Console.WriteLine($"Motion_close in port {port}: {err}");
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