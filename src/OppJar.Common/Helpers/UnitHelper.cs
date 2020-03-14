using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace OppJar.Common.Helpers
{
    public static class UnitHelper
    {
        public static string NullToStringEmpty(this string value)
        {
            if (value == null) return string.Empty;
            return value;
        }

        public static string ToSeoUrl(this string sUrl)
        {
            string encodedUrl = (sUrl ?? "").ToLower();
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");
            encodedUrl = encodedUrl.Replace("'", "");
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
            encodedUrl = encodedUrl.Trim('-');
            return encodedUrl;
        }

        public static string MakeLinkSEO(this string text, int articleId)
        {
            string linkSEO = text.ToLower().Replace(",", "");

            linkSEO = linkSEO.Replace("?", "");

            return linkSEO.Replace(" ", "-") + (articleId != 0 ? "-" + articleId.ToString() : "");
        }

        public static string CountDay(this object date)
        {

            TimeSpan duration = DateTime.Now - Convert.ToDateTime(date);


            string result = (duration.Minutes == 0 ? duration.Seconds.ToString() + " Giây Trước" : duration.Minutes.ToString() + " phút trước");

            if (duration.Hours != 0 && duration.Days == 0)
            {
                result = duration.Hours.ToString() + " giờ trước";
            }
            else if (duration.Days != 0 && duration.Hours != 0)
            {
                if (duration.Days >= 31)
                {
                    result = MonthDifference(DateTime.Now, Convert.ToDateTime(date)).ToString() + " tháng trước";
                }
                else
                {
                    result = duration.Days.ToString() + " ngày trước";
                }
            }

            return result;

        }

        private static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }

        public static string GetSortContent(this string str, int length = 300)
        {
            try
            {
                str = str.RemoveHtml();
                if (str.Length > 300)
                {
                    return str.Substring(0, length);
                }
                else
                {
                    return str;
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }

        }

        public static string RemoveHtml(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static DayOfWeek GetDayOfWeek()
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;

            return today;
        }

        public static string GenerateSeoCode()
        {
            string uuid = Guid.NewGuid().ToString("n");
            return uuid.Substring(0, 6);
        }

        public static string ToNiceUrl(this string text)
        {
            return $"{text.ToSeoUrl()}";
        }

        public static string BuildPreview(this string value)
        {
            var result = value;
            // TO DO
            // Use try-catch to don't break the old data
            // Will be removed when deploying to PROD
            try
            {
                var jsonObj = JObject.Parse(value);

                result = $"<div class=\"col-md-2 col-sm-2\"><img class=\"img-responsive\" src=\"{jsonObj.GetValue("image").Value<string>()}\"></div>" +
                    $"<div class=\"col-md-10 col-sm-10\"><a style=\"font-size:18px;\" href=\"{jsonObj.GetValue("url").Value<string>()}\" target=\"_blank\">{jsonObj.GetValue("title").Value<string>()}</a>" +
                    $"<p>{jsonObj.GetValue("description").Value<string>()}</p></div>";
            }
            catch { }

            return result;
        }
    }
}
