using Learun.Application.TwoDevelopment.LR_Desktop;
using Learun.Util;
using System.Web.Mvc;
using Learun.Application.Base.SystemModule;
using System;
using Newtonsoft.Json.Linq;
using Learun.DataBase.Repository;
using MongoDB.Bson;

namespace Learun.Application.Web.Areas.LR_Desktop.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2018-09-25 11:32
    /// 描 述：图标配置
    /// </summary>
    public class DTChartController : MvcControllerBase
    {
        private DTChartIBLL dTChartIBLL = new DTChartBLL();
        private DatabaseLinkIBLL databaseLinkIbll = new DatabaseLinkBLL();
        private DTChartIBLL1 dTChartIBLL1 = new DTChartBLL1();
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

            var data = dTChartIBLL.GetPageList(paginationobj, queryJson);
            if (paginationobj == null)
            {

                return JsonResult(data);
            }
            else
            {
                var jsonData = new
                {
                    rows = data,
                    total = paginationobj.total,
                    page = paginationobj.page,
                    records = paginationobj.records
                };
                return JsonResult(jsonData);
            }


        }
        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var LR_DT_ChartData = dTChartIBLL.GetLR_DT_ChartEntity(keyValue);
            var jsonData = new
            {
                LR_DT_Chart = LR_DT_ChartData,
            };
            return JsonResult(jsonData);
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
            dTChartIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, DTChartEntity entity)
        {

            dTChartIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSqlData(string Id)
        {


            var dtListEntity = dTChartIBLL.GetLR_DT_ChartEntity(Id);
            var reqDataTable = databaseLinkIbll.FindTable(dtListEntity.F_DataSourceId.Trim(), dtListEntity.F_Sql);
            //////////////////////////////////////////////////////////////////雄邦阀板取消20190505  zk
            /*if (Id == "c1e341d3-f956-4830-9ff1-9e588c7546d4")//首页饼状图
            {
                reqDataTable.Clear();
                //int run_state = 0;
                DateTime dtime = DateTime.Now.AddSeconds(-10);

                JObject queryParam1 = new JObject();
                JObject queryParam2 = new JObject();
                JObject queryParam3 = new JObject();
                JObject queryParam4 = new JObject();
                JObject queryParam5 = new JObject();
                //queryParam.Add("machine_id", machine_id);
                queryParam1.Add("time_second", dtime.Ticks.ToString());
                queryParam1.Add("run_state", 1);
                queryParam2.Add("time_second", dtime.Ticks.ToString());
                queryParam2.Add("run_state", 2);
                queryParam3.Add("time_second", dtime.Ticks.ToString());
                queryParam3.Add("run_state", 3);
                queryParam4.Add("time_second", dtime.Ticks.ToString());
                queryParam4.Add("run_state", 4);
                queryParam5.Add("time_second", dtime.Ticks.ToString());
                queryParam5.Add("run_state", 0);
                RepositoryFactory BR = new RepositoryFactory();
                var data1 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam1);
                var num1= data1.Count.ToDouble();//运行
                var data2 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam2);
                var num2 = data2.Count.ToDouble();//报警
                var data3 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam3);
                var num3 = data3.Count.ToDouble();//暂停
                var data4 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam4);
                var num4 = data4.Count.ToDouble();//空闲
                var data5 = BR.BaseRepository("MongoDB").FindMGList<BsonDocument>("clc_run_state", queryParam5);
                var num5 = data5.Count.ToDouble();//停机

                // reqDataTable = databaseLinkIbll.FindTable(dtListEntity.F_DataSourceId.Trim(), dtListEntity.F_Sql);
                //reqDataTable.Columns.Add("value", typeof(int));
                //reqDataTable.Columns.Add("name", typeof(string));
                reqDataTable.Rows.Add(num1,1, "运行");//Add里面参数的数据顺序要和dt中的列的顺序对应 
                reqDataTable.Rows.Add(num2,2, "报警");
                reqDataTable.Rows.Add(num3,3, "暂停");
                reqDataTable.Rows.Add(num4,4, "空闲");
                reqDataTable.Rows.Add(num5,0, "停机");
            }*/
            var jsonData = new
            {
                Id = Id,
                value = reqDataTable,

            };
            return JsonResult(jsonData);
        }

        #endregion



        /////////////////////////////////////////////////////////////////////////////

        #region  视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index1()
        {
            return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form1()
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
        public ActionResult GetPageList1(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();

            var data = dTChartIBLL1.GetPageList1(paginationobj, queryJson);
            if (paginationobj == null)
            {

                return JsonResult(data);
            }
            else
            {
                var jsonData = new
                {
                    rows = data,
                    total = paginationobj.total,
                    page = paginationobj.page,
                    records = paginationobj.records
                };
                return JsonResult(jsonData);
            }


        }
        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData1(string keyValue)
        {
            var LR_DT_ChartData = dTChartIBLL1.GetLR_DT_ChartEntity(keyValue);
            var jsonData = new
            {
                LR_DT_Chart = LR_DT_ChartData,
            };
            return JsonResult(jsonData);
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
        public ActionResult DeleteForm1(string keyValue)
        {
            dTChartIBLL1.DeleteEntity(keyValue);
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
        public ActionResult SaveForm1(string keyValue, DTChartEntity1 entity)
        {

            dTChartIBLL1.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSqlData1(string Id)
        {


            var dtListEntity = dTChartIBLL1.GetLR_DT_ChartEntity(Id);
            var reqDataTable = databaseLinkIbll.FindTable(dtListEntity.F_DataSourceId.Trim(), dtListEntity.F_Sql);
            var jsonData = new
            {
                Id = Id,
                value = reqDataTable,

            };
            return JsonResult(jsonData);
        }

        #endregion
    }
}
