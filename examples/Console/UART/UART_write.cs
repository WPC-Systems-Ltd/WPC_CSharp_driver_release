using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @example UART_write.cs
///
/// This example demonstrates how to write data to another device with UART interface from USBDAQF1TD.
/// 
/// First, it shows how to open UART port and configure UART parameters.
/// 
/// Second, write bytes to another device.
/// 
/// Last, close UART port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// 
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright(c) 2022 WPC Systems Ltd.
/// All rights reserved.
/// </summary>

class WPC_UART_write
{
    static public void Main()
    {

        Console.WriteLine("Start example code...");

        // Get C# driver version
        Console.WriteLine($"{WPC.PKG_FULL_NAME} - Version {WPC.VERSION}");

        // Create device handle
        USBDAQF1TD dev = new USBDAQF1TD();

        // Connect to device
        dev.connect("21JA1239");

        // Execute
        try
        {
            // Parameters setting
            int status;
            int UART_port = 2;
            int baudrate = 9600;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open UART port
            status = dev.UART_open(UART_port);
            Console.WriteLine($"UART_open status: {status}");

            // Set UART port and set baudrate to 9600
            status = dev.UART_setBaudRate(UART_port, baudrate);
            Console.WriteLine($"UART_setBaudRate status: {status}");

            // Set UART port and set data bit to 8-bit data
            status = dev.UART_setDataBit(UART_port, WPC.UART_DATA_SIZE_8_BITS);
            Console.WriteLine($"UART_setDataBit status: {status}");

            // Set UART port and set parity to None
            status = dev.UART_setParity(UART_port, WPC.UART_PARITY_NONE);
            Console.WriteLine($"UART_setParity status: {status}");

            // Set UART port and set stop bit to 1 bit
            status = dev.UART_setNumStopBit(UART_port, WPC.UART_STOP_BIT_1);
            Console.WriteLine($"UART_setNumStopBit status: {status}");

            // Set UART port and and write "chunglee people" to device
            status = dev.UART_write(UART_port, "chunglee_people");
            Console.WriteLine($"UART_write status: {status}");

            // Set UART port and and write "WPC_systems" to device
            status = dev.UART_write(UART_port, "WPC_systems");
            Console.WriteLine($"UART_write status: {status}");

            // Close UART port
            status = dev.UART_close(UART_port);
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
