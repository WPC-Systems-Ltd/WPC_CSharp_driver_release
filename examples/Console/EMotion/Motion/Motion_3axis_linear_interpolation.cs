/// Motion_3axis_linear_interpolation.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_linear_interpolation
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
            int dest_posi0 = -2000;
            int dest_posi1 = -2000;
            int dest_posi2 = 3000;
            int timeout = 3000; // ms

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
            err = dev.Motion_cfg3AxisLinearInterpo(port, Const.MOT_AXIS0, dest_posi0, Const.MOT_AXIS1, dest_posi1, Const.MOT_AXIS2, dest_posi2, speed:2000, accel:100000, decel:10000 ,timeout:timeout);
            Console.WriteLine($"Motion_cfg3AxisLinearInterpo in port {port}: {err}");

            // Motion configure
            err = dev.Motion_startLinearInterpo(port, timeout);
            Console.WriteLine($"Motion_startLinearInterpo in port {port}: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                int axis0_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS0, timeout);
                int axis1_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1, timeout);
                int axis2_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS2, timeout);
                move_status = axis0_move_status & axis1_move_status & axis2_move_status;
                if (move_status == 0) { Console.WriteLine($"Moving......"); }
                else { Console.WriteLine($"Move completed"); }
            }

            // Motion stop
            for (int i=0; i<3; i++)
            {
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION, timeout);
                Console.WriteLine($"Motion_stop in axis{i}: {err}");
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