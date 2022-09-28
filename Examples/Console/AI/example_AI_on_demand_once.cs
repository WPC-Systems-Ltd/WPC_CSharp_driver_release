using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class example_AI_on_demand_once
{
    static void Main1()
    {
        Console.WriteLine("Start example code...");

        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Get C# driver version
        Console.WriteLine($"{dev.getDriverName()} - Version {dev.getDriverVersion()}");

        // Connect to network device

        dev.connect("192.168.5.79");

        // Perform DAQ basic information 
        try
        {
            // Parameters setting
            int status;
            int port = 1;
            int mode = 0;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open port 1
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 1 and acquisition mode to on demand mode (0)
            status = dev.AI_setMode(port, mode);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 1 and data acquisition
            List<float> ch = dev.AI_readOnDemand(port);
            Console.WriteLine($"data: {ch[0]}, {ch[1]}, {ch[2]}, {ch[3]}, {ch[4]}, {ch[5]}, {ch[6]}, {ch[7]},");

            // Close port 1
            status = dev.AI_close(port);
            Console.WriteLine($"AI_close status: {status}");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        // Disconnect network device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}
