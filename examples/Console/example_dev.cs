using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WPC_example_code
{
    static void Main()
    {
        Console.WriteLine("Welcome to WPC C# driver example code.");
        Console.WriteLine("There are several example code in this project.");

        Console.WriteLine("");
        Console.WriteLine("For AI");
        Console.WriteLine("example_AI_N_samples_once, please input 10");
        Console.WriteLine("example_AI_on_demand_in_loop, please input 11");
        Console.WriteLine("example_AI_on_demand_once, please input 12");
        Console.WriteLine("example_AI_continuous, please input 13");
        Console.WriteLine("example_AI_N_samples_in_loop, please input 14");

        Console.WriteLine("");
        Console.WriteLine("For AIO");
        Console.WriteLine("example_AIO_all_channels_loopback, please input 20");
        Console.WriteLine("example_AIO_one_channel_loopback, please input 21");

        Console.WriteLine("");
        Console.WriteLine("For AO");
        Console.WriteLine("example_AO_write_all_channels, please input 30");
        Console.WriteLine("example_AO_write_one_channel, please input 31");

        Console.WriteLine("");
        Console.WriteLine("For DO");
        Console.WriteLine("example_DIO_loopback_pins, please input 40");
        Console.WriteLine("example_DIO_loopback_port, please input 41");
        Console.WriteLine("example_DO_blinky_pins, please input 42");
        Console.WriteLine("example_DO_blinky_port, please input 43");
        
        Console.WriteLine("");
        Console.WriteLine("For System");
        Console.WriteLine("example_get_network_info, please input 50");
        Console.WriteLine("example_get_USB_info, please input 51");
        Console.WriteLine("example_find_all_devices, please input 52");
        Console.WriteLine("example_get_WifiDAQ_status, please input 53");
 
        Console.WriteLine("");
        Console.WriteLine("For Temperature-TC");
        Console.WriteLine("example_TC_read_channel_data, please input 60");
        Console.WriteLine("example_TC_read_channel_status, please input 61");
 
        Console.WriteLine("");
        Console.WriteLine("For Temperature-RTD");
        Console.WriteLine("example_RTD_read_channel_data, please input 70");
        Console.WriteLine("example_RTD_read_channel_status, please input 71");

        Console.WriteLine("");
        Console.WriteLine("For I2C");
        Console.WriteLine("example_I2C_write_read, please input 80");
         
        Console.WriteLine("");
        Console.WriteLine("For SPI");
        Console.WriteLine("example_SPI_read_and_write, please input 90");
        Console.WriteLine("example_SPI_write, please input 91");

        Console.WriteLine("");
        Console.WriteLine("For UART");
        Console.WriteLine("example_UART_read, please input 100");
        Console.WriteLine("example_UART_write, please input 101");

        Console.WriteLine("");
        Console.WriteLine("For CAN");
        Console.WriteLine("example_CAN_read, please input 110");
        Console.WriteLine("example_CAN_write, please input 111");

        Console.WriteLine("");
        Console.WriteLine("Input number:  ");

        int input_number = Convert.ToInt32(Console.ReadLine());

        switch (input_number)
        {
            case 10:
                Console.WriteLine("Get 10, run example_AI_N_samples_once.cs");
                Console.WriteLine("");
                WPC_AI_N_samples_once.Main();
                break;

            case 11:
                Console.WriteLine("Get 11, run example_AI_on_demand_in_loop.cs");
                WPC_AI_on_demand_in_loop.Main();
                break;

            case 12:
                Console.WriteLine("Get 12, run example_AI_on_demand_once.cs");
                Console.WriteLine("");
                WPC_AI_on_demand_once.Main();
                break;

            case 13:
                Console.WriteLine("Get 13, run example_AI_continuous.cs");
                Console.WriteLine("");
                WPC_AI_continuous.Main();
                break;

            case 14:
                Console.WriteLine("Get 14, run example_AI_N_samples_in_loop.cs");
                Console.WriteLine("");
                WPC_AI_N_samples_in_loop.Main();
                break;

            case 20:
                Console.WriteLine("Get 20, run example_AIO_all_channels_loopback.cs");
                Console.WriteLine("");
                WPC_AIO_all_channels_loopback.Main();
                break;

            case 21:
                Console.WriteLine("Get 21, run example_AIO_one_channel_loopback.cs");
                Console.WriteLine("");
                WPC_AIO_one_channel_loopback.Main();
                break;

            case 30:
                Console.WriteLine("Get 30, run example_AO_write_all_channels.cs");
                Console.WriteLine("");
                WPC_AO_write_all_channels.Main();
                break;

            case 31:
                Console.WriteLine("Get 31, run example_AO_write_one_channel.cs");
                Console.WriteLine("");
                WPC_AO_write_one_channel.Main();
                break;

            case 40:
                Console.WriteLine("Get 40, run example_DIO_loopback_pins.cs");
                Console.WriteLine("");
                WPC_DIO_loopback_pins.Main();
                break;

            case 41:
                Console.WriteLine("Get 41, run example_DIO_loopback_port.cs");
                Console.WriteLine("");
                WPC_DIO_loopback_port.Main();
                break;

            case 42:
                Console.WriteLine("Get 42, run example_DO_blinky_pins.cs");
                Console.WriteLine("");
                WPC_DO_blinky_pins.Main();
                break;

            case 43:
                Console.WriteLine("Get 43, run example_DO_blinky_port.cs");
                Console.WriteLine("");
                WPC_DO_blinky_port.Main();
                break;

            case 50:
                Console.WriteLine("Get 50, run example_get_network_info.cs");
                Console.WriteLine("");
                WPC_get_network_info.Main();
                break;

            case 51:
                Console.WriteLine("Get 51, run example_get_USB_info.cs");
                Console.WriteLine("");
                WPC_get_USB_info.Main();
                break;

            case 52:
                Console.WriteLine("Get 52, run example_find_all_devices.cs");
                Console.WriteLine("");
                WPC_find_all_devices.Main();
                break;

            case 53:
                Console.WriteLine("Get 53, run example_get_WifiDAQ_status.cs");
                Console.WriteLine("");
                WPC_get_WifiDAQ_status.Main();
                break;

            case 60:
                Console.WriteLine("Get 60, run example_TC_read_channel_data.cs");
                Console.WriteLine("");
                WPC_TC_read_channel_data.Main();
                break;

            case 61:
                Console.WriteLine("Get 61, run example_TC_read_channel_status.cs");
                Console.WriteLine("");
                WPC_UART_write.Main();
                break;

            case 70:
                Console.WriteLine("Get 70, run example_RTD_read_channel_data.cs");
                Console.WriteLine("");
                WPC_RTD_read_channel_data.Main();
                break;

            case 71:
                Console.WriteLine("Get 71, run example_RTD_read_channel_status.cs");
                Console.WriteLine("");
                WPC_RTD_read_channel_status.Main();
                break;

            case 80:
                Console.WriteLine("Get 80, run example_I2C_write_read.cs");
                Console.WriteLine("");
                WPC_I2C_write_read.Main();
                break;

            case 90:
                Console.WriteLine("Get 90, run example_SPI_read_and_write.cs");
                Console.WriteLine("");
                WPC_SPI_read_and_write.Main();
                break;

            case 91:
                Console.WriteLine("Get 91, run example_SPI_write.cs");
                Console.WriteLine("");
                WPC_SPI_write.Main();
                break;

            case 100:
                Console.WriteLine("Get 100, run example_UART_read.cs");
                Console.WriteLine("");
                WPC_UART_read.Main();
                break;

            case 101:
                Console.WriteLine("Get 101, run example_UART_write.cs");
                Console.WriteLine("");
                WPC_UART_write.Main();
                break;

            case 110:
                Console.WriteLine("Get 110, run example_CAN_read.cs");
                Console.WriteLine(""); 
                WPC_CAN_read.Main();

                break;
            case 111:
                Console.WriteLine("Get 111, run example_CAN_write.cs");
                Console.WriteLine("");
                WPC_CAN_write.Main();
                break;
            default:
                Console.WriteLine("Wrong index, we will use example_find_all_devices example code");
                Console.WriteLine("");
                WPC_find_all_devices.Main();
                break;
        }
  
    }
}

