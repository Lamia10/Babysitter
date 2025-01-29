using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babysitter
{
    public partial class AboutBS : Form
    {
        int currentID;
        string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database= BabysitterDB ; integrated security=SSPI";
       
        public AboutBS(int sitterID)
        {
            currentID=sitterID;
            
            InitializeComponent();
            textBox4.PasswordChar = '*';
            LoadDetails();
            
        }


        private void LoadDetails()
        {
            string query = "SELECT  Name, Address, [Phone Number] , Birthdate, Email, Password, Institution, Gender FROM sitter WHERE sitterID = @sitterID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                
                    try {

                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@sitterID", currentID);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {

                            textBox1.Text = reader["Name"].ToString();
                            textBox2.Text = reader["Institution"].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(reader["Birthdate"]);
                            textBox5.Text = reader["Email"].ToString();
                            textBox3.Text = reader["Phone Number"].ToString();
                            textBox4.Text = reader["Password"].ToString();
                            richTextBox1.Text = reader["Address"].ToString();
                            string Gender = reader["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                radioButton2.Checked = true;
                            }
                            else if (Gender == "Female")
                            {
                                radioButton1.Checked = true;
                            }
                            else
                            {
                                radioButton3.Checked = true;
                            }







                        }
                        else
                        {
                            MessageBox.Show("No details found for the given Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close(); 
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show("An Error Occured: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            string newName = textBox1.Text.Trim();
            string newInstitution = textBox2.Text.Trim();
            string newPhoneNumber = textBox3.Text.Trim();
            string newEmail = textBox5.Text.Trim();
            string newPassword = textBox4.Text.Trim();
            string newAddress = richTextBox1.Text.Trim();
            string newBirthdate = dateTimePicker1.Value.ToString();
            string newGender = " ";
             {
                 if (radioButton1.Checked)
                 { newGender = "Female"; }
                 else if (radioButton2.Checked)
                 { newGender = "Male"; }
                 else
                 { newGender = "Others"; }

             }

           

            if (!int.TryParse(newPhoneNumber, out int parsednewPhoneNumber))
            {
                MessageBox.Show("Phone Number must be a valid numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE sitter SET @Name=Name, Birthdate= @Birthdate, Email=@Email, [Phone Number] = @PhoneNumber , Password=@Password, Institution= @Institution, Address= @Address WHERE sitterID = @sitterID";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@sitterID", currentID);
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@Birthdate", newBirthdate);
                    command.Parameters.AddWithValue("@Address", newAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", parsednewPhoneNumber);
                    command.Parameters.AddWithValue("@Gender", newGender);
                    command.Parameters.AddWithValue("@Email", newEmail);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@Institution", newInstitution);



                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("No record was updated. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    

        private void AboutBS_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( "Are you sure you want to delete this profile?","Confirm Deletion", MessageBoxButtons.YesNo,  MessageBoxIcon.Warning
              );

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM sitter WHERE sitterID = @sitterID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sitterID", currentID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        


                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            Sitters sitters = new Sitters();
                            sitters.Show();
                        }
                        else
                        {
                            MessageBox.Show("No profile was found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelBS panelbs = new panelBS(currentID);
            panelbs.Show();
            this.Hide();
            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
