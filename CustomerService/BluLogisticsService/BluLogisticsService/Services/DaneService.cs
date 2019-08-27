using SODA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCService.Services
{
    public class DaneService
    {
        SodaClient client;
        List<DaneResult> DaneCodes;

        public DaneService()
        {

        }

        private void Initialize()
        {
            client = new SodaClient("https://www.datos.gov.co/", "OhjEGmjKoXZ8HDBZZOCxWm2D3");
            Resource<DaneResult> dataset = client.GetResource<DaneResult>("p95u-vi7k");
            DaneCodes = dataset.GetRows(limit: 5000).OrderBy(i => i.departamento).ToList();
        }

        public List<object> GetDepartmentList()
        {
            Initialize();
            List<object> DepartmentList = new List<object>();

            foreach (DaneResult daneList in DaneCodes.GroupBy(c => c.departamento).Select(c => c.FirstOrDefault()).ToList())
            {
                DepartmentList.Add(new
                {
                   Name = daneList.departamento,
                   Code = daneList.c_digo_dane_del_departamento
                });
            };


            return DepartmentList;
        }

        public List<object> GetCityList(int departmentCode)
        {
            Initialize();
            List<object> CityList = new List<object>();

            foreach (DaneResult daneList in DaneCodes.Where(c => c.c_digo_dane_del_departamento == departmentCode).OrderBy(c => c.municipio).ToList())
            {
                CityList.Add(new
                {
                    Name = daneList.municipio,
                    Code = daneList.c_digo_dane_del_municipio
                });
            };

            return CityList;
        }

        public DaneResult GetCityByCode(int code)
        {
            Initialize();
            return DaneCodes.Find(x => x.c_digo_dane_del_municipio == code);
        }

        public static string ReturnDane8Digits(string code)
        {
            return (code.Length == 5) ? code + "000" : "0" + code + "000";
        }

    }


    

    public class DaneResult
    {
        public int c_digo_dane_del_departamento;
        public int c_digo_dane_del_municipio;
        public string departamento;
        public string municipio;
        public string region;
        public override string ToString()
        {
            return municipio;
        }
    }
}