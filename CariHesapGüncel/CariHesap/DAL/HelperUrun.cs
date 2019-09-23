using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CariHesap.DAL.Model;

namespace CariHesap.DAL
{
    class HelperUrun
    {
        public static Urun GetUrun(int urunId)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Urun.Where(x => x.UrunId == urunId).FirstOrDefault();
            }
        }
        public static List<Urun> GetUrunsList()
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Urun.ToList();
            }
        }
        public static List<Urun> GetUrunsList(int kategoriId)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Urun.Where(x => x.KategoriId == kategoriId).ToList();
            }
        }
        public static List<UrunModel> GetUrunModelsList()
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {

                List<UrunModel> um = new List<UrunModel>();

                var a = contex.Urun.ToList();
                foreach (var item in a)
                {
                    UrunModel u = new UrunModel();
                    u.UrunId = item.UrunId;
                    u.UrunAd = item.UrunAd;
                    u.UrunStok = item.UrunStok;
                    u.SatisFiyati = item.SatisFiyati;
                    u.AlisFiyati = item.AlisFiyati;
                    u.KategoriId = item.KategoriId;
                    u.Kategori.KategoriName = item.Kategori.KategoriName;
                    u.Aciklama = item.Aciklama;
                    um.Add(u);
                }
                return um;
            }
        }

        public static bool CUD(Urun u, EntityState state)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                contex.Entry(u).State = state;
                if (contex.SaveChanges() > 0)
                    return true;
                return false;

            }
        }

    }
}
