using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KztekKeyRegister.Tools;
using KztekKeyRegister.Models;
using System.IO;
using Kztek_Security;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KztekKeyRegister_NFW
{
    public partial class KeyMng : Form
    {
        public enum KeyStatus
        {
            Unregistered,
            Registered,
            ExprireSoon,
            Exprired,
        }

        private LicenseInfo licinfo;
        private string _AppCode = "APPTESTKZTEK";
        private string ActiveCode = string.Empty;
        private string ManageConfig = $"{AppDomain.CurrentDomain.BaseDirectory}ManageConfig.xml";
        private string ActiveFilepath = string.Empty;
        private readonly Dictionary<string, string> Licenses = new Dictionary<string, string>();
        private bool _IsLicenseValid = false;
        private DateTime? _ExpiredDate;
        private KeyStatus _ActiveStatus = KeyStatus.Unregistered;
        public DateTime? ExpiredDate { get => _ExpiredDate; }  //readonly
        public bool IsLicenseValid { get => _IsLicenseValid; }  //readonly
        public KeyStatus ActiveStatus { get => _ActiveStatus; }

        internal KeyMng()
        {
            InitializeComponent();
            CreateListSoftware();
        }
        public KeyMng(string AppCode)
        {
            this._AppCode = AppCode;
            InitializeComponent();
            CreateListSoftware();
        }
        public async Task<bool> CheckActiveStatus()
        {
            try
            {
                if (String.IsNullOrEmpty(ManageConfig)) return false;
                var dt = ConfigsManager<KeyManageConfig>.LoadConfig(ManageConfig);
                if (dt == null || String.IsNullOrEmpty(dt.ActivecodeFilePath)) return false;
                var licensedata = await CommonToolsFunc.ReadFileToText(dt.ActivecodeFilePath);
                if (string.IsNullOrEmpty(licensedata)) return false;
                var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                if (licInfo != null)
                {
                    var Encodedata = ProcessLicdata(licInfo);
                    if (_IsLicenseValid) return true;
                    ShowLicenseData(Encodedata);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void CreateListSoftware()
        {
            Licenses.Add("KZ_VC_V1_WD", "KztekVehicleCounting");
            Licenses.Add("APPTESTKZTEK", "app test");
            Licenses.Add("ACCESS", "IAccess");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var ctrl = sender as Button;
            if (ctrl == btnCopyUserCode)
                Clipboard.SetText(txtUserCode.Text.Trim(), TextDataFormat.UnicodeText);
            else Clipboard.SetText(licinfo.CD_KEY, TextDataFormat.UnicodeText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCdkey.Text.Trim()))
            {
                /* KzMessageBox.Show("Thông báo", "Vui lòng nhập cd key!", MessboxButton.OKCancel, MessboxType.Warning);*/
                MessageBox.Show("Vui lòng nhập cd key!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                LicenseRequest licRequest = LicenseGenerator.CreateLicenseRequest(_AppCode, txtCdkey.Text.Trim());
                string reqStr = LicenseGenerator.CreateUserCode(licRequest);
                txtUserCode.Text = reqStr;
                txtCdkey.Enabled = false;
                btnCopyUserCode.Visible = true;
                btnExportUsercode.Visible = true;
                tickUsercode.Visible = true;
            }
        }

        private void btnPasteCdKey_Click(object sender, EventArgs e)
        {
            txtCdkey.Text = Clipboard.GetText().Trim();
        }

        private void btnExportUsercode_Click(object sender, EventArgs e)
        {
            Licenses.TryGetValue(_AppCode, out string SoftName);
            SaveFileDialog Sfd = new SaveFileDialog();
            Sfd.Filter = "Text Files | *.txt";
            Sfd.FileName = $"{SoftName}_{txtCdkey.Text.Trim()}";
            Sfd.DefaultExt = "*.txt";
            if (Sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fileStream = Sfd.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                sw.WriteLine(txtUserCode.Text.Trim());
                sw.Flush();
                sw.Close();
            }
        }

        private async void btnImportActiveKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Files | *.dat";
            open.Multiselect = false;
            if (open.ShowDialog() == DialogResult.OK)
            {
                var licensedata = await CommonToolsFunc.ReadFileToText(open.FileName);
                ActiveCode = licensedata;
                ActiveFilepath = open.FileName;
            }
            tickLinklicense.Visible = true;
        }
        private void GetLicenseInfo(string licensedata)
        {
            try
            {
                if (String.IsNullOrEmpty(licensedata)) return;
                var licInfo = LicenseGenerator.ReadActiveKey(licensedata, _AppCode);
                var encodeData = ProcessLicdata(licInfo);
                if (licInfo != null)
                {
                    tickActivated.Visible = true;
                    /*var result = KzMessageBox.Show("Thông báo", "Kích hoạt bản quyền thành công!\n\rQuay trở về phần mềm chính?", MessboxButton.OKCancel, MessboxType.Info);*/
                    var result = MessageBox.Show($"Kích hoạt bản quyền thành công!\n\rMở lại phần mềm {encodeData.Software}?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    _IsLicenseValid = true;
                    this.licinfo = licInfo;
                    KeyManageConfig key = new KeyManageConfig()
                    {
                        ActivecodeFilePath = ActiveFilepath,
                    };
                    ConfigsManager<KeyManageConfig>.SaveConfig(key, ManageConfig);
                    if (result != DialogResult.OK)
                        ShowLicenseData(encodeData);
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                /* KzMessageBox.Show("Kích hoạt thất bại", $"Active key không hợp lệ. Vui lòng thử lại!\r\n{ex.Message}", MessboxButton.OKCancel, MessboxType.Error);*/
                MessageBox.Show($"Active key không hợp lệ. Vui lòng thử lại!\r\n{ex.Message}", "Kích hoạt thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private EncodedLicenseInfo ProcessLicdata(LicenseInfo licInfo)
        {
            var softName = string.Empty;
            string Status = "Đã kích hoạt";
            string date = "Vĩnh viễn";
            Licenses.TryGetValue(_AppCode, out softName);
            _IsLicenseValid = true;
            _ActiveStatus = KeyStatus.Registered;
            if (licInfo.IsExpire)
            {
                var timeleft = licInfo.ExpireDate.Value - DateTime.Now;
                if (licInfo.ExpireDate.Value < DateTime.Now)
                {
                    Status = "Đã hết hạn";
                    _IsLicenseValid = false;
                    _ActiveStatus = KeyStatus.Exprired;
                }
                else if (timeleft.TotalDays < 10)
                {
                    Status = "Sắp hết hạn";
                    _ActiveStatus = KeyStatus.ExprireSoon;
                }
                date = $"{licInfo.ExpireDate.Value.ToString("dd/MM/yyyy")} (còn {Math.Round(timeleft.TotalDays, 0)} ngày)";
                _ExpiredDate = licInfo.ExpireDate.Value;
            }
            EncodedLicenseInfo dt = new EncodedLicenseInfo()
            {
                Software = softName,
                Status = Status,
                DateExpire = date,
                Cdkey = licInfo.CD_KEY
            };
            return dt;
        }

        private void ShowLicenseData(EncodedLicenseInfo info)
        {
            lblunregist.Visible = false;
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.ColumnStyles.Clear();
            tbl.ColumnCount = 2;
            tbl.RowCount = 5;
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 0.20f));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 0.20f));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 0.20f));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 0.20f));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 0.20f));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.3f));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.7f));
            tbl.Controls.Add(newLabel("Loại License:"));
            tbl.Controls.Add(newLabel($"{info.Software}"));
            tbl.Controls.Add(newLabel("Trạng thái:"));
            tbl.Controls.Add(newLabel($"{info.Status}"));
            tbl.Controls.Add(newLabel("Hạn dùng:"));
            tbl.Controls.Add(newLabel($"{info.DateExpire}"));
            tbl.Controls.Add(newLabel("CD-KEY:"));
            tbl.Controls.Add(newLabel($"{info.Cdkey}"));
            tbl.Controls.Add(btnCdkeyCopy, 1, 4);
            btnCdkeyCopy.Visible = true;
            tbl.Dock = DockStyle.Fill;
            tickLinklicense.Visible = true;
            tickActivated.Visible = true;
            panel3.Controls.Add(tbl);
            tbl.BackColor = Color.Transparent;
        }

        private Label newLabel(string text)
        {
            Label lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;
            lb.Text = text;
            return lb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetLicenseInfo(ActiveCode);
        }

        private void txtActiveCode_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ActiveCode))
            {
                btnActive.Enabled = false;
            }
            else
            {
                btnActive.Enabled = true;
            }
        }


        private void Manage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsLicenseValid) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.No;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                var filename = $"{AppDomain.CurrentDomain.BaseDirectory}HDDangky.html";

                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = filename;
                System.Diagnostics.Process.Start(psi);

                /*    var runningProcess = System.Diagnostics.Process.GetProcessesByName("chrome");
                    if (runningProcess.Length != 0)
                    {
                        System.Diagnostics.Process.Start("chrome", filename);
                    }
                    runningProcess = System.Diagnostics.Process.GetProcessesByName("firefox");
                    if (runningProcess.Length != 0)
                    {
                        System.Diagnostics.Process.Start("firefox", filename);
                    }
                    runningProcess = System.Diagnostics.Process.GetProcessesByName("iexplore");
                    if (runningProcess.Length != 0)
                    {
                        System.Diagnostics.Process.Start("iexplore", filename);
                    }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tìm thấy file");
            }
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            var filename = $"http://kztek.net";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = filename;
            System.Diagnostics.Process.Start(psi);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    }
}
