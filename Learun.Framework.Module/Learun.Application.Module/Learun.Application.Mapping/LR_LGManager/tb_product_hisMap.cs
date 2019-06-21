using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-10 17:19
    /// 描 述：erweima2
    /// </summary>
    public class tb_product_hisMap : EntityTypeConfiguration<tb_product_hisEntity>
    {
        public tb_product_hisMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_PRODUCT_HIS");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

