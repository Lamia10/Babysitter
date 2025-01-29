using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babysitter
{
    public partial class SPabout : Form
    {
         int currentID;
        int currentparentID;
        string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database= BabysitterDB ; integrated security=SSPI";
        public SPabout(int sitterID, int parentID)
        {
            currentID = sitterID;
            InitializeComponent();
            LoadbsInfo();
            currentparentID = parentID;
        }

        private void LoadbsInfo()
        {





            string query = "SELECT Name, Address, [Phone Number], Birthdate, Email, Institution, Gender FROM sitter WHERE sitterID = @sitterID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sitterID", currentID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            textBox1.Text = reader["Name"].ToString();
                            textBox2.Text = reader["Address"].ToString();
                            textBox3.Text = reader["Phone Number"].ToString();

                            dateTimePicker1.Value = Convert.ToDateTime(reader["Birthdate"]); 
                            dateTimePicker1.Enabled = false; 

                            textBox5.Text = reader["Email"].ToString();
                            richTextBox1.Text = reader["Institution"].ToString();

                            string gender = reader["Gender"].ToString();
                            if (gender == "Male")
                                radioButton2.Checked = true;
                            else if (gender == "Female")
                                radioButton1.Checked = true;
                            else
                                radioButton3.Checked = true;
                        }
                    }
                }
            }




        }
        private void SPabout_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SitterProfile sitterProfile = new SitterProfile (currentID, currentparentID);
            sitterProfile.Show();
        }
    }
}
