//------------- btn_query -------------//
$(document).ready(function () {
    var group_id = $("#aLine").val();
    var machine_id = $("#aDevice").val();
    var startDate = $("#startDate").val();
    var endDate = $("#endDate").val();

    if (machine_id == null) {
        machine_id = 0;
    }

    maruti.peity();
    RePeriodProdNum(group_id, machine_id, startDate, endDate);
});
//------------- btn_query -------------//
$(document).ready(function () {
    var listDate = new Array();
    $("#btn_query").click(function () {
        var group_id = $("#aLine").val();
        var machine_id = $("#aDevice").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        RePeriodProdNum(group_id, machine_id, startDate, endDate);
    });
});

var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_value = new Array();
function RePeriodProdNum(group_id, machine_id, startDate, endDate) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            start_date: startDate,
            end_date: endDate
        },
        url: "/PView/GetDeviceProdNum_His",
        success: procPeriodProdNum
    });
}

function procPeriodProdNum(data) {
    xaxis_value.length = 0;
    yaxis_value.length = 0;
    xaxis_lable.length = 0;
    $.each(data, function (i, item) {
        if (data.length > 30 && item.calc_date.toString().length > 6) {
            xaxis_lable.push([i, item.calc_date.toString().substring(6, 8)]);
        } else if (data.length > 15 && item.calc_date.toString().length > 6) {
            xaxis_lable.push([i, item.calc_date.toString().substring(4, 8)]);
        } else {
            xaxis_lable.push([i, item.calc_date.toString()]);
        }
        xaxis_value.push([i, item.pass_prod_num]);
        yaxis_value.push([i, item.prod_num]);
    });
    procPeriodProdChart(xaxis_lable, xaxis_value, yaxis_value);
}

function procPeriodProdChart(xaxis_lable, xaxis_value, yaxis_value) {
    // === Make chart === //
    var plot = $.plot($(".period_prod_chart"),
           [{ data: yaxis_value, label: "产量", color: "#FF0F50" }], {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               grid: { hoverable: true, clickable: true },
               xaxis: {
                   ticks: xaxis_lable
               }
               //yaxis: { min: 0, max: 150 }
           });

    // === Point hover in chart === //
    var previousPoint = null;
    $(".period_prod_chart").bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $('#tooltip').remove();
                var x = item.datapoint[0].toFixed(2),
					y = item.datapoint[1].toFixed(2);

                maruti.flot_tooltip(item.pageX, item.pageY, item.series.label + " " + y);
            }

        } else {
            $('#tooltip').remove();
            previousPoint = null;
        }
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
    flot_tooltip: function (x, y, contents) {

        $('<div id="tooltip">' + contents + '</div>').css({
            top: y + 5,
            left: x + 5
        }).appendTo("body").fadeIn(200);
    }
}