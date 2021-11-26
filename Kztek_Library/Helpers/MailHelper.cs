using Kztek_Core.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kztek_Library.Helpers
{
    public class MailHelper
    {
        public static async Task<MessageReport> ValidateMail(string Email)
        {
            var result = new MessageReport(true, "");

            if (!string.IsNullOrEmpty(Email))
            {
                //email
                string pattern = @"^([\P{L}\p{IsBasicLatin}]+$)";

                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(Email))
                {
                    result = new MessageReport(false, "Định dạng Email không hợp lệ!");
                }

                var pattern2 = @"^[^@\s]+@[^@\s]+(\.[^@\s]+)+$";
                var regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);

                if (!regex2.IsMatch(Email))
                {
                    result = new MessageReport(false, "Định dạng Email không hợp lệ!");
                }
            }

            return await Task.FromResult(result);
        }

        public static async Task SendMail(string Subject, string HtmlContent, tblSystemConfig info)
        {
            var port = await AppSettingHelper.GetStringFromAppSetting("Email:System:Port");
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(info.EmailSystem);
            message.To.Add(new MailAddress(info.EmailTo));

            if (!string.IsNullOrWhiteSpace(info.EmailCC))
            {
                var mailCc = info.EmailCC.Split(';');
                if (mailCc.Length > 0)
                {
                    for (int i = 0; i < mailCc.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mailCc[i]))
                        {
                            message.CC.Add(new MailAddress(mailCc[i]));
                        }

                    }

                }
            }
            if (!string.IsNullOrWhiteSpace(info.EmailBCC))
            {
                var mailBcc = info.EmailBCC.Split(';');
                if (mailBcc.Length > 0)
                {
                    for (int i = 0; i < mailBcc.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mailBcc[i]))
                        {
                            message.Bcc.Add(new MailAddress(mailBcc[i]));
                        }

                    }

                }
            }

            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = HtmlContent;
            smtp.Port = !string.IsNullOrEmpty(port) ? Convert.ToInt32(port) : 0;
            smtp.Host = await AppSettingHelper.GetStringFromAppSetting("Email:System:Host"); //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(info.EmailSystem, info.EmailPass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}
