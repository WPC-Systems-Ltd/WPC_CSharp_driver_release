/// DO_write_port.cs with synchronous mode.
///
/// This example illustrates the process of writing a high or low signal to a DO port from EthanD.
///
/// To begin with, it demonstrates the steps required to open the DO port.
/// Next, voltage output is written to the DO port.
/// Lastly, it concludes by closing the DO port.

/// For other examples please check:
/// https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples
/// See README.md file to get detailed usage of this example.
///
/// Copyright (c) 2024 WPC Systems Ltd.
/// All rights reserved.

using WPC.Product;

class EthanD_DO_write_port
{
    static public void Main()
    {
        // Get C# driver version
        Console.WriteLine($"{Const.PKG_FULL_NAME} - Version {Const.VERSION}");

        // Create device handle
        EthanD dev = new EthanD();

        // Connect to device
        try
        {
            dev.connect("192.168.1.110"); // Depend on your device
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
            int err;
            int port = 0; // Depend on your device

            List<int> DO_value = new List<int> {1, 0, 1, 0};
            int timeout = 3000;  // ms

            // Open port with digital output
            err = dev.DO_openPort(port, timeout);
            Console.WriteLine($"DO_openPort in port {port}, status: {err}");

            // Write DO port to high or low
            err = dev.DO_writePort(port, DO_value, timeout);
            Console.WriteLine($"DO_writePort in DO_port {port}, status: {err}");

            // Wait for ms to see led status
            Thread.Sleep(3000); // delay [ms]

            // Close port with digital output
            err = dev.DO_closePort(port, timeout);
            Console.WriteLine($"DO_closePort in port {port}, status: {err}");
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
