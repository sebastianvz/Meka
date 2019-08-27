
using BluLogisticsService.BluServiceReference;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using TCCService.Models;

namespace BluLogisticsService.Services
{
    

    public class BluService
    {
        BluLogisticsService.BluServiceReference.WebServiceFPSoapClient client = new WebServiceFPSoapClient();
        BluLogisticsService.ProduccionBlu.WebServiceSolexSoapClient clientProd = new ProduccionBlu.WebServiceSolexSoapClient();
        ECiudades[] ciudades;
        static EEncabezado eEncabezado = new EEncabezado()

        {

            Usuario = "kioscoMed01",

            Clave = "kioscoMed01 ",

            Version = "",

            Terminal = "888888888888888"

        };
        public BluService()
        {
            ciudades = null;
       
            client.SolicitarCiudadesDestino(ref eEncabezado, ref ciudades);
            // viewLiquidacion preLiquidacion = new viewLiquidacion();



            //  var result = client.GetPreLiquidacion(ref eEncabezado, 2739, 4000, 4, 30, 40, "05001", "11001", ref preLiquidacion);



        }


        public List<object> GetCityList(string departmentCode)
        {
            //    List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");

            List<object> CityList = new List<object>();

            foreach (ECiudades daneList in ciudades.Where(c => c.IdCiudad.Substring(0,2) == departmentCode).OrderBy(c => c.Nombre).ToList())
            {
                CityList.Add(new
                {
                    Name = daneList.Nombre,
                    Code = daneList.IdCiudad
                });
            };

            return CityList;
        }


        public List<object> GetDepartmentList() {



            List<object> DepartmentList = new List<object>();

            foreach (ECiudades daneList in ciudades.GroupBy(c => c.Departamento).Select(c => c.FirstOrDefault()).OrderBy(c => c.Departamento).ToList())
            {
                DepartmentList.Add(new
                {
                    Name = daneList.Departamento,
                    Code = daneList.IdCiudad.Substring(0, 2)
                });
            };


            return DepartmentList;

        }

        public void GetCities()
        {

         

         }

        public ShippingModel GenerateGuide(ShippingModel shipping)
        {



            CustomerServiceModel.Guide guideResponse = new CustomerServiceModel.Guide();
            EFacturaKiosco fact = new EFacturaKiosco();
            fact.Cantidad = shipping.content.Measures.Count;
            fact.CelularDestino = shipping.receiver.Phone;
            fact.Contenido = shipping.content.Description;
            fact.DireccionCliente = shipping.receiver.Location.Address;
            fact.DireccionDestino = shipping.origin.Location.Address;
            fact.FormaPago = "CONTADO";
            fact.IdCuentaCliente = 2739;
            fact.IdDestino = shipping.receiver.Location.CityCode;
            fact.Identificacion = shipping.receiver.Identification;
            fact.IdOrigen = shipping.origin.Location.CityCode;
            fact.Kilos = (float)shipping.content.Measures.Sum(item => item.Weight);
            fact.KilosXvolumen = (int)shipping.content.Measures.Sum(item => item.VolumetricWeight);
            fact.NombreCliente = shipping.origin.Name;
            fact.NombreDestino = shipping.receiver.Name;
            fact.Obs = "Guia generada por kiosko";
            fact.Referencia = "";
            fact.TelefonoCliente = shipping.origin.Phone;
            fact.TelefonoDestino = shipping.receiver.Phone;
            fact.ValorDeclarado = (int)shipping.content.Value;
            
            string factid = "254546";
        
           

           
            EError Er = client.RegistrarFactura(ref eEncabezado, ref fact);
            factid = fact.IdFactura;



            shipping.guide.Code = fact.IdFactura;


            EFacturaKioscoDetalle efactura = new EFacturaKioscoDetalle();


            string Etiqueta = client.ConsultaEtiquetaPdf(ref eEncabezado, factid);


            EError err2= client.ConsultarFactura(ref eEncabezado, factid,  ref efactura);




           
            shipping.guide.PdfGuide = Etiqueta;
            shipping.guide.Code = factid;
           // shipping.guide.Id = factid;
            this.GenerateInvoice(shipping, efactura);

            return shipping;


        }
        public static bool SavePDF(string url, string path, string filename)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            string base64String = "";
            using (WebClient client = new WebClient())
            {
                var bytes = client.DownloadData(url);
                base64String = Convert.ToBase64String(bytes);
            }

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
        public static bool SaveBase64PdfToLocal(string path, string filename, string base64String)
        {
            try
            {
                if (string.IsNullOrEmpty(base64String))
                {
                    //log.Debug("EmptyBase String");
                    return false;
                }
                byte[] bytes = Convert.FromBase64String(base64String);
                FileStream stream = new FileStream(path + filename + ".pdf", FileMode.Create);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
               // log.Error(e.Message, e);
                return false;
            }
        }

