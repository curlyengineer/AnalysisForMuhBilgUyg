using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuhBilUyg.Models.isci_analizdb
{
    public class login
    {
        public int id { get; set; }
        public string login_name { get; set; }
        [DataType(DataType.Password)]
        public string login_password { get; set; }
    }
}