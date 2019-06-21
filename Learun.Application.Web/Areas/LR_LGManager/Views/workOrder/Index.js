/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-02-14 15:59
 * 描  述：保养工单维护
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var processId = '';
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
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/workOrder/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        //callBack: function (id) {
                        //    return top[id].acceptClick(refreshGirdData);
                        //}
                        callBack: function (id) {
                           
                            var res = true;
                            // 保存数据
                            if (res) {
                                processId = keyValue;
                                res = top[id].save(processId, refreshGirdData);
                            }
                            return res;
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/workOrder/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/LR_LGManager/workOrder/GetPageList',
                headData: [
                    { label: "工单号", name: "id", width: 100, align: "left"},
                    { label: "计划名称", name: "plan_maintain_name", width: 100, align: "left"},
                    { label: "设备名称", name: "machine_id", width: 100, align: "left"},
                    { label: "保养类型", name: "maintenance_id", width: 100, align: "left"},
                    { label: "开始日期", name: "begin_date", width: 100, align: "left"},
                    { label: "截止日期", name: "end_date", width: 100, align: "left"},
                    { label: "保养人", name: "executor", width: 100, align: "left"},
                    { label: "完成日期", name: "complete_date", width: 100, align: "left"},
                    { label: "完成情况", name: "complete_spec", width: 100, align: "left"},
                    { label: "备注", name: "remark", width: 100, align: "left"},
                ],
                mainId:'id',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    //refreshGirdData = function () {
    //    page.search();
    //};
    // 保存数据后回调刷新
    refreshGirdData = function (res, postData) {
        if (res.code == 200) {
            // 发起流程
            learun.workflowapi.create({
                isNew: true,
                schemeCode: 'Workorder',
                processId: processId,
                processName: '保养工单流程',
                processLevel: '1',
                description: '发起保养',
                formData: JSON.stringify(postData),
                callback: function (res, data) {
                }
            });

            console.log(res, processId);
            page.search();
        }


    }
    page.init();
}
