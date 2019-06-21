using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MachineDesign.Models;

namespace SiteJnrs.Models
{
    public class ShiftInfo : DataConnect
    {
        public static int ShiftId;
        public static string ShiftNo;
        public static string ShiftName;
        public static string ShiftDate;
        public static DateTime ShiftToDay = DateTime.Today;
        public static DateTime StartTime;
        public static DateTime EndTime;

        DataTable dtInfo = new DataTable();
        static ReadData rd = new ReadData();

        public ShiftInfo()
        {
            ShiftId = 0;
            ShiftNo = "";
            ShiftName = "";
            ShiftDate = DateTime.Today.ToString("yyyyMMdd");
            ShiftToDay = DateTime.Today;
            StartTime = DateTime.Today;
            EndTime = DateTime.Today.AddDays(1);
        }

        public static bool initShiftInfo()
        {
            if (rd.readShiftInfo())
            {
                return true;
            }
            return false;
        }
        public bool getShiftInfo()
        {
            string logMsg = "getShiftInfo";
            String strSql = "select wshift_id,wshift_no,wshift_name,wshift_date,wshift_type,start_dtime,end_dtime "
                            + " from tb_shift_date where start_dtime <= getdate() and end_dtime > getdate() "
                            + "  and wshift_date >= " + DateTime.Today.AddDays(-1).ToString("yyyyMMdd");

            try
            {
                dtInfo = getStrSqlData(logMsg, strSql);

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ShiftInfo.ShiftId = Convert.ToInt32(dtInfo.Rows[i]["wshift_id"].ToString());
                    ShiftInfo.ShiftNo = dtInfo.Rows[i]["wshift_no"].ToString();
                    ShiftInfo.ShiftName = dtInfo.Rows[i]["wshift_name"].ToString();
                    ShiftInfo.ShiftDate = dtInfo.Rows[i]["wshift_date"].ToString();
                    ShiftInfo.StartTime = Convert.ToDateTime(dtInfo.Rows[i]["start_dtime"]);
                    ShiftInfo.EndTime = Convert.ToDateTime(dtInfo.Rows[i]["end_dtime"]);
                }
            }
            catch (Exception ep)
            {
                Console.WriteLine("班次日期读取失败！" + ep.Message);
                return false;
            }
            return true;
        }
    }
}