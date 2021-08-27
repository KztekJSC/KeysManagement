using Kztek.Models.Common;
using Kztek.Tools;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KztekKeyRegister
{
    public partial class PreLogin : Form
    {
        private string LoginConfigFile = $"{AppDomain.CurrentDomain.BaseDirectory}LoginConfigs.xml";
        private bool IsPwSaved = false;
        private bool _LoggedIn = false;
        private AuthModel _auth;
        private string _ServerUrl;
        public AuthModel Auth { get => _auth;}
        public bool LoggedIn { get => _LoggedIn; }
        public string Token { get => _Token; }
        public string ServerUrl { get => _ServerUrl;}

        private string _Token;

        public PreLogin()
        {
            InitializeComponent();
            LoadLoginInfo();
        }
        /// <summary>
        /// Login vao he thong cua kztek
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Loggingin()
        {
            try
            {
                if (IsPwSaved)
                {
                    AuthModel Auth = new AuthModel()
                    {
                        Username = txtUsername.Text,
                        Password = txtPassword.Text
                    };
                    this._auth = Auth;
                    var dt = await PostLoginRequest(Auth);
                    if (dt.isSuccess)
                    {
                        var token = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(dt.Message).Token;
                        _LoggedIn = true;
                        _Token = token;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        private void LoadLoginInfo()
        {
            if (File.Exists(LoginConfigFile))
            {
                var dt = Kztek.Tools.ConfigManage.ConfigsManager<LoginConfigModel>.LoadConfig(LoginConfigFile);
                if (dt != null && !String.IsNullOrEmpty(dt.UserName))
                {
                    txtPassword.Text = CryptorEngine.Decrypt(dt.Password, true);
                    txtUsername.Text = dt.UserName;
                    txtServer.Text = dt.ServerUrl;
                    lblTitle.Text = $"Bạn đang đăng nhập bằng user: {dt.UserName}";
                    this._ServerUrl = dt.ServerUrl;
                    btnOK.Enabled = false;
                    btnLogout.Enabled = true;
                    IsPwSaved = true;
                }
            }
        }

        public async Task<MessageReport> PostLoginRequest(AuthModel auth)
        {
            try
            {
                var response = await ApiHelper.HttpPost(txtServer.Text.Trim() + "/api/login", auth);
                var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageReport>(await response.Content.ReadAsStringAsync());
                //server trả về 1 mess isSuccess- thành công hay ko. Message - dữ liệu TokenModel đã được Serialize - giá trị Token sẽ được dùng để sử dụng cho những lần trao đổi dữ liệu sau với server 
                if (responseContent != null)
                {
                    return responseContent;
                }
                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Exception orcured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new MessageReport()
                {
                    isSuccess = false,
                    Message = $"Exeption: {ex}",
                };
            }
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            AuthModel model = new AuthModel
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };
            var result = await PostLoginRequest(model);
            if (result != null && result.isSuccess)
            {
                LoginConfigModel dt = new LoginConfigModel()
                {
                    UserName = model.Username,
                    Password = CryptorEngine.Encrypt(model.Password, true),
                    ServerUrl = txtServer.Text.Trim(),
                };
                Kztek.Tools.ConfigManage.ConfigsManager<LoginConfigModel>.SaveConfig(dt, $"{AppDomain.CurrentDomain.BaseDirectory}LoginConfigs.xml");
                MessageBox.Show($"Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOK.Enabled = false;
                btnLogout.Enabled = true;
                _LoggedIn = true;
                this._ServerUrl = dt.ServerUrl;
                _Token = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(result.Message).Token;
                this.Close();
            }
            else
            {
                _LoggedIn = false;
                MessageBox.Show($"Đăng nhập không thành công!\r\n{result.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginConfigModel dt = new LoginConfigModel()
            {
                UserName = string.Empty,
                Password = string.Empty,
                ServerUrl = txtServer.Text.Trim(),
            };
            Kztek.Tools.ConfigManage.ConfigsManager<LoginConfigModel>.SaveConfig(dt, $"{AppDomain.CurrentDomain.BaseDirectory}LoginConfigs.xml");
            MessageBox.Show($"Đăng xuất thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _LoggedIn = false;
            btnOK.Enabled = true;
            btnLogout.Enabled = false;
            IsPwSaved = false;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text == String.Empty || (txtPassword.Text == String.Empty && txtUsername.Text == String.Empty) || txtServer.Text == String.Empty)
            {
                btnOK.Enabled = false;
            }
            else
                btnOK.Enabled = true;
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtServer.Text)) return;
            
        }
    }
}
