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

namespace LibraryManagementSystem1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = true; 
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false; 
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn1 = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd1 = new SqlCommand("select Username, Password from Users where username = @username and password = @password", conn1);
                    cmd1.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd1.Parameters.AddWithValue("@Password", txtPassword.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    conn1.Open();
                    da.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("The password you entered is incorrect.\nPlease try again!", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }

            }
        }
    }
}
