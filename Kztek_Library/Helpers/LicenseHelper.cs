using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Library.Helpers
{
    public class LicenseHelper
    {
        public string CPU_ID { get; set; }

        public string MOTHERBOARD_ID { get; set; }

        public string CDKEY { get; set; }

        public string ProjectName { get; set; }

        public bool IsExpire { get; set; }

        public string ExpireDate { get; set; }

    }
}
