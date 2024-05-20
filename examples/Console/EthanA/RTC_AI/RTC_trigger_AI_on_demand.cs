/// RTC_trigger_AI_on_demand.cs with synchronous mode.
///
/// This example demonstrates how to use RTC to trigger AI with on demand mode from EthanA.
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;
class EthanA_RTC_trigger_AI_on_demand
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanA dev = new EthanA();

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
            int channel = 8;
            int mode = Const.RTC_AI_MODE_ON_DEMAND;
            int timeout = 3000; // ms
            byte mode_alarm = 0;
            byte month = 4;
            byte day = 2;
            byte hour = 15;
            byte minute = 8;
            byte start_second = 40;
            byte second = 50;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open AI
            err = dev.AI_open(port, timeout);
            Console.WriteLine($"AI_open in port {port}, status: {err}");

            // Set AI mode
            err = dev.AI_setMode(port, mode, timeout);
            Console.WriteLine($"AI_setMode {mode} in port {port}, status: {err}");

            // Set RTC
            err = dev.Sys_setRTC(2024, month, day, hour, minute, start_second, timeout);
            Console.WriteLine($"Set RTC to 2024-{month}-{day}, {hour}:{minute}:{start_second} , status: {err}");

            // Start RTC alarm
            err = dev.Sys_startRTCAlarm(mode_alarm, day, hour, minute, second, timeout);
            Console.WriteLine($"Alarm RTC to 2024-{month}-{day}, {hour}:{minute}:{second} , status: {err}");

            for(int i=0; i<10; i++){
                string rtc_time = dev.Sys_getRTC(timeout);
                Console.WriteLine($"Get RTC: {rtc_time}");
                Thread.Sleep(1000); // delay [ms]
            }

            // Read data acquisition acquisition
            List<double> sample = dev.AI_readOnDemand(port, timeout);

            // Print data
            Console.WriteLine(string.Format("[{0}]", string.Join(", ", sample)));

            // Close AI
            err = dev.AI_close(port, timeout);
            Console.WriteLine($"AI_close in port {port}, status: {err}");

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