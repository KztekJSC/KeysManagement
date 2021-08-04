using System;
using System.Collections.Generic;
using System.Text;

namespace Kztek_Security
{
    public class LicenseInfo
    {
        public string CD_KEY { get; set; }

        public string ProjectName { get; set; }

        public bool IsExpire { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}
