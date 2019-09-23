using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CariHesap.DAL;
using CariHesap.BLL;
using CariHesap.DAL.Model;

namespace CariHesap
{
    public partial class Islem : Form
    {
        public int selectedmusteri;
        public int getID;
        public int kategoriId;
        public int selectedkategori;
        public int UrunID;
        public int selectedfiyat;

        public Islem()
        {
            InitializeComponent();
        }
        DataTable table;
        private void Islem_Load(object sender, EventArgs e)
        {
            label26.Visible = false;
            textBox15.PasswordChar = '●';
            textBox16.PasswordChar = '●';
            textBox17.PasswordChar = '●';
            label28.Text = "Hoşgeldiniz Sn.";
            label29.Text = HelperForm.Admin;
            comboBox2.ValueMember = "KategoriId";
            comboBox2.DisplayMember = "KategoriName";
            comboBox2.DataSource = HelperKategori.GetKategoriList();
            musterigridshow();
            kategorigridshow();
            comboBox3.ValueMember = "MusteriId";
            comboBox3.DisplayMember = "MusteriAd";
            comboBox3.DataSource = HelperMusteri.GetMusteriList();

            table = new DataTable();

            table.Columns.Add("MüşteriAdı", typeof(string));
            table.Columns.Add("ÜrünKategori", typeof(string));
            table.Columns.Add("ÜrünAdı", typeof(string));
            table.Columns.Add("Adedi", typeof(int));
            table.Columns.Add("Fiyatı", typeof(int));
            table.Columns.Add("EklenmeTarihi", typeof(DateTime));
            dataGridView5.DataSource = table;
            label30.Text = karZararHesapla().ToString();

            UrunListele();
            KategoriEkle();
        }
        void satisshow()
        {
            var result = HelperSatis.GetSatisModels();
            foreach (var item in result)
            {

                var adet = item.OdenenTutar / item.urun.SatisFiyati;
                table.Rows.Add(item.musteriler.MusteriAd + " " + item.musteriler.MusteriSoyad, item.urun.Kategori.KategoriName, item.urun.UrunAd, adet, item.OdenenTutar, item.KayitTarih);
            }
        }
        public void KategoriEkle()
        {
            comboBox1.Items.Clear();
            comboBox1.ValueMember = "KategoriId";
            comboBox1.DisplayMember = "KategoriName";
            comboBox1.DataSource = HelperKategori.GetKategoriList();
        }
        private void Button13_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Musteriler musteri = new Musteriler()
            {
                MusteriAd = textBox1.Text,
                MusteriSoyad = textBox2.Text,
                MusteriTel = textBox3.Text,
                MusteriAdres = textBox4.Text,
                KullaniciId = HelperForm.AdminID
            };
            var result = HelperMusteri.CUD(System.Data.Entity.EntityState.Added, musteri);
            MessageBox.Show(result == true ? "Müşteri eklendi." : "Müşteri eklenemedi...");
            musterigridshow();
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            getID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            var a = HelperMusteri.GetMusteri(getID);
            textBox1.Text = a.MusteriAd;
            textBox2.Text = a.MusteriSoyad;
            textBox3.Text = a.MusteriTel;
            textBox4.Text = a.MusteriAdres;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Musteriler m = new Musteriler()
            {
                MusteriId = getID,
                MusteriAd = textBox1.Text,
                MusteriSoyad = textBox2.Text,
                MusteriTel = textBox3.Text,
                MusteriAdres = textBox4.Text,
                KullaniciId = HelperForm.AdminID
            };

            bool result = HelperMusteri.CUD(System.Data.Entity.EntityState.Modified, m);
            MessageBox.Show(result == true ? "Müşteri güncellendi." : "Müşteri güncellenemedi.");
            musterigridshow();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Musteriler m = new Musteriler();
            m.MusteriId = getID;
            bool result = HelperMusteri.CUD(System.Data.Entity.EntityState.Deleted, m);
            MessageBox.Show(result == true ? "Müşteri silindi." : "Müşteri silinemedi.");
            musterigridshow();
        }
        void musterigridshow()
        {
            dataGridView1.Rows.Clear();
            var MusteriList = HelperMusteri.GetMusteriList();
            foreach (var item in MusteriList)
            {
                dataGridView1.Rows.Add(item.MusteriAd, item.MusteriSoyad, item.MusteriTel, item.MusteriAdres, item.MusteriId);
            }
        }

