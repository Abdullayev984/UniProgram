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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        static string sql = "Data Source =DESKTOP-PIDM175; Initial Catalog = new_database; Integrated Security = true";
        SqlConnection conn = new SqlConnection(sql);

        private void verileri_goster(string veri)
        {
            SqlDataAdapter da = new SqlDataAdapter(veri, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gridControl1.DataSource = ds.Tables[0];
        }




        private void Form3_Load(object sender, EventArgs e)
        {
            label9.Visible = false;
            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            verileri_goster("select * from telebeler where fenn_id='" + label9.Text + "'");
            gridView1.Columns["fenn_id"].Visible = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            conn.Open();



            SqlCommand cmd = new SqlCommand("insert into telebeler(fenn_id,ad,soyad,ata_adı,kollokium1,kollokium2,kollokium3,sərbəst_is,davamiyyət,imtahan_bal,yekun_bal) values (@fenn_idsi,@adi,@soyadi,@ata_adıi,@kollokium1i,@kollokium2i,@kollokium3i,@sərbəst_isi,@davamiyyəti,@imtahan_bali,@yekun_bali)", conn);

            cmd.Parameters.AddWithValue("@fenn_idsi", Convert.ToInt32(label9.Text));
            cmd.Parameters.AddWithValue("@adi", textEdit1.Text);

            cmd.Parameters.AddWithValue("@soyadi", textEdit2.Text);
            cmd.Parameters.AddWithValue("@ata_adıi", textEdit3.Text);
            cmd.Parameters.AddWithValue("@kollokium1i", Convert.ToInt32(textEdit4.Text));
            cmd.Parameters.AddWithValue("@kollokium2i", Convert.ToInt32(textEdit5.Text));
            cmd.Parameters.AddWithValue("@kollokium3i", Convert.ToInt32(textEdit6.Text));
            cmd.Parameters.AddWithValue("@sərbəst_isi", Convert.ToInt32(textEdit7.Text));
            cmd.Parameters.AddWithValue("@davamiyyəti", Convert.ToInt32(textEdit8.Text));
            cmd.Parameters.AddWithValue("@imtahan_bali", Convert.ToInt32(textEdit10.Text));
            cmd.Parameters.AddWithValue("@yekun_bali", Convert.ToInt32(textEdit11.Text));


            cmd.ExecuteScalar();
            verileri_goster("select * from telebeler where fenn_id='" + label9.Text + "'");






            conn.Close();
            textEdit1.Text="";
            textEdit2.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit8.Text = "";
            textEdit10.Text = "";
            textEdit11.Text = "";

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            conn.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("delete from telebeler where sıra_No=@sıra_Nosu", conn);
                cmd.Parameters.AddWithValue("@sıra_Nosu", textEdit9.Text);
                cmd.ExecuteNonQuery();

                verileri_goster("select * from telebeler where fenn_id='" + label9.Text + "'");
                // textBox13.Text = "";
                textEdit9.Text="";
            }
            catch (Exception)
            {

                MessageBox.Show("silinəcək qrupun sıra nömrəsinerri düzgün daxil edin!!!");
            }
            conn.Close();
        }

        
    }
}
