using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-06-04 14:58
    /// 描 述：报表测试
    /// </summary>
    public class machineinfo_exportService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取报表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT  ");
                strSql.Append(@" t.machine_id,  t.group_id,  t.machine_name,  t.machine_series,  t.machine_number,  t.comm_protocol,  t.comm_interface,  t.rank_num,  t.category,  t.sets_no,  t.is_run_state,  t.is_prod,  t.run_param,  t.is_alarm,  t.is_program,  t.is_barcode,  t.mis_visual,  t.station_cnt,  t.rank_sets,  t.is_03,  t.is_04,  t.is_05,  t.is_06,  t.is_07,  t.is_08,  t.is_09,  t.is_main,  t.price,  t.manufacture,  t.born_date,  t.buy_person,  t.photo,  t.enabled,  t.memo ");
                strSql.Append(@"  FROM (select * from tb_machine_info)t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["machine_name"].IsEmpty())
                {
                    dp.Add("machine_name", "%" + queryParam["machine_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.machine_name Like @machine_name ");
                }
                return this.BaseRepository("BaseDb1").FindTable(strSql.ToString(),dp);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

    }
}
        #endregion 

