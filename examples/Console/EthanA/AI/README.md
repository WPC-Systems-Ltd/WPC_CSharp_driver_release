# AI
> **Note**
> Make sure you are connected to correct IP or serial number.

## Overview

This example project demonstrates how to use WPC CSharp driver to acquire multi-channel AI data simultaneously.
Also, we show how to perform data acquisition in on-demand, N samples and continuous mode individually.

In order to use API correctly, please refer to the [documentation](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/).

If you'd like to create your own application, start by using this simple template, and then include your own code.

## How to use this example

### Channel count vs. sampling rate

| Product/module  |chan 0:0|chan 0:1|chan 0:2|chan 0:3|chan 0:4|chan 0:5|chan 0:6|chan 0:7|
|:---------------:|:------:|:------:|:------:|:------:|:------:|:------:|:------:|:------:|
| USB-DAQ-F1-AD   | 16k    | 8k     | 5.3k   | 4k     | 3.2k   | 2.6k   | 2.2k   | 2.0k   |
| USB-DAQ-F1-AOD  | 16k    | 8k     | 5.3k   | 4k     | 3.2k   | 2.6k   | 2.2k   | 2.0k   |
| Wifi-DAQ        | 10k    | 10k    | 10k    | 10k    | 10k    | 10k    | 10k    | 10k    |
| Ethan-A         | 20k    | 20k    | 20k    | 20k    | 20k    | 20k    | 20k    | 20k    |

### Chip select vs. sampling rate

| Product/module  |1 CS|2 CS|3 CS|
|:---------------:|:--:|:--:|:--:|
| STEM            |12k |6k  |4k  |

### Hardware requirement

In order to run this example, you should get EthanA product, which contains AI function.

Then, we take EthanA for example.

### EthanA

<img src="https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/Reference/Pinouts/pinout-EthanA.JPG" alt="drawing" width="600"/>

## AI interfacing SOP

Create device handle -> Connect to device -> Open AI port -> Read AI data -> Close AI port -> Disconnect device -> Release device handle.

If function return value is 0, it represents communication with `EthanA` successfully.

## Troubleshooting

For any technical support, please register new [issue](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/issues) on GitHub.