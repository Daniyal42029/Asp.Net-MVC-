using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class dbhelper
    {
        //private readonly string _connectionString;
        //private readonly string _connectionStringTest;
        public static string _connectionString { get; set; }
        //private SqlConnection con;
        //private SqlCommand cmd;
        //private SqlDataAdapter adp;

        public dbhelper()
        {
            _connectionString = ConfigurationManager.AppSettings["connString"];
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DbActionResult SaveChanges(string Query, CommandType Type, SqlParameter[] parameter)
        {
            var message = 0;
            var dbar = new DbActionResult();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);
                    con.Open();
                    message = cmd.ExecuteNonQuery();
                    con.Close();
                }
                dbar.Action = true;
                dbar.Message = message.ToString();
                return dbar;
            }
            catch (Exception ex)
            {
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }
        public DbActionResult SaveChangesWithoutPara(string Query, CommandType Type)
        {
            var message = 0;
            var dbar = new DbActionResult();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.CommandText = Query;
                    con.Open();
                    message = cmd.ExecuteNonQuery();
                    con.Close();
                }
                dbar.Action = true;
                dbar.Message = message.ToString();
                return dbar;
            }
            catch (Exception ex)
            {
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }

        public DbActionResult SaveChangeswithTransaction(string Query, CommandType Type, SqlParameter[] parameter)
        {
            var dbar = new DbActionResult();
            SqlTransaction trans = null;
            try
            {
                SqlConnection con = GetConnection();
                trans = con.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.Transaction = trans;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                dbar.Action = true;
                trans.Commit();
                return dbar;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }

        public DataTable ExececuteQuery(string sPName, CommandType type)
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.Connection = con;
            cmd.CommandType = type;
            cmd.CommandText = sPName;
            con.Open();
            adp.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable BindGrid(string Query, CommandType Type, SqlParameter[] parameter)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                cmd.CommandType = Type;
                cmd.CommandText = Query;
                cmd.Parameters.AddRange(parameter);
                con.Open();
                adp.Fill(ds);
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        public bool getDataProcedureDatatable(SqlCommand cmd, DataTable dt) //all data from a dataset
        {
            SqlConnection con = GetConnection();
            cmd.Connection = con;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            adp.Fill(dt);
            con.Close();
            return true;
        }

        public DataSet ExecuteDataset(string Query, CommandType Type, SqlParameter[] parameter)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;

            try
            {
                SqlConnection con = GetConnection();
                cmd = new SqlCommand();
                adp = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                cmd.CommandType = Type;
                cmd.CommandText = Query;
                cmd.Parameters.AddRange(parameter);
                con.Open();
                adp.Fill(ds);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                adp.Dispose();
                cmd.Dispose();
            }
            return ds;
        }

        public DbActionResult GetColumn(string Query, CommandType Type, SqlParameter[] parameter)
        {
            var dbar = new DbActionResult();
            try
            {
                SqlConnection con = GetConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);
                    con.Open();
                    dbar.Value = cmd.ExecuteScalar();
                    con.Close();
                }
                dbar.Action = true;
            }
            catch (Exception ex)
            {
                dbar.ErrorMessage = ex.Message;
            }
            return dbar;
        }

        public int Duplicate(string Query, CommandType Type, SqlParameter[] parameter)
        {
            int count = 0;
            try
            {
                SqlConnection con = GetConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);
                    con.Open();
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }

        public DbActionResult SaveReturnValue(string Query, CommandType Type, SqlParameter[] parameter)
        {
            var dbar = new DbActionResult();
            SqlTransaction trans = null;
            try
            {
                SqlConnection con = GetConnection();
                con.Open();
                trans = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.Transaction = trans;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);

                    dbar.Value = cmd.ExecuteScalar();
                }
                dbar.Action = true;
                trans.Commit();
                con.Close();
                dbar.Message = "Record Saved Successfully !";
                return dbar;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }

        public DbActionResult SaveReturnValue(string Query, CommandType Type)
        {
            var dbar = new DbActionResult();
            SqlTransaction trans = null;
            try
            {
                SqlConnection con = GetConnection();
                con.Open();
                trans = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.Transaction = trans;
                    cmd.CommandText = Query;
                    dbar.Value = cmd.ExecuteScalar();
                }
                dbar.Action = true;
                trans.Commit();
                con.Close();
                dbar.Message = "Record Saved Successfully !";
                return dbar;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }

        public void UpdateChanges(string Query, CommandType Type, SqlParameter[] parameter)
        {
            try
            {
                SqlConnection con = GetConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = Type;
                    cmd.CommandText = Query;
                    cmd.Parameters.AddRange(parameter);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ExecQueryReturnTable(string Query, CommandType type)
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Connection = con;
            cmd.CommandType = type;
            cmd.CommandText = Query;
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable ExecQueryReturnTable(string Query, CommandType type, SqlConnection con = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Connection = con;
            cmd.CommandType = type;
            cmd.CommandText = Query;
            //con.Open();
            da.Fill(dt);
            //con.Close();
            return dt;
        }

        public static DbActionResult SaveChangesTransaction(string query, CommandType type, SqlParameter[] parameter, SqlConnection con = null, SqlTransaction trans = null)
        {
            var dbar = new DbActionResult();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = type;
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(parameter);
                    dbar.Value = cmd.ExecuteScalar();
                }

                dbar.Action = true;
                dbar.Message = "Record Saved Successfully !";

                return dbar;
            }
            catch (Exception ex)
            {
                dbar.Action = false;
                dbar.ErrorMessage = ex.Message;
                return dbar;
            }
        }
    }
}