using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MachineDesign.Models
{
    public class DataConnect
    {
        public static string LogMsg = "";
        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();
        //public static SqlConnection sqlconn = new SqlConnection();
        SqlCommand sqlcom = null;
        //SqlDataAdapter sqlada = new SqlDataAdapter();

        Random rd = new Random();

        private static SqlConnection _sqlconn
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb1"].ToString();

                SqlConnection conn = new SqlConnection(connectionString);//在这里新建一个
                if (conn.State == ConnectionState.Closed)//增加一个判断语句
                {
                    conn.Close();
                    conn.Open();
                }
                if (conn == null)
                {
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn;
            }
        }

        public static SqlConnection sqlconn
        {
            get
            {
                return _sqlconn;
            }
        }

        public DataConnect()
        {
        }
        protected bool _initDBConnect()
        {
            return true;
        }

        protected void _endDBConnect()
        {
            sqlconn.Close();
            sqlconn.Dispose();
        }
        //数据库读取
        protected DataTable getStrSqlData(string logMsg, string strSql)
        {
            ds.Clear();
            dtInfo.Clear();

            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, sqlconn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dtInfo = ds.Tables[0];
                }
            }
            catch (Exception ep)
            {
                DataConnect.LogMsg = "读取记录时数据库错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message;
                dtInfo = null;
            }
            finally
            {
                sqlconn.Close();
                sqlconn.Dispose();
            }

            return dtInfo;
        }
        //数据库写入
        public bool putStrSqlData(string logMsg, string strSql)
        {
            sqlcom = sqlconn.CreateCommand();
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSql;

            try
            {
                if (sqlcom.ExecuteNonQuery() > 0)
                    Console.WriteLine("增加记录成功!" + strSql);
                else
                    Console.WriteLine("记录未增加!" + strSql);

                return true;
            }
            catch (Exception ep)
            {
                Console.WriteLine("增加记录时数据库错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message);
                DataConnect.LogMsg = "增加记录时数据库错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message;
            }
            finally
            {
                sqlcom.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            return false;
        }
        //数据库更新
        protected bool updateStrSqlData(string logMsg, string strSql)
        {
            sqlcom = sqlconn.CreateCommand();
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSql;

            try
            {
                if (sqlcom.ExecuteNonQuery() > 0)
                    Console.WriteLine("更新或删除记录成功!" + strSql);
                else
                    Console.WriteLine("记录未更新或删除!" + strSql);

                return true;
            }
            catch (Exception ep)
            {
                Console.WriteLine("更新或删除记录时错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message);
                DataConnect.LogMsg = "增加记录时数据库错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message;
            }
            finally
            {
                sqlcom.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            return false;
        }
    }
}