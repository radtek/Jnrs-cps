/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-06-04 14:58
 * 描  述：报表测试
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var map = {};
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
            // 导出
            $('#lr_outport').on('click', function () {
                learun.download({
                    method: 'POST',
                    url: '/Utility/ExportExcel',
                    param: {
                        fileName: '导出数据列表',
                        columnJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').headData),
                        dataJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').rowdatas)
                    }
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/machineinfo_export/GetList',
                headData: [
                    { label: "machine_id", name: "machine_id", width: 100, align: "left" },
                    { label: "group_id", name: "group_id", width: 100, align: "left" },
                    { label: "machine_name", name: "machine_name", width: 100, align: "left" },
                    { label: "machine_series", name: "machine_series", width: 100, align: "left" },
                    { label: "machine_number", name: "machine_number", width: 100, align: "left" },
                    { label: "comm_protocol", name: "comm_protocol", width: 100, align: "left" },
                    { label: "comm_interface", name: "comm_interface", width: 100, align: "left" },
                    { label: "rank_num", name: "rank_num", width: 100, align: "left" },
                    { label: "category", name: "category", width: 100, align: "left" },
                    { label: "sets_no", name: "sets_no", width: 100, align: "left" },
                    { label: "is_run_state", name: "is_run_state", width: 100, align: "left" },
                    { label: "is_prod", name: "is_prod", width: 100, align: "left" },
                    { label: "run_param", name: "run_param", width: 100, align: "left" },
                    { label: "is_alarm", name: "is_alarm", width: 100, align: "left" },
                    { label: "is_program", name: "is_program", width: 100, align: "left" },
                    { label: "is_barcode", name: "is_barcode", width: 100, align: "left" },
                    { label: "mis_visual", name: "mis_visual", width: 100, align: "left" },
                    { label: "station_cnt", name: "station_cnt", width: 100, align: "left" },
                    { label: "rank_sets", name: "rank_sets", width: 100, align: "left" },
                    { label: "is_03", name: "is_03", width: 100, align: "left" },
                    { label: "is_04", name: "is_04", width: 100, align: "left" },
                    { label: "is_05", name: "is_05", width: 100, align: "left" },
                    { label: "is_06", name: "is_06", width: 100, align: "left" },
                    { label: "is_07", name: "is_07", width: 100, align: "left" },
                    { label: "is_08", name: "is_08", width: 100, align: "left" },
                    { label: "is_09", name: "is_09", width: 100, align: "left" },
                    { label: "is_main", name: "is_main", width: 100, align: "left" },
                    { label: "price", name: "price", width: 100, align: "left" },
                    { label: "manufacture", name: "manufacture", width: 100, align: "left" },
                    { label: "born_date", name: "born_date", width: 100, align: "left" },
                    { label: "buy_person", name: "buy_person", width: 100, align: "left" },
                    { label: "photo", name: "photo", width: 100, align: "left" },
                    { label: "enabled", name: "enabled", width: 100, align: "left" },
                    { label: "memo", name: "memo", width: 100, align: "left" },
                ],
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
