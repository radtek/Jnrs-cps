var alarm_daynum = 0;

$(document).ready(function () {
    ReTRRealRunState();
    ReTR1RealProdNum();
    ReTR1RealPlanProdNum();
    ReTR2RealProdNum();
    ReTR2RealPlanProdNum();
    ReTLTRealProdNum();
    ReTLTRealPlanProdNum();
    ReTR1RealProdNo();
    ReTR2RealProdNo();

    setInterval(ReTRRealRunState, 5000);
    setInterval(ReTR1RealProdNum, 5000);
    setInterval(ReTR1RealPlanProdNum, 5000);
    setInterval(ReTR2RealProdNum, 5000);
    setInterval(ReTR2RealPlanProdNum, 5000);
    setInterval(ReTLTRealProdNum, 5000);
    setInterval(ReTLTRealPlanProdNum, 5000);
    setInterval(ReTR1RealProdNo, 5000);
    setInterval(ReTR2RealProdNo, 5000);

    $('#066ab').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#066cd').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReTRRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTRDeviceRunState",
        success: procTRRefreshRunState
    });
}

function procTRRefreshRunState(data) {
    var target = $("#draw_topr");
    var str_tar = "";
    target.empty();
    $.each(data, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/" + item.pic_path + "\" style=\"height:300px;width:683px\"/>";
    });
    //alert(str_tar);
    target.append(str_tar);
}
function ReTR1RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTR1RealProdNum",
        success: procTR1RealProdNum
    });
}

function procTR1RealProdNum(data) {
    var target = $("#prod_num_topr1");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_topr1").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
       else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });
    
    tr1prod_num = prod_num;
    tr1pass_prod_num = pass_prod_num;
    tr1prod_rate = prod_rate;
    tr1ttot_num = tot_num;

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
function ReTR2RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetTR2RealProdNum",
        success: procTR2RealProdNum
    });
}

function procTR2RealProdNum(data) {
    var target = $("#prod_num_topr2");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_topr2").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
        else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    tr2prod_num = prod_num;
    tr2pass_prod_num = pass_prod_num;
    tr2prod_rate = prod_rate;
    tr2tot_num = tot_num;

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
function ReTLTRealProdNum(data) {
    var target = $("#prod_num_toplt");
    var str_tar = "";
    target.empty();
    var plan_prod_num = $("#plan_num_toplt").text();

    var tprod_num = tl1prod_num + tl2prod_num + tr1prod_num + tr2prod_num;
    var tpass_prod_num = tl1pass_prod_num + tl2pass_prod_num + tr1pass_prod_num + tr2pass_prod_num;
    var ttot_num = tl1tot_num + tl2tot_num + tr1tot_num + tr2tot_num;
    if (plan_prod_num == 0) tprod_rate = 100.0;
    else tprod_rate = (tprod_num * 100.0 / plan_prod_num).toFixed(0);

    str_tar += "<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + tprod_num + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + (tprod_num - tpass_prod_num) + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + tpass_prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + tprod_rate + "%</td></tr>"
               + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + ((tprod_num == 0) ? 0 : (tpass_prod_num) * 100.0 / tprod_num).toFixed(0) + "%</td></tr>"
               + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + ttot_num + "</td></tr>";
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
function ReTR1RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTR1RealPlanProdNum",
        success: procTR1RealPlanProdNum
    });
}
function procTR1RealPlanProdNum(data) {
    var plan_num = $("#plan_num_topr1");
    plan_num.empty();
    plan_num.append(data);
    tr1plan_prod_num = data;
}
function ReTR2RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTR2RealPlanProdNum",
        success: procTR2RealPlanProdNum
    });
}
function procTR2RealPlanProdNum(data) {
    var plan_num = $("#plan_num_topr2");
    plan_num.empty();
    plan_num.append(data);
    tr2plan_prod_num = data;
}
function ReTLTRealPlanProdNum() {
    var plan_num = $("#plan_num_toplt");
    plan_num.empty();
    tplan_prod_num = tl1plan_prod_num + tl2plan_prod_num + tr1plan_prod_num + tr2plan_prod_num;
    plan_num.append(tplan_prod_num);
}
function ReTR1RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTR1RealProdNo",
        success: procTR1RealProdNo
    });
}
function procTR1RealProdNo(data) {
    var prod_no = $("#prod_no_tr1");
    prod_no.empty();
    prod_no.append(data.product_no);
}
function ReTR2RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetTR2RealProdNo",
        success: procTR2RealProdNo
    });
}
function procTR2RealProdNo(data) {
    var prod_no = $("#prod_no_tr2");
    prod_no.empty();
    prod_no.append(data.product_no);
}
