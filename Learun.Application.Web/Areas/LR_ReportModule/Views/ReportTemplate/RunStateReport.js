var bootstrap = function ($, learun) {
    "use strict";
    var startTime = learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6);
    var endTime = learun.getDate('yyyy-MM-dd 23:59:59');
    //var machine_id=$('#txt_Keyword').val();
    var machine_id = 11;
    $("#aLine").change(function () { SelectLineChangeDevice(); });
    SelectLineChangeDevice();
    var page = {
        init: function () {
            page.initChart();
            page.bind();
        },
        bind: function () {
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
            // 查询
            $('#btn_Search').on('click', function () {
                machine_id = $('#txt_Keyword').val();
                page.search();
               // page.searchs({ machine_id: keyword });
            });
            // 刷新
            $('#lr-replace').on('click', function () {
                location.reload();
            });
        },
        initChart: function () {
            loadmain();
            function loadmain() {
               // alert(startTime);
                var myChart = echarts.init(document.getElementById('main'));
                $.ajax({
                    data: {
                        machine_id: machine_id,
                        start_date: startTime,
                        end_date: endTime
                    },
                    url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetRunStateReportList',
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        $.each(data, function (vi, item) {
                            var option = {
                                tooltip: {
                                    trigger: 'axis',
                                    axisPointer: {
                                        type: 'shadow'
                                    }
                                },
                                color: ['green', 'red', 'orange', 'blue','black'],
                                legend: {
                                    data: ['运行时长', '报警时长', '暂停时长', '空闲时长', '停机时长']
                                },
                                toolbox: {
                                    show: true,
                                    orient: 'vertical',
                                    y: 'center',
                                    feature: {
                                        mark: { show: true },
                                        magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                                        restore: { show: true },
                                        saveAsImage: { show: true }
                                    }
                                },
                                calculable: true,
                                xAxis: [
                                    {
                                        type: 'category',
                                        boundaryGap: true,
                                        data: item.str
                                    }
                                ],
                                yAxis: [
                                    {
                                        type: 'value',
                                        boundaryGap: true,
                                        splitArea: { show: true }
                                    }
                                ],
                                grid: {
                                    x2: 40
                                },
                                series: [
                                    {
                                        name: '运行时长',
                                        type: 'bar',
                                        stack: '总量',
                                        data: item.data1
                                    },
                                    {
                                        name: '报警时长',
                                        type: 'bar',
                                        stack: '总量',
                                        data: item.data2
                                    },
                                    {
                                        name: '暂停时长',
                                        type: 'bar',
                                        stack: '总量',
                                        data: item.data3
                                    },
                                    {
                                        name: '空闲时长',
                                        type: 'bar',
                                        stack: '总量',
                                        data: item.data4
                                    },
                                    {
                                        name: '停机时长',
                                        type: 'bar',
                                        stack: '总量',
                                        data: item.data5
                                    }
                                ]
                            };
                            myChart.setOption(option);

                        });
                    }

                });
            }
        },
        //search: function (param) {
        //    param = param || {};
        //    $('#gridtable').jfGridSet('reload', param);
        //}
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            page.initChart();
        },
        searchs: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            page.initChart();
        }
    };
    page.init();
}
function SelectLineChangeDevice() {

    //获取下拉框选中项的value属性值
    var saLine = $("#aLine").val();
    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetSLineDevice/" + saLine,
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetSLineDevice/' + saLine,
        success: function (data) {
            var target = $("#txt_Keyword");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            //alert(tr_txt);
            target.append(tr_txt);
        }
    });
}


