var alarm_daynum = 0;
var pdata;

$(document).ready(function () {
    ReBLRealRunState();
    ReBLRRealRunState();
    ReBL1RealProdNum();
    ReBL1RealPlanProdNum();
    ReBL2RealProdNum();
    ReBL2RealPlanProdNum();
    ReBL1RealProdNo();
    ReBL2RealProdNo();

    setInterval(ReBLRealRunState, 5000);
    setInterval(ReBLRRealRunState, 5000);
    setInterval(ReBL1RealProdNum, 5000);
    setInterval(ReBL1RealPlanProdNum, 5000);
    setInterval(ReBL2RealProdNum, 5000);
    setInterval(ReBL2RealPlanProdNum, 5000);
    setInterval(ReBL1RealProdNo, 5000);
    setInterval(ReBL2RealProdNo, 5000);

    $('#065ef').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#065gh').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReBLRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBLDeviceRunState",
        success: procBLRefreshRunState
    });
}

function procBLRefreshRunState(data) {
    pdata = data;
}
function ReBLRRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBLRDeviceRunState",
        success: procBLRRefreshRunState
    });
}

function procBLRRefreshRunState(data) {
    var target = $("#draw_btml");
    var str_tar = "";
    target.empty();
    $.each(data, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/images/" + item.pic_path + "\" style=\"height:300px;width:683px;\"/>";
    });
    $.each(pdata, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/images/" + item.pic_path + "\" style=\"height:300px;width:683px;\"/>";
    });
    //alert(str_tar);
    target.append(str_tar);
}
function ReBL1RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBL1RealProdNum",
        success: procBL1RealProdNum
    });
}

function procBL1RealProdNum(data) {
    var target = $("#prod_num_btml1");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_btml1").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
        else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    bl1prod_num = prod_num;
    bl1pass_prod_num = pass_prod_num;
    bl1prod_rate = prod_rate;
    bl1tot_num = tot_num;

    str_tar += "<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + prod_num + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + (prod_num - pass_prod_num) + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + pass_prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + prod_rate + "%</td></tr>"
               + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + ((prod_num == 0) ? 0 : (pass_prod_num) * 100.0 / prod_num).toFixed(0) + "%</td></tr>"
               + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + tot_num + "</td></tr>";
    //alert(str_tar);
    target.append(str_tar);
}
function ReBL2RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBL2RealProdNum",
        success: procBL2RealProdNum
    });
}

function procBL2RealProdNum(data) {
    var target = $("#prod_num_btml2");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_tbml2").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
        else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    bl2prod_num = prod_num;
    bl2pass_prod_num = pass_prod_num;
    bl2prod_rate = prod_rate;
    bl2tot_num = tot_num;

    str_tar += "<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + prod_num + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + (prod_num - pass_prod_num) + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + pass_prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + prod_rate + "%</td></tr>"
               + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + ((prod_num == 0) ? 0 : (pass_prod_num) * 100.0 / prod_num).toFixed(0) + "%</td></tr>"
               + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + tot_num + "</td></tr>";
    //alert(str_tar);
    target.append(str_tar);
}

function updatePlanProdNum() {
    var plan_prod_num = $('#m_plan_num').val();
    var plan_prod_mor = $('#m_plan_mor').val();
    var plan_prod_mid = $('#m_plan_mid').val();
    var plan_prod_nig = $('#m_plan_nig').val();

    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            plan_prod_num: plan_prod_num,
            plan_prod_mor: plan_prod_mor,
            plan_prod_mid: plan_prod_mid,
            plan_prod_nig: plan_prod_nig,
        },
        url: "UpdateBPlanProdNum",
        success: procPlanPordNum
    });
    $('#modal-6').modal('hide');
}
function procPlanPordNum(data) {
    var plan_num = $("#plan_num");
    plan_num.empty();
    plan_num.append(data);
}
function ReBL1RealPlanProdNum() {
    var line_target = "065ef";
    $.ajax({
        type: "POST",
        dataType: "json",
        data: {
            line_target: line_target,
        },
        url: "GetBL1RealPlanProdNum",
        success: procBL1RealPlanProdNum
    });
}
function procBL1RealPlanProdNum(data) {
    var plan_num = $("#plan_num_btml1");
    plan_num.empty();
    plan_num.append(data);
    bl1plan_prod_num = data;
}
function ReBL2RealPlanProdNum() {
    var line_target = "065gh";
    $.ajax({
        type: "POST",
        dataType: "json",
        data: {
            line_target: line_target,
        },
        url: "GetBL1RealPlanProdNum",
        success: procBL2RealPlanProdNum
    });
}
function procBL2RealPlanProdNum(data) {
    var plan_num = $("#plan_num_btml2");
    plan_num.empty();
    plan_num.append(data);
    bl2plan_prod_num = data;
}
function ReBL1RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBL1RealProdNo",
        success: procBL1RealProdNo
    });
}
function procBL1RealProdNo(data) {
    var prod_no = $("#prod_no_bl1");
    prod_no.empty();
    prod_no.append(data.product_no);
}
function ReBL2RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBL2RealProdNo",
        success: procBL2RealProdNo
    });
}
function procBL2RealProdNo(data) {
    var prod_no = $("#prod_no_bl2");
    prod_no.empty();
    prod_no.append(data.product_no);
}