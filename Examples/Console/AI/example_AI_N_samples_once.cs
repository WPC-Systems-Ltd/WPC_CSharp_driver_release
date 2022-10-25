using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

public class example_AI_N_samples_once
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1AD dev = new USBDAQF1AD();

        // Connect to USB device
        dev.connect("21JA1245");
         
        // Execute
        try
        {
            // Parameters setting
            int status;
            byte port = 0;
            int samples = 10;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open port 0
            status = dev.AI_open(port);
            Console.WriteLine($"AI_open status: {status}");

            // Set AI port to 0 and acquisition mode to N-sample mode
            status = dev.AI_setMode(port, WPC.AI_MODE_N_SAMPLE);
            Console.WriteLine($"AI_setMode status: {status}");

            // Set AI port to 0 and # of samples to 5 (pts)
            status = dev.AI_setNumSamples(port, samples);
            Console.WriteLine($"AI_setNumSamples status: {status}");

            // Set AI port to 0 and start acquisition
            status = dev.AI_start(port);
            Console.WriteLine($"AI_start status: {status}");

            Thread.Sleep(100);

            // Set AI port to 0 and get 10 points 
            List<List<double>> streaming_list = dev.AI_readStreaming(port, samples, 10);

            foreach (List<double> sample in streaming_list)
            {
                // Read acquisition
                Console.WriteLine($"data : {sample[0]}, {sample[1]}, {sample[2]}, {sample[3]}, {sample[4]}, {sample[5]}, {sample[6]}, {sample[7]}");
            }

            // Close port 0
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