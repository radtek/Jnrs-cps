$(document).ready(function () {
    refreshDataTable();
    SelectALineChange();
    SelectLineChangeSeries();

    $("#aLine").change(function () { SelectLineChangeSeries(); SelectALineChange(); });
    $("#aSeries").change(function () { SelectSeriesChangeMachine(); });
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
    refreshDataTable();
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
function deleteMachinePhoto(group_id, machine_id, pic_state, pic_name, pic_kinds) {
    //获取模态框数据  
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
function refreshDataTable() {

    var saLine = $("#aLine").val();
    var $table = $('#table');
    $table.bootstrapTable('destroy');

    $table.bootstrapTable({
        method: "post",
        dataType: "json",
        queryParams: { group_id: saLine },
        url: "/PView/GetMachinePhoto",
        //classes: "table table-bordered table-striped",
        contentType: "application/x-www-form-urlencoded",
        striped: true,
        showRefresh: true,
        pagination: true, //分页
        singleSelect: false,
        locale: "zh-US", //表格汉化
        search: false, //显示搜索框
        pageSize: 6,
        pageList: [10, 50, 100, 200, 500],
        minimunCountColumns: 4,
        //sidePagination: "server", //服务端处理分页
        columns:
            [{
                title: "自动线",
                field: "group_name",
                align: "center",
                valign: "middle"
            },
            {
                title: "设备名称",
                field: "machine_name",
                align: "center",
                valign: "middle",
            },
            {
                title: "设备系列",
                field: "machine_series",
                align: "center",
            },
            {
                title: "设备编号",
                field: "machine_number",
                align: "center",
            },
            {
                title: "设备状态",
                field: "pic_states",
                align: "center",
            },
            {
                title: "照片名称",
                field: "pic_name",
                align: "center",
            },
            {
                title: "用途",
                field: "pic_kinds",
                align: "center",
            },
            {
                title: "路径",
                field: "pic_path",
                align: "center",
            },
            {
                title: "顺序号",
                field: "rank_num",
                align: "center",
            },
            {
                title: "说明",
                field: "memo",
                align: "center",
            },
            {
                title: "操作",
                fields: "group_id",
                fields: "machine_id",
                fields: "pic_state",
                fields: "pic_name",
                fields: "pic_kinds",
                align: 'center',
                formatter: function (value, row, index) {
                    var e = "<a href=\"javascript:;\" onclick=\"jQuery('#modal-6').modal('show', {backdrop: 'static'});\" class=\"edit_modal\">编辑</a> ";
                    var d = "<a href=\"javascript:;\" onclick=\"deleteMachinePhoto(" + row.group_id + "," + row.machine_id + "," + row.pic_state + "," + row.pic_name + "," + row.pic_kinds + ");\">删除</a> ";
                    return e + d;
                }
            }],
        onClickRow: function (row) {
            $('#m_edit_id').val(row.group_id);
            $('#m_group_id').val(row.group_id);
            $('#m_machine_id').val(row.machine_id);
            $('#m_pic_state').val(row.pic_state);
            $('#m_pic_name').val(row.pic_name);
            $('#m_pic_kinds').val(row.pic_kinds);
        }
    });
}