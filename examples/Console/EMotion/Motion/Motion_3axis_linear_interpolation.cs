/// Motion_3axis_linear_interpolation.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_3axis_linear_interpolation
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
            int dest_posi1 = 5000;
            int dest_posi2 = 5000;
            int dest_posi3 = 5000;

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
              
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            //// Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\Emotion.ini");

            err = dev.Motion_openCfgFile("3AxisStage_2P.ini");
            Console.WriteLine($"openCfgFile: {err}");
            
            err = dev.Motion_loadCfgFile();
            Console.WriteLine($"loadCfgFile: {err}");

            err = dev.Motion_cfg3AxisLinearInterpo(port, Const.MOT_AXIS1, dest_posi1, Const.MOT_AXIS2, dest_posi2, Const.MOT_AXIS3, dest_posi3, speed:2000);
            Console.WriteLine($"cfg3AxisLinearInterpo: {err}");

            err = dev.Motion_startLinearInterpo(port);
            Console.WriteLine($"startLinearInterpo: {err}");
 
            int move_status = 0;
            while (move_status == 0)
            { 
                int axis1_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS1);
                int axis2_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS2);
                int axis3_move_status = dev.Motion_getMoveStatus(port, Const.MOT_AXIS3);
                move_status = axis1_move_status & axis2_move_status & axis3_move_status;    
                if (move_status == 0) { Console.WriteLine($"Moving......"); }
                else { Console.WriteLine($"Move completed"); }
            }
            for (int i = 0; i < 3; i++)
            {
                err = dev.Motion_stop(port, i, Const.MOT_STOP_TYPE_DECELERATION);
                Console.WriteLine($"stop: {err}"); 
            }
            
            err = dev.Motion_releaseInterpoAxis(port);
            Console.WriteLine($"releaseInterpoAxis: {err}");  

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