/// Motion_2axis_circular_interpolation.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_2axis_circular_interpolation
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

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
              
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            //// Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\Emotion.ini");

            err = dev.Motion_openCfgFile("Emotion.ini");
            Console.WriteLine($"openCfgFile: {err}");

            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"loadCfgFile: {err}");
             
            err = dev.Motion_cfgCircularInterpo(port, Const.MOT_AXIS1, Const.MOT_AXIS2, 2000, 2000, 0, 0, Const.MOT_DIR_CW, circular_interpo_vector_speed:1000);
            Console.WriteLine($"cfgCircularInterpo: {err}");
             
            err = dev.Motion_startCircularInterpo(port);
            Console.WriteLine($"startCircularInterpo: {err}");
             
            int move_status = 0;
            while (move_status == 0)
            { 
                int axis1_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                int axis2_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS2);
                move_status = axis1_move_status & axis2_move_status;    
                if (move_status == 0) { Console.WriteLine($"Moving......"); }
                else { Console.WriteLine($"Move completed"); }
            } 
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