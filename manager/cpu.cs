﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;
using OxyPlot;
using OxyPlot.Series;
using System.Threading;

namespace cpumanager
{
    class cpu
    {
        /// <summary>
        /// Return processorId from first CPU in machine
        /// </summary>
        /// <returns>[string] ProcessorId</returns>
        public void GetCPUId()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select Name from Win32_Processor");
            new ManagementObjectSearcher("Select Name from Win32_Processor");
            foreach (ManagementObject proc in searcher.Get())
            {
                Console.WriteLine("CPU : {0}\n", proc.GetPropertyValue("Name"));
            }        
        }
        /// <summary>
        /// recuperation du clock speed
        /// </summary>
        /// <returns>Clock speed</returns>
        public int GetCPUCurrentClockSpeed()
        {
            int cpuClockSpeed = 0;
            
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection objCol = mgmt.GetInstances();
            // loop pour tous les processeurs detectés
            foreach (ManagementObject obj in objCol)
            {
                if (cpuClockSpeed == 0)
                {
                    // retourne uniquement le clock speed du premier CPU
                    cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
                }
            }
            //return the status
            return cpuClockSpeed;
        }

        protected readonly ListSet<ISensor> active = new ListSet<ISensor>();
        public event SensorEventHandler SensorAdded;
        
        protected virtual void ActivateSensor(ISensor sensor)
        {
            if (active.Add(sensor))
                if (SensorAdded != null)
                    SensorAdded(sensor);
        }

        /// <summary>
        /// recup d'infos (inutiles ?) du CPU
        /// </summary>
        /// <returns>void</returns>
        public void more_cpu_info()
        {
            /*
            Exepected hardware output
            MSR         EDX       EAX
            000000CE  00001000  60011900
            00000198  00002058  00001000
            0000019C  00000000  88320000
            000001A2  00000000  00560600
            000001B1  00000000  882B0000
            00000606  00000000  000A1003
            00000611  00000000  790A4AF0
            00000639  00000000  4781BAD1
            00000641  00000000  014DDAD8
            */
            var myComputer = new Computer();
            myComputer.GetType();
            myComputer.CPUEnabled = true;
            myComputer.ToCode();
            myComputer.Open();
            
            foreach (var hardwareItem in myComputer.Hardware)
            {
                hardwareItem.Update();
                hardwareItem.GetReport();

                Console.WriteLine(hardwareItem.GetReport());
                var series = new LineSeries();
                foreach (var sensor in hardwareItem.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature)
                    {
                            Console.WriteLine("{0} {1} = {2}", sensor.Name, sensor.SensorType, sensor.Value);
                    }
                }
            }
        }
    }
}
