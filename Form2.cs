using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection youConnection = new SqlConnection("Data Source =DESKTOP-PIDM175; Initial Catalog = new_database; Integrated Security = true");


        private void demonstrate(string verilenler)
        {


            using (SqlDataAdapter a = new SqlDataAdapter("SELECT *  FROM fennler where  id = '" + Convert.ToInt32(label1.Text) + "' ", youConnection))
            {

                //

                if (youConnection.State == ConnectionState.Closed)
                    youConnection.Open();



                var t = new DataTable();
                a.Fill(t);


                listBoxControl1.DisplayMember = "fenn_adi";
                listBoxControl1.ValueMember = "fenn_adii";
                listBoxControl1.DataSource = t;
                youConnection.Close();

            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            listBoxControl1.Name = "";

            textEdit2.Visible = false;
            label1.Visible = false;
            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            demonstrate("SELECT *  FROM fennler where id = '" + label1.Text + "'");
        }

       

        

       
        

       
        

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {

                youConnection.Open();


                SqlCommand cmd = new SqlCommand("insert into fennler(fenn_adi,id) values (@fenn_adii,@idsi)", youConnection);


                cmd.Parameters.AddWithValue("@fenn_adii", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@idsi", Convert.ToInt32(label1.Text));
                cmd.ExecuteNonQuery();

                youConnection.Close();
                richTextBox1.Text = "";
                demonstrate("SELECT *  FROM fennler where id = '" + label1.Text + "'");

            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex >= 0)
            {
                this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogResult result2 = MessageBox.Show("Bu fenni sildikde fenne aid bütün melumatlar silinəcək!!! Silmək istəyirsənsə Yes düyməsinə,silmək istəmirsənsə No düyməsinə basın",
                    "Important Query",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                //}
                if (result2 == DialogResult.Yes)
                {
                    youConnection.Open();
                    SqlCommand cmd = new SqlCommand("delete from  fennler where  fenn_id=@fenn_idsi  ", youConnection);

                    cmd.Parameters.AddWithValue("@fenn_idsi", Convert.ToInt32(textEdit2.Text));
                    cmd.ExecuteNonQuery();
                    // demonstrate("select *from fennler ");
                    demonstrate("SELECT *  FROM fennler where id = '" + Convert.ToInt32(label1.Text) + "'");

                }
            }
            else
                MessageBox.Show("Zəhmət olmasa qutudan siləcəyiniz qrupu seçin!!!");
            youConnection.Close();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex >= 0)
            {

                string query = "select * from fennler where fenn_adi =@p";
                SqlCommand cmd = new SqlCommand(query, youConnection);
                cmd.Parameters.AddWithValue("@p", listBoxControl1.Text);
                SqlDataReader dbr;

                try
                {
                    youConnection.Open();
                    dbr = cmd.ExecuteReader();
                    while (dbr.Read())
                    {
                        string idsi = (string)dbr["fenn_id"].ToString();
                        textEdit2.Text = idsi;
                        Form3 frmmm = new Form3();
                        frmmm.label9.Text = textEdit2.Text;
                        frmmm.Show();
                        this.Hide();

                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }

            }
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        

        private void listBoxControl1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (youConnection.State == ConnectionState.Closed)
                youConnection.Open();
            string query = "select * from fennler where fenn_adi = '" + listBoxControl1.Text + "' ";
            SqlCommand cmd = new SqlCommand(query, youConnection);
            SqlDataReader dbr;



            dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                string fenn_ad = (string)dbr["fenn_adi"].ToString();
                string idsi = (string)dbr["fenn_id"].ToString();
                textEdit2.Text = idsi;

            }
            youConnection.Close();
        }
    }
}