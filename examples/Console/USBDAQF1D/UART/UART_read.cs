/// <summary>
/// @example UART_read.cs
///
/// This example demonstrates how to read data from another device with UART interface from USBDAQF1D.
/// 
/// First, it shows how to open UART port and configure UART parameters.
/// 
/// Second, read bytes from another device.
/// 
/// Last, close UART port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2022 WPC Systems Ltd.
/// All rights reserved.
///  
/// </summary>

class USBDAQF1D_UART_read
{
    static public void Main()
    { 
        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1D dev = new USBDAQF1D();

        // Connect to device
        dev.connect("21JA1200");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 2;
            int baudrate = 9600;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open UART port
            status = dev.UART_open(port);
            Console.WriteLine($"UART_open status: {status}");

            // Set UART port and set baudrate to 9600
            status = dev.UART_setBaudRate(port, baudrate);
            Console.WriteLine($"UART_setBaudRate status: {status}");

            // Set UART port and set data bit to 8-bit data
            status = dev.UART_setDataBit(port, WPC.UART_DATA_SIZE_8_BITS);
            Console.WriteLine($"UART_setDataBit status: {status}");

            // Set UART port and set parity to None
            status = dev.UART_setParity(port, WPC.UART_PARITY_NONE);
            Console.WriteLine($"UART_setParity status: {status}");

            // Set UART port and set stop bit to to 1 bit
            status = dev.UART_setNumStopBit(port, WPC.UART_STOP_BIT_1);
            Console.WriteLine($"UART_setNumStopBit status: {status}");

            // Wait for 10 sec
            Thread.Sleep(10000); // delay [ms]

            // Set UART port and read 20 bytes
            List<byte> data  = dev.UART_read(port, 20);
            WPC_utilities.printByteList(data);
 
            // Close UART port
            status = dev.UART_close(port);
            Console.WriteLine($"UART_close status: {status}");
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