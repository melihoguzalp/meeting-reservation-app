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
using System.Data.Sql;


namespace Yeni
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (true)
            {
                this.Hide();
                Form3 yeni = new Form3();
                yeni.ShowDialog();
                this.Show();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (true)
            {

              //  this.Hide();
                Form4 yeni = new Form4();
                yeni.ShowDialog();
               

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }
        SqlConnection conn = new SqlConnection("data source =DESKTOP-0TMQHFI\\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true;");

        private void datagridFulle(string datas)
        {

            {

                SqlDataAdapter da = new SqlDataAdapter(datas, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }

        }


        private void button6_Click(object sender, EventArgs e)
        {
            datagridFulle("SELECT * FROM RECORDS");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          //  string baslangicSaati = "", bitisSaati = "";
           // baslangicSaati = comboBox2.Text + " . " + comboBox3.Text;
            //bitisSaati = comboBox4.Text + " . " + comboBox5.Text;

            int selectarea = dataGridView1.SelectedCells[0].RowIndex;
            id_no = dataGridView1.Rows[selectarea].Cells[0].Value.ToString();
            string Katılımcılar = dataGridView1.Rows[selectarea].Cells[1].Value.ToString();
            string Toplantı_Salonu= dataGridView1.Rows[selectarea].Cells[2].Value.ToString();
            DateTime Toplantı_Tarihi = (DateTime)dataGridView1.Rows[selectarea].Cells[3].Value;
            string Başlangıç_Saati = dataGridView1.Rows[selectarea].Cells[4].Value.ToString();
            string Bitiş_Saati = dataGridView1.Rows[selectarea].Cells[5].Value.ToString();
            string Toplantı_Gündemi = dataGridView1.Rows[selectarea].Cells[6].Value.ToString();
            string Organizatör = dataGridView1.Rows[selectarea].Cells[9].Value.ToString();





            textBox1.Text = Katılımcılar;
            comboBox1.Text = Toplantı_Salonu;
            dateTimePicker1.Value = Toplantı_Tarihi;
            textBox4.Text = Başlangıç_Saati;
            textBox5.Text = Bitiş_Saati;
            textBox3.Text = Toplantı_Gündemi;
            textBox2.Text = Organizatör;
          
            

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            conn.Open();
            SqlCommand command1 = new SqlCommand("UPDATE RECORDS SET Katılımcılar='" + textBox1.Text + "',Toplantı_Salonu='" + comboBox1.Text + "',Toplantı_Tarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',Başlangıç_Saati='" + textBox4.Text + "',Bitiş_Saati='" + textBox5.Text + "',Toplantı_Gündemi='" + textBox3.Text + "',Organizatör='" + textBox2.Text + "' WHERE ID='" + id_no + "'", conn);
            command1.ExecuteNonQuery();
            datagridFulle("SELECT * FROM RECORDS");
            conn.Close();
        }
    }
}
