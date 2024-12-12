using System;
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
    public partial class FrmSalonKayit : Form
    {
        public FrmSalonKayit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // SqlConnection nesnesini burada tanımlayın.
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7EPCTQ4;Initial Catalog=cinemaiXVT;Integrated Security=True");

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtSalonAdi.Text!="" && cBKoltukSayisi.Text!="") 
            {
                connection.Open();
                SqlCommand kaydet = new SqlCommand("insert into Tbl_Salonlar (SALONADI,KOLTUKSAYISI) Values (@p1,@p2)", connection);
                kaydet.Parameters.AddWithValue("@p1",txtSalonAdi.Text.ToUpper());
                kaydet.Parameters.AddWithValue("@p2", cBKoltukSayisi.Text);
                kaydet.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("işleminiz başarıyla kaydedilmiştir");
                txtSalonAdi.Text = "";
                cBKoltukSayisi.Text = "";
                txtSalonAdi.Focus();
            }
            else 
            {
                MessageBox.Show("Lütfen salon adı ve koltuk sayısı girdiğiinizden emin olun");
            }
        }

        private void FrmSalonKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
