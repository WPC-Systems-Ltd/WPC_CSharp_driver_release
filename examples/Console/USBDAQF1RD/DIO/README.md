# DIO
> **Note**
> Make sure you are connected to correct IP or serial number.

## Overview

This example project demonstrates how to use WPC CSharp driver to control DIO with whole port or assign pins

In order to use API correctly, please refer to the [documentation](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/).

If you'd like to create your own application, start by using this simple template, and then include your own code.

## How to use this example

### Hardware Requirement

In order to run this example, you should get USBDAQF1RD product, which contains DIO function.

Then, we take `USBDAQF1RD` for example.

### USBDAQF1RD

<img src="https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/Reference/Pinouts/pinout-USBDAQF1RD.JPG" alt="drawing" width="600"/>

## DIO interfacing SOP

# DO
Create device handle -> Connect to device -> Open DO port / pins -> Write high or low to digital output -> Close DO port / pins -> Disconnect device -> Release device handle.

# DI
Create device handle -> Connect to device -> Open DI port / pins -> Read high or low from digital input -> Close DI port / pins -> Disconnect device -> Release device handle.

If function return value is 0, it represents communication with `USBDAQF1RD` successfully.

## Troubleshooting

For any technical support, please register new [issue](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/issues) on GitHub.