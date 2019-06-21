//------------- Select -------------//
$(document).ready(function () {
    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetDateTime",
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetDateTime',
        success: function (data) {
            var dt = new Date(data);
            $('#startDate').attr("value", dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate());
            $('#endDate').attr("value", dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate());
        }
    });

    $("#aLine").change(function () { SelectLineChangeDevice(); });
});
function SelectLineChangeDevice() {
    //获取下拉框选中项的value属性值
    var saLine = $("#aLine").val();

    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetSLineDevice/" + saLine,
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetSLineDevice/' + saLine,
        success: function (data) {
            var target = $("#aDevice");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            target.append(tr_txt);
        }
    });
}
function SelectLineChangeSeries() {
    //获取下拉框选中项的value属性值
    var group_id = $("#aLine").val();

    $.ajax({
        type: "get",
        dataType: "json",
        data: {
            group_id: group_id
        },
        //url: "/PView/GetLineSeries",
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetLineSeries',
        success: function (data) {
            var target = $("#aSeries");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.series_id + ">" + item.machine_series + "</option>";
            });
            target.append(tr_txt);
        }
    });
}
function SelectSeriesChangeMachine() {
    //获取下拉框选中项的value属性值
    var group_id = $("#aLine").val();
    var series_id = $('#aSeries').val();
    var tagSeriesName = document.getElementById("aSeries");
    var sel_index = tagSeriesName.selectedIndex;
    var machine_series = tagSeriesName.options[sel_index].text;

    $.ajax({
        type: "get",
        dataType: "json",
        data: {
            group_id: group_id,
            series_id: series_id,
            machine_series: machine_series
        },
        //url: "/PView/GetLineSeriesDevice",
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetLineSeriesDevice',
        success: function (data) {
            var target = $("#aSeriesDevice");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            target.append(tr_txt);
        }
    });
}
function SelectSeriesChangeParam() {
    //获取下拉框选中项的value属性值
    var group_id = $("#aLine").val();
    var series_id = $('#aSeries').val();
    var tagSeriesName = document.getElementById("aSeries");
    var sel_index = tagSeriesName.selectedIndex;
    var machine_series = tagSeriesName.options[sel_index].text;

    $.ajax({
        type: "get",
        dataType: "json",
        data: {
            group_id: group_id,
            series_id: series_id,
            machine_series: machine_series
        },
        url: "/PView/GetLineSeriesParam",
        success: function (data) {
            var target = $("#aSeriesParam");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=\"" + item.param_id + "\" data-type=\"" + item.param_type + "\" data-desp=\"" + item.data_description + "\">"
                       + item.param_name + "</option>";
            });
            target.append(tr_txt);
        }
    });
}

//------------- Datepicker -------------//
$(document).ready(function () {
    $('.datepicker').datepicker();
});
//------------- Datepicker -------------//
if ($('#datepicker').length) {
    $("#datepicker").datepicker({
        showOtherMonths: true
    });
}
if ($('#datepicker-inline').length) {
    $('#datepicker-inline').datepicker({
        inline: true,
        showOtherMonths: true
    });
}
