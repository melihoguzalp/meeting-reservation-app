using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace Yeni
{
    public partial class Form4 : Form
    {
        string[] dizi;
        int counter = 0;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();//datagridi ekle
            SqlCommand command = new SqlCommand("SELECT * FROM RECORDS WHERE Organizatör LIKE '%" + textBox1.Text + "%'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            //panel1.Show();
        }

        private string[] haftalikListele(string salonAdi)
        {
            string doluSaatler = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT Başlangıç_Saati, Bitiş_Saati FROM RECORDS WHERE Toplantı_Tarihi = @0 AND Toplantı_Salonu = @1", conn);
                command.Parameters.Add(new SqlParameter("0", dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("1", salonAdi));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Başlangıç_Saati"] != null)
                        {
                            doluSaatler += reader["Başlangıç_Saati"].ToString();
                        }
                        if (reader["Bitiş_Saati"] != null)
                        {
                            doluSaatler += "    -   ";
                            doluSaatler += reader["Bitiş_Saati"].ToString();
                            doluSaatler += ",";
                        }
                    }

                }
            }
            string[] saatDizi = doluSaatler.Split(',');
            return saatDizi;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          //datagridFulle();
            listeleriDoldur();
        }

        private void listeleriDoldur()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            counter = 0;
            List<String> toplantiSalonları = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
                conn.Open();
                //SqlCommand command = new SqlCommand("SELECT Toplantı_Salonu FROM RECORDS", conn);
                //using (SqlDataReader reader = command.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        if (reader["Toplantı_Salonu"] != null)
                //        {
                //            counter++;
                //        }
                //    }
                //}
                //dizi = new string[counter];
            
                //counter = 0;
                SqlCommand command2 = new SqlCommand("SELECT DISTINCT Toplantı_Salonu FROM RECORDS ORDER BY Toplantı_Salonu", conn);
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Toplantı_Salonu"] != null)  // dizinin boyunu belirleriz.
                        {
                           // Console.WriteLine("tolantiSalonları" + reader["Toplantı_Salonu"].ToString());
                            toplantiSalonları.Add(reader["Toplantı_Salonu"].ToString());
                           // dizi[counter] = reader["Toplantı_Salonu"].ToString();
                            //counter++;
                        }
                    }
                }
            }
            foreach (string item in toplantiSalonları)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in haftalikListele(comboBox1.Text))
           {
                listBox1.Items.Add(item);
           }
        }
        SqlConnection conn = new SqlConnection("data source =DESKTOP-0TMQHFI\\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true;");

        private void datagridFulle(string datas)
        {
           
            {
               
                SqlDataAdapter da = new SqlDataAdapter(datas,conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
               
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();
               SqlCommand command = new SqlCommand("DELETE FROM RECORDS WHERE Organizatör=@organizator", conn);
               command.Parameters.AddWithValue("@Organizator", textBox2.Text);
               command.ExecuteNonQuery();
               datagridFulle("SELECT * FROM RECORDS");
               conn.Close();
               textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            datagridFulle("SELECT * FROM RECORDS");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.DialogResult != DialogResult.Cancel)
                // this.DialogResult = DialogResult.Cancel;
                this.Close();
            
        }

        









        //private void button4_Click(object sender, EventArgs e)
        //{
        // datagridFulle("SELECT * FROM RECORDS");
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    conn.Open();
        //    SqlCommand command = new SqlCommand("DELETE FROM RECORDS WHERE Organizatör=@organizator", conn);
        //    command.Parameters.AddWithValue("@Organizator", textBox2.Text);
        //    command.ExecuteNonQuery();
        //    datagridFulle("SELECT * FROM RECORDS");
        //    conn.Close();
        //    textBox2.Clear();
        //}

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
        //        conn.Open();
        //        DataTable table = new DataTable();
        //        SqlDataAdapter adapter = new SqlDataAdapter("Select * From RECORDS Where Organizatör Like '%" + textBox1.Text + "%'", conn);
        //        adapter.Fill(table);
        //        conn.Close();
        //        dataGridView1.DataSource = table;

        //    }
        //}
    }
    }

