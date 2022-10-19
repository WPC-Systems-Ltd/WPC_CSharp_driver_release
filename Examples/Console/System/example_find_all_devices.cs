using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class example_find_all_devices
{
    static public void Main()
    {
        Console.WriteLine("Start example code...");

        // Create device handle
        Broadcaster dev = new Broadcaster();

        // Get C# driver version
        Console.WriteLine($"{dev.getDriverName()} - Version {dev.getDriverVersion()}");

        // Connect to network device
        try
        {
            dev.connect();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Perform device information 
        try
        {
            List<List<string>> device_list = dev.Bcst_getDeviceInfo();
            Console.WriteLine($"device_list.Count(): {device_list.Count()}");
            if (device_list.Count() > 0)
            {
                foreach (List<string> device in device_list)
                {
                    Console.WriteLine($"IP: {device[0]}, Subnet: {device[1]}, MAC: {device[2]}, Firmware version: {device[3]}");
                }
            }
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
