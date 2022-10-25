using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class example_DIO_loopback_port
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
            byte port_DO = 0; // For DO
            byte port_DI = 1; // For DI

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open all pins in port 0 with digital output 
            status = dev.DO_openPort(port_DO);
            Console.WriteLine($"DO_openPort status: {status}");

            // Set pin0, pin1 and pin2 to high, others to low
            status = dev.DO_writePort(port_DO, new List<int> { 1, 0, 1, 0, 0, 0, 0, 0 });
            Console.WriteLine($"DO_writePort status: {status}");

            // Open all pins in port 1 with digital input
            status = dev.DI_openPort(port_DI);
            Console.WriteLine($"DI_openPort status: {status}");
            
            // Read all pins state in port 1
            List<int> pin_s = dev.DI_readPort(port_DI);
            Console.WriteLine($"DI_readPort: {pin_s[0]},{pin_s[1]},{pin_s[2]},{pin_s[3]},{pin_s[4]},{pin_s[5]}");

            // Close all pins in port 0 with digital output
            status = dev.DO_closePort(port_DO);
            Console.WriteLine($"DO_closePort status: {status}");

            // Close all pins in port 1 with digital input
            status = dev.DI_closePort(port_DI);
            Console.WriteLine($"DI_closePort status: {status}");

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



