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
using DevExpress.XtraBars.Ribbon.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string sql ="Data Source =DESKTOP-PIDM175; Initial Catalog = new_database; Integrated Security = true";
        SqlConnection myConnection = new SqlConnection(sql);
        string sorgu = "SELECT * FROM Qruplar";
        private void goster(string verilen)
        {
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            
            using (SqlDataAdapter a = new SqlDataAdapter(sorgu, myConnection))
            {

                var t = new DataTable();
                a.Fill(t);

                // Bind the table to the list box
                listBoxControl1.DisplayMember = "qrup_ad";
                listBoxControl1.ValueMember = "qrup_adi";
                listBoxControl1.DataSource = t;

            }
            myConnection.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textEdit1.Visible = false;
            textEdit2.Visible = false;
            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            goster(sql);

        }



        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            myConnection.Open();
            if (richTextBox1.Text != "")
            {
                
                string query = "INSERT INTO  Qruplar(qrup_ad) VALUES(@p)";
                SqlCommand cmd = new SqlCommand(query, myConnection);
                cmd.Parameters.AddWithValue("@p", richTextBox1.Text.Trim());

                cmd.ExecuteNonQuery();


                richTextBox1.Text = "";
                goster("select * from  Qruplar");

            }
            myConnection.Close();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex >= 0)
            {
                this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogResult result2 = MessageBox.Show("Bu qrupu sildikde qrupa aid bütün melumatlar silinəcək!!! Silmək istəyirsənsə Yes düyməsinə,silmək istəmirsənsə No düyməsinə basın",
                    "Important Query",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result2 == DialogResult.Yes)
                {
                    myConnection.Open();
                    
                    string sql = "delete from  Qruplar where  id=@idsi";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    cmd.Parameters.AddWithValue("@idsi", Convert.ToInt64(textEdit2.Text));//textBox2

                    cmd.ExecuteNonQuery();
                    goster("select * from  Qruplar");
                }
            }
            else
                MessageBox.Show("Zəhmət olmasa qutudan siləcəyiniz qrupu seçin!!!");

            myConnection.Close();
        }

        private void listBoxControl1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            string query = "select * from Qruplar where qrup_ad =@p";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            cmd.Parameters.AddWithValue("@p", listBoxControl1.Text);


            SqlDataReader dbr;



            dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                string qrup_adi = (string)dbr["qrup_ad"].ToString();
                string idsi = (string)dbr["id"].ToString();
                textEdit1.Text = qrup_adi;
                textEdit2.Text = idsi;              
            }
            myConnection.Close();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            myConnection.Open();
            if (this.listBoxControl1.SelectedIndex >= 0)
            {


                string query = "select * from  Qruplar where qrup_ad =@p";//" '" + listBoxControl1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, myConnection);
                cmd.Parameters.AddWithValue("@p", listBoxControl1.Text);
                SqlDataReader dbr;



                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    string qrup_adi = (string)dbr["qrup_ad"].ToString();
                    string idsi = (string)dbr["id"].ToString();
                    textEdit2.Text = qrup_adi;
                    textEdit1.Text = idsi;

                    Form2 frmm = new Form2();
                    frmm.textEdit2.Text = textEdit2.Text;
                    frmm.label1.Text = textEdit1.Text;//id
                    frmm.label3.Text = textEdit2.Text;//ad
                    frmm.Show();
                    this.Hide();


                }
                myConnection.Close();
            }
        }
    }
}