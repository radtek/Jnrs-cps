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
    public class V_ReadData : DataTConnect
    {
        ILog log = LogManager.GetLogger("VT_ReadData");

        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();

        DateTime dtime = new DateTime();
        DateTime wtime = new DateTime();

        public bool initDBConnect()
        {
            return _initDBTConnect();
        }

        public void endDBConnect()
        {
            _endDBTConnect();
        }
        public List<RunStateNum> readDeviceRunNum(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(ReadParameter.WShiftOffsetHour);
            RunStateNum rsn = null;
            List<RunStateNum> listRunNum = new List<RunStateNum>(5);

            String strSql = "select count(1) as run_num,run_state from (select distinct tri.machine_id,tri.run_state, "
                            + " row_number() over(partition by tri.machine_id order by tri.read_time desc) as row_ord "
                            + " from tb_run_info tri,vw_machine_info vmi where 1=1 and tri.machine_id = vmi.machine_id "
                            + " and (vmi.show_numv = 'M' or (vmi.show_numv = 'G' and vmi.is_main = 'YES')) "
                            + " and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "'"
                            + " and tri.calc_date=" + wtime.ToString("yyyyMMdd") + ") t where t.row_ord = 1 group by run_state ";

            listRunNum.Clear();
            dtInfo = getStrSqlTData("readDeviceRunNum", strSql);

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

                if (rsn.run_state == RunStateParam.RunState)
                {
                    listRunNum[0] = rsn;
                }
                else if (rsn.run_state == RunStateParam.AlarmState)
                {
                    listRunNum[1] = rsn;
                }
                else if (rsn.run_state == RunStateParam.PauseState)
                {
                    listRunNum[2] = rsn;
                }
                else if (rsn.run_state == RunStateParam.ReadyState)
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
        public List<DeviceRealState> readDeviceRunState(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(ReadParameter.WShiftOffsetHour);
            DeviceRealState drs = null;
            List<DeviceRealState> listDeviceRealState = new List<DeviceRealState>(5);

            String strSql = "select vt.machine_id,vt.run_state,tmp.pic_name,tmp.pic_state,tmp.pic_path,tmp.rank_num,tmp.pheight,tmp.pwidth,tmp.ptop,tmp.pbottom,tmp.pleft,tmp.pright "
                            + " from (select tri.machine_id,tri.run_state,row_number() over(partition by tri.machine_id order by tri.read_time desc) as row_ord "
                            + "         from tb_run_info tri,vw_machine_info vmi where 1=1 and tri.machine_id = vmi.machine_id "
                            + "               and (vmi.show_numv = 'M' or (vmi.show_numv = 'G' and vmi.is_main = 'YES')) "
                            + "               and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "' and tri.calc_date=" + wtime.ToString("yyyyMMdd") + ") vt, "
                            + "              tb_machine_photo tmp where vt.machine_id = tmp.machine_id and vt.run_state = tmp.pic_state "
                            + "  and tmp.pic_kinds = 'RunState' and vt.row_ord = 1 ";

            listDeviceRealState.Clear();
            dtInfo = getStrSqlTData("readDeviceRunState", strSql);
            DataRow[] drRow = dtInfo.Select("1=1", "rank_num asc");

            for (int i = 0; i < drRow.Length; i++)
            {
                drs = new DeviceRealState();

                drs.machine_id = Convert.ToInt32(drRow[i]["machine_id"]);
                drs.run_state = Convert.ToInt32(drRow[i]["run_state"]);
                drs.pic_name = drRow[i]["pic_name"].ToString();
                drs.pic_path = drRow[i]["pic_path"].ToString();
                if (!Convert.IsDBNull(drRow[i]["pheight"]))
                {
                    drs.pic_height = Convert.ToInt32(drRow[i]["pheight"]);
                }
                else
                {
                    drs.pic_height = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pwidth"]))
                {
                    drs.pic_width = Convert.ToInt32(drRow[i]["pwidth"]);
                }
                else
                {
                    drs.pic_width = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["ptop"]))
                {
                    drs.pic_top = Convert.ToInt32(drRow[i]["ptop"]);
                }
                else
                {
                    drs.pic_top = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pbottom"]))
                {
                    drs.pic_bottom = Convert.ToInt32(drRow[i]["pbottom"]);
                }
                else
                {
                    drs.pic_bottom = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pleft"]))
                {
                    drs.pic_left = Convert.ToInt32(drRow[i]["pleft"]);
                }
                else
                {
                    drs.pic_left = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pright"]))
                {
                    drs.pic_right = Convert.ToInt32(drRow[i]["pright"]);
                }
                else
                {
                    drs.pic_right = 0;
                }
                
                drs.rank_num = Convert.ToInt32(drRow[i]["rank_num"]);

                listDeviceRealState.Add(drs);
            }

            return listDeviceRealState;
        }
        public List<DeviceRealState> readTDeviceRunState(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(ReadParameter.WShiftOffsetHour);
            DeviceRealState drs = null;
            List<DeviceRealState> listDeviceRealState = new List<DeviceRealState>(5);

            //String strSql = "select vt.machine_id,vt.run_state,tmp.pic_name,tmp.pic_state,tmp.pic_path,tmp.rank_num,tmp.pheight,tmp.pwidth,tmp.ptop,tmp.pbottom,tmp.pleft,tmp.pright "
            //                + " from (select tri.machine_id,tri.run_state,row_number() over(partition by tri.machine_id order by tri.read_time desc) as row_ord "
            //                + "         from tb_run_info tri,vw_machine_info vmi where 1=1 and tri.machine_id = vmi.machine_id "
            //                + "               and (vmi.show_numv = 'M' or (vmi.show_numv = 'G' and vmi.is_main = 'YES')) "
            //                + "               and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "' and tri.calc_date=" + wtime.ToString("yyyyMMdd") + ") vt, "
            //                + "              tb_machine_lphoto tmp where vt.machine_id = tmp.machine_id and vt.run_state = tmp.pic_state "
            //                + "  and tmp.pic_kinds = 'RunState' and vt.row_ord = 1 ";
            String strSql = "select machine_id from  tb_machine_info where mis_visual = '" + gis_numv + "'";
            listDeviceRealState.Clear();
            dtInfo = getStrSqlTData("readDeviceRunState", strSql).Copy();
            DataRow[] drRow = dtInfo.Select("1=1", "machine_id asc");

            for (int i = 0; i < drRow.Length; i++)
            {
                drs = new DeviceRealState();

                drs.machine_id = Convert.ToInt32(drRow[i]["machine_id"]);
                //drs.run_state = Convert.ToInt32(drRow[i]["run_state"]);
                //drs.pic_name = drRow[i]["pic_name"].ToString();
                //drs.pic_path = drRow[i]["pic_path"].ToString();

                JObject queryParam = new JObject();
                queryParam.Add("machine_id", drs.machine_id);
                RepositoryFactory BR = new RepositoryFactory();
                var run_state = BR.BaseRepository("MongoDB").FindMGStateList<BsonDocument>("clc_run_state", queryParam);
                drs.run_state = run_state["run_state"].AsInt32;
                drs.pic_path = getPic_path(drs.machine_id, drs.run_state);
                /*
                if (!Convert.IsDBNull(drRow[i]["pheight"]))
                {
                    drs.pic_height = Convert.ToInt32(drRow[i]["pheight"]);
                }
                else
                {
                    drs.pic_height = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pwidth"]))
                {
                    drs.pic_width = Convert.ToInt32(drRow[i]["pwidth"]);
                }
                else
                {
                    drs.pic_width = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["ptop"]))
                {
                    drs.pic_top = Convert.ToInt32(drRow[i]["ptop"]);
                }
                else
                {
                    drs.pic_top = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pbottom"]))
                {
                    drs.pic_bottom = Convert.ToInt32(drRow[i]["pbottom"]);
                }
                else
                {
                    drs.pic_bottom = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pleft"]))
                {
                    drs.pic_left = Convert.ToInt32(drRow[i]["pleft"]);
                }
                else
                {
                    drs.pic_left = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pright"]))
                {
                    drs.pic_right = Convert.ToInt32(drRow[i]["pright"]);
                }
                else
                {
                    drs.pic_right = 0;
                }

                drs.rank_num = Convert.ToInt32(drRow[i]["rank_num"]);
                */
                listDeviceRealState.Add(drs);
            }
            return listDeviceRealState;
        }

        public string getPic_path(int machine_id,int run_state)
        {
            String pic_path = "";
            string logMsg = "getPic_path";
            String strSql = "select pic_path from tb_machine_lphoto where machine_id = " + machine_id + "  and pic_state = " + run_state;

            try
            {
                dtInfo = getStrSqlTData(logMsg, strSql);

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    pic_path= dtInfo.Rows[i]["pic_path"].ToString();
                }
            }
            catch (Exception ep)
            {
                Console.WriteLine("状态图片读取失败！" + ep.Message);
                return pic_path;
            }
            return pic_path;
        }


        public List<DeviceRealState> readTRobDeviceRunState(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(ReadParameter.WShiftOffsetHour);
            DeviceRealState drs = null;
            List<DeviceRealState> listDeviceRealState = new List<DeviceRealState>(5);

            String strSql = "select vt.machine_id,vt.run_state,tmp.pic_name,tmp.pic_state,tmp.pic_path,tmp.rank_num,tmp.pheight,tmp.pwidth,tmp.ptop,tmp.pbottom,tmp.pleft,tmp.pright "
                            + " from (select tri.machine_id,tri.run_state,row_number() over(partition by tri.machine_id order by tri.read_time desc) as row_ord "
                            + "         from tb_run_info tri,vw_machine_info vmi where 1=1 and tri.machine_id = vmi.machine_id "
                            + "               and vmi.is_program = 'Rob' "
                            + "               and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "' and tri.calc_date=" + wtime.ToString("yyyyMMdd") + ") vt, "
                            + "              tb_machine_rphoto tmp where vt.machine_id = tmp.machine_id and vt.run_state = tmp.pic_state "
                            + "  and tmp.pic_kinds = 'RunState' and vt.row_ord = 1 ";

            listDeviceRealState.Clear();
            dtInfo = getStrSqlTData("readDeviceRunState", strSql);
            DataRow[] drRow = dtInfo.Select("1=1", "rank_num asc");

            for (int i = 0; i < drRow.Length; i++)
            {
                drs = new DeviceRealState();

                drs.machine_id = Convert.ToInt32(drRow[i]["machine_id"]);
                drs.run_state = Convert.ToInt32(drRow[i]["run_state"]);
                drs.pic_name = drRow[i]["pic_name"].ToString();
                drs.pic_path = drRow[i]["pic_path"].ToString();
                if (!Convert.IsDBNull(drRow[i]["pheight"]))
                {
                    drs.pic_height = Convert.ToInt32(drRow[i]["pheight"]);
                }
                else
                {
                    drs.pic_height = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pwidth"]))
                {
                    drs.pic_width = Convert.ToInt32(drRow[i]["pwidth"]);
                }
                else
                {
                    drs.pic_width = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["ptop"]))
                {
                    drs.pic_top = Convert.ToInt32(drRow[i]["ptop"]);
                }
                else
                {
                    drs.pic_top = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pbottom"]))
                {
                    drs.pic_bottom = Convert.ToInt32(drRow[i]["pbottom"]);
                }
                else
                {
                    drs.pic_bottom = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pleft"]))
                {
                    drs.pic_left = Convert.ToInt32(drRow[i]["pleft"]);
                }
                else
                {
                    drs.pic_left = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pright"]))
                {
                    drs.pic_right = Convert.ToInt32(drRow[i]["pright"]);
                }
                else
                {
                    drs.pic_right = 0;
                }

                drs.rank_num = Convert.ToInt32(drRow[i]["rank_num"]);

                listDeviceRealState.Add(drs);
            }

            return listDeviceRealState;
        }
        public List<DeviceRealState> readDeviceRunState(int run_state, string gis_numv, string mis_numv)
        {
            DeviceRealState drs = null;
            List<DeviceRealState> listDeviceRealState = new List<DeviceRealState>(5);

            String strSql = "select vmi.machine_id,tmp.pic_state as run_state,tmp.pic_name,tmp.pic_state,tmp.pic_path,tmp.rank_num, "
                            + "     tmp.pheight,tmp.pwidth,tmp.ptop,tmp.pbottom,tmp.pleft,tmp.pright "
                            + " from vw_machine_info vmi left join tb_machine_photo tmp on vmi.machine_id = tmp.machine_id "
                            + "  and tmp.pic_kinds = 'RunState' and tmp.pic_state = " + RunStateParam.StopState
                            + " where 1=1 and (vmi.show_numv = 'M' or (vmi.show_numv = 'G' and vmi.is_main = 'YES')) "
                            + "   and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "'";

            listDeviceRealState.Clear();
            dtInfo = getStrSqlTData("readDeviceRunState", strSql);
            DataRow[] drRow = dtInfo.Select("1=1", "rank_num asc");

            for (int i = 0; i < drRow.Length; i++)
            {
                drs = new DeviceRealState();

                drs.machine_id = Convert.ToInt32(drRow[i]["machine_id"]);
                drs.run_state = Convert.ToInt32(drRow[i]["run_state"]);
                drs.pic_name = drRow[i]["pic_name"].ToString();
                drs.pic_path = drRow[i]["pic_path"].ToString();
                if (!Convert.IsDBNull(drRow[i]["pheight"]))
                {
                    drs.pic_height = Convert.ToInt32(drRow[i]["pheight"]);
                }
                else
                {
                    drs.pic_height = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pwidth"]))
                {
                    drs.pic_width = Convert.ToInt32(drRow[i]["pwidth"]);
                }
                else
                {
                    drs.pic_width = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["ptop"]))
                {
                    drs.pic_top = Convert.ToInt32(drRow[i]["ptop"]);
                }
                else
                {
                    drs.pic_top = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pbottom"]))
                {
                    drs.pic_bottom = Convert.ToInt32(drRow[i]["pbottom"]);
                }
                else
                {
                    drs.pic_bottom = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pleft"]))
                {
                    drs.pic_left = Convert.ToInt32(drRow[i]["pleft"]);
                }
                else
                {
                    drs.pic_left = 0;
                }
                if (!Convert.IsDBNull(drRow[i]["pright"]))
                {
                    drs.pic_right = Convert.ToInt32(drRow[i]["pright"]);
                }
                else
                {
                    drs.pic_right = 0;
                }
                drs.rank_num = Convert.ToInt32(drRow[i]["rank_num"]);

                listDeviceRealState.Add(drs);
            }

            return listDeviceRealState;
        }
        /*实时产量读取*/
        public List<MachineInfo> readRealProdNum(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(-8);

            MachineInfo mi = null;
            List<int> listGroupId = new List<int>();
            List<MachineInfo> listRealProdNum = new List<MachineInfo>();

            String strSql = " select group_id,group_name,sum(prod_num) as prod_num,plan_prod_num,sum(pass_prod_num) as pass_prod_num from ( "
                            + " select mi.group_id,mi.group_name,mi.machine_id,rd.prod_num,rd.plan_prod_num,rd.pass_prod_num, "
                            + "        rd.read_time,convert(time,rd.read_time) as long_time "
                            + "   from tb_curday_prod_num rd,vw_machine_info mi where 1=1 and rd.machine_id = mi.machine_id and mi.is_prod = 'SXL' "
                            + "    and mi.mis_visual in (" + mis_numv + ") and mi.gis_visual = '" + gis_numv + "'"
                            + "    and rd.is_stage = 'Day' and rd.calc_date = '" + wtime.ToString("yyyyMMdd") + "' "
                            + "   ) vt group by group_id,group_name,plan_prod_num ";

            dtInfo = getStrSqlTData("readRealProdNum", strSql).Copy();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                mi.plan_prod_num = Convert.ToInt32(dtInfo.Rows[i]["plan_prod_num"]);
                mi.pass_prod_num = Convert.ToInt32(dtInfo.Rows[i]["pass_prod_num"]);
                if (mi.prod_num == 0)
                {
                    mi.prod_rate = 0.0;
                }
                else
                {
                    mi.prod_rate = Math.Round((mi.prod_num * 100.0 / mi.plan_prod_num), 2);
                }
                listRealProdNum.Add(mi);
            }
            return listRealProdNum;
        }
        /*实时产量读取*/
        public List<MachineInfo> readTRealProdNum(string gis_numv, string mis_numv, string mac_prog)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(-8);

            MachineInfo mi = null;
            List<int> listGroupId = new List<int>();
            List<MachineInfo> listRealProdNum = new List<MachineInfo>();

            String strSql = " select group_id,group_name,sum(prod_num) as prod_num,plan_prod_num,sum(npass_prod_num) as pass_prod_num from ( "
                            + " select mi.group_id,mi.group_name,mi.machine_id,rd.prod_num,rd.plan_prod_num,rd.npass_prod_num, "
                            + "        rd.read_time,convert(time,rd.read_time) as long_time "
                            + "   from tb_curday_prod_num rd,vw_machine_info mi where 1=1 and rd.machine_id = mi.machine_id and mi.is_prod = 'SXL' "
                            + "    and mi.is_program = '" + mac_prog + "' and mi.mis_visual in (" + mis_numv + ") "
                            + "    and rd.is_stage = 'Hour' and rd.wshift_date = '" + wtime.ToString("yyyyMMdd") + "' "
                            + "   ) vt group by group_id,group_name,plan_prod_num ";

            dtInfo = getStrSqlTData("readRealProdNum", strSql).Copy();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                mi.plan_prod_num = Convert.ToInt32(dtInfo.Rows[i]["plan_prod_num"]);
                mi.pass_prod_num = Convert.ToInt32(dtInfo.Rows[i]["pass_prod_num"]);
                if (mi.prod_num == 0 || mi.plan_prod_num==0)
                {
                    mi.prod_rate = 0.0;
                }
                else
                {
                    mi.prod_rate = Math.Round((mi.prod_num * 100.0 / mi.plan_prod_num), 2);
                }
                listRealProdNum.Add(mi);
            }
            return listRealProdNum;
        }
        /*实时产量读取*/
        public List<MachineInfo> readRealProdMTotNum(string gis_numv, string mis_numv)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(-8);
            MachineInfo mi = null;
            List<int> listGroupId = new List<int>();
            List<MachineInfo> listRealProdNum = new List<MachineInfo>();
            DateTime mOneDay = (new DateTime(wtime.Year, wtime.Month, 1));
            DateTime nOneDay = mOneDay.AddMonths(1).AddDays(-1);

            String strSql = " select calc_date,group_id,group_name,sum(prod_num) as prod_num,sum(plan_prod_num) as plan_prod_num,sum(pass_prod_num) as pass_prod_num from ( "
                            + "  select rd.calc_date,mi.group_id,mi.group_name,rd.wshift_id,rd.machine_id,rd.prod_num,rd.plan_prod_num,rd.pass_prod_num,read_time "
                            + "   from tb_curday_prod_num rd,vw_machine_info mi where 1=1 and rd.machine_id = mi.machine_id and mi.is_prod = 'SXL' "
                            + "    and rd.is_stage = 'Day' and mis_visual in (" + mis_numv + ") and gis_visual = '" + gis_numv + "'"
                            + "    and rd.calc_date >= '" + mOneDay.ToString("yyyyMMdd") + "' and rd.calc_date <= '" + nOneDay.ToString("yyyyMMdd") + "'"
                            + "   ) vt group by calc_date,group_id,group_name ";

            dtInfo = getStrSqlTData("readRealProdMTotNum", strSql).Copy();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.calc_date = Convert.ToInt32(dtInfo.Rows[i]["calc_date"]);
                mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                mi.plan_prod_num = Convert.ToInt32(dtInfo.Rows[i]["plan_prod_num"]);
                mi.pass_prod_num = Convert.ToInt32(dtInfo.Rows[i]["pass_prod_num"]);
                if (mi.prod_num == 0)
                {
                    mi.prod_rate = 0.0;
                }
                else
                {
                    mi.prod_rate = Math.Round((mi.prod_num * 100.0 / mi.plan_prod_num), 2);
                }
                listRealProdNum.Add(mi);
            }
            
            return listRealProdNum;
        }
        /*实时产量读取*/
        public List<MachineInfo> readTRealProdMTotNum(string gis_numv, string mis_numv, string mac_prog)
        {
            dtime = DateTime.Now;
            wtime = DateTime.Now.AddHours(-8);
            MachineInfo mi = null;
            List<int> listGroupId = new List<int>();
            List<MachineInfo> listRealProdNum = new List<MachineInfo>();
            DateTime mOneDay = (new DateTime(wtime.Year, wtime.Month, 1));
            DateTime nOneDay = mOneDay.AddMonths(1).AddDays(-1);

            String strSql = " select calc_date,group_id,group_name,sum(prod_num) as prod_num,sum(plan_prod_num) as plan_prod_num,sum(npass_prod_num) as pass_prod_num from ( "
                            + "  select rd.calc_date,mi.group_id,mi.group_name,rd.wshift_id,rd.machine_id,rd.prod_num,rd.plan_prod_num,rd.npass_prod_num,read_time "
                            + "   from tb_curday_prod_num rd,vw_machine_info mi where 1=1 and rd.machine_id = mi.machine_id and mi.is_prod = 'SXL' "
                            + "    and rd.is_stage = 'Hour' and mi.is_program = '" + mac_prog + "' and mis_visual in (" + mis_numv + ")"
                            + "    and rd.calc_date >= '" + mOneDay.ToString("yyyyMMdd") + "' and rd.calc_date <= '" + nOneDay.ToString("yyyyMMdd") + "'"
                            + "   ) vt group by calc_date,group_id,group_name ";

            dtInfo = getStrSqlTData("readRealProdMTotNum", strSql).Copy();

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                mi = new MachineInfo();

                mi.calc_date = Convert.ToInt32(dtInfo.Rows[i]["calc_date"]);
                mi.group_id = Convert.ToInt32(dtInfo.Rows[i]["group_id"]);
                mi.group_name = dtInfo.Rows[i]["group_name"].ToString();
                mi.prod_num = Convert.ToInt32(dtInfo.Rows[i]["prod_num"]);
                mi.plan_prod_num = Convert.ToInt32(dtInfo.Rows[i]["plan_prod_num"]);
                mi.pass_prod_num = Convert.ToInt32(dtInfo.Rows[i]["pass_prod_num"]);
                if (mi.prod_num == 0)
                {
                    mi.prod_rate = 0.0;
                }
                else
                {
                    mi.prod_rate = Math.Round((mi.prod_num * 100.0 / mi.plan_prod_num), 2);
                }
                listRealProdNum.Add(mi);
            }

            return listRealProdNum;
        }
        /*读取班次信息*/
        public int readShiftData(string gis_numv, string mis_numv, string shift_no)
        {
            int num = 0;

            string strSql = "select top 1 va.data_value from tb_machine_param va, vw_machine_info vmi,tb_dict_parameter tdp "
                            + " where va.machine_id = vmi.machine_id and va.param_id = tdp.param_id "
                            + "   and vmi.mis_visual in (" + mis_numv + ") and vmi.gis_visual = '" + gis_numv + "' "
                            + "   and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "' "
                            + "   and tdp.param_name = '" + shift_no + "' ";

            dtInfo = getStrSqlTData("readShiftInfo", strSql);

            if (dtInfo == null || dtInfo.Rows.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                num = Convert.ToInt32(dtInfo.Rows[i]["data_value"]);
            }
            return num;
        }
        /*读取班次信息*/
        public int readShiftData(string gis_numv, string mis_numv, string mac_prog, string shift_no)
        {
            int num = 0;

            string strSql = "select top 1 va.data_value from tb_machine_param va, vw_machine_info vmi,tb_dict_parameter tdp "
                            + " where va.machine_id = vmi.machine_id and va.param_id = tdp.param_id "
                            + "   and vmi.is_program = '" + mac_prog + "' and vmi.mis_visual in (" + mis_numv + ") "
                            + "   and vmi.gis_visual = '" + gis_numv + "' and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "' "
                            + "   and tdp.param_name = '" + shift_no + "' ";

            dtInfo = getStrSqlTData("readShiftInfo", strSql);

            if (dtInfo == null || dtInfo.Rows.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                num = Convert.ToInt32(dtInfo.Rows[i]["data_value"]);
            }
            return num;
        }
        public ProductInfo readRealProdNo(string gis_numv, string mis_numv, string mac_prod)
        {
            ProductInfo data = null;
            if (initDBConnect())
            {
                int prod_class = readProductClass(gis_numv, mis_numv, mac_prod);
                data = readProductInfo(gis_numv, mis_numv, prod_class);
            }

            return data;
        }
        public int readProductClass(string gis_numv, string mis_numv, string mac_prog)
        {
            int prod_class = 0;

            string strSql = "select top 1 va.wshift_prod_id as data_value from tb_curday_prod_num va, vw_machine_info vmi "
                            + " where va.machine_id = vmi.machine_id "
                            + "   and vmi.is_program = '" + mac_prog + "' and vmi.mis_visual in (" + mis_numv + ") order by va.read_time desc ";

            //exe write
            try
            {
                dtInfo = getStrSqlTData("readProductClass", strSql);

                if (dtInfo.Rows.Count > 0 && !Convert.IsDBNull(dtInfo.Rows[0]["data_value"]))
                {
                    prod_class = Convert.ToInt32(dtInfo.Rows[0]["data_value"]);
                }
            }
            catch (SystemException ep)
            {
                log.Error("产品类别读取错误!ID:" + gis_numv + "\n" + ep.Message);
            }
            return prod_class;
        }
        public ProductInfo readProductInfo(string gis_numv, string mis_numv, int prod_class)
        {
            string strSql = "";
            ProductInfo pi = new ProductInfo();

            if (gis_numv.Equals("V_A")|| gis_numv.Equals("V_C"))
            {
                strSql = " select product_id,product_no,product_name from tb_product_info where product_no = '" + ReadParameter.ProductNo_065J + "'";
            }
            else if (gis_numv.Equals("V_B")||gis_numv.Equals("V_D"))
            {
                if (prod_class == 24)
                {
                    strSql = " select product_id,product_no,product_name from tb_product_info where product_no = '" + ReadParameter.ProductNo_066F + "'";
                }
                else
                {
                    strSql = " select product_id,product_no,product_name from tb_product_info where product_no = '" + ReadParameter.ProductNo_066B + "'";
                }
            }
            else
            {
                strSql = " select top 1 product_id,product_no,product_name from tb_product_info ";
            }

            //exe write
            try
            {
                dtInfo = getStrSqlTData("readProductInfo", strSql);

                if (dtInfo.Rows.Count > 0 && !Convert.IsDBNull(dtInfo.Rows[0]["product_id"]))
                {
                    pi.product_id = Convert.ToInt32(dtInfo.Rows[0]["product_id"]);
                    pi.product_no = dtInfo.Rows[0]["product_no"].ToString();
                    pi.product_name = dtInfo.Rows[0]["product_name"].ToString();
                }
            }
            catch (SystemException ep)
            {
                log.Error("产品ID读取错误!ID:" + gis_numv + "\n" + ep.Message);
            }
            return pi;
        }
    }
}