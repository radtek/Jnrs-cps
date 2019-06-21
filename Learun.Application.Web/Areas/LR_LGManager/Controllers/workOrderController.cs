using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-02-14 15:59
    /// 描 述：保养工单维护
    /// </summary>
    public class workOrderController : MvcControllerBase
    {
        private workOrderIBLL workOrderIBLL = new workOrderBLL();

        #region  视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        #endregion

        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = workOrderIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            //var tb_plan_cycle_dataData = workOrderIBLL.Gettb_plan_cycle_dataEntity( keyValue );
            //var tb_plan_cycle_exe_dataData = workOrderIBLL.Gettb_plan_cycle_exe_dataEntity( tb_plan_cycle_dataData.id );
            var tb_plan_cycle_exe_dataData = workOrderIBLL.Gettb_plan_cycle_exe_dataEntity(keyValue);
            var tb_plan_cycle_dataData = workOrderIBLL.Gettb_plan_cycle_dataEntity(tb_plan_cycle_exe_dataData.id);
            var jsonData = new {
                tb_plan_cycle_exe_data = tb_plan_cycle_exe_dataData,
                tb_plan_cycle_data = tb_plan_cycle_dataData,
            };
            return Success(jsonData);
        }
        #endregion

        #region  提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            workOrderIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strtb_plan_cycle_exe_dataEntity)
        {
            tb_plan_cycle_dataEntity entity = strEntity.ToObject<tb_plan_cycle_dataEntity>();
            tb_plan_cycle_exe_dataEntity tb_plan_cycle_exe_dataEntity = strtb_plan_cycle_exe_dataEntity.ToObject<tb_plan_cycle_exe_dataEntity>();
            workOrderIBLL.SaveEntity(keyValue,entity,tb_plan_cycle_exe_dataEntity);
            return Success("保存成功！");
        }
        #endregion

    }
}
