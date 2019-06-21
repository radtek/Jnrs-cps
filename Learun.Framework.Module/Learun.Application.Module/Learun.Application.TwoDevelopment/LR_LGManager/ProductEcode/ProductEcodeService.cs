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
    /// 日 期：2019-01-08 13:48
    /// 描 述：二维码追溯
    /// </summary>
    public class ProductEcodeService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_product_hisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                /*strSql.Append("SELECT ");
                strSql.Append(@"
                t.time_stamp,
                t.product_ecode,
                t.product_id,
                t.stage_name,
                t.calc_date,
                t.stage_time
                ");
                strSql.Append("  FROM tb_product_his t ");
                strSql.Append("  WHERE 1=1 ");*/
                strSql.Append(@" select top 100 percent a.calc_date,a.product_ecode,a.stage_time,
                b.product_no as product_id,
                --c.emp_name as stage_emp_id,
                a.calc_date+'-'+Convert(nvarchar(50),wshift_id) as  stage_emp_id,
                d.machine_name as stage_mac_id,
                e.group_name as stage_group_id
                from tb_product_his a
                inner join tb_product_info b on a.product_id = b.product_id
                --inner join tb_employee_info c on a.stage_emp_id = c.employee_id
                inner join tb_machine_info d on a.stage_mac_id = d.machine_id
                inner join tb_macgroup_info e on a.stage_group_id = e.group_id");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( a.stage_time >= @startTime AND a.stage_time <= @endTime ) ");
                    if (!queryParam["keyword"].IsEmpty())
                    {
                        dp.Add("keyword", queryParam["keyword"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.product_ecode = @keyword) ");
                    }
                    if (!queryParam["group_id"].IsEmpty())
                    {
                        dp.Add("group_id", queryParam["group_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_group_id = @group_id) ");
                    }
                    if (!queryParam["machine_id"].IsEmpty() && queryParam["machine_id"].ToString() != "0")
                    {
                        dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_mac_id = @machine_id) ");
                    }
                }
                strSql.Append(" order by a.stage_time desc ");
                return this.BaseRepository("BaseDb1").FindList<tb_product_hisEntity>(strSql.ToString(),dp, pagination);
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

        /// <summary>
        /// 获取tb_product_his表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_product_hisEntity Gettb_product_hisEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_product_hisEntity>(keyValue);
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

        #endregion

        #region  提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository("BaseDb").Delete<tb_product_hisEntity>(t=>t.id == keyValue);
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

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, tb_product_hisEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository("BaseDb").Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository("BaseDb").Insert(entity);
                }
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

        #endregion

    }
}
