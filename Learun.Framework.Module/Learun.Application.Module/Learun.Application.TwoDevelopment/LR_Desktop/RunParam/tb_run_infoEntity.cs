using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_Desktop
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-11 11:32
    /// 描 述：设备运行参数
    /// </summary>
    public class tb_run_infoEntity 
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
        /// main_prog_num
        /// </summary>
        [Column("MAIN_PROG_NUM")]
        public string main_prog_num { get; set; }
        /// <summary>
        /// running_prog_num
        /// </summary>
        [Column("RUNNING_PROG_NUM")]
        public string running_prog_num { get; set; }
        /// <summary>
        /// run_state
        /// </summary>
        [Column("RUN_STATE")]
        public int? run_state { get; set; }
        /// <summary>
        /// act_spindle_speed_0
        /// </summary>
        [Column("ACT_SPINDLE_SPEED_0")]
        public decimal? act_spindle_speed_0 { get; set; }
        /// <summary>
        /// act_spindle_override_0
        /// </summary>
        [Column("ACT_SPINDLE_OVERRIDE_0")]
        public decimal? act_spindle_override_0 { get; set; }
        /// <summary>
        /// act_feed_speed_0
        /// </summary>
        [Column("ACT_FEED_SPEED_0")]
        public decimal? act_feed_speed_0 { get; set; }
        /// <summary>
        /// act_feed_override_0
        /// </summary>
        [Column("ACT_FEED_OVERRIDE_0")]
        public decimal? act_feed_override_0 { get; set; }
        /// <summary>
        /// act_spindle_speed_1
        /// </summary>
        [Column("ACT_SPINDLE_SPEED_1")]
        public decimal? act_spindle_speed_1 { get; set; }
        /// <summary>
        /// act_feedrate_1
        /// </summary>
        [Column("ACT_FEEDRATE_1")]
        public decimal? act_feedrate_1 { get; set; }
        /// <summary>
        /// act_speed_1
        /// </summary>
        [Column("ACT_SPEED_1")]
        public decimal? act_speed_1 { get; set; }
        /// <summary>
        /// act_axis_1
        /// </summary>
        [Column("ACT_AXIS_1")]
        public decimal? act_axis_1 { get; set; }
        /// <summary>
        /// act_spindle_speed_2
        /// </summary>
        [Column("ACT_SPINDLE_SPEED_2")]
        public decimal? act_spindle_speed_2 { get; set; }
        /// <summary>
        /// act_feedrate_2
        /// </summary>
        [Column("ACT_FEEDRATE_2")]
        public decimal? act_feedrate_2 { get; set; }
        /// <summary>
        /// act_speed_2
        /// </summary>
        [Column("ACT_SPEED_2")]
        public decimal? act_speed_2 { get; set; }
        /// <summary>
        /// act_axis_2
        /// </summary>
        [Column("ACT_AXIS_2")]
        public decimal? act_axis_2 { get; set; }
        /// <summary>
        /// sp_load_0
        /// </summary>
        [Column("SP_LOAD_0")]
        public decimal? sp_load_0 { get; set; }
        /// <summary>
        /// sp_load_1
        /// </summary>
        [Column("SP_LOAD_1")]
        public decimal? sp_load_1 { get; set; }
        /// <summary>
        /// sp_load_2
        /// </summary>
        [Column("SP_LOAD_2")]
        public decimal? sp_load_2 { get; set; }
        /// <summary>
        /// sv_load_0
        /// </summary>
        [Column("SV_LOAD_0")]
        public decimal? sv_load_0 { get; set; }
        /// <summary>
        /// sv_load_1
        /// </summary>
        [Column("SV_LOAD_1")]
        public decimal? sv_load_1 { get; set; }
        /// <summary>
        /// sv_load_2
        /// </summary>
        [Column("SV_LOAD_2")]
        public decimal? sv_load_2 { get; set; }
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

