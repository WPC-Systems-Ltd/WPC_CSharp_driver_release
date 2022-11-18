
/// <summary>
/// SPI_write.cs
/// 
/// This example demonstrates how to communicate with USBDAQF1RD (master) and 25LC640(slave) with SPI interface.
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
///  
/// </summary>

using WPC.Product;

class USBDAQF1RD_SPI_write
{
    static public void Main()
    { 
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

        // Create device handle
        USBDAQF1RD dev = new USBDAQF1RD();

        // Connect to device
        dev.connect("21JA1385");

        // Execute
        try
        {
            // Parameters setting
            int status;
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
            status = dev.DO_openPins(DO_port, new List<int> { DO_pin });
            Console.WriteLine($"DO_openPins status: {status}");

            // Open SPI port
            status = dev.SPI_open(port);
            Console.WriteLine($"SPI_open status: {status}");

            // Set CS(pin0) to high
            status = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"DO_writePins status: {status}");

            /* 
            Set SPI parameter
            */

            // Set SPI port and set datasize to 8-bits data
            status = dev.SPI_setDataSize(port, Constant.SPI_DATA_SIZE_8_BITS);
            Console.WriteLine($"SPI_setDataSize status: {status}");

            // Set SPI port and set first_bit to MSB first
            status = dev.SPI_setFirstBit(port, Constant.SPI_FIRST_BIT_MSB);
            Console.WriteLine($"SPI_setFirstBit status: {status}");

            // Set SPI port and set prescaler to 64
            status = dev.SPI_setPrescaler(port, Constant.SPI_PRESCALER_64);
            Console.WriteLine($"SPI_setPrescaler status: {status}");

            // Set SPI port and set CPOL and CPHA to mode 0 
            status = dev.SPI_setMode(port, Constant.SPI_MODE_0);
            Console.WriteLine($"SPI_setMode status: {status}");

            /* 
            Write data via SPI
            */

            // Set CS(pin0) to low
            status = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 });
            Console.WriteLine($"DO_writePins status: {status}");

            // Write WREN byte
            status = dev.SPI_write(port, new List<byte> { WREN });
            Console.WriteLine($"SPI_write status: {status}");

            // Set CS(pin0) to high
            status = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"DO_writePins status: {status}");

            /* 
            Write data via SPI
            */

            // Set CS(pin0) to low
            status = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 });
            Console.WriteLine($"DO_writePins status: {status}");

            // Write data byte 0x55 in to address 0x0002
            status = dev.SPI_write(port, new List<byte> { WRITE, 0x00, 0x02, 0x55 });
            Console.WriteLine($"SPI_write status: {status}");

            // Set CS(pin0) to high
            status = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 });
            Console.WriteLine($"DO_writePins status: {status}");

            /* 
            Close DO pins and SPI port
            */

            // Close SPI port
            status = dev.SPI_close(port);
            Console.WriteLine($"SPI_close status: {status}");

            // Close pin0 in port0 with digital output
            status = dev.DO_closePins(DO_port, new List<int> { DO_pin });
            Console.WriteLine($"DO_closePins status: {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        // Disconnect device
        dev.disconnect();

        // Release device handle
        dev.close();

        Console.WriteLine("End example code...");
    }
}