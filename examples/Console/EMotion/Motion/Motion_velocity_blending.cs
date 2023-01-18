/// Motion_velocity_blending.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_velocity_blending
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
            int new_velo;

            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Motion open
            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");
            
            // Motion configure
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgAxis: {err}");

            err = dev.Motion_cfgLimit(port, Const.MOT_AXIS1, Const.MOT_TRUE, Const.MOT_TRUE, Const.MOT_ACTIVE_HIGH);
            Console.WriteLine($"cfgLimit: {err}");

            err = dev.Motion_cfgEncoder(port, Const.MOT_AXIS1, Const.MOT_ACTIVE_LOW);
            Console.WriteLine($"cfgEncoder: {err}");

            err = dev.Motion_rstEncoderPosi(port, Const.MOT_AXIS1);
            Console.WriteLine($"rstEncoderPosi: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_VELOCITY, target_posi: 10000, velo: 1000);
            Console.WriteLine($"cfgAxisMove: {err}");
 
            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_TRUE);
            Console.WriteLine($"enableServoOn: {err}");
            
            // Motion start
            err = dev.Motion_startSingleAxisMove(port, Const.MOT_AXIS1);
            Console.WriteLine($"startSingleAxisMove: {err}");

            Thread.Sleep(3000);

            // Motion override velocity
            new_velo = 5000;
            err = dev.Motion_overrideAxisVelocity(port, Const.MOT_AXIS1, new_velo);
            Console.WriteLine($"overrideAxisVelocity: {err}");

            Thread.Sleep(3000);

            // Motion override velocity
            new_velo = -3000;
            err = dev.Motion_overrideAxisVelocity(port, Const.MOT_AXIS1, new_velo);
            Console.WriteLine($"overrideAxisVelocity: {err}");

            Thread.Sleep(3000);

            // Motion stop
            err = dev.Motion_stop(port, Const.MOT_AXIS1, Const.MOT_STOP_TYPE_DECELERATION);
            Console.WriteLine($"stop: {err}");

            err = dev.Motion_enableServoOn(port, Const.MOT_AXIS1, Const.MOT_FALSE);
            Console.WriteLine($"enableServoOn: {err}");
            
            // Motion close
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