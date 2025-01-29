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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Babysitter
{
    public partial class Parents : Form
    {
        public Parents()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Psignup form2 = new Psignup();
            form2.Show();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

                string Name = textBox1.Text.Trim();
                string Password = textBox2.Text.Trim();

                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.Show("Please enter both Name and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";
                string query = "SELECT parentID FROM parents WHERE Name = @Name AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Password", Password);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            int parentID = Convert.ToInt32(result);
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            panelPR panelPR = new panelPR(parentID);
                            panelPR.Show();
                           this.Hide();
                    }
                        else
                        {
                            MessageBox.Show("Invalid Name or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            







        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
                if (checkBox1.Checked)
                {
                    textBox2.PasswordChar = '\0';
                }
                else
                {
                    textBox2.PasswordChar = '*';
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Startpage startpage= new Startpage();
            startpage.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
