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
    ReShiftProdNum(group_id, machine_id, startDate, endDate);
    ReQueryMaxProd(group_id, machine_id, startDate, endDate);
    ReShiftMaxProd(group_id, machine_id, startDate, endDate);
});
//------------- btn_query -------------//
$(document).ready(function () {
    var listDate = new Array();
    $("#btn_query").click(function () {
        var group_id = $("#aLine").val();
        var machine_id = $("#aDevice").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        RePeriodProdNum(group_id,machine_id,startDate,endDate);
        ReShiftProdNum(group_id, machine_id, startDate, endDate);
        ReQueryMaxProd(group_id, machine_id, startDate, endDate);
        ReShiftMaxProd(group_id, machine_id, startDate, endDate);
    });
    $("#query_max_prod").click(function () {
        var group_id = $("#aLine").val();
        var machine_id = $("#aDevice").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        //jQuery('#modal-5').modal('show', { backdrop: 'static' });
    });
    $("#query_conform_prod").click(function () {
        var group_id = $("#aLine").val();
        var machine_id = $("#aDevice").val();
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        //jQuery('#modal-6').modal('show', { backdrop: 'static' });

    });
    
    $('#modal-5').on('show.bs.modal', function (e) {
        //editInfo('#modal-6');
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        var date1 = new Date(startDate);
        var date2 = new Date(endDate);
        var days = Math.ceil((date2.getTime() - date1.getTime()) / 86400000);
        
        listDate.length = 0;
        var target = $(".modal-body-5");
        target.empty();
        var strget = "";
        for (var vi = 0; vi <= days; vi++) {
            listDate.push(date1.getDate + vi);
            strget += "<div class=\"row\"><div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\"> "
                   + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:60%;font-size:16px;padding-top:5px\">"
                   + date1.getFullYear() + "-" + (date1.getMonth() + 1) + "-" + (date1.getDate() + vi) + "</label>"
                   + "<input type=\"checkbox\" class=\"form-control-query\" style=\"margin-left:15px;padding-top:5px\" id=\"field-1\"></div></div>"
				   + "<div class=\"col-md-6\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                   + "<input type=\"text\" class=\"form-control\" style=\"font-size:small;padding-top:5px\" id=\"field-2\" placeholder=\"备注\"></div></div></div>";
        }
        target.append(strget);
    })
    $('#modal-6').on('show.bs.modal', function (e) {
        //editInfo('#modal-6');
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();
        
        var date1 = new Date(startDate);
        var date2 = new Date(endDate);
        var days = Math.ceil((date2.getTime() - date1.getTime()) / 86400000);
        
        listDate.length = 0;
        var target = $(".modal-body-6");
        target.empty();
        var strget = "";
        for (var vi = 0; vi <= days; vi++) {
            listDate.push(date1.getDate + vi);
            strget += "<div class=\"row\"><div class=\"col-md-3\"><div class=\"form-group\" style=\"margin-bottom:5px\"> "
                   + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:80%;font-size:16px;padding-top:5px\">"
                   + date1.getFullYear() + "-" + (date1.getMonth() + 1) + "-" + (date1.getDate() + vi) + "</label></div></div>"
				   + "<div class=\"col-md-3\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                   + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:30%;font-size:16px;padding-top:5px\">早班</label>"
                   + "<input type=\"text\" class=\"form-control\" style=\"font-size:small;padding-top:5px\" id=\"field-2\" placeholder=\"合格量\"></div></div>"
				   + "<div class=\"col-md-3\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
				   + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:30%;font-size:16px;padding-top:5px\">中班</label>"
                   + "<input type=\"text\" class=\"form-control\" style=\"font-size:small;padding-top:5px\" id=\"field-2\" placeholder=\"合格量\"></div></div>"
				   + "<div class=\"col-md-3\"><div class=\"form-group\" style=\"margin-bottom:5px\">"
                   + "<label for=\"field-1\" class=\"control-label-query\" style=\"width:30%;font-size:16px;padding-top:5px\">夜班</label>"
                   + "<input type=\"text\" class=\"form-control\" style=\"font-size:small;padding-top:5px\" id=\"field-2\" placeholder=\"合格量\"></div></div></div>";
        }
        target.append(strget);
    })
});

var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_value = new Array();
function RePeriodProdNum(group_id,machine_id,startDate,endDate) {
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
           [{ data: yaxis_value, label: "产量", color: "#4fb9f0" }], {
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

var xaxis_shift_lable = new Array();
var xaxis_shift_value = new Array();
var yaxis_shift_value = new Array();
var zaxis_shift_value = new Array();
function ReShiftProdNum(group_id, machine_id, startDate, endDate) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            start_date: startDate,
            end_date: endDate
        },
        url: "/PView/GetShiftProdNum_His",
        success: procShiftProdNum
    });
}

