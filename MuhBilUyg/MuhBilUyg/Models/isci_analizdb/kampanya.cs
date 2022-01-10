using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MuhBilUyg.Models.isci_analizdb
{
    public class kampanya
    {
        public int id { get; set; }
        [DisplayName("Kampanya Adı")]
        public string kampanya_adi { get; set; }
        [DisplayName("Kampanyadaki minimum ücret")]
        public int kampanya_minucret { get; set; }
        [DisplayName("Kampanyadaki minimum ürün adedi")]
        public int kampanya_minadet { get; set; }
        [DisplayName("Kampanya bitiş tarihi ")]
        public string kampanya_tarih { get; set; }
    }
}