/// Motion_3axis_synchronous_move.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_synchronous_move
{
    public static void Axis0_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS0);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 0..."); }

            // Wait
            Thread.Sleep(5); // delay [ms]
        }
    }

    public static void Axis1_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS1);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 1..."); }

            // Wait
            Thread.Sleep(5); // delay [ms]
        }
    }

    public static void Axis2_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS2);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 2..."); }

            // Wait
            Thread.Sleep(5); // delay [ms]
        }
    }

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
            int axis_0 = Const.MOT_AXIS0;
            int axis_1 = Const.MOT_AXIS1;
            int axis_2 = Const.MOT_AXIS2;

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Define Axis0 ~ Axis2 thread
            Thread thread_0 = new Thread(() => Axis0_Thread(dev, port));
            Thread thread_1 = new Thread(() => Axis1_Thread(dev, port));
            Thread thread_2 = new Thread(() => Axis2_Thread(dev, port));

            // Thread start
            thread_0.Start();
            thread_1.Start();
            thread_2.Start();

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open in port {port}, status: {err}");

            /*
            // Motion open configuration file
            err = dev.Motion_openCfgFile(file_name:@"C:\Users\user\Desktop\3AxisStage_2P.ini");
            Console.WriteLine($"Motion_openCfgFile, status: {err}");

            // Motion load configuration file
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"Motion_loadCfgFile, status: {err}");
            */

            // Motion configure for axis0
            err = dev.Motion_cfgAxis(port, axis_0, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis{axis_0}, status: {err}");

            err = dev.Motion_cfgLimit(port, axis_0, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis{axis_0}, status: {err}");

            err = dev.Motion_rstEncoderPosi(port, axis_0, encoder_posi:0, timeout:timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis{axis_0}, status: {err}");

            err = dev.Motion_cfgAxisMove(port, axis_0, Const.MOT_RELATIVE_POSITION, target_posi:5000, velo:10000, accel:100000, decel:100000, timeout:timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis{axis_0}, status: {err}");

            err = dev.Motion_enableServoOn(port, axis_0, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis{axis_0}, status: {err}");

            // Motion configure for axis1
            err = dev.Motion_cfgAxis(port, axis_1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis{axis_1}, status: {err}");

            err = dev.Motion_cfgLimit(port, axis_1, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis{axis_1}, status: {err}");

            err = dev.Motion_rstEncoderPosi(port, axis_1, encoder_posi:0, timeout:timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis{axis_1}, status: {err}");

            err = dev.Motion_cfgAxisMove(port, axis_1, Const.MOT_RELATIVE_POSITION, target_posi:5000, velo:10000, accel:100000, decel:100000, timeout:timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis{axis_1}, status: {err}");

            err = dev.Motion_enableServoOn(port, axis_1, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis{axis_1}, status: {err}");

            // Motion configure for axis2
            err = dev.Motion_cfgAxis(port, axis_2, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis{axis_2}, status: {err}");

            err = dev.Motion_cfgLimit(port, axis_2, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis{axis_2}, status: {err}");

            err = dev.Motion_rstEncoderPosi(port, axis_2, encoder_posi:0, timeout:timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis{axis_2}, status: {err}");

            err = dev.Motion_cfgAxisMove(port, axis_2, Const.MOT_RELATIVE_POSITION, target_posi:5000, velo:10000, accel:100000, decel:100000, timeout:timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis{axis_2}, status: {err}");

            err = dev.Motion_enableServoOn(port, axis_2, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis{axis_2}, status: {err}");

            // Motion start
            List<int> axis_list = new List<int> { axis_0, axis_1, axis_2 };
            err = dev.Motion_startMultiAxisMove(port, axis_list, timeout);
            Console.WriteLine($"Motion_startMultiAxisMove in port {port}, status: {err}");

            // Wait for thread completion
            thread_0.Join();
            Console.WriteLine("Axis0_Thread returned.");

            thread_1.Join();
            Console.WriteLine("Axis1_Thread returned.");

            thread_2.Join();
            Console.WriteLine("Axis2_Thread returned.");

            for (int i=0; i<3; i++)
            {
                // Motion stop
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION, timeout);
                Console.WriteLine($"Motion_stop in axis{i}, status: {err}");

                err = dev.Motion_enableServoOff(port, i, timeout);
                Console.WriteLine($"Motion_enableServoOff in axis{i}, status: {err}");
            }

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