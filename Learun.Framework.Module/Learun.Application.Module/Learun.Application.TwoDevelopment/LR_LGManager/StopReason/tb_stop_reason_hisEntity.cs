using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-23 11:05
    /// 描 述：停机原因反馈
    /// </summary>
    public class tb_stop_reason_hisEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// calc_date
        /// </summary>
        [Column("CALC_DATE")]
        public string calc_date { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// wshift_id
        /// </summary>
        [Column("WSHIFT_ID")]
        public int? wshift_id { get; set; }
        /// <summary>
        /// wshift_date
        /// </summary>
        [Column("WSHIFT_DATE")]
        public string wshift_date { get; set; }
        /// <summary>
        /// stop_cate_no
        /// </summary>
        [Column("STOP_CATE_NO")]
        public int? stop_cate_no { get; set; }
        /// <summary>
        /// stop_class_id
        /// </summary>
        [Column("STOP_CLASS_ID")]
        public int? stop_class_id { get; set; }
        /// <summary>
        /// stop_reason
        /// </summary>
        [Column("STOP_REASON")]
        public string stop_reason { get; set; }
        /// <summary>
        /// stop_analysis
        /// </summary>
        [Column("STOP_ANALYSIS")]
        public string stop_analysis { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Column("NOTE")]
        public string note { get; set; }
        /// <summary>
        /// stop_time
        /// </summary>
        [Column("STOP_TIME")]
        public DateTime? stop_time { get; set; }
        /// <summary>
        /// read_time
        /// </summary>
        [Column("READ_TIME")]
        public DateTime? read_time { get; set; }
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

