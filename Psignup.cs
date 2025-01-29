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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Babysitter
{
    public partial class Psignup : Form
    {
        public Psignup()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox4.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";

            string Name = textBox1.Text.Trim();
            string Occupation = textBox2.Text.Trim();
            string PhoneNumber = textBox3.Text.Trim();
            string Email = textBox5.Text.Trim();
            string Password = textBox4.Text.Trim();
            string Address = richTextBox1.Text.Trim();
            string Birthdate = dateTimePicker1.Value.ToString();
            string Gender =  " " ;
            
                if (radioButton1.Checked)
                { Gender = "Female"; }
                else if (radioButton2.Checked)
                { Gender = "Male"; }
                else if (radioButton3.Checked)
                { Gender = "Others"; }
                else
                {
                    MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            

            //string NID = button2.Text.Trim();

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) ||
                          string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Birthdate) ||
                          string.IsNullOrWhiteSpace(Occupation) || /* string.IsNullOrWhiteSpace(NID) || */
                          string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!int.TryParse(PhoneNumber, out int parsedPhoneNumber))
            {
                MessageBox.Show("Phone Number must be a valid numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkBox1.Checked)
            {
                MessageBox.Show("Please read and accept the Terms and Conditions to sign up.", "T&C Not Accepted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string query = "INSERT INTO parents (Name, Birthdate, Email , [Phone Number] , Password, Occupation, Gender , Address) " +
                "VALUES (@Name, @Birthdate, @Email, @PhoneNumber , @Password, @Occupation, @Gender, @Address)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Birthdate", Birthdate);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@PhoneNumber", parsedPhoneNumber);
                   // command.Parameters.AddWithValue("@NID", NID);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Occupation", Occupation);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                        Parents parents = new Parents();
                        parents.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }



        }








            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                TnC tnc = new TnC();
                tnc.Show();
            }

          /*  private void button2_Click(object sender, EventArgs e)
            {
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog1.Title = "Select Scanned NID";


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    string selectedFilePath = openFileDialog1.FileName;


                    pictureBox1.Image = Image.FromFile(selectedFilePath);



                }
            }
          */
            private void groupBox1_Enter(object sender, EventArgs e)
            {

            }

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void button3_Click(object sender, EventArgs e)
            {
                Parents form2 = new Parents();
                form2.Show();
            this.Close();

        }

        private void Psignup_Load(object sender, EventArgs e)
        {

        }
    }
}
