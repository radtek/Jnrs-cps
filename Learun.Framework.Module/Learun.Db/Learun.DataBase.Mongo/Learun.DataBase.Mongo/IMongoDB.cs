using Learun.Cache.Mongo;
using MongoDB.Driver;

namespace Learun.DataBase.Mongo
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬
    /// 日 期：2017.03.04
    /// 描 述：数据库方法操作接口
    /// </summary>
    public interface IMongoDB : IMgCache
    {
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        IMongoDatabase getDBContext();
        #region  对象实体 添加、修改、删除
        #endregion

        #region  对象实体 查询
        #endregion

        #region  数据源查询
        #endregion

        #region  扩展方法
        #endregion
    }
}
