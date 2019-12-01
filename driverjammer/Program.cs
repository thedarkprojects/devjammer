using NativeSetupDiLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace driverjammer
{
    //see https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/cim-logicaldevice
    class Program
    {
        static void Main(string[] args)
        {
            String name = "USBSTOR";
            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                if (usbDevice.GetPropertyValue("DeviceID").ToString().Contains(name))
                {
                    Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}, Status: {3}",
                        usbDevice.GetPropertyValue("DeviceID"), usbDevice.GetPropertyValue("SystemCreationClassName"), usbDevice.GetPropertyValue("Description"), usbDevice.GetPropertyValue("Status"));
                    
                    // every type of device has a hard-coded GUID, this is the one for mice
                    Guid mouseGuid = new Guid("{4d36e967-e325-11ce-bfc1-08002be10318}");

                    // get this from the properties dialog box of this device in Device Manager
                    string instancePath = @usbDevice.GetPropertyValue("DeviceID").ToString();

                    while (true) DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);
                    Console.WriteLine("Done");
                }
            }

            Console.Read();
        }

        static List<ManagementBaseObject> GetUSBDevices()
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
    }

}
