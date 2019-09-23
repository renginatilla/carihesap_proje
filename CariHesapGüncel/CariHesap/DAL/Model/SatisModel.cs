using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL.Model
{
    class SatisModel
    {
        public int SatisId { get; set; }
        public Nullable<int> MusteriId { get; set; }
        public Nullable<int> UrunId { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public int OdenenTutar { get; set; }

        public Musteriler musteriler = new Musteriler();

        public Urun urun = new Urun();
        
    }
}
