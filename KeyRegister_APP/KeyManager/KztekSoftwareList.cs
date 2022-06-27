using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister
{
    static class KztekSoftwareList
    {
        private static Dictionary<string,string> ListSoftware = new Dictionary<string, string>()
        {
            {"KZ_VC_V1_WD", "Kztek Vehicle Counting"},
            {"KZ_IP_V3_WD", "Kztek IParking V3"},
            {"KZ_IP_V6_WD", "Kztek IParking V6"},
            {"APPTESTKZTEK", "test"},
            {"KZ_AFC_V1_WD", "AFC HaiDuong" },
            {"KZ_SS_V1_WD", "Kztek iScale Station" }
        };
        public static string GetAppName(string APPCODE)
        {
            string appname = string.Empty;
            ListSoftware.TryGetValue(APPCODE, out appname);
            return appname;
        }

    }
}
