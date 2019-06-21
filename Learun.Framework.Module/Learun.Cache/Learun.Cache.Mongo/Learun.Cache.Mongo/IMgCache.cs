using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Learun.Cache.Mongo
{
    public interface IMgCache
    {
        #region  Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T Read<T>(string collectionName) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T ReadFirst<T>(string collectionName) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T Read<T>(string collectionName, string time_stamp) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T ReadFirst<T>(string collectionName, string time_stamp) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T Read<T>(string collectionName, FilterDefinition<T> filter) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T ReadFirst<T>(string collectionName, FilterDefinition<T> filter) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        List<T> ReadMany<T>(string collectionName, FilterDefinition<T> filter) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        List<T> ReadMany<T>(string collectionName, FilterDefinition<T> filter, out int total) where T : class;
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        List<T> ReadPage<T>(string collectionName, FilterDefinition<T> filter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        void Write<T>(string collectionName, T value) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        void Write<T>(string collectionName, T value, DateTime expireTime) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        void Write<T>(string collectionName, T value, TimeSpan timeSpan) where T : class;
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        void Remove<T>(string collectionName, T value);
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void RemoveAll<T>(string collectionName);
        #endregion
        T ReadSortFirst<T>(string clcName, FilterDefinition<T> filter, SortDefinition<T> sort) where T : class;
        //T ReadSortFirst<T>(string clcName, FilterDefinition<T> filter, SortDefinition<T> sort, ProjectionDefinition<T> Projection) where T : class;
    }
}
