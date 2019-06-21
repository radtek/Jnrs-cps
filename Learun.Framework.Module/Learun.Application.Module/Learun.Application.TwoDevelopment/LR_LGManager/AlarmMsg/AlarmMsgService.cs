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
    /// 日 期：2019-01-09 09:56
    /// 描 述：报警信息汇总
    /// </summary>
    public class AlarmMsgService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_alarm_historyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.time_stamp,
                t.calc_date,
                t1.machine_name,
                t.alarm_no,
                --t2.alarm_text as alarm_msg,
                t.alarm_msg,
                t.start_time,
                t.end_time,
                t.read_time
                ");
                strSql.Append("  FROM tb_alarm_history_init t ");
                strSql.Append("  LEFT JOIN tb_machine_info t1 ON t1.machine_id = t.machine_id ");
               // strSql.Append("  LEFT JOIN tb_alarm_info t2 ON t.alarm_msg = t2.alarm_no ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.read_time >= @startTime AND t.read_time <= @endTime ) ");
                    if (!queryParam["group_id"].IsEmpty())
                    {
                        dp.Add("group_id", queryParam["group_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( t.group_id = @group_id) ");
                    }
                    if (!queryParam["machine_id"].IsEmpty() && queryParam["machine_id"].ToString() != "0")
                    {
                        dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( t.machine_id = @machine_id) ");
                    }
                }
                return this.BaseRepository("BaseDb1").FindList<tb_alarm_historyEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_alarm_historyEntity> GetList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.calc_date,
                t1.machine_name,
                t.alarm_no,
                t.alarm_msg,
                t.start_time,
                t.end_time,
                t.read_time
                ");
                strSql.Append("  FROM tb_alarm_history_init t ");
                strSql.Append("  LEFT JOIN tb_machine_info t1 ON t1.machine_id = t.machine_id ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    dp.Add("machine_name", queryParam["machine_name"].ToString(), DbType.String);
                    strSql.Append(" AND ( t.read_time >= @startTime AND t.read_time <= @endTime ) ");
                    strSql.Append(" AND ( t1.machine_name = @machine_name) ");
                }
                return this.BaseRepository("BaseDb").FindList<tb_alarm_historyEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取tb_alarm_history表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_alarm_historyEntity Gettb_alarm_historyEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_alarm_historyEntity>(keyValue);
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
                var tb_alarm_historyEntity = Gettb_alarm_historyEntity(keyValue); 
                db.Delete<tb_machine_infoEntity>(t=>t.machine_id == tb_alarm_historyEntity.machine_id);
                db.Delete<tb_alarm_historyEntity>(t=>t.id == keyValue);
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
        public void SaveEntity(string keyValue, tb_alarm_historyEntity entity,tb_machine_infoEntity tb_machine_infoEntity)
        {
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var tb_alarm_historyEntityTmp = Gettb_alarm_historyEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<tb_machine_infoEntity>(t=>t.machine_id == tb_alarm_historyEntityTmp.machine_id);
                    tb_machine_infoEntity.Create();
                    tb_machine_infoEntity.machine_id = tb_alarm_historyEntityTmp.machine_id;
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
