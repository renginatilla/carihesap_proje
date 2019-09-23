using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL
{
    class HelperMusteri
    {
        public static Musteriler GetMusteri(int ID)
        {
            using (CariHesapEntities contex=new CariHesapEntities())
            {
                return contex.Musteriler.Where(x => x.MusteriId == ID).FirstOrDefault();
            }
        }
        public static bool CUD(EntityState state, Musteriler musteriler)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                contex.Entry(musteriler).State = state;
                if (contex.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
        public static List<Musteriler> GetMusteriList()
        {
            using (CariHesapEntities contex=new CariHesapEntities())
            {
                return contex.Musteriler.ToList();
            }
        }
    }
}
