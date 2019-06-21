
var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_lable = new Array();
var yaxis_value = new Array();

var time_cnt = 0;

$(document).ready(function () {

    // === Prepare peity charts === //
    maruti.peity();

    // === Prepare the chart data ===/
    var sin = [], cos = [];
    for (var i = 0; i < 14; i += 0.5) {
        sin.push([i, Math.sin(i)]);
        cos.push([i, Math.cos(i)]);
    }

    ReRealProdNum();
    ReRunDuration();

    $("#ul_state").click(function () {
        $.post("/Home/DeviceRealState",
            function () {
                window.location = "/Home/DeviceRealState";
            });
    });
    $("#real_prod").click(function () {
        $.post("/Analysis/ProductAnalysis",
            function () {
                window.location = "/Analysis/ProductAnalysis";
            });
    });
});

function ReRealProdNum() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/PView/GetRealProdNum",
        success: procRealProdNum
    });
}

function procRealProdNum(data) {

    var arr_group_id = new Array();
    var arr_group = new Array();
    xaxis_lable.length = 0;
    xaxis_value.length = 0;
    var gname0 = "";
    var gname1 = "";
    var gname2 = "";
    var gname3 = "";
    var xaxis_value0 = new Array();
    var xaxis_value1 = new Array();
    var xaxis_value2 = new Array();
    var xaxis_value3 = new Array();
    
    xaxis_value0.length = 0;
    xaxis_value1.length = 0;
    xaxis_value2.length = 0;
    xaxis_value3.length = 0;

    $.each(data, function (vi, item) {
        xaxis_value.length = 0;
        var gname = "";

        if (vi == 0) {
            xaxis_lable.push([0, "7:30"]);
        }
        if (vi == 0) {
            xaxis_value0.push([0, 0]);
        } else if (vi > 0 && vi == 1) {
            xaxis_value1.push([0, 0]);
        } else if (vi > 0 && vi == 2) {
            xaxis_value2.push([0, 0]);
        } else {
            xaxis_value3.push([0, 0]);
        }

        $.each(item, function (vj, titem) {
            if (vj == 0) {
                if (vi == 0) {
                    gname0 = titem.group_name;
                }else if (vi > 0 && vi == 1) {
                    gname1 = titem.group_name;
                } else if (vi > 0 && vi == 2) {
                    gname2 = titem.group_name;
                } else {
                    gname3 = titem.group_name;
                }
            }
            if (vi == 0) {
                xaxis_lable.push([vj+1, titem.day_time + ":30"]);
            }
            if (vi == 0) {
                xaxis_value0.push([vj+1, titem.prod_num]);
            } else if (vi > 0 && vi == 1) {
                xaxis_value1.push([vj+1, titem.prod_num]);
            } else if (vi > 0 && vi == 2) {
                xaxis_value2.push([vj+1, titem.prod_num]);
            } else {
                xaxis_value3.push([vj+1, titem.prod_num]);
            }
        });
        if (arr_group_id.indexOf(item.group_id) < 0) {
            arr_group_id.push(item.group_id);
            arr_group.push([item.group_id, item.group_name]);
        }
    });
    
    var dataset = new Array();
    dataset.length = 0;
    
    if (xaxis_value0.length == 0) {
        for (var vi = 0; vi < xaxis_lable.length; vi++) {
            xaxis_value0.push([vi, 0]);
        }
    }
    if (xaxis_value1.length == 0) {
        for (var vi = 0; vi < xaxis_lable.length; vi++) {
            xaxis_value1.push([vi, 0]);
        }
    }
    if (xaxis_value2.length == 0) {
        for (var vi = 0; vi < xaxis_lable.length; vi++) {
            xaxis_value2.push([vi, 0]);
        }
    }
    if (xaxis_value3.length == 0) {
        for (var vi = 0; vi < xaxis_lable.length; vi++) {
            xaxis_value3.push([vi, 0]);
        }
    }
    for (var vi = 0; vi < xaxis_lable.length; vi++) {
        var vitem = { xaxis_label: xaxis_lable[vi][1], data0: xaxis_value0[vi][1], data1: xaxis_value1[vi][1], data2: xaxis_value2[vi][1], data3: xaxis_value3[vi][1] };
        dataset.push(vitem);
    }

    procRealProdChart(xaxis_lable, gname0, gname1, gname2, gname3, xaxis_value0, xaxis_value1, xaxis_value2, xaxis_value3, dataset);
}