        void kategorigridshow()
        {
            dataGridView3.Rows.Clear();
            var kategorilist = HelperKategori.GetKategoriList();
            foreach (var item in kategorilist)
            {
                dataGridView3.Rows.Add(item.KategoriName, item.KategoriAciklama, item.KategoriId);
            }
        }
        private void DataGridView3_Click(object sender, EventArgs e)
        {
            kategoriId = Convert.ToInt32(dataGridView3.CurrentRow.Cells[2].Value);
            var k = HelperKategori.GetKategori(kategoriId);
            textBox13.Text = k.KategoriName;
            textBox12.Text = k.KategoriAciklama;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Kategori k = new Kategori()
            {
                KategoriName = textBox13.Text,
                KategoriAciklama = textBox12.Text
            };
            bool result = HelperKategori.CUD(System.Data.Entity.EntityState.Added, k);
            MessageBox.Show(result == true ? "Kategori eklendi." : "Kategori eklenemedi.");
            kategorigridshow();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Kategori k = new Kategori()
            {
                KategoriId = kategoriId,
                KategoriName = textBox13.Text,
                KategoriAciklama = textBox12.Text
            };
            bool result = HelperKategori.CUD(System.Data.Entity.EntityState.Modified, k);
            MessageBox.Show(result == true ? "Kategori güncellendi." : "Kategori güncellenemedi.");
            kategorigridshow();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Kategori k = new Kategori();
            k.KategoriId = kategoriId;
            bool result = HelperKategori.CUD(System.Data.Entity.EntityState.Deleted, k);
            MessageBox.Show(result == true ? "Kategori silindi." : "Kategori silinemedi.");
            kategorigridshow();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            int userID = HelperForm.AdminID;
            var u = HelperKullanici.GetKullanici(userID);
            bool result = false;
            if (u.KullaniciSifre == textBox17.Text)
            {
                if ((textBox16.Text != "" && textBox15.Text != "") && (textBox16.Text == textBox15.Text))
                {
                    u.KullaniciSifre = textBox15.Text;
                    result = HelperKullanici.CUD(System.Data.Entity.EntityState.Modified, u);
                }
                MessageBox.Show(result == true ? "Şifre güncellendi." : "Şifre güncellenemedi.");
                label26.Visible = false;

            }
            else
            {
                label26.ForeColor = Color.Red;
                label26.Visible = true;
                label26.Text = "Girdiğiniz şifre yanlış ya da eksik.";
            }

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            var urun = HelperUrun.GetUrunsList(selectedkategori);
            foreach (var item in urun)
            {
                dataGridView4.Rows.Add(item.UrunAd, item.SatisFiyati, item.Aciklama, item.UrunId);
            }
        }
        private void ComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedkategori = Convert.ToInt32(comboBox2.SelectedValue);
        }
        private void DataGridView4_Click(object sender, EventArgs e)
        {
            UrunID = Convert.ToInt32(dataGridView4.CurrentRow.Cells[3].Value);
            selectedfiyat = Convert.ToInt32(dataGridView4.CurrentRow.Cells[1].Value);
            var u = HelperUrun.GetUrun(UrunID);
            textBox11.Text = u.UrunAd;
            textBox6.Text = u.UrunStok.ToString();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            var urun = HelperUrun.GetUrun(UrunID);
            if (Convert.ToInt32(textBox6.Text) > urun.UrunStok)
            {
                MessageBox.Show("Stokta yeteri kadar ürün bulunmamaktadır.");
            }
            else
            {
                Satis s = new Satis();
                s.MusteriId = selectedmusteri;
                s.UrunId = UrunID;
                s.KayitTarih = DateTime.Now.Date;
                s.OdenenTutar = Convert.ToInt32(textBox6.Text) * selectedfiyat;
                bool result = HelperSatis.CUD(System.Data.Entity.EntityState.Added, s);
                MessageBox.Show(result == true ? "Satış Yapıldı." : "Satış Yapılamadı.");
                urun.UrunStok = urun.UrunStok - Convert.ToInt32(textBox6.Text);
                HelperUrun.CUD(urun,System.Data.Entity.EntityState.Modified);

            }
            satisshow();

        }
        private void ComboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedmusteri = Convert.ToInt32(comboBox3.SelectedValue);
        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            List<SatisModel> sorgu = new List<SatisModel>();     
            if (radioButton1.Checked == true)
            {
                sorgu = HelperSatis.GetSatisModels(textBox14.Text, null,null,dateTimePicker1.Value, dateTimePicker2.Value);
            }
            else if (radioButton2.Checked == true)
            {
                sorgu = HelperSatis.GetSatisModels( null, textBox14.Text, null, dateTimePicker1.Value, dateTimePicker2.Value);
            }
            else
            {
                sorgu = HelperSatis.GetSatisModels(null, null, textBox14.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            }

            table.Rows.Clear();
            foreach (var item in sorgu)
            {
                var adet = item.OdenenTutar / item.urun.SatisFiyati;
                table.Rows.Add(item.musteriler.MusteriAd + " " + item.musteriler.MusteriSoyad, item.urun.Kategori.KategoriName, item.urun.UrunAd, adet, item.OdenenTutar, item.KayitTarih);
            }
            dataGridView5.DataSource = table;
        }

        private void ComboBox3_Format(object sender, ListControlConvertEventArgs e)
        {
            string firstName = ((Musteriler)e.ListItem).MusteriAd;
            string surName = ((Musteriler)e.ListItem).MusteriSoyad;
            e.Value = firstName + " " + surName;
        }

        double karZararHesapla()
        {
            var satislar = HelperSatis.GetSatisModels();
            var karZarar = 0;

            foreach (var item in satislar)
            {
                var adet = item.OdenenTutar / item.urun.SatisFiyati;
                karZarar += adet * (item.urun.SatisFiyati - item.urun.AlisFiyati);
            }
            return karZarar;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Urun u = new Urun();
            if (Convert.ToInt32(textBox7.Text) < 0 || Convert.ToInt32(textBox9.Text) < 0 || Convert.ToInt32(textBox5.Text) < 0)
            {
                button6.Enabled = false;
            }
            else
            {
                u.UrunAd = textBox8.Text;
                u.AlisFiyati = Convert.ToInt32(textBox7.Text);
                u.SatisFiyati = Convert.ToInt32(textBox9.Text);
                u.UrunStok = Convert.ToInt32(textBox5.Text);
                u.KategoriId = Convert.ToInt32(comboBox1.SelectedValue);
                u.Aciklama = textBox10.Text;
                var result = HelperUrun.CUD(u, System.Data.Entity.EntityState.Added);
                MessageBox.Show(result == true ? "Ürün Başarıyla Eklendi" : "Hata");
                UrunListele();
                Temizle();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox7.Text) < 0 || Convert.ToInt32(textBox9.Text) < 0 || Convert.ToInt32(textBox5.Text) < 0)
            {
                button5.Enabled = false;
            }
            else
            {
                int a = Convert.ToInt32(dataGridView2.CurrentRow.Cells[6].Value);
                var aa = HelperUrun.GetUrun(a);
                aa.UrunAd = textBox8.Text;
                aa.KategoriId = Convert.ToInt32(comboBox1.SelectedValue);
                aa.AlisFiyati = Convert.ToInt32(textBox7.Text);
                aa.SatisFiyati = Convert.ToInt32(textBox9.Text);
                aa.UrunStok = Convert.ToInt32(textBox5.Text);
                aa.Aciklama = textBox10.Text;
                var result = HelperUrun.CUD(aa, System.Data.Entity.EntityState.Modified);
                MessageBox.Show(result == true ? "Güncelleme işlemi başarılı" : "HATA");
                UrunListele();
                Temizle();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(dataGridView2.CurrentRow.Cells[6].Value);
            var aa = HelperUrun.GetUrun(a);
            var result = HelperUrun.CUD(aa, System.Data.Entity.EntityState.Deleted);
            MessageBox.Show(result == true ? "Silme işlemi başarılı" : "HATA");
            UrunListele();
            Temizle();
        }
        public void Temizle()
        {
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox5.Clear();
            textBox10.Clear();

        }
        public void UrunListele()
        {
            dataGridView2.Rows.Clear();
            var aa = HelperUrun.GetUrunModelsList();
            foreach (var item in aa)
            {
                dataGridView2.Rows.Add(item.UrunAd, item.Kategori.KategoriName, item.AlisFiyati, item.SatisFiyati, item.UrunStok, item.Aciklama, item.UrunId);

            }
        }

        private void DataGridView2_Click(object sender, EventArgs e)
        {
            textBox8.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox7.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox9.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            textBox10.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
