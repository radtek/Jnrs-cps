using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 09:56
    /// 描 述：报警信息汇总
    /// </summary>
    public class tb_alarm_historyEntity 
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
        /// alarm_start_no
        /// </summary>
        [Column("ALARM_START_NO")]
        public int? alarm_start_no { get; set; }
        /// <summary>
        /// alarm_end_no
        /// </summary>
        [Column("ALARM_END_NO")]
        public int? alarm_end_no { get; set; }
        /// <summary>
        /// alarm_grp
        /// </summary>
        [Column("ALARM_GRP")]
        public int? alarm_grp { get; set; }
        /// <summary>
        /// alarm_no
        /// </summary>
        [Column("ALARM_NO")]
        public int? alarm_no { get; set; }
        /// <summary>
        /// alarm_msg
        /// </summary>
        [Column("ALARM_MSG")]
        public string alarm_msg { get; set; }
        /// <summary>
        /// axis_no
        /// </summary>
        [Column("AXIS_NO")]
        public int? axis_no { get; set; }
        /// <summary>
        /// axis_num
        /// </summary>
        [Column("AXIS_NUM")]
        public int? axis_num { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        [Column("START_TIME")]
        public DateTime? start_time { get; set; }
        /// <summary>
        /// end_time
        /// </summary>
        [Column("END_TIME")]
        public DateTime? end_time { get; set; }
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
        /// <summary>
        /// 设备名称
        /// </summary>
        [NotMapped]
        public string machine_name { get; set; }
        #endregion
    }
}

