

# <p style="text-align: center;" align="center"><img src="https://thedarkprojects.github.io/devjammer/favicon.png" alt="devjammer" style="width:180px;height:160px;" width="180" height="160" /><br /> devjammer</p>

<p style="text-align: center;" align="center">Regain device access if denied/disabled by other programs (esp. device control programs, ransomware)</p>

---

The name of the project is pronouced **divjammer** as in **device**.

DevJammer is a Windows utilities to enabled a device that was blocked by device control programs. It uses various method to keep the device enabled unitl it is manually disconnected from the system. Example if there is an anti-virus program in place that block access to USB port this util can be used to bypass it effects.

### Installation

To use the program you can choose to compile and run from 
Visual Studio. To install prebuilt binaries visit the download page. 

https://thedarkprojects.github.io/devjammer/download

Whe downloading the binary ensure you download the binary 
suitable for your system architecture i.e. download x86 for 
32 bits system and x64 version for 64 bits system.

### Usage 

The utility accept various commands 
```
devjammer [[help],[list],[enable],[disable],[jam]] "device name part"
devjammer list "WD Elements"

help      show this help message
list      list all devices
enable    enable a device that match the second argument
disable   disable a device that match the second argument
jam       keep enabling a device until system deny access(shady)
xjam      enable a device then keep the drive alive with cmd(shady)
```

## Contributing

Before you begin contribution please read the contribution guide at [CONTRIBUTING GUIDE](https://thedarkprojects.github.io/devjammer/contribute)

## License

MIT License Copyright (c) 2019 thedarkprojects

