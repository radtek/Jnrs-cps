//------------- init query -------------//
var dataset = new Array();
var choiceContainer = $("#choices");
dataset.length = 0;
    $(document).ready(function () {
        var group_id = $("#aLine").val();
        var machine_id = $("#aDevice").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        var sets_no = "";

        RefreshDurData(group_id, machine_id, sets_no, startDate, endDate);
    });
    //------------- btn_query -------------//
    $(document).ready(function () {
        $("#btn_query").click(function () {
            var group_id = $("#aLine").val();
            var machine_id = $("#aDevice").val();
            var startDate = $("#startDate").val();
            var endDate = $("#endDate").val();
            var sets_no = "";

            RefreshDurData(group_id, machine_id, sets_no, startDate, endDate);

            choiceContainer.empty();
            choiceContainer.append('选择状态：');
            choiceContainer.append('<input type="checkbox" name="' + 0 + '" checked="checked" id="id' + 0 + '">' +
                                       '<label for="id' + 0 + '">' + '运行' + '</label>');
            choiceContainer.append('<input type="checkbox" name="' + 1 + '" checked="checked" id="id' + 1 + '">' +
                                       '<label for="id' + 1 + '">' + '报警' + '</label>');
            choiceContainer.append('<input type="checkbox" name="' + 2 + '" checked="checked" id="id' + 2 + '">' +
                                       '<label for="id' + 2 + '">' + '暂停' + '</label>');
            choiceContainer.append('<input type="checkbox" name="' + 3 + '" checked="checked" id="id' + 3 + '">' +
                                       '<label for="id' + 3 + '">' + '停机' + '</label>');
            // 为可选框增加点击事件
            choiceContainer.find("input").click(plotAccordingToChoices);
            plotAccordingToChoices();
        });
        choiceContainer.append('选择状态：');
        choiceContainer.append('<input type="checkbox" name="' + 0 + '" checked="checked" id="id' + 0 + '">' +
                                   '<label for="id' + 0 + '">' + '运行' + '</label>');
        choiceContainer.append('<input type="checkbox" name="' + 1 + '" checked="checked" id="id' + 1 + '">' +
                                   '<label for="id' + 1 + '">' + '报警' + '</label>');
        choiceContainer.append('<input type="checkbox" name="' + 2 + '" checked="checked" id="id' + 2 + '">' +
                                   '<label for="id' + 2 + '">' + '暂停' + '</label>');
        choiceContainer.append('<input type="checkbox" name="' + 3 + '" checked="checked" id="id' + 3 + '">' +
                                   '<label for="id' + 3 + '">' + '停机' + '</label>');
        // 为可选框增加点击事件
        choiceContainer.find("input").click(plotAccordingToChoices);
        plotAccordingToChoices();
    });
    function plotAccordingToChoices() {
        var dataSeries = [];
        dataSeries.length = 0;
        choiceContainer.find("input:checked").each(function () {
            // 多选框的名字  
            var key = $(this).attr("name");
            // 有该属性，并且有该属性为Key的数据，则增加到显示区
            if (key && key == 0)
                dataSeries.push({ valueField: "run_duration", name: "运行", color: "#46df1f" });
            if (key && key == 1)
                dataSeries.push({ valueField: "alarm_duration", name: "报警", color: "#ff0033" });
            if (key && key == 2)
                dataSeries.push({ valueField: "pause_duration", name: "暂停", color: "#e5e500" });
            if (key && key == 3)
                dataSeries.push({ valueField: "stop_duration", name: "停机", color: "#2e363f" });
        });
        // 如果有数据则设置数据。等同与把图形重绘了，所以移除某个国家时可以不再显示  
        if (dataSeries.length > 0)
            $("#bar-gauge-2").dxChart({
                dataSource: dataset,
                commonSeriesSettings: {
                    argumentField: "calc_date",
                },
                series: dataSeries,
                valueAxis: {
                },
                argumentAxis: {
                    grid: {
                        visible: true
                    },
                },
                tooltip: {
                    enabled: true
                },
                title: {
                    text: "分段运行时长（单位：小时）",
                    font: {
                        size: 20
                    },
                },
                legend: {
                    verticalAlignment: "center",
                    horizontalAlignment: "right"
                },
                commonPaneSettings: {
                    border: {
                        visible: true,
                        right: false
                    },
                    title: {
                        margin: { left: 400 }
                    },
                    valueAxis: {
                        margin: { top: 0 },
                        verticalAlignment: "top",
                        horizontalAlignment: "center"
                    },
                }
            });
    }
    function RefreshDurData(group_id, machine_id, sets_no, startDate, endDate) {
        $.ajax({
            type: "post",
            dataType: "json",
            data: {
                group_id: group_id,
                machine_id: machine_id,
                sets_no: sets_no,
                start_date: startDate,
                end_date: endDate
            },
            url: "/PView/GetTotSecDurSeq_His",
            success: processDurData,
            complete: processNoteDefine
        });
    }
    function processDurData(data) {
        //圆环
        var hour1 = 0;
        var min1 = 0;
        if (data.totDurSeque.run_duration != null && data.totDurSeque.run_duration > 0) {
            hour1 = Math.floor(data.totDurSeque.run_duration / 3600);
            min1 = Math.ceil((data.totDurSeque.run_duration % 3600) / 60);
        }
        var hour2 = 0;
        var min2 = 0;
        if (data.totDurSeque.alarm_duration != null && data.totDurSeque.alarm_duration > 0) {
            hour2 = Math.floor(data.totDurSeque.alarm_duration / 3600);
            min2 = Math.ceil((data.totDurSeque.alarm_duration % 3600) / 60);
        }
        var hour3 = 0;
        var min3 = 0;
        if (data.totDurSeque.pause_duration != null && data.totDurSeque.pause_duration > 0) {
            hour3 = Math.floor(data.totDurSeque.pause_duration / 3600);
            min3 = Math.ceil((data.totDurSeque.pause_duration % 3600) / 60);
        }
        var hour4 = 0;
        var min4 = 0;
        if (data.totDurSeque.stop_duration != null && data.totDurSeque.stop_duration > 0) {
            hour4 = Math.floor(data.totDurSeque.stop_duration / 3600);
            min4 = Math.ceil((data.totDurSeque.stop_duration % 3600) / 60);
        }
        dataset.length = 0;
        var dpos = data.listSecDurSeque.length - 1;
        $.each(data.listSecDurSeque, function (i, item) {
            
            if (i < dpos) {
                var calc_date = '';
                if (data.listSecDurSeque.length > 15 && item.calc_date.length > 6) {
                    calc_date = item.calc_date.substring(6, 8); 
                } else if (item.calc_date.length > 6) {
                    calc_date = item.calc_date.substring(4, 8);
                } else {
                    calc_date = item.calc_date;
                }
                item.calc_date = calc_date;
                dataset.push(item);
            }
        });
        var target = $('#max_dur');
        target.empty();
        target.append(
            "<li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-star\" style=\"color:yellowgreen\"></i>"
            + "       <div class=\"progress-time progress-time-run\">"
            + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">" + hour1 + "小时" + min1 + "分钟</strong>"
            + "               <small style=\"color:whitesmoke\">合计运行时长</small></div></div></li>"
            + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-bullhorn\" style=\"color:yellowgreen\"></i>"
            + "       <div class=\"progress-time progress-time-alarm\">"
            + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">" + hour2 + "小时" + min2 + "分钟</strong>"
            + "               <small style=\"color:whitesmoke\">合计报警时长</small></div></div></li>"
            + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-asterisk\" style=\"color:yellowgreen\"></i>"
            + "       <div class=\"progress-time progress-time-pause\">"
            + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">" + hour3 + "小时" + min3 + "分钟</strong>"
            + "               <small style=\"color:whitesmoke\">合计暂停时长</small></div></div></li>"
            + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-certificate\" style=\"color:yellowgreen\"></i>"
            + "       <div class=\"progress-time progress-time-stop\">"
            + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">" + hour4 + "小时" + min4 + "分钟</strong>"
            + "               <small style=\"color:whitesmoke\">合计停机时长</small></div></div></li>"
        );
        var dataSource = dataset;

        $('#bar-gauge-1').dxPieChart({
            dataSource: [{ gname: "运行", viewers: data.listSecDurSeque[dpos].run_rate, channels: 1 },
                    { gname: "报警", viewers: data.listSecDurSeque[dpos].alarm_rate, chnnels: 1 },
                    { gname: "暂停", viewers: data.listSecDurSeque[dpos].pause_rate, chnnels: 1 },
                    { gname: "停机", viewers: data.listSecDurSeque[dpos].stop_rate, chnnels: 1 },
                    { gname: "空闲", viewers: data.listSecDurSeque[dpos].ready_rate, chnnels: 1 }],
            series: [
                {argumentField:"gname",
                valueField:"viewers",
                label: {
                    visible: true,
                    connector: {
                        visible: true,
                        width: 1
                    }
                }
                }],
            title: {
                text: "总运行时长（%）",
                font: {
                    size: 20
                },
                margin: { bottom: 30 }
            },
            legend: {
                margin: { top: 30 },
                verticalAlignment: "bottom",
                horizontalAlignment: "center"
            },
            palette: ['#46df1f', '#ff0033', '#e5e500', '#2e363f', '#3300cc']
        });
        

        $("#bar-gauge-2").dxChart({
            dataSource: dataSource,
            commonSeriesSettings: {
                argumentField: "calc_date",
            },
            series: [
                { valueField: "run_duration", name: "运行", color: "#46df1f" },
                { valueField: "alarm_duration", name: "报警", color: "#ff0033" },
                { valueField: "pause_duration", name: "暂停", color: "#e5e500" },
                { valueField: "stop_duration", name: "停机", color: "#2e363f" },
            ],
            valueAxis: {
            },
            argumentAxis: {
                grid: {
                    visible: true
                },
            },
            tooltip: {
                enabled: true
            },
            title: {
                text: "分段运行时长（单位：小时）",
                font: {
                    size: 20
                },
            },
            legend: {
                verticalAlignment: "center",
                horizontalAlignment: "right"
            },
            commonPaneSettings: {
                border: {
                    visible: true,
                    right: false
                },
                title: {
                    margin: { left: 400 }
                },
                valueAxis: {
                    margin: { top: 0 },
                    verticalAlignment: "top",
                    horizontalAlignment: "center"
                },
            }
        });
    }
    function processNoteDefine(data) {
        $(".progress-time-run").click(function () {
            $.post("/Home/UsedTime",
                function () {
                    //window.location = "/Home/UsedTime";
                });
        });
        $(".progress-time-alarm").click(function () {
            $.post("/Analysis/AlarmAnalysis",
                function () {
                    //window.location = "/Analysis/AlarmAnalysis";
                });
        });
        $(".progress-time-pause").click(function () {
            $.post("/Home/UsedTime",
                function () {
                    //window.location = "/Home/UsedTime";
                });
        });
        $(".progress-time-stop").click(function () {
            $.post("/Analysis/StopRAnalysis",
                function () {
                    //window.location = "/Analysis/StopRAnalysis";
                });
        });
        $('.progress-time-run').hover(function (e) {
            timer = setTimeout(function () {
                $("#tip_note").text("合计运行时长：在查询周期内自动线处于运行状态的总计时长");
                $("#tip_note").show();
            }, 3000);
        }, function () {
            clearTimeout(timer);
            $("#tip_note").hide(1000);
        });
        $('.progress-time-alarm').hover(function (e) {
            timer = setTimeout(function () {
                $("#tip_note").text("合计报警时长：在查询周期内自动线处于报警状态的总计时长");
                $("#tip_note").show();
            }, 3000);
        }, function () {
            clearTimeout(timer);
            $("#tip_note").hide(1000);
        });
        $('.progress-time-pause').hover(function (e) {
            timer = setTimeout(function () {
                $("#tip_note").text("合计暂停时长：在查询周期内自动线处于暂停状态的总计时长");
                $("#tip_note").show();
            }, 3000);
        }, function () {
            clearTimeout(timer);
            $("#tip_note").hide(1000);
        });
        $('.progress-time-stop').hover(function (e) {
            timer = setTimeout(function () {
                $("#tip_note").text("合计停机时长：在查询周期内自动线处于关机状态的总计时长");
                $("#tip_note").show();
            }, 3000);
        }, function () {
            clearTimeout(timer);
            $("#tip_note").hide(1000);
        });
    }
    $(function () {
 
        $("#dialog").dialog({
            autoOpen: false,
            modal: false,
            width: 400,
            buttons: [
                {
                    text: "Ok",
                    click: function () {
                        $(this).dialog("close");
                    }
                },
                {
                    text: "Cancel",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ],
        });

        // Link to open the dialog
        $("#btndialog").click(function (event) {
            $("#dialog").dialog("open");
            event.preventDefault();
        });
    });
	function between(randNumMin, randNumMax)
	{
	    var randInt = Math.floor((Math.random() * ((randNumMax + 1) - randNumMin)) + randNumMin);
									
	    return randInt;
	}