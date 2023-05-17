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
    public partial class SekreterGiris : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database = staj_degerlendirme; user=root;Pwd=");
        public SekreterGiris()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string sifre = txtSifre.Text;

            baglanti.Open();

            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT id FROM sekreter WHERE email=@email AND sifre=@sifre";
            komut.Parameters.AddWithValue("@email", email);
            komut.Parameters.AddWithValue("@sifre", sifre);

            var dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Sekreter s = new Sekreter();
                s.ID = Convert.ToInt32(dr["id"]);
                this.Hide();
                s.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Geçersiz e-posta veya şifre. Lütfen tekrar deneyin.");
            }

            baglanti.Close();
        }

        private void SekreterGiris_Load(object sender, EventArgs e)
        {
            txtEmail.Text = String.Empty;
            txtSifre.Text = String.Empty;
        }
    }
}
