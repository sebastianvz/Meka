using CoordinadoraService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoordinadoraService.com.coordinadora.sandbox.guias;
using CoordinadoraService.com.coordinadora.sandbox.despachos;
using CoordinadoraService.Controllers;
using System.Threading;
using DevExpress.XtraPrinting;
using Kiosko.Helpers;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using DevExpress.Pdf;
using System.Drawing;
using System.IO;
using Kiosko.Models;

namespace CoordinadoraService.Services
{
    public class CoordinadoraWebService
    {        
        private const string APIKEY = "80790a2a-e39f-11e8-9f32-f2801f1b9fd1";
        private const string PWD = "00cd63b4fa11b271bbf02706598bd1be2d05822266ece669ec2671625da2da36";
        private const string Cont = "skGLzl3VSr0IDW";
        private const string NIT = "900472103";
        private string Origen = "05001000";
        RpcServerSoapManagerService ServiceCotizadorManagement;
        JRpcServerSoapManagerService ServiceGuiaManagement;
        Agw_ciudadesOut[] Ciudades;


        public CoordinadoraWebService()
        {
            Agw_ciudadesIn input = new Agw_ciudadesIn();

            ServiceGuiaManagement = new JRpcServerSoapManagerService();

            input.clave = PWD;
            input.usuario = "mekagroup.ws";
            Ciudades = ServiceGuiaManagement.Guias_ciudades(input);

            

        }

        public List<object> GetDepartmentList()
        {
            

           // List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");

            List<object> DepartmentList = new List<object>();

            foreach (Agw_ciudadesOut daneList in Ciudades.GroupBy(c => c.codigo_departamento).Select(c => c.FirstOrDefault()).OrderBy(c => c.nombre_departamento).ToList())
            {
                DepartmentList.Add(new
                {
                    Name = daneList.nombre_departamento,
                    Code = daneList.codigo_departamento
                });
            };


            return DepartmentList;
        }


        public List<object> GetCityList(string departmentCode)
        {
        //    List<TCCDaneList> Departamentos = ReadFile<List<TCCDaneList>>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "tcc_dane.json");

            List<object> CityList = new List<object>();

            foreach (Agw_ciudadesOut daneList in Ciudades.Where(c => c.codigo_departamento == departmentCode ).OrderBy(c => c.nombre).ToList())
            {
                CityList.Add(new
                {
                    Name = daneList.nombre,
                    Code = daneList.codigo
                });
            };

            return CityList;
        }

        private ShippingModel GetPdfGuide(ShippingModel shipping)
        {
            string[] codigorem = new string[1];
            codigorem[0] = shipping.guide.Code;
            Agw_imprimirRotulosIn rotulo = new Agw_imprimirRotulosIn();
            rotulo.clave = PWD;
            rotulo.id_rotulo = "44";
            rotulo.usuario = "mekagroup.ws"; 
            rotulo.codigos_remisiones = codigorem;

            try
            {
                //Thread.Sleep(1000);
                var response = ServiceGuiaManagement.Guias_imprimirRotulos(rotulo);
                if(response.error)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "GetPdfGuide: " + response.errorMessage;

                    return shipping;
                }
                shipping.guide.PdfGuide = response.rotulos;                
            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "GetPdfGuide: " + e.Message;
            }

            return shipping;
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
                FileStream stream = new FileStream(path + filename + ".pdf", FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
                //log.Error(e.Message, e);
                return false;
            }
        }

