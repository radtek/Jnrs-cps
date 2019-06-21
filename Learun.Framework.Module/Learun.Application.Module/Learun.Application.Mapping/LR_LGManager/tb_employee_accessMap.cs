using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-04-19 09:24
    /// 描 述：班次人员登录
    /// </summary>
    public class tb_employee_accessMap : EntityTypeConfiguration<tb_employee_accessEntity>
    {
        public tb_employee_accessMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_EMPLOYEE_ACCESS");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

