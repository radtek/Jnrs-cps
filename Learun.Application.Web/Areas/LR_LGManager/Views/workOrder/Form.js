/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-02-14 15:59
 * 描  述：保养工单维护
 */
var acceptClick;
var save;
var keyValue = request('keyValue');
// 设置权限
var setAuthorize;
// 设置表单数据
var setFormData;
// 验证数据是否填写完整
var validForm;

var bootstrap = function ($, learun) {
    "use strict";
    var isUpdate = false;
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/workOrder/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id ).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            }
        }
    };
    // 设置权限
    setAuthorize = function (data) {
        for (var i = 0, l = data.length; i < l; i++) {
            var field = data[i];
            if (field.isLook != 1) {// 如果没有查看权限就直接移除
                $('#' + field.fieldId).parent().remove();
            }
            else {
                if (field.isEdit != 1) {
                    $('#' + field.fieldId).attr('disabled', 'disabled');
                    if ($('#' + field.fieldId).hasClass('lrUploader-wrap')) {
                        $('#' + field.fieldId).css({ 'padding-right': '58px' });
                        $('#' + field.fieldId).find('.btn-success').remove();
                    }
                }
            }
        }
    };
    // 设置表单数据
    setFormData = function (processId) {
        if (!!processId) {
            $.lrSetForm(top.$.rootUrl + '/LR_LGManager/workOrder/GetFormData?keyValue=' + processId, function (data) {//
                //if (!!data) {
                //    isUpdate = true;
                //    $('#form').lrSetFormData(data);
                //}
                for (var id in data) {
                    if (!!data[id].length && data[id].length > 0) {
                        $('#' + id).jfGridSet('refreshdata', data[id]);
                    }
                    else {
                        $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                    }
                }
            });
        }
    };
    // 验证数据是否填写完整
    validForm = function () {
        if (!$('#form').lrValidform()) {
            return false;
        }
        return true;
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {};
        postData.strtb_plan_cycle_exe_dataEntity = JSON.stringify($('[data-table="tb_plan_cycle_exe_data"]').lrGetFormData());
        postData.strEntity = JSON.stringify($('[data-table="tb_plan_cycle_data"]').lrGetFormData());
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/workOrder/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    save = function (processId, callBack, i) {
        //var postData = $('#form').lrGetFormData(keyValue);
        var postData = {};
        postData.strtb_plan_cycle_exe_dataEntity = JSON.stringify($('[data-table="tb_plan_cycle_exe_data"]').lrGetFormData());
        postData.strEntity = JSON.stringify($('[data-table="tb_plan_cycle_data"]').lrGetFormData());
        if (isUpdate) {
            keyValue = processId;
        }
        else {
            postData.id = processId;
            keyValue = processId;
        }
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/workOrder/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack(res, postData, i);
            }
        });
    };
    page.init();
}
