using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL
{
    class HelperKategori
    {
        public static bool CUD(EntityState state, Kategori kategori)
        {
            using (CariHesapEntities contex=new CariHesapEntities())
            {
                contex.Entry(kategori).State = state;
                if (contex.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static List<Kategori> GetKategoriList()
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Kategori.ToList();
            }
        }

        public static Kategori GetKategori(int Id)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Kategori.Where(x => x.KategoriId == Id).FirstOrDefault();
            }
        }
    }
}
