using BillingAndInventoryManagement1.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingAndInventoryManagement1.DAL
{
     class loginDAL
    {
        //static string to connect database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        internal bool isSuccess = false;
        internal string role = null;
        public void loginCheck(loginBLL l)
        {

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //sql query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password";

                //creating sql command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //Checking the rows in DataTable
                if (dt.Rows.Count > 0)
                {
                    //login successfull
                    isSuccess = true;
                    //checking role
                    role = dt.Rows[0][9].ToString();
                }
                else
                {
                    //login failed
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
