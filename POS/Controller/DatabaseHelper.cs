using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace POS.Controllers
{
    class DatabaseHelper
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        Messages msg;

        public DatabaseHelper()
        {
            msg = Messages.GetInstance();
            conn = new SqlConnection(@"Data Source=Shahgee\Shahgee;Initial Catalog=POS;Integrated Security=True");
        }

        public bool DataManupulationOperation(string query)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg.UserErrorMessage(ex.Message);
            }
            conn.Close();
            return false;
        }

        public DataTable DataManupulationOperationWhichReturnsID(string query)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                DataTable dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                msg.UserErrorMessage(ex.Message);
            }
            conn.Close();
            return null;
        }

        public DataTable DataNavigationOperations(string query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows != null)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                msg.UserErrorMessage(ex.Message);
            }
            return null;
        }
    }
}
