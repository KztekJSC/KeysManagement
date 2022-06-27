using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KztekKeyRegister.Tools
{
    internal class ApiHelper
    {
        public static HttpClient client;
        private static string UserName;
        private static string Password;
        static ApiHelper()
        {
            client = new HttpClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public static void SetCredential(string Username, string PassWord)
        {
            UserName = Username;
            Password = PassWord;
        }

        public static Dictionary<string, string> Headers(string secretKey) {
            var headers = new Dictionary<string, string>();
            headers.Add("SecretKey", secretKey);
            return headers;
        }

        public static AuthenticationHeaderValue Authentication(string token) {
            return new AuthenticationHeaderValue("Bearer", token);;
        }

        public static async Task<HttpResponseMessage> HttpGet(string uri, Dictionary<string, string> headers = null, AuthenticationHeaderValue auth_headers = null)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
               
            };

            return await HttpSend(request, headers, auth_headers).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> HttpPost<T>(string uri, T obj, Dictionary<string, string> headers = null, AuthenticationHeaderValue auth_headers = null)
        {
            var content = JsonConvert.SerializeObject(obj);
 
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
            };

            return await HttpSend(request, headers, auth_headers).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> HttpPut<T>(string uri, T obj, Dictionary<string, string> headers = null, AuthenticationHeaderValue auth_headers = null)
        {
            var content = JsonConvert.SerializeObject(obj);
 
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uri),
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
            };

            return await HttpSend(request, headers, auth_headers).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> HttpDelete(string uri, Dictionary<string, string> headers = null, AuthenticationHeaderValue auth_headers = null)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };

            return await HttpSend(request, headers, auth_headers).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> HttpSend(HttpRequestMessage request, Dictionary<string, string> headers = null, AuthenticationHeaderValue auth_headers = null)
        {
            if(headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if(auth_headers != null)
            {
                request.Headers.Authorization = auth_headers;
            }

            return await client.SendAsync(request).ConfigureAwait(false);
        }

        public static Task<T> ConvertResponse<T>(HttpResponseMessage response)
        {
            if (response != null && response.IsSuccessStatusCode)
            {
                var t = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return Task.FromResult(t);
            }

            return null;
        }

        public static async Task<HttpResponseMessage> GetHttpWithDigest(string uri)
        {
            Uri myUri = new Uri(uri);
            string query = myUri.PathAndQuery; // get query
            string hostName = myUri.ToString().Replace(query, ""); //get host
            var credCache = new CredentialCache();
            credCache.Add(new Uri(hostName), "Digest", new NetworkCredential(UserName, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            var answer = await httpClient.GetAsync(myUri).ConfigureAwait(false);
            return answer;
        }
    }
}