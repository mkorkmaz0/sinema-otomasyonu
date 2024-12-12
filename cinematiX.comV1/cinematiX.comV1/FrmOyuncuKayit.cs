﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace cinematiX.comV1
{
    public partial class FrmOyuncuKayit : Form
    {
        // SqlConnection nesnesini burada tanımlayın.
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7EPCTQ4;Initial Catalog=cinemaiXVT;Integrated Security=True");

        public FrmOyuncuKayit()
        {
            InitializeComponent();
        }

        public string resimYolu = "";
        public string cinsiyet = "0";
        public string bYas = "00";

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResimYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "RESİM SEÇME EKRANI";
            ofd.Filter = "PNG | *.png | JPG-JPEG | *jpg;*.jpeg | All Files | *.*";
            ofd.FilterIndex = 3;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pBResim.Image = new Bitmap(ofd.FileName);
                resimYolu = ofd.FileName.ToString();
            }
        }

        private void rBErkek_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "0";
        }

        private void rbkADİN_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "1";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            yasHesaplama();

            if (txtAd.Text != "" && txtSoyad.Text != "" && txtBiyografi.Text != "" && resimYolu != "")
            {
                string adSoyad = txtAd.Text.ToString().ToUpper() + " " + txtSoyad.Text.ToString().ToUpper();

                connection.Open();
                SqlCommand command = new SqlCommand("insert into TBL_Oyuncular (ADSOYAD,CINSIYET,YAS,BIYOGRAFI,RESİM) VALUES (@p1,@p2,@p3,@p4,@p5)", connection);
                command.Parameters.AddWithValue("@p1", adSoyad);
                command.Parameters.AddWithValue("@p2", cinsiyet);
                command.Parameters.AddWithValue("@p3", bYas);
                command.Parameters.AddWithValue("@p4", txtBiyografi.Text.ToString().ToUpper());
                command.Parameters.AddWithValue("@p5", resimYolu);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Yönetmen kaydınız yapılmıştır");
                aracTemizle();
            }
            else
            {
                MessageBox.Show("Bazı alanlar boş, lütfen doldurunuz.");
            }
        }

        void aracTemizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtBiyografi.Text = "";
            nGun.Value = 1;
            nAy.Value = 1;
            nYil.Value = 2023;
            rBErkek.Checked = true;
            rbkADİN.Checked = false;
            lblKarakter.Text = "300";
            cinsiyet = "0";
            bYas = "00";
            pBResim.ImageLocation = @"C:\Users\melik\Downloads\noim.png";
            txtAd.Focus();
        }

        void yasHesaplama()
        {
            string dogum = nGun.Value.ToString() + "-" + nAy.Value.ToString() + "-" + nYil.Value.ToString();
            DateTime dogumTarihi = Convert.ToDateTime(dogum);
            DateTime bugun = DateTime.Today;
            int yas = bugun.Year - dogumTarihi.Year;

            bYas = yas.ToString();
        }

        private void txtBiyografi_TextChanged(object sender, EventArgs e)
        {
            int count = txtBiyografi.Text.Length;
            int backdown = 300 - count;
            lblKarakter.Text = backdown.ToString();

            if (count > 20)
            {
                lblKarakter.ForeColor = Color.Teal;
            }
            else if (count > 10)
            {
                lblKarakter.ForeColor = Color.Purple;
            }
            else
            {
                lblKarakter.ForeColor = Color.Red;
            }
        }
    }
}
