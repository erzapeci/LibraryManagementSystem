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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GetUsers()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Users", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    dt.Clear();
                    da.Fill(dt);
                    dtgListU.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void InsertNewUsers()
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please fill in the Username field", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please fill in the Password field", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbRole.SelectedIndex == -1) 
            {
                MessageBox.Show("Please select a Role", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                try
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO Users (Username, Password, Role) 
                                   VALUES (@Username, @Password, @Role)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text); 
                        cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("New User inserted successfully", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GetUsers();
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("No rows were inserted. Please check your data.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ClearFields()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
        }

        private void UpdateUsers()
        {
            if (txtUID.Text == "")
            {
                MessageBox.Show("Please fill in the User ID field", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Please fill in the Username field", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please fill in the Password field", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbRole.SelectedIndex == -1) 
            {
                MessageBox.Show("Please select a Role", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE Users  
                SET Username = @Username, 
                    Password = @Password,  
                    Role = @Role  
                WHERE UserID = @UserID", connection);

                    cmd.Parameters.AddWithValue("@UserID", txtUID.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text); 
                    cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString()); 

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetUsers();  
                        ClearFields();
                        btnADD.Enabled = true;
                        btnEdit.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Please check the User ID.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            InsertNewUsers();
        }

        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateUsers();
        }

        private void dtgListU_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnADD.Enabled = false;
            btnEdit.Enabled = true;

            txtUID.Text = dtgListU.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUsername.Text = dtgListU.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPassword.Text = dtgListU.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbRole.Text = dtgListU.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dataView = new DataView(dt);
                dataView.RowFilter = $"Username like '%{filterText}%'";
                dtgListU.DataSource = dataView;
            }
            else
            {
                dtgListU.DataSource = dt;
            }
        }
    }
}
