using System;
using System.Collections.Generic;
using System.Text;

namespace KztekKeyRegister.Models
{
    public class KeyManageConfig
    {
        private string _ActivecodeFilePath;

        public string ActivecodeFilePath { get => _ActivecodeFilePath; set => _ActivecodeFilePath = value; }
    }
}
