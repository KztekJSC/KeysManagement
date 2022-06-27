using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KztekKeyRegister.Tools
{
    internal static class CommonToolsFunc
    {
        public static string ConvertToACSIIString(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static List<string> GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> IPs = null;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPs.Add(ip.ToString());
                }
            }
            return IPs;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static async Task<string> ReadFileToText(string filePath)
        {
            System.IO.StreamReader sw = new System.IO.StreamReader(filePath);
            var txt = await sw.ReadToEndAsync();
            return txt;
        }
        
        public static string ReadFileToTextSync(string filePath)
        {
            System.IO.StreamReader sw = new System.IO.StreamReader(filePath);
            var txt = sw.ReadToEnd();
            return txt;
        }
    }
}
