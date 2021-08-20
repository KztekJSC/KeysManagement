using Kztek_Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseDemoApp
{
    public partial class frmActivate : Form
    {
        public frmActivate()
        {
            InitializeComponent();
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCDKEY.Text)) return;

            LicenseRequest licRequest = LicenseGenerator.CreateLicenseRequest(LicenseDemoApp.APP_CODE, txtCDKEY.Text.Trim());
            string reqStr = LicenseGenerator.CreateUserCode(licRequest);
            txtUserCode.Text = reqStr;
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                var licdata = txtActiveCode.Text;
                var licInfo = LicenseGenerator.ReadActiveKey(licdata, LicenseDemoApp.APP_CODE);

                File.WriteAllText("license.dat", licdata);

                MessageBox.Show("Success");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Code");
            }
        }
    }
}
