using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister
{
    internal class EncodedLicenseInfo
    {
        private string _Software;
        private string _Status;
        private string _DateExpire;
        private string _Cdkey;

        public string Software { get => _Software; set => _Software = value; }
        public string Status { get => _Status; set => _Status = value; }
        public string DateExpire { get => _DateExpire; set => _DateExpire = value; }
        public string Cdkey { get => _Cdkey; set => _Cdkey = value; }
    }
}
