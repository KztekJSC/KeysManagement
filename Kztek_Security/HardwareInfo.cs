using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Kztek_Security
{
    public static class HardwareInfo
    {
        public static string PROCESSOR_ID { get; }
        public static string BASEBOARD_ID { get; }
        static HardwareInfo()
        {
            PROCESSOR_ID = GetHardwareInfo("Win32_processor", "ProcessorID");
            BASEBOARD_ID = GetHardwareInfo("Win32_BaseBoard", "SerialNumber");
        }

        private static string GetHardwareInfo(string key, string info)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher($"SELECT * FROM {key}");

            ManagementObjectCollection moc = mos.Get();

            string value = string.Empty;

            foreach (ManagementObject mo in moc)
            {
                value = mo[$"{info}"].ToString();
            }

            return value;
        }
    }
}
