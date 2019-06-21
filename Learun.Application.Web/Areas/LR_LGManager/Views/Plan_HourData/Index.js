/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-01-16 15:37
 * 描  述：小时计划产量维护
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
               learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/Plan_HourData/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/Plan_HourData/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                   learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/Plan_HourData/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/Plan_HourData/GetPageList',
                headData: [
                    { label: "0点", name: "plan00", width: 100, align: "left"},
                    { label: "1点", name: "plan01", width: 100, align: "left"},
                    { label: "2点", name: "plan02", width: 100, align: "left"},
                    { label: "3点", name: "plan03", width: 100, align: "left"},
                    { label: "4点", name: "plan04", width: 100, align: "left"},
                    { label: "5点", name: "plan05", width: 100, align: "left"},
                    { label: "6点", name: "plan06", width: 100, align: "left"},
                    { label: "7点", name: "plan07", width: 100, align: "left"},
                    { label: "8点", name: "plan08", width: 100, align: "left"},
                    { label: "9点", name: "plan09", width: 100, align: "left"},
                    { label: "10点", name: "plan10", width: 100, align: "left"},
                    { label: "11点", name: "plan11", width: 100, align: "left"},
                    { label: "12点", name: "plan12", width: 100, align: "left"},
                    { label: "13点", name: "plan13", width: 100, align: "left"},
                    { label: "14点", name: "plan14", width: 100, align: "left"},
                    { label: "15点", name: "plan15", width: 100, align: "left"},
                    { label: "16点", name: "plan16", width: 100, align: "left"},
                    { label: "17点", name: "plan17", width: 100, align: "left"},
                    { label: "18点", name: "plan18", width: 100, align: "left"},
                    { label: "19点", name: "plan19", width: 100, align: "left"},
                    { label: "20点", name: "plan20", width: 100, align: "left"},
                    { label: "21点", name: "plan21", width: 100, align: "left"},
                    { label: "22点", name: "plan22", width: 100, align: "left"},
                    { label: "23点", name: "plan23", width: 100, align: "left"},
                    { label: "日期", name: "read_time", width: 100, align: "left"},
                ],
                mainId:'F_FileId',
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
