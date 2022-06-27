using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Models
{
    internal class UI_EncodedLicenseInfo
    {
        public string[] Headers
        {
            get =>
            new string[]
            {
                "Loại License",
                "Trạng thái",
                "Hạn dùng",
                "CD-KEY",
            };
        }

        public string[] Infos
        {
            get =>
            new string[]
            {
                licenseInfo.Software,
                licenseInfo.Status == EncodedLicenseInfo.KeyStatus.Valid ? "Đã kích hoạt" : "Chưa kích hoạt",
                GetDate(),
                licenseInfo.Cdkey,
            };
        }

        private EncodedLicenseInfo licenseInfo;
        public UI_EncodedLicenseInfo(EncodedLicenseInfo licenseInfo)
        {
            this.licenseInfo = licenseInfo;
        }
        private string GetDate()
        {
            switch (licenseInfo.KeyExprireStatus) 
            {
                case EncodedLicenseInfo.ExprireStatus.NotFound :
                    return String.Empty;
                case EncodedLicenseInfo.ExprireStatus.Nonlimit:
                    return "Vĩnh viễn";
                default:
                    return $"{licenseInfo.DateExpire:dd/MM/yyyy} (còn ({(DateTime.Now - licenseInfo.DateExpire).TotalDays}))";
            }

        }

    }
}
