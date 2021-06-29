using Kztek_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
