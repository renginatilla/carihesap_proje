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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var result = HelperKullanici.GetKullanici(textBox1.Text, textBox2.Text);
                HelperForm.Admin = result.KullaniciAd;
                HelperForm.AdminID = result.KullaniciId;
                if (result!=null)
                {
                    if (result.Type==2)
                    {
                        Islem f = new Islem();
                        f.Show();
                        f.tabControl1.TabPages.Remove(f.tabPage4);
                        f.tabControl1.TabPages.Remove(f.tabPage5);
                        this.Hide();
                    }
                    else
                    {
                        Islem f = new Islem();
                        f.Show();
                        this.Hide();

                    }                    
                }                  
                else
                {
                    label4.Text = "Giriş Başarısız...";
                    label4.ForeColor = Color.Red;
                }              
                label4.Visible = true;
            }
            else
            {
                label4.Text = "Boş alanları doldurun!";
                label4.Visible = true;
                label4.ForeColor = Color.Red;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
            label4.Visible = false;
        }
    }
}
