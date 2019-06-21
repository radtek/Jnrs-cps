using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-16 15:37
    /// 描 述：小时计划产量维护
    /// </summary>
    public class tb_plan_detail_dataMap : EntityTypeConfiguration<tb_plan_detail_dataEntity>
    {
        public tb_plan_detail_dataMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_PLAN_DETAIL_DATA");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

