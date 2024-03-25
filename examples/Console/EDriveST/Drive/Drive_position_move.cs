/// Drive_position_move.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EDriveST_Drive_position_move
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EDriveST dev = new EDriveST();

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
            int position = 50000;
            int speed = 50000;
            int acceleration = 10000;
            int deceleration = 10000;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open: {err}");

            // Motion configure
            err = dev.Motion_cfgLimit(port, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH, timeout);
            Console.WriteLine($"Motion_cfgLimit: {err}");

            // Motion reset
            err = dev.Motion_rstEncoderPosi(port, timeout);
            Console.WriteLine($"Motion_rstEncoderPosi: {err}");

            // Motion servo on
            err = dev.Motion_enableServoOn(port, timeout);
            Console.WriteLine($"Motion_enableServoOn {err}");

            // Motion start
            err = dev.Motion_startPositionMove(port, position, speed, acceleration, deceleration, Const.MOT_RELATIVE_POSITION, timeout);
            Console.WriteLine($"Motion_startPositionMove {err}");

            int status = 1;
            while (status != 0) {
                status = dev.Motion_getProcessState(port, timeout);
                if (status == 0){
                    Console.WriteLine($"Motion_getProcessState {status}");
                }
            }

            // Motion stop
            err = dev.Motion_stopProcess(port, timeout);
            Console.WriteLine($"Motion_stopProcess: {err}");

            // Motion Servo off
            err = dev.Motion_enableServoOff(port, timeout);
            Console.WriteLine($"Motion_enableServoOff: {err}");

            // Motion close
            err = dev.Motion_close(port, timeout);
            Console.WriteLine($"Motion_close: {err}");
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