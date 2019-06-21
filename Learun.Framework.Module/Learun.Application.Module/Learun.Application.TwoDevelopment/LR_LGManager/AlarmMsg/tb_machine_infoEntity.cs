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
    public class tb_machine_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// group_number
        /// </summary>
        [Column("GROUP_NUMBER")]
        public int? group_number { get; set; }
        /// <summary>
        /// machine_name
        /// </summary>
        [Column("MACHINE_NAME")]
        public string machine_name { get; set; }
        /// <summary>
        /// machine_series
        /// </summary>
        [Column("MACHINE_SERIES")]
        public string machine_series { get; set; }
        /// <summary>
        /// machine_number
        /// </summary>
        [Column("MACHINE_NUMBER")]
        public string machine_number { get; set; }
        /// <summary>
        /// comm_protocol
        /// </summary>
        [Column("COMM_PROTOCOL")]
        public string comm_protocol { get; set; }
        /// <summary>
        /// comm_interface
        /// </summary>
        [Column("COMM_INTERFACE")]
        public string comm_interface { get; set; }
        /// <summary>
        /// rank_num
        /// </summary>
        [Column("RANK_NUM")]
        public int? rank_num { get; set; }
        /// <summary>
        /// category
        /// </summary>
        [Column("CATEGORY")]
        public string category { get; set; }
        /// <summary>
        /// sets_no
        /// </summary>
        [Column("SETS_NO")]
        public string sets_no { get; set; }
        /// <summary>
        /// is_run_state
        /// </summary>
        [Column("IS_RUN_STATE")]
        public string is_run_state { get; set; }
        /// <summary>
        /// is_prod
        /// </summary>
        [Column("IS_PROD")]
        public string is_prod { get; set; }
        /// <summary>
        /// run_param
        /// </summary>
        [Column("RUN_PARAM")]
        public string run_param { get; set; }
        /// <summary>
        /// is_alarm
        /// </summary>
        [Column("IS_ALARM")]
        public string is_alarm { get; set; }
        /// <summary>
        /// is_program
        /// </summary>
        [Column("IS_PROGRAM")]
        public string is_program { get; set; }
        /// <summary>
        /// is_barcode
        /// </summary>
        [Column("IS_BARCODE")]
        public string is_barcode { get; set; }
        /// <summary>
        /// mis_visual
        /// </summary>
        [Column("MIS_VISUAL")]
        public string mis_visual { get; set; }
        /// <summary>
        /// is_01
        /// </summary>
        [Column("IS_01")]
        public string is_01 { get; set; }
        /// <summary>
        /// is_02
        /// </summary>
        [Column("IS_02")]
        public string is_02 { get; set; }
        /// <summary>
        /// is_03
        /// </summary>
        [Column("IS_03")]
        public string is_03 { get; set; }
        /// <summary>
        /// is_04
        /// </summary>
        [Column("IS_04")]
        public string is_04 { get; set; }
        /// <summary>
        /// is_05
        /// </summary>
        [Column("IS_05")]
        public string is_05 { get; set; }
        /// <summary>
        /// is_06
        /// </summary>
        [Column("IS_06")]
        public string is_06 { get; set; }
        /// <summary>
        /// is_07
        /// </summary>
        [Column("IS_07")]
        public string is_07 { get; set; }
        /// <summary>
        /// is_08
        /// </summary>
        [Column("IS_08")]
        public string is_08 { get; set; }
        /// <summary>
        /// is_09
        /// </summary>
        [Column("IS_09")]
        public string is_09 { get; set; }
        /// <summary>
        /// is_main
        /// </summary>
        [Column("IS_MAIN")]
        public string is_main { get; set; }
        /// <summary>
        /// price
        /// </summary>
        [Column("PRICE")]
        public decimal? price { get; set; }
        /// <summary>
        /// manufacture
        /// </summary>
        [Column("MANUFACTURE")]
        public string manufacture { get; set; }
        /// <summary>
        /// born_date
        /// </summary>
        [Column("BORN_DATE")]
        public DateTime? born_date { get; set; }
        /// <summary>
        /// buy_person
        /// </summary>
        [Column("BUY_PERSON")]
        public string buy_person { get; set; }
        /// <summary>
        /// photo
        /// </summary>
        [Column("PHOTO")]
        public string photo { get; set; }
        /// <summary>
        /// enabled
        /// </summary>
        [Column("ENABLED")]
        public int? enabled { get; set; }
        /// <summary>
        /// memo
        /// </summary>
        [Column("MEMO")]
        public string memo { get; set; }
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
    }
}

