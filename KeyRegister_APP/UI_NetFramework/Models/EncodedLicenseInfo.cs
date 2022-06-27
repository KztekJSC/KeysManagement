using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Models
{
    public class EncodedLicenseInfo
    {
        public enum KeyStatus
        {
            Unregistered,
            Registered,
            ExprireSoon,
            Exprired,
        }

        private string _Software;
        private KeyStatus _Status;
        private DateTime _DateExpire;
        private string _Cdkey;
        private bool _isExpireCheck;

        public string Software { get => _Software; set => _Software = value; }
        public KeyStatus Status { get => _Status; set => _Status = value; }
        public DateTime DateExpire { get => _DateExpire; set => _DateExpire = value; }
        public string Cdkey { get => _Cdkey; set => _Cdkey = value; }
        public bool IsExpireCheck { get => _isExpireCheck; set => _isExpireCheck = value; }
    }
}
