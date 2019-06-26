using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Report
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2017-07-12 09:57
    /// 描 述：报表管理
    /// </summary>
    public class ReportTempBLL : ReportTempIBLL
    {
        private ReportTempService reportTempService = new ReportTempService();

        #region  获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public IEnumerable<ReportTempEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                return reportTempService.GetPageList(pagination, keyword);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="dataSourceId">数据库id</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataTable GetReportData(string dataSourceId, string strSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                    return new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获得报表数据-----------为班次产量统计专用
        /// </summary>
        /// <param name="dataSourceId">数据库id</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataTable GetReportData2(string dataSourceId,string strSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                { // return new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dt = new DataTable();
                    dt=new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dts = new DataTable();
                    dts = new DatabaseLinkBLL().FindTable("495b016e-d8ef-4796-adb3-152455b39ea0", 
                        "select ea.*,Convert(nvarchar(50),calc_date)+'-'+Convert(nvarchar(50),account_id)+'-'+Convert(nvarchar(50),machine_id) as  stage_emp_id, bu.F_RealName from tb_employee_access ea left join LR_Base_User bu on ea.employee_id=bu.f_account");
                    DataTable dtw = new DataTable();
                    dtw = new DatabaseLinkBLL().FindTable("601e7937-faee-4a2e-b588-6bad6ed334e6", 
                        "select Convert(nvarchar(50),calc_date)+'-'+Convert(nvarchar(50),wshift_id)+'-'+Convert(nvarchar(50),group_id) as  stage_emp_id,plan_wshift from tb_plan_wshift_data ");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string emp_id = dt.Rows[i]["人员"].ToString();
                        dt.Rows[i]["人员"] = "";
                        dt.Rows[i]["计划产量"] =0;
                        for (int j = 0; j < dts.Rows.Count; j++)
                        {
                            string stage_emp_id = dts.Rows[j]["stage_emp_id"].ToString();
                            if (emp_id== stage_emp_id)
                            {
                                dt.Rows[i]["人员"] += dts.Rows[j]["F_RealName"].ToString()+" ";
                            }
                        }
                        for (int l = 0; l < dtw.Rows.Count; l++)
                        {
                            string stage_emp_id = dtw.Rows[l]["stage_emp_id"].ToString();
                            if (emp_id == stage_emp_id)
                            {
                                dt.Rows[i]["计划产量"] = dtw.Rows[l]["plan_wshift"];
                            }
                        }
                    }
                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获得报表数据-----------为日产量统计专用
        /// </summary>
        /// <param name="dataSourceId">数据库id</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataTable GetReportData3(string dataSourceId, string strSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                { // return new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dt = new DataTable();
                    dt = new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dts = new DataTable();
                    dts = new DatabaseLinkBLL().FindTable("601e7937-faee-4a2e-b588-6bad6ed334e6", 
                        "select dd.plan_day,Convert(nvarchar(50),calc_date)+'-'+Convert(nvarchar(50),machine_id) as  stage_emp_id from tb_plan_day_data dd");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string emp_id = dt.Rows[i]["计划id"].ToString();
                        //dt.Rows[i]["计划id"] = "";
                        dt.Rows[i]["计划产量"] = 0;
                        for (int j = 0; j < dts.Rows.Count; j++)
                        {
                            string stage_emp_id = dts.Rows[j]["stage_emp_id"].ToString();
                            if (emp_id == stage_emp_id)
                            {
                                dt.Rows[i]["计划产量"] += dts.Rows[j]["plan_day"].ToString() + " ";
                            }
                        }
                    }
                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获得报表数据-----------为产量报表专用
        /// </summary>
        /// <param name="dataSourceId">数据库id</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataTable GetReportData4(string dataSourceId, string strSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                { // return new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dt = new DataTable();
                    dt = new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dt2 = new DataTable();
                    dt2 = new DatabaseLinkBLL().FindTable(dataSourceId, "select * from tb_plan_detail_data order by read_time desc");
                    DataTable dts = new DataTable();
                    dts = new DatabaseLinkBLL().FindTable("495b016e-d8ef-4796-adb3-152455b39ea0",
                        "select  tea.account_id,us.F_RealName,tea.machine_id,tea.calc_date"
                        + " from tb_employee_access tea"
                        + " inner join LR_Base_User us on convert(varchar, tea.employee_id) = us.F_Account"
                        + " where tea.machine_id = 10"
                        + " order by tea.calc_date desc");
                    DataTable dtr = new DataTable();
                    dtr.Columns.Add("生产时段", typeof(string));
                    dtr.Columns.Add("计划产量", typeof(string));
                    dtr.Columns.Add("生产产量", typeof(string));
                    dtr.Columns.Add("生产差异", typeof(string));
                    dtr.Columns.Add("操作人员", typeof(string));
                    dtr.Columns.Add("备注", typeof(string));
                    string abnormal = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string day= dt.Rows[i]["wshift_date"].ToString();
                        string day_time = dt.Rows[i]["day_time"].ToString();
                        string prod_num = dt.Rows[i]["prod_num"].ToString();
                        string plan_prod = null;
                        for (int j = 0; j < dts.Rows.Count; j++)
                        {
                            if (dts.Rows[j]["calc_date"].ToString().Equals(dt.Rows[i]["wshift_date"].ToString())
                                &&dts.Rows[j]["account_id"].ToString().Equals(dt.Rows[i]["wshift_id"].ToString()))
                            {
                                abnormal = dts.Rows[j]["F_RealName"].ToString();
                                break;
                            }
                          
                        }
                        for(int k = 0; k < dt2.Rows.Count; k++)
                        {
                            if (day.Equals(Convert.ToDateTime(dt2.Rows[k]["read_time"]).ToString("yyyyMMdd")))
                            {
                                if (day_time.Length == 2)
                                {
                                    plan_prod = dt2.Rows[0]["plan" + day_time + ""].ToString();
                                }
                                else
                                {
                                    plan_prod = dt2.Rows[0]["plan0" + day_time + ""].ToString();
                                }
                            }   
                        }                       
                        string diff_prod_num = (Convert.ToInt32(plan_prod) - Convert.ToInt32(prod_num)).ToString();
                        dtr.Rows.Add(day+"-"+day_time, plan_prod, prod_num, diff_prod_num, abnormal, null) ;
                        abnormal = null;
                    }
                    return dtr;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public DataTable GetReportDataOEE(string dataSourceId, string strSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                { // return new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dt = new DataTable();
                    dt = new DatabaseLinkBLL().FindTable(dataSourceId, strSql);
                    DataTable dtr = new DataTable();
                    dtr.Columns.Add("日期", typeof(string));
                    dtr.Columns.Add("OEE", typeof(string));
                    string day_time = null;
                    string OEE = null;
                  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        day_time = dt.Rows[i]["wshift_date"].ToString();
                        var qualified_no = Convert.ToDouble(dt.Rows[i]["qualified_no"].ToString().IsEmpty() ? "0": dt.Rows[i]["qualified_no"].ToString()) ;
                        var prod_num = Convert.ToDouble(dt.Rows[i]["prod_num"].ToString().IsEmpty()?"0": dt.Rows[i]["prod_num"].ToString());
                        var run_time_second = Convert.ToDouble(dt.Rows[i]["run_time_second"].ToString().IsEmpty()?"0": dt.Rows[i]["run_time_second"].ToString());
                        OEE = prod_num==0?"0":((qualified_no/prod_num)*(run_time_second/(24*60*60)*100)).ToString("0.00")+"%";
                        dtr.Rows.Add(day_time, OEE);
                    }
                    return dtr;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ReportTempEntity GetEntity(string keyValue)
        {
            try
            {
                return reportTempService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region  提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                reportTempService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ReportTempEntity entity)
        {
            try
            {
                reportTempService.SaveEntity(keyValue, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        public ReportTempEntity GetEntityByEnCode(string encode)
        {
            try
            {
              return   reportTempService.GetEntityByEnCode(encode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        public DataTable GetReportData(string dataSourceId, string strSql, string queryJson)
        {
            try
            { 
                if (!string.IsNullOrEmpty(strSql))
                    return new DatabaseLinkBLL().FindTable(dataSourceId, strSql, queryJson.ToJObject());
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}
