using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Kiosko.Libraries.PosPayment
{
    public class POSController
    {
        private static string INPUT_FILE = @"C:\Program Files\SIP\Cajas5.0x64\IN.txt";
        private static string OUTPUT_FILE = @"C:\Program Files\SIP\Cajas5.0x64\OUT.txt";
        public POSController()
        {

        }

        public POSModel.Response RequestPayment(string amount)
        {
            POSModel.Response response = new POSModel.Response()
            {
                Success = false,
                Message = "Ha ocurrido un error interno."
            };

            POSModel.Purchase purchase = new POSModel.Purchase()
            {
                OperationCode = "0",
                Amount = amount,
                Vat = "0",
                BasDev = "0",
                Invoice = "123456",
                ATMCode = "1",
                ConsumptionTax = "0"
            };

            response = DoPayment(purchase);

            return response;
        }


        private POSModel.Response DoPayment(POSModel.Purchase purchase)
        {
            int attempts = 0;
            bool file_exists = false;
            int timeout = 120000;

            //Check if a Redeban process is running and kill it
            List<Process> RedebanWinProcess = Process.GetProcesses().Where(s => s.ProcessName.Equals("Cajas5.2.3")).ToList(); ;
            foreach (Process Proc in RedebanWinProcess)
                Proc.Kill();

            //Reset input and output files
            if (File.Exists(INPUT_FILE)) { File.Delete(INPUT_FILE); }
            if (File.Exists(OUTPUT_FILE)) { File.Delete(OUTPUT_FILE); }

            System.Threading.Thread.Sleep(2000);
            CreatePlainText(purchase.ToString());
            ExecuteClient();


            while (file_exists == false && attempts <= timeout / 1000)
            {
                file_exists = File.Exists(OUTPUT_FILE);
                System.Threading.Thread.Sleep(1000);
                attempts++;
            }


            if (!file_exists)
            {
                POSModel.Response response = new POSModel.Response
                {
                    ResponseCode = "05",
                    Success = false
                };
                return response;
            }

            string data = File.ReadAllText(OUTPUT_FILE);

            return ProcessFileResponse(data);

        }


        private void ExecuteClient()
        {
            Process.Start(@"C:\\Program Files\SIP\Cajas5.0x64\Cajas5.2.3.exe");
        }


        private void CreatePlainText(string data)
        {
            File.WriteAllText(INPUT_FILE, data);
        }



        public POSModel.Response ProcessFileResponse(string data)
        {
            POSModel.Response response = new POSModel.Response()
            {
                Success = false,
                Message = "Hubo un error interno."
            };

            if (string.IsNullOrEmpty(data))
            {
                return response;
            }

            List<string> t = data.Split(',').Select(s => s).ToList();

            if (t[0] == "")
                return response;

            if (t[0] == "99")
            {
                response.Message = t[1];
                return response;
            }

            switch (t[0])
            {
                case "00": response.Success = true; response.Message = "Pago realizado correctamente."; break;
                case "01": response.Message = "Transación declinada."; break;
                case "02": response.Message = "Clave incorrecta."; break;
                case "04": response.Message = "No hay respuesta con la entidad financiera."; break;
                case "05": response.Message = "Ha superado el tiempo límite para pagar."; break;
                default: response.Message = "Ha ocurrido un error interno: La operación no existe"; break;
            }

            response.Authorization = t[1];
            response.Card = new POSModel.Card()
            {
                Number = t[2],
                Franchise = t[3],
                Type = t[4]
            };
            response.Amount = t[5];
            response.Vat = t[6];
            response.BasDev = t[7];
            response.Receipt = t[8];

            return response;

        }
    }
}