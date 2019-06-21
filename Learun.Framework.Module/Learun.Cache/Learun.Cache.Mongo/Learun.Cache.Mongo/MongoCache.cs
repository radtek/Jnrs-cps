using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using Learun.Util;
using Newtonsoft.Json.Linq;

namespace Learun.Cache.Mongo
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬
    /// 日 期：2017.03.03
    /// 描 述：redis操作方法
    /// </summary>
    public class MongoCache
    {
        #region Fields
        /// <summary>
        /// 缓存Mongodb集合
        /// </summary>
        private Dictionary<string, object> _collectionsMongoDb;
        #endregion
        
        #region  -- 连接信息 --
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static MongoConfigInfo mongoConfigInfo = MongoConfigInfo.GetConfig();
        /// <summary>
        /// Mongo客户端设置
        /// </summary>
        public static MongoClientSettings Settings { get; set; }
        /// <summary>
        /// Mongo上下文 
        /// </summary>
        public static IMongoDatabase DbContext { get; set; }
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static IMongoDatabase CreateManager(string dbname = "")
        {
            var connString = mongoConfigInfo.ConnectionString;
            if (string.IsNullOrWhiteSpace(connString))
                throw new ArgumentException("connectionString 连接字符串不能为空!");

            try
            {
                var mongoUrl = new MongoUrl(connString);
                Settings = MongoClientSettings.FromUrl(mongoUrl);

                var isDataBase = string.IsNullOrWhiteSpace(mongoUrl.DatabaseName);
                if (isDataBase && string.IsNullOrEmpty(dbname))
                    throw new ArgumentException("数据库名称不能为空！");
                
                // SSL加密
                if (Settings.UseSsl)
                {
                    Settings.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = SslProtocols.Tls12
                    };
                }

                var mongoClient = new MongoClient(Settings);
                DbContext = mongoClient.GetDatabase(mongoConfigInfo.MongoDB);
            }
            catch
            {
                throw;
            }
            return DbContext;
        }
        /// <summary>
        /// 获取集合对象
        /// </summary>
        /// <returns></returns>
        public static void InitDataBase(string dbName = "")
        {
            DbContext = GetClientManager(dbName);
        }
        /// <summary>
        /// 获取集合对象
        /// </summary>
        /// <returns></returns>
        public static IMongoCollection<T> GetCollection<T>(string clcName)
        {
            return DbContext.GetCollection<T>(clcName);
        }
        /// <summary>
        /// 异步获取集合
        /// </summary>
        /// <returns></returns>
        public async Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>() where TEntity : class
        {
            // 集合缓存如果为空，那么创建一个
            if (_collectionsMongoDb == null)
            {
                _collectionsMongoDb = new Dictionary<string, object>();
            }

            // 获取集合名称，使用的标准是在实体类型名后添加Set
            var collectionName = $"{typeof(TEntity).Name}Set";

            // 如果集合不存在，那么创建集合
            if (false == await IsCollectionExistsAsync<TEntity>())
            {
                await DbContext.CreateCollectionAsync(collectionName);
            }

            // 如果缓存中没有该集合，那么加入缓存
            if (!_collectionsMongoDb.ContainsKey(collectionName))
            {
                _collectionsMongoDb[collectionName] = DbContext.GetCollection<TEntity>(collectionName);
            }

            // 从缓存中取出集合返回
            return (IMongoCollection<TEntity>)_collectionsMongoDb[collectionName];
        }

        /// <summary>
        /// 集合是否存在
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<bool> IsCollectionExistsAsync<TEntity>()
        {
            var filter = new BsonDocument("name", $"{typeof(TEntity).Name}Set");
            // 通过集合名称过滤
            var collections = await DbContext.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            // 检查是否存在
            return await collections.AnyAsync();
        }
        /// <summary>
        /// 字串转数组
        /// </summary>
        /// <param name="strSource">字串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        /// <summary>
        /// 获取Mongo客户端
        /// </summary>
        /// <param name="dbname">Mongo库名称</param>
        /// <returns></returns>
        private static IMongoDatabase GetClientManager(string dbname = "")
        {
            return CreateManager(dbname);
        }
        #endregion
        
        #region  -- Item --
        /// <summary>
        /// 新增单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="t">值</param>
        /// <param name="dbname">库名称</param>
        /// <returns></returns>
        public static bool InsertOne<T>(string collectionName, T t)
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                collection.InsertOne(t);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 新增多个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="t">值</param>
        /// <param name="dbname">库名称</param>
        /// <returns></returns>
        public static bool InsertMany<T>(string collectionName, List<T> list)
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                collection.InsertMany(list);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetModel<T>(String collectionName) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                FilterDefinition<T> filter = builderFilter.Empty;
                return (T)collection.Find<T>(filter);
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetFirstModel<T>(String collectionName) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                FilterDefinition<T> filter = builderFilter.Empty;
                return (T)collection.Find<T>(filter).FirstOrDefault();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetModel<T>(String collectionName, string time_stamp) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                FilterDefinition<T> filter = builderFilter.Gte("time_stamp", time_stamp);
                return (T)collection.Find<T>(filter);
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetFirstModel<T>(String collectionName, string time_stamp) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                FilterDefinition<T> filter = builderFilter.Gte("time_stamp", time_stamp);
                return (T)collection.Find<T>(filter).FirstOrDefault();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetSortFirstModel<T>(String collectionName, FilterDefinition<T> filter, SortDefinition<T> sort) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                return collection.Find<T>(filter).Sort(sort).FirstOrDefault<T>();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetSortFirstModel<T>(String collectionName, FilterDefinition<T> filter, SortDefinition<T> sort, ProjectionDefinition<T,T> Projection) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                return collection.Find<T>(filter).Sort(sort).Project(Projection).FirstOrDefault<T>();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetModel<T>(String collectionName, FilterDefinition<T> filter) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                return (T)collection.Find(filter);
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T GetFirstModel<T>(String collectionName, FilterDefinition<T> filter) where T : class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                return (T)collection.Find<T>(filter).FirstOrDefault();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static List<T> GetManyModel<T>(String collectionName, FilterDefinition<T> filter) where T: class
        {
            var collection = GetCollection<T>(collectionName);
            try
            {
                return collection.Find<T>(filter).ToList<T>();
            }
            catch
            {
                return default(List<T>);
            }
        }
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static List<T> GetManyModel<T>(String collectionName, FilterDefinition<T> filter, out int total) where T : class
        {
            var collection = GetCollection<T>(collectionName);

            total = 0;
            try
            {
                total = Convert.ToInt32(collection.Find<T>(filter).CountDocuments());
                return collection.Find<T>(filter).ToList<T>();
            }
            catch
            {
                return default(List<T>);
            }
        }
        /// <summary>
        /// 获取分页对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public static List<T> GetPageModel<T>(string clcName, FilterDefinition<T> filter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            var collection = GetCollection<T>(clcName);

            total = 0;
            try
            {
                total = Convert.ToInt32(collection.Find<T>(filter).CountDocuments());
                return collection.Find<T>(filter).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList<T>();
            }
            catch
            {
                return default(List<T>);
            }
        }
        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key">键值</param>
        public static bool Remove<T>(String collectionName, T t)
        {
            GetClientManager();
            return true;
        }
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void RemoveAll<T>(String collectionName)
        {
            GetClientManager();
        }
        #endregion
    }
}
