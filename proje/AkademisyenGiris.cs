using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class AkademisyenGiris : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database = staj_degerlendirme; user=root;Pwd=");
        public AkademisyenGiris()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                string sifre = txtSifre.Text;

                MySqlCommand komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT id FROM akademisyen WHERE sifre=@sifre";
                komut.Parameters.AddWithValue("@sifre", sifre);

                using (var dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Akademisyen a = new Akademisyen();
                        a.ID = Convert.ToInt32(dr["id"]);
                        this.Hide();
                        a.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz e-posta veya şifre. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void AkademisyenGiris_Load(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }

                MySqlCommand komut1 = new MySqlCommand();
                komut1.Connection = baglanti;
                komut1.CommandText = "SELECT ad, unvan FROM akademisyen";

                using (var dr1 = komut1.ExecuteReader())
                {
                    while (dr1.Read())
                    {
                        string ad = dr1["ad"].ToString();
                        string unvan = dr1["unvan"].ToString();
                        string tamAd = string.Format("{0} {1}", unvan, ad);
                        comboBox1.Items.Add(tamAd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

    }
}
