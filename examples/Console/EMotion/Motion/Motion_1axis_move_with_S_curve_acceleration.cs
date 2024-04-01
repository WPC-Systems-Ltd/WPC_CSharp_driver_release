/// Motion_1axis_move_with_S_curve_acceleration.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_1axis_move_with_S_curve_acceleration
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
            err = dev.Motion_cfgLimit(port, axis, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis{axis}, status: {err}");

            err = dev.Motion_cfgAxisMove(port, axis, Const.MOT_RELATIVE_POSITION, target_posi:5000, velo:10000, accel:100000, decel:100000, timeout:timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis{axis}, status: {err}");

            err = dev.Motion_cfgJerkAndAccelMode(port, axis, 1, Const.MOT_SCURVE, timeout);
            Console.WriteLine($"Motion_cfgJerkAndAccelMode in axis{axis}, status: {err}");

            err = dev.Motion_rstEncoderPosi(port, axis, encoder_posi:0, timeout:timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis{axis}, status: {err}");

            // Servo on
            err = dev.Motion_enableServoOn(port, axis, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis{axis}, status: {err}");

            // Motion start
            err = dev.Motion_startSingleAxisMove(port, axis, timeout);
            Console.WriteLine($"Motion_startSingleAxisMove in axis{axis}, status: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, axis, timeout);
                Console.WriteLine($"Motion_getMoveStatus in axis{axis}: {move_status}");
            }

            // Motion stop
            err = dev.Motion_stop(port, axis, Const.MOT_STOP_INSTANT, timeout);
            Console.WriteLine($"Motion_stop in axis{axis}, status: {err}");

            // Servo off
            err = dev.Motion_enableServoOff(port, axis, timeout);
            Console.WriteLine($"Motion_enableServoOff in axis{axis}, status: {err}");

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