using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL.Model
{
    class UrunModel
    {
        public int UrunId { get; set; }
        public string UrunAd { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public Nullable<int> AlisFiyati { get; set; }
        public Nullable<int> SatisFiyati { get; set; }
        public Nullable<int> UrunStok { get; set; }
        public string Aciklama { get; set; }


        public Kategori Kategori = new Kategori();
        public SatisModel Satis { get; set; }

    }
}
