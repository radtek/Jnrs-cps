using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 13:48
    /// 描 述：二维码追溯
    /// </summary>
    public class tb_product_hisEntity 
    {
        #region  实体成员

        /// <summary>
        /// id
        /// </summary>
        [Column("id")]
        public string id { get; set; }

        /// <summary>
        /// calc_date
        /// </summary>
        [Column("CALC_DATE")]
        public int? calc_date { get; set; }
        /// <summary>
        /// product_id
        /// </summary>
        [Column("PRODUCT_ID")]
        public string product_id { get; set; }
        /// <summary>
        /// product_ecode
        /// </summary>
        [Column("PRODUCT_ECODE")]
        public string product_ecode { get; set; }
        /// <summary>
        /// stage_name
        /// </summary>
        [Column("STAGE_NAME")]
        public string stage_name { get; set; }
        /// <summary>
        /// stage_desp
        /// </summary>
        [Column("STAGE_DESP")]
        public string stage_desp { get; set; }
        /// <summary>
        /// stage_group_id
        /// </summary>
        [Column("STAGE_GROUP_ID")]
        public string stage_group_id { get; set; }
        /// <summary>
        /// stage_mac_id
        /// </summary>
        [Column("STAGE_MAC_ID")]
        public string stage_mac_id { get; set; }
        /// <summary>
        /// stage_emp_id
        /// </summary>
        [Column("STAGE_EMP_ID")]
        public string stage_emp_id { get; set; }
        /// <summary>
        /// stage_time
        /// </summary>
        [Column("STAGE_TIME")]
        public DateTime? stage_time { get; set; }
        #endregion

        #region  扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
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

