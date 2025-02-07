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
    public partial class OverdueLoans : Form
    {
        public OverdueLoans()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GenerateOverdueLoans()
        {
            dtgListOL.DataSource = null;
            dtgListOL.Rows.Clear();
            dt.Clear();

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT * FROM OverdueLoans
                WHERE (ReturnDate BETWEEN @DateFrom AND @DateTo)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DateFrom", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@DateTo", dtpTo.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    dtgListOL.DataSource = dt;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnGENERATE_Click(object sender, EventArgs e)
        {
            GenerateOverdueLoans();
        }
    }
}
