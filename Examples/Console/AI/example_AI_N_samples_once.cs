using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class example_AI_N_samples_once
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
            //  Parameters setting
            int status;
            byte port = 1;
            int mode = 1; // 0 : On demand, 1 : N-samples, 2 : Continuous.
            float sampling_rate = 1000;
            uint samples = 5;
            int read_points = 5;
            int delay = 5;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open port 1
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 1 and acquisition mode to N-samples mode (1)
            status = dev.AI_setMode(port, mode);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 1 and sampling rate to 1k(Hz)
            status = dev.AI_setSamplingRate(port, sampling_rate);
            Console.WriteLine($"AI_setSamplingRate status: {status}");

            // Set AI port to 1 and # of samples to 5 (pts)
            status = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"AI_setNumSamples status: {status}");

            // Set AI port to 1 and start acquisition
            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            // Wait amount of time (ms)
            Thread.Sleep(1000);

            // Set AI port to 1 and get 5 points 
            List<List<float>>? data_list = dev.AI_readStreaming(port, read_points, delay);

            if (data_list != null)
            {
                foreach (List<float> CH in data_list)
                {
                    // Read acquisition data 50 points 
                    Console.WriteLine($"data : {CH[0]}, {CH[1]}, {CH[2]}, {CH[3]}, {CH[4]}, {CH[5]}, {CH[6]}, {CH[7]}");
                }
            }
            else
            {
                Console.WriteLine("Null");
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
 


