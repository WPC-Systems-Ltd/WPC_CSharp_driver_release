/// Motion_velocity_blending_accerleration.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_velocity_blending_accerleration
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
             
            err = dev.Motion_cfgAxisParam(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxisParam: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_VELOCITY, velocity: 3000);
            Console.WriteLine($"cfgAxisMove: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit: {err}");

            err = dev.Motion_cfgEncoder(port, Const.MOT_AXIS1, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgEncoder: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1);
            Console.WriteLine($"startSingleAxisMove: {err}");
             
            Thread.Sleep(5000);
             
            err = dev.Motion_overrideAxisVelocity(port, Const.MOT_AXIS1, -3000);
            Console.WriteLine($"overrideAxisVelocity: {err}");

            err = dev.Motion_overrideAxisAccel(port, Const.MOT_AXIS1, 100, 100);
            Console.WriteLine($"overrideAxisAccel: {err}");;
             
            Thread.Sleep(5000);

            err = dev.Motion_overrideAxisVelocity(port, Const.MOT_AXIS1, 6000);
            Console.WriteLine($"overrideAxisVelocity: {err}");

            err = dev.Motion_overrideAxisAccel(port, Const.MOT_AXIS1, 100000, 100000);
            Console.WriteLine($"overrideAxisAccel: {err}");
             
            Thread.Sleep(5000);

            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");

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