        public ShippingModel GenerateGuide(ShippingModel shipping)
        {
            List<Agw_typeGuiaDetalle> empaque = new List<Agw_typeGuiaDetalle>();
            List<Agw_typeNotificaciones> notif = new List<Agw_typeNotificaciones>();
            ServiceGuiaManagement = new JRpcServerSoapManagerService();



            foreach (var values in shipping.content.Measures)
            {
                if(values.Weight < 1)
                {
                    values.Weight = 1;

                }
                if(values.Height==0 || values.Width==0 || values.Length == 0)
                {
                    values.Height = 1;
                    values.Width = 1;
                    values.Length = 1;


                }

                empaque.Add(new Agw_typeGuiaDetalle()
                {
                    alto = (float)values.Height,
                    ancho = (float)values.Width,
                    largo = (float)values.Length,
                    peso = (float)values.Weight,
                    ubl = GetProductCode(),
                    unidades = 1
                });
            }

            notif.Add(new Agw_typeNotificaciones()
            {
                destino_notificacion = shipping.origin.Email,
                tipo_medio = "1"
            });

            Agw_typeGenerarGuiaIn Guia = new Agw_typeGenerarGuiaIn
            {
                centro_costos = "Kiosko_1",
                //notificaciones = notif.ToArray(),
                clave = PWD,
              
                id_cliente = 29508,
                nombre_remitente = shipping.origin.Name,
                direccion_remitente = shipping.origin.Location.Address,
                telefono_remitente = shipping.origin.Phone,
                ciudad_remitente = shipping.origin.Location.CityCode,
                nit_destinatario = shipping.receiver.Identification,
                nombre_destinatario = shipping.receiver.Name,
                direccion_destinatario = shipping.receiver.Location.Address,
                ciudad_destinatario = shipping.receiver.Location.CityCode,
                telefono_destinatario = shipping.receiver.Phone,
                valor_declarado = (float) shipping.content.Value,
                codigo_producto = GetProductCode(),
                codigo_cuenta = 1,
                nivel_servicio = 1,
                contenido = shipping.content.Description,
                referencia = "Factura",
                observaciones = "Guia generada por Kiosko",
                estado = "IMPRESO",
                detalle = empaque.ToArray(),
                margen_izquierdo = 2,
                margen_superior = 2,
                formato_impresion = "pdf",
                usuario = "mekagroup.ws",

            };

            try
            {
                var response = ServiceGuiaManagement.Guias_generarGuia(Guia);
                shipping.guide.Code = response.codigo_remision;
                shipping.guide.Url = response.url_terceros;
                shipping.guide = GetPdfGuide(shipping).guide;


               CoordinadoraConfigModel configCoord=  ConfigService.readCoordinadoraConfig();

                if (configCoord.BoxesInStorage == false)
                {////LLAMAR SERVICIO DE RECOGIDA.

                    ServiceCotizadorManagement = new RpcServerSoapManagerService();
                    Recogidas_solicitarIn recogida = new Recogidas_solicitarIn()
                    {

                        apikey = APIKEY,
                        clave = Cont,
                        fecha_recogida = Utilities.GetDate(),
                        nit_cliente="900472103",
                        div_cliente="01",
                        nombre_contacto = "Kiosko",
                        direccion = "Calle 67 52-20",
                        ciudad_origen = Origen,
                        nombre_empresa="Mekagroup SAS",
                        referencia= shipping.guide.Code,
                        estado=0,
                        telefono = "3046570967",
                        producto = 4,
                        modalidad= 2,
                       
                        ciudad_destino = shipping.receiver.Location.CityCode
                    };
                    Recogidas_solicitarOut sol= ServiceCotizadorManagement.Recogidas_programar(recogida);

                    configCoord.BoxesInStorage = true;
                    Kiosko.Helpers.Utilities.WriteJson(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "coordinadora.json", configCoord);

                }
                AlegraService alg = new AlegraService();

                ///REMOVE THIS ON RELEASE
                AlegraResult fact = alg.CreateDummyInvoice(shipping);
                 
                
                //   = alg.CreateInvoice(shipping);                    
                FacturaCoordinadora factura = new FacturaCoordinadora();
                FacturaModel mdl = new FacturaModel();
                mdl.Model = shipping;
                mdl.Res = Utilities.GetResolucion();
                mdl.Direccion = "Calle 12 sur # 51 79";
                mdl.NIT = "900.472.103-1";
                mdl.RazonSocial = "MekaGroup SAS";
                mdl.FactNumber = fact.numberTemplate.number;
                mdl.Fecha = Utilities.GetDate();
                List<FacturaModel> lst = new List<FacturaModel>();
                lst.Add(mdl);

               

              


                SaveInvoice(lst, shipping);

               

                //attempt to save guide locally
               
                //factura.DataSource = lst;
                //PdfExportOptions pdfOptions = new PdfExportOptions();                
                //factura.CreateDocument(true);
                //factura.ExportToPdf(Properties.Settings.Default.GUIDE_PATH + "FACTURA.pdf");
                //factura.Dispose();


            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "GenerateGuide : " + e.Message;
            }

            return shipping;
        }



