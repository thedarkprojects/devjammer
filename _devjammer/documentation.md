---
title: Documentation
category: Main Menu
order: 2
tags: [Cronux, DevJammer, Thecarisma, Windows]
years: 2019
tile-header: front_image.png
tile: front_image.png
links:
  
---

## List Device

To view all the devices present in the system, the `list` command is issued 
to the program. 

```batch
devjammer list
```

All the devices listed might not be a total complete list because the devices are fetched 
from the windows [`CIM_LogicalDevice`](https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/cim-logicaldevice) 
group. 

To list a particular device information the caption name or part of the caption name or instance 
id can be issued as the second parameter. Ensure the second parameter is in quote if it more than 
one word.

```
devjammer list "CPU"
```

The command above list all the devices whose name or instance id contains the text **CPU**. 
similar result below ...

<code>
Name: Intel(R) Core(TM) i7-8550U CPU @ 1.80GHz, Status: OK, Instance Path: ...<br />
Name: Intel(R) Core(TM) i7-8550U CPU @ 1.80GHz, Status: OK, Instance Path: ...<br />
Name: Intel(R) Core(TM) i7-8550U CPU @ 1.80GHz, Status: OK, Instance Path: ...<br />
Name: Intel(R) Core(TM) i7-8550U CPU @ 1.80GHz, Status: OK, Instance Path: ACPI\GE<br />
</code>

## Enable A Device

The command `enable` can be used to enable an idle or disabled device. The operation will fail if 
devjammer is executed in admin environment.

The second argument which is the instance id or part of instance id/name of the devices must be 
supplied.

```
devjammer enable Bluetooth
```

The command above will enable all devices related to the bluetooth.

## Disable A Device

The command `disable` can be used to disable an already enabled device. The operation will fail if 
devjammer is executed in admin environment.

The second argument which is the instance id or part of instance id/name of the devices must be 
supplied.

```
devjammer disable Bluetooth
```

The command above will disable all devices related to the bluetooth.

## Jaming A Device

In a situation where there is a ransom ware holding a device or USB port captive or a device access control 
program is preventing you from using a USB port, the `jam` and `xjam` command can be used to gain access to the 
device till the device is disconnected. 

### Jam

The `jam` command uses a simple brute force method that keep enabling the device till the system deny access 
to any program trying to disable or enable the device. In this mode the device keep enabling and disbling continously 
util around the 14th time when it become stable for use for the session. 

The drawback of this method is that it is a bit slow, the windows notification will keep comming up which is annoying. 
A simple example below... 

```
devjammer jam "Mass Storage Device"
```

### XJam

The `xjam` command is a better alternative to the jam command as it is very fast and neat, the method used is to 
enable the device and open an invisible command prompt that navigate to the device folder. 

The drawback of this method is that it currently just work for USB Drive devices (tested), it open an invisible 
command prompt that is kept alive till the USB is manually disconnected. This method was proposed by [shegzee](https://github.com/shegzee). 
A simple example below...

```
devjammer xjam "Mass Storage Device"
```

The command above recover the drive from any device controll program in less than a minute.

## Help

To view all available command from the command line, use the `help` command

```
devjammer help
```

---
<ul>
{% for link in page.links %}
  <li>{{ link | markdownify | remove: "<p>" | remove: "</p>" }}</li>
{% endfor %}
</ul>
