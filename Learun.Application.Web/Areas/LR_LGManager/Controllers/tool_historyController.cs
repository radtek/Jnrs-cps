﻿using Learun.Util;
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
    /// 日 期：2019-06-04 14:05
    /// 描 述：刀具寿命管理
    /// </summary>
    public class tool_historyController : MvcControllerBase
    {
        private tool_historyIBLL tool_historyIBLL = new tool_historyBLL();

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
            var data = tool_historyIBLL.GetPageList(paginationobj, queryJson);
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
            var tb_mac_tool_historyData = tool_historyIBLL.Gettb_mac_tool_historyEntity( keyValue );
            var jsonData = new {
                tb_mac_tool_history = tb_mac_tool_historyData,
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
            tool_historyIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            tb_mac_tool_historyEntity entity = strEntity.ToObject<tb_mac_tool_historyEntity>();
            tool_historyIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
