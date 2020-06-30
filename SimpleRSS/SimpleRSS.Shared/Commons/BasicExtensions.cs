using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SimpleRSS.Commons
{
    public static class BasicExtensions
    {
        /// <summary>
        /// 스트링 반환
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetString(this XmlNode element)
        {
            if (element == null
                || element.InnerText == null) return string.Empty;
            return element.InnerText?.Trim();
        }

        /// <summary>
        /// 날짜시간 반환
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this XmlNode element)
        {
            if (element == null
                || element.InnerText == null) return DateTime.MinValue;
            if (int.TryParse(element.InnerText, out int idate))
            {
                return DateTime.Parse(idate.ToString("####-##-##")).ToLocalTime();
            }
            else
            {
                return DateTime.Parse(element.InnerText).ToLocalTime();
            }
        }
    }
}
