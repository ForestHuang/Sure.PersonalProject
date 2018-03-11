namespace Sure.PersonalProject.Utilities
{
    using Newtonsoft.Json;
    /*---------------------------------------------------------------------
[author]:senlin.huang
[time]:2017-8-14
[explain]: HttpGeneral  
-----------------------------------------------------------------------*/
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// HTTP 请求
    /// </summary>
    public static class HttpGeneral
    {
        /// <summary>
        /// GET 
        /// </summary>
        /// <param name="url">请求地址,URLEncode</param>
        /// <param name="encoding">格式,默认UTF-8</param>
        /// <returns>返回结果</returns>
        public static string GET(string url, Encoding encoding = null)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Proxy = null;
                webClient.Encoding = encoding ?? Encoding.UTF8;
                string result = webClient.DownloadString(url);
                result = JsonTree(result);
                return Regex.Unescape(result);
            }
            catch (Exception ex)
            {
                return "GET Message: " + ex.Message;
            }
        }

        /// <summary>
        /// JSON字符串格式化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonTree(string json)
        {
            int level = 0;
            var jsonArr = json.ToArray(); // Using System.Linq;
            string jsonTree = string.Empty;
            for (int i = 0; i < json.Length; i++)
            {
                char c = jsonArr[i];
                if (level > 0 && '\n' == jsonTree.ToArray()[jsonTree.Length - 1])
                {
                    jsonTree += TreeLevel(level);
                }
                switch (c)
                {
                    case '[':
                        jsonTree += c + "\n";
                        level++;
                        break;
                    case ',':
                        jsonTree += c + "\n";
                        break;
                    case ']':
                        jsonTree += "\n";
                        level--;
                        jsonTree += TreeLevel(level);
                        jsonTree += c;
                        break;
                    default:
                        jsonTree += c;
                        break;
                }
            }
            return jsonTree;
        }

        /// <summary>
        /// 树等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static string TreeLevel(int level)
        {
            string leaf = string.Empty;
            for (int t = 0; t < level; t++)
            {
                leaf += "\t";
            }
            return leaf;
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="url">调用地址</param>
        /// <param name="requestData">入参</param>
        /// <returns>返回参</returns>
        public static string POST(string url, string requestData = "")
        {
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(requestData);
                HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(new Uri(url));
                httpWebRequest.Method = "POST";
                httpWebRequest.ServicePoint.Expect100Continue = false;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = postData.Length;
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(postData, 0, postData.Length);
                }
                HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responeData = stream.ReadToEnd();
                response.Close();
                stream.Close();
                return responeData;
            }
            catch (Exception ex)
            {
                return "POST Message: " + ex.Message;
            }
        }

        /// <summary>
        /// Url Encode
        /// </summary>
        /// <param name="str">入参</param>
        /// <returns>返回</returns>
        public static string UrlEncode(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte[] _byte = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < _byte.Length; i++)
            {
                stringBuilder.Append(@"%" + Convert.ToString(_byte[i], 16));
            }
            return (stringBuilder.ToString());
        }

        /// <summary>
        /// UTC 时间校正
        /// </summary>
        /// <param name="queryStr">入参</param>
        /// <returns>返回</returns>
        public static string CorrectingDateTime(string queryStr)
        {
            var result = string.Empty;

            //将时间正确转化:\\/Date(1501053660000)\\/\  -->  2017-07-26 15:50:00
            result = Regex.Replace(queryStr, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dateTime = new DateTime(1970, 1, 1);
                dateTime = dateTime.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dateTime = dateTime.ToLocalTime();
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            });

            //将时间正确转化: \\/Date(-2209017600000)\\/\ --> ""  传过来的时间不正确 
            result = Regex.Replace(queryStr, @"\\/Date\(\-(\d+)\)\\/", match => { return string.Empty; });
           
            return result;
        }
    }
}