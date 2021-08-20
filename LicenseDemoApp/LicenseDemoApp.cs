using Kztek_Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseDemoApp
{
    public partial class LicenseDemoApp : Form
    {
        public static string APP_CODE = "APPTESTKZTEK";
        public LicenseDemoApp()
        {
            InitializeComponent();
        }

        private void LicenseDemoApp_Load(object sender, EventArgs e)
        {
            txtCPU.Text = HardwareInfo.PROCESSOR_ID;
            txtBOARDID.Text = HardwareInfo.BASEBOARD_ID;
            txtAppCode.Text = APP_CODE;
            ReadLicense();
        }

        private void ReadLicense()
        {
            try
            {
                var licdata = File.ReadAllText("license.dat");
                var licInfo = LicenseGenerator.ReadActiveKey(licdata, txtAppCode.Text);

                lbStatus.Text = licInfo.IsExpire ? "Trial" : "Full";
                lbExpire.Text = licInfo.IsExpire ? licInfo.ExpireDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "No Expire";
                txtCDKEY.Text = licInfo.CD_KEY;
            }
            catch (FileNotFoundException)
            {
                lbStatus.Text = "Expired";
                lbExpire.Text = "Expired";
            }
            catch (Exception)
            {
                lbStatus.Text = "Expired";
                lbExpire.Text = "Expired";

                MessageBox.Show("Invalid License File");
            }
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if(new frmActivate().ShowDialog() == DialogResult.OK)
            {
                ReadLicense();
            }
        }
    }
}
