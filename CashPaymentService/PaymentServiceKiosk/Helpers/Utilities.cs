using Newtonsoft.Json;
using RestSharp;
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

        public T DeserializeObject<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data);

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
        public static void WriteOnFile(string path, string message)
        {
            using (StreamWriter file = System.IO.File.AppendText(path))
            {
                file.WriteLine(message);
                file.Flush();
                file.Close();
            }

        }

        public static T ReadFile<T>(string file) where T : new()
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }

        }

        public static string GetMachinePid()
        {
            return Security.FinguerValue();
        }

        public static string UrlPdfToBase64(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            string base64String = "";
            using (WebClient client = new WebClient())
            {
                var bytes = client.DownloadData(url);
                base64String = Convert.ToBase64String(bytes);
            }

            return base64String;
        }

        public static bool SaveBase64PdfToLocal(string path, string filename, string base64String)
        {
            try
            {
                if (string.IsNullOrEmpty(base64String))
                    return false;

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

        public bool RetryUntilSuccessOrTimeout(Func<bool> task, TimeSpan timeSpan)
        {
            bool success = false;
            int elapsed = 0;
            while ((!success) && (elapsed < timeSpan.TotalMilliseconds))
            {
                Thread.Sleep(1000);
                elapsed += 1000;
                success = task();
            }
            return success;
        }




        //public static KioskoModel GetKioskoInfo()
        //{
        //    var file = Properties.Settings.Default.CONFIGURATION_PATH;
        //    if (!System.IO.File.Exists(file))
        //        new KioskoController().RequestKioskoConfiguration();

        //    var response = ReadFile<KioskoModel>(file);

        //    if (response == null)
        //    {
        //        new KioskoController().RequestKioskoConfiguration();
        //        response = ReadFile<KioskoModel>(file);
        //    }
        //    return response;
        //}


        public class Security
        {

            private static string fingerPrint = string.Empty;


            public static string FinguerValue()
            {
                if (string.IsNullOrEmpty(fingerPrint))
                {
                    fingerPrint = GetHash("CPU >> " + cpuId() + "\nMAC >> " + baseId() + "\biosId >>" + biosId());

                }
                return fingerPrint;
            }
            private static string GetHash(string s)
            {
                MD5 sec = new MD5CryptoServiceProvider();
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] bt = enc.GetBytes(s);
                return GetHexString(sec.ComputeHash(bt));
            }
            private static string GetHexString(byte[] bt)
            {
                string s = string.Empty;
                for (int i = 0; i < bt.Length; i++)
                {
                    byte b = bt[i];
                    int n, n1, n2;
                    n = (int)b;
                    n1 = n & 15;
                    n2 = (n >> 4) & 15;
                    if (n2 > 9)
                        s += ((char)(n2 - 10 + (int)'A')).ToString();
                    else
                        s += n2.ToString();
                    if (n1 > 9)
                        s += ((char)(n1 - 10 + (int)'A')).ToString();
                    else
                        s += n1.ToString();
                    if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
                }
                return s;
            }
            #region Original Device ID Getting Code
            //Return a hardware identifier
            private static string identifier
            (string wmiClass, string wmiProperty, string wmiMustBeTrue)
            {
                string result = "";
                System.Management.ManagementClass mc =
            new System.Management.ManagementClass(wmiClass);
                System.Management.ManagementObjectCollection moc = mc.GetInstances();
                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (mo[wmiMustBeTrue].ToString() == "True")
                    {
                        //Only get the first one
                        if (result == "")
                        {
                            try
                            {
                                result = mo[wmiProperty].ToString();
                                break;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                return result;
            }
            //Return a hardware identifier
            public static string identifier(string wmiClass, string wmiProperty)
            {
                string result = "";
                System.Management.ManagementClass mc =
            new System.Management.ManagementClass(wmiClass);
                System.Management.ManagementObjectCollection moc = mc.GetInstances();
                foreach (System.Management.ManagementObject mo in moc)
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch (SystemException E)
                        {

                        }
                    }
                }
                return result;
            }
            private static string cpuId()
            {
                //Uses first CPU identifier available in order of preference
                //Don't get all identifiers, as it is very time consuming
                string retVal = identifier("Win32_Processor", "UniqueId");
                if (retVal == "") //If no UniqueID, use ProcessorID
                {
                    retVal = identifier("Win32_Processor", "ProcessorId");
                    if (retVal == "") //If no ProcessorId, use Name
                    {
                        retVal = identifier("Win32_Processor", "Name");
                        if (retVal == "") //If no Name, use Manufacturer
                        {
                            retVal = identifier("Win32_Processor", "Manufacturer");
                        }
                        //Add clock speed for extra security
                        retVal += identifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
                return retVal;
            }
            //BIOS Identifier
            private static string biosId()
            {
                return identifier("Win32_BIOS", "Manufacturer")
                + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                + identifier("Win32_BIOS", "IdentificationCode")
                + identifier("Win32_BIOS", "SerialNumber")
                + identifier("Win32_BIOS", "ReleaseDate")
                + identifier("Win32_BIOS", "Version");
            }
            //Main physical hard drive ID
            private static string diskId()
            {
                return identifier("Win32_DiskDrive", "Model")
                + identifier("Win32_DiskDrive", "Manufacturer")
                + identifier("Win32_DiskDrive", "Signature")
                + identifier("Win32_DiskDrive", "TotalHeads");
            }
            //Motherboard ID
            private static string baseId()
            {
                return identifier("Win32_BaseBoard", "Model")
                + identifier("Win32_BaseBoard", "Manufacturer")
                + identifier("Win32_BaseBoard", "Name")
                + identifier("Win32_BaseBoard", "SerialNumber");
            }
            //Primary video controller ID
            private static string videoId()
            {
                return identifier("Win32_VideoController", "DriverVersion")
                + identifier("Win32_VideoController", "Name");
            }
            //First enabled network card ID
            private static string macId()
            {
                return identifier("Win32_NetworkAdapterConfiguration",
                    "MACAddress", "IPEnabled");
            }
            #endregion
        }

    }
}