using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using SiteJnrs.Models;
using MachineDesign.Models;

namespace SiteXBFb.Areas.Visual.Models
{
    public class V_WriteData : DataConnect
    {
        ILog log = LogManager.GetLogger("APP_API");

        public bool initDBConnect()
        {
            return _initDBConnect();
        }

        public void endDBConnect()
        {
            _endDBConnect();
        }

        public bool updatePlanProdNum(string LR,string gis_numv, string mis_numv, int plan_prod_num, int plan_prod_mor, int plan_prod_mid, int plan_prod_nig)
        {
            string logMsg = "updatePlanProdNum";
            List<string> listStrSql = new List<string>();

            listStrSql.Add(" update tb_machine_param set data_setup = " + plan_prod_num + ", data_value = " + plan_prod_num
                            + "  from tb_machine_param va, (select machine_id,param_id from vw_machine_info vmi,tb_dict_parameter tdp "
                            + "         where vmi.mis_visual in (" + mis_numv + ") and vmi.is_program = '" + LR + "'"
                            + "           and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "'"
                            + "           and tdp.param_type = 'ProdData' and prw_type = '" + ReadParameter.InOutParamPrwType + "'"
                            + "           and param_name = 'PlanProdNum') vb "
                            + " where va.machine_id = vb.machine_id and va.param_id = vb.param_id ");
            listStrSql.Add(" update tb_machine_param set data_setup = " + plan_prod_mor + ", data_value = " + plan_prod_mor
                            + "  from tb_machine_param va, (select machine_id,param_id from vw_machine_info vmi,tb_dict_parameter tdp "
                            + "         where vmi.mis_visual in (" + mis_numv + ") and vmi.gis_visual = '" + gis_numv + "'"
                            + "           and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "'"
                            + "           and tdp.param_type = 'ProdData' and prw_type = '" + ReadParameter.InOutParamPrwType + "'"
                            + "           and param_name = 'PlanProdNum_Mor') vb "
                            + " where va.machine_id = vb.machine_id and va.param_id = vb.param_id ");
            listStrSql.Add(" update tb_machine_param set data_setup = " + plan_prod_mid + ", data_value = " + plan_prod_mid
                            + "  from tb_machine_param va, (select machine_id,param_id from vw_machine_info vmi,tb_dict_parameter tdp "
                            + "         where vmi.mis_visual in (" + mis_numv + ") and vmi.gis_visual = '" + gis_numv + "'"
                            + "           and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "'"
                            + "           and tdp.param_type = 'ProdData' and prw_type = '" + ReadParameter.InOutParamPrwType + "'"
                            + "           and param_name = 'PlanProdNum_Mid') vb "
                            + " where va.machine_id = vb.machine_id and va.param_id = vb.param_id ");
            listStrSql.Add(" update tb_machine_param set data_setup = " + plan_prod_nig + ", data_value = " + plan_prod_nig
                            + "  from tb_machine_param va, (select machine_id,param_id from vw_machine_info vmi,tb_dict_parameter tdp "
                            + "         where vmi.mis_visual in (" + mis_numv + ") and vmi.gis_visual = '" + gis_numv + "'"
                            + "           and vmi.is_prod = '" + ReadParameter.DeviceSXLProd + "'"
                            + "           and tdp.param_type = 'ProdData' and prw_type = '" + ReadParameter.InOutParamPrwType + "'"
                            + "           and param_name = 'PlanProdNum_Nig') vb "
                            + " where va.machine_id = vb.machine_id and va.param_id = vb.param_id ");
            for (int i = 0; i < listStrSql.Count; i++)
            {
                try
                {
                    if (!updateStrSqlData(logMsg, listStrSql[i]))
                    {
                        log.Info("计划产量信息更新失败!");
                    }
                }
                catch (SystemException ep)
                {
                    log.Error("计划产量信息更新错误!" + ep.Message);
                    return false;
                }
            }
            return true;
        }
    }
}