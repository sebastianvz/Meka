using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using static Kiosko.Models.Common;

namespace Kiosko.Controllers
{
    public class PrinterController
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PrinterService _printerService;
        //string filepath = @"C:\Kiosko\temp\guide\";
        public PrinterController()
        {
            _printerService = new PrinterService();
        }

        /**
         * Prints pdf guide Models.CustomerServiceModel.Guide
         *
         */
        public Error PrintGuide(Models.ShippingModel.Guide guide)
        {
            /**
             * Some customers returns the GUIDE (pdf) in URL format.
             * The idea is to keep all as base64
             * */

            log.Debug("test");

            if (string.IsNullOrEmpty(guide.PdfGuide))
            {
                guide.PdfGuide = Helpers.Utilities.UrlPdfToBase64(guide.Url);
            }

            string fileGuide = Properties.Settings.Default.GUIDE_PATH + guide.Code +".pdf";
            //attempt to save guide locally
            if (!File.Exists(fileGuide))
            {
                if (!Helpers.Utilities.SaveBase64PdfToLocal(Properties.Settings.Default.GUIDE_PATH, guide.Code, guide.PdfGuide))
                {

                    return new Error()
                    {
                        HasError = true,
                        Message = "Error on SaveBase64PdfToLocal"
                    };
                }
            }
            

            try
            {

                PrinterSettings setings = new PrinterSettings();
                setings.PrinterName = "KioskoPrinterGuide";

                DevExpress.Pdf.PdfPrinterSettings sett = new DevExpress.Pdf.PdfPrinterSettings(setings);

                sett.ScaleMode = DevExpress.Pdf.PdfPrintScaleMode.ActualSize;
                ////sett.PageOrientation = DevExpress.Pdf.PdfPrintPageOrientation.Landscape;
                sett.Settings.DefaultPageSettings.Margins.Left = 20;
                sett.Settings.DefaultPageSettings.Margins.Right = 0;
                sett.Settings.DefaultPageSettings.Margins.Bottom = 80;
                sett.Settings.DefaultPageSettings.Margins.Top = 0;
                PageSettings sets = sett.Settings.DefaultPageSettings;
                sets.Margins.Top = 0;
                sets.Margins.Left = 0;

                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    string path = @Properties.Settings.Default.GUIDE_PATH + guide.Code + ".pdf";

                    pdfViewer.LoadDocument(path);
                    PaperSize paperSize = new PaperSize("Print", (int)(900 / 2.94), (int)(1800 / 2.94));
                    paperSize.RawKind = (int)PaperKind.Custom;
                    sets.PaperSize = paperSize;
                    pdfViewer.ShowPrintStatusDialog = false;
                    if (!_printerService.CheckPrinterStatus(setings.PrinterName).HasError)
                    {
                        _printerService.CheckPrinterStatus(setings.PrinterName);
                        pdfViewer.Print(sett);
                        pdfViewer.CloseDocument();
                    }
                    else
                    {
                        return new Error()
                        {
                            HasError = true,
                            Message = _printerService.CheckPrinterStatus(setings.PrinterName).Message
                        };
                    }

                }

            }catch(Exception E)
            {

                return new Error()
                {
                    HasError = true,
                    Message = E.Message
                }; 

            }
            //_printerService.SetPrinterName("KioskoPrinterGuide");
            //_printerService.SetFileName(Properties.Settings.Default.GUIDE_PATH + guide.Code + ".pdf");
            //var printResponse = _printerService.Print(150);


            return  new Error()
            {
                HasError = false,
                Message = ":)"
            }; ;

        }

        /**
         * Prints pdf bill
         *
         */
        public Error PrintBill(string invoice)
        {
            _printerService.SetPrinterName("KioskoPrinterBill");
            _printerService.SetFileName(Properties.Settings.Default.INVOICE_PATH + invoice + ".pdf");
            var printResponse = _printerService.Print(250);

            return printResponse;
        }
    }
}