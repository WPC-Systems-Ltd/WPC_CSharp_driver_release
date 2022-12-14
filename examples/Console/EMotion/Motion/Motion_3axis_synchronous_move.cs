/// Motion_3axis_synchronous_move.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
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
            Console.WriteLine($"Axis 1 move status: {move_status}");
            Thread.Sleep(5);
        }
    }

    public static void Axis2_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS2);
            Console.WriteLine($"Axis 2 move status: {move_status}");
            Thread.Sleep(5);
        }
    }

    public static void Axis3_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_getMoveStatus(port, Const.MOT_AXIS3);
            Console.WriteLine($"Axis 3 move status: {move_status}");
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
              
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");
            
            // For Axis 1
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis1: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit axis1: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, target_position: 10000);
            Console.WriteLine($"cfgAxisMove axis1: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");


            // For Axis 2
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS2, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis2: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS2, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
             Console.WriteLine($"cfgLimit axis2: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS2, Const.MOT_RELATIVE_POSITION, target_position: 10000);
            Console.WriteLine($"cfgAxisMove axis2: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS2, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");
            
            // For Axis 3
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS3, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis axis3: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS3, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit axis3: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS3, Const.MOT_RELATIVE_POSITION, target_position: 10000);
            Console.WriteLine($"cfgAxisMove axis3: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS3, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");


            List<int> axis_list = new List<int> {0, 1, 2};
            err = dev.Motion_startMultiAxisMove(port, axis_list);
            Console.WriteLine($"startMultiAxisMove: {err}");

            Thread T1 = new Thread(() => Axis1_Thread(dev, port));
            Thread T2 = new Thread(() => Axis2_Thread(dev, port));
            Thread T3 = new Thread(() => Axis3_Thread(dev, port));
 
            T1.Start();
            T2.Start();
            T3.Start();
             
            T1.Join();
            Console.WriteLine("Axis1_Thread returned.");

            T2.Join();
            Console.WriteLine("Axis2_Thread returned.");

            T3.Join();
            Console.WriteLine("Axis3_Thread returned.");
             
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis1: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");

            err = dev.Motion_stop(port, Const.MOT_AXIS2, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis2: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS2, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");

            err = dev.Motion_stop(port, Const.MOT_AXIS3, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop axis3: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS3, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");

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