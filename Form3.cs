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
using System.Net.Mail;




namespace Yeni
{
    public partial class Form3 : Form
    {
       // string cs = "data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
        public Form3()
        {
            InitializeComponent();
        }


        private void button4_Click(object sender, EventArgs e)
        {



            string eposta = "";
            foreach (var item in textBox5.Text)
            {
                eposta += ",";
                eposta += item;
            }

         //   eposta = eposta.Remove(0, 1);

            string ekipmanlar = "", ikramlar = "";
            foreach (var item in checkedListBox1.CheckedItems)
            {
                ekipmanlar += item;
                ekipmanlar += ",";
                
            }
           // ekipmanlar = ekipmanlar.Remove(0, 1);
            foreach (var item in checkedListBox2.CheckedItems)
            {
               
                ikramlar += item;
                ikramlar += ",";
            }
            //ikramlar = "";//ikramlar.Remove(0, 1);

            string katilimcilar = "";
            foreach (var item in listBox3.Items)
            {
                katilimcilar += item;
                katilimcilar += ",";
                
            }
           //katilimcilar = katilimcilar.Remove(0, 1);

            string baslangicSaati = "", bitisSaati = "";
            baslangicSaati = comboBox2.Text + " : " + comboBox3.Text;
            bitisSaati = comboBox4.Text + " : " + comboBox5.Text;

            listBox3.Items.Add(textBox1.Text);
            listBox1.Items.Add(textBox5.Text);
            using (SqlConnection conn = new SqlConnection())   //sql  bağlantısı kuruldu
            {
                conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO RECORDS VALUES(
@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)", conn);
                command.Parameters.Add(new SqlParameter("0", katilimcilar)); //listbox tan çekilecek.
                command.Parameters.Add(new SqlParameter("1", comboBox1.Text));
                command.Parameters.Add(new SqlParameter("2", dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("3", baslangicSaati));
                command.Parameters.Add(new SqlParameter("4", bitisSaati));
                command.Parameters.Add(new SqlParameter("5", textBox3.Text));
                command.Parameters.Add(new SqlParameter("6", ekipmanlar));
                command.Parameters.Add(new SqlParameter("7", ikramlar));
                command.Parameters.Add(new SqlParameter("8", textBox4.Text));
                command.Parameters.Add(new SqlParameter("9", textBox5.Text));
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Lütfen Boş Bırakılan Yerleri Doldurunuz !");
                }
                }



        //    try             //mail atma kısmı
        //    {

        //        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //        MailMessage message = new MailMessage();
        //        message.From = new MailAddress("melihoguzalpp@gmail.com");
        //        message.To.Add(textBox5.Text);
        //        message.Body = textBox3.Text;
        //        message.Subject = textBox4.Text;
        //        client.UseDefaultCredentials = false;
        //        client.EnableSsl = true;
        //        if (textBox6.Text != "")
        //        {
        //            message.Attachments.Add(new Attachment(textBox6.Text));

        //        }
        //        client.Credentials = new System.Net.NetworkCredential("melihoguzalpp@gmail.com", "damar123");
        //        client.Send(message);
        //        message = null;
        //        MessageBox.Show("Giriş işleminiz Başarıyla Tamamlanmıştır..");
        //    }


        //    catch (Exception s)
        //    {
        //        MessageBox.Show("Giriş İşleminiz Başarısız Olmuştur!");
        //    }


        //}

            try         
                                       //mail atma kısmı
            {

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                MailMessage message = new MailMessage();

                message.From = new MailAddress("halkbank.meetingreservation@gmail.com");

                foreach (var item in listBox1.Items)
                {
                    message.To.Add(new MailAddress(item.ToString()));
                }
                

                message.Body ="Toplatı Talebiniz Oluşturulmuştur\n"+
                              "Katılımcılar : "+katilimcilar+"\n"+
                              "Toplantı Salonu : "+comboBox1.Text.ToString()+"\n"+
                              "Talep Eden : "+textBox4.Text.ToString()+"\n"+
                              "Toplantı Konusu : "+ textBox3.Text.ToString()+"\n"+
                              "Toplantı Tarihi : "+ dateTimePicker1.Value.ToString("yyyy-MM-dd")+"\n"+
                              "Toplantı Saati : "+ baslangicSaati+"-"+bitisSaati+"\n"+
                              "Ekipman : "+ekipmanlar+"\n"+
                              "İkram : "+ikramlar+"\n";

                message.Subject = textBox4.Text.ToString();

                client.UseDefaultCredentials = true;//önceden false idi sonradan değiştirdim

                client.EnableSsl = true;
              
                if (textBox6.Text != "")
                {
                     message.Attachments.Add(new Attachment(textBox6.Text));

                }
                client.Credentials = new System.Net.NetworkCredential("halkbank.meetingreservation@gmail.com", "hlkbnk44");

                client.Send(message);

                message = null;

                MessageBox.Show("Giriş işleminiz Başarıyla Tamamlanmıştır..");
            }


            catch(Exception s)
            {
                MessageBox.Show("Giriş İşleminiz Başarısız Olmuştur!");
            }
       
             
        }

       

