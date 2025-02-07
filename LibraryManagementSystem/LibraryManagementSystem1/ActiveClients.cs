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
    public partial class ActiveClients : Form
    {
        public ActiveClients()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GenerateActiveClients()
        {
            dtgListAC.DataSource = null;  
            dtgListAC.Rows.Clear();        
            dt.Clear();                    

            using (SqlConnection conn = ConnectDB.GetConnection())  
            {
                try
                {
                    conn.Open();  

                    
                    SqlCommand cmd = new SqlCommand(@"
                SELECT ClientID, FirstName, LastName, DateOfBirth, Email, Phone, Address, RegistrationDate, MembershipActive
                FROM ActiveClients
                WHERE RegistrationDate BETWEEN @StartDate AND @EndDate
                AND MembershipActive = 1", conn);

                    
                    cmd.Parameters.AddWithValue("@StartDate", dtpSA.Value.ToString("yyyy-MM-dd"));  
                    cmd.Parameters.AddWithValue("@EndDate", dtpEA.Value.ToString("yyyy-MM-dd"));    

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);  

                    
                    if (dt.Rows.Count > 0)
                    {
                        dtgListAC.DataSource = dt;  
                    }
                    else
                    {
                        MessageBox.Show("No active clients found for the selected date range.");
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void btnGENERATE_Click(object sender, EventArgs e)
        {
            GenerateActiveClients();
        }
    }
}
