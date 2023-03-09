/// Motion_save_configuration_file.cs with synchronous mode.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_save_configuration_file
{
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

            // Motion open
            err = dev.Motion_open(port, timeout);
            Console.WriteLine($"Motion_open in port{port}: {err}");

            // Or specify a specific name in a specific dir
            //err = dev.Motion_openCfgFile(@"C:\Users\user\Desktop\3AxisStage_2P.ini");

            // Motion open configuration file
            err = dev.Motion_openCfgFile("3AxisStage_2P.ini");
            Console.WriteLine($"Motion_openCfgFile in port{port}: {err}");

            // Motion configure
            err = dev.Motion_cfgAxis(port, Const.MOT_AXIS1, Const.MOT_TWO_PULSE, Const.MOT_DIR_CW, Const.MOT_DIR_CW, Const.MOT_ACTIVE_LOW, timeout);
            Console.WriteLine($"Motion_cfgAxis in port{port}: {err}");

            err = dev.Motion_cfgAxisMove(port, Const.MOT_AXIS1, Const.MOT_RELATIVE_POSITION, velo: 2000, target_posi: 10000, accel: 1000, decel: 1000, timeout: timeout);
            Console.WriteLine($"Motion_cfgAxisMove in port{port}: {err}");

            // Motion save configuration file
            err = dev.Motion_saveCfgFile();
            Console.WriteLine($"Motion_saveCfgFile in port{port}: {err}");

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