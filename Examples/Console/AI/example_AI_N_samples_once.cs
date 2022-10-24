using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

public class example_AI_N_samples_once
{
    static public void Main()
    {
        // Create device handle
        WifiDAQE3A dev = new WifiDAQE3A();

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Connect to network device
        try
        {
            dev.connect("192.168.5.79");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }
        // Execute
        try
        {
            // Parameters setting
            int status;
            byte port = 1;
            int samples = 10;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open port 1
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 1 and acquisition mode to on demand mode (1)
            status = dev.AI_setMode(port, 1);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 1 and # of samples to 5 (pts)
            status = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"AI_setNumSamples status: {status}");


            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            Thread.Sleep(100);

            // Set AI port to 0 and get 50 points 
            List<List<double>> streaming_list = dev.AI_readStreaming(port, samples, 10);

            foreach (List<double> sample in streaming_list)
            {
                // Read acquisition data 50 points 
                Console.WriteLine($"data : {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

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