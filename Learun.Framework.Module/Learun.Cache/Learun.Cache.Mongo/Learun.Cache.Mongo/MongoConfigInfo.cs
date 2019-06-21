using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MongoDB.Driver;

namespace Learun.Cache.Mongo
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬
    /// 日 期：2017.03.03
    /// 描 述：redis配置信息
    /// </summary>
    public sealed class MongoConfigInfo : ConfigurationSection
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static MongoConfigInfo GetConfig()
        {
            return GetConfig("mongoconfig");
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static MongoConfigInfo GetConfig(string sectionName)
        {
            MongoConfigInfo section = (MongoConfigInfo)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        /// <summary>
        /// 可写的Mongo链接地址
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = false)]
        public string ConnectionString
        {
            get
            {
                //return ConfigurationManager.AppSettings["ConnectionString"];
                return (string)base["ConnectionString"];
            }
            set
            {
                base["ConnectionString"] = value;
            }
        }
        /// <summary>
        /// 可读的Mongo链接地址
        /// </summary>
        [ConfigurationProperty("MongoDB", IsRequired = false)]
        public string MongoDB
        {
            get
            {
                //return ConfigurationManager.AppSettings["MongoDB"];
                return (string)base["MongoDB"];
            }
            set
            {
                base["MongoDB"] = value;
            }
        }
        /// <summary>
        /// 最大写链接数
        /// </summary>
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false, DefaultValue = 500)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 500;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }
        /// <summary>
        /// 最大读链接数
        /// </summary>
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 500)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 500;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }
        /// <summary>
        /// 最大连接池
        /// </summary>
        [ConfigurationProperty("MaxConnectionPoolSize", IsRequired = false, DefaultValue = 500)]
        public int MaxConnectionPoolSize
        {
            get
            {
                int _maxConnectionPoolSize = (int)base["MaxConnectionPoolSize"];
                return _maxConnectionPoolSize > 0 ? _maxConnectionPoolSize : 500;
            }
            set
            {
                base["MaxConnectionPoolSize"] = value;
            }
        }
        /// <summary>
        /// 最大闲置时间
        /// </summary>
        [ConfigurationProperty("MaxConnectionIdleTime", IsRequired = false, DefaultValue = 30)]
        public int MaxConnectionIdleTime
        {
            get
            {
                int _maxConnectionIdleTime = (int)base["MaxConnectionIdleTime"];
                return _maxConnectionIdleTime > 0 ? _maxConnectionIdleTime : 30;
            }
            set
            {
                base["MaxConnectionIdleTime"] = value;
            }
        }
        /// <summary>
        /// 最大存活时间
        /// </summary>
        [ConfigurationProperty("MaxConnectionLifeTime", IsRequired = false, DefaultValue = 60)]
        public int MaxConnectionLifeTime
        {
            get
            {
                int _maxConnectionLifeTime = (int)base["MaxConnectionLifeTime"];
                return _maxConnectionLifeTime > 0 ? _maxConnectionLifeTime : 60;
            }
            set
            {
                base["MaxConnectionLifeTime"] = value;
            }
        }
        /// <summary>
        /// 链接时间
        /// </summary>
        [ConfigurationProperty("ConnectTimeout", IsRequired = false, DefaultValue = 10)]
        public int ConnectTimeout
        {
            get
            {
                int _connectTimeout = (int)base["ConnectTimeout"];
                return _connectTimeout > 0 ? _connectTimeout : 10;
            }
            set
            {
                base["ConnectTimeout"] = value;
            }
        }
        /// <summary>
        /// 等待队列大小
        /// </summary>
        [ConfigurationProperty("WaitQueueSize", IsRequired = false, DefaultValue = 50)]
        public int WaitQueueSize
        {
            get
            {
                int _waitQueueSize = (int)base["WaitQueueSize"];
                return _waitQueueSize > 0 ? _waitQueueSize : 50;
            }
            set
            {
                base["WaitQueueSize"] = value;
            }
        }
        /// <summary>
        /// socket超时时间
        /// </summary>
        [ConfigurationProperty("SocketTimeout", IsRequired = false, DefaultValue = 10)]
        public int SocketTimeout
        {
            get
            {
                int _socketTimeout = (int)base["SocketTimeout"];
                return _socketTimeout > 0 ? _socketTimeout : 10;
            }
            set
            {
                base["SocketTimeout"] = value;
            }
        }
        /// <summary>
        /// 队列等待时间
        /// </summary>
        [ConfigurationProperty("WaitQueueTimeout", IsRequired = false, DefaultValue = 60)]
        public int WaitQueueTimeout
        {
            get
            {
                int _waitQueueTimeout = (int)base["WaitQueueTimeout"];
                return _waitQueueTimeout > 0 ? _waitQueueTimeout : 60;
            }
            set
            {
                base["WaitQueueTimeout"] = value;
            }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        [ConfigurationProperty("OperationTimeout", IsRequired = false, DefaultValue = 60)]
        public int OperationTimeout
        {
            get
            {
                int _waitQueueTimeout = (int)base["OperationTimeout"];
                return _waitQueueTimeout > 0 ? _waitQueueTimeout : 60;
            }
            set
            {
                base["OperationTimeout"] = value;
            }
        }
        /// <summary>
        /// 自动重启
        /// </summary>
        [ConfigurationProperty("AutoStart", IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)base["AutoStart"];
            }
            set
            {
                base["AutoStart"] = value;
            }
        }
        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        [ConfigurationProperty("LocalCacheTime", IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)base["LocalCacheTime"];
            }
            set
            {
                base["LocalCacheTime"] = value;
            }
        }
        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }
        /// <summary>
        /// 默认开始db
        /// </summary>
        [ConfigurationProperty("DefaultDb", IsRequired = false)]
        public long DefaultDb
        {
            get
            {
                return (long)base["DefaultDb"];
            }
            set
            {
                base["DefaultDb"] = value;
            }
        }

    }
}
