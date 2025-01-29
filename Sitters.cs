using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Babysitter
{
    public partial class Sitters : Form
    {
        public Sitters()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BSsignup form2 = new BSsignup();
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
            string query = "SELECT sitterID FROM sitter WHERE Name = @Name AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

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
                        int sitterID = Convert.ToInt32(result);
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        panelBS panelBS = new panelBS(sitterID);
                        panelBS.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Startpage form1 = new Startpage();
            form1.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Sitters_Load(object sender, EventArgs e)
        {

        }
    }
}