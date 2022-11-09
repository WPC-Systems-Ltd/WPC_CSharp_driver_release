# Overview

**WPC CSharp driver** contains APIs for interacting with basically WPC DAQ cards or any other WPC USB, WiFi and Ethernet based devices.  

Some API functions in the package may not compatible with earlier versions of WPC DAQ firmware. To update device firmware to the latest version, please use WPC Device Manager and [LabVIEW Run-time engine](https://drive.google.com/file/d/1Uj6r65KhNxvuApiqrMkZp-NWyq-Eek-k/view).
You can download WPC Device Manager by visiting [WPC Systems Ltd. official website](http://www.wpc.com.tw/36039260092584721462-daq1.html).

> **Note**  
> Make sure the latest version of firmware is up to date with your products. 
 

|                   |                 Link                                                            |
|:------------------|:--------------------------------------------------------------------------------|
| WPC official site | http://www.wpc.com.tw/                                                          |
| GitHub			| https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release                                                       |
| User guide        | https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/                    |
| Example code      | https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/tree/main/examples |

 
## Products

Ethernet based DAQ card
- Ethan-A
- Ethan-D

USB interface DAQ card
- USB-DAQ-F1-D (Digital)
- USB-DAQ-F1-DSNK (24V Digital)
- USB-DAQ-F1-AD (Digital + AI)
- USB-DAQ-F1-TD (Digital + Thermocouple)
- USB-DAQ-F1-RD (Digital + RTD)
- USB-DAQ-F1-CD (Digital + CAN)
- USB-DAQ-F1-AOD (Digital + AI + AO)

Wifi based DAQ card
- Wifi-DAQ-E3-A

## I/O Function Table

| Model           | AI  | AO | DI         | DO         | CAN | UART | SPI | I2C  | RTD | Thermocouple |
|:----------------|:---:|:--:|:----------:|:----------:|:---:|:----:|:---:|:----:|:---:|:------------:|
| Ethan-A         | 0   | -  | -          | -          |-    |-     |-    |-     | -   |-             |
| Ethan-D         | -   | -  | 1          | 0          |-    |-     |-    |-     | -   |-             |
| USB-DAQ-F1-D    | -   | -  | 0, 1, 2, 3 | 0, 1, 2, 3 |-    |1, 2  |1, 2 | 1, 2 | -   |-             |
| USB-DAQ-F1-DSNK | -   | -  | 0, 1       | 2, 3       |-    |-     |-    |-     | -   |-             |
| USB-DAQ-F1-AD   | 0   | -  | 0, 1, 2, 3 | 0, 1, 2, 3 |-    |1, 2  |2    | 1, 2 | -   |-             |
| USB-DAQ-F1-TD   | -   | -  | 0, 1, 2, 3 | 0, 1, 2, 3 |-    |1, 2  |2    | 1, 2 | -   |1             |
| USB-DAQ-F1-RD   | -   | -  | 0, 1, 2, 3 | 0, 1, 2, 3 |-    |1, 2  |2    | 1, 2 | 1   |-             |
| USB-DAQ-F1-CD   | -   | -  | 0, 1, 2, 3 | 0, 1, 2, 3 |1    |1, 2  |2    | 1, 2 | -   |-             |
| USB-DAQ-F1-AOD  | 0   | 0  | 0, 1, 2, 3 | 0, 1, 2, 3 |-    |1, 2  |-    | 1, 2 | -   |-             |
| Wifi-DAQ-E3-A   | 1   | -  | -          | -          |-    |-     |-    |-     | -   |-             |

Take `USB-DAQ-F1-AOD` for example:
- Port 0 is available for `AI`
- Port 2 is available for `DI`
- Ports 0 & 1 are available for `DO`
- Port 2 is available for `UART`

## License

**WPC CSharp driver release** is licensed under an MIT-style license see
[LICENSE](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/LICENSE). Other incorporated projects may be licensed under different licenses.
All licenses allow for non-commercial and commercial use.