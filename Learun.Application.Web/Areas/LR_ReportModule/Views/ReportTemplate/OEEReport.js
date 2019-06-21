var bootstrap = function ($, learun) {
    "use strict";
    var startTime = learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6);
    var endTime = learun.getDate('yyyy-MM-dd 23:59:59');
    //var machine_id=$('#txt_Keyword').val();
    var machine_id = 11;
    var machine_name = "";
    var dayorshift = "Day";
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
                dayorshift = $('#dayorshift').val();
                var options = $("#txt_Keyword option:selected");//获取当前选择项.
                machine_name = options.text();//获取当前选择项的文本.
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
                        end_date: endTime,
                        dayorshift: dayorshift
                    },
                    url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetOEEReportList',
                    //url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetRunStateReportList',
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        $.each(data, function (vi, item) {
                            var  option = {
                                title: {
                                    text: machine_name+' OEE统计分析%'
                                },
                                tooltip: {
                                    trigger: 'axis'
                                },
                                legend: {
                                    data: ['时间开动率', '性能开动率', '合格品率', '设备综合效率(OEE)']
                                },
                                grid: {
                                    left: '3%',
                                    right: '4%',
                                    bottom: '3%',
                                    containLabel: true
                                },
                                toolbox: {
                                    feature: {
                                        saveAsImage: {}
                                    }
                                },
                                xAxis: {
                                    type: 'category',
                                    boundaryGap: false,
                                    data: item.str
                                },
                                yAxis: {
                                    type: 'value'
                                },
                                series: [
                                    {
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'top',
                                                    textStyle: {
                                                        color: '#615a5a'
                                                    },
                                                    formatter: function (params) {
                                                        if (params.value == 0) {
                                                            return '';
                                                        } else {
                                                            return params.value;
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        name: '时间开动率',
                                        type: 'line',
                                        stack: '总量',
                                        data: item.data1
                                    },
                                    {
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'top',
                                                    textStyle: {
                                                        color: '#615a5a'
                                                    },
                                                    formatter: function (params) {
                                                        if (params.value == 0) {
                                                            return '';
                                                        } else {
                                                            return params.value;
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        name: '性能开动率',
                                        type: 'line',
                                        stack: '总量',
                                        data: item.data3
                                    },
                                    {
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'top',
                                                    textStyle: {
                                                        color: '#615a5a'
                                                    },
                                                    formatter: function (params) {
                                                        if (params.value == 0) {
                                                            return '';
                                                        } else {
                                                            return params.value;
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        name: '合格品率',
                                        type: 'line',
                                        stack: '总量',
                                        data: item.data2
                                    },
                                    {
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    position: 'top',
                                                    textStyle: {
                                                        color: '#615a5a'
                                                    },
                                                    formatter: function (params) {
                                                        if (params.value == 0) {
                                                            return '';
                                                        } else {
                                                            return params.value;
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        name: '设备综合效率(OEE)',
                                        type: 'line',
                                        stack: '总量',
                                        data: item.data4
                                    }
                                ]
                            };
                            /*var  option = {
                                title: {
                                    text: machine_name+' OEE统计分析%'
                                },
                                tooltip: {
                                    trigger: 'axis'
                                },
                                legend: {
                                    data: ['时间开动率', '性能开动率', '合格品率', '设备综合效率(OEE)']
                                },
                                grid: {
                                    left: '3%',
                                    right: '4%',
                                    bottom: '3%',
                                    containLabel: true
                                },
                                toolbox: {
                                    feature: {
                                        saveAsImage: {}
                                    }
                                },
                                xAxis: {
                                    type: 'category',
                                    boundaryGap: false,
                                    data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
                                },
                                yAxis: {
                                    type: 'value'
                                },
                                series: [
                                    {
                                        name: '时间开动率',
                                        type: 'line',
                                        stack: '总量',
                                        data: [80, 90, 70, 54, 60, 50, 90]
                                    },
                                    {
                                        name: '性能开动率',
                                        type: 'line',
                                        stack: '总量',
                                        data: [40, 82, 91, 34, 10, 30, 80]
                                    },
                                    {
                                        name: '合格品率',
                                        type: 'line',
                                        stack: '总量',
                                        data: [50, 32, 81, 54, 50, 30, 60]
                                    },
                                    {
                                        name: '设备综合效率(OEE)',
                                        type: 'line',
                                        stack: '总量',
                                        data: [20, 32, 51, 34, 50, 30, 20]
                                    }
                                ]
                            };*/
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
            param.dayorshift = dayorshift;
            page.initChart();
        },
        searchs: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.dayorshift = dayorshift;
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
            //var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            var tr_txt = "";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            //alert(tr_txt);
            target.append(tr_txt);
        }
    });
}


