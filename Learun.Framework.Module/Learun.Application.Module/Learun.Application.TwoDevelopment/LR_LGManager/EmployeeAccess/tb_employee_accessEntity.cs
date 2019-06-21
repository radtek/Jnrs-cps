using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-04-19 09:24
    /// 描 述：班次人员登录
    /// </summary>
    public class tb_employee_accessEntity 
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
        public int? calc_date { get; set; }
        /// <summary>
        /// employee_id
        /// </summary>
        [Column("EMPLOYEE_ID")]
        public string employee_id { get; set; }
        /// <summary>
        /// account_id
        /// </summary>
        [Column("ACCOUNT_ID")]
        public int? account_id { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// login_ip
        /// </summary>
        [Column("LOGIN_IP")]
        public string login_ip { get; set; }
        /// <summary>
        /// access_type
        /// </summary>
        [Column("ACCESS_TYPE")]
        public string access_type { get; set; }
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
            DateTime dtime = DateTime.Now;
            this.calc_date = Convert.ToInt32(Convert.ToDateTime(this.last_time).ToString("yyyyMMdd"));
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
            DateTime dtime = DateTime.Now;
            this.calc_date = Convert.ToInt32(Convert.ToDateTime(this.last_time).ToString("yyyyMMdd"));
        }
        #endregion
        #region  扩展字段
        #endregion
    }
}

