using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_Desktop
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-11 11:32
    /// 描 述：设备运行参数
    /// </summary>
    public class RunParamService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_run_infoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t1.machine_name,
                t.act_spindle_speed_0,
                 t.act_spindle_override_0,
                 t.act_feed_speed_0,
                 t.act_feed_override_0,
                 t.main_prog_num,
                t.read_time
                ");
                strSql.Append("  FROM tb_run_info t ");
                strSql.Append("  LEFT JOIN tb_machine_info t1 ON t1.machine_id = t.machine_id ");
                strSql.Append("  WHERE t.run_state=1 and  1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.read_time >= @startTime AND t.read_time <= @endTime ) ");
                }
                if (!queryParam["machine_name"].IsEmpty())
                {
                    dp.Add("machine_name", "%" + queryParam["machine_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.machine_name Like @machine_name ");
                }
                return this.BaseRepository("BaseDb").FindList<tb_run_infoEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取tb_machine_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_machine_infoEntity Gettb_machine_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_machine_infoEntity>(t=>t.id == keyValue);
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
        /// 获取tb_run_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_run_infoEntity Gettb_run_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_run_infoEntity>(keyValue);
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
                var tb_run_infoEntity = Gettb_run_infoEntity(keyValue); 
                db.Delete<tb_machine_infoEntity>(t=>t.machine_id == tb_run_infoEntity.machine_id);
                db.Delete<tb_run_infoEntity>(t=>t.id == keyValue);
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
        public void SaveEntity(string keyValue, tb_run_infoEntity entity,tb_machine_infoEntity tb_machine_infoEntity)
        {
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var tb_run_infoEntityTmp = Gettb_run_infoEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<tb_machine_infoEntity>(t=>t.machine_id == tb_run_infoEntityTmp.machine_id);
                    tb_machine_infoEntity.Create();
                    tb_machine_infoEntity.machine_id = tb_run_infoEntityTmp.machine_id;
                    db.Insert(tb_machine_infoEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    tb_machine_infoEntity.Create();
                    tb_machine_infoEntity.machine_id = entity.machine_id;
                    db.Insert(tb_machine_infoEntity);
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
