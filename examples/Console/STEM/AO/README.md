# AO
> **Note**
> Make sure you are connected to correct IP or serial number.

## Overview

This example project demonstrates how to use WPC CSharp driver to output digital signal.

In order to use API correctly, please refer to the [documentation](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/).

If you'd like to create your own application, start by using this simple template, and then include your own code.

## How to use this example

### AO output range

| Product         |AO range    |
|:---------------:|:----------:|
| USB-DAQ-F1-AOD  | 0 ~ 5 V    |
| Ethan-O         | -10 ~ 10 V |
| STEM            | -10 ~ 10 V |

### Hardware Requirement

In order to run this example, you should get STEM product, which contains AO function.

Then, we take `STEM` for example.

### STEM

<img src="https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/Reference/Pinouts/pinout-STEM.JPG" alt="drawing" width="600"/>

## AO interfacing SOP

Create device handle -> Connect to device -> Open AO port -> Write AO signal -> Close AO port -> Disconnect device -> Release device handle.

If function return value is 0, it represents communication with `STEM` successfully.

## Troubleshooting

For any technical support, please register new [issue](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/issues) on GitHub.