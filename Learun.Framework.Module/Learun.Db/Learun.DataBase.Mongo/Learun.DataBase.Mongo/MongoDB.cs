using System;
using Learun.Cache.Mongo;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Learun.DataBase.Mongo
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬(Learun敏捷框架数据库小组)
    /// 日 期：2017.03.04
    /// 描 述：数据库操作类
    /// </summary>
    public class MongoDB : IMongoDB
    {
        #region  构造函数
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        public MongoDB(string connString)
        {
            getDBContext();
        }
        #endregion
        #region  属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public IMongoDatabase dbContext { get; set; }
        #endregion

        #region  属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public IMongoDatabase getDBContext()
        {
            MongoCache.InitDataBase();
            return dbContext = MongoCache.DbContext;
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T Read<T>(string clcName) where T : class
        {
            return MongoCache.GetModel<T>(clcName);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T ReadFirst<T>(string clcName) where T : class
        {
            return MongoCache.GetFirstModel<T>(clcName);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T Read<T>(string clcName, string time_stamp) where T : class
        {
            return MongoCache.GetModel<T>(clcName, time_stamp);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T ReadFirst<T>(string clcName, string time_stamp) where T : class
        {
            return MongoCache.GetFirstModel<T>(clcName, time_stamp);
        }
        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T ReadSortFirst<T>(string clcName, FilterDefinition<T> filter, SortDefinition<T> sort) where T : class
        {
            return MongoCache.GetSortFirstModel<T>(clcName, filter, sort);
        }
        #endregion
        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T ReadSortFirst<T>(string clcName, FilterDefinition<T> filter, SortDefinition<T> sort,ProjectionDefinition<T,T> Projection) where T : class
        {
            return MongoCache.GetSortFirstModel<T>(clcName, filter, sort, Projection);
        }
        #endregion
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T Read<T>(string clcName, FilterDefinition<T> filter) where T : class
        {
            return MongoCache.GetModel<T>(clcName, filter);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public T ReadFirst<T>(string clcName, FilterDefinition<T> filter) where T : class
        {
            return MongoCache.GetFirstModel<T>(clcName, filter);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public List<T> ReadMany<T>(string clcName, FilterDefinition<T> filter) where T : class
        {
            return MongoCache.GetManyModel<T>(clcName, filter);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public List<T> ReadMany<T>(string clcName, FilterDefinition<T> filter, out int total) where T : class
        {
            return MongoCache.GetManyModel<T>(clcName, filter, out total);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public List<T> ReadPage<T>(string clcName, FilterDefinition<T> filter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            return MongoCache.GetPageModel<T>(clcName, filter, orderField, isAsc, pageSize, pageIndex, out total);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public void Write<BsonDocument>(string clcName, BsonDocument bd) where BsonDocument : class
        {
            MongoCache.InsertOne<BsonDocument>(clcName, bd);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public void Write<BsonDocument>(string clcName, BsonDocument bd, DateTime expireTime) where BsonDocument : class
        {
            MongoCache.InsertOne<BsonDocument>(clcName, bd);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public void Write<BsonDocument>(string clcName, BsonDocument bd, TimeSpan timeSpan) where BsonDocument : class
        {
            MongoCache.InsertOne<BsonDocument>(clcName, bd);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public void Remove<BsonDocument>(string clcName, BsonDocument bd)
        {
            MongoCache.Remove<BsonDocument>(clcName, bd);
        }
        #endregion

        #region  属性
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public void RemoveAll<BsonDocument>(string clcName)
        {
            MongoCache.RemoveAll<BsonDocument>(clcName);
        }
        #endregion
    }
}