function procRealProdChart(xaxis_lable, gname0, gname1, gname2, gname3, xaxis_value0, xaxis_value1, xaxis_value2, xaxis_value3, dataset) {
    // === Make chart === //
    var plot = $.plot($(".shift_chart"), //dataset,
           [{ label: gname0, data: xaxis_value0, color: "#FF7400" }
           ],
           {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               grid: { hoverable: true, clickable: true },
               xaxis: {
                   //mode: "categories",
                   ticks: xaxis_lable
               },
           });

    // === Point hover in chart === //
    var previousPoint = null;
    var previousLable = null;
    $(".shift_chart").bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousLable != item.series.label || previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;
                previousLable = item.series.label;

                $('#tooltip').remove();
                var x = item.datapoint[0].toFixed(2),
					y = item.datapoint[1].toFixed(2);

                //maruti.flot_tooltip(item.pageX, item.pageY, item.series.label + " of " + x + " = " + y);
                maruti.flot_tooltip(item.pageX, item.pageY, xaxis_lable[item.datapoint[0].toFixed(0)][1] + "产量 = " + y);
            }

        } else {
            $('#tooltip').remove();
            previousPoint = null;
        }
    });
}

function ReRunDuration() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/PView/GetLineRunDuration",
        success: procRunDuration
    });
}

function procRunDuration(data) {

    xaxis_lable.length = 0;
    yaxis_lable.length = 0;
    xaxis_lable.push([1, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开动率(=运行时长/24小时*100)"]);
    yaxis_lable.push([1, "运行"]);
    yaxis_lable.push([2, "报警"]);
    yaxis_lable.push([3, "暂停"]);
    yaxis_lable.push([4, "停机"]);
    //yaxis_lable.push([5, "空闲"]);

    var ds_workrate = new Array();
    var ds_timerate = new Array();

    if (time_cnt == data.length) {
        time_cnt = 0;
    }

    xaxis_value.length = 0;
    yaxis_value.length = 0;
    xaxis_value.push([1, data[time_cnt].work_rate.toFixed(2)]);
    yaxis_value.push([1, data[time_cnt].run_rate.toFixed(2)]);
    yaxis_value.push([2, data[time_cnt].alarm_rate.toFixed(2)]);
    yaxis_value.push([3, data[time_cnt].pause_rate.toFixed(2)]);
    yaxis_value.push([4, data[time_cnt].stop_rate.toFixed(2)]);
    //yaxis_value.push([5, data[time_cnt].ready_rate.toFixed(2)]);

    ds_workrate = [{ data: xaxis_value, label: "开动率 " + data[time_cnt].group_name, color: "#00FF00" }];
    ds_timerate = [{ data: yaxis_value, label: "用时占比 " + data[time_cnt].group_name, color: "#00FF00" }];

    procRunDurationChart(ds_workrate, ds_timerate);
    procRealDurData(data, time_cnt);
    processNoteDefine();
    time_cnt++;
}

function procRunDurationChart(ds_workrate, ds_timerate) {
    //=== Make chart === //
    var plot = $.plot($(".shift_wr_chart"), ds_workrate, {
                series: {
                    stack: true,
                    bars: {
                        show: true,
                        numbers: {
                            show: true,
                            xAlign: function (x) { return x + 0.5; },
                            font: { size: 12, weight: "bold", family: "Verdana", color: "#545454" }
                        }
                    },
                    points: { show: true },
                },
                grid: { hoverable: true, clickable: true },
                xaxis: {
                    ticks: xaxis_lable,
                    min: 0,
                    max: 3
                },
                yaxis: { min: 0, max: 100 }
            });

    //=== Point hover in chart === //
    var previousPoint = null;
    var previousLable = null;
    $(".shift_wr_chart").bind("plothover", function (event, pos, item) {
        if (item) {
            if (previousLable != item.series.label || previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;
                previousLable = item.series.label;

                $('#tooltip').remove();
                var x = item.datapoint[0].toFixed(2),
    				y = item.datapoint[1].toFixed(2);
                
                maruti.flot_tooltip(item.pageX, item.pageY, yaxis_lable[x - 1][1] + " = " + y);
            }

        } else {
            $('#tooltip').remove();
            previousPoint = null;
        }
    });

    //=== Make chart === //
    var plot = $.plot($(".shift_ut_chart"), ds_timerate, {
                series: {
                    stack: true,
                    bars: {
                        show: true,
                        numbers: {
                            show: true,
                            xAlign: function (x) { return x + 0.5; },
                            font: { size: 12, weight: "bold", family: "Verdana", color: "#545454" }
                        }
                    },
                    points: { show: true },
                },
                grid: { hoverable: true, clickable: true },
                xaxis: {
                    ticks: yaxis_lable,
                    min: 0,
                    max: 6
                },
                yaxis: { min: 0, max: 100 }
            });

    //=== Point hover in chart === //
    var previousLable = null;
    var previousPoint = null;
    $(".shift_ut_chart").bind("plothover", function (event, pos, item) {
        if (item) {
            if (previousLable != item.series.label || previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;
                previousLable = item.series.label;

                $('#tooltip').remove();
                var x = item.datapoint[0].toFixed(2),
    				y = item.datapoint[1].toFixed(2);
                maruti.flot_tooltip(item.pageX, item.pageY, yaxis_lable[x-1][1] + " = " + y);
            }

        } else {
            $('#tooltip').remove();
            previousPoint = null;
        }
    });
}

function RefreshRunNum() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/PView/GetRunNumData",
        success: processData
    });
}

