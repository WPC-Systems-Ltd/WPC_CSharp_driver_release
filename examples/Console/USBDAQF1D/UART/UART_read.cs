/// UART_read.cs with synchronous mode.
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
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class USBDAQF1D_UART_read
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
            int baudrate = 9600;
            int timeout = 3000; // ms
            int read_delay = 5; // ms

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo(timeout);
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open UART
            err = dev.UART_open(port, timeout);
            Console.WriteLine($"UART_open in port {port}, status: {err}");;

            // Set UART port and set baudrate to 9600
            err = dev.UART_setBaudRate(port, baudrate, timeout);
            Console.WriteLine($"UART_setBaudRate in port {port}, status: {err}");

            // Set UART port and set data bit to 8-bit data
            err = dev.UART_setDataBit(port, Const.UART_DATA_SIZE_8_BITS, timeout);
            Console.WriteLine($"UART_setDataBit in port {port}, status: {err}");

            // Set UART port and set parity to None
            err = dev.UART_setParity(port, Const.UART_PARITY_NONE, timeout);
            Console.WriteLine($"UART_setParity in port {port}, status: {err}");

            // Set UART port and set stop bit to to 1 bit
            err = dev.UART_setNumStopBit(port, Const.UART_STOP_BIT_1, timeout);
            Console.WriteLine($"UART_setNumStopBit in port {port}, status: {err}");

            // Print information
            Console.WriteLine("Wait for 10 seconds to receive data from other devices");

            // Wait
            Thread.Sleep(10000); // delay [ms]

            // Set UART port and read 20 bytes
            List<byte> data = dev.UART_read(port, 20, read_delay);
            WPC_utilities.printByteList(data);

            // Close UART
            err = dev.UART_close(port, timeout);
            Console.WriteLine($"UART_close in port {port}, status: {err}");
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