using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-16 14:02
    /// 描 述：质量问题上报
    /// </summary>
    public class tb_quantity_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// group_no
        /// </summary>
        [Column("GROUP_NO")]
        public string group_no { get; set; }
        /// <summary>
        /// work_name
        /// </summary>
        [Column("WORK_NAME")]
        public string work_name { get; set; }
        /// <summary>
        /// product_time
        /// </summary>
        [Column("PRODUCT_TIME")]
        public DateTime? product_time { get; set; }
        /// <summary>
        /// unqualified_no
        /// </summary>
        [Column("UNQUALIFIED_NO")]
        public int? unqualified_no { get; set; }
        /// <summary>
        /// unqualified_note
        /// </summary>
        [Column("UNQUALIFIED_NOTE")]
        public string unqualified_note { get; set; }
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

