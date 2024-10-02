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
using System.Data.Common;

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn =new SqlConnection (@"Data Source=DESKTOP-BC3LOP2\SQLEXPRESS01;Initial Catalog=Testbenzin;Integrated Security=True;Connect Timeout=30;");

        private void Form1_Load(object sender, EventArgs e)
        {
            fiyatListesi();
            hareketdata();
            temizle();
        }
        
        void fiyatListesi()
        {
            //Kurşunsuz 95
            conn.Open ();

            SqlCommand cmd = new SqlCommand("Select * From TBLBENZİN where petroltur='Kurşunsuz95'", conn);
            SqlDataReader dr = cmd.ExecuteReader ();
            while (dr.Read())
            {
                lblkursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblkursunsuz95Litre.Text = dr[4].ToString();
            }
            conn.Close ();


            //Kurşunsuz97
            conn.Open();

            SqlCommand cmd1 = new SqlCommand("Select * From TBLBENZİN where petroltur='Kurşunsuz97'", conn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                 lblkursunsuz97.Text = dr1[3].ToString();
                progressBar2.Value = int.Parse(dr1[4].ToString());
                lblkursunsuz97Litre.Text = dr1[4].ToString();
            }
            conn.Close();

            //EuroDizel10
            conn.Open();

            SqlCommand cmd2 = new SqlCommand("Select * From TBLBENZİN where petroltur='EuroDizel10'", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lblEuroDizel10.Text = dr2[3].ToString();
                progressBar3.Value = int.Parse(dr2[4].ToString());
                lblEuroDizel10Litre.Text = dr2[4].ToString();
            }
            conn.Close();

            //YeniProDizel
            conn.Open();

            SqlCommand cmd3 = new SqlCommand("Select * From TBLBENZİN where petroltur='YeniProDizel'", conn);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
               lblnewprodizel.Text = dr3[3].ToString();
                progressBar4.Value = int.Parse(dr3[4].ToString());
                lblnewprodizelLitre.Text = dr3[4].ToString();
            }
            conn.Close();

            //Gaz
            conn.Open();

            SqlCommand cmd4 = new SqlCommand("Select * From TBLBENZİN where petroltur='Gaz'", conn);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
               lblgaz.Text= dr4[3].ToString();
                progressBar5.Value = int.Parse(dr4[4].ToString());
                lblgazLitre.Text = dr4[4].ToString();
            }
            conn.Close();


            conn.Open();
            SqlCommand cmd5 = new SqlCommand("Select * from Tblkasa", conn);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while(dr5.Read())
            {
                lblkasatoplam.Text = dr5[0].ToString();
            }
            conn.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lblkursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz95 * litre;
            Txtkursunsuz95.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(lblkursunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = kursunsuz97 * litre;
            Txtkursunsuz97.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double EuroDizel10, litre, tutar;
            EuroDizel10 = Convert.ToDouble(lblEuroDizel10.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = EuroDizel10 * litre;
            TxtEuroDizel10.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double yeniProDizel, litre, tutar;
            yeniProDizel = Convert.ToDouble(lblnewprodizel.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = yeniProDizel * litre;
            Txtnewprodizel.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lblgaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = gaz * litre;
            Txtgaz.Text = tutar.ToString();
        }

        private void BtnDepoDoldur_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value >0 )
            {
                if (TxtPlaka.Text != "")
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTURU,LITRE,FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    cmd.Parameters.AddWithValue("@p2", "Kurşunsuz 95");
                    cmd.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(Txtkursunsuz95.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR+@p1", conn);
                    kmt2.Parameters.AddWithValue("@p1", decimal.Parse(Txtkursunsuz95.Text));
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Satış yapıldı");

                    conn.Open();
                    SqlCommand komut3 = new SqlCommand("update TBLBENZİN set STOK=STOK-@p1 where PETROLTUR='Kurşunsuz95'", conn);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                    komut3.ExecuteNonQuery();
                    conn.Close();
                    fiyatListesi();
                    temizle();
                    hareketdata();
                }
                else 
                {
                    MessageBox.Show("Boş alanlar var");
                }
            }

            if (numericUpDown2.Value > 0)
            {
                if (TxtPlaka.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTURU,LITRE,FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    cmd.Parameters.AddWithValue("@p2", "Kurşunsuz 97");
                    cmd.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(Txtkursunsuz97.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR+@p1", conn);
                    kmt2.Parameters.AddWithValue("@p1", decimal.Parse(Txtkursunsuz97.Text));
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Satış yapıldı");

                    conn.Open();
                    SqlCommand komut3 = new SqlCommand("update TBLBENZİN set STOK=STOK-@p1 where PETROLTUR='Kurşunsuz97'", conn);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                    komut3.ExecuteNonQuery();
                    conn.Close();
                    fiyatListesi();
                    temizle();
                    hareketdata();
                }

                else
                {
                    MessageBox.Show("Boş alanlar var");
                }
            }
            if (numericUpDown3.Value != 0)
            {
                if (TxtPlaka.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTURU,LITRE,FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    cmd.Parameters.AddWithValue("@p2", "EuroDizel 10");
                    cmd.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtEuroDizel10.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR+@p1", conn);
                    kmt2.Parameters.AddWithValue("@p1", decimal.Parse(TxtEuroDizel10.Text));
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Satış yapıldı");

                    conn.Open();
                    SqlCommand komut3 = new SqlCommand("update TBLBENZİN set STOK=STOK-@p1 where PETROLTUR='EuroDizel10'", conn);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                    komut3.ExecuteNonQuery();
                    conn.Close();
                    fiyatListesi();
                    temizle();
                    hareketdata();

                }
                else
                {
                    MessageBox.Show("Boş alanlar var");
                }
            }
            if (numericUpDown4.Value > 0)
            {
                if (TxtPlaka.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTURU,LITRE,FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    cmd.Parameters.AddWithValue("@p2", "EuroDizel 10");
                    cmd.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(Txtnewprodizel.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR+@p1", conn);
                    kmt2.Parameters.AddWithValue("@p1", decimal.Parse(Txtnewprodizel.Text));
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Satış yapıldı");

                    conn.Open();
                    SqlCommand komut3 = new SqlCommand("update TBLBENZİN set STOK=STOK-@p1 where PETROLTUR='YeniProDizel'", conn);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                    komut3.ExecuteNonQuery();
                    conn.Close();
                    fiyatListesi();
                    temizle();
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Boş alanlar var");
                }
            }
            if (numericUpDown5.Value > 0)
            {
                if (TxtPlaka.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTURU,LITRE,FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    cmd.Parameters.AddWithValue("@p2", "EuroDizel 10");
                    cmd.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(Txtgaz.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR+@p1", conn);
                    kmt2.Parameters.AddWithValue("@p1", decimal.Parse(Txtgaz.Text));
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Satış yapıldı");

                    conn.Open();
                    SqlCommand komut3 = new SqlCommand("update TBLBENZİN set STOK=STOK-@p1 where PETROLTUR='Gaz'", conn);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                    komut3.ExecuteNonQuery();
                    conn.Close();
                    fiyatListesi();
                    temizle();
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Boş alanlar var");
                }

            }



        }

        void hareketdata()
        {

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLHAREKET ", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
          
        }
        void temizle()
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            TxtEuroDizel10.Text = "";
            Txtkursunsuz95.Text = "";
            Txtkursunsuz97.Text = "";
            Txtnewprodizel.Text = "";
            Txtgaz.Text = "";
            TxtPlaka.Text = "";
            txtLitre.Text = "";
        }

        private void btnEklek95_Click(object sender, EventArgs e)
        {
            if (lblkursunsuz95Litre.Text != "10000")
            { 
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
            conn.Open();

            // Check the current stock level first
            SqlCommand checkStockCmd = new SqlCommand("SELECT STOK FROM TBLBENZİN WHERE PETROLTUR = 'Kurşunsuz95'", conn);
            int currentStock = (int)checkStockCmd.ExecuteScalar();

            int newLitre = int.Parse(txtLitre.Text);
            if (currentStock + newLitre <= 10000)  // Check if adding the new stock exceeds 10000
            {
                // Deduct the amount from the cash register (Tblkasa)
                SqlCommand komut = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR-@p1 WHERE MIKTAR>=0", conn);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();

                // Update the stock (TBLBENZİN)
                SqlCommand komut2 = new SqlCommand("update TBLBENZİN set STOK=STOK+@p1 where PETROLTUR='Kurşunsuz97' AND STOK <= 10000", conn);
                komut2.Parameters.AddWithValue("@p1", newLitre);
                komut2.ExecuteNonQuery();

                // Insert transaction into TBLHAREKET
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU, LITRE, FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                komut13.Parameters.AddWithValue("@p3", newLitre);
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();

                MessageBox.Show("Alış Gerçekleşti");
                fiyatListesi(); // Refresh price list
                temizle();      // Clear inputs
                    hareketdata();
                }
            else
            {
                MessageBox.Show("Depo kapasitesini aşacak. Maksimum 10,000 litre!");
            }

            conn.Close();
        }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }



        }

        private void btneklek97_Click(object sender, EventArgs e)
        {
            if (lblkursunsuz97Litre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
                conn.Open();

                // Check the current stock level first
                SqlCommand checkStockCmd = new SqlCommand("SELECT STOK FROM TBLBENZİN WHERE PETROLTUR = 'Kurşunsuz97'", conn);
                int currentStock = (int)checkStockCmd.ExecuteScalar();

                int newLitre = int.Parse(txtLitre.Text);
                if (currentStock + newLitre <= 10000)  // Check if adding the new stock exceeds 10000
                {
                    // Deduct the amount from the cash register (Tblkasa)
                    SqlCommand komut = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR-@p1 WHERE MIKTAR>=0", conn);
                    komut.Parameters.AddWithValue("@p1", tutar);
                    komut.ExecuteNonQuery();

                    // Update the stock (TBLBENZİN)
                    SqlCommand komut2 = new SqlCommand("update TBLBENZİN set STOK=STOK+@p1 where PETROLTUR='Kurşunsuz97' AND STOK <= 10000", conn);
                    komut2.Parameters.AddWithValue("@p1", newLitre);
                    komut2.ExecuteNonQuery();

                    // Insert transaction into TBLHAREKET
                    SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU, LITRE, FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    komut13.Parameters.AddWithValue("@p1", "ALIM");
                    komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                    komut13.Parameters.AddWithValue("@p3", newLitre);
                    komut13.Parameters.AddWithValue("@p4", tutar);
                    komut13.ExecuteNonQuery();

                    MessageBox.Show("Alış Gerçekleşti");
                    fiyatListesi(); // Refresh price list
                    temizle();      // Clear inputs
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Depo kapasitesini aşacak. Maksimum 10,000 litre!");
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }


        private void btnEkleYpd_Click(object sender, EventArgs e)
        {
            if (lblnewprodizel.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
                conn.Open();

                // Check the current stock level first
                SqlCommand checkStockCmd = new SqlCommand("SELECT STOK FROM TBLBENZİN WHERE PETROLTUR = 'YeniProDizel'", conn);
                int currentStock = (int)checkStockCmd.ExecuteScalar();

                int newLitre = int.Parse(txtLitre.Text);
                if (currentStock + newLitre <= 10000)  // Check if adding the new stock exceeds 10000
                {
                    // Deduct the amount from the cash register (Tblkasa)
                    SqlCommand komut = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR-@p1 WHERE MIKTAR>=0", conn);
                    komut.Parameters.AddWithValue("@p1", tutar);
                    komut.ExecuteNonQuery();

                    // Update the stock (TBLBENZİN)
                    SqlCommand komut2 = new SqlCommand("update TBLBENZİN set STOK=STOK+@p1 where PETROLTUR='Kurşunsuz97' AND STOK <= 10000", conn);
                    komut2.Parameters.AddWithValue("@p1", newLitre);
                    komut2.ExecuteNonQuery();

                    // Insert transaction into TBLHAREKET
                    SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU, LITRE, FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    komut13.Parameters.AddWithValue("@p1", "ALIM");
                    komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                    komut13.Parameters.AddWithValue("@p3", newLitre);
                    komut13.Parameters.AddWithValue("@p4", tutar);
                    komut13.ExecuteNonQuery();

                    MessageBox.Show("Alış Gerçekleşti");
                    fiyatListesi(); // Refresh price list
                    temizle();      // Clear inputs
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Depo kapasitesini aşacak. Maksimum 10,000 litre!");
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }

        private void btnEkleGaz_Click(object sender, EventArgs e)
        {
            if (lblgazLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
                conn.Open();

                // Check the current stock level first
                SqlCommand checkStockCmd = new SqlCommand("SELECT STOK FROM TBLBENZİN WHERE PETROLTUR = 'Gaz'", conn);
                int currentStock = (int)checkStockCmd.ExecuteScalar();

                int newLitre = int.Parse(txtLitre.Text);
                if (currentStock + newLitre <= 10000)  // Check if adding the new stock exceeds 10000
                {
                    // Deduct the amount from the cash register (Tblkasa)
                    SqlCommand komut = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR-@p1 WHERE MIKTAR>=0", conn);
                    komut.Parameters.AddWithValue("@p1", tutar);
                    komut.ExecuteNonQuery();

                    // Update the stock (TBLBENZİN)
                    SqlCommand komut2 = new SqlCommand("update TBLBENZİN set STOK=STOK+@p1 where PETROLTUR='Kurşunsuz97' AND STOK <= 10000", conn);
                    komut2.Parameters.AddWithValue("@p1", newLitre);
                    komut2.ExecuteNonQuery();

                    // Insert transaction into TBLHAREKET
                    SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU, LITRE, FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    komut13.Parameters.AddWithValue("@p1", "ALIM");
                    komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                    komut13.Parameters.AddWithValue("@p3", newLitre);
                    komut13.Parameters.AddWithValue("@p4", tutar);
                    komut13.ExecuteNonQuery();

                    MessageBox.Show("Alış Gerçekleşti");
                    fiyatListesi(); // Refresh price list
                    temizle();      // Clear inputs
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Depo kapasitesini aşacak. Maksimum 10,000 litre!");
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }

        private void btneuro(object sender, EventArgs e)
        {
            if (lblEuroDizel10Litre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
                conn.Open();

                // Check the current stock level first
                SqlCommand checkStockCmd = new SqlCommand("SELECT STOK FROM TBLBENZİN WHERE PETROLTUR = 'Kurşunsuz97'", conn);
                int currentStock = (int)checkStockCmd.ExecuteScalar();

                int newLitre = int.Parse(txtLitre.Text);
                if (currentStock + newLitre <= 10000)  // Check if adding the new stock exceeds 10000
                {
                    // Deduct the amount from the cash register (Tblkasa)
                    SqlCommand komut = new SqlCommand("update Tblkasa set MIKTAR=MIKTAR-@p1 WHERE MIKTAR>=0", conn);
                    komut.Parameters.AddWithValue("@p1", tutar);
                    komut.ExecuteNonQuery();

                    // Update the stock (TBLBENZİN)
                    SqlCommand komut2 = new SqlCommand("update TBLBENZİN set STOK=STOK+@p1 where PETROLTUR='EuroDizel10' AND STOK <= 10000", conn);
                    komut2.Parameters.AddWithValue("@p1", newLitre);
                    komut2.ExecuteNonQuery();

                    // Insert transaction into TBLHAREKET
                    SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU, LITRE, FİYAT) values (@p1,@p2,@p3,@p4)", conn);
                    komut13.Parameters.AddWithValue("@p1", "ALIM");
                    komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                    komut13.Parameters.AddWithValue("@p3", newLitre);
                    komut13.Parameters.AddWithValue("@p4", tutar);
                    komut13.ExecuteNonQuery();

                    MessageBox.Show("Alış Gerçekleşti");
                    fiyatListesi(); // Refresh price list
                    temizle();      // Clear inputs
                    hareketdata();
                }
                else
                {
                    MessageBox.Show("Depo kapasitesini aşacak. Maksimum 10,000 litre!");
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }
    }
}
