using Learun.Application.Report;
using Learun.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;
using MachineDesign.Models;

namespace Learun.Application.Web.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2017-07-12 09:57
    /// 描 述：报表管理
    /// </summary>
    public class ReportManageController : MvcControllerBase
    {
        private ReportTempIBLL reportTempIBLL = new ReportTempBLL();

        #region  视图功能
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 浏览页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Preview()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        #endregion

        #region  获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(string pagination, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = reportTempIBLL.GetPageList(paginationobj, keyword);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return JsonResult(jsonData);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEntity(string keyValue)
        {
            var data = reportTempIBLL.GetEntity(keyValue);
            return JsonResult(data);
        }
        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <param name="reportId">报表主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetReportData(string reportId)
        {
           
            ReportTempEntity reportEntity = reportTempIBLL.GetEntity(reportId);
            dynamic paramJson = reportEntity.F_ParamJson.ToJson();
             
                var data = new
                {
                    tempStyle = reportEntity.F_TempStyle,
                    chartType = reportEntity.F_TempType,
                    chartData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ChartSqlString.ToString()),
                    listData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ListSqlString.ToString())
                };
                return Content(data.ToJson());
            
        }
        /// <summary>
        /// 获取报表数据+查询条件
        /// </summary>
        /// <param name="reportId">报表主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetReportData1(string reportId, string queryJson)
        {

            ReportTempEntity reportEntity = reportTempIBLL.GetEntity(reportId);
            dynamic paramJson = reportEntity.F_ParamJson.ToJson();

            string sql = paramJson.F_ChartSqlString;
            string sql_chart = paramJson.F_ChartSqlString;
            JObject jo = (JObject)JsonConvert.DeserializeObject(queryJson);
            DateTime EndTime = jo["EndTime"].ToDate();
            DateTime StartTime = jo["StartTime"].ToDate();
            string machine_id = jo["machine_id"].ToString();
            string group_id = jo["group_id"].ToString();
            if (reportId == "40a0b5ef-9440-4ed9-97f8-1aec36642f0a")//小时产量统计
            {
                sql = "select sp.day_time as 小时,mi.group_name as 设备组,mi.machine_name as 设备号,sum(sp.prod_num) as 实际产量,"
                          + " sum(sp.pass_prod_num) as 合格产量"
                          + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup" 
                          + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                          + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = "+ machine_id + "and sp.group_id = " + group_id + ""
                          + " and sp.wshift_date= '" + EndTime.ToString("yyyyMMdd") + "'group by day_time,mi.group_name,mi.machine_name,calc_date " 
                          + " order by sp.calc_date, sp.day_time asc";

                sql_chart = "select sp.day_time as name,mi.group_name as 设备组,mi.machine_name as 设备号,sum(sp.prod_num) as value,sum(sp.plan_prod_num) as 计划产量,"
                          + " sum(sp.pass_prod_num) as 合格产量"
                          + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                          + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                          + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                          + " and sp.wshift_date= '" + EndTime.ToString("yyyyMMdd") + "'group by day_time,mi.group_name,mi.machine_name,calc_date " 
                          + " order by sp.calc_date, sp.day_time asc";
                         }
            else if (reportId == "f43e6528-4f04-454f-9922-ead91c938add") {//年产量统计
                sql = "select LEFT(sp.wshift_date, 4) as 年 ,mi.group_name as 设备组,"
                        + " mi.machine_name as 设备号, sum(sp.plan_prod_num) as 计划产量,sum(sp.prod_num) as 实际产量, sum(sp.pass_prod_num) as 合格产量"
                        + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                        + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                        + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id =  " + machine_id + "and sp.group_id = " + group_id + ""
                        + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                        + " group by LEFT(sp.wshift_date, 4),mi.machine_name,mi.group_name"
                        + " order by  LEFT(sp.wshift_date, 4) asc";

                sql_chart = "select LEFT(sp.wshift_date, 4) as name ,sum(sp.prod_num) as value, "
                        + " mi.machine_name as 设备号,mi.group_name as 设备组, sum(sp.plan_prod_num) as 计划产量,sum(sp.pass_prod_num) as 合格产量"
                        + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                        + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                        + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id =  " + machine_id + "and sp.group_id = " + group_id + ""
                        + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                        + " group by LEFT(sp.wshift_date, 4),mi.machine_name,mi.group_name"
                        + " order by name asc";
                     }
            else if (reportId == "522af585-5f4c-47fe-a3aa-0930754fb03b")//日产量统计
            {
                sql = "select sp.wshift_date as 日期, Convert(nvarchar(50),wshift_date)+'-'+Convert(nvarchar(50),mi.machine_id) as 计划id,mi.group_name as 设备组, "
                        + " mi.machine_name as 设备号,sum(sp.plan_prod_num) as 计划产量,sum(sp.prod_num) as 实际产量,sum(sp.pass_prod_num) as 合格产量"
                        + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                        + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                        + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                        + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                        + " group by sp.wshift_date,mi.machine_name,mi.machine_id,mi.group_name order by wshift_date asc";

                sql_chart = "select sp.wshift_date as name,sum(sp.prod_num) as value,"
                        + " mi.machine_name as 设备号,mi.group_name as 设备组, sum(sp.plan_prod_num) as 计划产量,sum(sp.pass_prod_num) as 合格产量"
                        + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                        + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                        + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                        + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                        + " group by sp.wshift_date,mi.machine_name,mi.group_name order by wshift_date asc";
                     }
            else if (reportId == "6ac82b75-c508-4237-8ec9-bae1df8066cb")//月产量统计
            {
                sql = "select LEFT(sp.wshift_date, 6) as 月份 , mi.group_name as 设备组, "
                       + " mi.machine_name as 设备号,sum(sp.plan_prod_num) as 计划产量,sum(sp.prod_num) as 实际产量,sum(sp.pass_prod_num) as 合格产量"
                       + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                       + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                       + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                       + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                       + " group by LEFT(sp.wshift_date, 6),mi.machine_name,mi.group_name"
                       + " order by LEFT(sp.wshift_date, 6) asc";

                sql_chart = "select LEFT(sp.wshift_date, 6) as name ,sum(sp.prod_num) as value, "
                       + " mi.machine_name as 设备号,mi.group_name as 设备组, sum(sp.plan_prod_num) as 计划产量,sum(sp.pass_prod_num) as 合格产量"
                       + " from tb_curday_prod_num sp inner join  tb_dict_parameter dp on sp.day_time = dp.data_setup"
                       + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                       + " where dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                       + " and sp.wshift_date <= '" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" + StartTime.ToString("yyyyMMdd") + "'"
                       + " group by LEFT(sp.wshift_date, 6),mi.machine_name,mi.group_name"
                       + " order by name asc";
                            }
            else if (reportId == "8060e1eb-5286-4c22-97bd-07ebde2d4cc8")//班次产量统计
            {
                sql = "select Convert(nvarchar(50),wshift_date)+'-'+Convert(nvarchar(50),sp.wshift_id)+'-'+Convert(nvarchar(50),mi.group_id) as 人员, sp.wshift_date+b.wshift_name as 班次,mi.group_name as 设备组, "
                       + " mi.machine_name as 设备号,sp.plan_prod_num as 计划产量,sp.prod_num as 实际产量,sp.pass_prod_num as 合格产量"
                       + " from tb_curday_prod_num sp inner join tb_shift_info b on sp.wshift_id=b.wshift_id "
                       + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                       + " where 1 = 1 and sp.is_stage = 'Shift' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                       + " and sp.wshift_date <='" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '" 
                       + StartTime.ToString("yyyyMMdd") + "'  order by wshift_date asc,b.start_time";

                sql_chart = "select  sp.wshift_date+b.wshift_name as name,sp.prod_num as value,"
                       + " mi.machine_name as 设备号,mi.group_name as 设备组, sp.plan_prod_num as 计划产量,sp.pass_prod_num as 合格产量"
                       + " from tb_curday_prod_num sp inner join tb_shift_info b on sp.wshift_id=b.wshift_id "
                       + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                       + " where 1 = 1 and sp.is_stage = 'Shift' and sp.machine_id = " + machine_id + "and sp.group_id = " + group_id + ""
                       + " and sp.wshift_date <='" + EndTime.ToString("yyyyMMdd") + "' AND sp.wshift_date >= '"
                       + StartTime.ToString("yyyyMMdd") + "'  order by wshift_date asc,b.start_time";
                            }
            else if (reportId == "8d76ac76-fe7a-4b7f-af71-1250851d11df")//设备状态统计
            {
                if (machine_id == "0")
                {
                    sql_chart = sql = "select a.*,  isnull(b.value, 0) as value    from("
                + " SELECT  1 as run_state, '运行' as name"
                + " union"
                + " SELECT  2 as run_state, '报警' as name"
                + " union"
                + " SELECT  3 as run_state, '暂停' as name"
                + " union"
                + " SELECT  4 as run_state, '空闲' as name"
                + " union"
                + " SELECT  0 as run_state, '停机' as name) a"
                + " left join("
                + " SELECT top 100 PERCENT run_state, case  when run_state=1 then '运行' when run_state=2 then '报警' "
                + " when run_state = 3 then '暂停' when run_state=4 then '空闲'  when run_state=0 then '停机' end as name,convert(varchar(50),"
                + " sum([run_dur])/60)  as value"
                + " FROM tb_run_state_seque where calc_date <='" + EndTime.ToString("yyyyMMdd") + "' AND calc_date >= '"
                + StartTime.ToString("yyyyMMdd") + "' and group_id =  " + group_id
                + " and run_state in  (0, 1, 2, 3, 4)"
                + " group by run_state"
                + " ) b on a.name = b.name"
                + " order by  case a.run_state"
                + " when '0' then 5"
                + " when '1' then 1"
                + " when '2' then 2"
                + " when '3' then 3"
                + " else 4"
                + " end";
                }
                else
                {
                    sql_chart = sql = "select a.*,  isnull(b.value, 0) as value    from("
                    + " SELECT  1 as run_state, '运行' as name"
                    + " union"
                    + " SELECT  2 as run_state, '报警' as name"
                    + " union"
                    + " SELECT  3 as run_state, '暂停' as name"
                    + " union"
                    + " SELECT  4 as run_state, '空闲' as name"
                    + " union"
                    + " SELECT  0 as run_state, '停机' as name) a"
                    + " left join("
                    + " SELECT top 100 PERCENT run_state, case  when run_state=1 then '运行' when run_state=2 then '报警' "
                    + " when run_state = 3 then '暂停' when run_state=4 then '空闲'  when run_state=0 then '停机' end as name,convert(varchar(50),"
                    + " sum([run_dur])/60)  as value"
                    + " FROM tb_run_state_seque where calc_date <='" + EndTime.ToString("yyyyMMdd") + "' AND calc_date >= '"
                    + StartTime.ToString("yyyyMMdd") + "' and machine_id =  " + machine_id
                    + " and run_state in  (0, 1, 2, 3, 4)"
                    + " group by run_state"
                    + " ) b on a.name = b.name"
                    + " order by  case a.run_state"
                    + " when '0' then 5"
                    + " when '1' then 1"
                    + " when '2' then 2"
                    + " when '3' then 3"
                    + " else 4"
                    + " end";
                }
            }
            else if (reportId == "7cca72d4-92f0-4e3f-82e4-6b47a087e414")//报警信息统计-按报警号
            {
                if (machine_id != null && machine_id != "0") {
                    sql_chart = sql = "select  alarm_no  as name,count(1) as value from tb_alarm_history_init where calc_date <='"
                        + EndTime.ToString("yyyyMMdd") + "' AND calc_date >= '" + StartTime.ToString("yyyyMMdd") + "' and machine_id =" 
                        + machine_id + " group by alarm_no ";
                }
                else
                    sql_chart = sql = "select  alarm_no  as name,count(1) as value from tb_alarm_history_init where calc_date <='"
                       + EndTime.ToString("yyyyMMdd") + "' AND calc_date >= '" + StartTime.ToString("yyyyMMdd") + "' group by alarm_no ";
            }
            //ac4baec2-0e81-470d-95ad-040ecca9cc5d&type=preview
              else if (reportId == "ac4baec2-0e81-470d-95ad-040ecca9cc5d")//报警信息统计-按设备
            {
                    sql_chart = sql = "select  mi.machine_name  as name,count(1) as value from tb_alarm_history_init sp"
                        + " left join vw_machine_info mi on sp.group_id = mi.group_id and mi.machine_id = sp.machine_id"
                        + " where calc_date <='"
                        + EndTime.ToString("yyyyMMdd") + "' AND calc_date >= '" + StartTime.ToString("yyyyMMdd") + "' and sp.group_id =" 
                        + group_id+ " group by mi.machine_name ";
            }
            else
            {
               /* //ZK
                JObject jo = (JObject)JsonConvert.DeserializeObject(queryJson);
                DateTime EndTime = jo["EndTime"].ToDate();
                DateTime StartTime = jo["StartTime"].ToDate();
                sql = " select sp.day_time as name," +
               " sp.group_id," +
               " sp.group_name," +
               " sp.prod_num as value," +
               " sp.plan_prod_num," +
               " sp.pass_prod_num" +
               "        from tb_curday_prod_num sp," +
               " tb_dict_parameter dp where 1 = 1 and sp.day_time = dp.data_setup" +
                       " and dp.param_type = 'DayTime' and dp.prw_type = 'ToOut' and sp.is_stage = 'Hour' and sp.machine_id = 11" +
                       " and sp.calc_date <='" + EndTime.ToString("yyyyMMdd") + "' AND sp.calc_date >= '" + StartTime.ToString("yyyyMMdd") + "' order by dp.data_addr asc," +
               " sp.day_time asc";
                //paramJson.F_ChartSqlString = sql;
                //paramJson.F_ListSqlString = sql;
                //END;*/
            }

            if (reportId == "8060e1eb-5286-4c22-97bd-07ebde2d4cc8")//班次产量统计
            {
                var data = new
                {
                    tempStyle = reportEntity.F_TempStyle,
                    chartType = reportEntity.F_TempType,
                    chartData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), sql_chart),
                    listData = reportTempIBLL.GetReportData2(paramJson.F_DataSourceId.ToString(), sql)
                };
                return Content(data.ToJson());
            }
            else if (reportId == "522af585-5f4c-47fe-a3aa-0930754fb03b")//日产量统计
            {
                var data = new
                {
                    tempStyle = reportEntity.F_TempStyle,
                    chartType = reportEntity.F_TempType,
                    chartData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), sql_chart),
                    listData = reportTempIBLL.GetReportData3(paramJson.F_DataSourceId.ToString(), sql)
                };
                return Content(data.ToJson());
            }
            else
            {
                var data = new
                {
                    tempStyle = reportEntity.F_TempStyle,
                    chartType = reportEntity.F_TempType,
                    chartData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), sql_chart),
                    listData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), sql)
                };
                return Content(data.ToJson());
            }
           

        }
        #endregion

        #region  提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly]
        public ActionResult SaveForm(string keyValue, ReportTempEntity entity)
        {
            reportTempIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            reportTempIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        #endregion
    }
}