using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL.Model
{
    class MusteriModel
    {
        public int MusteriId { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriTel { get; set; }
        public string MusteriAdres { get; set; }
        public Nullable<int> KullaniciId { get; set; }

        public Kullanici kullanici = new Kullanici();   
    }
}
