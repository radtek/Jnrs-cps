$(document).ready(function () {
    SelectALineChange();
    SelectLineChangeSeries();

    $("#aLine").change(function () { SelectLineChangeSeries(); SelectALineChange(); });
    $("#aSeries").change(function () { SelectSeriesChangeMachine(); });

    //触发模态框的同时调用此方法 
    $(".edit-modal").click(function () {
        var edit_id = $(this).data('rowId');
        editInfo('#modal-6', edit_id);
    });
});

jQuery('#submit').click(function () {

    var group_id = $("#aLine").val();
    var machine_id = $('#aSeriesDevice').val();
    var state_value = $('#aMacState').val();

    var pic_name = $('#aPicName').val();
    var pic_kinds = $('#aPicKinds').val();
    var pic_path = $('#aPicPath').val();
    var rank_num = $('#aRankNum').val();
    var memo = $('#aMemo').val();

    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            pic_state: state_value,
            pic_name: pic_name,
            pic_kinds: pic_kinds,
            pic_path: pic_path,
            rank_num: rank_num,
            memo: memo
        },
        url: "/Home/AddMachinePhoto",
        success: alert("suc")
    });
});
function SelectALineChange() {
    //获取下拉框选中项的value属性值
    var saLine = $("#aLine").val();

    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            group_id: saLine
        },
        url: "/PView/GetMachinePhoto/" + group_id,
        success: procMachinePhoto
    });
}

function procMachinePhoto(data) {
    // === Make chart === //
    var target = $("#tb_machine_photo");
    target.empty();
    var tr_txt = "";
    $.each(data, function (i, item) {
        if (i % 2 == 1) {
            tr_txt += "<tr class=\"odd gradeX\">"
                      + "<td>" + item.group_name + "</td>"
                      + "<td>" + item.machine_name + "</td>"
                      + "<td>" + item.machine_series + "</td>"
                      + "<td class=\"center\">" + item.machine_number + "</td>"
                      + "<td class=\"center\">" + item.pic_states + "</td>"
                      + "<td class=\"center\">" + item.pic_name + "</td>"
                      + "<td class=\"center\">" + item.pic_kinds + "</td>"
                      + "<td class=\"center\">" + item.pic_path + "</td>"
                      + "<td class=\"center\">" + item.rank_num + "</td>"
                      + "<td class=\"center\">" + item.memo + "</td>"
                      + "<td class=\"center\"><input type=\"hidden\" name=\"group_id\" value=\"" + item.group_id + "\"/>"
                      + "<input type=\"hidden\" name=\"machine_id\" value=\"" + item.machine_id + "\">"
                      + "<input type=\"hidden\" name=\"pic_state\" value=\"" + item.pic_state + "\">"
                      + "<input type=\"hidden\" name=\"pic_name\" value=\"" + item.pic_name + "\">"
                      + "<input type=\"hidden\" name=\"pic_kinds\" value=\"" + item.pic_kinds + "\">"
                      + "<a href=\"javascript:;\" onclick=\"jQuery('#modal-6').modal('show', {backdrop: 'static'});\" data-row-id=" + i + " class=\"btn btn-info btn-large edit-modal\">编辑</a>"
                      + "<a href=\"javascript:;\" onclick=\"deleteMachinePhoto(" + i + ");\" class=\"btn btn-info btn-large\">删除</a>"
                      + "</tr>"
        } else {
            tr_txt += "<tr class=\"even gradeC\">"
                      + "<td>" + item.group_name + "</td>"
                      + "<td>" + item.machine_name + "</td>"
                      + "<td>" + item.machine_series + "</td>"
                      + "<td class=\"center\">" + item.machine_number + "</td>"
                      + "<td class=\"center\">" + item.pic_states + "</td>"
                      + "<td class=\"center\">" + item.pic_name + "</td>"
                      + "<td class=\"center\">" + item.pic_kinds + "</td>"
                      + "<td class=\"center\">" + item.pic_path + "</td>"
                      + "<td class=\"center\">" + item.rank_num + "</td>"
                      + "<td class=\"center\">" + item.memo + "</td>"
                      + "<td class=\"center\"><input type=\"hidden\" name=\"group_id\" value=\"" + item.group_id + "\"/>"
                      + "<input type=\"hidden\" name=\"machine_id\" value=\"" + item.machine_id + "\">"
                      + "<input type=\"hidden\" name=\"pic_state\" value=\"" + item.pic_state + "\">"
                      + "<input type=\"hidden\" name=\"pic_name\" value=\"" + item.pic_name + "\">"
                      + "<input type=\"hidden\" name=\"pic_kinds\" value=\"" + item.pic_kinds + "\">"
                      + "<a href=\"javascript:;\" onclick=\"jQuery('#modal-6').modal('show', {backdrop: 'static'});\" data-row-id=" + i + " class=\"btn btn-info btn-large edit-modal\">编辑</a>"
                      + "<a href=\"javascript:;\" onclick=\"deleteMachinePhoto(" + i + ");\" class=\"btn btn-info btn-large\">删除</a>"
                      + "</tr>"
        }
    });
    target.append(tr_txt);
}

function editInfo(obj, edit_id) {
    var id = $(obj).attr("id");
    //获取表格中的一行数据
    var tag_input = document.getElementById("tb_machine_photo").rows[edit_id].cells[10].getElementsByTagName("input");
    var group_id = tag_input[0].value;
    var machine_id = tag_input[1].value;
    var pic_state = tag_input[2].value;
    var pic_name = tag_input[3].value;
    var pic_kinds = tag_input[4].value;

    //向模态框中传值  
    $('#group_id').val(group_id);
    $('#machine_id').val(machine_id);
    $('#pic_state').val(pic_state);
    $('#pic_name').val(pic_name);
    $('#pic_kinds').val(pic_kinds);
    $('#m_aPicName').val(pic_name);
    $('#m_aPicKinds').val(pic_kinds);
}
//提交更改  
function updateMachinePhoto() {
    //获取模态框数据  
    var group_id = $('#group_id').val();
    var machine_id = $('#machine_id').val();
    var pic_state = $('#pic_state').val();
    var pic_name = $('#m_aPicName').val();
    var pic_kinds = $('#m_aPicKinds').val();

    var pic_path = $('#m_aPicPath').val();
    var rank_num = $('#m_aRankNum').val();
    var memo = $('#m_aMemo').val();

    $.ajax({
        type: "POST",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            pic_state: pic_state,
            pic_name: pic_name,
            pic_kinds: pic_kinds,
            pic_path: pic_path,
            rank_num: rank_num,
            memo: memo
        },
        dataType: 'json',
        url: "/Home/UpdateMachinePhoto",
        success: function (data) {
            $('#modal-on0').modal('show', { backdrop: 'static' });
        }
    });
    $('#modal-6').modal('hide');
}
//提交更改  
function deleteMachinePhoto(vcount) {
    //获取模态框数据  
    var tag_input = document.getElementById("tb_machine_photo").rows[vcount].cells[10].getElementsByTagName("input");
    var group_id = tag_input[0].value;
    var machine_id = tag_input[1].value;
    var pic_state = tag_input[2].value;
    var pic_name = tag_input[3].value;
    var pic_kinds = tag_input[4].value;

    $.ajax({
        type: "POST",
        data: {
            group_id: group_id,
            machine_id: machine_id,
            pic_state: pic_state,
            pic_name: pic_name,
            pic_kinds: pic_kinds
        },
        dataType: 'json',
        url: "/Home/DeleteMachinePhoto",
        success: function (data) {
            $('#modal-on0').modal('show', { backdrop: 'static' });
        }
    });
}