        private void button2_Click(object sender, EventArgs e) //listboxtaki kişileri silme
        {
            if (listBox3.Items.Count > 0)
            {
                if (listBox3.SelectedIndex != -1)
               {
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                }
           }
            else
                MessageBox.Show("Listede Eleman Yok.");
        }

        private void button5_Click(object sender, EventArgs e)  // formu temizle
        {
            textBox1.Clear();
            //textBox2.Clear();
            comboBox1.Text = "";
            textBox3.Clear();
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            listBox1.Items.Clear();//
           // listBox2.Items.Clear();//         sonradan girdim
            listBox3.Items.Clear();//
          //  listBox4.Items.Clear();//

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Cancel)
                Application.Exit();
        }

        

        private void button6_Click(object sender, EventArgs e)    //veritabanına kaydedilen kişileri tabloya(listbox3'e) ekleme
        {
            //if (listBox4.SelectedItem != null)
           // {

                //if (listBox4.SelectedItem.ToString().Length != 0)            katılımcı ekle butonunun kodu
                //{
                 //   listBox3.Items.Add(listBox4.SelectedItem);
                 //   listBox4.Items.RemoveAt(listBox4.SelectedIndex);
               // }
          //  }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection(cs);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //cmd.CommandText = "SELECT E_posta FROM RECORS";
            //conn.Open();
            //SqlDataReader dr = cmd.EndExecuteNonQuery();
            //ArrayList isimler = new ArrayList();
            //while(dr.Read())
            //{
            //    isimler.Add(dr["E_posta"]);

            //}
            //dr.Close();
            //conn.Close();

            //listele();
        }

        private void listele()  // kişi bilgilerini listeleme
        {
           //listBox2.Items.Clear();// sonradan ekledim
           // listBox4.Items.Clear();
           // using (SqlConnection conn = new SqlConnection())  //sql e bağlanma
           // {
           //     conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
           //     conn.Open();
           //    SqlCommand command = new SqlCommand(@"SELECT * FROM RESERVATION__PARTICIPANT", conn); 

           //     using (SqlDataReader reader = command.ExecuteReader())
           //    {
                   

           //         while (reader.Read()) // listbox a kaydedilen kişileri Okuma
           //         {
           //             listBox4.Items.Add(reader["USERNAME"].ToString());
           //             listBox2.Items.Add(reader["E_posta"].ToString());
           //        }
                  
           //     }
           // }
        }


        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != null)
            {

                if (textBox1.Text.ToString().Length != 0)
                {
                    listBox3.Items.Add(textBox1.Text);

                }
            }
            if (textBox5.Text != null)
            {

                if (textBox5.Text.ToString().Length != 0)
                {
                    listBox1.Items.Add(textBox5.Text);

                }
            }


            using (SqlConnection conn = new SqlConnection())  //sql e bağlanma
            {
                conn.ConnectionString = @"data source =DESKTOP-0TMQHFI\SQLEXPRESS;initial catalog=MEETING_RESERVATION;trusted_connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO RESERVATION__PARTICIPANT VALUES(
@0,@1)", conn);
                command.Parameters.Add(new SqlParameter("0", textBox1.Text)); //listbox tan çekilecek.
                command.Parameters.Add(new SqlParameter("1", textBox5.Text));
                command.ExecuteNonQuery();
              


            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               textBox6.Text= openFileDialog1.FileName.ToString(); // dosya ekleme
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //if (listBox2.SelectedItem != null)
          //  {

               // if (listBox2.SelectedItem.ToString().Length != 0)
                //{
             //       listBox1.Items.Add(listBox2.SelectedItem);
              //      listBox2.Items.RemoveAt(listBox2.SelectedIndex);
               // }
           // }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

           if (listBox1.Items.Count > 0)
            {
                if (listBox1.SelectedIndex != -1)
                {
                   listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
           }
            else
               MessageBox.Show("Listede Eleman Yok.");
        }
    }
    }