        public ShippingModel GenerateInvoice(ShippingModel shipping, EFacturaKioscoDetalle detalle)
        {

            ///////////////////////////////////
            /*             
            Cuando es mensajería  No se imprimen rótulos Siempre debe llevar la bolsa
            Cuando es contado en origen Se imprime Factura, para el usuario
            Remesa (la remesa hay que indicarle al usuario que la meta en la bolsa)

            Cuando es contado en destino (flete contra entrega)
            Se imprime 2 remesas , una para el usuario y la otra hay que indicarle que lo meta en la bolsa
           */

            PrinterSettings setings = new PrinterSettings();
            setings.PrinterName = "KioskoPrinterBill";

            DevExpress.Pdf.PdfPrinterSettings sett = new DevExpress.Pdf.PdfPrinterSettings(setings);

            sett.ScaleMode = DevExpress.Pdf.PdfPrintScaleMode.Fit;
            sett.PageOrientation = DevExpress.Pdf.PdfPrintPageOrientation.Portrait;
            sett.Settings.DefaultPageSettings.Margins.Left = 0;
            sett.Settings.DefaultPageSettings.Margins.Right = 0;
            sett.Settings.DefaultPageSettings.Margins.Bottom = 0;
            sett.Settings.DefaultPageSettings.Margins.Top = 0;
            PageSettings sets = sett.Settings.DefaultPageSettings;

            XtraReport xtrareport;
            FacturaModel fact;
           
            
           
                xtrareport = new Factura();


                fact = new FacturaModel
                {
                    NIT = "830.051.440-7",
                    RazonSocial = "Exxe Logistica S.a.s",
                    Model = shipping,
                    BluDetail= detalle,
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                    Res = new ResolucionDian()
                    {
                        Resolucion = 18762014168428,
                    }
                };
                ///MENSAJERIA


           
           
            List<FacturaModel> ord = new List<FacturaModel>();
            ord.Add(fact);
            PdfExportOptions opts = new PdfExportOptions();

           
            
                try
                {
                    /// SE DESCARGA LA GUIA:
                    /// 
                    string path2 = @"C:\Kiosko\temp\guide\";
                  

                    
                    if (SaveBase64PdfToLocal(path2, shipping.guide.Code, shipping.guide.PdfGuide))
                    {
                        //imprimir guia;

                        using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                        {
                            PrinterSettings settingGuide = new PrinterSettings();
                            settingGuide.PrinterName = "KioskoPrinterBill";

                            DevExpress.Pdf.PdfPrinterSettings settguide = new DevExpress.Pdf.PdfPrinterSettings(settingGuide);

                            pdfViewer.LoadDocument(path2 + shipping.guide.Code + ".pdf");
                            PageSettings setsGG = settguide.Settings.DefaultPageSettings;

                            PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(800 / 2.94));
                            paperSize.RawKind = (int)PaperKind.Custom;

                            setsGG.PaperSize = paperSize;
                            setsGG.Margins.Left = 0;
                            setsGG.Margins.Right = 0;
                            setsGG.Margins.Top = 0;
                            setsGG.Margins.Bottom = 0;
                            pdfViewer.ShowPrintStatusDialog = false;
                            pdfViewer.Print(settguide);
                            pdfViewer.CloseDocument();
                        }
                    }

                    ///SE IMPRIME FACTURA Y ROTULO;
                    xtrareport.DataSource = ord;
                    xtrareport.PrinterName = "KioskoPrinterBill";
                    xtrareport.ShowPrintStatusDialog = false;
                    xtrareport.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    xtrareport.CreateDocument(false);

                    using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                    {
                        string path = @"C:\Kiosko\temp\invoice\" + shipping.guide.Code + ".pdf";
                        xtrareport.ExportToPdf(path);
                        pdfViewer.LoadDocument(path);
                        PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(xtrareport.PageHeight / 2.94));
                        paperSize.RawKind = (int)PaperKind.Custom;
                        sets.PaperSize = paperSize;
                        pdfViewer.ShowPrintStatusDialog = false;
                        pdfViewer.Print(sett);
                        pdfViewer.CloseDocument();
                    }
                }
                catch (Exception E)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "[SavePDF]" + E.Message;
                    return shipping;
                }


            


            return shipping;
        }


        public CustomerServiceModel.Cost GetCost(ShippingModel shipping)
        {
            CustomerServiceModel.Cost costResponse = new CustomerServiceModel.Cost();
            viewLiquidacion preLiquidacion = new viewLiquidacion();


            double VL = shipping.content.Measures.Sum(c => c.VolumetricWeight);
            double VLW = shipping.content.Measures.Sum(c => c.Weight);

            try
            {
                var result = client.GetPreLiquidacion(ref eEncabezado, 2739, (decimal)shipping.content.Value, shipping.content.Quantity, (decimal)VLW, (decimal)VL, shipping.origin.Location.CityCode, shipping.receiver.Location.CityCode, ref preLiquidacion);

                costResponse.MainCost = (float)preLiquidacion.valorFlete;
                costResponse.VariableCost = (float)preLiquidacion.costoManejo;
                costResponse.TotalCost = (float)preLiquidacion.TotalFlete;

            }
            catch(Exception e)
            {

                costResponse.error.HasError = true;
                costResponse.error.Message = e.Message;

            }


            return costResponse;



        }


    }
}