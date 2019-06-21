using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-16 14:02
    /// 描 述：质量问题上报
    /// </summary>
    public class tb_quantity_infoMap : EntityTypeConfiguration<tb_quantity_infoEntity>
    {
        public tb_quantity_infoMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_QUANTITY_INFO");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

