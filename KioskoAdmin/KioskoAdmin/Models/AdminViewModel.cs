using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KioskoAdmin.Models
{
    public class AdminViewModel
    {       

        public class Cards
        {
            public string Title { get; set; }
            public string Action { get; set; }
            public string Text { get; set; }

            public string Class { get; set; }
        }
    }
}