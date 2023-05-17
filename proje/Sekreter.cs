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
    public partial class Sekreter : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database = staj_degerlendirme; user=root;Pwd=");
        private int id;
        private int currentRowIndex = 0;
        private DataTable dataTable = new DataTable();
        public Sekreter()
        {
            InitializeComponent();
        }

        public Sekreter(int id)
        {
            this.id = id;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private void Sekreter_Load(object sender, EventArgs e)
        {
            btnIlk.Enabled = false;
            btnOnceki.Enabled = false;
            lblId.Text = id.ToString();
            GetOgrenciVerileri();
            sekreterGetir(1);
            if (dataTable.Rows.Count > 0) { VerileriDoldur(currentRowIndex);}
        }

        private void GetOgrenciVerileri()
        {
            string query = "SELECT * FROM ogrenci WHERE sekreter_id = @sekreterId";

            using (MySqlCommand cmd = new MySqlCommand(query, baglanti))
            {
                cmd.Parameters.AddWithValue("@sekreterId", id);

                try
                {
                    baglanti.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }
        private void sekreterGetir(int index)
        {
            string query = "SELECT sekreter.ad, sekreter.soyad FROM sekreter " +
                           "INNER JOIN ogrenci ON ogrenci.sekreter_id = sekreter.id " +
                           "WHERE ogrenci.id = @ogrenciId";

            using (MySqlCommand cmd = new MySqlCommand(query, baglanti))
            {
                cmd.Parameters.AddWithValue("@ogrenciId", index);

                try
                {
                    baglanti.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSekreter.Text = reader["ad"].ToString() + " " + reader["soyad"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        private void VerileriDoldur(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataTable.Rows.Count)
            {
                DataRow reader = dataTable.Rows[rowIndex];
                txtTckimlik.Text = reader["tc_kimlik"].ToString();
                txtAd.Text = reader["ad"].ToString();
                txtSoyad.Text = reader["soyad"].ToString();
                txtOgrenciNo.Text = reader["ogrenci_no"].ToString();
                txtSinif.Text = reader["sinif"].ToString();
                txtTel.Text = reader["telefon"].ToString();
                txtEposta.Text = reader["eposta"].ToString();
                txtStajKodu.Text = reader["staj_kodu"].ToString();
                txtStajYeri.Text = reader["staj_yeri"].ToString();
                txtStajBaslangicTarihi.Text = reader["staj_baslama_tarihi"].ToString();
                txtStajBitisTarihi.Text = reader["staj_bitis_tarihi"].ToString();
                cbStajEvraklari.Checked = Convert.ToBoolean(reader["evrak"]);
                cbBasvuruDilekcesi.Checked = Convert.ToBoolean(reader["basvuru_dilekcesi"]);
                cbKabulYazisi.Checked = Convert.ToBoolean(reader["kabul_yazisi"]);
                cbMustehaklik.Checked = Convert.ToBoolean(reader["mustehaklik_belgesi"]);
                cbKimlik.Checked = Convert.ToBoolean(reader["kimlik_fotokopisi"]);
                cbStajDegerlendirmeFormu.Checked = Convert.ToBoolean(reader["staj_degerlendirme_formu"]);
                cbStajRaporu.Checked = Convert.ToBoolean(reader["staj_raporu"]);
                txtAciklamalar.Text = reader["aciklama"].ToString();

                btnIlk.Enabled = (rowIndex > 0);
                btnOnceki.Enabled = (rowIndex > 0);
                btnSonraki.Enabled = (rowIndex < dataTable.Rows.Count - 1);
                btnSon.Enabled = (rowIndex < dataTable.Rows.Count - 1);
            }
        }
        private void btnIlk_Click(object sender, EventArgs e)
        {
            currentRowIndex = 0;
            VerileriDoldur(currentRowIndex);
            sekreterGetir(currentRowIndex);
        }
        private void btnOnceki_Click(object sender, EventArgs e)
        {
            if (currentRowIndex > 0)
            {
                currentRowIndex--;
                VerileriDoldur(currentRowIndex);
                sekreterGetir(currentRowIndex);
            }
        }
        private void btnSonraki_Click(object sender, EventArgs e)
        {
            if (currentRowIndex < dataTable.Rows.Count - 1)
            {
                currentRowIndex++;
                VerileriDoldur(currentRowIndex);
                sekreterGetir(currentRowIndex);
            }
        }
        private void btnSon_Click(object sender, EventArgs e)
        {
            currentRowIndex = dataTable.Rows.Count - 1;
            VerileriDoldur(currentRowIndex);
            sekreterGetir(currentRowIndex);
            btnSon.Enabled = false;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (currentRowIndex >= 0 && currentRowIndex < dataTable.Rows.Count)
            {
                DataRow row = dataTable.Rows[currentRowIndex];
                row["ad"] = txtAd.Text;
                row["soyad"] = txtSoyad.Text;
                row["ogrenci_no"] = txtOgrenciNo.Text;
                row["sinif"] = txtSinif.Text;
                row["telefon"] = txtTel.Text;
                row["eposta"] = txtEposta.Text;
                row["staj_kodu"] = txtStajKodu.Text;
                row["staj_yeri"] = txtStajYeri.Text;
                row["staj_baslama_tarihi"] = txtStajBaslangicTarihi.Text;
                row["staj_bitis_tarihi"] = txtStajBitisTarihi.Text;
                row["evrak"] = cbStajEvraklari.Checked == true ? 1 : 0;
                row["basvuru_dilekcesi"] = cbBasvuruDilekcesi.Checked == true ? 1 : 0;
                row["kabul_yazisi"] = cbKabulYazisi.Checked == true ? 1 : 0;
                row["mustehaklik_belgesi"] = cbMustehaklik.Checked == true ? 1 : 0;
                row["kimlik_fotokopisi"] = cbKimlik.Checked == true ? 1 : 0;
                row["staj_degerlendirme_formu"] = cbStajDegerlendirmeFormu.Checked == true ? 1 : 0;
                row["staj_raporu"] = cbStajRaporu.Checked == true ? 1 : 0;
                row["aciklama"] = txtAciklamalar.Text;

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE ogrenci SET tc_kimlik = @tckimlik, ad = @ad, soyad = @soyad, ogrenci_no = @ogrenciNo, sinif = @sinif, telefon = @telefon, eposta = @eposta, staj_kodu = @stajKodu, staj_yeri = @stajYeri, staj_baslama_tarihi = @stajBaslamaTarihi, staj_bitis_tarihi = @stajBitisTarihi, evrak = @evrak, basvuru_dilekcesi = @basvuruDilekcesi, kabul_yazisi = @kabulYazisi, mustehaklik_belgesi = @mustehaklikBelgesi, kimlik_fotokopisi = @kimlikFotokopisi, staj_degerlendirme_formu = @stajDegerlendirmeFormu, staj_raporu = @stajRaporu, aciklama = @aciklama WHERE id = @ogrenciId", baglanti))
                    {
                        cmd.Parameters.AddWithValue("@tckimlik", txtTckimlik.Text);
                        cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                        cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                        cmd.Parameters.AddWithValue("@ogrenciNo", txtOgrenciNo.Text);
                        cmd.Parameters.AddWithValue("@sinif", txtSinif.Text);
                        cmd.Parameters.AddWithValue("@telefon", txtTel.Text);
                        cmd.Parameters.AddWithValue("@eposta", txtEposta.Text);
                        cmd.Parameters.AddWithValue("@stajKodu", txtStajKodu.Text);
                        cmd.Parameters.AddWithValue("@stajYeri", txtStajYeri.Text);
                        cmd.Parameters.AddWithValue("@stajBaslamaTarihi", txtStajBaslangicTarihi.Text);
                        cmd.Parameters.AddWithValue("@stajBitisTarihi", txtStajBitisTarihi.Text);
                        cmd.Parameters.AddWithValue("@evrak", cbStajEvraklari.Checked);
                        cmd.Parameters.AddWithValue("@basvuruDilekcesi", cbBasvuruDilekcesi.Checked);
                        cmd.Parameters.AddWithValue("@kabulYazisi", cbKabulYazisi.Checked);
                        cmd.Parameters.AddWithValue("@mustehaklikBelgesi", cbMustehaklik.Checked);
                        cmd.Parameters.AddWithValue("@kimlikFotokopisi", cbKimlik.Checked);
                        cmd.Parameters.AddWithValue("@stajDegerlendirmeFormu", cbStajDegerlendirmeFormu.Checked);
                        cmd.Parameters.AddWithValue("@stajRaporu", cbStajRaporu.Checked);
                        cmd.Parameters.AddWithValue("@aciklama", txtAciklamalar.Text);
                        cmd.Parameters.AddWithValue("@ogrenciId", Convert.ToInt32(row["id"]));

                        baglanti.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt güncellendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

    }
}
