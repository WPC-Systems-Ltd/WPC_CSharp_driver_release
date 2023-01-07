/// Motion_3axis_synchronous_move.cs
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
            Thread.Sleep(5);
        }
    }

    public static void Axis2_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS2);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 2..."); }
            Thread.Sleep(5);
        }
    }

    public static void Axis3_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS3);
            if (move_status != 0) { Console.WriteLine($"Move completed axis 3..."); }
            Thread.Sleep(5);
        }
    }
    
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

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            Thread thread_1 = new Thread(() => Axis1_Thread(dev, port));
            Thread thread_2 = new Thread(() => Axis2_Thread(dev, port));
            Thread thread_3 = new Thread(() => Axis3_Thread(dev, port));

            thread_1.Start();
            thread_2.Start();
            thread_3.Start();

            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            // For Axis 1
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis1: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgLimit axis1: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi axis1: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_position: 5000);
            Console.WriteLine($"cfgAxisMove axis1: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn axis1: {err}");


            // For Axis 2
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS2, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis2: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS2, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgLimit axis2: {err}");
           
            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS2);
            Console.WriteLine($"rstEncoderPosi axis2: {err}");
            
            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS2, Const.MOT_RELATIVE_POSITION, target_position: 5000);
            Console.WriteLine($"cfgAxisMove axis2: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS2, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn axis2: {err}");

            // For Axis 3
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS3, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis3: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS3, Const.MOT_FALSE, Const.MOT_FALSE, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgLimit axis3: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS3);
            Console.WriteLine($"rstEncoderPosi axis3: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS3, Const.MOT_RELATIVE_POSITION, target_position: 5000);
            Console.WriteLine($"cfgAxisMove axis3: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS3, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn axis3: {err}");

            List<int> axis_list = new List<int> { Const.MOT_AXIS1, Const.MOT_AXIS2, Const.MOT_AXIS3 };
            err = dev.Motion_startMultiAxisMove(port, axis_list);
            Console.WriteLine($"startMultiAxisMove: {err}");
 
            thread_1.Join();
            Console.WriteLine("Axis1_Thread returned.");

            thread_2.Join();
            Console.WriteLine("Axis2_Thread returned.");

            thread_3.Join();
            Console.WriteLine("Axis3_Thread returned.");

            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis1: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn axis1: {err}");

            err = dev.Motion_stop(port, Const.MOT_AXIS2, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis2: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS2, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn axis2: {err}");

            err = dev.Motion_stop(port, Const.MOT_AXIS3, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis3: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS3, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn axis3: {err}");

            err = dev.Motion_close(port);
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