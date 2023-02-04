/// UART_write.cs with synchronous mode.
///
/// This example demonstrates how to write data to another device with UART interface from USBDAQF1D.
/// 
/// First, it shows how to open UART port and configure UART parameters.
/// 
/// Second, write bytes to another device.
/// 
/// Last, close UART port.
/// 
/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
/// 
/// Copyright (c) 2023 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_UART_write
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
            int port = 2;
            int baudrate = 9600;
            int timeout = 3000;
            
            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open UART port
            err = dev.UART_open(port);
            Console.WriteLine($"open: {err}");

            // Set UART port and set baudrate to 9600
            err = dev.UART_setBaudRate(port, baudrate);
            Console.WriteLine($"setBaudRate: {err}");

            // Set UART port and set data bit to 8-bit data
            err = dev.UART_setDataBit(port, Const.UART_DATA_SIZE_8_BITS);
            Console.WriteLine($"setDataBit: {err}");

            // Set UART port and set parity to None
            err = dev.UART_setParity(port, Const.UART_PARITY_NONE);
            Console.WriteLine($"setParity: {err}");

            // Set UART port and set stop bit to 1 bit
            err = dev.UART_setNumStopBit(port, Const.UART_STOP_BIT_1);
            Console.WriteLine($"setNumStopBit: {err}");

            // Set UART port and and write "chunglee people" to device
            err = dev.UART_write(port, "chunglee_people");
            Console.WriteLine($"write: {err}");;

            // Set UART port and and write "WPC_systems" to device
            err = dev.UART_write(port, "WPC_systems");
            Console.WriteLine($"write: {err}");;

            // Close UART port
            err = dev.UART_close(port);
            Console.WriteLine($"close: {err}");;
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