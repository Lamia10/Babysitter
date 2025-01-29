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

namespace Babysitter
{
    public partial class BSsignup : Form
    {
        public BSsignup()
        {
            {
                InitializeComponent();
                textBox5.PasswordChar = '*';
                string[] Institution = new string[3];
                Institution[0] = "School";
                Institution[1] = "College";
                Institution[2] = "University";


                comboBox1.DataSource = Institution;
               



            }



        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox5.PasswordChar = '\0';
            }
            else
            {
                textBox5.PasswordChar = '*';
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";

            string Name = textBox1.Text.Trim();
            string Institution = textBox2.Text.Trim();
            string PhoneNumber = textBox3.Text.Trim();
            string Email = textBox4.Text.Trim();
            string Password = textBox5.Text.Trim();
            string Address = richTextBox1.Text.Trim();
            string Birthdate = dateTimePicker1.Value.ToString();
           // string StudentID = button3.Text.Trim(); 
            string Gender = " ";
            {
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
            }

           // string NID = button2.Text.Trim();

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) ||
                          string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Birthdate) ||
                          string.IsNullOrWhiteSpace(Institution) ||/* string.IsNullOrWhiteSpace(NID) || string.IsNullOrWhiteSpace(StudentID) || */
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


            string query = "INSERT INTO sitter (Name, Birthdate, Email , [Phone Number] , Password, Institution,  Gender, Address ) " +
                "VALUES (@Name, @Birthdate, @Email, @PhoneNumber , @Password, @Institution, @Gender, @Address)";

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
                    command.Parameters.AddWithValue("@Institution", Institution);
                    //command.Parameters.AddWithValue("@StudentID", StudentID);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                       Sitters sitters = new Sitters();
                        sitters.Show();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
              

                

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      /* private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Select Scanned NID/Birth Certificate";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string selectedFilePath = openFileDialog1.FileName;


                pictureBox1.Image = Image.FromFile(selectedFilePath);



            }

        } 

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog2.Title = "Select Scanned NID/Birth Certificate";


            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {

                string selectedFilePath = openFileDialog2.FileName;


                pictureBox2.Image = Image.FromFile(selectedFilePath);



            }
        }
      */
        private void button4_Click(object sender, EventArgs e)
        {

            Sitters form3 = new Sitters();
            form3.Show();
            this.Close();

        }

        private void BSsignup_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
