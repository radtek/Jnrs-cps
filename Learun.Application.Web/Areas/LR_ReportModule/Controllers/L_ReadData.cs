using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using System.Data;
using System.Data.SqlClient;
using SiteJnrs.Models;
using Newtonsoft.Json.Linq;
using Learun.DataBase.Repository;
using MongoDB.Bson;
using Learun.Util;
using Newtonsoft.Json;

namespace SiteXBFb.Areas.Visual.Models
{
    public class L_ReadData : DataLConnect
    {
        ILog log = LogManager.GetLogger("L_ReadData");

        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();

        DateTime dtime = new DateTime();
        DateTime wtime = new DateTime();

        public bool initDBConnect()
        {
            return _initDBLConnect();
        }

        public void endDBConnect()
        {
            _endDBLConnect();
        }
        /*读取班次信息*/
        public int readShiftData(string gis_numv, string mis_numv, string mac_prog, string shift_no)
        {
            int num = 0;
            int machine_id = 0;

            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(ReadParameter.WShiftOffsetHour);
            string strSql1 = "select machine_id from tb_machine_info where mis_visual='" + gis_numv + "' and is_program='"+ mac_prog + "'";

            DataTConnect dt = new DataTConnect();

            dtInfo = dt.getStrSqlTData("readShiftInfo", strSql1).Copy();

            if (dtInfo == null || dtInfo.Rows.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
            }


            string strSql = "select plan_day from tb_plan_day_data where machine_id='"+machine_id+ "' and calc_date='" + wtime.ToString("yyyyMMdd")+"'";

            dtInfo = getStrSqlLData("readShiftInfo", strSql).Copy();

            if (dtInfo == null || dtInfo.Rows.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                num = Convert.ToInt32(dtInfo.Rows[i]["plan_day"]);
            }
            return num;
        }
    }
}