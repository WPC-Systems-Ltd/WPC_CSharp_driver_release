/// SPI_read_and_write.cs with synchronous mode.
///
/// This example demonstrates how to communicate with USBDAQF1D (master) and 25LC640(slave) with SPI interface.
///
/// First, it shows how to open SPI port & DIO pins and configure SPI parameters.
///
/// Second, write some bytes with address into EEPROM (25LC640). We have to make sure that bytes written in address is correct however read address from EEPROM (25LC640).
///
/// Last, close SPI port & DIO pins
///
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_SPI_read_and_write
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

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
            int port = 2;
            int DO_port = 0;
            int DO_pin = 0;
            byte WRITE = 0x02;
            byte DUMMY = 0x01;
            byte READ = 0x03;
            byte WREN = 0x06;
            int timeout = 3000; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            /*
             Open DO pins & SPI port & set CS(pin0) to high
            */

            // Open pin0 in port0 with digital output
            err = dev.DO_openPins(DO_port, new List<int> {DO_pin}, timeout);
            Console.WriteLine($"DO_openPins in port {DO_port}: {err}");

            // Open SPI port
            err = dev.SPI_open(port, timeout);
            Console.WriteLine($"SPI_open in port {port}: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> {DO_pin}, new List<int> {1}, timeout);
            Console.WriteLine($"DO_writePins in port {DO_port}: {err}");

            /*
            Set SPI parameter
            */

            // Set SPI port and set datasize to 8-bits data
            err = dev.SPI_setDataSize(port, Const.SPI_DATA_SIZE_8_BITS, timeout);
            Console.WriteLine($"SPI_setDataSize in port {port}: {err}");

            // Set SPI port and set first_bit to MSB first
            err = dev.SPI_setFirstBit(port, Const.SPI_FIRST_BIT_MSB, timeout);
            Console.WriteLine($"SPI_setFirstBit in port {port}: {err}");

            // Set SPI port and set prescaler to 64
            err = dev.SPI_setPrescaler(port, Const.SPI_PRESCALER_64, timeout);
            Console.WriteLine($"SPI_setPrescaler in port {port}: {err}");

            // Set SPI port and set CPOL and CPHA to mode 0
            err = dev.SPI_setMode(port, Const.SPI_MODE_0, timeout);
            Console.WriteLine($"SPI_setMode in port {port}: {err}");

            /*
            Write data via SPI
            */

            // Set CS(pin0) to low
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 }, timeout);
            Console.WriteLine($"DO_writePins in port {DO_port}: {err}");

            // Write WREN byte
            err = dev.SPI_write(port, new List<byte> { WREN }, timeout);
            Console.WriteLine($"SPI_write in port {port}: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 }, timeout);
            Console.WriteLine($"DO_writePins in port {DO_port}: {err}");

            /*
            Write data via SPI
            */

            // Set CS(pin0) to low
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 }, timeout);
            Console.WriteLine($"DO_writePins in port {DO_port}: {err}");;

            // Write data byte 0x0A in to address 0x0001
            err = dev.SPI_write(port, new List<byte> { WRITE, 0x00, 0x01, 0x0A }, timeout);
            Console.WriteLine($"SPI_write in port {port}: {err}");

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 }, timeout);
            Console.WriteLine($"DO_writePins in port {DO_port}: {err}");

            /*
            Read data via SPI
            */

            // Set CS(pin0) to low
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 0 }, timeout);
            Console.WriteLine($"writePins: {err}");

            // Read the written byte from the generated address
            List<byte> data = dev.SPI_readAndWrite(port, new List<byte> { READ, 0x00, 0x01, DUMMY }, timeout);

            WPC_utilities.printByteArray(data.ToArray());

            // Set CS(pin0) to high
            err = dev.DO_writePins(DO_port, new List<int> { DO_pin }, new List<int> { 1 }, timeout);
            Console.WriteLine($"writePins: {err}");

            /*
            Close DO pins and SPI port
            */

            // Close SPI port
            err = dev.SPI_close(port, timeout);
            Console.WriteLine($"SPI_close in port {port}: {err}");

            // Close pin0 in port0 with digital output
            err = dev.DO_closePins(DO_port, new List<int> { DO_pin }, timeout);
            Console.WriteLine($"DO_closePins in port {DO_port}: {err}");
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