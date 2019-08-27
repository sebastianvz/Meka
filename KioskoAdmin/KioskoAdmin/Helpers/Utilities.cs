using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using KioskoAdmin.Models;
using System.Threading;

namespace KioskoAdmin.Helpers
{
    public class Utilities
    {
        /**
             * Make a HTTP request using Rest Client
             * 
             * @params
             * String url
             * String resource         
             * Example of usage:      
             * -> url : "localhost"
             * -> resource : "api/test"
             * 
             * var res = doRequest<model>(url , resource)
             * 
             * returns model
             * */
        public static T doRequest<T>(string url, string resource, object parameters = null, string type = "get", int timeout = 5000) where T : new()
        {
            RestClient restclient;
            restclient = new RestClient
            {
                BaseUrl = new Uri(url)
            };

            var method = (type.ToLower().Equals("post")) ? Method.POST : Method.GET;

            var request = new RestRequest(method)
            {
                Resource = resource,
                RequestFormat = DataFormat.Json,
                Timeout = timeout
            };

            if (parameters != null)
            {
                request.AddHeader("Accept", "application/json charset=utf-8");
                request.Parameters.Clear();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            var response = restclient.Execute<T>(request);
            if (response.Data == null)
            {
                WriteLocalLog("Error on DoRequest: [" + url + "/" + resource + "] WHY: " + response.ErrorMessage);
            }

            return response.Data;

        }

        public static bool ValidateApiSession(User user)
        {
            if (user == null)
                return false;

            string _sessionKey = GetSessionKey(user).ToLower();
            
            return _sessionKey.Equals(user.session.ToLower());
        }

        public static  string GetSessionKey(User user)
        {
            string sessionKey = "";

            string date = DateTime.Now.ToString("yyyyMMdd");

            sessionKey = user.email + date;
            return GetStringSha256Hash(sessionKey);
        }
        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
        public T DeserializeObject<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data);

        }

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static void WriteLocalLog(string message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                message = "<p>[" + GetDate() + "] " + message + "</p>";
                WriteOnFile("C:/Kiosko/log.html", message);
            }

        }

        public static void WriteOnFile(string path, string message)
        {
            using (StreamWriter file = System.IO.File.AppendText(path))
            {
                file.WriteLine(message);
                file.Flush();
                file.Close();
            }

        }
        public static void WriteJson(string path, object data)
        {
            EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");

            waitHandle.WaitOne();
            using (StreamWriter file = System.IO.File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
                file.Close();
            }
            waitHandle.Set();
        }

        public static T ReadFile<T>(string file) where T : new()
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }

        }
    }
}