function processData(data) {
    var target = $("#ul_state");
    target.empty();
    target.append(
        "<li class=\"bg_lg bg_run\"> <a href=\"#\"> <i class=\"icon-play\"></i> <h4><span class=\"label label-success\">" + data[0].run_num + "</span></h4><strong> 运 行 </strong></a> </li>"
        + "<li class=\"bg_lr bg_alarm\"> <a href=\"#\"> <i class=\"icon-bullhorn\"></i><h4><span class=\"label label-success\">" + data[1].run_num + "</span></h4><strong> 报 警 </strong></a> </li>"
        + "<li class=\"bg_ly bg_pause\"> <a href=\"#\"> <i class=\"icon-pause\"></i><h4><span class=\"label label-success\">" + data[2].run_num + "</span></h4><strong> 暂 停 </strong></a> </li>"
        + "<li class=\"bg_lh bg_stop\"> <a href=\"#\"> <i class=\"icon-stop\"></i><h4><span class=\"label label-success\">" + data[3].run_num + "</span></h4><strong> 停 机 </strong></a> </li>"
    );

    var previousPoint = null;
    $('.bg_run').hover(function (e) {
        if (previousPoint != e.pageX) {
            previousPoint = e.pageX;

            $('#tooltip').remove();
        }
        var x = e.pageX.toFixed(2),
    		y = e.pageY.toFixed(2);
        maruti.flot_tooltip(e.pageX, e.pageY, "运行：设备处于运行状态（绿色灯亮）");
    }, function () {
        $('#tooltip').remove();
        previousPoint = null;
    });

    previousPoint = null;
    $('.bg_alarm').hover(function (e) {
        if (previousPoint != e.pageX) {
            previousPoint = e.pageX;

            $('#tooltip').remove();
        }
        var x = e.pageX.toFixed(2),
    		y = e.pageY.toFixed(2);
        maruti.flot_tooltip(e.pageX, e.pageY, "报警：设备处于报警状态（红色灯亮）");
    }, function () {
        $('#tooltip').remove();
        previousPoint = null;
    });

    previousPoint = null;
    $('.bg_pause').hover(function (e) {
        if (previousPoint != e.pageX) {
            previousPoint = e.pageX;

            $('#tooltip').remove();
        }
        var x = e.pageX.toFixed(2),
    		y = e.pageY.toFixed(2);
        maruti.flot_tooltip(e.pageX, e.pageY, "暂停：设备处于暂停状态（黄色灯亮）");
    }, function () {
        $('#tooltip').remove();
        previousPoint = null;
    });

    previousPoint = null;
    $('.bg_stop').hover(function (e) {
        if (previousPoint != e.pageX) {
            previousPoint = e.pageX;

            $('#tooltip').remove();
        }
        var x = e.pageX.toFixed(2),
    		y = e.pageY.toFixed(2);
        maruti.flot_tooltip(e.pageX, e.pageY, "停机：设备处于停机状态（设备关机）");
    }, function () {
        $('#tooltip').remove();
        previousPoint = null;
    });

    previousPoint = null;
    $('.bg_ready').hover(function (e) {
        if (previousPoint != e.pageX) {
            previousPoint = e.pageX;

            $('#tooltip').remove();
        }
        var x = e.pageX.toFixed(2),
    		y = e.pageY.toFixed(2);
        maruti.flot_tooltip(e.pageX, e.pageY, "空闲：设备处于空闲状态（三色灯全灭）");
    }, function () {
        $('#tooltip').remove();
        previousPoint = null;
    });
}

