using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KioskoAdmin.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string session { get; set; }

        public string UID { get; set; }
    }


}