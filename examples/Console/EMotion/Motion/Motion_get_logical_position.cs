/// Motion_get_logical_position.cs
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022-2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EMotion_get_logical_position
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
            int err;
            int port = 0;
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            err = dev.Motion_open(port);
            Console.WriteLine($"open: {err}");

            for (int i = 0; i < 100; i++) 
            {   
                err = dev.Motion_setLogicalPosi(port, Const.MOT_AXIS1, i);
                if (err != 0)
                {
                    Console.WriteLine($"setLogicalPosi: {err}");
                }
                int posi = dev.Motion_getLogicalPosi(port, Const.MOT_AXIS1);
                Console.WriteLine($"getLogicalPosi: {posi}");
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