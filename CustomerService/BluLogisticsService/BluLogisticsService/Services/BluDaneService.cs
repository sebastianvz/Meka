using BluLogisticsService.BluServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BluLogisticsService.Services
{
    public class BluDaneService
    {
        BluLogisticsService.BluServiceReference.WebServiceFPSoapClient client = new WebServiceFPSoapClient();


        static EEncabezado eEncabezado = new EEncabezado()

        {

            Usuario = "kioscoMed01",

            Clave = "kioscoMed01 ",

            Version = "",

            Terminal = "888888888888888"

        };
        ECiudades[] ciudades;
        public void Initialize() 
        {
            ciudades = new ECiudades[1];
            ciudades[0] = new ECiudades();
            ciudades[0].IdCiudad = "05001";
            client.SolicitarCiudadesDestino(ref eEncabezado, ref ciudades);


        }


        public List<object> GetDepartmentList()
        {
            Initialize();
            List<object> DepartmentList = new List<object>();

            foreach (ECiudades daneList in ciudades.GroupBy(c => c.Departamento).Select(c => c.FirstOrDefault()).ToList())
            {
                DepartmentList.Add(new
                {
                    Name = daneList.Departamento,
                    Code = Convert.ToInt32(daneList.IdCiudad.Substring(0, 2))
                });
            };


            return DepartmentList;
        }

        public List<object> GetCityList(int departmentCode)
        {
            Initialize();
            List<object> CityList = new List<object>();

            foreach (ECiudades daneList in ciudades.Where(c => Convert.ToInt32(c.IdCiudad.Substring(0,2)) == departmentCode).OrderBy(c => c.Nombre).ToList())
            {
                CityList.Add(new
                {
                    Name = daneList.Nombre,
                    Code = daneList.IdCiudad
                });
            };

            return CityList;
        }


    }
}