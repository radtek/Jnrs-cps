using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-02-14 15:59
    /// 描 述：保养工单维护
    /// </summary>
    public class tb_plan_cycle_exe_dataEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// workorder_id
        /// </summary>
        [Column("WORKORDER_ID")]
        public string workorder_id { get; set; }
        /// <summary>
        /// plan_maintain_id
        /// </summary>
        [Column("PLAN_MAINTAIN_ID")]
        public string plan_maintain_id { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// category_id
        /// </summary>
        [Column("CATEGORY_ID")]
        public int? category_id { get; set; }
        /// <summary>
        /// maintenance_id
        /// </summary>
        [Column("MAINTENANCE_ID")]
        public int? maintenance_id { get; set; }
        /// <summary>
        /// begin_date
        /// </summary>
        [Column("BEGIN_DATE")]
        public DateTime? begin_date { get; set; }
        /// <summary>
        /// end_date
        /// </summary>
        [Column("END_DATE")]
        public DateTime? end_date { get; set; }
        /// <summary>
        /// executor
        /// </summary>
        [Column("EXECUTOR")]
        public string executor { get; set; }
        /// <summary>
        /// complete_date
        /// </summary>
        [Column("COMPLETE_DATE")]
        public DateTime? complete_date { get; set; }
        /// <summary>
        /// complete_spec
        /// </summary>
        [Column("COMPLETE_SPEC")]
        public string complete_spec { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        [Column("REMARK")]
        public string remark { get; set; }
        /// <summary>
        /// enabled
        /// </summary>
        [Column("ENABLED")]
        public int? enabled { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
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

