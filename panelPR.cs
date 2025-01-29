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
    public partial class panelPR : Form
    {
        private int currentparentID;

        public panelPR(int parentID )
         

        {
            currentparentID = parentID;

            InitializeComponent();
            string query = "SELECT * FROM sitter";
            FillDataGridView(query);
            LoadParentName();
            LoadSBookingInfo();

        }







        private void LoadSBookingInfo()
        {
            
            string query = @"
        SELECT 
            s.sitterID AS sitterID, 
            s.Name AS Name, 
            s.Address, 
            s.[Phone Number], 
            s.Email
            
        FROM dbo.Bookings b
        INNER JOIN dbo.sitter s ON b.sitterID = s.sitterID
        WHERE b.parentID = @parentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@parentID", currentparentID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                   
                    dataGridViewSitters.DataSource = dataTable;
                }
            }
        }




       


        private void LoadParentName()
        {
            string query = "SELECT Name FROM parents WHERE parentID = @parentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parentID", currentparentID);

                    connection.Open();
                    textBoxName.Text = command.ExecuteScalar().ToString();
                }
            }
        }








        private void FillDataGridView(string query)
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
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
     
    private void panelPR_Load(object sender, EventArgs e)
        {
            LoadSBookingInfo();
           
            string connectionString = "data source=DESKTOP-JN103D7\\SQLEXPRESS; database=BabysitterDB; integrated security=SSPI";
    string query = "SELECT sitterID, Name, Address, [Phone Number] , Email FROM sitter" ;

    using (SqlConnection connection = new SqlConnection(connectionString))
    using (SqlCommand command = new SqlCommand(query, connection))
    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
    {
        DataTable table = new DataTable();
        adapter.Fill(table);

        dataGridView1.DataSource = table;

        DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn
        {
            Name = "Name",
            DataPropertyName = "Name",
            HeaderText = "Name",
            LinkBehavior = LinkBehavior.SystemDefault
        };

        dataGridView1.Columns.Remove("Name");
        dataGridView1.Columns.Add(linkColumn);
               
    } 
             
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutPR about = new AboutPR(currentparentID);
            about.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchValue = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadAllSitters();
                return;
            }

            string query = @"SELECT * FROM sitter
                        WHERE Name LIKE @searchTerm 
                           OR Address LIKE @searchTerm 
                         
                           OR Institution LIKE @searchTerm";


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

        private void LoadAllSitters()
        {
            string query = "SELECT * FROM sitter";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
             
                  if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewLinkColumn)
                  {
       
                        int currentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["sitterID"].Value);

        
                         ShowSitterProfile(currentID);
                  }
              

             
        }

      

        private void ShowSitterProfile(int currentID)
        {
            
            SitterProfile sitterProfile = new SitterProfile(currentID, currentparentID);
            sitterProfile.ShowDialog();
        }


        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult == DialogResult.Yes)
            {

                currentparentID = 0;
                Parents parents = new Parents();
                parents.Show();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewParents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
