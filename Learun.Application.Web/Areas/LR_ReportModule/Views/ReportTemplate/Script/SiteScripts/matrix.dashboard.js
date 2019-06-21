
var xaxis_lable = new Array();
var xaxis_value = new Array();
var yaxis_lable = new Array();
var yaxis_value = new Array();

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
    var xaxis_value0 = new Array();
    var xaxis_value1 = new Array();
    var xaxis_value2 = new Array();
    
    $.each(data, function (vi, item) {
        xaxis_value.length = 0;
        var gname = "";
        $.each(item, function (vj, titem) {
            if (vj == 0) {
                if (vi == 0) {
                    gname0 = titem.group_name;
                }else if (vi > 0 && vi == 1) {
                    gname1 = titem.group_name;
                } else{
                    gname2 = titem.group_name;
                }
            }
            if (vi == 0) {
                xaxis_lable.push([vj, titem.day_time + ":00"]);
            }
            if (vi == 0) {
                xaxis_value0.push([vj, titem.prod_num]);
            } else if (vi > 0 && vi == 1) {
                xaxis_value1.push([vj, titem.prod_num]);
            } else {
                xaxis_value2.push([vj, titem.prod_num]);
            }
        });
        if (arr_group_id.indexOf(item.group_id) < 0) {
            arr_group_id.push(item.group_id);
            arr_group.push([item.group_id, item.group_name]);
        }
    });
    
    var dataset = [{ label: gname0, data: xaxis_value0, color: "#00FF00" },
                { label: gname1, data: xaxis_value1, color: "#003300" },
                { label: gname2, data: xaxis_value2, color: "#0011FF" }
              ];
    procRealProdChart(xaxis_lable, gname0, gname1, gname2, xaxis_value0, xaxis_value1, xaxis_value2, dataset);
}

function procRealProdChart(xaxis_lable, gname0, gname1, gname2, xaxis_value0, xaxis_value1, xaxis_value2, dataset) {
    // === Make chart === //
    var plot = $.plot($(".shift_chart"), //dataset,
           [{ label: gname0, data: xaxis_value0, color: "#00FF00" },
               { label: gname1, data: xaxis_value1, color: "#003300" },
               { label: gname2, data: xaxis_value2, color: "#0011FF" }
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
    $(".shift_chart").bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $('#tooltip').fadeOut(1, function () {
                    $(this).remove();
                });
                var x = item.datapoint[0].toFixed(2),
					y = item.datapoint[1].toFixed(2);

                //maruti.flot_tooltip(item.pageX, item.pageY, item.series.label + " of " + x + " = " + y);
                maruti.flot_tooltip(item.pageX, item.pageY, "产量" + " of " + x + " = " + y);
            }

        } else {
            $('#tooltip').fadeOut(1, function () {
                $(this).remove();
            });
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
        "<li class=\"bg_lg\"> <a href=\"#\"> <i class=\"icon-play\"></i> <h4><span class=\"label label-success\">" + data[0].run_num + "</span></h4><strong> 运 行 </strong></a> </li>"
        + "<li class=\"bg_lr\"> <a href=\"#\"> <i class=\"icon-bullhorn\"></i><h4><span class=\"label label-success\">" + data[1].run_num + "</span></h4><strong> 报 警 </strong></a> </li>"
        + "<li class=\"bg_ly\"> <a href=\"#\"> <i class=\"icon-pause\"></i><h4><span class=\"label label-success\">" + data[2].run_num + "</span></h4><strong> 暂 停 </strong></a> </li>"
        + "<li class=\"bg_lh\"> <a href=\"#\"> <i class=\"icon-stop\"></i><h4><span class=\"label label-success\">" + data[3].run_num + "</span></h4><strong> 停 机 </strong></a> </li>"
        + "<li class=\"bg_lb\"> <a href=\"#\"> <i class=\"icon-edit\"></i><h4><span class=\"label label-success\">" + data[4].run_num + "</span></h4><strong> 空 闲 </strong></a> </li>"
    )
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