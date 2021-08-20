using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Kztek_Security
{
    public static class LicenseGenerator
    {
        public const string publicKeyFile = "kzPublicKey.pem";
        public const string privateKeyFile = "kzPrivateKey.pem";
        public const string licFile = "kzlic.dat";

        /// <summary>
        /// [App] Mã hóa thông tin từ app thành UserCode
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string CreateUserCode(LicenseRequest request)
        {
            string key = File.ReadAllText(publicKeyFile);

            RSACryptoServiceProvider rsa = RSAKeys.ImportPublicKey(key);

            string json_data = JsonConvert.SerializeObject(request);

            byte[] decrypted_data = Encoding.UTF8.GetBytes(json_data);

            byte[] encrypted_data = rsa.Encrypt(decrypted_data, true);

            string result = Convert.ToBase64String(encrypted_data);

            return result;
        }

        /// <summary>
        /// [Server] Giải mã usercode thành thông tin 
        /// </summary>
        /// <param name="encryptedStr"></param>
        /// <returns></returns>
        public static LicenseRequest ReadUserCode(string encryptedStr)
        {
            string key = File.ReadAllText(privateKeyFile);

            RSACryptoServiceProvider rsa = RSAKeys.ImportPrivateKey(key);

            byte[] encrypted_data = Convert.FromBase64String(encryptedStr);

            byte[] decrypted_data = rsa.Decrypt(encrypted_data, true);

            string json_data = Encoding.UTF8.GetString(decrypted_data);

            LicenseRequest result = JsonConvert.DeserializeObject<LicenseRequest>(json_data);

            return result;
        }

        /// <summary>
        /// [App] Tạo thông tin gửi lên server
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="cdKey"></param>
        /// <returns></returns>
        public static LicenseRequest CreateLicenseRequest(string appId, string cdKey)
        {
            var request = new LicenseRequest();
            request.PROCESSOR_ID = HardwareInfo.PROCESSOR_ID;
            request.BASEBOARD_ID = HardwareInfo.BASEBOARD_ID;
            request.CD_KEY = cdKey;
            request.APP_CODE = appId;

            return request;
        }


        /// <summary>
        /// [Server] Tạo ActiveKey
        /// </summary>
        /// <param name="request"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string CreateActiveKey(LicenseRequest request, LicenseInfo info)
        {
            string passphrase = CreateSymPassword(request.PROCESSOR_ID, request.BASEBOARD_ID, request.APP_CODE);

            string licInfo_json = JsonConvert.SerializeObject(info);

            string encoded_data = CryptoProvider.SimpleEncryptWithPassword(licInfo_json, passphrase);

            return encoded_data;
        }

        /// <summary>
        /// [App] Đọc ActiveKey
        /// </summary>
        /// <param name="encoded_data"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static LicenseInfo ReadActiveKey(string encoded_data, string appId)
        {
            string passphrase = CreateSymPassword(HardwareInfo.PROCESSOR_ID, HardwareInfo.BASEBOARD_ID, appId);

            string decoded_data = CryptoProvider.SimpleDecryptWithPassword(encoded_data, passphrase);

            LicenseInfo licInfo = JsonConvert.DeserializeObject<LicenseInfo>(decoded_data);

            return licInfo;
        }

        private static string HashedBytesToString(byte[] hashBytes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; hashBytes != null && i < hashBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hashBytes[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Tạo mật khẩu mã hóa thông tin license
        /// </summary>
        /// <param name="cpu_id"></param>
        /// <param name="board_id"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        private static string CreateSymPassword(string cpu_id, string board_id, string appId)
        {
            SHA512 sha = SHA512CryptoServiceProvider.Create();

            List<byte> byteArr = new List<byte>();
            byteArr.AddRange(Encoding.UTF8.GetBytes(cpu_id));
            byteArr.AddRange(Encoding.UTF8.GetBytes(board_id));
            byteArr.AddRange(Encoding.UTF8.GetBytes(appId));

            byte[] hashedBytes = sha.ComputeHash(byteArr.ToArray());

            string passphrase = HashedBytesToString(hashedBytes);

            return passphrase;
        }

        public static void Demo()
        {
            //Tạo appCode và CDKEY ngẫu nhiên
            string appCode = Guid.NewGuid().ToString();
            string CDKEY = Guid.NewGuid().ToString();

            //Client tạo request, gửi lên sv thông tin phần cứng
            LicenseRequest req = LicenseGenerator.CreateLicenseRequest(appCode, CDKEY);
            string reqStr = LicenseGenerator.CreateUserCode(req);

            //Server đọc request, tạo response dựa trên thông tin phần cứng
            LicenseRequest decryptedReq = LicenseGenerator.ReadUserCode(reqStr);

            var info = new LicenseInfo()
            {
                CD_KEY = req.CD_KEY,
                ExpireDate = DateTime.Now,
                IsExpire = true,
                ProjectName = "FUTECH"
            };

            string respStr = LicenseGenerator.CreateActiveKey(req, info);

            //Client đọc response, lưu vào file
            var licData = LicenseGenerator.ReadActiveKey(respStr, appCode);
        }
    }
}
