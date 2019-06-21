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
    /// 日 期：2019-01-10 16:11
    /// 描 述：erweima
    /// </summary>
    public class EcodeService : RepositoryFactory
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
                strSql.Append(@" t.calc_date,  t.product_id,  t.product_ecode,  t.stage_name,  t.stage_mac_id,  t.stage_time ");
                strSql.Append(@"  FROM tb_product_his t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["product_ecode"].IsEmpty())
                {
                    dp.Add("product_ecode", "%" + queryParam["product_ecode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_ecode Like @product_ecode ");
                }
                if (!queryParam["stage_time"].IsEmpty())
                {
                    dp.Add("stage_time", "%" + queryParam["stage_time"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.stage_time Like @stage_time ");
                }
                return this.BaseRepository("BaseDb").FindTable(strSql.ToString(),dp);
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

