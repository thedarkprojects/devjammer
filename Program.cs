using NativeSetupDiLib;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;

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
                Console.WriteLine("Error: invalid first argument '" + arg + "'");
                Console.WriteLine(GetHelp());
                Environment.Exit(1);
            }
            String query = String.Format(@"Select * From CIM_LogicalDevice WHERE DeviceID LIKE '%{0}%' OR Caption LIKE '%{0}%' OR Description LIKE '%{0}%'", extraParam);
            var usbDevices = GetLogicalDevices(query);
            foreach (var usbDevice in usbDevices)
            {
                try
                {
                    if (usbDevice.GetPropertyValue("DeviceID").ToString().StartsWith("\\\\") ||
                        usbDevice.GetPropertyValue("DeviceID").ToString().StartsWith("//"))
                    {
                        continue;
                    }
                    if (usbDevice.GetPropertyValue("DeviceID").ToString().ToLower().Contains(extraParam) ||
                        usbDevice.GetPropertyValue("Caption").ToString().ToLower().Contains(extraParam) ||
                        usbDevice.GetPropertyValue("Description").ToString().ToLower().Contains(extraParam) ||
                        usbDevice.GetPropertyValue("DeviceID").ToString().ToLower().Contains(extraParam))
                    {
                        Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                        string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();
                        if (arg.Equals("enable"))
                        {
                            try
                            {
                                DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);
                            } catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else if (arg.Equals("disable"))
                        {
                            try
                            {
                                DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else if (arg.Equals("jam"))
                        {
                            if (usbDevice.GetPropertyValue("Status").ToString().ToUpper().Equals("OK"))
                            {
                                continue;
                            }
                            try
                            {
                                Thread thread1 = new Thread(delegate ()
                                {
                                    while (true) DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);
                                });
                                Thread thread2 = new Thread(delegate ()
                                {
                                    do
                                    {
                                        Thread.Sleep(30000);//we need to sleep here so we can wait enough to check if the device is disabled again
                                    } while (!GetLogicalDevices(String.Format("{0} AND NOT DeviceID LIKE '\\\\%' AND NOT DeviceID LIKE '//%' AND Status='Error'", query))[0].GetPropertyValue("Status").ToString().ToUpper().Equals("OK"));
                                    thread1.Abort();
                                });
                                thread1.Start();
                                thread2.Start();
                                thread1.Join();
                                thread2.Join();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        Console.WriteLine("{0}ed {1}-{2}",
                           arg,
                           usbDevice.GetPropertyValue("Caption"),
                           usbDevice.GetPropertyValue("DeviceID"));
                    }
                } catch (Exception ex)
                {

                }
            }
        }

        static void PrintAllLogicalDevice(String extraParam)
        {
            var usbDevices = GetLogicalDevices(String.Format(@"Select * From CIM_LogicalDevice WHERE DeviceID LIKE '%{0}%' OR Caption LIKE '%{0}%' OR Description LIKE '%{0}%'", extraParam));
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

        static List<ManagementBaseObject> GetLogicalDevices(string query = @"Select * From CIM_LogicalDevice")
        {
            List<ManagementBaseObject> devices = new List<ManagementBaseObject>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", query))
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
