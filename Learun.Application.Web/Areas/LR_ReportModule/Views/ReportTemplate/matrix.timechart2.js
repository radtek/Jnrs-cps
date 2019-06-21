var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_value = new Array();

var dateflag = Array();//全局变量

$(document).ready(function () {

    //var machine_id = 0;
    //var group_id = $("#aLine").val();
    //var sets_no = $("#aSets_no").val();
    //var startDate = $("#startDate").val();
    //var endDate = $("#endDate").val();

    var machine_id = 0;
    var group_id =10;
    var sets_no ="";
    var startDate = "2019-04-08"
    var endDate = "2019-04-08";

    //var tar_dates = $("#wdates");
    //var tar_issues = $("#wissues");

    ReDeviceUsedTime(group_id, machine_id, sets_no, startDate, endDate);

});

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
       // url: "/PView/GetDeviceUsedTime_His",
        //url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetDeviceUsedTime_His',
        url:  '/ReportTemplate/GetDeviceUsedTime_His',
        //error: alert("error"),
        //complete: alert("complete"),
        success: procDeviceUsedTime,
        complete: processNoteDefine
    });
}
function procDeviceUsedTime(data) {
    var target = $("#ul_used_time");
    target.empty();
    var tr_txt = "<tr>"+
        "<th class=\"text-center\">设备名称</th>"+
            "<th class=\"text-left;\" style=\"color:#548C00\">8:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;12:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;14:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;16:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;18:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;20:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;22:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;24:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8:00</th>"+
                    "</tr>";
    $.each(data.listUsedTimeSeque, function (i, item) {
        if (item.litime_seq.length > 0)
        {
            var zuoye = "盖板";
            if (i == 0) zuoye = "盖板";
            else if (i == 1) { zuoye = "U型螺栓"; }
            else if (i == 2) { zuoye = "连接板"; }
            else if (i == 3) { zuoye = "调整块"; }
            else if (i == 4) { zuoye = "螺杆地脚"; }
            else if (i == 5) { zuoye = "垫圈"; }
            else if (i == 6) { zuoye = "轨道"; }
            var tot_rate = 0;
            tr_txt += "<tr><td style = \"width:10%\">" + item.machine_number + "</td ><td>";
            tr_txt += " <div class=\"progress\">";
            $.each(item.litime_seq, function (i, item1) {
                tot_rate += item1.dur_rate;
                if (tot_rate >= 100.0) {
                    //tr_txt += "<div style=\"width:" + (item1.dur_rate - (tot_rate - 100)) + "%\" class=\"" + item1.run_class + "\"></div>";
                    tr_txt += "<div class=\"" + item1.run_class + "\" role=\"progressbar\" aria - valuenow=\"60\" aria - valuemin=\"0\" aria - valuemax=\"100\" style = \"width: " + (item1.dur_rate - (tot_rate - 100)) + "%\" ><span class=\"sr-only\">" + (item1.dur_rate - (tot_rate - 100)) + "%\" 完成</span> </div >";
                } else {
                    tr_txt += "<div class=\"" + item1.run_class + "\" role=\"progressbar\" aria - valuenow=\"60\" aria - valuemin=\"0\" aria - valuemax=\"100\" style = \"width: " + item1.dur_rate + "%\" ><span class=\"sr-only\">" + item1.dur_rate  + "%\" 完成</span> </div >";
                }
            });
            tr_txt += "</div>";
            tr_txt += "</td>";
            tr_txt += "</tr>";
        }
    });
    target.append(tr_txt);
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
function setFlag(start,end){
    var cdate = new Array();
    cdate = start.split("-");
    var cd = cdate[1]+"/"+cdate[2]+"/"+cdate[0]; 
    var daynum = dateDiff(end,start);
    for (var i = daynum; i >= 0; i--) {
        dateflag.push(addDays(cd, i));
    }
}//end fun
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