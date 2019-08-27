using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Web;
using System.IO;
using System.Printing;
using static Kiosko.Models.Common;

using System.Text;
using System.Drawing.Printing;
using DevExpress.XtraPrinting.Export.Pdf;

namespace Kiosko.Services
{
    public class PrinterService
    {

        private string FilePath;
        private string PrinterName;

        public void SetPrinterName(string _printerName)
        {
            PrinterName = _printerName;
        }

        public void SetFileName(string _file)
        {
            FilePath = _file;
        }

        public bool Test()
        {

            PrintDocument pdoc = new PrintDocument();
            pdoc.DefaultPageSettings.PrinterSettings.PrinterName = "Zebra KR203";



            return true;


        }

        private void Pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {

        }

        public Error Print(int pageHeight)
        {
            Error response = new Error();
            if (!File.Exists(FilePath))
            {
                response.HasError = true;
                response.Message = "Error: El archivo << " + FilePath + " >> no se encuentra.";
            }


            if (!PrinterExists(PrinterName))
            {
                response.HasError = true;
                response.Message = "La impresora << " + PrinterName + " >> no está conectada.";
               
            }

            var printerStatus = CheckPrinterStatus(PrinterName);

            if (printerStatus.HasError)
            {
                return printerStatus;
            }

            try
            {
                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    pdfViewer.LoadDocument(FilePath);

                    PrinterSettings setings = new PrinterSettings();
                    setings.PrinterName = @PrinterName;
                    
                    DevExpress.Pdf.PdfPrinterSettings sett = new DevExpress.Pdf.PdfPrinterSettings(setings);

                    sett.ScaleMode = DevExpress.Pdf.PdfPrintScaleMode.Fit;
                   // sett.PageOrientation = DevExpress.Pdf.PdfPrintPageOrientation.Portrait;
                    sett.Settings.DefaultPageSettings.Margins.Left = 0;
                    sett.Settings.DefaultPageSettings.Margins.Right = 0;
                    sett.Settings.DefaultPageSettings.Margins.Bottom = 0;
                    sett.Settings.DefaultPageSettings.Margins.Top = 0;
                    //sett.Settings.PrintToFile = true;

                    pdfViewer.ShowPrintStatusDialog = true;
                    pdfViewer.Print(sett);
                    pdfViewer.CloseDocument();

                    response.HasError = false;
                }
            }catch (Exception e){
                response.HasError = true;
                response.Message = "[PrinterService] " + e.Message;
            }

            return response;
        }
           
            //ProcessStartInfo info = new ProcessStartInfo
            //{

            //    Verb = "PrintTo",
            //    FileName = @"" + FilePath,
            //    Arguments = "\"" + PrinterName + "\"",

            //    CreateNoWindow = true,
            //    WindowStyle = ProcessWindowStyle.Normal,
            //    UseShellExecute = true,
            //    ErrorDialog = true
            //};
            //try
            //{
            //    Process p = new Process();
            //    p.StartInfo = info;
            //    p.Start();
            //    p.WaitForInputIdle();
            //    System.Threading.Thread.Sleep(3000);
            //    return new Error()
            //    {
            //        HasError = false
            //    };
            //}
            //catch (Exception e)
            //{
            //    return new Error()
            //    {
            //        HasError = true,
            //        Message = e.Message + " [ " + FilePath + " ]",
            //    };
            //}

        


        /**
         * 
         * This function Check if the printer Exists
         * 
         * */
        public static bool PrinterExists(string printerName)
        {
            try
            {
                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();
                ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                foreach (ManagementObject printer in searcher.Get())
                {
                    if (printer["Name"].ToString().ToLower().Equals((@printerName).ToLower()))
                    {
                        var x = printer["WorkOffline"];
                        return printer["WorkOffline"].ToString().ToLower().Equals("false");
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /**
         * This function check if the printer has some problem
         * */
        public KioskoServiceStatus CheckService()
        {
            KioskoServiceStatus ServiceStatus = new KioskoServiceStatus(CubiQManagerModel.KioskoService.PRINTERSERVICE);
          
            List<String> KioskoPrinter = new List<String>()
            {
                "KioskoPrinterGuide" ,"KioskoPrinterBill"
            };
            
            foreach(string Printer in KioskoPrinter)
            {
                //Check if printer exists
                if (!PrinterExists(Printer))
                {
                    ServiceStatus.Message += "[" + Printer + ":] not connected";
                    ServiceStatus.Active = false;
                }

                //Check for printer status
                Error statusError = CheckPrinterStatus(Printer);

                if (statusError.HasError)
                {
                    ServiceStatus.Message += "[" + Printer + ":]" + statusError.Message;
                    ServiceStatus.Active = false;
                }
            }
                     

            return ServiceStatus;
        }


        /**
         * This funcion return an error message of the printer
         * 
         * */
        public Error CheckPrinterStatus(string printerName)
        {
            Error _error = new Error();
            try
            {
                //Get local print server
                var server = new LocalPrintServer();

                //Load queue for correct printer
                PrintQueue queue = server.GetPrintQueue(printerName, new string[0] { });

                //Check some properties of printQueue    
                bool isInError = queue.IsInError;
                bool isOutOfPaper = queue.IsOutOfPaper;
                bool isOffline = queue.IsOffline;
                bool isBusy = queue.IsBusy;

                _error.HasError = (isInError || isOutOfPaper || isOffline || isBusy);
                _error.Message = queue.QueueStatus.ToString();
            }
            catch(Exception e)
            {
                _error.HasError = true;
                _error.Message = e.Message;
            }

            return _error;
            
        }
    }
}