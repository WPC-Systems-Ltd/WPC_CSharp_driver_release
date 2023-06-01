/// AI_on_demand_in_loop.cs with synchronous mode.
///
/// This example demonstrates how to get AI data with loop in ondemand mode from STEM.
///
/// First, it shows how to open AI port and configure AI parameters.
///
/// Second, read AI data
///
/// Last, close AI port.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class STEM_AI_on_demand_in_loop
{
    static void loop_func(STEM handle, int port, int delay=50, int exit_loop_time=300)
    {
        int time_cal = 0;
        while (time_cal < exit_loop_time)
        {
            // Data acquisition
            List<double> sample = handle.AI_readOnDemand(port);
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Wait
            Thread.Sleep(delay); // delay [ms]
            time_cal += delay;
        }
    }

    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        STEM dev = new STEM();

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
            int port = 1;
            int mode = Const.AI_MODE_ON_DEMAND;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout:timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");
            
            // Get port mode
            string port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // If the port mode is not set to "AIO", set the port mode to "AIO"
            if (port_mode != "AIO"){
                err = dev.Sys_setPortAIOMode(port, timeout:timeout);
                Console.WriteLine($"Sys_setPortAIOMode: {err}");
            }
            // Get port mode
            port_mode = dev.Sys_getPortMode(port, timeout:timeout);
            Console.WriteLine($"Slot mode: {port_mode}");

            // Open AI port
            err = dev.AI_open(port, timeout:timeout);
            Console.WriteLine($"AI_open in port{port}: {err}");

            // Enable CS
            err = dev.AI_enableCS(port, new List<int> {0, 1}, timeout:timeout);
            Console.WriteLine($"AI_start in port{port}: {err}");
            
            // Set AI port and acquisition mode to on demand
            err = dev.AI_setMode(port, mode, timeout:timeout);
            Console.WriteLine($"AI_setMode {mode}: {err}");

            // loop parameters
            int delay = 50;
            int exit_loop_time = 300;

            // Start loop
            loop_func(dev, port, delay:delay, exit_loop_time:exit_loop_time);

            // Close AI port
            err = dev.AI_close(port, timeout:timeout);
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