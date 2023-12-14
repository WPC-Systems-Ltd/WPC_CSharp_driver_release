# AHRS
> **Note**
> Make sure you are connected to correct IP or serial number.

## Overview

This example project demonstrates how to use WPC CSharp driver to acquire AHRS data.

In order to use API correctly, please refer to the [documentation](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/).

If you'd like to create your own application, start by using this simple template, and then include your own code.

## How to use this example

### Hardware requirement

In order to run this example, you should get WifiDAQE3A product, which contains AHRS function.

Then, we take WifiDAQE3A for example.

### WifiDAQE3A

<img src="https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/mAHRSn/Reference/Pinouts/pinout-WifiDAQE3A.JPG" alt="drawing" width="600"/>

## AHRS interfacing SOP

Create device handle -> Connect to device -> Open AHRS port -> Read AHRS data -> Close AHRS port -> Disconnect device -> Release device handle.

If function return value is 0, it represents communication with `WifiDAQE3A` successfully.

## Troubleshooting

For any technical support, please register new [issue](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/issues) on GitHub.