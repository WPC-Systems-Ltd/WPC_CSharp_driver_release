/// SPI_write.cs
/// 
/// This example demonstrates how to communicate with USBDAQF1CD (master) and 25LC640(slave) with SPI interface.
/// 
/// First, it shows how to open SPI port & DIO pins and configure SPI parameters.
/// 
/// Second, write some bytes with address into EEPROM (25LC640).
/// 
/// Last, close SPI port & DIO pins
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1CD_SPI_write
{
    static public void Main()
    { 
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1CD dev = new USBDAQF1CD();

        // Connect to device
        dev.connect("21JA1312");

        // Execute
        try
        {
            // Parameters setting
            int err;
            int port = 2;
            int DO_port = 0;
            int DO_pin = 0;
            byte WRITE = 0x02; 
            byte WREN = 0x06;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            /* 
             Open DO pins & SPI port & set CS(pin0) to high
            */

            // Open pin0 in port0 with digital output
            err = dev.DO_openPins(DO_port, new List<int> { DO_pin });
            Console.WriteLine($"openPins: {err}");

            // Open SPI port
            err = dev.SPI_open(port);
            Console.WriteLine($"open: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"writePins: {err}");

            /* 
            Set SPI parameter
            */

            // Set SPI port and set datasize to 8-bits data
            err = dev.SPI_setDataSize(port, Const.SPI_DATA_SIZE_8_BITS);
            Console.WriteLine($"setDataSize: {err}");

            // Set SPI port and set first_bit to MSB first
            err = dev.SPI_setFirstBit(port, Const.SPI_FIRST_BIT_MSB);
            Console.WriteLine($"setFirstBit: {err}");

            // Set SPI port and set prescaler to 64
            err = dev.SPI_setPrescaler(port, Const.SPI_PRESCALER_64);
            Console.WriteLine($"setPrescaler: {err}");

            // Set SPI port and set CPOL and CPHA to mode 0 
            err = dev.SPI_setMode(port, Const.SPI_MODE_0);
            Console.WriteLine($"setMode: {err}");

            /* 
            Write data via SPI
            */

            // Set CS(pin0) to low
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 });
            Console.WriteLine($"writePins: {err}");

            // Write WREN byte
            err = dev.SPI_write(port, new List<byte> { WREN });
            Console.WriteLine($"write: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"writePins: {err}");

            /* 
            Write data via SPI
            */

            // Set CS(pin0) to low
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 });
            Console.WriteLine($"writePins: {err}");

            // Write data byte 0x55 in to address 0x0002
            err = dev.SPI_write(port, new List<byte> { WRITE, 0x00, 0x02, 0x55 });
            Console.WriteLine($"write: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"writePins: {err}");

            /* 
            Close DO pins and SPI port
            */

            // Close SPI port
            err = dev.SPI_close(port);
            Console.WriteLine($"close: {err}");

            // Close pin0 in port0 with digital output
            err = dev.DO_closePins(DO_port, new List<int> { DO_pin });
            Console.WriteLine($"closePins: {err}");
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