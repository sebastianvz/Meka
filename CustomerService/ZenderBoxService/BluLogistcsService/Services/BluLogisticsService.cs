using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Globalization;
using BluLogisticsService.Models;

namespace ZenderBoxServiceServices
{
    public class BluLogistcsService
    {
        BluWebService.WebServiceFPSoapClient client;
        BluWebService.EEncabezado enc;
        public BluLogistcsService()
        {
            enc = new BluWebService.EEncabezado();
            enc.Usuario = "kioscoMed01";
            enc.Terminal = "888888888888888";
            enc.Clave = "kioscoMed01";
            enc.Version = "";
            client = new BluWebService.WebServiceFPSoapClient();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
        }


        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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

            BluWebService.viewLiquidacion liq = new BluWebService.viewLiquidacion();


            BluWebService.EError Er = client.GetPreLiquidacion(ref enc, 2739, (decimal)shipping.content.Value, (decimal)shipping.content.Measures.Count, (decimal)shipping.content.Measures.Sum(item=>item.Weight),(decimal)shipping.content.Measures.Sum(item => item.VolumetricWeight), shipping.origin.Location.CityCode , shipping.receiver.Location.CityCode, ref liq);

            if (Er.Codigo == 1)
            {
                costResponse.MainCost = (float)liq.valorFlete;
                costResponse.VariableCost = (float)liq.costoManejo;
                costResponse.TotalCost = (float)liq.TotalFlete;
                return costResponse;


            }else
            {

                costResponse.error.HasError = true;
                costResponse.error.Message = Er.Descripcion;

                return costResponse;
             }
            

            
        }

        public void ObtenerCiudadesDestino()
        {

            BluWebService.ECiudades[] cds= new BluWebService.ECiudades[0];
            //cds[0] = new BluWebService.ECiudades(){ IdCiudad= "05001", Departamento="Antioquia", Nombre="Medellin" };

            BluWebService.EError Er=  client.SolicitarCiudadesDestino(ref enc,ref cds);

        }


        //public CustomerServiceModel.Guide ConsultarFactura(ShippingModel shipping)
        //{
           





        //}


        public CustomerServiceModel.Guide GenerateGuide(ShippingModel shipping)
        {

            CustomerServiceModel.Guide guideResponse = new CustomerServiceModel.Guide();
            BluWebService.EFacturaKiosco fact = new BluWebService.EFacturaKiosco();
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



            BluWebService.EError Er = client.RegistrarFactura(ref enc, ref fact, ref factid);
            if (Er.Codigo == 1) {
                guideResponse.Id = Convert.ToInt32(fact.Identificacion);
                guideResponse.Code = fact.IdFactura;
                BluWebService.EFacturas fals = new BluWebService.EFacturas();
                fals.IdFactura = fact.IdFactura;
                BluWebService.EFacturas fals2 = new BluWebService.EFacturas();
                BluWebService.EError er= client.ConsultarFactura(ref enc, factid,  fals, out fals2);


                return guideResponse;

                   }else
            {

                guideResponse.error.HasError = true;
                guideResponse.error.Message = Er.Descripcion;
                return guideResponse;


            }

           
        }
       
    }
}