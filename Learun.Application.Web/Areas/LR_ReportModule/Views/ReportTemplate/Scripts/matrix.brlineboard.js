var alarm_daynum = 0;

$(document).ready(function () {
    ReBRRealRunState();
    ReBR1RealProdNum();
    ReBR1RealPlanProdNum();
    ReBR2RealProdNum();
    ReBR2RealPlanProdNum();
    ReBLTRealProdNum();
    ReBLTRealPlanProdNum();
    ReBR1RealProdNo();
    ReBR2RealProdNo();

    setInterval(ReBRRealRunState, 5000);
    setInterval(ReBR1RealProdNum, 5000);
    setInterval(ReBR1RealPlanProdNum, 5000);
    setInterval(ReBR2RealProdNum, 5000);
    setInterval(ReBR2RealPlanProdNum, 5000);
    setInterval(ReBLTRealProdNum, 5000);
    setInterval(ReBLTRealPlanProdNum, 5000);
    setInterval(ReBR1RealProdNo, 5000);
    setInterval(ReBR2RealProdNo, 5000);

    $('#065cd').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#065ab').click(function () {
        $('#Line_Target').val(this.id);
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReBRRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBRDeviceRunState",
        success: procBRRefreshRunState
    });
}

function procBRRefreshRunState(data) {
    var target = $("#draw_btmr");
    var str_tar = "";
    target.empty();
    $.each(data, function (vi, item) {
        str_tar += "<img class=\"repngT2\" src=\"../../Areas/LR_ReportModule/Images/Content/images/" + item.pic_path + "\" style=\"height:300px;width:683px;\"/>";
    });
    //alert(str_tar);
    target.append(str_tar);
}
function ReBR1RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBR1RealProdNum",
        success: procBR1RealProdNum
    });
}

function procBR1RealProdNum(data) {
    var target = $("#prod_num_btmr1");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_btmr1").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
        else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    br1prod_num = prod_num;
    br1pass_prod_num = pass_prod_num;
    br1prod_rate = prod_rate;
    br1tot_num = tot_num;

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
function ReBR2RealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "GetBR2RealProdNum",
        success: procBR2RealProdNum
    });
}

function procBR2RealProdNum(data) {
    var target = $("#prod_num_btmr2");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    var plan_prod_num = $("#plan_num_btmr2").text();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        if (plan_prod_num == 0) prod_rate = 100.0;
        else prod_rate = (prod_num * 100.0 / plan_prod_num).toFixed(0);
    });

    br2prod_num = prod_num;
    br2pass_prod_num = pass_prod_num;
    br2prod_rate = prod_rate;
    br2tot_num = tot_num;
    
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
function ReBLTRealProdNum(data) {
    var target = $("#prod_num_btmlt");
    var str_tar = "";
    target.empty();
    var plan_prod_num = $("#plan_num_btmlt").text();

    var bprod_num = bl1prod_num + bl2prod_num + br1prod_num + br2prod_num;
    var bpass_prod_num = bl1pass_prod_num + bl2pass_prod_num + br1pass_prod_num + br2pass_prod_num;
    var btot_num = bl1tot_num + bl2tot_num + br1tot_num + br2tot_num;
    if (plan_prod_num == 0) bprod_rate = 100.0;
    else bprod_rate = (bprod_num * 100.0 / plan_prod_num).toFixed(0);

    str_tar += "<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + bprod_num + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + (bprod_num - bpass_prod_num) + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + bpass_prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + bprod_rate + "%</td></tr>"
               + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + ((bprod_num == 0) ? 0 : (bpass_prod_num) * 100.0 / bprod_num).toFixed(0) + "%</td></tr>"
               + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + btot_num + "</td></tr>";
    //alert(str_tar);
    target.append(str_tar);
}
function updatePlanProdNum() {
    alert($('#Line_Target').val());
    var plan_prod_num = $('#m_plan_num').val();
    var plan_prod_mor = $('#m_plan_mor').val();
    var plan_prod_mid = $('#m_plan_mid').val();
    var plan_prod_nig = $('#m_plan_nig').val();
    var line_target = $('#Line_Target').val();
    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            plan_prod_num: plan_prod_num,
            plan_prod_mor: plan_prod_mor,
            plan_prod_mid: plan_prod_mid,
            plan_prod_nig: plan_prod_nig,
            line_target: line_target
        },
        url: "UpdateBPlanProdNum",
        success: procPlanPordNum
    });
    $('#modal-6').modal('hide');
}
function procPlanPordNum(data) {
    var plan_num = $("#plan_num_btmr1");
    plan_num.empty();
    plan_num.append(data);
}
function ReBR1RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBR1RealPlanProdNum",
        success: procBR1RealPlanProdNum
    });
}
function procBR1RealPlanProdNum(data) {
    var plan_num = $("#plan_num_btmr1");
    plan_num.empty();
    plan_num.append(data);
    br1plan_prod_num = data;
}
function ReBR2RealPlanProdNum() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBR2RealPlanProdNum",
        success: procBR2RealPlanProdNum
    });
}
function procBR2RealPlanProdNum(data) {
    var plan_num = $("#plan_num_btmr2");
    plan_num.empty();
    plan_num.append(data);
    br2plan_prod_num = data;
}
function ReBLTRealPlanProdNum() {
    var plan_num = $("#plan_num_btmlt");
    plan_num.empty();
    bplan_prod_num = bl1plan_prod_num + bl2plan_prod_num + br1plan_prod_num + br2plan_prod_num;
    plan_num.append(bplan_prod_num);
}
function ReBR1RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBR1RealProdNo",
        success: procBR1RealProdNo
    });
}
function procBR1RealProdNo(data) {
    var prod_no = $("#prod_no_br1");
    prod_no.empty();
    prod_no.append(data.product_no);
}
function ReBR2RealProdNo() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "GetBR2RealProdNo",
        success: procBR2RealProdNo
    });
}
function procBR2RealProdNo(data) {
    var prod_no = $("#prod_no_br2");
    prod_no.empty();
    prod_no.append(data.product_no);
}