function procShiftProdNum(data) {
    xaxis_shift_lable = xaxis_lable;
    xaxis_shift_value.length = 0;
    yaxis_shift_value.length = 0;
    zaxis_shift_value.length = 0;
    var vx = 0, vy = 0, vz = 0, vk = 0;
    $.each(data, function (i, item) {
        //alert(item.wshift_name);
        if (item.wshift_name.indexOf('早班') >= 0) {
            xaxis_shift_value.push([vx++, item.prod_num]);
        }
        if (item.wshift_name == "中班") {
            yaxis_shift_value.push([vy++, item.prod_num]);
        }
        if (item.wshift_name == "夜班") {
            zaxis_shift_value.push([vz++, item.prod_num]);
        }
    });
    procShiftProdChart(xaxis_lable, xaxis_shift_value, yaxis_shift_value, zaxis_shift_value);
}

function procShiftProdChart(xaxis_lable, xaxis_shift_value, yaxis_shift_value, zaxis_shift_value) {
    // === Make chart === //
    var plot = $.plot($(".shift_prod_chart"),
           [{ data: xaxis_shift_value, label: "早班", color: "#ee7951" },
            { data: yaxis_shift_value, label: "中班", color: "#4fb9f0" },
            { data: zaxis_shift_value, label: "夜班", color: "#801990" }
           ], {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               bars : {
                   show : false,
                   barWidth : 0.3,
                   order : 1,
                   fill:0.3,
                   lineWidth: 1,
                   fillColor: { colors: [{ color: "#ee7951", opacity: 1 }, { color: "#4fb9f0", opacity: 1 }, { color: "#801990", opacity: 1 }] },
                   align: "center",
               },
               grid: { hoverable: true, clickable: true },
               xaxis: { ticks: xaxis_lable }
               //yaxis: { min: 0, max: 1000 }
           });

    // === Point hover in chart === //
    previousPoint = null;
    $(".shift_prod_chart").bind("plothover", function (event, pos, item) {

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
	peity: function(){
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
		}).appendTo("body").fadeIn(200);
	}
}
function ReQueryMaxProd(group_id, machine_id, startDate, endDate) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            start_date: startDate,
            end_date: endDate
        },
        url: "/PView/GetQueryMaxProd_His",
        success: procQueryMaxProd
    });
}
function procQueryMaxProd(data) {
    var target = $("#query_max_prod");
    target.empty();
    target.append(
        "<li class=\"bg_sh\"><i class=\"icon-flag\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data1 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">单日最高产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-fire\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data2 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">单日最低产量</small></li>"
    )
    var target2 = $("#query_conform_prod");
    target2.empty();
    target2.append(
        "<li class=\"bg_sh\"><i class=\"icon-glass\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.dou_data3 + "<b>&nbsp;</b></strong></div>"
        + "    </div><small style=\"color:whitesmoke\">总计产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-leaf\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.dou_data4 + "<b>&nbsp;</b></strong></div>"
        + "    </div><small style=\"color:whitesmoke\">平均日产量</small></li>"
    )
}
function ReShiftMaxProd(group_id, machine_id, startDate, endDate) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            start_date: startDate,
            end_date: endDate
        },
        url: "/PView/GetQueryShiftMaxProd_His",
        success: procQueryShiftMaxProd
    });
}
function procQueryShiftMaxProd(data) {
    var target = $("#shift_max_prod");
    target.empty();
    target.append(
        "<li class=\"bg_sh\"><i class=\"icon-flag\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data1 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">班次最高产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-fire\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.int_data2 + "</strong></div>"
        + "    </div><small style=\"color:whitesmoke\">班次最低产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-glass\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.dou_data3 + "<b>&nbsp;</b></strong></div>"
        + "    </div><small style=\"color:whitesmoke\">班次合计产量</small></li>"
        + "<li class=\"bg_sh\"><i class=\"icon-leaf\" style=\"color:yellowgreen\"></i>"
        + "    <div class=\"progress-data progress-data-warning\">"
        + "        <div style=\"width:100%\" class=\"bar\"><strong style=\"color:#f00;padding-top:5px\">" + data.dou_data4 + "<b>&nbsp;</b></strong></div>"
        + "    </div><small style=\"color:whitesmoke\">班次平均产量</small></li>"
    )
}