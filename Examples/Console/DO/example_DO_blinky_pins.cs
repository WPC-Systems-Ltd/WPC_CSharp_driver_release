using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class example_DO_blinky_pins
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

            List<int> DO_pins = new List<int> { 0, 1, 2, 3 };

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open pin0, pin1, pin3 and pin4 in port 0 with digital output
            status = dev.DO_openPins(port_DO, DO_pins);
            Console.WriteLine($"DO_openPins status: {status}");

            for (int i = 0; i < 160; i++)
            {
                List<byte> u8_list = WPC_utilities.convertU16ToStateList((ushort)(i % 16));
                List<int> i32_list = WPC_utilities.convertU8ToI32InList(u8_list);
                status = dev.DO_writePins(port_DO, DO_pins, i32_list);
                Console.WriteLine($"DO_writePins status: {status}");
                Thread.Sleep(5);
            }
          
            Thread.Sleep(1000);

            // Close pin0, pin1, pin3 and pin4 in port 0 with digital input 
            status = dev.DO_closePins(port_DO, DO_pins);
            Console.WriteLine($"DO_closePins status: {status}");

        
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
