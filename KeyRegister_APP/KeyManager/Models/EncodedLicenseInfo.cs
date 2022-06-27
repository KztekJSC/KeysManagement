using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister
{
    public class EncodedLicenseInfo
    {
        public enum KeyStatus
        {
            Valid,
            Invalid,
        }

        public enum ExprireStatus
        {
            NotFound = -1,
            Nonlimit,
            ExpireSoon,
            Exprired,
        }


        private string _Software = "N/A";
        private KeyStatus _Status = KeyStatus.Invalid;
        private ExprireStatus _ExprireStatus = ExprireStatus.NotFound;
        private DateTime _DateExpire = DateTime.MinValue;
        private string _Cdkey = String.Empty;

        public string Software { get => _Software; set => _Software = value; }
        public KeyStatus Status { get => _Status; set => _Status = value; }
        public DateTime DateExpire { get => _DateExpire; set => _DateExpire = value; }
        public string Cdkey { get => _Cdkey; set => _Cdkey = value; }
        public ExprireStatus KeyExprireStatus { get => _ExprireStatus; set => _ExprireStatus = value; }
    }
}
