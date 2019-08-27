
using CoordinadoraService.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Kiosko.Helpers
{
    public class Utilities
    {


        public static ResolucionDian GetResolucion()
        {

            ResolucionDian res = new ResolucionDian();
            res.Fecha = new DateTime(2017, 12, 19);
                res.Resolucion = 18762006191658;
            res.InicioNumeracion = 90;
            res.FinNumeracion = 9999;
            return res;

        }




        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static bool SaveBase64PdfToLocal(string path, string filename, string base64String)
        {
            try
            {
                if (string.IsNullOrEmpty(base64String))
                {
                   
                    return false;
                }
                byte[] bytes = Convert.FromBase64String(base64String);
                FileStream stream = new FileStream(path + filename + ".pdf", FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
              
                return false;
            }
        }

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
        public static T doRequest<T>(string url, string resource, object parameters = null, string type = "get", int timeout = 5000, string token = null) where T : new()
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
            if(token!= null)
            {
                restclient.Authenticator = new HttpBasicAuthenticator("camilo.garcia@mekagroupcol.com", "26227c0b2a984dfb01a7");
                //request.AddHeader("Authorization: Basic", token);

            }
            if (parameters != null)
            {
               // request.AddHeader("Accept", "application/json charset=utf-8");
               
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

        public T DeserializeObject<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data);

        }
        public static void WriteLocalLog(string message)
        {

            //Thread.Sleep(1000);
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
        public string SerializeObject<T>(string pattern) where T : new()
        {

            /* var text = string.Join
             (
                 ",",
                 <T>.GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Select
                     (
                         prop => prop.GetValue(obj).ToString()
                     )
             );
             */

            return "1";
        }
        public static T ReadFile<T>(string file) where T : new()
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
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

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

       
        

       
      
     

      
     
       
       



     
   

     

  
    }
}