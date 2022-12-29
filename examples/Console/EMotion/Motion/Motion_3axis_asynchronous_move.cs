/// Motion_3axis_asynchronous_move.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_asynchronous_move
{
    public static void Axis1_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_readMoveStatus(port, Constant.MOTION_AXIS_1);
            Console.WriteLine($"Axis 1 move status: {move_status}");
            Thread.Sleep(5);
        }
    }

    public static void Axis2_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_readMoveStatus(port, Constant.MOTION_AXIS_2);
            Console.WriteLine($"Axis 2 move status: {move_status}");
            Thread.Sleep(5);
        }
    }

    public static void Axis3_Thread(EMotion handle, int port)
    {
        int move_status = 0;
        while (move_status == 0)
        {
            move_status = handle.Motion_readMoveStatus(port, Constant.MOTION_AXIS_3);
            Console.WriteLine($"Axis 3 move status: {move_status}");
            Thread.Sleep(5);
        }
    }
    
    static public void Main()
    { 
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        EMotion dev = new EMotion();

        // Connect to device
        dev.connect("192.168.1.110");
  
        try
        {   
            // Parameters setting
            int status;
            int port = 0;

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
              
            status = dev.Motion_open(port);
            Console.WriteLine($"Motion_open status: {status}");
            
            // For Axis 1
            status = dev.Motion_cfgAxisParam(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_DIRECTION_CW, Constant.MOTION_DIRECTION_CW, Constant.MOTION_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_cfgAxisParam status: {status}");

            status = dev.Motion_cfgLimit(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_cfgLimit status: {status}"); 

            status = dev.Motion_cfgAxisMove(port, Constant.MOTION_AXIS_1, Constant.MOTION_RELATIVE_POSITION_MODE, target_position: 10000);
            Console.WriteLine($"Motion_cfgAxisMove status: {status}");

            // For Axis 2
            status = dev.Motion_cfgAxisParam(port, Constant.MOTION_AXIS_2, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_DIRECTION_CW, Constant.MOTION_DIRECTION_CW, Constant.MOTION_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_cfgAxisParam status: {status}");

            status = dev.Motion_cfgLimit(port, Constant.MOTION_AXIS_2, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_cfgLimit status: {status}");

            status = dev.Motion_cfgAxisMove(port, Constant.MOTION_AXIS_2, Constant.MOTION_RELATIVE_POSITION_MODE, target_position: 10000);
            Console.WriteLine($"Motion_cfgAxisMove status: {status}");
             
            // For Axis 3
            status = dev.Motion_cfgAxisParam(port, Constant.MOTION_AXIS_3, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_DIRECTION_CW, Constant.MOTION_DIRECTION_CW, Constant.MOTION_POLARITY_ACTIVE_LOW);
            Console.WriteLine($"Motion_cfgAxisParam status: {status}");

            status = dev.Motion_cfgLimit(port, Constant.MOTION_AXIS_3, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_ENABLE_TRUE, Constant.MOTION_POLARITY_ACTIVE_HIGH);
            Console.WriteLine($"Motion_cfgLimit status: {status}");

            status = dev.Motion_cfgAxisMove(port, Constant.MOTION_AXIS_3, Constant.MOTION_RELATIVE_POSITION_MODE, target_position: 10000);
            Console.WriteLine($"Motion_cfgAxisMove status: {status}");

            List<int> axis_list = new List<int> {0, 1, 2};
            status = dev.Motion_startMultiAxisMove(port, axis_list);
            Console.WriteLine($"Motion_startMultiAxisMove status: {status}");

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
             
            status = dev.Motion_stop(port, Constant.MOTION_AXIS_1, Constant.MOTION_STOP_TYPE_DECELERATION);
            Console.WriteLine($"Motion_stop status: {status}");

            status = dev.Motion_stop(port, Constant.MOTION_AXIS_2, Constant.MOTION_STOP_TYPE_DECELERATION);
            Console.WriteLine($"Motion_stop status: {status}");

            status = dev.Motion_stop(port, Constant.MOTION_AXIS_3, Constant.MOTION_STOP_TYPE_DECELERATION);
            Console.WriteLine($"Motion_stop status: {status}");

            status = dev.Motion_close(port);
            Console.WriteLine($"Motion_close status: {status}"); 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}