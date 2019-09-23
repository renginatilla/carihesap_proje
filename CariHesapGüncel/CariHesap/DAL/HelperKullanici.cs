using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL.Model
{
    class HelperKullanici
    {
        public static Kullanici GetKullanici(string kullanici, string sifre)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Kullanici.Where(x => x.KullaniciAd == kullanici && x.KullaniciSifre == sifre).FirstOrDefault();
            }
        }

        public static Kullanici GetKullanici(int ID)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                return contex.Kullanici.Where(x => x.KullaniciId == ID).FirstOrDefault();
            }
        }
        public static bool CUD(EntityState state, Kullanici kullanici)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                contex.Entry(kullanici).State = state;
                if (contex.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
