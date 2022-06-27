using Kztek_Security;
using KztekKeyRegister.Models;
using KztekKeyRegister.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KztekKeyRegister
{
    public class KzKeyManager
    {
        private string _AppCode = "APPTESTKZTEK";
        private string LicenseFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}ManageConfig.xml";
        private KeyMng UI;

        #region Properties
        private bool _IsLicenseValid = false;
        public bool IsLicenseValid { get => _IsLicenseValid; }  //readonly

        private EncodedLicenseInfo _LicenseInfo = null;
        public EncodedLicenseInfo LicenseInfo { get => _LicenseInfo; }
        #endregion

        public KzKeyManager(string AppCode)
        {
            this._AppCode = AppCode;
            UI = new KeyMng(AppCode);
            UI.DoActive += new Action<object, EventArgs>(DoActivateLicense);
            UI.GetUserCode += new Action<object, EventArgs>((object sender, EventArgs e) => 
            {
                GetUsercode(UI.CDKEY);
            });
        }
        /// <summary>
        /// Check license info asyncnochoruos
        /// </summary>
        /// <returns>true if license is valid</returns>
        public async Task<bool> CheckActiveStatusAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(LicenseFilePath))
                {
                    var dt = ConfigsManager<KeyManageConfig>.LoadConfig(LicenseFilePath);
                    if(dt != null && !String.IsNullOrEmpty(dt.ActivecodeFilePath))
                    {
                        var licensedata = await CommonToolsFunc.ReadFileToText(dt.ActivecodeFilePath);
                        if (!String.IsNullOrEmpty(licensedata))
                        {
                            var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                            if(licInfo != null)
                            {
                                _LicenseInfo = ProcessLicdata(licInfo);
                                if (_IsLicenseValid) //Nếu key không hợp lệ thì show thông tin
                                {
                                    return true;
                                }
                                UI.ShowLicenseData();
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// Check license info syncnochoruos
        /// </summary>
        /// <returns>true if license is valid</returns>
        public bool CheckActiveStatusSync()
        {
            try
            {
                if (!String.IsNullOrEmpty(LicenseFilePath))
                {
                    var dt = ConfigsManager<KeyManageConfig>.LoadConfig(LicenseFilePath);
                    if (dt != null && !String.IsNullOrEmpty(dt.ActivecodeFilePath))
                    {
                        var licensedata = CommonToolsFunc.ReadFileToTextSync(dt.ActivecodeFilePath);
                        if (!String.IsNullOrEmpty(licensedata))
                        {
                            var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                            if (licInfo != null)
                            {
                                _LicenseInfo = ProcessLicdata(licInfo);
                                if (_IsLicenseValid) //Nếu key không hợp lệ thì show thông tin
                                {
                                    return true;
                                }
                                UI.ShowLicenseData();
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
                throw;
            }
        }

        private EncodedLicenseInfo ProcessLicdata(LicenseInfo li)
        {
            _IsLicenseValid = true;
            var LicenseStatus = EncodedLicenseInfo.KeyStatus.Registered;
            var softName = KztekKeyRegister.Helper.KztekSoftwareList.GetAppName(_AppCode);
            if (li.IsExpire)
            {
                var timeleft = li.ExpireDate.Value - DateTime.Now;
                if (li.ExpireDate.Value < DateTime.Now)
                {
                    _IsLicenseValid = false;
                    LicenseStatus = EncodedLicenseInfo.KeyStatus.Exprired;
                }
                else if (timeleft.TotalDays < 10)
                {
                    LicenseStatus = EncodedLicenseInfo.KeyStatus.ExprireSoon;
                }
            }
            EncodedLicenseInfo dt = new EncodedLicenseInfo()
            {
                Software = softName,
                Status = LicenseStatus,
                DateExpire = li.ExpireDate.Value,
                Cdkey = li.CD_KEY,
                IsExpireCheck = li.IsExpire
            };
            return dt;
        }

        /// <summary>
        /// Get the EncodedLicenseData form text data in license file
        /// </summary>
        /// <param name="licensedata">text data</param>
        /// <returns>true if text data was decode-able</returns>
        private bool GetLicenseEncodedData(string licensedata)
        {
            try
            {
                if (String.IsNullOrEmpty(licensedata))
                {
                    return false;
                } 
                var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                if (licInfo != null)
                {
                    _LicenseInfo = ProcessLicdata(licInfo);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Active key không hợp lệ. Vui lòng thử lại!\r\n{ex.Message}", "Kích hoạt thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool GetUsercode(string cdkey)
        {
            try
            {
                LicenseRequest licRequest = LicenseGenerator.CreateLicenseRequest(_AppCode, cdkey);
                UI.UserCode = LicenseGenerator.CreateUserCode(licRequest);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DoActivateLicense(object sender, EventArgs e)
        {
            try
            {
                string licdata = string.Empty;
                licdata = CommonToolsFunc.ReadFileToTextSync(UI.LicenseFilePath);
                var dt = GetLicenseEncodedData(licdata);
                if (dt)
                {
                    ConfigsManager<KeyManageConfig>.SaveConfig(new KeyManageConfig(UI.LicenseFilePath) , LicenseFilePath);
                    UI.IsActivated = _IsLicenseValid;
                    UI.LicenseInfo = _LicenseInfo;
                    UI.ShowLicenseData();
                    if (_IsLicenseValid)
                    {
                        UI.ActiveSucess();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Show the Registration UI.
        /// </summary>
        /// <returns>true if new Registed License is valid </returns>
        public bool ShowRegistrationForm()
        {
            if(UI.ShowDialog() == DialogResult.OK) //tro ve phan mem chinh
            {
                return _IsLicenseValid;
            }
            else 
            {
                return false;
            }
        }
    }
}
