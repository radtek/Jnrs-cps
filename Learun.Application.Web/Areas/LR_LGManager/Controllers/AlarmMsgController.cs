using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;
using MachineDesign.Models;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 09:56
    /// 描 述：报警信息汇总
    /// </summary>
    public class AlarmMsgController : MvcControllerBase
    {
        private AlarmMsgIBLL alarmMsgIBLL = new AlarmMsgBLL();

        #region  视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
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
            var data = alarmMsgIBLL.GetPageList(paginationobj, queryJson);
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
            var tb_alarm_historyData = alarmMsgIBLL.Gettb_alarm_historyEntity( keyValue );
            var tb_machine_infoData = alarmMsgIBLL.Gettb_machine_infoEntity( tb_alarm_historyData.id );
            var jsonData = new {
                tb_machine_info = tb_machine_infoData,
                tb_alarm_history = tb_alarm_historyData,
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
            alarmMsgIBLL.DeleteEntity(keyValue);
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
            tb_alarm_historyEntity entity = strEntity.ToObject<tb_alarm_historyEntity>();
            tb_machine_infoEntity tb_machine_infoEntity = strtb_machine_infoEntity.ToObject<tb_machine_infoEntity>();
            alarmMsgIBLL.SaveEntity(keyValue,entity,tb_machine_infoEntity);
            return Success("保存成功！");
        }
        #endregion

    }
}
