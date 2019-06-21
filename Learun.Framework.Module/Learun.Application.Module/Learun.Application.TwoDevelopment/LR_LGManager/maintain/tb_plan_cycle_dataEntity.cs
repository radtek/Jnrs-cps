using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-02-14 14:31
    /// 描 述：设备维护保养
    /// </summary>
    public class tb_plan_cycle_dataEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// plan_maintain_id
        /// </summary>
        [Column("PLAN_MAINTAIN_ID")]
        public string plan_maintain_id { get; set; }
        /// <summary>
        /// plan_maintain_name
        /// </summary>
        [Column("PLAN_MAINTAIN_NAME")]
        public string plan_maintain_name { get; set; }
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
        /// cycle
        /// </summary>
        [Column("CYCLE")]
        public int? cycle { get; set; }
        /// <summary>
        /// cycle_unit
        /// </summary>
        [Column("CYCLE_UNIT")]
        public string cycle_unit { get; set; }
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
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
        /// <summary>
        /// creater
        /// </summary>
        [Column("CREATER")]
        public int? creater { get; set; }
        /// <summary>
        /// executor
        /// </summary>
        [Column("EXECUTOR")]
        public string executor { get; set; }
        /// <summary>
        /// enabled
        /// </summary>
        [Column("ENABLED")]
        public int? enabled { get; set; }
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

