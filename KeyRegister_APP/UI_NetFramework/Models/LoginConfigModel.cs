using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Models
{
    internal class LoginConfigModel
    {
        private string _ServerUrl;
        private string _UserName;
        private string _Password;
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string ServerUrl { get => _ServerUrl; set => _ServerUrl = value; }
    }
}
