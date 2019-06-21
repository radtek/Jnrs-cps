using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-04-22 10:08
    /// 描 述：班次计划产量维护
    /// </summary>
    public class tb_plan_wshift_dataEntity 
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
        /// plan_wshift
        /// </summary>
        [Column("PLAN_WSHIFT")]
        public int? plan_wshift { get; set; }
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
            DateTime dtime = DateTime.Now;
            this.calc_date = Convert.ToDateTime(this.read_time).ToString("yyyyMMdd");
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
            DateTime dtime = DateTime.Now;
            this.calc_date = Convert.ToDateTime(this.read_time).ToString("yyyyMMdd");
        }
        #endregion
        #region  扩展字段
        #endregion
    }
}

