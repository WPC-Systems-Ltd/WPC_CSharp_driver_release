using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class WPC_example_code
{
    #region ProductMessage
    static void showProductMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("DeviceFinder, please input 0");
        Console.WriteLine("Emotion, please input 1");
        Console.WriteLine("EthanA, please input 2");
        Console.WriteLine("EthanD, please input 3");
        Console.WriteLine("EthanL, please input 4");
        Console.WriteLine("EthanO, please input 5");
        Console.WriteLine("USBDAQF1AD, please input 6");
        Console.WriteLine("USBDAQF1AOD, please input 7");
        Console.WriteLine("USBDAQF1CD, please input 8");
        Console.WriteLine("USBDAQF1D, please input 9");
        Console.WriteLine("USBDAQF1DSNK, please input 10");
        Console.WriteLine("USBDAQF1RD, please input 11");
        Console.WriteLine("USBDAQF1TD, please input 12");
        Console.WriteLine("WifiDAQE3A, please input 13");
        Console.WriteLine("STEM, please input 14");
    }

    #endregion

    #region DeviceFinderMessage
    static void showDeviceFinderMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For Find_devices");
        Console.WriteLine("----------------");
        Console.WriteLine("Run find_all_devices.cs, please input 0");
    }
    #endregion

    #region SysETHMessage
    static void showSysETHMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For System_ETH");
        Console.WriteLine("----------------");
        Console.WriteLine("Run get_network_info.cs, please input 1");
    }
    #endregion

    #region SysUSBMessage
    static void showSysUSBMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For System_USB");
        Console.WriteLine("----------------");
        Console.WriteLine("Run get_USB_info.cs, please input 2");
    }
    #endregion

    #region SysWifiMessage
    static void showSysWifiMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For System_Wifi");
        Console.WriteLine("----------------");
        Console.WriteLine("Run get_network_info.cs, please input 1");
        Console.WriteLine("Run get_Wifi_DAQ_status.cs, please input 3");
    }
    #endregion

    #region AIMessage
    static void showAIMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For AI");
        Console.WriteLine("------");
        Console.WriteLine("Run AI_continuous_with_logger.cs, please input 9");
        Console.WriteLine("Run AI_continuous.cs, please input 10");
        Console.WriteLine("Run AI_N_samples_in_loop.cs, please input 11");
        Console.WriteLine("Run AI_N_samples_once.cs, please input 12");
        Console.WriteLine("Run AI_on_demand_in_loop.cs, please input 13");
        Console.WriteLine("Run AI_on_demand_once.cs, please input 14");
    }
    #endregion

    #region AIOMessage
    static void showAIOMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For AIO");
        Console.WriteLine("-------");
        Console.WriteLine("Run AIO_all_channels_loopback.cs, please input 15");
        Console.WriteLine("Run AIO_one_channel_loopback.cs, please input 16");
    }
    #endregion

    #region AOMessage
    static void showAOMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For AO");
        Console.WriteLine("-------");
        Console.WriteLine("Run AO_write_all_channels.cs, please input 17");
        Console.WriteLine("Run AO_write_one_channel.cs, please input 18");
        Console.WriteLine("Run AO_waveform_generation.cs, please input 19");
    }
    #endregion

    #region DIOMessage
    static void showDIOMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For DIO");
        Console.WriteLine("-------");
        Console.WriteLine("Run DIO_loopback_pins.cs, please input 20");
        Console.WriteLine("Run DIO_loopback_port.cs, please input 21");
        Console.WriteLine("Run DO_blinky_pins.cs, please input 22");
        Console.WriteLine("Run DO_blinky_port.cs, please input 23");
    }
    #endregion

    #region I2CMessage
    static void showI2CMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For I2C");
        Console.WriteLine("-------");
        Console.WriteLine("Run I2C_write_read.cs, please input 30");
    }
    #endregion

    #region SPIMessage
    static void showSPIMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For SPI");
        Console.WriteLine("-------");
        Console.WriteLine("Run SPI_read_and_write.cs, please input 40");
        Console.WriteLine("Run SPI_write.cs, please input 41");
    }
    #endregion

    #region UARTMessage
    static void showUARTMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For UART");
        Console.WriteLine("--------");
        Console.WriteLine("Run UART_read.cs, please input 50");
        Console.WriteLine("Run UART_write.cs, please input 51");
    }
    #endregion

    #region CANMessage
    static void showCANMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For CAN");
        Console.WriteLine("-------");
        Console.WriteLine("Run CAN_read.cs, please input 60");
        Console.WriteLine("Run CAN_write.cs, please input 61");
    }
    #endregion

    #region RTDMessage
    static void showRTDMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For RTD");
        Console.WriteLine("-------");
        Console.WriteLine("Run RTD_read_channel_data.cs, please input 70");
        Console.WriteLine("Run RTD_read_channel_status.cs, please input 71");
        Console.WriteLine("Run RTD_read_channel_data_with_logger.cs, please input 72");
    }
    #endregion

    #region TCMessage
    static void showTCMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For ThermoCouple");
        Console.WriteLine("----------------");
        Console.WriteLine("Run TC_read_channel_data.cs, please input 80");
        Console.WriteLine("Run TC_read_channel_status.cs, please input 81");
        Console.WriteLine("Run TC_read_channel_data_with_logger.cs, please input 82");
    }
    #endregion

    #region MotionMessage
    static void showMotionMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For motion");
        Console.WriteLine("----------------");
        Console.WriteLine("Run Motion_1axis_move.cs, please input 90");
        Console.WriteLine("Run Motion_1axis_move_with_alarm_in.cs, please input 91");
        Console.WriteLine("Run Motion_1axis_move_with_breakpoint.cs, please input 92");
        Console.WriteLine("Run Motion_1axis_move_with_capture.cs, please input 93");
        Console.WriteLine("Run Motion_1axis_move_with_configuration_file.cs, please input 94");
        Console.WriteLine("Run Motion_1axis_move_with_inposition.cs, please input 95");
        Console.WriteLine("Run Motion_1axis_move_with_S_curve_acceleration.cs, please input 96");
        Console.WriteLine("Run Motion_2axis_circular_interpolation.cs, please input 97");
        Console.WriteLine("Run Motion_2axis_linear_interpolation.cs, please input 98");
        Console.WriteLine("Run Motion_3axis_asynchronous_move.cs, please input 99");
        Console.WriteLine("Run Motion_3axis_helical_interpolation.cs, please input 100");
        Console.WriteLine("Run Motion_3axis_linear_interpolation.cs, please input 101");
        Console.WriteLine("Run Motion_3axis_synchronous_move.cs, please input 102");
        Console.WriteLine("Run Motion_find_home.cs, please input 103");
        Console.WriteLine("Run Motion_find_index.cs, please input 104");
        Console.WriteLine("Run Motion_find_limit.cs, please input 105");
        Console.WriteLine("Run Motion_get_logical_position.cs, please input 106");
        Console.WriteLine("Run Motion_load_configuration_file.cs, please input 107");
        Console.WriteLine("Run Motion_save_configuration_file.cs, please input 108");
        Console.WriteLine("Run Motion_velocity_blending.cs, please input 109");
        Console.WriteLine("Run Motion_velocity_blending_accerleration.cs, please input 110");
        Console.WriteLine("Run Motion_position_blending.cs, please input 111");
        Console.WriteLine("Run Motion_servo_on.cs, please input 112");
    }
    #endregion

    #region RelayMessage
    static void showRelayMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("For Relay");
        Console.WriteLine("----------------");
        Console.WriteLine("Run Relay_read_counters.cs, please input 200");
        Console.WriteLine("Run Relay_set_channel.cs, please input 201");
    }
    #endregion


    static void Main()
    {
        Console.WriteLine("Welcome to WPC C# driver example code.");
        Console.WriteLine("Please select the product you have");

        showProductMessage();

        Console.WriteLine("");
        Console.WriteLine("Product code: ");

        int product_code = Convert.ToInt32(Console.ReadLine());
        switch (product_code)
        {
            case 0:
                Console.WriteLine("Get 0, show DeviceFinder series example code");
                showDeviceFinderMessage();
                break;

            case 1:
                Console.WriteLine("Get 1, show Emotion series example code");
                showSysETHMessage();
                showMotionMessage();
                break;

            case 2:
                Console.WriteLine("Get 2, show EthanA series example code");
                showSysETHMessage();
                showAIMessage();
                break;

            case 3:
                Console.WriteLine("Get 3, show EthanD series example code");
                showSysETHMessage();
                showDIOMessage();
                break;

            case 4:
                Console.WriteLine("Get 4, show EthanL series example code");
                showSysETHMessage();
                showRelayMessage();
                break;

            case 5:
                Console.WriteLine("Get 5, show EthanO series example code");
                showSysETHMessage();
                showAOMessage();
                break;

            case 6:
                Console.WriteLine("Get 6, show USBDAQF1AD series example code");
                showSysUSBMessage();
                showAIMessage();
                showDIOMessage();
                showI2CMessage();
                showSPIMessage();
                showUARTMessage();
                break;

            case 7:
                Console.WriteLine("Get 7, show USBDAQF1AOD series example code");
                showSysUSBMessage();
                showAIMessage();
                showAIOMessage();
                showAOMessage();
                showDIOMessage();
                showI2CMessage();
                showUARTMessage();
                break;

            case 8:
                Console.WriteLine("Get 8, show USBDAQF1CD series example code");
                showSysUSBMessage();
                showDIOMessage();
                showI2CMessage();
                showSPIMessage();
                showUARTMessage();
                showCANMessage();
                break;

            case 9:
                Console.WriteLine("Get 9, show USBDAQF1D series example code");
                showSysUSBMessage();
                showDIOMessage();
                showI2CMessage();
                showSPIMessage();
                showUARTMessage();
                break;

            case 10:
                Console.WriteLine("Get 10, show USBDAQF1DSNK series example code");
                showSysUSBMessage();
                showDIOMessage();
                break;

            case 11:
                Console.WriteLine("Get 1, show USBDAQF1RD series example code");
                showSysUSBMessage();
                showDIOMessage();
                showI2CMessage();
                showSPIMessage();
                showUARTMessage();
                showRTDMessage();
                break;

            case 12:
                Console.WriteLine("Get 12, show USBDAQF1TD series example code");
                showSysUSBMessage();
                showDIOMessage();
                showI2CMessage();
                showSPIMessage();
                showUARTMessage();
                showTCMessage();
                break;

            case 13:
                Console.WriteLine("Get 13, show WifiDAQE3A series example code");
                showSysWifiMessage();
                showAIMessage();
                break;

            case 14:
                Console.WriteLine("Get 14, show STEM series example code");
                showSysETHMessage();
                showAIMessage();
                showAIOMessage();
                showAOMessage();
                showDIOMessage();
                break;

            default:
                break;
        }
        Console.WriteLine("===================================================");
        Console.WriteLine("example code: ");
        int example_code = Convert.ToInt32(Console.ReadLine());

        switch (product_code)
        {
            case 0:
                switch (example_code)
                {
                    #region DeviceFinder
                    case 0:
                        DeviceFinder_find_all_devices.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 1:
                switch (example_code)
                {
                    #region Emotion_Sys
                    case 1:
                        EMotion_get_network_info.Main();
                        break;
                    #endregion

                    #region Emotion_motion
                    case 90:
                        EMotion_1axis_move.Main();
                        break;
                    case 91:
                        EMotion_1axis_move_with_alarm_in.Main();
                        break;
                    case 92:
                        EMotion_1axis_move_with_breakpoint.Main();
                        break;
                    case 93:
                        EMotion_1axis_move_with_capture.Main();
                        break;
                    case 94:
                        EMotion_1axis_move_with_configuration_file.Main();
                        break;
                    case 95:
                        EMotion_1axis_move_with_inposition.Main();
                        break;
                    case 96:
                        EMotion_1axis_move_with_S_curve_acceleration.Main();
                        break;
                    case 97:
                        EMotion_2axis_circular_interpolation.Main();
                        break;
                    case 98:
                        EMotion_2axis_linear_interpolation.Main();
                        break;
                    case 99:
                        EMotion_3axis_asynchronous_move.Main();
                        break;
                    case 100:
                        EMotion_3axis_helical_interpolation.Main();
                        break;
                    case 101:
                        EMotion_3axis_linear_interpolation.Main();
                        break;
                    case 102:
                        EMotion_3axis_synchronous_move.Main();
                        break;
                    case 103:
                        EMotion_find_home.Main();
                        break;
                    case 104:
                        EMotion_find_index.Main();
                        break;
                    case 105:
                        EMotion_find_limit.Main();
                        break;
                    case 106:
                        EMotion_get_logical_position.Main();
                        break;
                    case 107:
                        EMotion_load_configuration_file.Main();
                        break;
                    case 108:
                        EMotion_save_configuration_file.Main();
                        break;
                    case 109:
                        EMotion_velocity_blending.Main();
                        break;
                    case 110:
                        EMotion_velocity_blending_accerleration.Main();
                        break;
                    case 111:
                        EMotion_position_blending.Main();
                        break;
                    case 112:
                        EMotion_servo_on.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 2:
                switch (example_code)
                {
                    #region EthanA_Sys
                    case 1:
                        EthanA_get_network_info.Main();
                        break;
                    #endregion

                    #region EthanA_AI
                    case 9:
                        EthanA_DataLogger_AI_continuous.Main();
                        break;
                    case 10:
                        EthanA_AI_continuous.Main();
                        break;
                    case 11:
                        EthanA_AI_N_samples_in_loop.Main();
                        break;
                    case 12:
                        EthanA_AI_N_samples_once.Main();
                        break;
                    case 13:
                        EthanA_AI_on_demand_in_loop.Main();
                        break;
                    case 14:
                        EthanA_AI_on_demand_once.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 3:
                switch (example_code)
                {
                    #region EthanD_Sys
                    case 1:
                        EthanD_get_network_info.Main();
                        break;
                    #endregion

                    #region EthanD_DIO
                    case 20:
                        EthanD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        EthanD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        EthanD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        EthanD_DO_blinky_port.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 4:
                switch (example_code)
                {
                    #region EthanL_Sys
                    case 1:
                        EthanL_get_network_info.Main();
                        break;
                    #endregion

                    #region EthanL_Relay
                    case 200:
                        EthanL_Relay_read_counters.Main();
                        break;
                    case 201:
                        EthanL_Relay_set_channel.Main();
                        break;

                    #endregion
                    default:
                        break;
                }
                break;

            case 5:
                switch (example_code)
                {
                    #region EthanO_Sys
                    case 1:
                        EthanO_get_network_info.Main();
                        break;
                    #endregion

                    #region EthanO_AO
                    case 17:
                        EthanO_AO_write_all_channels.Main();
                        break;
                    case 18:
                        EthanO_AO_write_one_channel.Main();
                        break;
                    case 19:
                        EthanO_AO_waveform_generation.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 6:
                switch (example_code)
                {
                    #region USBDAQF1AD_Sys
                    case 2:
                        USBDAQF1AD_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1AD_AI
                    case 9:
                        USBDAQF1AD_DataLogger_AI_continuous.Main();
                        break;
                    case 10:
                        USBDAQF1AD_AI_continuous.Main();
                        break;
                    case 11:
                        USBDAQF1AD_AI_N_samples_in_loop.Main();
                        break;
                    case 12:
                        USBDAQF1AD_AI_N_samples_once.Main();
                        break;
                    case 13:
                        USBDAQF1AD_AI_on_demand_in_loop.Main();
                        break;
                    case 14:
                        USBDAQF1AD_AI_on_demand_once.Main();
                        break;
                    #endregion

                    #region USBDAQF1AD_DIO
                    case 20:
                        USBDAQF1AD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1AD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1AD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1AD_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1AD_I2C
                    case 30:
                        USBDAQF1AD_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1AD_SPI
                    case 40:
                        USBDAQF1AD_SPI_read_and_write.Main();
                        break;
                    case 41:
                        USBDAQF1AD_SPI_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1AD_UART
                    case 50:
                        USBDAQF1AD_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1AD_UART_write.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 7:
                switch (example_code)
                {
                    #region USBDAQF1AOD_Sys
                    case 2:
                        USBDAQF1AOD_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_AI
                    case 9:
                        USBDAQF1AOD_DataLogger_AI_continuous.Main();
                        break;
                    case 10:
                        USBDAQF1AOD_AI_continuous.Main();
                        break;
                    case 11:
                        USBDAQF1AOD_AI_N_samples_in_loop.Main();
                        break;
                    case 12:
                        USBDAQF1AOD_AI_N_samples_once.Main();
                        break;
                    case 13:
                        USBDAQF1AOD_AI_on_demand_in_loop.Main();
                        break;
                    case 14:
                        USBDAQF1AOD_AI_on_demand_once.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_AIO
                    case 15:
                        USBDAQF1AOD_AIO_all_channels_loopback.Main();
                        break;
                    case 16:
                        USBDAQF1AOD_AIO_one_channel_loopback.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_AO
                    case 17:
                        USBDAQF1AOD_AO_write_all_channels.Main();
                        break;
                    case 18:
                        USBDAQF1AOD_AO_write_one_channel.Main();
                        break;
                    case 19:
                        USBDAQF1AOD_AO_waveform_generation.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_DIO
                    case 20:
                        USBDAQF1AOD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1AOD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1AOD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1AOD_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_I2C
                    case 30:
                        USBDAQF1AOD_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1AOD_UART
                    case 50:
                        USBDAQF1AOD_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1AOD_UART_write.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 8:
                switch (example_code)
                {
                    #region USBDAQF1CD_Sys
                    case 2:
                        USBDAQF1CD_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1CD_DIO
                    case 20:
                        USBDAQF1CD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1CD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1CD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1CD_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1CD_I2C
                    case 30:
                        USBDAQF1CD_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1CD_SPI
                    case 40:
                        USBDAQF1CD_SPI_read_and_write.Main();
                        break;
                    case 41:
                        USBDAQF1CD_SPI_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1CD_UART
                    case 50:
                        USBDAQF1CD_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1CD_UART_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1CD_CAN
                    case 60:
                        USBDAQF1CD_CAN_read.Main();
                        break;
                    case 61:
                        USBDAQF1CD_CAN_write.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 9:
                switch (example_code)
                {
                    #region USBDAQF1D_Sys
                    case 2:
                        USBDAQF1D_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1D_DIO
                    case 20:
                        USBDAQF1D_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1D_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1D_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1D_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1D_I2C
                    case 30:
                        USBDAQF1D_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1D_SPI
                    case 40:
                        USBDAQF1D_SPI_read_and_write.Main();
                        break;
                    case 41:
                        USBDAQF1D_SPI_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1D_UART
                    case 50:
                        USBDAQF1D_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1D_UART_write.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 10:
                switch (example_code)
                {
                    #region USBDAQF1DSNK_Sys
                    case 2:
                        USBDAQF1DSNK_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1DSNK_DIO
                    case 20:
                        USBDAQF1DSNK_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1DSNK_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1DSNK_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1DSNK_DO_blinky_port.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 11:
                switch (example_code)
                {
                    #region USBDAQF1RD_Sys
                    case 2:
                        USBDAQF1RD_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1RD_DIO
                    case 20:
                        USBDAQF1RD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1RD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1RD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1RD_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1RD_I2C
                    case 30:
                        USBDAQF1RD_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1RD_SPI
                    case 40:
                        USBDAQF1RD_SPI_read_and_write.Main();
                        break;
                    case 41:
                        USBDAQF1RD_SPI_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1RD_UART
                    case 50:
                        USBDAQF1RD_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1RD_UART_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1RD_RTD
                    case 70:
                        USBDAQF1RD_RTD_read_channel_data.Main();
                        break;
                    case 71:
                        USBDAQF1RD_RTD_read_channel_status.Main();
                        break;
                    case 72:
                        USBDAQF1RD_RTD_read_channel_data_with_logger.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 12:
                switch (example_code)
                {
                    #region USBDAQF1TD_Sys
                    case 2:
                        USBDAQF1TD_get_USB_info.Main();
                        break;
                    #endregion

                    #region USBDAQF1TD_DIO
                    case 20:
                        USBDAQF1TD_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        USBDAQF1TD_DIO_loopback_port.Main();
                        break;
                    case 22:
                        USBDAQF1TD_DO_blinky_pins.Main();
                        break;
                    case 23:
                        USBDAQF1TD_DO_blinky_port.Main();
                        break;
                    #endregion

                    #region USBDAQF1TD_I2C
                    case 30:
                        USBDAQF1TD_I2C_write_read.Main();
                        break;
                    #endregion

                    #region USBDAQF1TD_SPI
                    case 40:
                        USBDAQF1TD_SPI_read_and_write.Main();
                        break;
                    case 41:
                        USBDAQF1TD_SPI_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1TD_UART
                    case 50:
                        USBDAQF1TD_UART_read.Main();
                        break;
                    case 51:
                        USBDAQF1TD_UART_write.Main();
                        break;
                    #endregion

                    #region USBDAQF1TD_TC
                    case 80:
                        USBDAQF1TD_TC_read_channel_data.Main();
                        break;
                    case 81:
                        USBDAQF1TD_TC_read_channel_status.Main();
                        break;
                    case 82:
                        USBDAQF1TD_TC_read_channel_data_with_logger.Main();
                        break;

                    #endregion

                    default:
                        break;
                }
                break;

            case 13:
                switch (example_code)
                {
                    #region WifiDAQE3A_Sys
                    case 1:
                        WifiDAQE3A_get_network_info.Main();
                        break;
                    case 3:
                        WifiDAQE3A_get_WifiDAQ_status.Main();
                        break;
                    #endregion

                    #region WifiDAQE3A_AI
                    case 9:
                        WifiDAQE3A_DataLogger_AI_continuous.Main();
                        break;
                    case 10:
                        WifiDAQE3A_AI_continuous.Main();
                        break;
                    case 11:
                        WifiDAQE3A_AI_N_samples_in_loop.Main();
                        break;
                    case 12:
                        WifiDAQE3A_AI_N_samples_once.Main();
                        break;
                    case 13:
                        WifiDAQE3A_AI_on_demand_in_loop.Main();
                        break;
                    case 14:
                        WifiDAQE3A_AI_on_demand_once.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            case 14:
                switch (example_code)
                {
                    #region STEM_Sys
                    case 1:
                        STEM_get_network_info.Main();
                        break;
                    #endregion


                    #region STEM_AI
                    case 9:
                        STEM_DataLogger_AI_continuous.Main();
                        break;
                    case 10:
                        STEM_AI_continuous.Main();
                        break;
                    case 11:
                        STEM_AI_N_samples_in_loop.Main();
                        break;
                    case 12:
                        STEM_AI_N_samples_once.Main();
                        break;
                    case 13:
                        STEM_AI_on_demand_in_loop.Main();
                        break;
                    case 14:
                        STEM_AI_on_demand_once.Main();
                        break;
                    #endregion

                    #region STEM_AIO
                    case 15:
                        STEM_AIO_all_channels_loopback.Main();
                        break;
                    case 16:
                        STEM_AIO_one_channel_loopback.Main();
                        break;
                    #endregion

                    #region STEM_AO
                    case 17:
                        STEM_AO_write_all_channels.Main();
                        break;
                    case 18:
                        STEM_AO_write_one_channel.Main();
                        break;
                    case 19:
                        STEM_AO_waveform_generation.Main();
                        break;
                    #endregion

                    #region STEM_DIO
                    case 20:
                        STEM_DIO_loopback_pins.Main();
                        break;
                    case 21:
                        STEM_DIO_loopback_port.Main();
                        break;
                    case 22:
                        STEM_DO_blinky_pins.Main();
                        break;
                    case 23:
                        STEM_DO_blinky_port.Main();
                        break;
                    #endregion

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }
}
