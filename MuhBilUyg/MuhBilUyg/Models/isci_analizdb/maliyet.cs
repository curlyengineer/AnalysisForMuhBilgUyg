using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuhBilUyg.Models.isci_analizdb
{
    public class maliyet
    {
        public int id { get; set; }
        public int sabit_ucret { get; set; }
        public int prim_urun_ucreti { get; set; }
        public int prim_urun_adet { get; set; }
        public int prim_ucret { get; set; }
        public int akort_urun_fiyat { get; set; }
        public int akort_min_urun { get; set; }
        public int akort_artan_fiyat { get; set; }
        public int akort_toplam_urun { get; set; }
        public int akort_ucret { get; set; }
    }
}