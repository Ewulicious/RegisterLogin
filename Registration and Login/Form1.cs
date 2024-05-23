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
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;database=login;username=root;password=");
        MySqlCommand command;
        MySqlDataReader mdr;

        public static string name;
        public static string fname;
        public static string lname;
        public static string user;
        public static string pass;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please input  Username, Password");
            }
            else
            {
                Form3 form3 = new Form3();
                connection.Open();
                string selectQuery = "SELECT * FROM Login.users WHERE username = '" + textBox2.Text + "' AND password = '" + form3.HashPassword(textBox3.Text) + "';";
                command = new MySqlCommand(selectQuery, connection);
                mdr = command.ExecuteReader();
               
                if(mdr.Read())
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    Form1.fname = mdr.GetString(1);
                    Form1.lname = mdr.GetString(2);
                    Form2 form2 = new Form2();
                    form2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Incorrect Login Information! Try again. ");
                }

                connection.Close();
            }
            
          
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
