var alarm_daynum = 0;
var pdata;

$(document).ready(function () {
    ReTLRealRunState();
    ReTLRRealRunState();
    ReTL1RealProdNum();
    ReTL1RealPlanProdNum();
    ReTL2RealProdNum();
    ReTL2RealPlanProdNum();
    ReTL1RealProdNo();
    ReTL2RealProdNo();

    setInterval(ReTLRealRunState, 5000);
    setInterval(ReTLRRealRunState, 5000);
    setInterval(ReTL1RealProdNum, 5000);
    setInterval(ReTL1RealPlanProdNum, 5000);
    setInterval(ReTL2RealProdNum, 5000);
    setInterval(ReTL2RealPlanProdNum, 5000);
    setInterval(ReTL1RealProdNo, 5000);
    setInterval(ReTL2RealProdNo, 5000);

    $('#066ef').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#066gh').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReTLRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTLDeviceRunState",
        success: procTLRefreshRunState
    });
}

function procTLRefreshRunState(data) {
    pdata = data;
}
function ReTLRRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTLRDeviceRunState",
        success: procTLRRefreshRunState
    });
}

function procTLRRefreshRunState(data) {
    var target = $("#draw_topl");
    var str_tar = "";
    target.empty();
    $.each(data, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/" + item.pic_path + "\" style=\"height:300px;width:683px;\"/>";
    });
    $.each(pdata, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/" + item.pic_path + "\" style=\"height:300px;width:683px\"/>";
    });
    //alert(str_tar);
    target.append(str_tar);
}
function ReTL1RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTL1RealProdNum",
        success: procTL1RealProdNum
    });
}

function procTL1RealProdNum(data) {
    var target = $("#prod_num_topl1");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_topl1").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });
    
    tl1prod_num = prod_num;
    tl1pass_prod_num = pass_prod_num;
    tl1prod_rate = prod_rate;
    tl1tot_num = tot_num;

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
function ReTL2RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTL2RealProdNum",
        success: procTL2RealProdNum
    });
}

function procTL2RealProdNum(data) {
    var target = $("#prod_num_topl2");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_topl2").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    tl2prod_num = prod_num;
    tl2pass_prod_num = pass_prod_num;
    tl2prod_rate = prod_rate;
    tl2tot_num = tot_num;

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
        url: "UpdateAPlanProdNum",
        success: procPlanPordNum
    });
    $('#modal-6').modal('hide');
}
function procPlanPordNum(data) {
    var plan_num = $("#plan_num");
    plan_num.empty();
    plan_num.append(data);
}
function ReTL1RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTL1RealPlanProdNum",
        success: procTL1RealPlanProdNum
    });
}
function procTL1RealPlanProdNum(data) {
    var plan_num = $("#plan_num_topl1");
    plan_num.empty();
    plan_num.append(data);
    tl1plan_prod_num = data;
}
function ReTL2RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTL2RealPlanProdNum",
        success: procTL2RealPlanProdNum
    });
}
function procTL2RealPlanProdNum(data) {
    var plan_num = $("#plan_num_topl2");
    plan_num.empty();
    plan_num.append(data);
    tl2plan_prod_num = data;
}

function ReTL1RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTL1RealProdNo",
        success: procTL1RealProdNo
    });
}
function procTL1RealProdNo(data) {
    var prod_no = $("#prod_no_tl1");
    prod_no.empty();
    prod_no.append(data.product_no);
}
function ReTL2RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTL2RealProdNo",
        success: procTL2RealProdNo
    });
}
function procTL2RealProdNo(data) {
    var prod_no = $("#prod_no_tl2");
    prod_no.empty();
    prod_no.append(data.product_no);
}