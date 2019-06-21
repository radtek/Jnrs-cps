using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_RunParam;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_RunParam.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-14 17:49
    /// 描 述：设备运行参数
    /// </summary>
    public class LR_RunParamController : MvcControllerBase
    {
        private LR_RunParamIBLL lR_RunParamIBLL = new LR_RunParamBLL();

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
            var data = lR_RunParamIBLL.GetPageList(paginationobj, queryJson);
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
            var tb_run_infoData = lR_RunParamIBLL.Gettb_run_infoEntity( keyValue );
            var tb_machine_infoData = lR_RunParamIBLL.Gettb_machine_infoEntity( tb_run_infoData.id );
            var jsonData = new {
                tb_machine_info = tb_machine_infoData,
                tb_run_info = tb_run_infoData,
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
            lR_RunParamIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strtb_machine_infoEntity)
        {
            tb_run_infoEntity entity = strEntity.ToObject<tb_run_infoEntity>();
            tb_machine_infoEntity tb_machine_infoEntity = strtb_machine_infoEntity.ToObject<tb_machine_infoEntity>();
            lR_RunParamIBLL.SaveEntity(keyValue,entity,tb_machine_infoEntity);
            return Success("保存成功！");
        }
        #endregion

    }
}
