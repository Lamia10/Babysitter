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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Babysitter
{
    public partial class panelBS : Form
    {
        private int currentID;
       
        public panelBS(int sitterID)
       
        {
            
            currentID = sitterID;
            InitializeComponent();
            string query = "SELECT * FROM parents";
            FillDataGridView(query);
            LoadSitterName();
          
           LoadPBookingInfo();
           

        }









      



        private void LoadSitterName()
        {
            string query = "SELECT Name FROM sitter WHERE sitterID = @sitterID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sitterID", currentID);

                    connection.Open();
                    textBoxName.Text = command.ExecuteScalar().ToString();
                }
            }
        }


        private void FillDataGridView(string query = "Select Name, Address, [Phone Number] , Birthdate, Occupation, Email  FROM parents")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
            }

        }
        string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";


      
        private void panelBS_Load(object sender, EventArgs e)
        {
            LoadPBookingInfo();
            LoadAvailabilityStatus();
            
        }



        private void LoadAvailabilityStatus()
        {
            string query = "SELECT availabilityStatus FROM availability WHERE sitterID = (SELECT sitterID FROM sitter WHERE sitterID = @sitterID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sitterID", currentID);
                    connection.Open();

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string availabilityStatus = result.ToString();

                        if (availabilityStatus == "ON")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (availabilityStatus == "OFF")
                        {
                            radioButton2.Checked = true;
                        }
                    }
                    else
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                    }
                }
            }
        }

      

        private void UpdateAvailability()
        {
            string status = "";

            if (radioButton1.Checked)
            {
                status = "ON";
            }
            else if (radioButton2.Checked)
            {
                status = "OFF";
            }

            if (!string.IsNullOrEmpty(status))
            {
                string query = "IF EXISTS (SELECT 1 FROM availability WHERE sitterID = @sitterID) " +
                               "UPDATE availability SET availabilityStatus = @status WHERE sitterID = @sitterID " +
                               "ELSE " +
                               "INSERT INTO availability (sitterID, availabilityStatus) VALUES (@sitterID, @status)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@status", status);
                            command.Parameters.AddWithValue("@sitterID", currentID);
                            connection.Open();

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                label3.Text = $"       Availability";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }







        private void LoadPBookingInfo()
        {
            
            string query = @"
        SELECT 
            p.parentID AS parentID, 
            p.Name AS Name, 
            p.Address, 
            p.[Phone Number], 
            p.Email 
        FROM dbo.Bookings b
        INNER JOIN dbo.parents p ON b.parentID = p.parentID
        WHERE b.sitterID = @sitterID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@sitterID", currentID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    
                    dataGridViewParents.DataSource = dataTable;
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            AboutBS aboutBS = new AboutBS(currentID);
            aboutBS.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchValue = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadAllParents();
                return;
            }

            string query = @"SELECT * FROM parents
                        WHERE Name LIKE @searchTerm 
                           OR Address LIKE @searchTerm 
                           OR Gender LIKE @searchTerm 
                           OR Occupation LIKE @searchTerm";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void LoadAllParents()
        {
            string query = "SELECT * FROM parents";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Upload Profile Picture";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string selectedFilePath = openFileDialog1.FileName;


                pictureBox1.Image = Image.FromFile(selectedFilePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                button1.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
           DialogResult= MessageBox.Show("Are you sure you want to log out?" , "Confirm log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult == DialogResult.Yes)
            {

                currentID = 0;
                Sitters sitters = new Sitters();
                sitters.Show();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewParents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                UpdateAvailability();
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                UpdateAvailability();
            }
        }
    }




}
