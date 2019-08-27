using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiosko.Models;
namespace Kiosko.Controllers
{
    public class CubiQController
    {
        public CubiQModel.Measure GetMeasures(string mode)
        {
            Services.CubiQService _cubiqService = new Services.CubiQService(KioskoController.GetCubiQService());
            var measures = _cubiqService.GetMeasure();
            string objectMode = mode.Equals("package") ? "Paquete" : "Sobre";

            if(!Helpers.KioskoRestrictions.IsValidMeasure(measures , mode))
            {
                measures.Error.HasError = true;
                measures.Error.Message = "El " + objectMode +" ingresado con medidas: ALTO: " + measures.Height + " " +
                                 "ANCHO: " + measures.Width + " LARGO: " + measures.Length + " PESO: " + measures.Weight + " posee valores en cero.";
                return measures;
            }

            if (Helpers.KioskoRestrictions.IsMaxMeasure(measures, mode))
            {
                measures.Error.HasError = true;
                measures.Error.Message = "El " + objectMode + " ingresado con medidas: ALTO: " + measures.Height + " " +
                                 "ANCHO: " + measures.Width + " LARGO: " + measures.Length + " PESO: " + measures.Weight + " supera las medidas máximas permitidas para este punto de atención.";
                return measures;
            }

            if (Helpers.KioskoRestrictions.IsMinMeasure(measures, mode))
            {
                measures.Error.HasError = true;
                measures.Error.Message = "El " + objectMode + " ingresado con medidas: ALTO: " + measures.Height + " " +
                                 "ANCHO: " + measures.Width + " LARGO: " + measures.Length + " PESO: " + measures.Weight + " no cumple con las dimensiones mínimas permitidas para este punto de atención.";
                return measures;
            }

            //if (measures.Status != "STABLE")
            //{
            //    measures.Error.HasError = true;
            //    measures.Error.Message = "Las medidas no son estables.";
            //    return measures;
            //}

            return measures;
        }

        public KioskoServiceStatus CheckService()
        {
            Services.CubiQService _cubiqService = new Services.CubiQService(KioskoController.GetCubiQService());
            KioskoServiceStatus serviceStatus = new KioskoServiceStatus(CubiQManagerModel.KioskoService.CUBIQSERVICE);
            var generalInformation = _cubiqService.GetGeneralInformation();

            if(generalInformation.Error.HasError)
            {
                serviceStatus.Active = false;
                serviceStatus.Message = generalInformation.Error.Message;
            }

            return serviceStatus;
        }



    }
}