        public ShippingModel PrintInvoice(ShippingModel shipping)
        {
            try
            {
                PrinterSettings setings = new PrinterSettings();
                setings.PrinterName = "KioskoPrinterBill";
                DevExpress.Pdf.PdfPrinterSettings sett = new DevExpress.Pdf.PdfPrinterSettings(setings);

                sett.ScaleMode = DevExpress.Pdf.PdfPrintScaleMode.Fit;
                sett.PageOrientation = DevExpress.Pdf.PdfPrintPageOrientation.Portrait;
                sett.Settings.DefaultPageSettings.Margins.Left = 0;
                sett.Settings.DefaultPageSettings.Margins.Right = 0;
                sett.Settings.DefaultPageSettings.Margins.Bottom = 0;
                sett.Settings.DefaultPageSettings.Margins.Top = 0;

                shipping.error.HasError = false;
                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    string path = @Properties.Settings.Default.INVOICE_PATH + shipping.guide.Code + ".pdf";
                    pdfViewer.LoadDocument(path);
                    SizeF pg = pdfViewer.GetPageSize(1);
                    PageSettings sets = sett.Settings.DefaultPageSettings;
                    PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(3000 / 2.94));
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

        public ShippingModel SaveInvoice(List<FacturaModel> ord, ShippingModel shipping)
        {
            try
            {
                shipping.error.HasError = false;
                using (var pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer())
                {
                    
                    XtraReport rem = new FacturaCoordinadora();
                    rem.DataSource = ord;
                 
                    rem.ShowPrintStatusDialog = false;
                    rem.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    //string path= Properties.Settings.Default.INVOICE_PATH + shipping.guide.Code + ".pdf";

                    string path = @"C:\Kiosko\temp\invoice\" + shipping.guide.Code + ".pdf";
                    rem.CreateDocument(false);
                    rem.ExportToPdf(path);
                    
                    //rem.ExportToPdf(path);
                    //PaperSize paperSize = new PaperSize("Print", (int)(800 / 2.94), (int)(rem.PageHeight / 2.94));
                    //paperSize.RawKind = (int)PaperKind.Custom;
                    //sets.PaperSize = paperSize;
                    //pdfViewer.LoadDocument(path);
                    //pdfViewer.ShowPrintStatusDialog = false;
                    //pdfViewer.Print(sett);
                    //pdfViewer.CloseDocument();
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


        public CustomerServiceModel.Cost GetCost(ShippingModel shipping)
        {
            ServiceCotizadorManagement = new RpcServerSoapManagerService();
            ServiceGuiaManagement = new JRpcServerSoapManagerService();
            List<Cotizador_detalleEmpaques> empaque = new List<Cotizador_detalleEmpaques>();
            CustomerServiceModel.Cost costResponse = new CustomerServiceModel.Cost();
            
            Cotizador_cotizarIn CotizarIn = new Cotizador_cotizarIn
            {
                apikey = APIKEY,
                clave = Cont,
                cuenta = "1",
                nit = "",
                origen = Origen,
                producto = "0",
                nivel_servicio = new int[0],
                valoracion = shipping.content.Value.ToString().Replace(".", "").Replace(",", ""),
                destino = shipping.receiver.Location.CityCode,
            };
            
            foreach (var measure in shipping.content.Measures)
            {
                empaque.Add(new Cotizador_detalleEmpaques()
                {
                    alto = measure.Height.ToString(),
                    ancho = measure.Width.ToString(),
                    largo = measure.Length.ToString(),
                    peso = measure.Weight.ToString(),
                    ubl = "0",
                    unidades = "1"
                });
            }

            CotizarIn.detalle = empaque.ToArray();
            try
            {
                var request = ServiceCotizadorManagement.Cotizador_cotizar(CotizarIn);
                costResponse.MainCost = request.flete_fijo;
                costResponse.VariableCost = request.flete_variable;
                costResponse.TotalCost = request.flete_total;
            }
            catch (Exception e)
            {
                costResponse.error.HasError = true;
                costResponse.error.Message = e.Message;
            }

            return costResponse;

        }



        //public void GetDepartmentList()
        //{
        //    ServiceCotizadorManagement = new RpcServerSoapManagerService();
        //    com.coordinadora.sandbox.despachos.Type_void p = new com.coordinadora.sandbox.despachos.Type_void();

        //    try
        //    {


        //        var response = ServiceCotizadorManagement.Cotizador_departamentos(p);
        //    }
        //    catch(Exception e )
        //    {

        //    }
        //}
       
        private int GetProductCode()
        {
            return 0;
        }

    }
}