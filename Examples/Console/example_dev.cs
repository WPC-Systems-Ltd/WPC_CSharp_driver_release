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
        Console.WriteLine("For DO");
        Console.WriteLine("example_DIO_loopback_pins, please input 20");
        Console.WriteLine("example_DIO_loopback_port, please input 21");
        Console.WriteLine("example_DO_blinky_pins, please input 22");
        Console.WriteLine("example_DO_blinky_port, please input 23");
        
        Console.WriteLine("");
        Console.WriteLine("For System");
        Console.WriteLine("example_get_device_info, please input 30");
        Console.WriteLine("example_find_all_devices, please input 31");
        Console.WriteLine("example_get_WifiDAQ_status, please input 32");
        Console.WriteLine("");
        Console.WriteLine("Input number:  ");

        int input_number = Convert.ToInt32(Console.ReadLine());

        switch (input_number)
        {
            case 10:
                Console.WriteLine("Get 10, run example_AI_N_samples_once.cs");
                Console.WriteLine("");
                example_AI_N_samples_once.Main();
                break;
            case 11:
                Console.WriteLine("Get 11, run example_AI_on_demand_in_loop.cs");
                Console.WriteLine("");
                example_AI_on_demand_in_loop.Main();
                break;
            case 12:
                Console.WriteLine("Get 12, run example_AI_on_demand_once.cs");
                Console.WriteLine("");
                example_AI_on_demand_once.Main();
                break;
            case 13:
                Console.WriteLine("Get 13, run example_AI_continuous.cs");
                Console.WriteLine("");
                example_AI_continuous.Main();
                break;
            case 14:
                Console.WriteLine("Get 14, run example_AI_N_samples_in_loop.cs");
                Console.WriteLine("");
                example_AI_N_samples_in_loop.Main();
                break;
                 
                
            case 20:
                Console.WriteLine("Get 20, run example_DIO_loopback_pins.cs");
                Console.WriteLine("");
                example_DIO_loopback_pins.Main();
                break;
            case 21:
                Console.WriteLine("Get 21, run example_DIO_loopback_port.cs");
                Console.WriteLine("");
                example_DIO_loopback_port.Main();
                break;

            case 22:
                Console.WriteLine("Get 22, run example_DO_blinky_pins.cs");
                Console.WriteLine("");
                example_DO_blinky_pins.Main();
                break;

            case 23:
                Console.WriteLine("Get 23, run example_DO_blinky_port.cs");
                Console.WriteLine("");
                example_DO_blinky_port.Main();
                break;

            case 30:
                Console.WriteLine("Get 30, run example_get_device_info.cs");
                Console.WriteLine("");
                example_get_device_info.Main();
                break;
            case 31:
                Console.WriteLine("Get 31, run example_find_all_devices.cs");
                Console.WriteLine("");
                example_find_all_devices.Main();
                break;
            case 32:
                Console.WriteLine("Get 32, run example_get_WifiDAQ_status.cs");
                Console.WriteLine("");
                example_get_WifiDAQ_status.Main();
                break;

            default:
                Console.WriteLine("Wrong index, we will use example_find_all_devices example code");
                Console.WriteLine("");
                example_find_all_devices.Main();
                break;
        }
  
    }
}

