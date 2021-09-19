using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Models
{
    public class AuthModel
    {
        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public bool isRemember { get; set; } = true;

        public bool isAny { get; set; } = false;

        public string AreaCode { get; set; } = "";

    }
}
