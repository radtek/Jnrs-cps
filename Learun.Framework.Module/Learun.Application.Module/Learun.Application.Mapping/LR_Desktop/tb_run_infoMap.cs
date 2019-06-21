using Learun.Application.TwoDevelopment.LR_Desktop;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-11 11:32
    /// 描 述：设备运行参数
    /// </summary>
    public class tb_run_infoMap : EntityTypeConfiguration<tb_run_infoEntity>
    {
        public tb_run_infoMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_RUN_INFO");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

