using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Services
{
    public class CubiQService
    {
        private string CubiQServiceUrl;

        public CubiQService(string _IpAddress)
        {
            CubiQServiceUrl = "http://" + _IpAddress.ToString() + "/WebService/api/";
        }

        public CubiQModel.Measure GetMeasure()
        {
            CubiQModel.Measure measure = new CubiQModel.Measure();
            try
            {
                var MeasureRequest = Helpers.Utilities.doRequest<CubiQModel.Measure>(CubiQServiceUrl, CubiQModel.Resource.MEASUREMENTS, null, "get", 5000);

                if (MeasureRequest == null)
                {
                    measure.Error.HasError = true;
                    measure.Error.Message = "Error on Measure request";
                    return measure;
                }
                var ImageRequest = Helpers.Utilities.doRequest<CubiQModel.Image>(CubiQServiceUrl, CubiQModel.Resource.IMAGE , null , "get" , 5000);

                // To use in Edge
                string newImageRequest = ImageRequest.Image64.Insert(11, "png");

                if (ImageRequest == null)
                {
                    measure.Error.HasError = true;
                    measure.Error.Message = "Error on Image request";
                    return measure;
                }

                measure = MeasureRequest;
                measure.ImageBase64 = newImageRequest;
            }
            catch (Exception e){
                measure.Error.HasError = true;
                measure.Error.Message = e.Message;
                return measure;
            }            

            return measure;
        }


        public CubiQModel.GeneralInformation GetGeneralInformation()
        {
            CubiQModel.GeneralInformation GeneralInformation = new CubiQModel.GeneralInformation();
            try
            {
                var GeneralInformationRequest = Helpers.Utilities.doRequest<CubiQModel.GeneralInformation>(CubiQServiceUrl, CubiQModel.Resource.GENERALINFORMATION);

                if(GeneralInformationRequest == null)
                {
                    GeneralInformation.Success = false;
                    return GeneralInformation;
                }

                GeneralInformation = GeneralInformationRequest;
                GeneralInformation.Success = true;
            }catch(Exception e)
            {
                GeneralInformation.Success = false;
            }

            return GeneralInformation;
        }

    }
}