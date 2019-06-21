var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_value = new Array();

var dateflag = Array();//全局变量
//------------- init query -------------//
$(document).ready(function () {
    $("#device_chart").removeClass("used_time_chart");
    SelectLineChangeDevice();

    var machine_id = 0;
    var group_id = $("#aLine").val();
    var sets_no = $("#aDevice").val();
    var startDate = $("#startDate").val();
    var endDate = $("#endDate").val();

    var tar_dates = $("#wdates");
    var tar_issues = $("#wissues");
    AddLiContent(tar_dates, tar_issues, group_id, machine_id, sets_no, startDate, endDate);
    ReDeviceUsedTime(group_id, machine_id, sets_no, endDate, endDate);
});
//------------- btn_query -------------//
$(document).ready(function () {
    $("#btn_query").click(function () {
        var machine_id = 0;
        var group_id = $("#aLine").val();
        var sets_no = $("#aSets_no").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        var tar_dates = $("#wdates");
        var tar_issues = $("#wissues");
        AddLiContent(tar_dates, tar_issues, group_id, machine_id, sets_no, startDate, endDate);
        ReDeviceUsedTime(group_id, machine_id, sets_no, endDate, endDate);

        ReWTimeLinr();
    });
    $("#ul_used_time").click(function () {
        $.post("/Home/WorkRate",
            function () {
                //window.location = "/Home/WorkRate";
            });
    });
    $(function () {
        $('#wdates li a').click(function () {
            var machine_id = 0;
            var group_id = $("#aLine").val();
            var sets_no = $("#aSets_no").val();
            var qendDate = $(this).parent().attr("id");
            ReDeviceUsedTime(group_id, machine_id, sets_no, qendDate, qendDate);
        })
    });
});
function AddLiContent(tar_dates, tar_issues, group_id, machine_id, sets_no, startDate, endDate) {
    tar_dates.empty();
    tar_issues.empty();
    var strText = "";
    var strIssues = "";
    dateflag.length = 0;
    var week_day = new Array('周日', '周一', '周二', '周三', '周四', '周五', '周六');

    setFlag(startDate, endDate);
    $.each(dateflag, function (vi, item) {
        var new_day = new Date(item);
        strText += "<li id=\"" + item + "\"><a href=\"#\">" + week_day[new_day.getDay()] + " " + item + "</a></li>";
        strIssues += "<li id=\"" + week_day[new_day.getDay()] + " " + item + "\"></li>";
    });

    tar_dates.append(strText);
    tar_issues.append(strIssues);

    $(function () {
        $('#wdates li a').click(function () {
            var endDate = $(this).parent().attr("id");
            ReDeviceUsedTime(group_id, machine_id, sets_no, endDate, endDate);
        })
    });
}
function ReDeviceUsedTime(group_id, machine_id, sets_no, startDate, endDate) {
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
        url: "/PView/GetDeviceUsedTime_His",
        //error: alert("error"),
        //complete: alert("complete"),
        success: procDeviceUsedTime,
        complete: processNoteDefine
    });
}
function procDeviceUsedTime(data) {
    var target = $("#ul_used_time");
    target.empty();
    var tr_txt = "";
    $.each(data.listUsedTimeSeque, function (i, item) {
        if (item.litime_seq.length > 0)
        {
            var tot_rate = 0;
            tr_txt += "<li><span class=\"icon24 icomoon-icon-arrow-up-2 green\"></span>设备号：" + item.machine_number + "<span class=\"pull-right strong\">" + "</span>";
            tr_txt += "<div class=\"progress progress-striped \">";
            $.each(item.litime_seq, function (i, item1) {
                tot_rate += item1.dur_rate;
                if (tot_rate >= 100.0) {
                    tr_txt += "<div style=\"width:" + (item1.dur_rate - (tot_rate - 100)) + "%\" class=\"" + item1.run_class + "\"></div>";
                } else {
                    tr_txt += "<div style=\"width:" + item1.dur_rate + "%\" class=\"" + item1.run_class + "\"></div>";
                }
            });
            tr_txt += "</div>";
            tr_txt += "</li>";
        }
    });
    target.append(tr_txt);

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

    var tar_max_dur = $('#max_dur');
    tar_max_dur.empty();
    tar_max_dur.append(
        "<li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-star\" style=\"color:yellowgreen\"></i>"
        + "       <div class=\"progress-time progress-time-run\">"
        + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">"
        +                 hour1 + "小时" + min1 + "分钟</strong>"
        + "               <small style=\"color:whitesmoke\">合计运行时长</small></div></div></li>"
        + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-bullhorn\" style=\"color:yellowgreen\"></i>"
        + "       <div class=\"progress-time progress-time-alarm\">"
        + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">"
        +                 hour2 + "小时" + min2 + "分钟</strong>"
        + "               <small style=\"color:whitesmoke\">合计报警时长</small></div></div></li>"
        + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-asterisk\" style=\"color:yellowgreen\"></i>"
        + "       <div class=\"progress-time progress-time-pause\">"
        + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">"
        +                 hour3 + "小时" + min3 + "分钟</strong>"
        + "               <small style=\"color:whitesmoke\">合计暂停时长</small></div></div></li>"
        + "   <li class=\"bg_sh\" style=\"padding-bottom:0px\"><i class=\"icon-certificate\" style=\"color:yellowgreen\"></i>"
        + "       <div class=\"progress-time progress-time-stop\">"
        + "           <div style=\"width:100%\" class=\"bar\"><strong style=\"color:whitesmoke;padding-top:5px\">"
        +                 hour4 + "小时" + min4 + "分钟</strong>"
        + "               <small style=\"color:whitesmoke\">合计停机时长</small></div></div></li>"
    );
}
function processNoteDefine(data) {
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
//设置周期内的日期(数组)
//function setFlag(start,end){
//    var cdate = new Array();
//    cdate = start.split("-");
//    var cd = cdate[1]+"/"+cdate[2]+"/"+cdate[0]; 
//    var daynum = dateDiff(end,start);
//    for (var i = daynum; i >= 0; i--) {
//        dateflag.push(addDays(cd, i));
//    }
//}//end fun
//日期加上天数后的新日期.
function addDays(date,days){
    var nd = new Date(date);
    nd = nd.valueOf();
    nd = nd + days * 24 * 60 * 60 * 1000;
    nd = new Date(nd);
    //alert(nd.getFullYear() + "年" + (nd.getMonth() + 1) + "月" + nd.getDate() + "日");
    var y = nd.getFullYear();
    var m = nd.getMonth()+1;
    var d = nd.getDate();
    if(m <= 9) m = "0"+m;
    if(d <= 9) d = "0"+d; 
    var cdate = y+"-"+m+"-"+d;
    return cdate;
}
//两个日期的差值(d1 - d2).
function dateDiff(d1,d2){
    var day = 24 * 60 * 60 *1000;
    try{  
        var dateArr = d1.split("-");
        var checkDate = new Date();
        checkDate.setFullYear(dateArr[0], dateArr[1]-1, dateArr[2]);
        var checkTime = checkDate.getTime();
  
        var dateArr2 = d2.split("-");
        var checkDate2 = new Date();
        checkDate2.setFullYear(dateArr2[0], dateArr2[1]-1, dateArr2[2]);
        var checkTime2 = checkDate2.getTime();
   
        var cha = (checkTime - checkTime2)/day; 
        return cha;
    }catch(e){
        return false;
    }
}//end fun
function showTime(dtime) {
    var show_day = new Array('周一', '周二', '周三', '周四', '周五', '周六', '周日');
    var time = new Date();
    var year = dtime.getYear();
    var month = dtime.getMonth();
    var date = dtime.getDate();
    var day = dtime.getDay();
    var hour = dtime.getHours();
    var minutes = dtime.getMinutes();
    var second = dtime.getSeconds();
    month < 10 ? month = '0' + month : month;
    month = month + 1;
    hour < 10 ? hour = '0' + hour : hour;
    minutes < 10 ? minutes = '0' + minutes : minutes;
    second < 10 ? second = '0' + second : second;
    var now_time = '当前时间：' + year + '年' + month + '月' + date + '日' + ' ' + show_day[day - 1] + ' ' + hour + ':' + minutes + ':' + second;
}
function ReWTimeLinr() {
    $(function () {
        $().wtimelinr({
            orientation: 'vertical',
            // value: horizontal | vertical, default to horizontal
            containerDiv: '#wtimeline',
            // value: any HTML tag or #id, default to #timeline
            datesDiv: '#wdates',
            // value: any HTML tag or #id, default to #dates
            datesSelectedClass: 'selected',
            // value: any class, default to selected
            datesSpeed: 'normal',
            // value: integer between 100 and 1000 (recommended) or 'slow', 'normal' or 'fast'; default to normal
            issuesDiv: '#wissues',
            // value: any HTML tag or #id, default to #issues
            issuesSelectedClass: 'selected',
            // value: any class, default to selected
            issuesSpeed: 'fast',
            // value: integer between 100 and 1000 (recommended) or 'slow', 'normal' or 'fast'; default to fast
            issuesTransparency: 0.2,
            // value: integer between 0 and 1 (recommended), default to 0.2
            issuesTransparencySpeed: 500,
            // value: integer between 100 and 1000 (recommended), default to 500 (normal)
            prevButton: '#wprev',
            // value: any HTML tag or #id, default to #prev
            nextButton: '#wnext',
            // value: any HTML tag or #id, default to #next
            arrowKeys: 'false',
            // value: true/false, default to false
            startAt: 1,
            // value: integer, default to 1 (first)
            autoPlay: 'false',
            // value: true | false, default to false
            autoPlayDirection: 'forward',
            // value: forward | backward, default to forward
            autoPlayPause: 2000
            // value: integer (1000 = 1 seg), default to 2000 (2segs)< });
        });
    });
}