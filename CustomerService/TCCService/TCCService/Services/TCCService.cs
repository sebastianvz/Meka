using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCCService.Models;

using System.Xml.Serialization;
using System.IO;
using System.Xml;

using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Drawing.Printing;
using TCCService.co.com.tcc.clientes.remesas;
using TCCService.co.com.tcc.clientes.liquidacion;
using DevExpress.Pdf;
using Newtonsoft.Json;
using log4net;
using System.Reflection;

namespace TCCService.Services
{
    public class TCCService
    {
        wsDespachosSoapQSService _dispatchesService;
        LiquidacionAcuerdos _paymentService;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public TCCService()
        {


            _paymentService = new LiquidacionAcuerdos();
            _dispatchesService = new wsDespachosSoapQSService();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
        }
        public static T ReadFile<T>(string file) where T : new()
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }

        }

        public List<object> GetDepartmentList()
        {


            List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");

            List<object> DepartmentList = new List<object>();

            foreach (TCCDaneList daneList in Departamentos.GroupBy(c => c.DEPARTAMENTO).Select(c => c.FirstOrDefault()).OrderBy(c=>c.DEPARTAMENTO).ToList())
            {
                DepartmentList.Add(new
                {
                    Name = daneList.DEPARTAMENTO,
                    Code = daneList.DANE.Substring(0, 2)
                });
            };


            return DepartmentList;
        }

        public TCCDaneList GetCityByCode(string code)
        {
            List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");


            return Departamentos.Find(x => x.DANE == code);
        }

        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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
        public List<object> GetCityList(string departmentCode)
        {
            List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");

            List<object> CityList = new List<object>();

            foreach (TCCDaneList daneList in Departamentos.Where(c => c.DANE.Substring(0,2) == departmentCode && c.ESTADO=="A").OrderBy(c => c.NOMBRE_POBLACION).ToList())
            {
                CityList.Add(new
                {
                    Name = daneList.NOMBRE_POBLACION,
                    Code = daneList.DANE
                });
            };

            return CityList;
        }
        public static bool Serialize<T>(T value, ref string serializeXml)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);

                xmlserializer.Serialize(writer, value);

                serializeXml = stringWriter.ToString();

                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public CustomerServiceModel.Cost GetCost(ShippingModel shipping)
        {
            CustomerServiceModel.Cost costResponse = new CustomerServiceModel.Cost();

            int unidad = ReturnUnidadDeNegocio(shipping);

            Liquidacion liquidacion = new Liquidacion
            {
                boomerang = "0",
                tipoenvio = "1",
                idciudadorigen = "05001000",
                idciudaddestino = shipping.receiver.Location.CityCode,
                valormercancia = shipping.content.Value.ToString().Replace("." , "").Replace("," , ""),
                cuenta = "",
                fecharemesa = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                idunidadestrategicanegocio = unidad.ToString(),

            };

            co.com.tcc.clientes.liquidacion.unidad[] _unidades = new co.com.tcc.clientes.liquidacion.unidad[shipping.content.Measures.Count];

            int i = 0;

            if (unidad == 1)
            {
                foreach (var measure in shipping.content.Measures)
                {
                    measure.VolumetricWeight = measure.VolumetricWeight == 0 ? 1 : measure.VolumetricWeight;
                    measure.Weight = measure.Weight == 0 ? 1 : measure.Weight;

                    co.com.tcc.clientes.liquidacion.unidad x = new co.com.tcc.clientes.liquidacion.unidad
                    {
                        alto = measure.Height.ToString(),
                        ancho = measure.Width.ToString(),
                        largo = measure.Length.ToString(),
                        numerounidades = "1",
                        pesoreal = measure.Weight.ToString(),
                        pesovolumen = measure.VolumetricWeight.ToString(),
                        tipoempaque = ""

                    };
                    _unidades[i] = x;
                    i++;
                }



            }
            else
            {
                shipping.content.Measures[0].Weight= shipping.content.Measures[0].Weight == 0 ? 1 : shipping.content.Measures[0].Weight;
                shipping.content.Measures[0].VolumetricWeight = shipping.content.Measures[0].VolumetricWeight == 0 ? 1 : shipping.content.Measures[0].VolumetricWeight;
                _unidades[0] = new co.com.tcc.clientes.liquidacion.unidad
                {
                    alto = shipping.content.Measures[0].Height.ToString(),
                    ancho = shipping.content.Measures[0].Width.ToString(),
                    largo = shipping.content.Measures[0].Length.ToString(),
                    numerounidades = "1",
                    pesoreal = shipping.content.Measures[0].Weight.ToString(),
                   
                    pesovolumen = shipping.content.Measures[0].VolumetricWeight.ToString(),
                    tipoempaque = " ",
                    

                };



                //_unidades[0] = new co.com.tcc.clientes.liquidacion.unidad
                //{
                //    alto = shipping.content.Measures[0].Height.ToString(),
                //    ancho = shipping.content.Measures[0].Width.ToString(),
                //    largo = shipping.content.Measures[0].Length.ToString(),
                //    dicecontener = shipping.content.Description,
                //    kilosreales = shipping.content.Measures[0].Weight > shipping.content.Measures[0].VolumetricWeight ? shipping.content.Measures[0].Weight.ToString() : shipping.content.Measures[0].VolumetricWeight.ToString(),
                //    valormercancia = shipping.content.Value.ToString(),
                //    pesovolumen = "1",
                //    tipoempaque = " ",
                //    tipounidad = "TIPO_UND_PAQ",
                //    claseempaque = "CLEM_CAJA",
                //    codigobarras = " ",
                //    numerobolsa = " ",
                //    referencias = " "

                //};


            }



            liquidacion.unidades = _unidades;

            
            try
            {
                string user = "CLIENTETCC608W3A61CJ";

                log.Debug(liquidacion);

                var request = _paymentService.consultarliquidacion(user, liquidacion);
                if (request.respuesta.codigo != "0")
                {
                    costResponse.error.HasError = true;
                    costResponse.error.Message = request.respuesta.mensaje + " | | " + request.respuesta.mensajeinterno + " usuario: " + user;
                    return costResponse;
                }
                
                costResponse.VariableCost = float.Parse(request.subtotales[0].valor, CultureInfo.InvariantCulture.NumberFormat);
                costResponse.MainCost = float.Parse(request.subtotales[1].valor, CultureInfo.InvariantCulture.NumberFormat);

                costResponse.TotalCost = float.Parse(request.total.totaldespacho);
            }
            catch (Exception e)
            {
                costResponse.error.HasError = true;
                costResponse.error.Message = e.Message;
            }

            return costResponse;
        }

        private int ReturnUnidadDeNegocio(ShippingModel shipping)
        {
            int unidad = 1;
            if (shipping.content.Measures.Sum(t => t.Weight) <= 5 && shipping.content.Measures.Sum(t => t.VolumetricWeight) <= 5)
            {
                return unidad = 2;
            }
            else
            {
                return unidad;
            }
        }

        public ShippingModel GenerateInvoice(ShippingModel shipping)
        {

            int Weight=0;
            int VolWeight=0;

            foreach(var model in shipping.content.Measures)
            {
                Weight +=(int) Math.Round(model.Weight, 0);
                VolWeight += (int)Math.Round(model.VolumetricWeight, 0);

                model.VolumetricWeight = Math.Round(model.VolumetricWeight, 0);
                model.Weight = Math.Round(model.Weight, 0);

            }

           int ValorFactura = Weight > VolWeight ? Weight : VolWeight;


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
              

            XtraReport xtrareport;
            FacturaModel fact;
            string FormaDePago = "";
            if (shipping.payment.PaymentMethod == "COD")
            {
                FormaDePago = "CONTADO EN DESTINO";
               
                ///CONTADO EN ORIGEN;
            }else
            {

                FormaDePago = "CONTADO EN ORIGEN";

            }
          
            int unidaddenegocio = ReturnUnidadDeNegocio(shipping);
            if (unidaddenegocio == 1)
            {


                ////PAQUETERIA
                xtrareport = new RemesaTCC();


                fact = new FacturaModel
                {
                    NIT = "860.016.640",
                    RazonSocial = "TCC S.A.S",
                    Model = shipping,
                    FormaDePago = FormaDePago,
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                    ValorFactura= ValorFactura,
                    Weight= Weight,
                    VolWeight= VolWeight,
                    Res = new ResolucionDian()
                    {
                        Resolucion = 18762014168428,
                    }
                };
                ///MENSAJERIA
               

            }
            else
            {



                xtrareport = new Factura();

                fact = new FacturaModel
                {
                    NIT = "900.053.978-6",
                    RazonSocial = "GLOBAL MENSAJERIA S.A.S",
                    Model = shipping,
                    FormaDePago = FormaDePago,
                    FactNumber = shipping.payment.Invoice,
                    ValorFactura= ValorFactura,
                    Weight = Weight,
                    VolWeight = VolWeight,
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                    Res = new ResolucionDian() { Resolucion = 18762012374330 }

                };


            }
            List<FacturaModel> ord = new List<FacturaModel>();
            ord.Add(fact);              
            PdfExportOptions opts = new PdfExportOptions();

            unidaddenegocio = ReturnUnidadDeNegocio(shipping);
            if (unidaddenegocio == 1)
            {
                try
                {
                    ////PAQUETERIA CONTADO EN DESTINO Y ORIGEN, IMPRIME REMESA


                    ////PAQUETERIA NO IMPRIME FACTURA

                   

                    /// SE DESCARGA LA GUIA:
                    /// 
                    string path2 = @"C:\Kiosko\temp\guide\";

                   if(SavePDF(shipping.guide.Url, path2, shipping.guide.Code))
                    {
                        //imprimir guia;

                        using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                        {
                            PrinterSettings settingGuide = new PrinterSettings();
                            settingGuide.PrinterName = "KioskoPrinterGuide";
                           
                            DevExpress.Pdf.PdfPrinterSettings settguide = new DevExpress.Pdf.PdfPrinterSettings(settingGuide);

                            pdfViewer.LoadDocument(path2+ shipping.guide.Code + ".pdf");
                            PageSettings setsGG = settguide.Settings.DefaultPageSettings;

                            PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(1500 / 2.94));
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


                    shipping = this.PrintRemesa(ord, shipping, sett);
                   



                }
                catch (Exception E)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "[SavePDF]" + E.Message;
                    return shipping;
                }


            }
            else
            {

                ////MENSAJERIA CONTADO EN ORIGEN, IMPRIME FACTURA Y UNA REMESA


               ////MENSAJERIA CONTADO EN DESTINO, SE IMPRIMEN 2 REMESAS.


                if(fact.FormaDePago== "CONTADO EN DESTINO")
                {



                    shipping = this.PrintRemesa(ord, shipping, sett);
                    if (!shipping.error.HasError)
                    {
                        shipping = this.PrintRemesa(ord, shipping, sett);
                    }else
                    {
                        return shipping;
                    }


                }
                else
                {

                    shipping = this.PrintRemesa(ord, shipping, sett);

                    if (!shipping.error.HasError)
                    {
                        shipping = this.PrintFactura(ord, shipping, sett);
                    }
                    else
                    {
                        return shipping;
                    }



                }


               
            }         

            return shipping;
        }

        public ShippingModel PrintFactura(List<FacturaModel> ord, ShippingModel shipping, PdfPrinterSettings sett)
        {
            try
            {
                shipping.error.HasError = false;
                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    PageSettings sets = sett.Settings.DefaultPageSettings;
                    XtraReport rem = new Factura();
                    rem.DataSource = ord;
                    rem.PrinterName = "kioskobillprint";
                    rem.ShowPrintStatusDialog = false;
                    rem.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    string path = @"C:\Kiosko\temp\invoice\" + shipping.guide.Code + ".pdf";
                    rem.CreateDocument(false);
                    rem.ExportToPdf(path);
                    rem.ExportToPdf(path);
                    PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(rem.PageHeight / 2.94));
                    paperSize.RawKind = (int)PaperKind.Custom;
                    sets.PaperSize = paperSize;
                    pdfViewer.LoadDocument(path);
                    pdfViewer.ShowPrintStatusDialog = false;
                    pdfViewer.Print(sett);
                    pdfViewer.CloseDocument();
                }
            }
            catch (SystemException E)
            {
                shipping.error.HasError = true;
                shipping.error.Message = E.Message;

                return shipping;


            }
            return shipping;


        }

        public ShippingModel PrintRemesa(List<FacturaModel> ord, ShippingModel shipping, PdfPrinterSettings sett)
        {   try
            {
                shipping.error.HasError = false;
                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    PageSettings sets = sett.Settings.DefaultPageSettings;
                    XtraReport rem = new RemesaTCC();
                    rem.DataSource = ord;
                    rem.PrinterName = "kioskobillprint";
                    rem.ShowPrintStatusDialog = false;
                    rem.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    string path = @"C:\Kiosko\temp\invoice\" + shipping.guide.Code + ".pdf";
                    rem.CreateDocument(false);
                    rem.ExportToPdf(path);
                    rem.ExportToPdf(path);
                    pdfViewer.LoadDocument(path);
                    PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(rem.PageHeight / 2.94));
                    paperSize.RawKind = (int)PaperKind.Custom;
                    sets.PaperSize = paperSize;
                    pdfViewer.ShowPrintStatusDialog = false;
                    pdfViewer.Print(sett);
                    pdfViewer.CloseDocument();
                }
            }catch(SystemException E)
            {
                shipping.error.HasError = true;
                shipping.error.Message = E.Message;

                return shipping;


            }
            return shipping;

        }


        public ShippingModel GenerateGuide(ShippingModel shipping)
        {
            int unidad = this.ReturnUnidadDeNegocio(shipping);



            shipping.shippingType.KeyName = ShippingModel.ShippingTypeKeyName.PACKAGE;
            if (unidad==2)
            {
              
                shipping.shippingType.KeyName = ShippingModel.ShippingTypeKeyName.LABEL;
            }
            ////SI EL PAGO ES EN DESTINO, FORMA DE PAGO TIENE QUE IR VACIO.

            CustomerServiceModel.Guide guideResponse = new CustomerServiceModel.Guide();

            remesa despacho = new remesa()
            {
                clave = "KIOSKO001K19WSJEL5AT",
                numerorelacion = "",
                fechahorarelacion = "",
                solicitudrecogida = new recogida()
                {
                    numero = "",
                    fecha = "",
                    ventanainicio = "",
                    ventanafin = ""
                },
                unidadnegocio = unidad.ToString(),
                numeroremesa = "",
                fechadespacho = DateTime.Now.ToString("yyyy-MM-dd"),
                cuentaremitente = "-1",

                tipoidentificacionremitente = shipping.origin.IdentificationType,
                identificacionremitente = shipping.origin.Identification,
                sederemitente = "",
                primernombreremitente = shipping.origin.Name,
                segundonombreremitente = " ",
                primerapellidoremitente = " ",
                segundoapellidoremitente = " ",
                razonsocialremitente = shipping.origin.Name,
                naturalezaremitente = shipping.origin.IdentificationType.Equals("NIT") ? "J" : "N",
                direccionremitente =shipping.origin.Location.Address,
                contactoremitente = shipping.origin.Phone,
                emailremitente = shipping.origin.Email,
                telefonoremitente = shipping.origin.Phone,
                ciudadorigen = "05001000",
                tipoidentificaciondestinatario = shipping.receiver.IdentificationType,
                identificaciondestinatario = shipping.receiver.Identification,
                sededestinatario = " ",
                barriodestinatario = " ",
                primernombredestinatario = shipping.receiver.Name,
                segundoapellidodestinatario = " ",
                segundonombredestinatario = " ",
                primerapellidodestinatario = " ",
                razonsocialdestinatario = shipping.receiver.Name,
                naturalezadestinatario = shipping.receiver.IdentificationType.Equals("NIT") ? "J" : "N",
                direcciondestinatario = shipping.receiver.Location.Address,
                contactodestinatario = " ",
                emaildestinatario = shipping.receiver.Email,
                telefonodestinatario = shipping.receiver.Phone,
                ciudaddestinatario = shipping.receiver.Location.CityCode,
                totalpeso = shipping.content.Measures.Sum(t => t.Weight).ToString(),
                totalpesovolumen = shipping.content.Measures.Sum(t => t.VolumetricWeight).ToString(),
                totalvalormercancia = shipping.content.Value.ToString().Replace(".", "").Replace(",", ""),
                observaciones = "Guia generada por Kiosko",
                formapago = shipping.payment.PaymentMethod == "COD" ? "2":"1",
                recogebodega = "",
                llevabodega = "",
                centrocostos = "",
                totalvalorproducto = "",
                tiposervicio = ""
            };

            co.com.tcc.clientes.remesas.unidad[] _unidades = new co.com.tcc.clientes.remesas.unidad[shipping.content.Measures.Count];
            string pesoVolumen = "";
            string Kilos = "";
           


            int i = 0;

            foreach (var measure in shipping.content.Measures)
            {
                _unidades[i] = new co.com.tcc.clientes.remesas.unidad
                {
                    alto = measure.Height.ToString(),
                    ancho = measure.Width.ToString(),
                    largo = measure.Length.ToString(),
                    dicecontener = shipping.content.Description,
                    kilosreales = Convert.ToInt32(measure.Weight)==0? "1": Convert.ToInt32(measure.Weight).ToString(),
                    valormercancia = shipping.content.Value.ToString(),
                    pesovolumen = Convert.ToInt32(measure.VolumetricWeight)==0?"1": Convert.ToInt32(measure.VolumetricWeight).ToString(),
                    tipoempaque = " ",
                    tipounidad = "TIPO_UND_PAQ",
                    claseempaque = "CLEM_CAJA",
                    codigobarras = " ",
                    numerobolsa = " ",
                    referencias = " "

                };
                i++;
            }


           

         
            documentoreferencia[] documentoreferencia = new documentoreferencia[1];
                       
            despacho.unidad = _unidades;
            despacho.documentoreferencia = documentoreferencia;
            
            grabardespacho7 dp = new grabardespacho7
            {
                despacho = despacho
            };
          
            try
            {
                var request = _dispatchesService.grabardespacho7(dp);

                
                shipping.code = request.remesa;
                shipping.guide.Url = request.urlrotulos;


                shipping.guide.Code = request.remesa;
                shipping.error.Message = request.mensaje;
                this.GenerateInvoice(shipping);

            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "[GenerateGuide]" + e.Message;
            }

            return shipping;
        }
       
    }
}