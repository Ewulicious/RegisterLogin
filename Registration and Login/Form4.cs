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
    public partial class Form4 : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;database=login;username=root;password=");

        public static string Userid;
        public static string fname;
        public static string lname;
        public static string sex;
        public static string birthday;
        public static string email;


        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            textBox3.Text = Userid;
            textBox1.Text = fname;
            textBox2.Text = lname;
            comboBox1.Text = sex;
            dateTimePicker1.Text = birthday;
            textBox4.Text = email;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("UPDATE users SET fname = @fname, lname = @lname, sex = @sex, birthday = @birthday, email = @email WHERE id = @id", conn);
                command.Parameters.AddWithValue("@id", textBox3.Text);
                command.Parameters.AddWithValue("@fname", textBox1.Text);
                command.Parameters.AddWithValue("@lname", textBox2.Text);
                command.Parameters.AddWithValue("@sex", comboBox1.Text);
                command.Parameters.AddWithValue("@birthday", dateTimePicker1.Text);
                command.Parameters.AddWithValue("@email", textBox4.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!");
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
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
    }
}
