/// Motion_1axis_move_with_capture.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_1axis_move_with_capture
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
            err = dev.Motion_cfgCapture(port, Const.MOT_AXIS1, Const.MOT_CAPTURE_RISING_EDGE, Const.MOT_CAPTURE_LOGICAL_POSITION, timeout);
            Console.WriteLine($"cfgCapture: {err}");

            err = dev.Motion_enableCapture(port, Const.MOT_AXIS1, Const.MOT_TRUE, timeout);
            Console.WriteLine($"setCaptureEnable: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_posi: 10000, timeout: timeout);
            Console.WriteLine($"cfgAxisMove: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1, timeout);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE, timeout);
            Console.WriteLine($"enableServoOn: {err}");

            // Motion start
            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1, timeout);
            Console.WriteLine($"startSingleAxisMove: {err}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1, timeout);
                Console.WriteLine($"getMoveStatus : {move_status}");

                int capture_points = dev.Motion_readCapturePoint(port, Const.MOT_AXIS1, timeout);
                Console.WriteLine($"readCapturePoint: {move_status}");
            }

            // Motion stop
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION, timeout);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE, timeout);
            Console.WriteLine($"enableServoOn: {err}");

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