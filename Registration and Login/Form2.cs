using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Registration_and_Login
{
    public partial class Form2 : Form
    {

        MySqlConnection conn = new MySqlConnection("datasource=localhost;database=login;username=root;password=");
        public Form2()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = Form1.fname + " " + Form1.lname;

            conn.Open();
            string quer = "SELECT id, fname, lname, sex, birthday, email FROM users;";
            MySqlDataAdapter myDa = new MySqlDataAdapter(quer, conn);
            DataTable myDt = new DataTable();
            myDa.Fill(myDt);

            dataGridView1.DataSource = myDt;
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string selectQuery = "SELECT id, fname, lname, sex, birthday, email FROM users WHERE id = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "';";
            MySqlCommand command =  new MySqlCommand(selectQuery, conn);
            MySqlDataReader myReader = command.ExecuteReader();

            if(myReader.Read())
            {
                Form4.Userid = myReader.GetString(0);
                Form4.fname = myReader.GetString(1);
                Form4.lname = myReader.GetString(2);
                Form4.sex = myReader.GetString(3);
                Form4.birthday = myReader.GetString(4);
                Form4.email = myReader.GetString(5);
                this.Hide();
                Form4 form4 = new Form4();
                form4.Show();
            }
            else
            {
                MessageBox.Show("Select row to update");
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {

            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM users WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                    command.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!");
                    this.Hide();
                    Form2 form2 = new Form2();
                    form2.Show();
                    conn.Close();
                    // Refresh DataGridView
                    RefreshDataGridView();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
              
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
            }
        }
        private void RefreshDataGridView()
        {
            conn.Open();
            string query = "SELECT id, fname, lname, sex, birthday, email FROM users;";
            MySqlDataAdapter myDa = new MySqlDataAdapter(query, conn);
            DataTable myDt = new DataTable();
            myDa.Fill(myDt);
            dataGridView1.DataSource = myDt;
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             if (result == DialogResult.Yes)
             {
                 this.Hide();
                 Form1 form1 = new Form1();
                 form1.Show();
             }
        }

       
    }
}
