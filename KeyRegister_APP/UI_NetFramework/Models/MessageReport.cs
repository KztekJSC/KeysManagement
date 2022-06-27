using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Models
{
    internal class MessageReport
    {
        private bool _isSuccess;

        private string _Message;

        public bool isSuccess { get => _isSuccess; set => _isSuccess = value; }
        public string Message { get => _Message; set => _Message = value; }
        public string message { get => _Message; set => _Message = value; }
    }
}
