/// <summary>
/// Motion_get_logical_position.cs
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

class EMotion_get_logical_position
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
            int status;
            int port = 0;
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            for (int i = 0; i < 1000; i++) 
            {   
                status = dev.Motion_setLogicalPostion(port, Constant.MOTION_AXIS_1, i);
                if (status != 0)
                {
                    Console.WriteLine($"Motion_setLogicalPostion status: {status}"); 
                }
                
                int posi = dev.Motion_GetLogicalPostion(port, Constant.MOTION_AXIS_1);
                Console.WriteLine($"Motion_GetLogicalP3ostion: {posi}");
            }
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