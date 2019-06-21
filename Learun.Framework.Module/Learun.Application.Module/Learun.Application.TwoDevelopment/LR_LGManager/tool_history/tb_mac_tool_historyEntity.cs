using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-06-04 14:05
    /// 描 述：刀具寿命管理
    /// </summary>
    public class tb_mac_tool_historyEntity 
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
        /// tool_no
        /// </summary>
        [Column("TOOL_NO")]
        public int? tool_no { get; set; }
        /// <summary>
        /// len_offset
        /// </summary>
        [Column("LEN_OFFSET")]
        public decimal? len_offset { get; set; }
        /// <summary>
        /// len_wear_compensate
        /// </summary>
        [Column("LEN_WEAR_COMPENSATE")]
        public decimal? len_wear_compensate { get; set; }
        /// <summary>
        /// diameter_compensate
        /// </summary>
        [Column("DIAMETER_COMPENSATE")]
        public decimal? diameter_compensate { get; set; }
        /// <summary>
        /// diameter_wear_compensate
        /// </summary>
        [Column("DIAMETER_WEAR_COMPENSATE")]
        public decimal? diameter_wear_compensate { get; set; }
        /// <summary>
        /// initial_life
        /// </summary>
        [Column("INITIAL_LIFE")]
        public int? initial_life { get; set; }
        /// <summary>
        /// life_prediction
        /// </summary>
        [Column("LIFE_PREDICTION")]
        public int? life_prediction { get; set; }
        /// <summary>
        /// tool_life
        /// </summary>
        [Column("TOOL_LIFE")]
        public int? tool_life { get; set; }
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

