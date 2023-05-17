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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proje
{
    public partial class Akademisyen : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database = staj_degerlendirme; user=root;Pwd=");
        private int id;
        private int ogrenciId;
        public Akademisyen()
        {
            InitializeComponent();
        }

        public Akademisyen(int id)
        {
            this.id = id;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private void OgrenciListele()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ogrenci", baglanti))
                {
                    baglanti.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewOgrenciler.DataSource = dataTable;
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


        private void Akademisyen_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                cbIsyeriDegerlendirmesi.Items.Add(i);
                cbSekil.Items.Add(i);
                cbSoru1.Items.Add(i);
                cbSoru2.Items.Add(i);
                cbSoru3.Items.Add(i);
                cbSoru4.Items.Add(i);
                cbSoru5.Items.Add(i);
                cbSoru6.Items.Add(i);
                cbSoru7.Items.Add(i);
                cbSoru8.Items.Add(i);
                cbSoru9.Items.Add(i);
                cbSoru10.Items.Add(i);
                cbSoru11.Items.Add(i);
                cbSoru12.Items.Add(i);
                cbSoru13.Items.Add(i);
                cbSoru14.Items.Add(i);
                cbSoru15.Items.Add(i);
                cbSoru16.Items.Add(i);
                cbSoru17.Items.Add(i);
                cbSoru18.Items.Add(i);
                cbSoru19.Items.Add(i);
            }
            lblId.Text = id.ToString();
            OgrenciListele();
            PuanlariGetir(1);
            ortalamaGetir(1);
        }

        public void ortalamaGetir(int id)
        {
            try
            {
                string query = "SELECT ortalama FROM puan WHERE ogrenci_id = @ogrenciId";

                using (MySqlCommand cmd = new MySqlCommand(query, baglanti))
                {
                    cmd.Parameters.AddWithValue("@ogrenciId", id);

                    baglanti.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        double ortalama = Convert.ToDouble(result);
                        lblOrtalama.Text = "Ortalama: " + ortalama.ToString();
                    }
                    else
                    {
                        lblOrtalama.Text = "N/A";
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


        private void PuanlariGetir(int ogrenci_id)
        {
            if (dataGridViewOgrenciler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM puan WHERE ogrenci_id = @ogrenciId";

            using (MySqlCommand cmd = new MySqlCommand(query, baglanti))
            {
                cmd.Parameters.AddWithValue("@ogrenciId", ogrenci_id);

                baglanti.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cbIsyeriDegerlendirmesi.SelectedItem = Convert.ToInt32(reader["isyeri_degerlendirmesi"]);
                        cbSekil.SelectedItem = Convert.ToInt32(reader["sekil_bicim_yazidili"]);

                        cbSoru1.SelectedItem = Convert.ToInt32(reader["soru1"]);
                        cbSoru2.SelectedItem = Convert.ToInt32(reader["soru2"]);
                        cbSoru3.SelectedItem = Convert.ToInt32(reader["soru3"]);
                        cbSoru4.SelectedItem = Convert.ToInt32(reader["soru4"]);
                        cbSoru5.SelectedItem = Convert.ToInt32(reader["soru5"]);
                        cbSoru6.SelectedItem = Convert.ToInt32(reader["soru6"]);
                        cbSoru7.SelectedItem = Convert.ToInt32(reader["soru7"]);
                        cbSoru8.SelectedItem = Convert.ToInt32(reader["soru8"]);
                        cbSoru9.SelectedItem = Convert.ToInt32(reader["soru9"]);
                        cbSoru10.SelectedItem = Convert.ToInt32(reader["soru10"]);
                        cbSoru11.SelectedItem = Convert.ToInt32(reader["soru11"]);
                        cbSoru12.SelectedItem = Convert.ToInt32(reader["soru12"]);
                        cbSoru13.SelectedItem = Convert.ToInt32(reader["soru13"]);
                        cbSoru14.SelectedItem = Convert.ToInt32(reader["soru14"]);
                        cbSoru15.SelectedItem = Convert.ToInt32(reader["soru15"]);
                        cbSoru16.SelectedItem = Convert.ToInt32(reader["soru16"]);
                        cbSoru17.SelectedItem = Convert.ToInt32(reader["soru17"]);
                        cbSoru18.SelectedItem = Convert.ToInt32(reader["soru18"]);
                        cbSoru19.SelectedItem = Convert.ToInt32(reader["soru19"]);
                    }
                }

                baglanti.Close();
                ortalamaGetir(ogrenciId);
            }
        }

        private void btnGetOgrenci_Click(object sender, EventArgs e)
        {
            if (dataGridViewOgrenciler.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewOgrenciler.CurrentRow;
                ogrenciId = Convert.ToInt32(selectedRow.Cells["id"].Value);
                PuanlariGetir(ogrenciId);
                ortalamaGetir(ogrenciId);
            }
        }

        private void btnPuanKaydet_Click(object sender, EventArgs e)
        {
            if (ogrenciId == 0)
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int ortalama = (
                    Convert.ToInt32(cbIsyeriDegerlendirmesi.SelectedItem) +
                    Convert.ToInt32(cbSekil.SelectedIndex) +
                    Convert.ToInt32(cbSoru1.SelectedItem) +
                    Convert.ToInt32(cbSoru1.SelectedItem) +
                    Convert.ToInt32(cbSoru2.SelectedItem) +
                    Convert.ToInt32(cbSoru3.SelectedItem) +
                    Convert.ToInt32(cbSoru4.SelectedItem) +
                    Convert.ToInt32(cbSoru5.SelectedItem) +
                    Convert.ToInt32(cbSoru6.SelectedItem) +
                    Convert.ToInt32(cbSoru7.SelectedItem) +
                    Convert.ToInt32(cbSoru8.SelectedItem) +
                    Convert.ToInt32(cbSoru9.SelectedItem) +
                    Convert.ToInt32(cbSoru10.SelectedItem) +
                    Convert.ToInt32(cbSoru11.SelectedItem) +
                    Convert.ToInt32(cbSoru12.SelectedItem) +
                    Convert.ToInt32(cbSoru13.SelectedItem) +
                    Convert.ToInt32(cbSoru14.SelectedItem) +
                    Convert.ToInt32(cbSoru15.SelectedItem) +
                    Convert.ToInt32(cbSoru16.SelectedItem) +
                    Convert.ToInt32(cbSoru17.SelectedItem) +
                    Convert.ToInt32(cbSoru18.SelectedItem) +
                    Convert.ToInt32(cbSoru19.SelectedItem)
                ) / 21;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE puan SET ortalama = @ortalama, soru1 = @soru1, soru2 = @soru2, soru3 = @soru3, soru4 = @soru4, soru5 = @soru5, soru6 = @soru6, soru7 = @soru7, soru8 = @soru8, soru9 = @soru9, soru10 = @soru10, soru11 = @soru11, soru12 = @soru12, soru13 = @soru13, soru14 = @soru14, soru15 = @soru15, soru16 = @soru16, soru17 = @soru17, soru18 = @soru18, soru19 = @soru19 WHERE ogrenci_id = @ogrenciId", baglanti))
                {
                    cmd.Parameters.AddWithValue("@ortalama", ortalama);
                    cmd.Parameters.AddWithValue("@soru1", cbSoru1.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru2", cbSoru2.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru3", cbSoru3.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru4", cbSoru4.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru5", cbSoru5.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru6", cbSoru6.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru7", cbSoru7.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru8", cbSoru8.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru9", cbSoru9.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru10", cbSoru10.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru11", cbSoru11.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru12", cbSoru12.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru13", cbSoru13.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru14", cbSoru14.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru15", cbSoru15.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru16", cbSoru16.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru17", cbSoru17.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru18", cbSoru18.SelectedItem);
                    cmd.Parameters.AddWithValue("@soru19", cbSoru19.SelectedItem);
                    cmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);

                    baglanti.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Puanlar güncellendi.");
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
