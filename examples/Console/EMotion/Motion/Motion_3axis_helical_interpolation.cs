/// Motion_3axis_helical_interpolation.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_helical_interpolation
{
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

            //// Or specify a specific name in a specific dir
            //status = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\Emotion.ini");

            status = dev.Motion_openCfgFile("Emotion.ini");
            Console.WriteLine($"Motion_openCfgFile status: {status}");

            status = dev.Motion_loadCfgFile();
            Console.WriteLine($"Motion_loadCfgFile status: {status}");

            status = dev.Motion_cfgHelicalInterpo(port, 0, 0, 100, 100, Constant.MOTION_ENABLE_FALSE, 0, Constant.MOTION_ENABLE_FALSE, 0, 0, 0, Constant.MOTION_DIRECTION_CW, 1000);
            Console.WriteLine($"Motion_cfgHelicalInterpo status: {status}");
             
            status = dev.Motion_startHelicalInterpo(port);
            Console.WriteLine($"Motion_startHelicalInterpo status: {status}");

            int move_status = 0;
            while (move_status == 0)
            {
                move_status = dev.Motion_readMoveStatus(port, Constant.MOTION_AXIS_1);
                Console.WriteLine($"move_status status: {move_status}"); 
            }

            status = dev.Motion_stop(port, Constant.MOTION_AXIS_1, Constant.MOTION_STOP_TYPE_DECELERATION);
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