function ReRealRunProd() {
    $.ajax({
        type: "post",
        dataType: "json",
        data:
            {
                run_state: -1,
                run_num: -1,
                run_type: "real_prod"
            },
        url: "/PView/GetRunCountData/real_prod",
        success: procRealProdData,
        complete: processProdDefine
    });
}

function procRealProdData(data) {
    var target = $("#real_prod");
    target.empty();
    target.append(
        "<li class=\"bg_sh\"><i class=\"icon-star\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-day\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data1 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">日产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-cog\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-week\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data2 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">周产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-asterisk\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-month\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data3 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">月产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-certificate\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-year\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data4 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">年产量</small></li>"
    )
}
function processProdDefine(data) {
    $('.tip-day').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_prod").text("日产量：自动线当天的产量");
            $("#tip_prod").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_prod").hide(500);
    });
    $('.tip-week').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_prod").text("周产量：截止今日最近一周自动线的产量");
            $("#tip_prod").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_prod").hide(500);
    });
    $('.tip-month').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_prod").text("月产量：截止今日最近一月自动线的产量");
            $("#tip_prod").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_prod").hide(500);
    });
    $('.tip-year').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_prod").text("年产量：截止今日最近一年自动线的产量");
            $("#tip_prod").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_prod").hide(500);
    });

}
function ReRealRunDur() {
    $.ajax({
        type: "get",
        dataType: "json",
        data:
            {
                run_state: -1,
                run_num: -1,
                run_type: "real_dur"
            },
        url: "/PView/GetRunCountData/real_dur",
        success: procRealDurData,
        complete: processNoteDefine
    });
}

