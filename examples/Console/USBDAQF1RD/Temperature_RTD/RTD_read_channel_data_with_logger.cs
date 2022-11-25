/// <summary>
/// RTD_read_channel_data_with_logger.cs
/// 
/// This example demonstrates how to read RTD data and write to CSV file in two channels from USBDAQF1RD.
/// 
/// First, it shows how to open thermal port
/// 
/// Second, read channel 0 and channel 1 RTD data
/// 
/// Last, close thermal port.
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

class USBDAQF1RD_DataLogger_RTD_read_channel_data
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

        // Create datalogger handle
        Datalogger dev_logger = new Datalogger();
        
        // Open file with WPC_test.csv
        dev_logger.Logger_openFile("WPC_tester_USBDAQF1RD_RTD.csv");
        
        // Write header into CSV file
        var data_write = $"RTD CH0, RTD CH1"; 
        dev_logger.Logger_writeValue(data_write);

        // Execute
        try
        {
            // Parameters setting
            int status;
            int port = 1;
            int channel_0 = 0;
            int channel_1 = 1;

            // Get firmware model & version
            string[] driver_info = dev.Sys_getDriverInfo();
            Console.WriteLine($"Model name: {driver_info[0]}");
            Console.WriteLine($"Firmware version: {driver_info.Last()}");

            // Open RTD port
            status = dev.Thermal_open(port);
            Console.WriteLine($"Thermal_open status: {status}");

            // Wait for 0.1 sec
            Thread.Sleep(100); // delay [ms]

            // Set RTD port and read RTD in channel 0
            float data0 = dev.Thermal_readSensor(port, channel_0);
            Console.WriteLine($"Read channel 0 data: {data0} °C ");

            // Set RTD port and read RTD in channel 1
            float data1 = dev.Thermal_readSensor(port, channel_1);
            Console.WriteLine($"Read channel 1 data: {data1} °C ");
            
            // Write data into CSV file
            var data = $"{data0}, {data1}"; 
            dev_logger.Logger_writeValue(data);  

            // Close RTD port
            status = dev.Thermal_close(port);
            Console.WriteLine($"Thermal_close status: {status}");
            
            // Close File
            dev_logger.Logger_closeFile();

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