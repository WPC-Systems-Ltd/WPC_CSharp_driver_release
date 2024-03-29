# Temperature-TC
> **Note**
> Make sure you are connected to correct IP or serial number.

## Overview

This example project demonstrates how to use WPC CSharp driver to read thermocouple sensor temperature in Celcius.

In order to use API correctly, please refer to the [documentation](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/).

If you'd like to create your own application, start by using this simple template, and then include your own code.

## How to use this example

### Hardware Requirement

In order to run this example, you should get `USBDAQF1TD` product, which contains thermocouple sensor and its function.

Then, we take `USBDAQF1TD` for example.

### USBDAQF1TD

<img src="https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/Reference/Pinouts/pinout-USBDAQF1TD.JPG" alt="drawing" width="600"/>

## Thermocouple interfacing SOP

Create device handle -> Connect to device -> Open Thermal port -> Set Thermal parameters ->  Read thermocouple data -> Close Thermal port -> Disconnect device -> Release device handle.

If function return value is 0, it represents communication with `USBDAQF1TD` successfully.

## Troubleshooting

For any technical support, please register new [issue](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/issues) on GitHub.