namespace proje
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnAkademisyenGiris = new System.Windows.Forms.Button();
            this.btnSekreterGiris = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(148, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Giriş Yap";
            // 
            // btnAkademisyenGiris
            // 
            this.btnAkademisyenGiris.Location = new System.Drawing.Point(142, 120);
            this.btnAkademisyenGiris.Name = "btnAkademisyenGiris";
            this.btnAkademisyenGiris.Size = new System.Drawing.Size(108, 37);
            this.btnAkademisyenGiris.TabIndex = 6;
            this.btnAkademisyenGiris.Text = "Akademisyen";
            this.btnAkademisyenGiris.UseVisualStyleBackColor = true;
            this.btnAkademisyenGiris.Click += new System.EventHandler(this.btnAkademisyenGiris_Click);
            // 
            // btnSekreterGiris
            // 
            this.btnSekreterGiris.Location = new System.Drawing.Point(142, 164);
            this.btnSekreterGiris.Name = "btnSekreterGiris";
            this.btnSekreterGiris.Size = new System.Drawing.Size(108, 40);
            this.btnSekreterGiris.TabIndex = 7;
            this.btnSekreterGiris.Text = "Sekreter";
            this.btnSekreterGiris.UseVisualStyleBackColor = true;
            this.btnSekreterGiris.Click += new System.EventHandler(this.btnSekreterGiris_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 302);
            this.Controls.Add(this.btnSekreterGiris);
            this.Controls.Add(this.btnAkademisyenGiris);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staj Takip Uyuglaması Giriş Formu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAkademisyenGiris;
        private System.Windows.Forms.Button btnSekreterGiris;
    }
}

