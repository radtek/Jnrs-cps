using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 11:17
    /// 描 述：设备分组维护
    /// </summary>
    public class tb_macgroup_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// group_id
        /// </summary>
        [Column("GROUP_ID")]
        public int? group_id { get; set; }
        /// <summary>
        /// group_name
        /// </summary>
        [Column("GROUP_NAME")]
        public string group_name { get; set; }
        /// <summary>
        /// show_numt
        /// </summary>
        [Column("SHOW_NUMT")]
        public string show_numt { get; set; }
        /// <summary>
        /// show_numv
        /// </summary>
        [Column("SHOW_NUMV")]
        public string show_numv { get; set; }
        /// <summary>
        /// gis_visual
        /// </summary>
        [Column("GIS_VISUAL")]
        public string gis_visual { get; set; }
        /// <summary>
        /// enabled
        /// </summary>
        [Column("ENABLED")]
        public int? enabled { get; set; }
        #endregion

        #region  扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
        }
        #endregion
        #region  扩展字段
        #endregion
    }
}

