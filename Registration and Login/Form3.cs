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
    public partial class Form3 : Form
    {

        MySqlConnection connection = new MySqlConnection("datasource=localhost;database=login;username=root;password=");
        
        

        public Form3()
        {
            InitializeComponent();
        }
        public string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hashedpassword = HashPassword(textBox6.Text);

            if (!this.textBox4.Text.Contains('@') || !this.textBox4.Text.Contains('.'))
            {
                MessageBox.Show("Please Enter a Valid Email" ,"Invalid Email Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(textBox6.Text != textBox7.Text)
            {
                MessageBox.Show("Password doesn't match!", "Error");
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please fill out the information!");
            }
            else
            {
                connection.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM login.users WHERE username = @username", connection),
                cmd2 = new MySqlCommand("SELECT * FROM login.users WHERE email = @email", connection);

                cmd1.Parameters.AddWithValue("@username", textBox5.Text);
                cmd2.Parameters.AddWithValue("@email", textBox4.Text);


                bool userExists = false, mailExists = false;

                using (var dr1 = cmd1.ExecuteReader())
                    if (userExists = dr1.HasRows) MessageBox.Show("Username not available!");
                using (var dr2 = cmd2.ExecuteReader())
                    if (mailExists = dr2.HasRows) MessageBox.Show("Email not available!");

                if (!(userExists || mailExists))
                {
                    string iquery = "INSERT INTO login.users (`fname`, `lname`, `sex`,`birthday`,`email`,`username`,`password`) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "', '" + comboBox1.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox4.Text + "', '" + textBox5.Text + "', @password)";
                    MySqlCommand commandDatabase = new MySqlCommand(iquery, connection);
                    commandDatabase.Parameters.AddWithValue("@password", hashedpassword);
                    commandDatabase.CommandTimeout = 60;
            
           
                    try
                    {
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                        MessageBox.Show("Register Successful!");
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Incorrect Registration Information! Try again. ");
                    }
                }
               
               
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}

