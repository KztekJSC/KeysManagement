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

namespace KztekKeyRegister
{
    internal partial class KeyMng : Form
    {
        public Action<object , EventArgs > GetUserCode { get; set; }
        public Action<object , EventArgs > DoActive { get; set; }

        private string Appcode;

        private string activeCode;
        public string ActiveCode { get => activeCode; set => activeCode = value; }
        
        private EncodedLicenseInfo _LicenseInfo;
        public EncodedLicenseInfo LicenseInfo { get => _LicenseInfo; set => _LicenseInfo = value; }

        public string UserCode { get => txtUserCode.Text.Trim(); set => txtUserCode.Text = value; }
        public string CDKEY { get => txtCdkey.Text.Trim(); }

        private string _LicenseFilePath;
        public string LicenseFilePath { get => _LicenseFilePath; set => _LicenseFilePath = value; }

        private bool isActivated = false;
        public bool IsActivated { get => isActivated; set => isActivated = value; }


        internal KeyMng()
        {
            InitializeComponent();
        }

        internal KeyMng(string AppCode)
        {
            this.Appcode = AppCode;
            InitializeComponent();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var ctrl = sender as Button;
            if (ctrl == btnCopyUserCode)
            {
                Clipboard.SetText(txtUserCode.Text.Trim(), TextDataFormat.UnicodeText);
            }
            else
            {
                Clipboard.SetText(LicenseInfo.Cdkey, TextDataFormat.UnicodeText);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCdkey.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập CD KEY", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCdkey.Focus();
                return;
            }
            GetUserCode?.Invoke(this, e);
        }

        private void btnPasteCdKey_Click(object sender, EventArgs e)
        {
            txtCdkey.Text = Clipboard.GetText().Trim();
        }

        private void btnExportUsercode_Click(object sender, EventArgs e)
        {
            var SoftName = Helper.KztekSoftwareList.GetAppName(Appcode);
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
                _LicenseFilePath = open.FileName;
            }
            tickLinklicense.Visible = true;
        }

        public void ActiveSucess()
        {
            var result = MessageBox.Show($"Kích hoạt bản quyền thành công!\n\r" +
                $"OK - Trở lại phần mềm {_LicenseInfo.Software}?\n\r" +
                $"Cancel - Xem thông tin License",
                     "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.DialogResult = result;
            }
        }

        public void ShowLicenseData()
        {
            if(LicenseInfo != null)
            {
                lblunregist.Visible = false;
                string Date = String.Empty;

                if (LicenseInfo.IsExpireCheck)
                {
                    Date = "Vĩnh viễn";
                }
                else
                {
                    var dayleft = Math.Round((LicenseInfo.DateExpire - DateTime.Now).TotalDays,0);
                    Date = $"{LicenseInfo.DateExpire} - còn {dayleft} ngày";
                }

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
                tbl.Controls.Add(newLabel($"{LicenseInfo.Software}"));
                tbl.Controls.Add(newLabel("Trạng thái:"));
                tbl.Controls.Add(newLabel($"{LicenseInfo.Status}"));
                tbl.Controls.Add(newLabel("Hạn dùng:"));
                tbl.Controls.Add(newLabel($"{Date}"));
                tbl.Controls.Add(newLabel("CD-KEY:"));
                tbl.Controls.Add(newLabel($"{LicenseInfo.Cdkey}"));
                tbl.Controls.Add(btnCdkeyCopy, 1, 4);
                tbl.BackColor = Color.Transparent;
                tbl.ForeColor = MAINPANEL.BackColor == Color.Black ? Color.FromArgb(240, 247, 212) : Color.Black;
                tbl.Dock = DockStyle.Fill;
                panel3.Controls.Add(tbl);
                btnCdkeyCopy.Visible = IsActivated;
                tickActivated.Visible = IsActivated;
            }
        }

        private Label newLabel(string text)
        {
            Label lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;
            lb.Text = text;
            return lb;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                var filename = $"{AppDomain.CurrentDomain.BaseDirectory}HDDangky.html";

                var psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = filename;
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tìm thấy file");
            }
        }

        private void txtUserCode_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUserCode.Text))
            {
                txtCdkey.Enabled = false;
                btnCopyUserCode.Visible = true;
                btnExportUsercode.Visible = true;
                tickUsercode.Visible = true;
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            var filename = $"http://kztek.net";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = filename;
            Process.Start(psi);
        }

        private void btnExit_click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DoActive?.Invoke(this, e);
        }

        private void buttonMinimize_click(object sender, EventArgs e)
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

        private void KeyMng_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (IsActivated)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(MAINPANEL.BackColor == Color.Black)
            {
                MAINPANEL.BackColor = Color.WhiteSmoke;
                panel5.BackColor = label1.BackColor = statusStrip1.BackColor = Color.FromArgb(54, 81, 148);
                btnActive.BackColor = btnExportUsercode.BackColor = btnCopyUserCode.BackColor = btnGetUsercode.BackColor =
                    btnImportActiveKey.BackColor = btnActive.BackColor = Color.FromArgb(37, 51, 84);
                panel3.ForeColor = label4.ForeColor = label5.ForeColor = lblunregist.ForeColor = Color.Black;
                
                btnCdkeyPaste.ImageIndex = 0;
            }
            else
            {
                MAINPANEL.BackColor = Color.Black;
                panel5.BackColor = label1.BackColor = statusStrip1.BackColor = Color.FromArgb(21, 0, 80);
                btnActive.BackColor = btnExportUsercode.BackColor = btnCopyUserCode.BackColor = btnGetUsercode.BackColor =
                    btnImportActiveKey.BackColor = btnActive.BackColor = Color.FromArgb(97, 0, 148);
                panel3.ForeColor = label4.ForeColor = label5.ForeColor = lblunregist.ForeColor = Color.FromArgb(240, 247, 212);
                btnCdkeyPaste.ImageIndex = 1;
            }
        }
    }
}
