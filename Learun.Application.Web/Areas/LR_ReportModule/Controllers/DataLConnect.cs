using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace SiteJnrs.Models
{
    public class DataLConnect
    {
        log4net.ILog log = log4net.LogManager.GetLogger("DataLConnect");

        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();
        SqlCommand sqlcom = null;

        Random rd = new Random();
        
        private static SqlConnection _sqltconn
        {
            get
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ToString();

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
                catch (Exception ex)
                {
                    //
                    return new SqlConnection();
                }
            }
        }

        public static SqlConnection sqltconn
        {
            get
            {
                return _sqltconn;
            }
        }

        public DataLConnect()
        {
        }
        
        protected bool _initDBLConnect()
        {
            if (sqltconn.State == ConnectionState.Open)
                return true;
            else return false;
        }

        protected void _endDBLConnect()
        {
            if (sqltconn != null)
            {
                sqltconn.Close();
                sqltconn.Dispose();
            }

            log.Info("数据库结束动作!");
        }
        //数据库读取
        protected DataTable getStrSqlLData(string logMsg, string strSql)
        {
            ds.Clear();
            dtInfo.Clear();

            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, sqltconn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dtInfo = ds.Tables[0];
                }
            }
            catch (Exception ep)
            {
                log.Error("数据库读取错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message);
                dtInfo = null;
            }

            log.Info(string.Format("logMsg:{0} strSql:{1}", logMsg, strSql));
            return dtInfo;
        }
        //数据库写入
        protected bool putStrSqlLData(string logMsg, string strSql)
        {
            sqlcom = sqltconn.CreateCommand();
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSql;

            try
            {
                if (sqlcom.ExecuteNonQuery() > 0)
                    log.Info("增加记录成功!" + strSql);
                else
                    log.Error("记录未增加!" + strSql);

                return true;
            }
            catch (Exception ep)
            {
                log.Error("增加记录时数据库错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message);
            }
            finally
            {
                sqlcom.Dispose();
            }
            return false;
        }
        //数据库更新
        protected bool updateStrSqlLData(string logMsg, string strSql)
        {
            sqlcom = sqltconn.CreateCommand();
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSql;

            try
            {
                if (sqlcom.ExecuteNonQuery() > 0)
                    log.Info("更新或删除记录成功!" + strSql);
                else
                    log.Error("记录未更新或删除!" + strSql);

                return true;
            }
            catch (Exception ep)
            {
                log.Error("更新或删除记录时错误!\n" + logMsg + ":" + strSql + "\n" + ep.Message);
            }
            finally
            {
                sqlcom.Dispose();
            }
            return false;
        }
    }
}