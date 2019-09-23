using CariHesap.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesap.DAL
{
    class HelperSatis
    {
        public static bool CUD(EntityState state, Satis satis)
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                contex.Entry(satis).State = state;
                if (contex.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
        public static List<SatisModel> GetSatisModels()
        {
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                var list = contex.Satis.ToList();
                List<SatisModel> sml = new List<SatisModel>();
                foreach (var item in list)
                {
                    SatisModel model = new SatisModel();
                    model.MusteriId = item.MusteriId;
                    model.musteriler.MusteriAd = item.Musteriler.MusteriAd;
                    model.musteriler.MusteriSoyad = item.Musteriler.MusteriSoyad;
                    model.urun.Kategori = item.Urun.Kategori;
                    model.UrunId = item.UrunId;
                    model.urun.UrunAd = item.Urun.UrunAd;
                    model.urun.SatisFiyati = item.Urun.SatisFiyati;
                    model.OdenenTutar = item.OdenenTutar;
                    model.KayitTarih = item.KayitTarih;
                    sml.Add(model);
                }
                return sml;
            }
        }

        public static List<SatisModel> GetSatisModels(string musteri, string urun, string kategori, DateTime dt1, DateTime dt2)
        {
            List<Satis> Satiss = new List<Satis>();

            using (CariHesapEntities contex = new CariHesapEntities())
            {
                if (musteri != null)
                {
                    Satiss = contex.Satis.Where(x => x.KayitTarih >= dt1 && x.KayitTarih <= dt2).Where
                        (x => x.Musteriler.MusteriAd.Contains(musteri) || x.Musteriler.MusteriSoyad.Contains(musteri)).ToList();
                }
                else if (urun != null)
                {
                    Satiss = contex.Satis.Where(x => x.KayitTarih >= dt1 && x.KayitTarih <= dt2).Where(x => x.Urun.UrunAd.Contains(urun)).ToList();
                }
                else if (kategori != null)
                {
                    Satiss = contex.Satis.Where(x => x.KayitTarih >= dt1 && x.KayitTarih <= dt2).Where(x => x.Urun.Kategori.KategoriName.Contains(kategori)).ToList();
                }
                return ConvertToSatisModel(Satiss);
            }
        }

        static List<SatisModel> ConvertToSatisModel(List<Satis> satislar)
        {
            var SatisModels = new List<SatisModel>();
            using (CariHesapEntities contex = new CariHesapEntities())
            {
                foreach (var item in satislar)
                {
                    var model = new SatisModel();

                    model.MusteriId = item.MusteriId;
                    model.musteriler.MusteriAd = item.Musteriler.MusteriAd;
                    model.musteriler.MusteriSoyad = item.Musteriler.MusteriSoyad;
                    model.urun.Kategori = item.Urun.Kategori;
                    model.UrunId = item.UrunId;
                    model.urun.UrunAd = item.Urun.UrunAd;
                    model.urun.SatisFiyati = item.Urun.SatisFiyati;
                    model.OdenenTutar = item.OdenenTutar;
                    model.KayitTarih = item.KayitTarih;
                    SatisModels.Add(model);
                }
                return SatisModels;
            }
        }
    }
}

