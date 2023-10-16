# Overview

Welcome to **WPC CSharp driver** API documentation. It is an easy-to-use open-source tool for beginners.

We provide excellent example codes to help you quickly get started with our products, connecting code to real-world usage. This makes it a great way to learn.

Therefore, we highly recommend using our driver because it is simple to use. Just open, read/write, and close - allowing you to access or update data with ease.

Adding WPC CSharp driver to your toolkit not only simplifies tasks but also provides a practical learning experience that bridges theory and real-world application.

Last but not least, it is a valuable resource for both learning and working efficiently.

[![nuget](https://img.shields.io/nuget/v/cswpc)](https://www.nuget.org/packages/cswpc)
[![Downloads](https://img.shields.io/nuget/dt/cswpc?color=%20)](https://www.nuget.org/packages/cswpc)
[![NET](https://img.shields.io/badge/.NET-6.0-blue.svg)](https://dotnet.microsoft.com/en-us/)
[![Documentation](https://img.shields.io/badge/docs-website-purple.svg)](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

> [!Note]
> Make sure the latest version of firmware is up to date with your products.

# Install toolkit

This can be done either manually or through the [NuGet package](https://www.nuget.org/packages/cswpc)

```
PM> NuGet\Install-Package cswpc
```

or with dotnet cli:

```
dotnet add package cswpc
```

# Quick Start

**Easy, fast, and just works!**

```csharp
using WPC.Product;

// Get C# driver name
Console.WriteLine($"{Constant.PKG_NAME}");

// Get C# driver full name and version
Console.WriteLine($"{Constant.PKG_FULL_NAME} - Version {Constant.VERSION}");

// Get C# handle list
foreach (var item in Constant.HANDLE_LIST)
{
  Console.WriteLine($"{item}");
}
```

# References
- [GitHub](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release)
- [Documentation - WPC CSharp driver](https://wpc-systems-ltd.github.io/WPC_CSharp_driver_release/)
- [How to install Visual Studio IDE](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/wiki/How-to-install-Visual-Studio-IDE)
- [How to start up a new project with cswpc package](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/wiki/How-to-start-up-a-new-project-with-cswpc-package)
- [How to build your own C# codes into EXE files](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/wiki/How-to-build-your-own-C%23-codes-into-EXE-files)

# License

**WPC CSharp driver** is licensed under an MIT-style license see
[LICENSE](https://github.com/WPC-Systems-Ltd/WPC_CSharp_driver_release/blob/main/LICENSE). Other incorporated projects may be licensed under different licenses.
All licenses allow for non-commercial and commercial use.
