/// Motion_3axis_synchronous_move.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_synchronous_move
{
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

    public static void Axis3_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS3);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 3..."); }

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

            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Define Axis1 ~ Axis3 thread
            Thread thread_1 = new Thread(() => Axis1_Thread(dev, port));
            Thread thread_2 = new Thread(() => Axis2_Thread(dev, port));
            Thread thread_3 = new Thread(() => Axis3_Thread(dev, port));

            // Thread start
            thread_1.Start();
            thread_2.Start();
            thread_3.Start();

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open in port{port}: {err}");

            // Motion configure for axis1
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis {Const.MOT_AXIS1} in port{port}: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis {Const.MOT_AXIS1} in port{port}: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1, timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis {Const.MOT_AXIS1} in port{port}: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_posi: 5000, timeout: timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis {Const.MOT_AXIS1} in port{port}: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis {Const.MOT_AXIS1} in port{port}: {err}");

            // Motion configure for axis2
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS2, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis {Const.MOT_AXIS2} in port{port}: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS2, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis {Const.MOT_AXIS2} in port{port}: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS2, timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis {Const.MOT_AXIS2} in port{port}: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS2, Const.MOT_RELATIVE_POSITION, target_posi: 5000, timeout: timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis {Const.MOT_AXIS2} in port{port}: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS2, Const.MOT_TRUE, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis {Const.MOT_AXIS2} in port{port}: {err}");

            // Motion configure for axis3
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS3, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in axis {Const.MOT_AXIS3} in port{port}: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS3, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgLimit in axis {Const.MOT_AXIS3} in port{port}: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS3, timeout);
            Console.WriteLine($"Motion_rstEncoderPosi in axis {Const.MOT_AXIS3} in port{port}: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS3, Const.MOT_RELATIVE_POSITION, target_posi: 5000, timeout: timeout);
            Console.WriteLine($"Motion_cfgAxisMove in axis {Const.MOT_AXIS3} in port{port}: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS3, Const.MOT_TRUE, timeout);
            Console.WriteLine($"Motion_enableServoOn in axis {Const.MOT_AXIS3} in port{port}: {err}");

            // Motion start
            List<int> axis_list = new List<int> { Const.MOT_AXIS1, Const.MOT_AXIS2, Const.MOT_AXIS3 };
            err = dev.Motion_startMultiAxisMove(port, axis_list, timeout);
            Console.WriteLine($"Motion_startMultiAxisMove in port{port}: {err}");

            // Wait for thread completion
            thread_1.Join();
            Console.WriteLine("Axis1_Thread returned.");

            thread_2.Join();
            Console.WriteLine("Axis2_Thread returned.");

            thread_3.Join();
            Console.WriteLine("Axis3_Thread returned.");

            for (int i = 0; i < 3; i++)
            {
                // Motion stop
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION, timeout);
                Console.WriteLine($"Motion_stop in axis {i} in port{port}: {err}");

                err = dev.Motion_enableServoOn(port, i, Const.MOT_FALSE, timeout);
                Console.WriteLine($"Motion_enableServoOn in axis {i} in port{port}: {err}");
            }

            // Motion close
            err = dev.Motion_close(port, timeout);
            Console.WriteLine($"Motion_close in port{port}: {err}");
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