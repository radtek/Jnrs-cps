using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data.OleDb;
using SiteJnrs.Models;
using Learun.Application.TwoDevelopment.LR_RunParam;
using Learun.Util;
using MongoDB.Bson;
using Learun.DataBase.Repository;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MachineDesign.Models
{
    public class ReadData : DataConnect
    {
        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();
        DateTime dtime = new DateTime();

        public bool initDBConnect()
        {
            return _initDBConnect();
        }

        public void endDBConnect()
        {
            _endDBConnect();
        }
        public List<string> GetTimeList(DateTime time1, DateTime time2)
        {
            List<string> timeList = new List<string>();

            while (time1 <= time2)
            {
                timeList.Add(time1.ToString("yyyy-MM-dd"));
                time1 = time1.AddDays(1);
            }
            return timeList;
        }
        public List<ProductSchedual> readProductSchedual(string logMsg, int machine_id, DateTime start_date, DateTime end_date)
        {
            DateTime dtime = DateTime.Now;

            ProductSchedual ps = new ProductSchedual();
            List<ProductSchedual> data = new List<ProductSchedual>();
            string strSql = "";

            int dayNum = Convert.ToInt32(end_date.ToString("yyyyMMdd")) - Convert.ToInt32(start_date.ToString("yyyyMMdd")) + 1;

 
            List<string> sjList = new List<string>();
            /*strSql = "SELECT " +
            " calc_date," +
            " run_state," +
            " case when run_state = 0 then '停机' when run_state = 1 then '运行' when run_state = 2 then '报警' when run_state = 3 then '空闲'  when run_state = 4 then '暂停'  end as name" +
            " ,convert(varchar(50),sum([run_dur])/60)  as value" +
            " FROM tb_run_state_seque" +
            " where calc_date<=" + end_date.ToString("yyyyMMdd") + " and calc_date>=" + start_date.ToString("yyyyMMdd") + "  and machine_id = " + machine_id +
            " group by run_state,calc_date order by calc_date, run_state asc";*/

            strSql  = ""+
            " select a.*,  isnull(b.value, 0) as value    from("
            + " SELECT distinct  1 as run_state, '运行' as name, calc_date from tb_run_state_seque where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + ""
            + " union"
            + " SELECT distinct 2 as run_state, '报警' as name, calc_date from tb_run_state_seque where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + ""
            + " union"
            + " SELECT distinct 3 as run_state, '暂停' as name, calc_date from tb_run_state_seque where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + ""
            + " union"
            + " SELECT distinct 4 as run_state, '空闲' as name, calc_date from tb_run_state_seque where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + ""
            + " union"
            + " SELECT distinct 0 as run_state, '停机' as name, calc_date from tb_run_state_seque where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + ") a"
            + " left join("
            + " SELECT  top 100 PERCENT  run_state, case  when run_state = 1 then '运行' when run_state = 2 then '报警'  when run_state = 3 then '暂停' when run_state = 4 then '空闲'  when run_state = 0 then '停机' end as name,"
            + " convert(varchar(50), sum([run_dur]) / 60) as value,calc_date FROM tb_run_state_seque"
            + " where calc_date <= " + end_date.ToString("yyyyMMdd") + " AND calc_date >= " + start_date.ToString("yyyyMMdd") + " and machine_id =  " + machine_id +" and run_state in  (0, 1, 2, 3, 4)"
            + " group by run_state ,calc_date"
            + " order by calc_date, case run_state when '0' then 5 when '1' then 1 when '2' then 2 when '3' then 3 else 4 end"
            + " ) b on a.name = b.name and a.calc_date = b.calc_date"
            + " order by a.calc_date, case a.run_state when '0' then 5 when '1' then 1 when '2' then 2 when '3' then 3 else 4 end";

            try
            {
                dtInfo = getStrSqlData("", strSql);

                Trace.WriteLine(dtInfo.Rows.Count);

            }
            catch (Exception ex)
            {
                LogMsg = logMsg + ":" + ex.Message;
                Trace.WriteLine(LogMsg);
            }

            if (dtInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    sjList.Add(dtInfo.Rows[i]["calc_date"].ToString());
                }
               DataRow[] drRow = dtInfo.Select("run_state=1", "calc_date asc");

                int[] number = new int[dayNum];
                int[] number1 = new int[dayNum];
                int[] number2 = new int[dayNum];
                int[] number3 = new int[dayNum];
                int[] number4 = new int[dayNum];
               
                for (int i = 0; i < drRow.Length; i++)
                {
                  
                    number[i] = Convert.ToInt32(drRow[i]["value"]);                  //运行
                    
                }
                ps.data1 = number;

                DataRow[] drRow1 = dtInfo.Select("run_state=2", "calc_date asc");

                for (int i = 0; i < drRow1.Length; i++)
                {

                    number1[i] = Convert.ToInt32(drRow1[i]["value"]);                 //报警

                }
                ps.data2 = number1;

                DataRow[] drRow2 = dtInfo.Select("run_state=3", "calc_date asc");

                for (int i = 0; i < drRow2.Length; i++)
                {

                    number2[i] = Convert.ToInt32(drRow2[i]["value"]);                 //暂停

                }
                ps.data3 = number2;

                DataRow[] drRow3 = dtInfo.Select("run_state=4", "calc_date asc");

                for (int i = 0; i < drRow3.Length; i++)
                {

                    number3[i] = Convert.ToInt32(drRow3[i]["value"]);                 //空闲

                }
                ps.data4 = number3;

                DataRow[] drRow4 = dtInfo.Select("run_state=0", "calc_date asc");

                for (int i = 0; i < drRow4.Length; i++)
                {

                    number4[i] = Convert.ToInt32(drRow4[i]["value"]);                 //停机

                }
                ps.data5 = number4;
                ps.str = sjList.Distinct().ToArray();
                data.Add(ps);
            }
            return data;
        }
        public List<ProductSchedual> readOEESchedual(string logMsg, int machine_id, DateTime start_date, DateTime end_date)
        {
            DateTime dtime = DateTime.Now;

            ProductSchedual ps = new ProductSchedual();
            List<ProductSchedual> data = new List<ProductSchedual>();
            string strSql = "";

            int dayNum = Convert.ToInt32(end_date.ToString("yyyyMMdd")) - Convert.ToInt32(start_date.ToString("yyyyMMdd")) + 1;

            int[] number = new int[dayNum];
            int[] number1 = new int[dayNum];
            int[] number2 = new int[dayNum];
            int[] number3 = new int[dayNum];
            int[] number4 = new int[dayNum];

            List<string> sjList = new List<string>();


            DataTable dtInfo1 = new DataTable();
            DataTable dtInfo2 = new DataTable();

            strSql = " select calc_date, Convert(decimal(18,2),sum([run_dur])*100/86400) as value, Convert(decimal(18,2),sum([run_dur])) as dur "
                     + " from tb_run_state_seque"
                     + " where calc_date <= '" + end_date.ToString("yyyyMMdd") + "' AND calc_date >=  '" + start_date.ToString("yyyyMMdd") 
                     + "' and machine_id = " + machine_id + " and run_state != 0"
                     + " group by calc_date order by calc_date";

            try
            {

                dtInfo1 = getStrSqlData("", strSql).Copy();
            }
            catch (Exception ex)
            {
                LogMsg = logMsg + ":" + ex.Message;
            }

            if (dtInfo1.Rows.Count > 0)
            {
                for (int i = 0; i < dtInfo1.Rows.Count; i++)
                {
                    sjList.Add(dtInfo1.Rows[i]["calc_date"].ToString());
                    number[i] = Convert.ToInt32(dtInfo1.Rows[i]["value"]);                  //时间开动率
                }
                ps.data1 = number;
                ps.str = sjList.Distinct().ToArray();
            }
            strSql = "select sp.wshift_date as name,sum(sp.pass_prod_num)/sum(sp.prod_num)*100 as value,"
                     + " mi.machine_name as 设备号,mi.group_name as 设备组, sum(sp.prod_num) as prod_num,sum(sp.pass_prod_num) as 不合格产量"
                     + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                     + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                     + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + ""
                     + " and sp.wshift_date <= '" + end_date.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + start_date.ToString("yyyyMMdd") + "'"
                     + " group by sp.wshift_date,mi.machine_name,mi.group_name order by wshift_date asc";

            try
            {
                dtInfo2 = getStrSqlData("", strSql).Copy();
            }
            catch (Exception ex)
            {
                LogMsg = logMsg + ":" + ex.Message;
            }

            if (dtInfo2.Rows.Count > 0)
            {
                for (int i = 0; i < dtInfo2.Rows.Count; i++)
                {
                    number1[i] = Convert.ToInt32(dtInfo2.Rows[i]["value"]);                  //合格品率
                    int prod_num = Convert.ToInt32(dtInfo2.Rows[i]["prod_num"]);
                    string calc_date = dtInfo2.Rows[i]["name"].ToString();//日期
                    for (int k = 0; k < dtInfo1.Rows.Count; k++)
                        {
                        if (calc_date == dtInfo1.Rows[k]["calc_date"].ToString())
                            {
                                number2[k] = (int)(Math.Round((double)prod_num *10/ Convert.ToInt32(dtInfo1.Rows[k]["dur"]),2)*100);  //性能开动率
                        }
                        }
                    }
                ps.data2 = number1;
                ps.data3 = number2;
            }

            for (int t = 0; t < number.Length; t++)
            {
                number3[t] = number[t]* number1[t]* number2[t]/10000;  //设备综合效率
            }
            ps.data4 = number3;
            data.Add(ps);
            return data;
        }
        public List<ProductSchedual> readOEESchedual(string logMsg, int machine_id, DateTime start_date, DateTime end_date,string dayorshift)
        {
            DateTime dtime = DateTime.Now;

            ProductSchedual ps = new ProductSchedual();
            List<ProductSchedual> data = new List<ProductSchedual>();
            string strSql = "";
            string sql = null;

            int dayNum = Convert.ToInt32(end_date.ToString("yyyyMMdd")) - Convert.ToInt32(start_date.ToString("yyyyMMdd")) + 1;

            int[] number = new int[dayNum];
            int[] number1 = new int[dayNum];
            int[] number2 = new int[dayNum];
            int[] number3 = new int[dayNum];
            int[] number4 = new int[dayNum];

            List<string> sjList = new List<string>();


            DataTable dtInfo1 = new DataTable();
            DataTable dtInfo2 = new DataTable();

            if (dayorshift == "Day")
            {
                strSql = " select wshift_date as calc_date, time_work_rate, perfrm_work_rate,pass_prod_rate,oee_calculate "
                     + " from tb_oee_calculate"
                     + " where wshift_date <= '" + end_date.ToString("yyyyMMdd") + "' AND wshift_date >=  '" + start_date.ToString("yyyyMMdd")
                     + "' and machine_id = " + machine_id + " and calc_stage='" + dayorshift + "'"
                     + "  order by wshift_date";

                sql = "  select tcp.group_id ,tcp.prod_num,tcp.wshift_date,trs.time_second as run_time_second,"
                      + " (case when tqi.unqualified_no is null then 0 else tqi.unqualified_no end) as unqualified_no"
                      + " ,(case when tqi.unqualified_no is null then tcp.prod_num else (tcp.prod_num - tqi.unqualified_no) end )as qualified_no"
                      + " from tb_curday_prod_num tcp"
                      + " left join (select sum(trs.run_dur) as time_second ,trs.machine_id,trs.wshift_date"
                      + " from tb_run_state_seque trs"
                      + " where trs.run_state = '1'"
                      + " group by trs.machine_id,trs.wshift_date"
                      + " )trs on tcp.machine_id = trs.machine_id and tcp.wshift_date = trs.wshift_date"
                      + " left join("
                      + " select tqi.group_no, tqi.unqualified_no, convert(varchar(8) ,tqi.product_time, 112) as wshift_date"
                      + " from tb_quantity_info tqi)tqi on tcp.wshift_date = tqi.wshift_date"
                      + " where is_stage = 'Day'"
                      + " and tcp.wshift_date <= '" + end_date.ToString("yyyyMMdd") + "' and tcp.wshift_date >= '" + start_date.ToString("yyyyMMdd") + "'"
                      + " and tcp.machine_id = " + machine_id + ""
                      + " order by tcp.wshift_date";
            }
            else
            {
                strSql = " select wshift_date+b.wshift_name as calc_date, time_work_rate, perfrm_work_rate,pass_prod_rate,oee_calculate "
                     + " from tb_oee_calculate a left join tb_shift_info b on a.wshift_id=b.wshift_id "
                     + " where a.wshift_date <= '" + end_date.ToString("yyyyMMdd") + "' AND a.wshift_date >=  '" + start_date.ToString("yyyyMMdd")
                     + "' and a.machine_id = " + machine_id + " and a.calc_stage='" + dayorshift + "'"
                     + "  order by a.wshift_date";

                sql = "  select tcp.group_id ,tcp.prod_num,tcp.wshift_date+tsi.wshift_name as wshift_date,trs.time_second as run_time_second,"
                      + " (case when tqi.unqualified_no is null then 0 else tqi.unqualified_no end) as unqualified_no"
                      + " ,(case when tqi.unqualified_no is null then tcp.prod_num else (tcp.prod_num - tqi.unqualified_no) end )as qualified_no"
                      + " from tb_curday_prod_num tcp"
                      + " left join (select sum(trs.run_dur) as time_second ,trs.machine_id,trs.wshift_date"
                      + " from tb_run_state_seque trs"
                      + " where trs.run_state = '1'"
                      + " group by trs.machine_id,trs.wshift_date"
                      + " )trs on tcp.machine_id = trs.machine_id and tcp.wshift_date = trs.wshift_date"
                      + " left join("
                      + " select tqi.group_no, tqi.unqualified_no, convert(varchar(8) ,tqi.product_time, 112) as wshift_date"
                      + " from tb_quantity_info tqi)tqi on tcp.wshift_date = tqi.wshift_date"
                      + " inner join tb_shift_info tsi on tcp.wshift_id=tsi.wshift_id"
                      + " where is_stage = 'Shift'"
                      + " and tcp.wshift_date <= '" + end_date.ToString("yyyyMMdd") + "' and tcp.wshift_date >= '" + start_date.ToString("yyyyMMdd") + "'"
                      + " and tcp.machine_id = " + machine_id + ""
                      + " order by tcp.wshift_date";
            }
            try
            {

                dtInfo1 = getStrSqlData("", strSql).Copy();
                dtInfo2 = getStrSqlData("", sql).Copy();
            }
            catch (Exception ex)
            {
                LogMsg = logMsg + ":" + ex.Message;
            }
            string OEE = null;
            if (dtInfo1.Rows.Count > 0)
            {
                for (int i = 0; i < dtInfo1.Rows.Count; i++)
                {
                    sjList.Add(dtInfo1.Rows[i]["calc_date"].ToString());
                    number[i] = Convert.ToInt32(Convert.ToDouble(dtInfo1.Rows[i]["time_work_rate"].ToString().IsEmpty()?0:dtInfo1.Rows[i]["time_work_rate"])*100);                  //时间开动率
                    number1[i] = Convert.ToInt32(Convert.ToDouble(dtInfo1.Rows[i]["pass_prod_rate"].ToString().IsEmpty() ? 0:dtInfo1.Rows[i]["pass_prod_rate"]) * 100);                  //合格品率
                    number2[i] = Convert.ToInt32(Convert.ToDouble(dtInfo1.Rows[i]["perfrm_work_rate"].ToString().IsEmpty() ? 0:dtInfo1.Rows[i]["perfrm_work_rate"]) * 100);                  //性能开动率
                    //Convert.ToInt32(Convert.ToDouble(dtInfo1.Rows[i]["oee_calculate"].ToString().IsEmpty() ? 0:dtInfo1.Rows[i]["oee_calculate"]) * 100);                  //设备综合效率
                    for(int j = 0; j < dtInfo2.Rows.Count; j++)
                    {
                        if (dtInfo1.Rows[i]["calc_date"].ToString().Equals(dtInfo2.Rows[j]["wshift_date"]))
                        {
                            var qualified_no = Convert.ToDouble(dtInfo2.Rows[j]["qualified_no"].ToString().IsEmpty() ? "0" : dtInfo2.Rows[j]["qualified_no"].ToString());
                            var prod_num = Convert.ToDouble(dtInfo2.Rows[j]["prod_num"].ToString().IsEmpty() ? "0" : dtInfo2.Rows[j]["prod_num"].ToString());
                            var run_time_second = Convert.ToDouble(dtInfo2.Rows[j]["run_time_second"].ToString().IsEmpty() ? "0" : dtInfo2.Rows[j]["run_time_second"].ToString());
                            OEE = prod_num == 0 ? "0" : ((qualified_no / prod_num) * (run_time_second / (24 * 60 * 60) * 100)).ToString("0");
                            break;
                        }
                       
                    }
                    number3[i] = Convert.ToInt32(OEE);
                }
                ps.data1 = number;
                ps.data2 = number1;
                ps.data3 = number2;
                ps.data4 = number3;
                ps.str = sjList.Distinct().ToArray();
            }
            data.Add(ps);
            return data;
        }
        /*用时分析*/
        public List<UsedTimeSeque> readUsedTimeSeque(int group_id, int machine_id, string sets_no, DateTime start_date, DateTime end_date, int dur_length, out SecTimeSeque totDurSeq)
        {
            string strSql = createUsedTimeInMain(group_id, machine_id, sets_no, start_date, end_date);

            List<UsedTimeSeque> listUsedTime = getSeqTotDurSeq(strSql, dur_length);

            totDurSeq = getTotDurSeq(dur_length, listUsedTime);
            //stateNum = getStateNum_Custom(listUsedTime);

            return listUsedTime;
        }
        public List<MachineInfo> readMainMachineInfo(int group_id)
        {
            MachineInfo mi = null;
            List<MachineInfo> lmi = new List<MachineInfo>();

            string strSql = "";
            if (group_id == 0)
            {
                strSql = " select group_id,group_name,machine_id,machine_name,machine_series,machine_number,rank_num,sets_no "
                         + " from vw_machine_info where is_main = 'YES' ";
            }
            else
            {
                strSql = " select group_id,group_name,machine_id,machine_name,machine_series,machine_number,rank_num,sets_no "
                         + " from vw_machine_info where is_main = 'YES' and group_id = " + group_id;
            }
            try
            {
                DataTable dtInfo = getStrSqlData("initMachineInfo", strSql).Copy();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    mi = new MachineInfo();

                    mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    mi.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    mi.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    mi.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    mi.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    mi.rank_num = Convert.ToInt32(dtInfo.Rows[i]["rank_num"]);
                    mi.sets_no = dtInfo.Rows[i]["sets_no"].ToString();

                    lmi.Add(mi);
                }
            }
            catch (Exception ep)
            {
               // log.Error("执行readMachineInfo错误！" + ep.Message);
                return null;
            }
            return lmi;
        }
        private string createUsedTimeInMain(int group_id, int machine_id, string sets_no, DateTime start_date, DateTime end_date)
        {
            string strSql = "select distinct ri.group_id,mi.group_name,ri.machine_id,mi.machine_name,mi.machine_series,mi.machine_number,mi.sets_no, "
                            + "      ri.wshift_id,ri.run_state,ri.run_dur,ri.dur_rate,ri.start_time,ri.end_time,ri.read_time "
                            + " from tb_run_state_seque ri,vw_machine_info mi where 1=1 and ri.run_dur > 0 "
                            + "   and ri.machine_id = mi.machine_id and ri.group_id = mi.group_id ";

            if (group_id == 0 || (machine_id == 0 && string.IsNullOrEmpty(sets_no)))
            {
                if (group_id == 0)
                {
                    strSql += "  and mi.is_run_state = 'YES' and is_main = 'YES' "
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
                else
                {
                    strSql += "  and mi.is_run_state = 'YES' and is_main = 'YES' and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
            }
            else
            {
                if (machine_id > 0)
                {
                    strSql += "  and mi.is_run_state = 'YES' and is_main = 'YES' and ri.machine_id = " + machine_id + " and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
                else
                {
                    strSql += "  and mi.is_run_state = 'YES' and is_main = 'YES' and mi.sets_no = '" + sets_no + "' and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
            }
            return strSql;
        }
        public List<UsedTimeSeque> getSeqTotDurSeq(string strSql, int dur_length)
        {
            UsedTimeSeque uts = null;
            List<UsedTimeSeque> listUsedTime = new List<UsedTimeSeque>();

            dtInfo = getStrSqlData("readUsedTimeSeque", strSql);

            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                DataRow[] dtRow = dtInfo.Select("1=1", "read_time asc");

                int dtInfoCount = dtRow.Length;

                for (int i = 0; i < dtInfoCount; i++)
                {
                    int grp_id = Convert.ToInt32(dtRow[i]["group_id"]);
                    int mac_id = Convert.ToInt32(dtRow[i]["machine_id"]);

                    UsedTimeSeque re_uts = listUsedTime.Find(delegate (UsedTimeSeque tut_t1)
                    {
                        if (tut_t1.group_id == grp_id && tut_t1.machine_id == mac_id)
                            return true;
                        else
                            return false;
                    });

                    if (re_uts != null)
                    {
                        TimeSeque ts = new TimeSeque();
                        ts.run_state = Convert.ToInt32(dtRow[i]["run_state"]);
                        if (ts.run_state == 1)
                        {
                            ts.run_class = "bar bar-run";
                            //ts.run_class = "progress-bar progress-bar-run";
                        }
                        else if (ts.run_state == 2)
                        {
                            ts.run_class = "bar bar-alarm";
                            //ts.run_class = "progress-bar progress-bar-success";
                        }
                        else if (ts.run_state == 3)
                        {
                            ts.run_class = "bar bar-pause";
                            //ts.run_class = "progress-bar progress-bar-info";
                        }
                        else if (ts.run_state == 4)
                        {
                            ts.run_class = "bar bar-ready";
                            //ts.run_class = "progress-bar progress-bar-warning";
                        }
                        else
                        {
                            ts.run_class = "bar bar-stop";
                            //ts.run_class = "progress-bar progress-bar-danger";
                        }
                        ts.read_time = Convert.ToDateTime(dtRow[i]["end_time"]);
                        ts.state_start_time = Convert.ToDateTime(dtRow[i]["start_time"]);
                        ts.run_duration = Convert.ToInt32(dtRow[i]["run_dur"]);
                        ts.dur_rate = Convert.ToDouble(dtRow[i]["dur_rate"]);

                        re_uts.litime_seq.Add(ts);
                    }
                    else
                    {
                        uts = new UsedTimeSeque();

                        uts.group_id = Convert.ToInt32(dtRow[i]["group_id"]);
                        uts.group_name = dtRow[i]["group_name"].ToString();
                        uts.machine_id = Convert.ToInt32(dtRow[i]["machine_id"]);
                        uts.machine_name = dtRow[i]["machine_name"].ToString();
                        uts.machine_series = dtRow[i]["machine_series"].ToString();
                        uts.machine_number = dtRow[i]["machine_number"].ToString();
                        uts.sets_no = dtRow[i]["sets_no"].ToString();

                        uts.litime_seq = new List<TimeSeque>();
                        TimeSeque ts = new TimeSeque();
                        ts.run_state = Convert.ToInt32(dtRow[i]["run_state"]);
                        if (ts.run_state == 1)
                        {
                            ts.run_class = "bar bar-run";
                        }
                        else if (ts.run_state == 2)
                        {
                            ts.run_class = "bar bar-alarm";
                        }
                        else if (ts.run_state == 3)
                        {
                            ts.run_class = "bar bar-pause";
                        }
                        else if (ts.run_state ==4)
                        {
                            ts.run_class = "bar bar-ready";
                        }
                        else
                        {
                            ts.run_class = "bar bar-stop";
                        }
                        ts.read_time = Convert.ToDateTime(dtRow[i]["end_time"]);
                        ts.state_start_time = Convert.ToDateTime(dtRow[i]["start_time"]);
                        ts.run_duration = Convert.ToInt32(dtRow[i]["run_dur"]);
                        ts.dur_rate = Convert.ToDouble(dtRow[i]["dur_rate"]);

                        uts.litime_seq.Add(ts);
                        listUsedTime.Add(uts);
                    }
                }
            }

            return listUsedTime;
        }
        private SecTimeSeque getTotDurSeq(int dur_length, List<UsedTimeSeque> listUsedTime)
        {
            SecTimeSeque sts_tot = new SecTimeSeque();

            int tsp_run = 0;
            int tsp_alarm = 0;
            int tsp_pause = 0;
            int tsp_ready = 0;
            //int tsp_stop = 0;
            int tsp_edit = 0;
            int tot_dur = 0;

            foreach (UsedTimeSeque uts in listUsedTime)
            {
                foreach (TimeSeque ts in uts.litime_seq)
                {
                    if (ts.run_state == RunStateParam.RunState)
                    {
                        tsp_run += ts.run_duration;
                    }
                    if (ts.run_state == RunStateParam.AlarmState)
                    {
                        tsp_alarm += ts.run_duration;
                    }
                    if (ts.run_state == RunStateParam.PauseState)
                    {
                        tsp_pause += ts.run_duration;
                    }
                    //if (ts.run_state == RunStateParam.ReadyState)
                    //{
                    //    //tsp_ready += ts.run_duration;
                    //    tsp_stop += ts.run_duration;
                    //}
                    //if (ts.run_state == RunStateParam.StopState)
                    //{
                    //    tsp_stop += ts.run_duration;
                    //}
                    //if (ts.run_state == RunStateParam.EditState)
                    //{
                    //    tsp_edit += ts.run_duration;
                    //}
                }
                tot_dur += dur_length;
            }
            sts_tot.run_duration = tsp_run;
            sts_tot.alarm_duration = tsp_alarm;
            sts_tot.pause_duration = tsp_pause;
            sts_tot.ready_duration = tsp_ready;
            sts_tot.edit_duration = tsp_edit;
            sts_tot.stop_duration = tot_dur - (tsp_run + tsp_alarm + tsp_pause + tsp_ready + tsp_edit);
            sts_tot.run_rate = Math.Round((tsp_run * 100.0) / tot_dur, 2);
            sts_tot.alarm_rate = Math.Round((tsp_alarm * 100.0) / tot_dur, 2);
            sts_tot.pause_rate = Math.Round((tsp_pause * 100.0) / tot_dur, 2);
            sts_tot.ready_rate = Math.Round((tsp_ready * 100.0) / tot_dur, 2);
            sts_tot.edit_rate = Math.Round((tsp_edit * 100.0) / tot_dur, 2);
            sts_tot.stop_rate = Math.Round((sts_tot.stop_duration * 100.0) / tot_dur, 2);
            System.Diagnostics.Trace.WriteLine(sts_tot.stop_rate);

            sts_tot.dur_length = dur_length;

            return sts_tot;
        }
        /*自动线条数*/
        public List<DeviceLineNum> readDeviceLineNum()
        {
            DeviceLineNum dln = null;
            List<DeviceLineNum> listDeviceLineNum = new List<DeviceLineNum>();

            String strSql = " select distinct group_id,group_name from vw_machine_info where 1=1";

            dtInfo = getStrSqlData("readDeviceLineNum", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                dln = new DeviceLineNum();

                dln.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                dln.group_name = dtInfo.Rows[i]["group_name"].ToString();

                listDeviceLineNum.Add(dln);
            }
            return listDeviceLineNum;
        }
        public List<MachineInfo> readMachineInfo(string comm_protocol, string comm_interface)
        {
            MachineInfo mi = null;
            List<MachineInfo> lmi = new List<MachineInfo>();
            String strSql = " select ta.group_number,gi.group_name,ta.machine_id,ta.machine_name,ta.machine_series,ta.machine_number,ta.rank_num,tb.ip_addr,tb.ip_com "
                            + " from tb_machine_info ta,tb_macgroup_info gi,tb_machine_ipaddr tb where ta.machine_id = tb.machine_id and ta.group_number = gi.group_id "
                            + "  and ta.comm_protocol = \'" + comm_protocol + "\' and ta.comm_interface = \'" + comm_interface + "\' and ta.enabled = 1 and gi.enabled = 1 and tb.enabled = 1 ";

            try
            {
                DataTable dtInfo = getStrSqlData("initMachineInfo", strSql).Copy();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    mi = new MachineInfo();

                    mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_number"]);
                    mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    mi.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    mi.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    mi.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    mi.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    mi.rank_num = Convert.ToInt32(dtInfo.Rows[i]["rank_num"]);
                    mi.machine_ip = dtInfo.Rows[i]["ip_addr"].ToString();
                    mi.machine_port = ushort.Parse(dtInfo.Rows[i]["ip_com"].ToString());
                    mi.connect_state = false;

                    lmi.Add(mi);
                }
            }
            catch (Exception ep)
            {
                //log.Error("执行readMachineInfo错误！" + ep.Message);
                return null;
            }
            return lmi;
        }
        public bool readInitRunState()
        {
            DataTable dtInfo = null;
            String strSql = "select param_name,data_setup from dbo.tb_dict_parameter where param_type = 'RunState' and prw_type = '" + ReadParameter.ToOutParamPrwType + "'";

            //取设备 
            try
            {
                if (initDBConnect())
                {
                    dtInfo = getStrSqlData("readInitRunState", strSql);

                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("RunState"))
                        {
                            RunStateParam.RunState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("AlarmState"))
                        {
                            RunStateParam.AlarmState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("PauseState"))
                        {
                            RunStateParam.PauseState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("ReadyState"))
                        {
                            RunStateParam.ReadyState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("EditState"))
                        {
                            RunStateParam.EditState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("StopState"))
                        {
                            RunStateParam.StopState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("ExcpState"))
                        {
                            RunStateParam.ExcpState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                        if (dtInfo.Rows[i]["param_name"].ToString().Equals("ReadState"))
                        {
                            RunStateParam.ReadState = Convert.ToInt32(dtInfo.Rows[i]["data_setup"]);
                        }
                    }
                    //log.Info("RunStateParam is OK!");
                }
                return true;
            }
            catch (SystemException ep)
            {
                //log.Error("执行RunStateParam失败！\n" + strSql + "\n" + ep.Message);
                return false;
            }
        }
        /*读取班次信息*/
        public string readShiftInfo(string strSql)
        {
            string dt = "00:00:00";

            dtInfo = getStrSqlData("readShiftInfo", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {

                dt = Convert.ToDateTime(dtInfo.Rows[i]["start_time"]).ToLongTimeString();
            }
            return dt;
        }
        public bool readShiftInfo()
        {
            String strSql = "select wshift_id,wshift_no,wshift_name,wshift_date, "
                            + "     cast(wshift_date + ' 12:30:00' as datetime) as wshift_today,wshift_type,start_dtime,end_dtime "
                            + " from tb_shift_date where start_dtime <= getdate() and end_dtime > getdate() "
                            + "  and wshift_date >= " + DateTime.Today.AddDays(-1).ToString("yyyyMMdd");

            try
            {
                dtInfo = getStrSqlData("initShiftInfo", strSql);

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ShiftInfo.ShiftId = Convert.ToInt32(dtInfo.Rows[i]["wshift_id"].ToString());
                    ShiftInfo.ShiftNo = dtInfo.Rows[i]["wshift_no"].ToString();
                    ShiftInfo.ShiftName = dtInfo.Rows[i]["wshift_name"].ToString();
                    ShiftInfo.ShiftDate = dtInfo.Rows[i]["wshift_date"].ToString();
                    ShiftInfo.ShiftToDay = Convert.ToDateTime(dtInfo.Rows[i]["wshift_today"]);
                    ShiftInfo.StartTime = Convert.ToDateTime(dtInfo.Rows[i]["start_dtime"]);
                    ShiftInfo.EndTime = Convert.ToDateTime(dtInfo.Rows[i]["end_dtime"]);
                   // log.Error("班次日期:！" + Convert.ToDateTime(dtInfo.Rows[i]["start_dtime"]));
                }
            }
            catch (Exception ep)
            {
               // log.Error("班次日期读取失败！" + ep.Message);
                return false;
            }
            return true;
        }
        /*分解时间坐标*/
        public List<DateTime> readUsedTimeAxis(DateTime start_date, DateTime end_date, out int dur_length)
        {
            dur_length = 3600 * 24;
            List<DateTime> listSubTime = new List<DateTime>();

            if (start_date.Equals(end_date))
            {
                dur_length = 3600 * 24;
                for (int i = 0; i < 24; i += 2)
                {
                    listSubTime.Add(Convert.ToDateTime(start_date.ToLongDateString() + " " + i.ToString() + ":00"));
                }
            }
            else
            {
                TimeSpan ts1 = new TimeSpan(end_date.AddDays(1).Ticks);
                TimeSpan ts2 = new TimeSpan(start_date.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                dur_length = Convert.ToInt32(Math.Round(ts.TotalSeconds, 0));

                int days = Convert.ToInt32(Math.Round(ts.TotalDays, 0));
                for (int i = 0; i < days; i++)
                {
                    listSubTime.Add(start_date.AddDays(i));
                }
            }

            for (int i = 0; i < listSubTime.Count; i++)
            {
               // log.Info(string.Format("listSubTime:{0} {1}", i, listSubTime[i].ToString()));
            }

            return listSubTime;
        }
        /*用时分析*/
        public List<UsedTimeSeque> readDeviceUsedTimeSeque_His(int group_id, int machine_id, string sets_no, DateTime start_date, DateTime end_date, int dur_length, out SecTimeSeque totDurSeq)
        {
            string strSql = createUsedTimeNoMain_His(group_id, machine_id, sets_no, start_date, end_date);

            List<UsedTimeSeque> listUsedTime = getSeqTotDurSeq(strSql, dur_length);

            totDurSeq = getTotDurSeq(dur_length, listUsedTime);
            //stateNum = getStateNum_Custom(listUsedTime);

            return listUsedTime;
        }
        private string createUsedTimeNoMain_His(int group_id, int machine_id, string sets_no, DateTime start_date, DateTime end_date)
        {
            string strSql = "select distinct ri.group_id,mi.group_name,ri.machine_id,mi.machine_name,mi.machine_series,mi.machine_number,mi.sets_no, "
                            + "      ri.wshift_id,ri.run_state,ri.run_dur,ri.dur_rate,ri.start_time,ri.end_time,ri.read_time "
                            + " from tb_run_state_seque ri,vw_machine_info mi where 1=1 and ri.run_dur > 0 and ri.machine_id = mi.machine_id ";

            if (group_id == 0 || (machine_id == 0 && string.IsNullOrEmpty(sets_no)))
            {
                if (group_id == 0)
                {
                    strSql += "  and ri.group_id = mi.group_id and mi.is_run_state = 'YES' "
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                            + " order by ri.start_time asc,ri.read_time asc";
                }
                else
                {
                    strSql += "  and ri.group_id = mi.group_id and mi.is_run_state = 'YES' and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                            + " order by ri.start_time asc,ri.read_time asc";
                }
            }
            else
            {
                if (machine_id > 0)
                {
                    strSql += "  and ri.group_id = mi.group_id and mi.is_run_state = 'YES' and ri.machine_id = " + machine_id + " and ri.group_id = " + group_id
                             + " and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                             + " order by ri.start_time asc,ri.read_time asc";
                }
                else
                {
                    strSql += "  and ri.group_id = mi.group_id and mi.is_run_state = 'YES' and mi.sets_no = '" + sets_no + "' and ri.group_id = " + group_id
                             + " and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                             + " order by ri.start_time asc,ri.read_time asc";
                }
            }
            return strSql;
        }
        /*机器数*/
        public List<MachineInfo> readSLineDeviceNum(int group_id)
        {
            MachineInfo mi = null;
            List<MachineInfo> listSLineMachine = new List<MachineInfo>();

            String strSql = " select machine_id,machine_number from vw_machine_info where 1 = 1 and category != 'PLC' and group_id = " + group_id;

            dtInfo = getStrSqlData("listSLineMachine", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                mi.machine_number = dtInfo.Rows[i]["machine_number"].ToString();

                listSLineMachine.Add(mi);
            }
            return listSLineMachine;
        }
        /*自动线条数*/
        public List<MachineSeries> readLineSeries(int group_id)
        {
            MachineSeries ms = null;
            List<MachineSeries> listLineSeries = new List<MachineSeries>();

            String strSql = "";
            if (group_id == 0)
            {
                strSql = " select series_id,machine_series,comm_protocol,comm_interface,category from tb_machine_series where enabled = 1 ";
            }
            else
            {
                strSql = " select series_id,machine_series,comm_protocol,comm_interface,category from tb_machine_series where enabled = 1 ";
            }

            dtInfo = getStrSqlData("readLineSeries", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ms = new MachineSeries();

                ms.series_id = Convert.ToInt32(dtInfo.Rows[i]["series_id"]);
                ms.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                ms.comm_protocol = dtInfo.Rows[i]["comm_protocol"].ToString();
                ms.comm_interface = dtInfo.Rows[i]["comm_interface"].ToString();
                ms.category = dtInfo.Rows[i]["category"].ToString();

                listLineSeries.Add(ms);
            }
            return listLineSeries;
        }
        /*机器数*/
        public List<MachineInfo> GetLineSeriesMachine(int group_id, int series_id, string machine_series)
        {
            MachineInfo mi = null;
            List<MachineInfo> listLineSeriesMachine = new List<MachineInfo>();

            String strSql = " select machine_id,machine_number from vw_machine_info where 1 = 1 and machine_series = '" + machine_series + "' and group_id = " + group_id;

            dtInfo = getStrSqlData("listLineSeriesMachine", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                mi.machine_number = dtInfo.Rows[i]["machine_number"].ToString();

                listLineSeriesMachine.Add(mi);
            }
            return listLineSeriesMachine;
        }
        /*自动线条数*/
        public List<DeviceLineNum> readRealDeviceLineNum()
        {
            DeviceLineNum dln = null;
            List<DeviceLineNum> listDeviceLineNum = new List<DeviceLineNum>();

            String strSql = " select distinct group_id,group_name,sets_no,rank_sets from vw_machine_info where 1=1 and is_run_state = 'YES' order by rank_sets asc ";

            dtInfo = getStrSqlData("readDeviceLineNum", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                dln = new DeviceLineNum();

                dln.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                dln.group_name = dtInfo.Rows[i]["group_name"].ToString();
                dln.sets_no = dtInfo.Rows[i]["sets_no"].ToString();

                listDeviceLineNum.Add(dln);
            }
            return listDeviceLineNum;
        }
        /*实时状态*/
        public List<DeviceRealState> readDeviceRealState(string min_group)
        {
            DeviceRealState drs = null;
            List<DeviceRealState> listRealState = new List<DeviceRealState>();
            //RepositoryFactory BR = new RepositoryFactory();
            //var mac = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_machine_info");
            String strSql = "";
            /**
            if (min_group != null && min_group != "")
            {
                strSql = " select distinct vri.group_id,vri.group_name,vri.machine_id,vri.machine_name,vri.machine_series,vri.machine_number,vri.sets_no,vri.rank_num, "
                         + "      vri.run_state,tmp.pic_path,vri.main_prog_num,vri.running_prog_num,vri.act_spindle_speed_0,vri.act_spindle_override_0, "
                         + "      vri.act_feed_speed_0,vri.act_feed_override_0,vri.act_spindle_speed_1,vri.act_spindle_override_1,vri.act_feed_speed_1, "
                         + "      vri.act_feed_override_1,vri.act_spindle_speed_2,vri.act_spindle_override_2,vri.act_feed_speed_2,vri.act_feed_override_2, "
                         + "      vri.sp_load_0,vri.sp_load_1,vri.sp_load_2,vri.sv_load_0,vri.sv_load_1,vri.sv_load_2,vri.read_time  "
                         + " from vw_run_info vri,tb_machine_photo tmp where 1=1 and vri.machine_id = tmp.machine_id and vri.run_state = tmp.pic_state "
                         + "  and vri.row_ord = 1 and tmp.pic_kinds = 'RealState' and CAST(vri.group_id AS varchar(2))+vri.sets_no = '" + min_group + "'"
                         + " order by vri.rank_num asc,vri.read_time desc ";
            }
            else
            {
                strSql = " select distinct vri.group_id,vri.group_name,vri.machine_id,vri.machine_name,vri.machine_series,vri.machine_number,vri.sets_no,vri.rank_num, "
                         + "      vri.run_state,tmp.pic_path,vri.main_prog_num,vri.running_prog_num,vri.act_spindle_speed_0,vri.act_spindle_override_0, "
                         + "      vri.act_feed_speed_0,vri.act_feed_override_0,vri.act_spindle_speed_1,vri.act_spindle_override_1,vri.act_feed_speed_1, "
                         + "      vri.act_feed_override_1,vri.act_spindle_speed_2,vri.act_spindle_override_2,vri.act_feed_speed_2,vri.act_feed_override_2, "
                         + "      vri.sp_load_0,vri.sp_load_1,vri.sp_load_2,vri.sv_load_0,vri.sv_load_1,vri.sv_load_2,vri.read_time  "
                         + " from vw_run_info vri,tb_machine_photo tmp where 1=1 and vri.machine_id = tmp.machine_id and vri.run_state = tmp.pic_state "
                         + "  and vri.row_ord = 1 and tmp.pic_kinds = 'RealState' order by vri.rank_num asc,vri.read_time desc";
            }**/
            if (min_group != null && min_group != "")
            {
                strSql = " SELECT DISTINCT mi.group_id,mi.group_name,mi.machine_id,mi.machine_name,mi.machine_series,mi.machine_number,mi.sets_no, "
                          + "      mi.rank_num,ri.run_state,tmp.pic_path,ri.main_prog_num,ri.running_prog_num,ri.act_spindle_speed_0,ri.act_spindle_override_0, "
                          + "      ri.act_feed_speed_0,ri.act_feed_override_0,ri.act_spindle_speed_1,ri.act_spindle_override_1,ri.act_feed_speed_1, "
                          + "      ri.act_feed_override_1,ri.act_spindle_speed_2,ri.act_spindle_override_2,ri.act_feed_speed_2,ri.act_feed_override_2, "
                          + "      ri.sp_load_0,ri.sp_load_1,ri.sp_load_2,ri.sv_load_0,ri.sv_load_1,ri.sv_load_2,ri.read_time "
                          + " FROM (select wshift_id,wshift_date,machine_id,run_state,main_prog_num,running_prog_num,act_spindle_speed_0,act_spindle_override_0, "
                          + "              act_feed_speed_0,act_feed_override_0,act_spindle_speed_1,act_spindle_override_1,act_feed_speed_1,act_feed_override_1, "
                          + "              act_spindle_speed_2,act_spindle_override_2,act_feed_speed_2,act_feed_override_2,sp_load_0,sp_load_1,sp_load_2, "
                          + "              sv_load_0,sv_load_1,sv_load_2,read_time,row_number() over(partition by machine_id order by read_time desc) as row_ord "
                          + "         from tb_run_info) ri,vw_machine_info mi,tb_machine_photo tmp "
                          + " WHERE 1=1 AND mi.machine_id = ri.machine_id AND ri.machine_id = tmp.machine_id AND ri.run_state = tmp.pic_state "
                          + "   AND ri.wshift_date = CONVERT(VARCHAR(10),GETDATE(),112) AND mi.run_param IN ('YES','YES_S') "
                          + "   AND tmp.pic_kinds = 'RealState' and ri.row_ord = 1 "
                          + " ORDER BY mi.rank_num ASC,ri.read_time DESC ";
            }
            else
            {
                strSql = " SELECT DISTINCT mi.group_id,mi.group_name,mi.machine_id,mi.machine_name,mi.machine_series,mi.machine_number,mi.sets_no, "
                         + "      mi.rank_num,ri.run_state,tmp.pic_path,ri.main_prog_num,ri.running_prog_num,ri.act_spindle_speed_0,ri.act_spindle_override_0, "
                         + "      ri.act_feed_speed_0,ri.act_feed_override_0,ri.act_spindle_speed_1,ri.act_spindle_override_1,ri.act_feed_speed_1, "
                         + "      ri.act_feed_override_1,ri.act_spindle_speed_2,ri.act_spindle_override_2,ri.act_feed_speed_2,ri.act_feed_override_2, "
                         + "      ri.sp_load_0,ri.sp_load_1,ri.sp_load_2,ri.sv_load_0,ri.sv_load_1,ri.sv_load_2,ri.read_time "
                         + " FROM (select wshift_id,wshift_date,machine_id,run_state,main_prog_num,running_prog_num,act_spindle_speed_0,act_spindle_override_0, "
                         + "              act_feed_speed_0,act_feed_override_0,act_spindle_speed_1,act_spindle_override_1,act_feed_speed_1,act_feed_override_1, "
                         + "              act_spindle_speed_2,act_spindle_override_2,act_feed_speed_2,act_feed_override_2,sp_load_0,sp_load_1,sp_load_2, "
                         + "              sv_load_0,sv_load_1,sv_load_2,read_time,row_number() over(partition by machine_id order by read_time desc) as row_ord "
                         + "         from tb_run_info) ri,vw_machine_info mi,tb_machine_photo tmp "
                         + " WHERE 1=1 AND mi.machine_id = ri.machine_id AND ri.machine_id = tmp.machine_id AND ri.run_state = tmp.pic_state "
                         + "   AND ri.wshift_date = CONVERT(VARCHAR(10),GETDATE(),112) AND mi.run_param IN ('YES','YES_S') "
                         + "   AND tmp.pic_kinds = 'RealState' and ri.row_ord = 1 "
                         + " ORDER BY mi.machine_id ASC,ri.read_time DESC ";

            }

            dtInfo = getStrSqlData("readDeviceRealState", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                drs = new DeviceRealState();

                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("S7-1200"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                   // drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("CJ2M-CPU31"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                   // drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("BR-X20"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                    //drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("M-20iA"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                    drs.main_prog_num = dtInfo.Rows[i]["main_prog_num"].ToString();
                    drs.running_prog_num = dtInfo.Rows[i]["running_prog_num"].ToString();
                   // drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("FX3G"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                   // drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("E3230"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                    drs.main_prog_num = dtInfo.Rows[i]["main_prog_num"].ToString();
                    drs.running_prog_num = dtInfo.Rows[i]["running_prog_num"].ToString();
                    drs.act_spindle_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_speed_0"]);
                    drs.act_spindle_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_override_0"]);
                    drs.act_feed_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_speed_0"]);
                    drs.act_feed_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_override_0"]);
                    //drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("0i-TD"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                    drs.main_prog_num = dtInfo.Rows[i]["main_prog_num"].ToString();
                    drs.running_prog_num = dtInfo.Rows[i]["running_prog_num"].ToString();
                    drs.act_spindle_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_speed_0"]);
                    drs.act_spindle_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_override_0"]);
                    drs.act_feed_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_speed_0"]);
                    drs.act_feed_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_override_0"]);
                    //drs.sp_load_0 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_0"]);
                    //drs.sp_load_1 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_1"]);
                    //drs.sp_load_2 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_2"]);
                    //drs.sv_load_0 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_0"]);
                    //drs.sv_load_1 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_1"]);
                    //drs.sv_load_2 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_2"]);
                    //drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }
                if (dtInfo.Rows[i]["machine_series"].ToString().Equals("F32i-B"))
                {
                    drs.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                    drs.group_name = dtInfo.Rows[i]["group_name"].ToString();
                    drs.machine_id = Convert.ToInt32(dtInfo.Rows[i]["machine_id"]);
                    drs.machine_name = dtInfo.Rows[i]["machine_name"].ToString();
                    drs.machine_series = dtInfo.Rows[i]["machine_series"].ToString();
                    drs.machine_number = dtInfo.Rows[i]["machine_number"].ToString();
                    drs.sets_no = dtInfo.Rows[i]["sets_no"].ToString();
                    drs.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                    drs.main_prog_num = dtInfo.Rows[i]["main_prog_num"].ToString();
                    drs.running_prog_num = dtInfo.Rows[i]["running_prog_num"].ToString();
                    drs.act_spindle_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_speed_0"]);
                    drs.act_spindle_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_spindle_override_0"]);
                    drs.act_feed_speed_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_speed_0"]);
                    drs.act_feed_override_0 = Convert.ToDouble(dtInfo.Rows[i]["act_feed_override_0"]);
                    //drs.sp_load_0 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_0"]);
                    //drs.sp_load_1 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_1"]);
                    //drs.sp_load_2 = Convert.ToDouble(dtInfo.Rows[i]["sp_load_2"]);
                    //drs.sv_load_0 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_0"]);
                    //drs.sv_load_1 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_1"]);
                    //drs.sv_load_2 = Convert.ToDouble(dtInfo.Rows[i]["sv_load_2"]);
                   // drs.pic_path = dtInfo.Rows[i]["pic_path"].ToString();
                    drs.read_short_date = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToLongDateString();
                    drs.read_short_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToShortTimeString();
                    drs.read_time = Convert.ToDateTime(dtInfo.Rows[i]["read_time"]).ToString("f");
                }

                if (drs.run_state == RunStateParam.RunState)
                {
                    drs.run_class = "col-subject col-subject-run";
                }
                else if (drs.run_state == RunStateParam.AlarmState)
                {
                    drs.run_class = "col-subject col-subject-alarm";
                }
                else if (drs.run_state == RunStateParam.PauseState)
                {
                    drs.run_class = "col-subject col-subject-pause";
                }
                else if (drs.run_state == RunStateParam.ReadyState)
                {
                    drs.run_class = "col-subject col-subject-ready";
                }
                else if (drs.run_state == RunStateParam.EditState)
                {
                    drs.run_class = "col-subject col-subject-edit";
                }
                else
                {
                    drs.run_class = "col-subject col-subject-stop";
                }

                listRealState.Add(drs);
            }
            if (dtInfo.Rows.Count < 4)
            {
                for (int i = 0; i < 4 - dtInfo.Rows.Count; i++)
                {
                    drs = new DeviceRealState();
                    drs.group_id = 0;
                    drs.group_name = "";
                    drs.machine_id = 0;
                    drs.machine_name = "";
                    drs.machine_series = "";
                    drs.machine_number = "";
                    drs.sets_no = "";
                    drs.run_state = 0;
                    drs.main_prog_num = "";
                    drs.running_prog_num = "";
                    drs.pic_path = "Images/default.png";
                    drs.read_short_date = DateTime.Now.ToLongDateString();
                    drs.read_short_time = DateTime.Now.ToShortTimeString();
                    drs.read_time = DateTime.Now.ToString("f");
                    drs.run_class = "col-subject col-subject-stop";

                    listRealState.Add(drs);
                }
            }
            return listRealState;
        }

        public List<string> GetMGPageList(string queryJson)
        {
            List<string> listRealState = new List<string>();
            var dtime = DateTime.Now.AddSeconds(-10);
            try
            {
                DeviceRealState dr = new DeviceRealState();
                var queryParam = queryJson.ToJObject();
                RepositoryFactory BR = new RepositoryFactory();
                //var date = BR.BaseRepository("MongoDB").FindMG<BsonDocument>("clc_calc_date", dtime.Ticks.ToString());
                var mac = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_machine_info");
                for (int i = 0; i < mac.Count; i++) {
                    queryParam.Add("time_second", dtime.Ticks.ToString());
                    queryParam.Add("machine_id", mac[i].GetElement(1).Value.ToString());
                    //var data = BR.BaseRepository("MongoDB").FindMGAscFirst<BsonDocument>("clc_run_param", queryParam);
                    //if (data != null)
                    //{
                    //    var DataToJson = data.ToJson();
                    //    listRealState.Add(DataToJson);
                    //}

                    queryParam.RemoveAll();
                }
               
                return listRealState;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowDataAccessException(ex);
                }
            }
        }
        /*取时间差总秒数*/
        public int readDiffTotalSeconds(DateTime start_time, DateTime end_time)
        {
            TimeSpan ts1 = new TimeSpan(end_time.Ticks);
            TimeSpan ts2 = new TimeSpan(start_time.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            int run_duration = Convert.ToInt32(Math.Round(ts.TotalSeconds, 0));

            return run_duration;
        }
        /*用时分析*/
        public List<UsedTimeSeque> readSeqTotDurSeq_His(int group_id, int machine_id, string sets_no, DateTime start_time, DateTime end_time, int dur_length, out RunCountData stateNum, out SecTimeSeque totDurSeq, out List<SecTimeSeque> listSecDurSeq)
        {
            string strSql = createUsedTimeInMain_His(group_id, machine_id, sets_no, start_time, end_time);

            List<UsedTimeSeque> listUsedTime = getSeqTotDurSeq(strSql, dur_length);

            stateNum = getStateNum_Custom(listUsedTime);
            totDurSeq = getTotDurSeq(dur_length, listUsedTime);
            listSecDurSeq = getSecDurSeq(start_time, end_time, dur_length, listUsedTime);
            listSecDurSeq.Add(totDurSeq);

            return listUsedTime;
        }
        /*取时间差总秒数*/
        public int getDiffTotalDays(DateTime start_time, DateTime end_time)
        {
            if (DateTime.Compare(end_time, start_time) < 0)
                return 0;

            TimeSpan ts1 = new TimeSpan(end_time.Ticks);
            TimeSpan ts2 = new TimeSpan(start_time.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            int run_duration = Convert.ToInt32(Math.Round(ts.TotalDays, 0));

            return run_duration;
        }
        private string createUsedTimeInMain_His(int group_id, int machine_id, string sets_no, DateTime start_date, DateTime end_date)
        {
            string strSql = "select distinct ri.group_id,mi.group_name,ri.machine_id,mi.machine_name,mi.machine_series,mi.machine_number,mi.sets_no, "
                           + "      ri.wshift_id,ri.run_state,ri.run_dur,ri.dur_rate,ri.start_time,ri.end_time,,ri.read_time "
                           + " from tb_run_state_seque ri,vw_machine_info mi where 1=1 and ri.run_dur > 0 "
                           + "   and ri.machine_id = mi.machine_id and ri.group_id = mi.group_id ";

            if (group_id == 0 || (machine_id == 0 && string.IsNullOrEmpty(sets_no)))
            {
                if (group_id == 0)
                {
                    strSql += "  and mi.is_run_state = 'YES' and mi.is_main = 'YES' "
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                            +" order by read_time desc";
                }
                else
                {
                    strSql += "  and mi.is_run_state = 'YES' and mi.is_main = 'YES' and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                              + " order by read_time desc";
                }
            }
            else
            {
                if (machine_id > 0)
                {
                    strSql += "  and mi.is_run_state = 'YES'  and ri.machine_id = " + machine_id + " and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                              + " order by read_time desc";
                }
                else
                {
                    strSql += "  and mi.is_run_state = 'YES'  and mi.sets_no = '" + sets_no + "' and ri.group_id = " + group_id
                            + "  and ri.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and ri.wshift_date <= " + end_date.ToString("yyyyMMdd")
                              + " order by read_time desc";
                }
            }
            return strSql;
        }
        private RunCountData getStateNum_Custom(List<UsedTimeSeque> listUsedTime)
        {
            RunCountData rcd = new RunCountData();

            int tsp_run = 0;
            int tsp_alarm = 0;
            int tsp_pause = 0;
            //int tsp_ready = 0;
            int tsp_stop = 0;

            foreach (UsedTimeSeque uts in listUsedTime)
            {
                foreach (TimeSeque ts in uts.litime_seq)
                {
                    if (ts.run_state == RunStateParam.RunState && ts.run_duration > 600)
                    {
                        tsp_run++;
                    }
                    if (ts.run_state == RunStateParam.AlarmState && ts.run_duration > 3600)
                    {
                        tsp_alarm++;
                    }
                    if (ts.run_state == RunStateParam.PauseState && ts.run_duration > 600)
                    {
                        tsp_pause++;
                    }
                    if (ts.run_state == RunStateParam.ReadyState && ts.run_duration > 600)
                    {
                        //tsp_ready++;
                        tsp_stop++;
                    }
                    if (ts.run_state == RunStateParam.StopState && ts.run_duration > 3600)
                    {
                        tsp_stop++;
                    }
                }
            }
            rcd.int_data1 = tsp_run;
            rcd.int_data2 = tsp_alarm;
            rcd.int_data3 = tsp_pause;
            rcd.int_data4 = tsp_stop;

            return rcd;
        }
        private List<SecTimeSeque> getSecDurSeq(DateTime start_time, DateTime end_time, int dur_length, List<UsedTimeSeque> listUsedTime)
        {
            double tsec_run = 0.0;
            double tsec_alarm = 0.0;
            double tsec_pause = 0.0;
            double tsec_ready = 0.0;
            double tsec_stop = 0.0;
            double tsec_edit = 0.0;

            DateTime vtime = start_time;

            TimeSpan ts1s = new TimeSpan(end_time.AddDays(1).Ticks);
            TimeSpan ts2s = new TimeSpan(start_time.Ticks);
            TimeSpan tss = ts1s.Subtract(ts2s);
            int run_days = tss.Days;

            SecTimeSeque sts = null;
            List<SecTimeSeque> listSecDurSeq = new List<SecTimeSeque>();

            for (int i = 0; i < run_days; i++)
            {
                vtime = start_time.AddDays(i);
                string wshift_date = vtime.ToString("yyyyMMdd");

                sts = new SecTimeSeque();

                foreach (UsedTimeSeque uts in listUsedTime)
                {
                    foreach (TimeSeque ts in uts.litime_seq)
                    {
                        if (ts.run_state == RunStateParam.RunState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        {
                            tsec_run += Math.Round(ts.run_duration / 3600.0, 3);
                        }
                        if (ts.run_state == RunStateParam.AlarmState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        {
                            tsec_alarm += Math.Round(ts.run_duration / 3600.0, 3);
                        }
                        if (ts.run_state == RunStateParam.PauseState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        {
                            tsec_pause += Math.Round(ts.run_duration / 3600.0, 3);
                        }
                        //if (ts.run_state == RunStateParam.ReadyState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        //{
                        //    tsec_ready += Math.Round(ts.run_duration / 3600.0,3);
                        //}
                        //if (ts.run_state == RunStateParam.StopState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        //{
                        //    tsec_stop += Convert.ToInt32(Math.Ceiling(ts.run_duration / 60.0));
                        //}
                        //if (ts.run_state == RunStateParam.EditState && ts.state_start_time.ToString("yyyyMMdd").Equals(wshift_date))
                        //{
                        //    tsec_edit += Convert.ToInt32(Math.Ceiling(ts.run_duration/60.0));
                        //}
                        tsec_stop = 24.0 - (tsec_run + tsec_alarm + tsec_pause + tsec_ready);
                        if (tsec_stop < 0)
                        {
                            tsec_stop = 0;
                        }

                    }
                }
                sts.id = i;
                sts.run_duration = tsec_run;
                sts.alarm_duration = tsec_alarm;
                sts.pause_duration = tsec_pause;
                sts.ready_duration = tsec_ready;
                sts.stop_duration = tsec_stop;
                sts.edit_duration = tsec_edit;
                sts.calc_date = wshift_date;
                sts.read_time = vtime;

                listSecDurSeq.Add(sts);

                tsec_run = 0;
                tsec_alarm = 0;
                tsec_pause = 0;
                tsec_ready = 0;
                tsec_stop = 0;
                tsec_edit = 0;
            }

            return listSecDurSeq;
        }
        /*产量分析*/
        public List<MachineInfo> readShiftProdNum_His(int group_id, int machine_id, DateTime start_date, DateTime end_date)
        {
            MachineInfo mi = null;
            List<MachineInfo> listDeviceProdNum = new List<MachineInfo>();

            String strSql = "select wshift_id,wshift_name from tb_shift_date where wshift_date = " + ShiftInfo.ShiftDate;
            dtInfo = getStrSqlData("readShiftProdNum", strSql).Copy();

            int days = getDiffTotalDays(start_date, end_date);
            int years = end_date.Year - start_date.Year;
            int months = years * 12 + (end_date.Month - start_date.Month);
            int mdays = start_date.AddDays(1 - start_date.Day).AddMonths(1).AddDays(-1).Day;

            if (months > 12)
            {
                for (int i = 0; i <= years; i++)
                {
                    for (int vi = 0; vi < dtInfo.Rows.Count; vi++)
                    {
                        mi = new MachineInfo();

                        mi.calc_date = start_date.Year + i;
                        mi.prod_num = 0;

                        mi.wshift_id = Convert.ToInt32(dtInfo.Rows[vi]["wshift_id"]);
                        mi.wshift_name = dtInfo.Rows[vi]["wshift_name"].ToString();

                        listDeviceProdNum.Add(mi);
                    }
                }
            }
            else
            {
                if (months >= 1 && days > mdays)
                {
                    for (int i = 0; i <= months; i++)
                    {
                        for (int vi = 0; vi < dtInfo.Rows.Count; vi++)
                        {
                            DateTime tmpDate = start_date.AddMonths(i);
                            mi = new MachineInfo();

                            mi.calc_date = Convert.ToInt32(tmpDate.ToString("yyyyMM"));
                            mi.prod_num = 0;

                            mi.wshift_id = Convert.ToInt32(dtInfo.Rows[vi]["wshift_id"]);
                            mi.wshift_name = dtInfo.Rows[vi]["wshift_name"].ToString();

                            listDeviceProdNum.Add(mi);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= days; i++)
                    {
                        for (int vi = 0; vi < dtInfo.Rows.Count; vi++)
                        {
                            mi = new MachineInfo();

                            mi.calc_date = Convert.ToInt32(start_date.AddDays(i).ToString("yyyyMMdd"));
                            mi.prod_num = 0;

                            mi.wshift_id = Convert.ToInt32(dtInfo.Rows[vi]["wshift_id"]);
                            mi.wshift_name = dtInfo.Rows[vi]["wshift_name"].ToString();

                            listDeviceProdNum.Add(mi);
                        }
                    }
                }
            }

            if (group_id == 0 || machine_id == 0)
            {
                if (group_id == 0)
                {
                    strSql = " select sp.wshift_date,sp.prod_num,sp.wshift_id,vsi.wshift_name "
                             + " from tb_curday_prod_num_his sp,tb_shift_date vsi "
                             + " where 1=1 and sp.wshift_id = vsi.wshift_id and sp.wshift_date = vsi.wshift_date "
                             + "   and sp.is_stage = 'Shift' and sp.machine_id = 0 "
                             + "   and sp.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and sp.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
                else
                {
                    strSql = " select sp.wshift_date,sp.prod_num,sp.wshift_id,vsi.wshift_name "
                             + " from tb_curday_prod_num_his sp,tb_shift_date vsi "
                             + " where 1=1 and sp.wshift_id = vsi.wshift_id and sp.wshift_date = vsi.wshift_date "
                             + "   and sp.is_stage = 'Shift' and sp.machine_id = 0 and group_id = " + group_id
                             + "   and sp.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and sp.wshift_date <= " + end_date.ToString("yyyyMMdd");
                }
            }
            else
            {
                strSql = " select sp.wshift_date,sp.prod_num,sp.wshift_id,vsi.wshift_name "
                         + " from tb_curday_prod_num_his sp,tb_shift_date vsi "
                         + " where 1=1 and sp.wshift_id = vsi.wshift_id and sp.wshift_date = vsi.wshift_date "
                         + "   and sp.is_stage = 'Shift' and sp.machine_id = " + machine_id + " and group_id = " + group_id
                         + "   and sp.wshift_date >= " + start_date.ToString("yyyyMMdd") + " and sp.wshift_date <= " + end_date.ToString("yyyyMMdd");
            }

            dtInfo = getStrSqlData("readDeviceProdNum", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                if (months > 12)
                {
                    int calc_date = Convert.ToInt32(dtInfo.Rows[i]["wshift_date"].ToString().Substring(0, 4));
                    int wshift_id = Convert.ToInt32(dtInfo.Rows[i]["wshift_id"]);

                    MachineInfo vmi = listDeviceProdNum.Find(delegate (MachineInfo tmi)
                    {
                        return tmi.calc_date == calc_date && tmi.wshift_id == wshift_id;
                    });
                    vmi.prod_num += Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                }
                else
                {
                    if (months >= 1 && days > mdays)
                    {
                        int calc_date = Convert.ToInt32(dtInfo.Rows[i]["wshift_date"].ToString().Substring(0, 6));
                        int wshift_id = Convert.ToInt32(dtInfo.Rows[i]["wshift_id"]);

                        MachineInfo vmi = listDeviceProdNum.Find(delegate (MachineInfo tmi)
                        {
                            return tmi.calc_date == calc_date && tmi.wshift_id == wshift_id;
                        });
                        vmi.prod_num += Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                    }
                    else
                    {
                        int calc_date = Convert.ToInt32(dtInfo.Rows[i]["wshift_date"]);
                        int wshift_id = Convert.ToInt32(dtInfo.Rows[i]["wshift_id"]);

                        MachineInfo vmi = listDeviceProdNum.Find(delegate (MachineInfo tmi)
                        {
                            return tmi.calc_date == calc_date && tmi.wshift_id == wshift_id;
                        });
                        vmi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                    }
                }
            }

            return listDeviceProdNum;
        }
        public List<RunStateNum> readDeviceRunNum()
        {
            dtime = DateTime.Now;
            RunStateNum rsn = null;
            List<RunStateNum> listRunNum = new List<RunStateNum>(5);

            String strSql = "select count(1) as run_num,run_state from (select distinct tri.machine_id,tri.run_state, "
                            + " row_number() over(partition by tri.machine_id order by tri.read_time desc) as row_ord "
                            + " from tb_run_info tri,vw_machine_info vmi where 1=1 and tri.machine_id = vmi.machine_id "
                            + " and (vmi.show_numv = 'M' or (vmi.show_numv = 'G' and vmi.is_main = 'YES')) "
                            //+ " and tri.calc_date=" + dtime.ToString("yyyyMMdd") + ") t where t.row_ord = 1 group by run_state ";
                            + " and tri.calc_date='" + dtime.ToString("yyyyMMdd") + "') t where t.row_ord = 1 group by run_state ";

            listRunNum.Clear();

            //dtInfo = getStrSqlData("readDeviceRunNum", strSql);

            dtInfo.Clear();
            //int run_state = 0;
            DateTime time = DateTime.Now.AddSeconds(-10);

            JObject queryParam1 = new JObject();
            JObject queryParam2 = new JObject();
            JObject queryParam3 = new JObject();
            JObject queryParam4 = new JObject();
            JObject queryParam5 = new JObject();
            //queryParam.Add("machine_id", machine_id);
            queryParam1.Add("time_second", time.Ticks.ToString());
            queryParam1.Add("run_state", 1);
            queryParam2.Add("time_second", time.Ticks.ToString());
            queryParam2.Add("run_state", 2);
            queryParam3.Add("time_second", time.Ticks.ToString());
            queryParam3.Add("run_state", 3);
            queryParam4.Add("time_second", time.Ticks.ToString());
            queryParam4.Add("run_state", 4);
            queryParam5.Add("time_second", time.Ticks.ToString());
            queryParam5.Add("run_state", 0);
            RepositoryFactory BR = new RepositoryFactory();
            var data1 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam1);
            var num1 = data1.Count.ToDouble();//运行
            var data2 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam2);
            var num2 = data2.Count.ToDouble();//报警
            var data3 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam3);
            var num3 = data3.Count.ToDouble();//暂停
            var data4 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam4);
            var num4 = data4.Count.ToDouble();//空闲
            var data5 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam5);
            var num5 = data5.Count.ToDouble();//停机
            dtInfo.Columns.Add("run_num");
            dtInfo.Columns.Add("run_state");
            dtInfo.Rows.Add(num1, 1);//Add里面参数的数据顺序要和dt中的列的顺序对应 
            dtInfo.Rows.Add(num2, 2);
            dtInfo.Rows.Add(num3, 3);
            dtInfo.Rows.Add(num4, 4);
            dtInfo.Rows.Add(num5, 0);

            Random rd = new Random();

            rsn = new RunStateNum();
            rsn.run_state = RunStateParam.RunState;
            rsn.run_num = rd.Next(0, 0);

            listRunNum.Add(rsn);

            rsn = new RunStateNum();
            rsn.run_state = RunStateParam.AlarmState;
            rsn.run_num = rd.Next(0, 0);

            listRunNum.Add(rsn);

            rsn = new RunStateNum();
            rsn.run_state = RunStateParam.PauseState;
            rsn.run_num = rd.Next(0, 0);

            listRunNum.Add(rsn);

            rsn = new RunStateNum();
            rsn.run_state = RunStateParam.StopState;
            rsn.run_num = rd.Next(0, 0);

            listRunNum.Add(rsn);

            rsn = new RunStateNum();
            rsn.run_state = RunStateParam.ReadyState;
            rsn.run_num = rd.Next(0, 0);

            listRunNum.Add(rsn);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                rsn = new RunStateNum();

                rsn.run_state = Convert.ToInt32(dtInfo.Rows[i]["run_state"]);
                rsn.run_num = Convert.ToInt32(dtInfo.Rows[i]["run_num"]);

                if (rsn.run_state == 1)
                {
                    listRunNum[0] = rsn;
                }
                else if (rsn.run_state == 2)
                {
                    listRunNum[1] = rsn;
                }
                else if (rsn.run_state == 3)
                {
                    listRunNum[2] = rsn;
                }
                else if (rsn.run_state == 4)
                {
                    listRunNum[4] = rsn;
                }
                else
                {
                    listRunNum[3] = rsn;
                }
            }

            return listRunNum;
        }
        /*产量分析*/
        public List<MachineInfo> readDeviceProdNum(int group_id, int machine_id, DateTime start_date, DateTime end_date)
        {
            MachineInfo mi = null;
            List<MachineInfo> listDeviceProdNum = new List<MachineInfo>();

            dtime = DateTime.Now;

            String strSql = "select top(9) sp.day_time as name,sp.group_id,sp.group_name,sp.prod_num as value,sp.plan_prod_num,sp.pass_prod_num "
                        + " from tb_curday_prod_num sp,tb_dict_parameter dp where 1 = 1 and sp.prod_num!=0  and sp.day_time = dp.data_setup"
                        + " and dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = 11"
                        + " and sp.calc_date = " + dtime.ToString("yyyyMMdd") + " order by dp.data_addr asc, sp.day_time asc";

            /*
        if (group_id == 0 || machine_id == 0)
        {
            if (group_id == 0)
            {
                strSql = " select sp.calc_date,sum(sp.prod_num) as prod_num,sum(sp.pass_prod_num) as pass_prod_num from vw_shift_prod sp,vw_machine_info mi "
                         + " where 1=1 and sp.machine_id = mi.machine_id and sp.row_ord = 1 and mi.is_prod = '" + ReadParameter.DeviceSXLProd + "'"
                         + " and sp.calc_date >= " + start_date.ToString("yyyyMMdd") + " and sp.calc_date <= " + end_date.ToString("yyyyMMdd")
                         + " group by sp.calc_date ";
            }
            else
            {
                strSql = " select sp.calc_date,sum(sp.prod_num) as prod_num,sum(sp.pass_prod_num) as pass_prod_num from vw_shift_prod sp,vw_machine_info mi "
                         + " where 1=1 and sp.machine_id = mi.machine_id and sp.row_ord = 1 and mi.is_prod = '" + ReadParameter.DeviceSXLProd + "' and mi.group_id = " + group_id
                         + " and sp.calc_date >= " + start_date.ToString("yyyyMMdd") + " and sp.calc_date <= " + end_date.ToString("yyyyMMdd")
                         + " group by sp.calc_date ";
            }
        }
        else
        {
            strSql = " select sp.calc_date,sum(sp.prod_num) as prod_num,sum(sp.pass_prod_num) as pass_prod_num from vw_shift_prod sp,vw_machine_info mi "
                     + " where 1=1 and sp.machine_id = mi.machine_id and sp.row_ord = 1 and mi.is_prod = '" + ReadParameter.DeviceSXLProd + "' and mi.machine_id = " + machine_id + " and mi.group_id = " + group_id
                     + " and sp.calc_date >= " + start_date.ToString("yyyyMMdd") + " and sp.calc_date <= " + end_date.ToString("yyyyMMdd")
                     + " group by sp.calc_date ";
        }
        */
            dtInfo = getStrSqlData("readDeviceProdNum", strSql);

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                //mi.calc_date = Convert.ToInt32(dtInfo.Rows[i]["calc_date"]);
                //mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                //mi.pass_prod_num = Convert.ToInt32(dtInfo.Rows[i]["pass_prod_num"]);
                mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["value"]);
                listDeviceProdNum.Add(mi);
            }
            return listDeviceProdNum;
        }
    }
}