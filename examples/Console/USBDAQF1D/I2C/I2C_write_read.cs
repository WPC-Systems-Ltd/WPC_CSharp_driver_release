/// I2C_write_read.cs with synchronous mode.
/// 
/// This example demonstrates how to communicate with USBDAQF1D (master) and 24C08C(slave) with I2C interface.
/// 
/// First, it shows how to open I2C port and configure I2C parameters.
/// 
/// Second, write some bytes with address into EEPROM (24C08C). We have to make sure that bytes written in address is correct however read address from EEPROM (24C08C).
/// 
/// Last, close I2C port
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_I2C_write_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

        // Connect to device
        dev.connect("21JA1200");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 1;
            int timeout = 3000;
            int device_address = 0x50;
            byte word_address = 0x00;
            List<byte> I2C_write_data = new List<byte> { word_address, 0xAA, 0x55, 0xAA, 0x55};

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open I2C port
            err = dev.I2C_open(port, timeout);
            Console.WriteLine($"open: {err}"); 

            // Set I2C port and set clock rate to standard mode
            err = dev.I2C_setClockRate(port, Const.I2C_SPEED_STANDARD, timeout);
            Console.WriteLine($"setClockRate: {err}"); 

            // Write WREN byte
            err = dev.I2C_write(port, device_address, I2C_write_data, timeout);
            Console.WriteLine($"write: {err}"); 

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Read data via I2C
            err = dev.I2C_write(port, device_address, new List<byte> {word_address}, timeout);
            Console.WriteLine($"write: {err}"); 

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            List<byte> data = dev.I2C_read(port, device_address, 4, timeout); 
            Console.WriteLine($"I2C_read data: {data[0]},{data[1]},{data[2]},{data[3]}");

            // Close I2C port
            err = dev.I2C_close(port, timeout);
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