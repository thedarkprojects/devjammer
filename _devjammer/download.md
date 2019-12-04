---
title: Download
category: Main Menu
order: 1
tags: [Download, Windows, x86, x64, Cronux, DevJammer, Thecarisma]
years: 2019
tile-header: tile-header640w@2x.jpg
tile: tile400w@2x.jpg
links:
  
---

{% assign version = 1.0 %}

## System Requirements

Windows Vista or later, Ensure you download the executable according to your system 
architecture. Download x86 version for a 32 bits system and x64 version for a 64 bits 
system.

## Tip

To ensure the folder of devjammer is added to the environment path download the installer 
which also comes with an uninstaller to remove the application from your system and from 
environment path. Download the portable version for a quick run e.g. to execute from a shared 
network folder or from a USB drive. 

## Latest Version {{ version }}

#### Installer

 - [devjammer-{{ version }}-x64.exe](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-{{ version }}-x64.exe)
 - [devjammer-{{ version }}-x86.exe](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-{{ version }}-x86.exe)
 
#### Portable

 - [devjammer-x64.exe](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-x64.exe)
 - [devjammer-x86.exe](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-x86.exe)
 
#### Archive

 - [devjammer-{{ version }}-x64.zip](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-{{ version }}-x64.zip)
 - [devjammer-{{ version }}-x86.zip](https://github.com/thedarkprojects/devjammer/releases/download/{{ version }}/devjammer-{{ version }}-x86.zip)

## Other versions 

[To download other version visit the github release page here](https://github.com/thedarkprojects/devjammer/releases)

___
<ul>
{% for link in page.links %}
  <li>{{ link | markdownify | remove: "<p>" | remove: "</p>" }}</li>
{% endfor %}
</ul>
