using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuhBilUyg.Models.isci_analizdb
{
    public class hesaplama
    {
        public int id { get; set; }
        public string kisi_ismi { get; set; }
        public string hesaplama_tur { get; set; }
        public int urun_fiyat { get; set; }
        public int min_adet { get; set; }
        public int saatlik_ucret { get; set; }
        public int toplam_saat { get; set; }
        public int katlanan_fiyat { get; set; }
        public int toplam_urun { get; set; }
        public int ekstra_ucret { get; set; }
        public int odeme { get; set; }
        public string kampanya { get; set; }
        public string tarih { get; set; }
    }
}