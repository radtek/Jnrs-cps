using Learun.Util;
using MachineDesign.Models;
using SiteJnrs.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using sModels = SiteJnrs.Models;
using vModels = SiteXBFb.Areas.Visual.Models;
namespace Learun.Application.Web.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬
    /// 日 期：2017.03.09
    /// 描 述：报表模板
    /// </summary>
    public class ReportTemplateController : MvcControllerBase
    {
        vModels.V_ReadData trd = new vModels.V_ReadData();
        vModels.L_ReadData tld = new vModels.L_ReadData();
        vModels.VT_ReadData rd = new vModels.VT_ReadData();
        sModels.ShiftInfo si = new sModels.ShiftInfo();
        vModels.V_WriteData wd = new vModels.V_WriteData();
        #region  视图功能
        /// <summary>
        /// 采购报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PurchaseReport()
        {
            return View();
        }
        /// <summary>
        /// 销售报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SalesReport()
        {
            return View();
        }
        /// <summary>
        /// 仓库报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StockReport()
        {
            return View();
        }
        /// <summary>
        /// 收支报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FinanceReport()
        {
            return View();
        }
        /// <summary>
        /// 北京展
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult bjz()
        {
            return View();
        }
        /// <summary>
        /// 阀板线看板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexTLine()
        {
            return View();
        }

        public ActionResult StateALine()
        {
            return View();
        }
        /// <summary>
        /// 阀板线看板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult sessionKeeper()
        {
            return View();
        }
        /// <summary>
        /// 运行状态报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunStateReport()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        /// <summary>
        /// OEE报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OEEReport()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        /// <summary>
        /// 时间开动率报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TimeRateReport()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        /// <summary>
        /// 性能开动率报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PerformanceRateReport()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        /// <summary>
        /// 合格品率报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QualifiedRateReport()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            return View();
        }
        #endregion

        #region  获取数据
        /// <summary>
        /// 获取采购报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPurchaseReportList()
        {
            var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/PurchaseReport.xlsx"));
            return JsonResult(data);
        }
        /// <summary>
        /// 获取销售报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSalesReportList()
        {
            var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/SalesReport.xlsx"));
            return JsonResult(data);
        }
        /// <summary>
        /// 获取仓库报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStockReportList()
        {
            var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/StockReport.xlsx"));
            return JsonResult(data);
        }
        /// <summary>
        /// 获取收支报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFinanceReportList()
        {
            var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/FinanceReport.xlsx"));
            return JsonResult(data);
        }
        /// <summary>
        /// 获取设备状态报表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRunStateReportList()
        {
            //var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/RunStateReport.xlsx"));
            //return JsonResult(data);
            ReadData rd = new ReadData();
            List<ProductSchedual> data = null;

            int machine_id = 0;
            DateTime start_date = ShiftInfo.ShiftToDay;
            DateTime end_date = ShiftInfo.ShiftToDay;

            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            if (!string.IsNullOrEmpty(Request["end_date"]))
            {
                end_date = Convert.ToDateTime(Request["end_date"]);
            }

            if (rd.initDBConnect())
            {
                data = rd.readProductSchedual("GetQueryInfo", machine_id, start_date, end_date);
            }

            PageProductSchedual.ListProdSch = data;
            ViewData["ProductSchedual"] = data;

            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }

        /// <summary>
        /// 获取设备状态报表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOEEReportList()
        {
            //var data = ExcelHelper.ExcelImport(Server.MapPath("~/Areas/LR_ReportModule/Views/ReportTemplate/ReportData/RunStateReport.xlsx"));
            //return JsonResult(data);
            ReadData rd = new ReadData();
            List<ProductSchedual> data = null;

            int machine_id = 0;
            string dayorshift = "";
            DateTime start_date = ShiftInfo.ShiftToDay;
            DateTime end_date = ShiftInfo.ShiftToDay;

            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            if (!string.IsNullOrEmpty(Request["end_date"]))
            {
                end_date = Convert.ToDateTime(Request["end_date"]);
            }

            if (!string.IsNullOrEmpty(Request["dayorshift"]))
            {
                dayorshift = Request["dayorshift"].ToString();;
            }

            if (rd.initDBConnect())
            {
                data = rd.readOEESchedual("GetQueryInfo", machine_id, start_date, end_date, dayorshift);
            }

            PageProductSchedual.ListProdSch = data;
            ViewData["ProductSchedual"] = data;

            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }

        public ActionResult UsedTime()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            ReadData rd = new ReadData();
            DateTime start_date = DateTime.Today;
            DateTime end_date = DateTime.Today;
            List<DateTime> listSubTime = new List<DateTime>();
            DateTime vtime = start_date;

            TimeSpan ts1 = new TimeSpan(end_date.AddDays(1).Ticks);
            TimeSpan ts2 = new TimeSpan(start_date.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            int dur_length = Convert.ToInt32(Math.Round(ts.TotalSeconds, 0));
            int sub_length = dur_length / 10;
            for (int i = 0; i < 10; i++)
            {
                vtime = vtime.AddSeconds(sub_length);
                listSubTime.Add(vtime);
            }
           // log.Info(dur_length);

            ViewBag.StartSubTime = start_date;
            ViewBag.ListSubTime = listSubTime;

            //RunCountData maxDur = null;
            SecTimeSeque totDurSeq = null;

            if (rd.initDBConnect())
            {
                List<MachineInfo> lmi = rd.readMainMachineInfo(0);
                List<UsedTimeSeque> listUsedTime = new List<UsedTimeSeque>();

                foreach (MachineInfo mi in lmi)
                {
                    List<UsedTimeSeque> lst = rd.readUsedTimeSeque(mi.group_id, mi.machine_id, mi.sets_no, start_date, end_date, dur_length, out totDurSeq);
                    foreach (UsedTimeSeque uts in lst)
                    {
                        listUsedTime.Add(uts);
                    }
                }

                //ViewBag.MaxRunDur = maxDur;
                ViewBag.TotDurSeque = totDurSeq;
                ViewBag.DurLength = dur_length;
                ViewData["ListUsedTime"] = listUsedTime;
                ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            }

            return View();
        }
        public ActionResult GetDeviceUsedTime_His()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            int group_id = 0;
            int machine_id = 0;
            string sets_no = "";
            DateTime start_date = DateTime.Now;
            DateTime end_date = DateTime.Now;

            if (!string.IsNullOrEmpty(Request["group_id"]))
            {
                group_id = Convert.ToInt32(Request["group_id"]);
            }
            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["sets_no"]))
            {
                sets_no = Request["sets_no"].ToString();
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            if (!string.IsNullOrEmpty(Request["end_date"]))
            {
                end_date = Convert.ToDateTime(Request["end_date"]);
            }
            ReadData rd = new ReadData();
            int dur_length = 0;
            List<DateTime> listSubTime = rd.readUsedTimeAxis(start_date, end_date, out dur_length);
            DateTime vtime = start_date;

            ListSequeData listsd = new ListSequeData();
            //RunCountData maxDur = null;
            SecTimeSeque totDurSeq = null;

            if (rd.initDBConnect())
            {
                listsd.listUsedTimeSeque = rd.readDeviceUsedTimeSeque_His(group_id, machine_id, sets_no, start_date, end_date, dur_length, out totDurSeq);
                //listsd.stateNum = maxDur;
                listsd.totDurSeque = totDurSeq;

                if (Request.IsAjaxRequest())
                {
                    return Json(listsd, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(listsd);
                }
            }

            return PartialView();
        }
        public ActionResult GetSLineDevice(string id)
        {
            ReadData rd = new ReadData();
            int group_id = Convert.ToInt32(id);
            if (rd.initDBConnect())
            {
                List<MachineInfo> data = rd.readSLineDeviceNum(group_id);

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult GetDateTime()
        {
            string data = string.Format("{0:s}", DateTime.Now);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetLineSeries(string id)
        {
            int group_id = 0;
            ReadData rd = new ReadData();
            String method = Request.HttpMethod.ToUpper();
            if (method == "GET")
            {
                if (Request["group_id"] != null)
                {
                    group_id = Convert.ToInt32(Request["group_id"]);
                }
            }

            if (rd.initDBConnect())
            {
                List<MachineSeries> data = rd.readLineSeries(group_id);

                if (Request.IsAjaxRequest())
                {

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult GetLineSeriesDevice(string id)
        {
            int group_id = 0;
            int series_id = 0;
            string machine_series = "";
            ReadData rd = new ReadData();
            String method = Request.HttpMethod.ToUpper();
            if (method == "GET")
            {
                if (Request["group_id"] != null)
                {
                    group_id = Convert.ToInt32(Request["group_id"]);
                }
                if (Request["series_id"] != null)
                {
                    series_id = Convert.ToInt32(Request["series_id"]);
                }
                if (Request["machine_series"] != null)
                {
                    machine_series = Request["machine_series"].ToString();
                }
            }

            if (rd.initDBConnect())
            {
                List<MachineInfo> data = rd.GetLineSeriesMachine(group_id, series_id, machine_series);

                if (Request.IsAjaxRequest())
                {

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult DeviceRealState(string id)
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            ReadData rd = new ReadData();
            if (rd.initDBConnect())
            {
                ViewBag.ID = id;

                List<DeviceLineNum> listDLN = rd.readRealDeviceLineNum();

                ViewBag.MinGroup = "";
                if (listDLN.Count > 0)
                {
                    ViewBag.MinGroup = listDLN[0].group_id + listDLN[0].sets_no;
                }
                ViewData["ListDeviceLineNum"] = listDLN;

            }

            return View();
        }
        public ActionResult GetDeviceRealState(string id)
        {
            ReadData rd = new ReadData();
            if (rd.initDBConnect())
            {
                List<string> data = rd.GetMGPageList(id);

                if (Request.IsAjaxRequest())
                {

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
                
            }

            return PartialView();
        }
        public ActionResult GetTotSecDurSeq_His()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            ReadData rd = new ReadData();
            int group_id = 0;
            int machine_id = 0;
            string sets_no = "";
            DateTime start_date = ShiftInfo.ShiftToDay;
            DateTime end_date = ShiftInfo.ShiftToDay;

            if (!string.IsNullOrEmpty(Request["group_id"]))
            {
                group_id = Convert.ToInt32(Request["group_id"]);
            }
            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["sets_no"]))
            {
                sets_no = Request["sets_no"].ToString();
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            if (!string.IsNullOrEmpty(Request["end_date"]))
            {
                end_date = Convert.ToDateTime(Request["end_date"]);
            }

            List<DateTime> listSubTime = new List<DateTime>();
            DateTime vtime = start_date;
            int dur_length = rd.readDiffTotalSeconds(start_date, end_date.AddDays(1));

            RunCountData stateNum = null;
            SecTimeSeque sts = null;
            SecTimeSeque totDurSeq = null;
            List<SecTimeSeque> listSecDurSeq = null;
            ListSequeData listsd = new ListSequeData();
            listsd.listSecDurSeque = new List<SecTimeSeque>();

            if (rd.initDBConnect())
            {
                rd.readSeqTotDurSeq_His(group_id, machine_id, sets_no, start_date, end_date, dur_length, out stateNum, out totDurSeq, out listSecDurSeq);

                listsd.stateNum = stateNum;
                listsd.totDurSeque = totDurSeq;
                //listsd.listSecDurSeque = listSecDurSeq;
                listsd.DurLength = dur_length;

                int days = rd.getDiffTotalDays(start_date, end_date);
                int years = end_date.Year - start_date.Year;
                int months = years * 12 + (end_date.Month - start_date.Month);
                int mdays = start_date.AddDays(1 - start_date.Day).AddMonths(1).AddDays(-1).Day;

                if (months > 12)
                {
                    for (int i = 0; i <= years; i++)
                    {
                        sts = new SecTimeSeque();
                        sts.id = i;

                        sts.run_duration = 0;
                        sts.alarm_duration = 0;
                        sts.pause_duration = 0;
                        sts.ready_duration = 0;
                        sts.stop_duration = 0;
                        sts.edit_duration = 0;

                        for (int vj = 0; vj < listSecDurSeq.Count - 1; vj++)
                        {
                            if (listSecDurSeq[vj].calc_date.Substring(0, 4).Equals((start_date.Year + i).ToString()))
                            {
                                sts.run_duration += listSecDurSeq[vj].run_duration;
                                sts.alarm_duration += listSecDurSeq[vj].alarm_duration;
                                sts.pause_duration += listSecDurSeq[vj].pause_duration;
                                sts.ready_duration += listSecDurSeq[vj].ready_duration;
                                sts.stop_duration += listSecDurSeq[vj].stop_duration;
                                sts.edit_duration += listSecDurSeq[vj].edit_duration;
                            }
                        }
                        sts.calc_date = (start_date.Year + i).ToString();
                        //sts.read_time = listSecDurSeq[i].read_time;
                        listsd.listSecDurSeque.Add(sts);
                    }
                    listsd.listSecDurSeque.Add(listSecDurSeq[listSecDurSeq.Count - 1]);
                }
                else
                {
                    if (months >= 1 && days > mdays)
                    {
                        for (int i = 0; i <= months; i++)
                        {
                            DateTime tmpDate = start_date.AddMonths(i);

                            sts = new SecTimeSeque();
                            sts.id = i;

                            sts.run_duration = 0;
                            sts.alarm_duration = 0;
                            sts.pause_duration = 0;
                            sts.ready_duration = 0;
                            sts.stop_duration = 0;
                            sts.edit_duration = 0;

                            for (int vj = 0; vj < listSecDurSeq.Count - 1; vj++)
                            {
                                if (listSecDurSeq[vj].calc_date.Substring(0, 6).Equals(tmpDate.ToString("yyyyMM")))
                                {
                                    sts.run_duration += listSecDurSeq[vj].run_duration;
                                    sts.alarm_duration += listSecDurSeq[vj].alarm_duration;
                                    sts.pause_duration += listSecDurSeq[vj].pause_duration;
                                    sts.ready_duration += listSecDurSeq[vj].ready_duration;
                                    sts.stop_duration += listSecDurSeq[vj].stop_duration;
                                    sts.edit_duration += listSecDurSeq[vj].edit_duration;
                                }
                            }
                            sts.calc_date = tmpDate.ToString("yyyyMM");
                            //sts.read_time = listSecDurSeq[i].read_time;
                            listsd.listSecDurSeque.Add(sts);
                        }
                        listsd.listSecDurSeque.Add(listSecDurSeq[listSecDurSeq.Count - 1]);
                    }
                    else
                    {
                        for (int i = 0; i <= days; i++)
                        {
                            listsd.listSecDurSeque = listSecDurSeq;
                        }
                    }
                }

                if (Request.IsAjaxRequest())
                {
                    return Json(listsd, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(listSecDurSeq);
                }
            }

            return PartialView();
        }
        public ActionResult GetShiftProdNum_His()
        {
            int group_id = 0;
            int machine_id = 0;
            DateTime start_date = ShiftInfo.ShiftToDay;
            DateTime end_date = ShiftInfo.ShiftToDay;
            ReadData rd = new ReadData();
            if (!string.IsNullOrEmpty(Request["group_id"]))
            {
                group_id = Convert.ToInt32(Request["group_id"]);
            }
            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            if (!string.IsNullOrEmpty(Request["end_date"]))
            {
                end_date = Convert.ToDateTime(Request["end_date"]);
            }

            if (rd.initDBConnect())
            {
                List<MachineInfo> data = rd.readShiftProdNum_His(group_id, machine_id, start_date, end_date);

                if (Request.IsAjaxRequest())
                {

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult RecordSecDurSeq_His()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            //WriteData wd = new WriteData();
            int group_id = 0;
            int machine_id = 0;
            string sets_no = "";
            DateTime start_date = ShiftInfo.ShiftToDay;
            DateTime end_date = ShiftInfo.ShiftToDay;
            DateTime dtime = DateTime.Now;
            string Run_time = "";
            string Stop_time = "";
            string Free_time = "";
            if (!string.IsNullOrEmpty(Request["group_id"]))
            {
                group_id = Convert.ToInt32(Request["group_id"]);
            }
            if (!string.IsNullOrEmpty(Request["machine_id"]))
            {
                machine_id = Convert.ToInt32(Request["machine_id"]);
            }
            if (!string.IsNullOrEmpty(Request["sets_no"]))
            {
                sets_no = Request["sets_no"].ToString();
            }
            if (!string.IsNullOrEmpty(Request["start_date"]))
            {
                start_date = Convert.ToDateTime(Request["start_date"]);
            }
            //if (!string.IsNullOrEmpty(Request["end_date"]))
            //{
            //    end_date = Convert.ToDateTime(Request["end_date"]);
            //}
            if (!string.IsNullOrEmpty(Request["Run_time"]))
            {
                Run_time = Convert.ToString(Request["Run_time"]);
            }
            if (!string.IsNullOrEmpty(Request["Stop_time"]))
            {
                Stop_time = Convert.ToString(Request["Stop_time"]);
            }
            if (!string.IsNullOrEmpty(Request["Free_time"]))
            {
                Free_time = Convert.ToString(Request["Free_time"]);
            }

            string strSql = " insert into tb_Record_Dur(group_id,machine_id,Run_time,Stop_time,Free_time,read_time) values (" + group_id + "," + machine_id + ",'" + Run_time + "','" + Stop_time + "','" + Free_time + "','" + dtime + "')";

            //wd.putRecord_Dur(strSql);

            return View();
        }
        public ActionResult UsedTime2()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            ReadData rd = new ReadData();
            DateTime start_date = DateTime.Today;
            DateTime end_date = DateTime.Today;
            List<DateTime> listSubTime = new List<DateTime>();
            DateTime vtime = start_date;

            TimeSpan ts1 = new TimeSpan(end_date.AddDays(1).Ticks);
            TimeSpan ts2 = new TimeSpan(start_date.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            int dur_length = Convert.ToInt32(Math.Round(ts.TotalSeconds, 0));
            int sub_length = dur_length / 10;
            for (int i = 0; i < 10; i++)
            {
                vtime = vtime.AddSeconds(sub_length);
                listSubTime.Add(vtime);
            }
            // log.Info(dur_length);

            ViewBag.StartSubTime = start_date;
            ViewBag.ListSubTime = listSubTime;

            //RunCountData maxDur = null;
            SecTimeSeque totDurSeq = null;

            if (rd.initDBConnect())
            {
                List<MachineInfo> lmi = rd.readMainMachineInfo(0);
                List<UsedTimeSeque> listUsedTime = new List<UsedTimeSeque>();

                foreach (MachineInfo mi in lmi)
                {
                    List<UsedTimeSeque> lst = rd.readUsedTimeSeque(mi.group_id, mi.machine_id, mi.sets_no, start_date, end_date, dur_length, out totDurSeq);
                    foreach (UsedTimeSeque uts in lst)
                    {
                        listUsedTime.Add(uts);
                    }
                }

                //ViewBag.MaxRunDur = maxDur;
                ViewBag.TotDurSeque = totDurSeq;
                ViewBag.DurLength = dur_length;
                ViewData["ListUsedTime"] = listUsedTime;
                ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            }

            return View();
        }

        public ActionResult MachineState()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            ReadData rd = new ReadData();
            DateTime start_date = DateTime.Today;
            DateTime end_date = DateTime.Today;
            List<DateTime> listSubTime = new List<DateTime>();
            DateTime vtime = start_date;

            TimeSpan ts1 = new TimeSpan(end_date.AddDays(1).Ticks);
            TimeSpan ts2 = new TimeSpan(start_date.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            int dur_length = Convert.ToInt32(Math.Round(ts.TotalSeconds, 0));
            int sub_length = dur_length / 10;
            for (int i = 0; i < 10; i++)
            {
                vtime = vtime.AddSeconds(sub_length);
                listSubTime.Add(vtime);
            }
            // log.Info(dur_length);

            ViewBag.StartSubTime = start_date;
            ViewBag.ListSubTime = listSubTime;

            //RunCountData maxDur = null;
            SecTimeSeque totDurSeq = null;

            if (rd.initDBConnect())
            {
                List<MachineInfo> lmi = rd.readMainMachineInfo(0);
                List<UsedTimeSeque> listUsedTime = new List<UsedTimeSeque>();

                foreach (MachineInfo mi in lmi)
                {
                    List<UsedTimeSeque> lst = rd.readUsedTimeSeque(mi.group_id, mi.machine_id, mi.sets_no, start_date, end_date, dur_length, out totDurSeq);
                    foreach (UsedTimeSeque uts in lst)
                    {
                        listUsedTime.Add(uts);
                    }
                }

                //ViewBag.MaxRunDur = maxDur;
                ViewBag.TotDurSeque = totDurSeq;
                ViewBag.DurLength = dur_length;
                ViewData["ListUsedTime"] = listUsedTime;
                ViewData["ListDeviceLineNum"] = rd.readDeviceLineNum();
            }

            return View();
        }
        public ActionResult GetRunNumData()
        {
            ReadData rd = new ReadData();
            if (rd.initDBConnect())
            {
                List<RunStateNum> data = rd.readDeviceRunNum();

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult AllInfo()
        {
            ReadData rd = new ReadData();
            if (rd.initDBConnect())
            {
                //int group_id = Convert.ToInt32(Request["group_id"]);
                //int machine_id = Convert.ToInt32(Request["machine_id"]);
                //DateTime start_date = Convert.ToDateTime(Request["start_date"]);
                //DateTime end_date = Convert.ToDateTime(Request["end_date"]);
                int group_id = 11;
                int machine_id = 11;
                DateTime start_date = new DateTime();
                DateTime end_date = new DateTime();
                List <MachineInfo> data = rd.readDeviceProdNum(group_id, machine_id, start_date, end_date);

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult MachineInfo()
        {
            ReadData rd = new ReadData();
            if (rd.initDBConnect())
            {
                //int group_id = Convert.ToInt32(Request["group_id"]);
                //int machine_id = Convert.ToInt32(Request["machine_id"]);
                //DateTime start_date = Convert.ToDateTime(Request["start_date"]);
                //DateTime end_date = Convert.ToDateTime(Request["end_date"]);
                int group_id = 11;
                int machine_id = 11;
                DateTime start_date = new DateTime();
                DateTime end_date = new DateTime();
                List<MachineInfo> data = rd.readDeviceProdNum(group_id, machine_id, start_date, end_date);

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }
            }

            return PartialView();
        }
        public ActionResult GetRealTime()
        {
            //DateTime data = DateTime.Now;
            string data = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToShortTimeString();

            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetBLDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVLDeviceRunState(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.GisNumvCLine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        private List<sModels.DeviceRealState> readVLDeviceRunState(string gis_numv, string mis_numv)
        {
            List<sModels.DeviceRealState> data = null;
            if (trd.initDBConnect())
            {
                //List<sModels.DeviceRealState> vdata = rd.readDeviceRunState(sModels.RunStateParam.EditState, gis_numv, mis_numv);
                List<sModels.DeviceRealState> tdata = trd.readTDeviceRunState(gis_numv, mis_numv);
                data = new List<sModels.DeviceRealState>();

                for (int i = 0; i < tdata.Count; i++)
                {
                    data.Add(tdata[i]);
                }
            }
            else
            {
                data = new List<sModels.DeviceRealState>();
            }
            return data;
        }
        public ActionResult GetBLRDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVLRobDeviceRunState(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        private List<sModels.DeviceRealState> readVLRobDeviceRunState(string gis_numv, string mis_numv)
        {
            List<sModels.DeviceRealState> data = null;
            if (rd.initDBConnect())
            {
                //List<sModels.DeviceRealState> vdata = rd.readDeviceRunState(sModels.RunStateParam.EditState, gis_numv, mis_numv);
                List<sModels.DeviceRealState> tdata = rd.readTRobDeviceRunState(gis_numv, mis_numv);
                data = new List<sModels.DeviceRealState>();

                for (int i = 0; i < tdata.Count; i++)
                {
                    data.Add(tdata[i]);
                }
            }
            else
            {
                data = new List<sModels.DeviceRealState>();
            }
            return data;
        }
        public ActionResult GetBL1RealProdNum()
        {
            List<sModels.MachineInfo> data = readLRealProdNum(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "L");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        private List<sModels.MachineInfo> readLRealProdNum(string gis_numv, string mis_numv, string mac_prog)
        {
            List<sModels.MachineInfo> data = null;
            List<sModels.MachineInfo> tdata = null;
            if (trd.initDBConnect())
            {
                data = trd.readTRealProdNum(gis_numv, mis_numv, mac_prog);
                tdata = trd.readTRealProdMTotNum(gis_numv, mis_numv, mac_prog);

                foreach (sModels.MachineInfo tmi in tdata)
                {
                    sModels.MachineInfo mi = data.Find(delegate (sModels.MachineInfo vmi)
                    {
                        return vmi.group_id == tmi.group_id;
                    });
                    if (mi != null)
                    {
                        mi.tot_prod_num += tmi.prod_num;
                    }
                }
            }

            return data;
        }
        public ActionResult GetBL2RealProdNum()
        {
            List<sModels.MachineInfo> data = readLRealProdNum(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "R");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetBL1RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();
            string line_target = "";
            if (method == "POST")
            {
                int data = 0;
                if (Request["line_target"] != null && Request["line_target"] != "")
                {
                    line_target = Request["line_target"].ToString();
                }
                //data = tld.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L", "PlanProdNum");

                switch (line_target)
                {
                    case "066cd":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum");
                        break;
                    case "065cd":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.GisNumvALine, "L", "PlanProdNum");
                        break;
                    case "066ab":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum");
                        break;
                    case "065ab":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum");
                        break;
                    case "066ef":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "L", "PlanProdNum");
                        break;
                    case "065ef":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "L", "PlanProdNum");
                        break;
                    case "066gh":;
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "R", "PlanProdNum");
                        break;
                    case "065gh":
                        data = tld.readShiftData(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "R", "PlanProdNum");
                        break;
                }

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBL2RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = trd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = trd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Mid");
                    else data = trd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Nig");
                }
                else
                {
                    data = trd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult UpdateBPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            int plan_prod_num = -1;
            int plan_prod_mor = 0;
            int plan_prod_mid = 0;
            int plan_prod_nig = 0;
            string line_target = "";
            if (method == "POST")
            {
                if (Request["plan_prod_num"] != null && Request["plan_prod_num"] != "")
                {
                    plan_prod_num = Convert.ToInt32(Request["plan_prod_num"]);
                }
                if (Request["plan_prod_mor"] != null && Request["plan_prod_mor"] != "")
                {
                    plan_prod_mor = Convert.ToInt32(Request["plan_prod_mor"]);
                }
                if (Request["plan_prod_mid"] != null && Request["plan_prod_mid"] != "")
                {
                    plan_prod_mid = Convert.ToInt32(Request["plan_prod_mid"]);
                }
                if (Request["plan_prod_nig"] != null && Request["plan_prod_nig"] != "")
                {
                    plan_prod_nig = Convert.ToInt32(Request["plan_prod_nig"]);
                }
                if (Request["line_target"] != null && Request["line_target"] != "")
                {
                    line_target = Request["line_target"].ToString();
                }

                int data = 0;
                if (ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (ShiftInfo.ShiftNo.Equals("Mor")) data = plan_prod_mor;
                    else if (ShiftInfo.ShiftNo.Equals("Mid")) data = plan_prod_mid;
                    else data = plan_prod_nig;

                    plan_prod_num = plan_prod_mor + plan_prod_nig + plan_prod_nig;
                }
                else
                {
                    data = plan_prod_num;
                    double avg = plan_prod_num / 3;
                    plan_prod_mor = (int)Math.Ceiling(avg);
                    plan_prod_mid = plan_prod_mor;
                    plan_prod_nig = plan_prod_num - plan_prod_mor - plan_prod_mid;
                }
                switch (line_target)
                {
                    case "066cd":
                        wd.updatePlanProdNum("L",ReadParameter.GisNumvBLine, ReadParameter.MisNumvBLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "065cd":
                        wd.updatePlanProdNum("L",ReadParameter.GisNumvALine, ReadParameter.MisNumvALine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "066ab":
                        wd.updatePlanProdNum("R", ReadParameter.GisNumvBLine, ReadParameter.MisNumvBLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "065ab":
                        wd.updatePlanProdNum("R", ReadParameter.GisNumvALine, ReadParameter.MisNumvALine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "066ef":
                        wd.updatePlanProdNum("L", ReadParameter.GisNumvDLine, ReadParameter.MisNumvDLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "065ef":
                        wd.updatePlanProdNum("L", ReadParameter.GisNumvCLine, ReadParameter.MisNumvCLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "066gh":
                        wd.updatePlanProdNum("R", ReadParameter.GisNumvDLine, ReadParameter.MisNumvDLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                    case "065gh":
                        wd.updatePlanProdNum("R", ReadParameter.GisNumvCLine, ReadParameter.MisNumvCLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);
                        break;
                }
                //wd.updatePlanProdNum(ReadParameter.GisNumvBLine, ReadParameter.MisNumvBLine, plan_prod_num, plan_prod_mor, plan_prod_mid, plan_prod_nig);

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBRDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVRDeviceRunState(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        private List<sModels.DeviceRealState> readVRDeviceRunState(string gis_numv, string mis_numv)
        {
            List<sModels.DeviceRealState> data = null;
            if (rd.initDBConnect())
            {
                //List<sModels.DeviceRealState> vdata = rd.readDeviceRunState(sModels.RunStateParam.EditState, gis_numv, mis_numv);
                List<sModels.DeviceRealState> tdata = rd.readTDeviceRunState(gis_numv, mis_numv);
                data = new List<sModels.DeviceRealState>();

                for (int i = 0; i < tdata.Count; i++)
                {
                    data.Add(tdata[i]);
                }
            }
            else
            {
                data = new List<sModels.DeviceRealState>();
            }
            return data;
        }
        public ActionResult GetBR1RealProdNum()
        {
            List<sModels.MachineInfo> data = readRRealProdNum(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        private List<sModels.MachineInfo> readRRealProdNum(string gis_numv, string mis_numv, string mac_prog)
        {
            List<sModels.MachineInfo> data = null;
            List<sModels.MachineInfo> tdata = null;
            if (rd.initDBConnect())
            {
                data = rd.readTRealProdNum(gis_numv, mis_numv, mac_prog);
                tdata = rd.readTRealProdMTotNum(gis_numv, mis_numv, mac_prog);

                foreach (sModels.MachineInfo tmi in tdata)
                {
                    sModels.MachineInfo mi = data.Find(delegate (sModels.MachineInfo vmi)
                    {
                        return vmi.group_id == tmi.group_id;
                    });
                    if (mi != null)
                    {
                        mi.tot_prod_num += tmi.prod_num;
                    }
                }
            }

            return data;
        }
        public ActionResult GetBR2RealProdNum()
        {
            List<sModels.MachineInfo> data = readRRealProdNum(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetBR1RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L", "PlanProdNum_Mid");
                    else data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L", "PlanProdNum_Nig");
                }
                else
                {
                    data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBR2RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Mid");
                    else data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum_Nig");
                }
                else
                {
                    data = rd.readShiftData(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTRDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVRDeviceRunState(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTR1RealProdNum()
        {
            List<sModels.MachineInfo> data = readRRealProdNum(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTR2RealProdNum()
        {
            List<sModels.MachineInfo> data = readRRealProdNum(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTR1RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Mid");
                    else data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Nig");
                }
                else
                {
                    data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTR2RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Mid");
                    else data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Nig");
                }
                else
                {
                    data = rd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTLDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVLDeviceRunState(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTLRDeviceRunState()
        {
            List<sModels.DeviceRealState> data = readVLRobDeviceRunState(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine);
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTL1RealProdNum()
        {
            List<sModels.MachineInfo> data = readLRealProdNum(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "L");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTL2RealProdNum()
        {
            List<sModels.MachineInfo> data = readLRealProdNum(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "R");
            if (Request.IsAjaxRequest())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }
        public ActionResult GetTL1RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Mid");
                    else data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum_Nig");
                }
                else
                {
                    data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTL2RealPlanProdNum()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                int data = 0;
                if (sModels.ReadParameter.PlanProdFlag == 1)
                {
                    si.getShiftInfo();
                    if (sModels.ShiftInfo.ShiftNo.Equals("Mor")) data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Mor");
                    else if (sModels.ShiftInfo.ShiftNo.Equals("Mid")) data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Mid");
                    else data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum_Nig");
                }
                else
                {
                    data = trd.readShiftData(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R", "PlanProdNum");
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTL1RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "L");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTL2RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvDLine, sModels.ReadParameter.MisNumvDLine, "R");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBL1RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "L");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBL2RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvCLine, sModels.ReadParameter.MisNumvCLine, "R");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTR1RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "L");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetTR2RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvBLine, sModels.ReadParameter.MisNumvBLine, "R");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBR1RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "L");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        public ActionResult GetBR2RealProdNo()
        {
            String method = Request.HttpMethod.ToUpper();

            if (method == "POST")
            {
                sModels.ProductInfo data = trd.readRealProdNo(sModels.ReadParameter.GisNumvALine, sModels.ReadParameter.MisNumvALine, "R");

                if (Request.IsAjaxRequest())
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView(data);
                }

            }
            return View();
        }
        #endregion
    }
}