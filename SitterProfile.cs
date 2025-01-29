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
    public partial class SitterProfile : Form
    {
     
         int currentID;
        int currentparentID;
        public SitterProfile(int sitterID, int parentID)
        {
            InitializeComponent();
            currentID = sitterID;
            string query = "SELECT * FROM sitter , parents ";
          
            LoadSitterName();
            currentparentID= parentID;
           

        }



       
        string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";

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




        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";
            string availabilityQuery = "SELECT availabilityStatus FROM dbo.availability WHERE sitterID = @sitterID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdAvailability = new SqlCommand(availabilityQuery, conn))
                {
                    cmdAvailability.Parameters.AddWithValue("@sitterID", currentID);
                    conn.Open();
                    object availabilityStatus = cmdAvailability.ExecuteScalar();
                    conn.Close();

                    if (availabilityStatus == null || availabilityStatus.ToString() == "Unavailable")
                    {
                        MessageBox.Show("You cannot book this sitter as they are not available at this moment.");
                        button3.Enabled = false; 
                        button3.BackColor = Color.Gray;  
                        return;
                    }
                }

                string query = "INSERT INTO dbo.Bookings (sitterID, parentID) VALUES (@sitterID, @parentID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sitterID", currentID);
                    cmd.Parameters.AddWithValue("@parentID", currentparentID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Booking successful!");
                    button3.Text = "Booked";
                    button3.Enabled = false;
                    button3.BackColor = Color.Gray;  
                }
            }

        }


        private void SitterProfile_Load(object sender, EventArgs e)
        {
            LoadSitterAvailability();
            UpdateStatus();
        }



        private void LoadSitterAvailability()
        {
            string query = "SELECT availabilityStatus FROM availability WHERE sitterID = @sitterID";

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
                        if (availabilityStatus == "On")
                        {
                            label3.Text = "Status: Available";
                        }
                        else
                        {
                            label3.Text = "Status: Unavailable";
                        }
                    }
                    else
                    {
                        label3.Text = "  Status: Unavailable  "; 
                    }
                }
            }
        }





        private void UpdateStatus()
        {
            string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";
            string availabilityQuery = "SELECT availabilityStatus FROM dbo.availability WHERE sitterID = @sitterID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdAvailability = new SqlCommand(availabilityQuery, conn))
                {
                    cmdAvailability.Parameters.AddWithValue("@sitterID", currentID);
                    conn.Open();
                    object availabilityStatus = cmdAvailability.ExecuteScalar();
                    conn.Close();

                    if (availabilityStatus == null || availabilityStatus.ToString() == "Unavailable")
                    {
                        label3.Text = "Status: Unavailable";
                    }
                    else
                    {
                        label3.Text = "Status: Available";
                    }
                }
            }
        }




        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SPabout sPabout = new SPabout(currentID, currentparentID);
            sPabout.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panelPR panelPR = new panelPR(currentparentID);
            panelPR.Show();
        }
    }
}


