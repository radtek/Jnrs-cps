using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-16 15:37
    /// 描 述：小时计划产量维护
    /// </summary>
    public class tb_plan_detail_dataEntity 
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
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// machine_name
        /// </summary>
        [Column("MACHINE_NAME")]
        public string machine_name { get; set; }
        /// <summary>
        /// machine_number
        /// </summary>
        [Column("MACHINE_NUMBER")]
        public string machine_number { get; set; }
        /// <summary>
        /// wshift_id
        /// </summary>
        [Column("WSHIFT_ID")]
        public int? wshift_id { get; set; }
        /// <summary>
        /// wshift_name
        /// </summary>
        [Column("WSHIFT_NAME")]
        public string wshift_name { get; set; }
        /// <summary>
        /// wshift_date
        /// </summary>
        [Column("WSHIFT_DATE")]
        public string wshift_date { get; set; }
        /// <summary>
        /// product_id
        /// </summary>
        [Column("PRODUCT_ID")]
        public int? product_id { get; set; }
        /// <summary>
        /// product_name
        /// </summary>
        [Column("PRODUCT_NAME")]
        public string product_name { get; set; }
        /// <summary>
        /// sheet_no
        /// </summary>
        [Column("SHEET_NO")]
        public string sheet_no { get; set; }
        /// <summary>
        /// emp_id
        /// </summary>
        [Column("EMP_ID")]
        public int? emp_id { get; set; }
        /// <summary>
        /// emp_name
        /// </summary>
        [Column("EMP_NAME")]
        public string emp_name { get; set; }
        /// <summary>
        /// plan_month
        /// </summary>
        [Column("PLAN_MONTH")]
        public int? plan_month { get; set; }
        /// <summary>
        /// plan00
        /// </summary>
        [Column("PLAN00")]
        public int? plan00 { get; set; }
        /// <summary>
        /// plan01
        /// </summary>
        [Column("PLAN01")]
        public int? plan01 { get; set; }
        /// <summary>
        /// plan02
        /// </summary>
        [Column("PLAN02")]
        public int? plan02 { get; set; }
        /// <summary>
        /// plan03
        /// </summary>
        [Column("PLAN03")]
        public int? plan03 { get; set; }
        /// <summary>
        /// plan04
        /// </summary>
        [Column("PLAN04")]
        public int? plan04 { get; set; }
        /// <summary>
        /// plan05
        /// </summary>
        [Column("PLAN05")]
        public int? plan05 { get; set; }
        /// <summary>
        /// plan06
        /// </summary>
        [Column("PLAN06")]
        public int? plan06 { get; set; }
        /// <summary>
        /// plan07
        /// </summary>
        [Column("PLAN07")]
        public int? plan07 { get; set; }
        /// <summary>
        /// plan08
        /// </summary>
        [Column("PLAN08")]
        public int? plan08 { get; set; }
        /// <summary>
        /// plan09
        /// </summary>
        [Column("PLAN09")]
        public int? plan09 { get; set; }
        /// <summary>
        /// plan10
        /// </summary>
        [Column("PLAN10")]
        public int? plan10 { get; set; }
        /// <summary>
        /// plan11
        /// </summary>
        [Column("PLAN11")]
        public int? plan11 { get; set; }
        /// <summary>
        /// plan12
        /// </summary>
        [Column("PLAN12")]
        public int? plan12 { get; set; }
        /// <summary>
        /// plan13
        /// </summary>
        [Column("PLAN13")]
        public int? plan13 { get; set; }
        /// <summary>
        /// plan14
        /// </summary>
        [Column("PLAN14")]
        public int? plan14 { get; set; }
        /// <summary>
        /// plan15
        /// </summary>
        [Column("PLAN15")]
        public int? plan15 { get; set; }
        /// <summary>
        /// plan16
        /// </summary>
        [Column("PLAN16")]
        public int? plan16 { get; set; }
        /// <summary>
        /// plan17
        /// </summary>
        [Column("PLAN17")]
        public int? plan17 { get; set; }
        /// <summary>
        /// plan18
        /// </summary>
        [Column("PLAN18")]
        public int? plan18 { get; set; }
        /// <summary>
        /// plan19
        /// </summary>
        [Column("PLAN19")]
        public int? plan19 { get; set; }
        /// <summary>
        /// plan20
        /// </summary>
        [Column("PLAN20")]
        public int? plan20 { get; set; }
        /// <summary>
        /// plan21
        /// </summary>
        [Column("PLAN21")]
        public int? plan21 { get; set; }
        /// <summary>
        /// plan22
        /// </summary>
        [Column("PLAN22")]
        public int? plan22 { get; set; }
        /// <summary>
        /// plan23
        /// </summary>
        [Column("PLAN23")]
        public int? plan23 { get; set; }
        /// <summary>
        /// enabled
        /// </summary>
        [Column("ENABLED")]
        public int? enabled { get; set; }
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

