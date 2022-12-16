/// <summary>
/// Motion_set_configuration_file.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

using WPC.Product;

class EMotion_set_configuration_file
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

            status = dev.Motion_openConfigurationFile("Emotion.ini");
            Console.WriteLine($"Motion_openConfigurationFile status: {status}");

            status = dev.Motion_configAxisModeAndDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_STEPPER_OUTPUT_TWO_PULSE, Constant.MOTION_AXIS_DIR_CW);
            Console.WriteLine($"Motion_configAxisModeAndDirection status: {status}");

            status = dev.Motion_configEncoderDirection(port, Constant.MOTION_AXIS_1, Constant.MOTION_ENCODER_DIR_CW);
            Console.WriteLine($"Motion_configEncoderDirection status: {status}");

            status = dev.Motion_configAxisMove(port, Constant.MOTION_AXIS_1, Constant.MOTION_RELATIVE_POSITION_MODE, velocity: 2000, target_position: 10000, acceleration: 1000, deceleration: 1000);
            Console.WriteLine($"Motion_configAxisMove status: {status}");

            status = dev.Motion_configInstantLimitStopMode(port, Constant.MOTION_AXIS_1, Constant.MOTION_STOP_DECELERATING);
            Console.WriteLine($"Motion_configInstantLimitStopMode status: {status}");

            status = dev.Motion_saveConfigurationFile();
            Console.WriteLine($"Motion_saveConfigurationFile status: {status}");

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