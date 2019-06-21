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
    /// 日 期：2019-02-14 15:59
    /// 描 述：保养工单维护
    /// </summary>
    public class workOrderService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_plan_cycle_exe_dataEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t1.id,
                t.plan_maintain_name,
                t1.machine_id,
                t1.maintenance_id,
                t1.begin_date,
                t1.end_date,
                t1.executor,
                t1.complete_date,
                t1.complete_spec,
                t1.remark
                ");
                strSql.Append("  FROM tb_plan_cycle_data t ");
                strSql.Append("  RIGHT JOIN tb_plan_cycle_exe_data t1 ON t1.plan_maintain_id = t.id ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", "%" + queryParam["machine_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.machine_id Like @machine_id ");
                }
                if (!queryParam["maintenance_id"].IsEmpty())
                {
                    dp.Add("maintenance_id", "%" + queryParam["maintenance_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.maintenance_id Like @maintenance_id ");
                }
                if (!queryParam["begin_date"].IsEmpty())
                {
                    dp.Add("begin_date",queryParam["begin_date"].ToString(), DbType.String);
                    strSql.Append(" AND t1.begin_date = @begin_date ");
                }
                if (!queryParam["end_date"].IsEmpty())
                {
                    dp.Add("end_date",queryParam["end_date"].ToString(), DbType.String);
                    strSql.Append(" AND t1.end_date = @end_date ");
                }
                return this.BaseRepository("BaseDb").FindList<tb_plan_cycle_exe_dataEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取tb_plan_cycle_exe_data表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_plan_cycle_exe_dataEntity Gettb_plan_cycle_exe_dataEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_plan_cycle_exe_dataEntity>(t=>t.id == keyValue);
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
        /// 获取tb_plan_cycle_data表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_plan_cycle_dataEntity Gettb_plan_cycle_dataEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_plan_cycle_dataEntity>(keyValue);
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
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                var tb_plan_cycle_dataEntity = Gettb_plan_cycle_dataEntity(keyValue); 
                db.Delete<tb_plan_cycle_exe_dataEntity>(t=>t.plan_maintain_id == tb_plan_cycle_dataEntity.id);
                db.Delete<tb_plan_cycle_dataEntity>(t=>t.id == keyValue);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        public void SaveEntity(string keyValue, tb_plan_cycle_dataEntity entity,tb_plan_cycle_exe_dataEntity tb_plan_cycle_exe_dataEntity)
        {
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var tb_plan_cycle_exe_dataEntityTmp = Gettb_plan_cycle_exe_dataEntity(keyValue);
                    tb_plan_cycle_exe_dataEntity.Modify(keyValue);
                    db.Update(tb_plan_cycle_exe_dataEntity);
                    //db.Delete<tb_plan_cycle_exe_dataEntity>(t=>t.id == tb_plan_cycle_exe_dataEntityTmp.id);
                    //tb_plan_cycle_exe_dataEntity.Create();
                    //tb_plan_cycle_exe_dataEntity.id = tb_plan_cycle_exe_dataEntityTmp.id;
                    //db.Insert(tb_plan_cycle_exe_dataEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    tb_plan_cycle_exe_dataEntity.Create();
                    tb_plan_cycle_exe_dataEntity.id = entity.id;
                    db.Insert(tb_plan_cycle_exe_dataEntity);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