function procRealDurData(data, time_cnt) {
    var hour1 = 0;
    var min1 = 0;
    if (data[time_cnt].run_duration != null && data[time_cnt].run_duration > 0) {
        hour1 = Math.floor(data[time_cnt].run_duration / 3600);
        min1 = Math.ceil((data[time_cnt].run_duration % 3600) / 60);
    }
    var hour2 = 0;
    var min2 = 0;
    if (data[time_cnt].alarm_duration != null && data[time_cnt].alarm_duration > 0) {
        hour2 = Math.floor(data[time_cnt].alarm_duration / 3600);
        min2 = Math.ceil((data[time_cnt].alarm_duration % 3600) / 60);
    }
    var hour3 = 0;
    var min3 = 0;
    if (data[time_cnt].pause_duration != null && data[time_cnt].pause_duration > 0) {
        hour3 = Math.floor(data[time_cnt].pause_duration / 3600);
        min3 = Math.ceil((data[time_cnt].pause_duration % 3600) / 60);
    }
    var hour4 = 0;
    var min4 = 0;
    if (data[time_cnt].stop_duration != null && data[time_cnt].stop_duration > 0) {//pause_duration zk
        hour4 = Math.floor(data[time_cnt].stop_duration / 3600);
        min4 = Math.ceil((data[time_cnt].stop_duration % 3600) / 60);
    }
    var target = $("#real_dur");
    target.empty();
    target.append(
        "<li class=\"bg_sh\"><i class=\"icon-star\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-run\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + hour1 + "小时" + min1 + "分钟</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">今日运行时长</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-bullhorn\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-alarm\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + hour2 + "小时" + min2 + "分钟</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">今日报警时长</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-asterisk\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-pause\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + hour3 + "小时" + min3 + "分钟</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">今日暂停时长</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-certificate\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning tip-stop\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + hour4 + "小时" + min4 + "分钟</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">今日停机时长</small></li>"
    )
}
function processNoteDefine(data) {
    $('.tip-run').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_note").text("今日运行时长：自动线处于运行状态的总时长");
            $("#tip_note").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_note").hide(500);
    });
    $('.tip-alarm').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_note").text("今日报警时长：自动线处于报警状态的总时长");
            $("#tip_note").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_note").hide(500);
    });
    $('.tip-pause').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_note").text("今日暂停时长：自动线处于暂停状态的总时长");
            $("#tip_note").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_note").hide(500);
    });
    $('.tip-stop').hover(function (e) {
        timer = setTimeout(function () {
            $("#tip_note").text("今日停机时长：自动线处于关机状态的总时长");
            $("#tip_note").show();
        }, 1500);
    }, function () {
        clearTimeout(timer);
        $("#tip_note").hide(500);
    });

}
maruti = {
    // === Peity charts === //
    peity: function () {
        $.fn.peity.defaults.line = {
            strokeWidth: 1,
            delimeter: ",",
            height: 24,
            max: null,
            min: 0,
            width: 50
        };
        $.fn.peity.defaults.bar = {
            delimeter: ",",
            height: 24,
            max: null,
            min: 0,
            width: 50
        };
        $(".peity_line_good span").peity("line", {
            colour: "#57a532",
            strokeColour: "#459D1C"
        });
        $(".peity_line_bad span").peity("line", {
            colour: "#FFC4C7",
            strokeColour: "#BA1E20"
        });
        $(".peity_line_neutral span").peity("line", {
            colour: "#CCCCCC",
            strokeColour: "#757575"
        });
        $(".peity_bar_good span").peity("bar", {
            colour: "#459D1C"
        });
        $(".peity_bar_bad span").peity("bar", {
            colour: "#BA1E20"
        });
        $(".peity_bar_neutral span").peity("bar", {
            colour: "#4fb9f0"
        });
    },
	// === Tooltip for flot charts === //
	flot_tooltip: function(x, y, contents) {
		$('<div id="tooltip">' + contents + '</div>').css( {
			top: y + 5,
			left: x + 5
		}).appendTo("body").fadeIn(300);
	}
}
function showDailyProdManual(data, date1, date2) {
    var listDate = new Array();

    listDate.length = 0;
    listGroup.length = 0;
    var target = $(".modal-body-6");
    var days = Math.ceil((date2.getTime() - date1.getTime()) / 86400000);

    for (var vi = 0; vi <= days; vi++) {
        listDate.push(date1.getDate + vi);
        var strget = "";
        strget += "<div class=\"row\" style=\"margin-top:-50px;margin-bottom:20px\"><div class=\"col-md-12\"><div class=\"form-group\" style=\"margin-bottom:5px\"> "
               + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:100%;text-align:center;font-size:16px;padding-top:5px\">"
               + date1.getFullYear() + "年" + (date1.getMonth() + 1) + "月" + (date1.getDate() + vi) + "日</label></div></div></div>"
        $.each(data, function (vj, item) {
            if (vi == 0) {
                listGroup.push([item.wshift_id, item.group_id]);
            }
            if ((item.pre_jprod == 1 && item.next_jprod == 0) || (item.next_jprod == 1 && item.pre_jprod == 0)) {
                strget += "<div class=\"row\"><div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                       + "<label for=\"m_group\" class=\"control-label-query\" style=\"width:100%;text-align:left;font-size:16px;padding-top:5px\">" + item.group_name + "</label></div></div>"
                       + "<div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                       + "<label for=\"m_npass_" + item.group_id + "\" class=\"control-label-query\" style=\"width:30%;display:inline;font-size:16px;padding-top:5px\">" + item.wshift_name + " </label>"
                       + "<input type=\"text\" class=\"form-control\" style=\"width:65%;display:inline;font-size:small;padding-top:5px\" id=\"m_npass_" + item.group_id + "\" placeholder=\"不合格量\"></div></div></div>";
            }
        });

        target.empty();
        if (data.length > 0) {
            target.append(strget);
        } else {
            target.append(strget + "<div class=\"row\"><div class=\"col-md-10\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                                 + "<label class=\"control-label-query\" style=\"width:100%;text-align:left;font-size:16px;padding-top:5px\">未在时间范围内，请勿操作！</label></div></div></div>");
        }
    }
    jQuery('#modal-6').modal('show', { backdrop: 'static' });
}
function showDailyProdJudge(data, date1, date2) {
    var listDate = new Array();

    var npass_num = 0;
    listDate.length = 0;
    listGroup.length = 0;

    var target = $(".modal-body-6");
    var days = Math.ceil((date2.getTime() - date1.getTime()) / 86400000);
    for (var vi = 0; vi <= days; vi++) {
        listDate.push(date1.getDate + vi);
        var strget = "";
        strget += "<div class=\"row\" style=\"margin-top:-50px;margin-bottom:20px\"><div class=\"col-md-12\"><div class=\"form-group\" style=\"margin-bottom:5px\"> "
               + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:100%;text-align:center;font-size:16px;padding-top:5px\">"
               + date1.getFullYear() + "年" + (date1.getMonth() + 1) + "月" + (date1.getDate() + vi) + "日</label></div></div></div>"
        $.each(data, function (vj, item) {
            if (vi == 0) {
                listGroup.push([item.wshift_id, item.group_id]);
            }
            if (item.npass_jflg == 1 && ((item.pre_jprod == 1 && item.next_jprod == 0) || (item.next_jprod == 1 && item.pre_jprod == 0))) {
                strget += "<div class=\"row\"><div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                       + "<label for=\"m_group\" class=\"control-label-query\" style=\"width:100%;text-align:left;font-size:16px;padding-top:5px\">" + item.group_name + "</label></div></div>"
                       + "<div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                       + "<label for=\"m_npass_" + item.group_id + "\" class=\"control-label-query\" style=\"width:30%;display:inline;font-size:16px;padding-top:5px\">" + item.wshift_name + " </label>"
                       + "<input type=\"text\" class=\"form-control\" style=\"width:65%;display:inline;font-size:small;padding-top:5px\" id=\"m_npass_" + item.group_id + "\" placeholder=\"不合格量\"></div></div></div>";
                npass_num++;
            }
        });
        if (data.length > 0 && npass_num > 0) {
            target.empty();
            target.append(strget);
        }
    }
    if (data.length > 0 && npass_num > 0) {
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    }
}
function updatePassProdNum() {
    var pass_prod_mor = $('#m_pass_mor').val();
    var pass_prod_mid = $('#m_pass_mid').val();
    var pass_prod_nig = $('#m_pass_nig').val();

    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            pass_prod_mor: pass_prod_mor,
            pass_prod_mid: pass_prod_mid,
            pass_prod_nig: pass_prod_nig,
        },
        url: "/Home/UpdatePassProdNum",
        success: alert('success')
    });
    $('#modal-6').modal('hide');
}
function updateNPassProdNum() {
    var suc = 0;

    for (var i = 0; i < listGroup.length; i++) {
        var group = listGroup[i];
        var npass_prod = $('#m_npass_' + group[1]).val();

        $.ajax({
            type: "post",
            dataType: "json",
            data: {
                wshift_id: group[0],
                group_id: group[1],
                npass_prod: npass_prod
            },
            url: "/Home/UpdateNPassProdNum",
            success: suc++
        });
    }
    $('#modal-6').modal('hide');
}