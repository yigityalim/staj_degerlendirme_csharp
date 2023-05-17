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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSekreterGiris_Click(object sender, EventArgs e)
        {
            SekreterGiris sg = new SekreterGiris();
            this.Hide();
            sg.ShowDialog();
            this.Show();
        }

        private void btnAkademisyenGiris_Click(object sender, EventArgs e)
        {
            AkademisyenGiris ag = new AkademisyenGiris();
            this.Hide();
            ag.ShowDialog();
            this.Show();
        }
    }
}
