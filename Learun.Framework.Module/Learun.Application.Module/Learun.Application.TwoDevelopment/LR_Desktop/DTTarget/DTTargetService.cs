using Dapper;
 
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Learun.Application.Base.SystemModule;
using Learun.DataBase.Repository;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace Learun.Application.TwoDevelopment.LR_Desktop
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2018-09-21 16:35
    /// 描 述：统计配置
    /// </summary>
    public class DTTargetService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<DTTargetEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_Name,
                t.F_Icon,
                t.F_Url,
                t.F_Sort,
                t.F_Sql,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_CreateDate,
                t.F_Description,
                t.F_DataSourceId 

                ");
                strSql.Append("  FROM LR_DT_Target t ");
                strSql.Append("  WHERE 1=1 ");
                if (!string.IsNullOrEmpty(queryJson))
                {
                    var queryParam = queryJson?.ToJObject();
                    // 虚拟参数
                    var dp = new DynamicParameters(new { });
                    if (!queryParam["F_Name"].IsEmpty())
                    {
                        dp.Add("F_Name", "%" + queryParam["F_Name"].ToString() + "%", DbType.String);
                        strSql.Append(" AND t.F_Name Like @F_Name ");
                    }
                    if (!queryParam["F_Description"].IsEmpty())
                    {
                        dp.Add("F_Description", "%" + queryParam["F_Description"].ToString() + "%", DbType.String);
                        strSql.Append(" AND t.F_Description Like @F_Description ");
                    }

                     
                    return this.BaseRepository().FindList<DTTargetEntity>(strSql.ToString(), dp, pagination);
                }
                else
                {
                    return this.BaseRepository().FindList<DTTargetEntity>(strSql.ToString());

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

        /// <summary>
        /// 获取LR_DT_Target表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public DTTargetEntity GetLR_DT_TargetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<DTTargetEntity>(keyValue);
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

        internal double GetSqlData(string id)
        {
            try
            {
                int  run_state = 0;
                if (id == "0dac9e34-6e32-4137-8430-5833eb678397") run_state = 1;
                else if (id == "459247e3-7957-4cb3-912d-4a95da526644") run_state = 2 ;
                else if (id == "46235e3e-f3b7-48e3-a4ab-c5b159787785") run_state = 3;
                else if (id == "9b552fc3-28fc-4629-8abb-45a948de591b") run_state = 4;
                else if (id == "f4850d41-5458-40d1-9cf3-2d0ddb06d5c3") run_state = 0;
                DateTime dtime = DateTime.Now.AddSeconds(-10);
                var chartEntity = this.BaseRepository().FindEntity<DTTargetEntity>(p => p.F_Id == id);
                var databsseLinkEntity = this.BaseRepository().FindEntity<DatabaseLinkEntity>(p => p.F_DatabaseLinkId == chartEntity.F_DataSourceId);

                var reqtable = this.BaseRepository(databsseLinkEntity.F_DbConnection, databsseLinkEntity.F_DbType)
                    .FindTable(chartEntity.F_Sql);
                //return reqtable.Rows[0][0].ToDouble();

                JObject queryParam = new JObject();
                //queryParam.Add("machine_id", machine_id);
                queryParam.Add("time_second", dtime.Ticks.ToString());
                queryParam.Add("run_state", run_state);
                var data = this.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam);
                return data.Count.ToDouble(); 

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
                this.BaseRepository().Delete<DTTargetEntity>(t=>t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, DTTargetEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
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
