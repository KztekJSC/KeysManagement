using System;
using System.IO;
using Kztek_Security;
using System.Drawing;
using Kztek.CommonUI5;
using System.Windows.Forms;
using System.Threading.Tasks;
using KztekKeyRegister.Models;
using System.Runtime.InteropServices;
using System.Threading;

namespace KztekKeyRegister
{
    internal partial class FormKeyActivate : Form
    {
        internal Func<LicenseInfo, EncodedLicenseInfo> ActivateRequest { get; set; }

        private EncodedLicenseInfo licenseInfo;
        internal EncodedLicenseInfo LicenseInfo { get => licenseInfo; set => licenseInfo = value; }
        private string appCode;
        public string AppCode { get => appCode; set => appCode = value; }

        private LicenseInfo licinfo;
        private string ActiveCode = String.Empty;
        private string ActiveFilepath = String.Empty;
        private string ManageConfig = $"{AppDomain.CurrentDomain.BaseDirectory}ManageConfig.xml";

        internal FormKeyActivate()
        {
            InitializeComponent();
            this.FormClosing += FormKeyActivate_FormClosing;
        }

        private void FormKeyActivate_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                var ctrl = sender as Button;
                if (ctrl == btnCopyUserCode)
                {
                    Clipboard.SetText(txtUserCode.Text.Trim(), TextDataFormat.UnicodeText);
                }
                else
                {
                    Clipboard.SetText(licinfo?.CD_KEY, TextDataFormat.UnicodeText);
                }
            }
            catch 
            {
                Clipboard.SetText(String.Empty, TextDataFormat.UnicodeText);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCdkey.Text.Trim()))
            {
                KzMessageBox.Show( "Thông báo", "Vui lòng nhập cd key!", MessboxButton.OKCancel, MessboxType.Warning);
                return;
            }
            else
            {
                LicenseRequest licRequest = LicenseGenerator.CreateLicenseRequest(appCode, txtCdkey.Text.Trim());
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
            try
            {
                txtCdkey.Text = Clipboard.GetText().Trim();
            }
            catch 
            {
            }
        }

        private void btnExportUsercode_Click(object sender, EventArgs e)
        {
            
            string usercode = txtUserCode.Text.Trim();
            if (String.IsNullOrEmpty(usercode))
            {
                return;
            }
            var SoftName = KztekSoftwareList.GetAppName(appCode);
            Thread thread = new Thread(() =>
                {
                    SaveFileDialog Sfd = new SaveFileDialog();
                    Sfd.Filter = "Text Files | *.txt";
                    Sfd.FileName = $"{SoftName}_{txtCdkey.Text.Trim()}";
                    Sfd.DefaultExt = "*.txt";
                    if (Sfd.ShowDialog() == DialogResult.OK)
                    {
                        Stream fileStream = Sfd.OpenFile();
                        StreamWriter sw = new StreamWriter(fileStream);
                        sw.WriteLine(usercode);
                        sw.Flush();
                        sw.Close();
                    }
                }
            );

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void btnImportActiveKey_Click(object sender, EventArgs e)
        {
            try
            {
                //thread sta để chạy được các COM hệ thống
                Thread thread = new Thread(async () =>
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Text Files | *.dat";
                    open.Multiselect = false;
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        var licensedata = await Kztek.Utilities.FileIO.ReadTextFile(open.FileName);
                        if(!String.IsNullOrEmpty(licensedata))
                        {
                            ActiveCode = licensedata;
                            ActiveFilepath = open.FileName;
                            SetVisible(tickLinklicense, true);
                        }
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception)
            {
                SetVisible(tickLinklicense, false);
            }
        }

        internal void Init()
        {
            if(licenseInfo != null)
            {
                ShowLicenseData();
            }
        }

        private void SaveLicFileLocation()
        {
            Thread thread = new Thread(() => Kztek.Utilities.XmlFileIO<KeyManageConfig>.SaveConfig(new KeyManageConfig(ActiveFilepath), ManageConfig));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void ShowLicenseData()
        {
            var data = new UI_EncodedLicenseInfo(licenseInfo);
            tblLicenseInfo.Rows.Clear();
            for (int i = 0; i < data.Infos.Length; i++)
            {
                tblLicenseInfo.Rows.Add(data.Headers[i], data.Infos[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(ActiveCode)) return;
                var licInfo = LicenseGenerator.ReadActiveKey(ActiveCode, appCode);
                if (licInfo != null)
                {
                    var encodeData = ActivateRequest?.Invoke(licInfo);
                    if (encodeData.Status == EncodedLicenseInfo.KeyStatus.Valid)
                    {
                        tickActivated.Visible = true;
                        licenseInfo = encodeData;
                        SaveLicFileLocation();
                        var result = KzMessageBox.ShowDialog("Thông báo", $"Kích hoạt bản quyền thành công!\n\rTrở về phần mềm {encodeData.Software}?", MessboxButton.OKCancel, MessboxType.Info);
                        if (result != DialogResult.OK)
                        {
                            ShowLicenseData();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                KzMessageBox.Show("Kích hoạt thất bại", $"Active key không hợp lệ. Vui lòng thử lại!\r\n{ex.Message}", MessboxButton.OKCancel, MessboxType.Error);
            }
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void SetVisible(Control control, bool visible)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action<Control, bool>(SetVisible), control, visible);
            }
            else
            {
                control.Visible = visible;
            }
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
