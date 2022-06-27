using Kztek_Security;
using KztekKeyRegister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister
{
    public class KztekLicenseManager
    {
        private EncodedLicenseInfo licenseInfo = new EncodedLicenseInfo();
        private bool _IsLicenseValid = false;
        private string _AppCode = String.Empty;
        private string ManageConfig = $"{AppDomain.CurrentDomain.BaseDirectory}ManageConfig.xml";
        private FormKeyActivate activateUI;

        public KztekLicenseManager(string AppCode)
        {
            this._AppCode = AppCode;    
        }

        public EncodedLicenseInfo LicenseInfo { get => licenseInfo; set => licenseInfo = value; }

        public bool CheckActiveStatus()
        {
            try
            {
                var dt = Kztek.Utilities.XmlFileIO<KeyManageConfig>.LoadConfig(ManageConfig);
                if (dt != null)
                {
                    if (File.Exists(dt.ActivecodeFilePath))
                    {
                        var licensedata = File.ReadAllText(dt.ActivecodeFilePath);
                        if (!String.IsNullOrEmpty(licensedata))
                        {
                            var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                            if (licInfo != null)
                            {
                                //xử lý dữ liệu đọc từ file lic
                                LicenseInfo = ProcessLicdata(licInfo);
                                if (_IsLicenseValid)
                                {
                                    return true;
                                }
                            }
                        } 
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        //public async Task<bool> CheckActiveStatus()
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(ManageConfig))
        //        {
        //            return false;
        //        }
        //        var dt = Kztek.Utilities.XmlFileIO<KeyManageConfig>.LoadConfig(ManageConfig);
        //        if (dt == null || String.IsNullOrEmpty(dt.ActivecodeFilePath))
        //        {
        //            return false;
        //        }
        //        var licensedata = await File.ReadAllTextAsync(dt.ActivecodeFilePath);
        //        if (string.IsNullOrEmpty(licensedata))
        //        {
        //            return false;
        //        }
        //        var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
        //        if (licInfo != null)
        //        {
        //            //xử lý dữ liệu đọc từ file lic
        //            var Encodedata = ProcessLicdata(licInfo);
        //            if (_IsLicenseValid)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        [STAThread]
        public void ShowActivateForm()
        {
            if(activateUI == null)
            {
                activateUI = new FormKeyActivate();
            }
            if(LicenseInfo != null)
            {
                activateUI.LicenseInfo = LicenseInfo;
            }
            activateUI.AppCode= _AppCode;
            activateUI.ActivateRequest = new Func<LicenseInfo, EncodedLicenseInfo>(ProcessLicdata);
            activateUI.Init();
            activateUI.ShowDialog();
        }

        private EncodedLicenseInfo ProcessLicdata(LicenseInfo licInfo)
        {
            try
            {
                EncodedLicenseInfo dt = new EncodedLicenseInfo()
                {
                    Software = KztekSoftwareList.GetAppName(_AppCode),
                    Status = EncodedLicenseInfo.KeyStatus.Valid,
                    KeyExprireStatus = EncodedLicenseInfo.ExprireStatus.Nonlimit,
                    DateExpire = DateTime.MaxValue,
                };
                //có check hạn
                if (licInfo.IsExpire)
                {
                    dt.DateExpire = licInfo.ExpireDate.Value;
                    if (licInfo.ExpireDate.Value < DateTime.Now)
                    {
                        dt.Status = EncodedLicenseInfo.KeyStatus.Invalid;
                        dt.KeyExprireStatus = EncodedLicenseInfo.ExprireStatus.Exprired;
                    }
                    else if ((licInfo.ExpireDate.Value - DateTime.Now).TotalDays < 7)
                    {
                        dt.KeyExprireStatus = EncodedLicenseInfo.ExprireStatus.ExpireSoon;
                    }
                }
                licenseInfo = dt;
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
