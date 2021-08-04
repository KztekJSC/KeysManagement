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

        public static string EncryptRequest(LicenseRequest request)
        {
            string key = File.ReadAllText(publicKeyFile);

            RSACryptoServiceProvider rsa = RSAKeys.ImportPublicKey(key);

            string json_data = JsonConvert.SerializeObject(request);

            byte[] decrypted_data = Encoding.UTF8.GetBytes(json_data);

            byte[] encrypted_data = rsa.Encrypt(decrypted_data, true);

            string result = Convert.ToBase64String(encrypted_data);

            return result;
        }

        public static LicenseRequest DecryptRequest(string encryptedStr)
        {
            string key = File.ReadAllText(privateKeyFile);

            RSACryptoServiceProvider rsa = RSAKeys.ImportPrivateKey(key);

            byte[] encrypted_data = Convert.FromBase64String(encryptedStr);

            byte[] decrypted_data = rsa.Decrypt(encrypted_data, true);

            string json_data = Encoding.UTF8.GetString(decrypted_data);

            LicenseRequest result = JsonConvert.DeserializeObject<LicenseRequest>(json_data);

            return result;
        }

        public static LicenseRequest CreateLicenseRequest(string appId, string cdKey)
        {
            var request = new LicenseRequest();
            request.PROCESSOR_ID = HardwareInfo.PROCESSOR_ID;
            request.BASEBOARD_ID = HardwareInfo.BASEBOARD_ID;
            request.CD_KEY = appId;
            request.APP_ID = cdKey;

            return request;
        }

        public static string CreateLicenseResponse(LicenseRequest request, LicenseInfo info)
        {
            string passphrase = CreateSymPassword(request.PROCESSOR_ID, request.BASEBOARD_ID, request.CD_KEY, request.APP_ID);

            string licInfo_json = JsonConvert.SerializeObject(info);

            string encoded_data = CryptoProvider.SimpleEncryptWithPassword(licInfo_json, passphrase);

            return encoded_data;
        }

        public static LicenseInfo DecodeLicenseResponse(string encoded_data, string appId, string cdKey)
        {
            string passphrase = CreateSymPassword(HardwareInfo.PROCESSOR_ID, HardwareInfo.BASEBOARD_ID, appId, cdKey);

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

        private static string CreateSymPassword(string cpu_id, string board_id, string appId, string cdKey)
        {
            SHA512 sha = SHA512CryptoServiceProvider.Create();

            List<byte> byteArr = new List<byte>();
            byteArr.AddRange(Encoding.UTF8.GetBytes(cpu_id));
            byteArr.AddRange(Encoding.UTF8.GetBytes(board_id));
            byteArr.AddRange(Encoding.UTF8.GetBytes(appId));
            byteArr.AddRange(Encoding.UTF8.GetBytes(cdKey));

            byte[] hashedBytes = sha.ComputeHash(byteArr.ToArray());

            string passphrase = HashedBytesToString(hashedBytes);

            return passphrase;
        }

        public static void Demo()
        {
            //Tạo appId và CDKEY ngẫu nhiên
            string appId = Guid.NewGuid().ToString();
            string CDKEY = Guid.NewGuid().ToString();

            //Client tạo request, gửi lên sv thông tin phần cứng
            LicenseRequest req = LicenseGenerator.CreateLicenseRequest(appId, CDKEY);
            string reqStr = LicenseGenerator.EncryptRequest(req);

            //Server đọc request, tạo response dựa trên thông tin phần cứng
            LicenseRequest decryptedReq = LicenseGenerator.DecryptRequest(reqStr);

            var info = new LicenseInfo()
            {
                CD_KEY = req.CD_KEY,
                ExpireDate = DateTime.Now,
                IsExpire = true,
                ProjectName = "FUTECH"
            };

            string respStr = LicenseGenerator.CreateLicenseResponse(req, info);

            //Client đọc response, lưu vào file
            var licData = LicenseGenerator.DecodeLicenseResponse(respStr, appId, CDKEY);
        }
    }
}
