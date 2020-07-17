using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Class
{
    public class clsDB
    {

        private MySqlConnection objConn;
        private MySqlCommand objCmd;
        private MySqlTransaction Trans;
        private String strConnString;

        public clsDB()
        {
            //strConnString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
            strConnString = "Server = localhost; User Id = root; Password = nopassword; Database = vms; charset=utf8";
        }

        public MySqlDataReader ExecuteDataReader(String strSQL)
        {
            MySqlDataReader dtReader;
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            objCmd = new MySqlCommand(strSQL, objConn);
            dtReader = objCmd.ExecuteReader();
            return dtReader; //*** Return DataReader ***//
        }

        public DataSet ExecuteDataSet(String strSQL)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter dtAdapter = new MySqlDataAdapter();
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            objCmd = new MySqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = strSQL;
            objCmd.CommandType = CommandType.Text;

            dtAdapter.SelectCommand = objCmd;
            dtAdapter.Fill(ds);
            return ds;   //*** Return DataSet ***//
        }

        public DataTable ExecuteDataTable(String strSQL)
        {
            MySqlDataAdapter dtAdapter;
            DataTable dt = new DataTable();
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            dtAdapter = new MySqlDataAdapter(strSQL, objConn);
            dtAdapter.Fill(dt);
            return dt; //*** Return DataTable ***//
        }

        public int ExecuteNonQuery(String strSQL)
        {
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            try
            {
                objCmd = new MySqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strSQL;

                int ret;
                ret = objCmd.ExecuteNonQuery();
                return ret; //*** Return True ***//
            }
            catch (Exception)
            {
                return 0; //*** Return False ***//
            }
        }


        public Object ExecuteScalar(String strSQL)
        {
            Object obj;
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            try
            {
                objCmd = new MySqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strSQL;

                obj = objCmd.ExecuteScalar();  //*** Return Scalar ***//
                return obj;
            }
            catch (Exception)
            {
                return null; //*** Return Nothing ***//
            }
        }

        public void TransStart()
        {
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();
            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
        }


        public void TransExecute(String strSQL)
        {
            objCmd = new MySqlCommand();
            objCmd.Connection = objConn;
            objCmd.Transaction = Trans;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;
            objCmd.ExecuteNonQuery();
        }


        public void TransRollBack()
        {
            Trans.Rollback();
        }

        public void TransCommit()
        {
            Trans.Commit();
        }

        public void Close()
        {
            objConn.Close();
            objConn = null;
        }
    }
}

