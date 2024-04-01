/// I2C_write_read.cs with synchronous mode.
///
/// This example demonstrates how to communicate with USBDAQF1TD (master) and 24C08C(slave) with I2C interface.
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
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1TD_I2C_write_read
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

        // Connect to device
        try
        {
            dev.connect("default"); // Depend on your device
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
            int timeout = 3000; // ms
            int device_address = 0x50; // 01010000
            byte word_address = 0x00;
            List<byte> I2C_write_data = new List<byte> { word_address, 0xAA, 0x55, 0xAA, 0x55};

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open I2C port
            err = dev.I2C_open(port, timeout);
            Console.WriteLine($"I2C_open in port {port}: {err}");

            // Set I2C port and set clock rate to standard mode
            err = dev.I2C_setClockRate(port, Const.I2C_SPEED_STANDARD, timeout);
            Console.WriteLine($"I2C_setClockRate in port {port}: {err}");

            // Write WREN byte
            err = dev.I2C_write(port, device_address, I2C_write_data, timeout);
            Console.WriteLine($"I2C_write in port {port}: {err}");

            // Read data acquisition via I2C
            err = dev.I2C_write(port, device_address, new List<byte> {word_address}, timeout);
            Console.WriteLine($"I2C_write in port {port}: {err}");

            List<byte> data = dev.I2C_read(port, device_address, 4, timeout);
            Console.WriteLine($"I2C_read data in port {port}: {data[0]},{data[1]},{data[2]},{data[3]}");

            // Close I2C port
            err = dev.I2C_close(port, timeout);
            Console.WriteLine($"I2C_close in port {port}: {err}");
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