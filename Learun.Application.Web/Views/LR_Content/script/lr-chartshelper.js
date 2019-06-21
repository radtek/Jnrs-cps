var Title_time = "";
var machine_name = "";
var bootstrap = function ($, learun) {
    var startTime = learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6);
    var endTime = learun.getDate('yyyy-MM-dd 23:59:59');
    var machine_id = 0;
    var group_id = 19;
    $("#aLine").change(function () { SelectLineChangeDevice(); });
    SelectLineChangeDevice();
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
            Title_time = end;
            var param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.machine_id = machine_id;
            param.group_id = group_id;
            $.LoadReport({
                url: top.$.rootUrl + "/LR_ReportModule/ReportManage/GetReportData1?reportId=" + request('reportId') + "&queryJson=" + JSON.stringify(param),
                element: $("#report-pane")

            });
        }
    });
    // 查询
    $('#btn_Search').on('click', function () {
        machine_id = $('#txt_Keyword').val();
        group_id = $("#aLine").val();
        var options = $("#txt_Keyword option:selected");//获取当前选择项.
        machine_name =options.text();//获取当前选择项的文本.
       // alert(machine_id);

        if (machine_id == null || machine_id == undefined || machine_id == "") {
            alert("请输入设备编号！");
        }
        else {
            var param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.machine_id = machine_id;
            param.group_id = group_id;
            $.LoadReport({
                url: top.$.rootUrl + "/LR_ReportModule/ReportManage/GetReportData1?reportId=" + request('reportId') + "&queryJson=" + JSON.stringify(param),
                element: $("#report-pane")

            });
        }
    });
    // 用户数据导出
    $('#lr_outport').on('click', function () {

        learun.layerForm({
            id: "ExcelExportForm",
            title: '导出Excel数据',
            url: top.$.rootUrl + '/Utility/ExcelExportForm?gridId=gridtable&filename=LeaveInfo',
            width: 500,
            height: 380,
            callBack: function (id) {
                return top[id].acceptClick();
            },
            btn: ['导出Excel', '关闭']
        });
    });
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
$.LoadReport = function (options) {
   
    //$.extend(defaults);
    $.ajax({
        url: options.url,
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            
            var $echart, $list;
            if (data.tempStyle == 1) {
                if (data.listData.length > 0) {
                    $list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    options.element.append($list);
                    DrawList(data.listData, $list);
                } else {
                    //$list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    //options.element.append($list);               
                }
            } else if (data.tempStyle == 2) {
                if (data.chartData.length > 0) {
                    $echart = $('<div id="echart" style="width: 100%; height: 800px;"></div>');
                    options.element.empty();
                    options.element.append($echart);
                    switch (data.chartType) {
                        case 'pie':
                            DrawPie(data.chartData, $echart[0]);
                            break;
                        case 'bar':
                            DrawBar(data.chartData, $echart[0]);
                            break;
                        case 'line':
                            DrawLine(data.chartData, $echart[0]);
                            break;

                        default:
                    }

                } else {
                    options.element.empty();
                }
            } else {
                if (data.chartData.length > 0) {
                    $echart = $('<div id="echart" style="width: 100%; height: 400px;"></div>');
                    options.element.empty();
                    options.element.append($echart);
                    switch (data.chartType) {
                        case 'pie':
                            DrawPie(data.chartData, $echart[0]);
                            break;
                        case 'bar':
                            DrawBar(data.chartData, $echart[0]);
                            break;
                        case 'line':
                            DrawLine(data.chartData, $echart[0]);
                            break;
                        default:
                    }

                } else {
                    options.element.empty();
                }
                if (data.listData.length > 0) {
                    $list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    options.element.append($list);
                    DrawList(data.listData, $list);
                } else {
                    //$list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    //options.element.append($list);
                }
            }
        }
    });
    function DrawPie(data, echartElement) {
        var myChart = echarts.init(echartElement);
        var option = ECharts.ChartOptionTemplates.Pie(data);
        myChart.setOption(option);
    }
    function DrawBar(data, echartElement) {
        var myChart = echarts.init(echartElement);
        switch (request('reportId')) {
            case "40a0b5ef-9440-4ed9-97f8-1aec36642f0a"://小时产量统计
                //alert(machine_name);
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name+" "+ dateToString(Title_time)+"小时产量统计");
                break;
            case "f43e6528-4f04-454f-9922-ead91c938add"://年产量统计
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name +" 年产量统计");
                break;
            case "6ac82b75-c508-4237-8ec9-bae1df8066cb"://月产量统计
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name +" 月产量统计");
                break;
            case "8060e1eb-5286-4c22-97bd-07ebde2d4cc8"://班次产量统计
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name +" 班次产量统计");
                break;
            case "522af585-5f4c-47fe-a3aa-0930754fb03b"://日产量统计
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name +" 日产量统计");
                break;
            case "7cca72d4-92f0-4e3f-82e4-6b47a087e414"://报警信息统计
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true, machine_name + " 报警信息统计");
                break;
            default:
                var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true,"");
        }
        //var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true);
        myChart.setOption(option);
    }
    function DrawLine(data, echartElement) {
        var myChart = echarts.init(echartElement);
        var option = ECharts.ChartOptionTemplates.Lines(data, 'line', true);
        myChart.setOption(option);
    }
    function DrawList(data, listElement) {
        listElement.jfGrid({
            headData: function () {
                var colModelData = [];
                for (key in data[0]) {
                    colModelData.push({ name: key, label: key, width: 120, align: "left" });
                }
                return colModelData;
            }(),
            rowdatas: data,
            isAutoHeight: true
        });
    }

    function dateToString(date) {
        var date = new Date(date);
        var year = date.getFullYear();
        var month = (date.getMonth() + 1).toString();
        var day = (date.getDate()).toString();
        if (month.length == 1) {
            month = "0" + month;
        }
        if (day.length == 1) {
            day = "0" + day;
        }
        var dateTime = year + "-" + month + "-" + day;
        return dateTime;
    }


}
