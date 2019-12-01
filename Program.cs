using NativeSetupDiLib;
using System;
using System.Collections.Generic;
using System.Management;

namespace devjammer
{
    //see https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/cim-logicaldevice
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: You need to specify an argument ");
                Console.WriteLine(GetHelp());
                Environment.Exit(1);
            }
            String arg = args[0].ToLower();
            String extraParam = (args.Length > 1 ? args[1].ToLower() : "");
            if (arg.Equals("help"))
            {
                Console.WriteLine(GetHelp());
                Environment.Exit(0);
            }
            else if (arg.Equals("list"))
            {
                PrintAllLogicalDevice(extraParam);
                Environment.Exit(0);
            }
            else if (arg.Equals("enable") || arg.Equals("disable") || arg.Equals("jam"))
            {

            }
            else
            {
                Console.WriteLine("Error: invalid first argument ");
                Console.WriteLine(GetHelp());
                Environment.Exit(1);
            }
            var usbDevices = GetLogicalDevices();
            foreach (var usbDevice in usbDevices)
            {
                if (usbDevice.GetPropertyValue("DeviceID").ToString().ToLower().Contains(extraParam) ||
                    usbDevice.GetPropertyValue("Caption").ToString().ToLower().Contains(extraParam) ||
                    usbDevice.GetPropertyValue("Description").ToString().ToLower().Contains(extraParam) ||
                    usbDevice.GetPropertyValue("DeviceID").ToString().ToLower().Contains(extraParam))
                {
                    /**Console.WriteLine("Name: {0}, Status: {1}, Instance Path: {2}",
                       usbDevice.GetPropertyValue("Caption"),
                       usbDevice.GetPropertyValue("Status"),
                       usbDevice.GetPropertyValue("DeviceID"));

                    Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                    string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();**/

                    //while (true) DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);
                    //Console.WriteLine("Done");
                }
            }

            Console.Read();
        }

        static void PrintAllLogicalDevice(String extraParam)
        {
            var usbDevices = GetLogicalDevices();
            foreach (var usbDevice in usbDevices)
            {
                try
                {
                    if (usbDevice.GetPropertyValue("DeviceID").ToString().ToLower().Contains(extraParam) ||
                        usbDevice.GetPropertyValue("Caption").ToString().ToLower().Contains(extraParam) ||
                        usbDevice.GetPropertyValue("Description").ToString().ToLower().Contains(extraParam))
                    {
                        Console.WriteLine("Name: {0}, Status: {1}, Instance Path: {2}",
                           usbDevice.GetPropertyValue("Caption"),
                           usbDevice.GetPropertyValue("Status"),
                           usbDevice.GetPropertyValue("DeviceID"));
                    }
                } catch (Exception ex)
                {

                }
            }
        }

        static List<ManagementBaseObject> GetLogicalDevices()
        {
            List<ManagementBaseObject> devices = new List<ManagementBaseObject>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", @"Select * From CIM_LogicalDevice"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(device);
            }

            collection.Dispose();
            return devices;
        }

        static String GetHelp()
        {
            return "devjammer [[help],[list],[enable],[disable],[jam]] \"device part\"" +
                "\ndevjammer list \"WD Elements\"" +
                "\n" +
                "\nhelp      show this help message" +
                "\nlist      list all devices" +
                "\nenable    enable a device that match the second argument" +
                "\ndisable   disable a device that match the second argument" +
                "\njam       keep enabling a device until system deny access(shady)";
        }

    }

}
