using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-16 14:41
    /// 描 述：产品信息维护
    /// </summary>
    public class tb_product_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// product_id
        /// </summary>
        [Column("PRODUCT_ID")]
        public int? product_id { get; set; }
        /// <summary>
        /// product_no
        /// </summary>
        [Column("PRODUCT_NO")]
        public string product_no { get; set; }
        /// <summary>
        /// product_name
        /// </summary>
        [Column("PRODUCT_NAME")]
        public string product_name { get; set; }
        /// <summary>
        /// custom_no
        /// </summary>
        [Column("CUSTOM_NO")]
        public string custom_no { get; set; }
        /// <summary>
        /// custom_name
        /// </summary>
        [Column("CUSTOM_NAME")]
        public string custom_name { get; set; }
        /// <summary>
        /// product_type
        /// </summary>
        [Column("PRODUCT_TYPE")]
        public string product_type { get; set; }
        /// <summary>
        /// product_ecode
        /// </summary>
        [Column("PRODUCT_ECODE")]
        public string product_ecode { get; set; }
        /// <summary>
        /// sheet_no
        /// </summary>
        [Column("SHEET_NO")]
        public string sheet_no { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        [Column("UNIT")]
        public string unit { get; set; }
        /// <summary>
        /// group_id
        /// </summary>
        [Column("GROUP_ID")]
        public int? group_id { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
        /// <summary>
        /// create_employee_id
        /// </summary>
        [Column("CREATE_EMPLOYEE_ID")]
        public int? create_employee_id { get; set; }
        /// <summary>
        /// alter_time
        /// </summary>
        [Column("ALTER_TIME")]
        public DateTime? alter_time { get; set; }
        /// <summary>
        /// alter_employee_id
        /// </summary>
        [Column("ALTER_EMPLOYEE_ID")]
        public int? alter_employee_id { get; set; }
        /// <summary>
        /// machine_time
        /// </summary>
        [Column("MACHINE_TIME")]
        public DateTime? machine_time { get; set; }
        /// <summary>
        /// machine_employee_id
        /// </summary>
        [Column("MACHINE_EMPLOYEE_ID")]
        public int? machine_employee_id { get; set; }
        /// <summary>
        /// last_time
        /// </summary>
        [Column("LAST_TIME")]
        public DateTime? last_time { get; set; }
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

