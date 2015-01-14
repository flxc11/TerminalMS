<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerList.aspx.cs" Inherits="WebSite.admin.ManagerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=HD.Config.UIConfig.SoftName %></title>
    <link rel="stylesheet" href="/css/Global.css" />
    <link rel="stylesheet" href="/themes/default/easyui.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="/themes/icon.css" />
    <script src="/js/jquery.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script src="/js/easyui-lang-zh_CN.js"></script>
    <script src="/js/cnvp.js"></script>
    <script src="/js/jQuery.query.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap-header">
        <div class="header">
            <div class="lg-info">
                <h2>您好!欢迎使用本系统</h2>
                <a href="#">
                    <span class="manager"><%=LoginInfo.LoginName %></span>
                </a>
                <a href="login.aspx">
                    <span class="lg-out">退 出</span>
                </a>
            </div>
        </div>
    </div>
    <div class="main-l">
        <!--#include file="/control/adminleft.html" -->
    </div>
    <div class="main-r">
        <div id="tb"></div>
    </div>
    </form>
</body>
<script>
    function getWidth(percent) {
        //return document.body.clientWidth * percent ;
        return $(".main-r").width() * percent;
    }
    var editIndex = undefined;
    function endEditing() {
        if (editIndex == undefined) { return true }
        if ($('#tb').datagrid('validateRow', editIndex)) {
            var ed = $('#tb').datagrid('getEditor', { index: editIndex, field: 'Id' });
            $('#tb').datagrid('endEdit', editIndex);
            editIndex = undefined;
            return true;
        } else {
            return false;
        }
    }
    function onClickRow(index) {
        if (editIndex != index) {
            if (endEditing()) {
                $('#tb').datagrid('selectRow', index)
                        .datagrid('beginEdit', index);
                editIndex = index;
            } else {
                $('#tb').datagrid('selectRow', editIndex);
            }
        }
    }
    function append() {
        if (endEditing()) {
            $('#tb').datagrid('appendRow', { status: 'P' });
            editIndex = $('#tb').datagrid('getRows').length - 1;
            $('#tb').datagrid('selectRow', editIndex)
                    .datagrid('beginEdit', editIndex);
        }
    }
    function removeit() {
        if (editIndex == undefined) {
            $.messager.alert('消息', '请选择需要删除的行！', 'info');
            return;
        }

        $.messager.confirm('确认', '您确认想要删除该用户吗？', function (r) {
            if (r) {
                var selectedRow = $('#tb').datagrid('getSelected');  //获取选中行
                $.ajax({
                    url: 'adminjson.aspx?Time=' + new Date().getTime(),
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        action: 'DeleteManager',
                        Id: selectedRow.Id
                    }
                })
                .done(function (d) {
                    if (d.returnval == "1") {
                        $.messager.alert('提示', '删除成功');
                    } else if (d.returnval == "0") {
                        $.messager.alert('警告', '删除失败');
                    };
                })
                .fail(function (d) {
                    $.messager.alert('警告', '删除失败');
                })
                .always(function () {
                    //console.log("complete");
                    $('#tb').datagrid('reload');
                });
            } else {
                reject();
            }
        });

        // $('#tb').datagrid('cancelEdit', editIndex)
        //         .datagrid('deleteRow', editIndex);
        editIndex = undefined;
    }
    function accept() {
        if (endEditing()) {
            //$('#tb').datagrid('acceptChanges');
            var selectedRow = $('#tb').datagrid('getSelected');  //获取选中行
            $.ajax({
                url: 'adminjson.aspx?Time=' + new Date().getTime(),
                type: 'POST',
                dataType: 'json',
                data: {
                    action: 'UpdateManager',
                    Id: selectedRow.Id,
                    TrueName: selectedRow.TrueName,
                    UserTel: selectedRow.UserTel,
                    UserUnit: selectedRow.UserUnit,
                    UserEmail: selectedRow.UserEmail
                }
            })
            .done(function (d) {
                //$('#tb').datagrid('acceptChanges');
                if (d.returnval == "1") {

                };
            })
            .fail(function () {
                $.messager.alert('警告', '更新失败');
                //console.log("error");
            })
            .always(function () {
                //console.log("complete");
                $('#tb').datagrid('reload');
            });

            //console.log(selectedRow)
        }
    }
    function reject() {
        $('#tb').datagrid('rejectChanges');
        editIndex = undefined;
    }
    function getChanges() {
        var rows = $('#tb').datagrid('getChanges');
        alert(rows.length + ' rows are changed!');
    }
    function resetUserPass() {
        $.messager.prompt('提示信息', '请输入新的密码：', function (r) {
            if (r) {
                var selectedRow = $('#tb').datagrid('getSelected');  //获取选中行
                $.ajax({
                    url: 'adminjson.aspx?Time=' + new Date().getTime(),
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        action: 'ResetManagerPass',
                        Id: selectedRow.Id,
                        UserPass: r
                    }
                })
                .done(function (d) {
                    if (d.returnval == 1) {
                        $.messager.alert('提示', '密码重置成功');

                    };
                })
                .fail(function (d) {
                    if (d.returnval == 0) {
                        $.messager.alert('警告', '密码重置失败');
                    };
                })
                .always(function () {
                    //console.log("complete");
                    $('#tb').datagrid('reload');
                });
            } else {
                reject();
            }
        });
    }
    var fields = "Id,UserName,TrueName,UserTel,UserUnit,UserEmail,CreateTime";
    var flag = true;
    // var grid = $('#tb');
    // var options = grid.datagrid('getPager').data("pagination").options;
    // var curr = options.pageNumber;
    // var total = options.total;
    // var max = Math.ceil(total/options.pageSize);
    //var page = $.query.get("page");
    $('#tb').datagrid({
        title: '当前位置：账号管理列表',
        width: 'auto',
        height: 'auto',
        nowrap: false,
        striped: true,  //是否显示斑马线效果
        url: 'adminjson.aspx',
        queryParams: {
            action: 'GetManagerList',
            easyGrid_Sort: fields,
            Time: new Date().getTime()
        },
        idField: 'Id',
        fix: false,
        frozenColumns: [[

        ]],
        onClickRow: onClickRow,
        singleSelect: true,
        columns: [[
            {
                title: '序号', field: 'ooo', width: getWidth(0.05), align: 'center',
                formatter: function (value, row, index) {
                    return index + 1;
                }
            },
            {
                title: '用户名', field: 'UserName', width: getWidth(0.05), align: 'center'
            },
            {
                title: '真实姓名', field: 'TrueName', width: getWidth(0.1), align: 'center',
                editor: {
                    type: 'validatebox',
                    options: {
                        valueField: 'TrueName',
                        required: true
                    }
                }
            },
            { title: '联系电话', field: 'UserTel', width: getWidth(0.1), align: 'center', editor: 'text' },
            { title: '邮箱地址', field: 'UserEmail', width: getWidth(0.15), align: 'center', editor: 'text' },
            { title: '单位名称', field: 'UserUnit', width: getWidth(0.1), align: 'center', editor: 'text' },
            {
                title: '注册时间', field: 'CreateTime', width: getWidth(0.1), align: 'center',
                formatter: function (value, row, index) {
                    return Common.TimeFormatter(row.CreateTime, row, index);
                }
            },
            {
                title: '操作', field: 'xxx', width: getWidth(0.15), align: 'center',
                formatter: function (value, row, index) {
                    return "<a href='javascript:;' onclick='resetUserPass()'>密码重置</a> <a href='javascript:;' onclick='resetUserPass()'>权限设置</a>";
                }
            }
        ]],
        pagination: true,
        pageSize: 10,
        pageNumber: parseInt($.query.get("page")) || 1,
        pageList: [10, 15, 20],//可以设置每页记录条数的列表  
        rownumbers: false,
        toolbar: [
            {
                id: 'btnadd',
                text: '新增',
                iconCls: 'icon-add',
                handler: function () {
                    window.location.href = 'ManagerAdd.aspx';
                }
            }, '-',
            {
                id: 'btndelete',
                text: '删除',
                iconCls: 'icon-cancel',
                handler: function () {
                    return removeit();
                }
            }, '-',
            {
                id: 'btnsave',
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    return accept();
                }
            }, '-',
            {
                id: 'btnundo',
                text: '撤销',
                iconCls: 'icon-undo',
                handler: function () {
                    return reject();
                }
            }, '-',
            {
                id: 'btnquery',
                text: '查询',
                iconCls: 'icon-search',
                handler: function () {
                    $('#btnquery').linkbutton('enable');
                    alert('查询');

                }
            }
        ]
    });
    var p = $('#tb');
    var opts = p.datagrid('options');
    var pager = p.datagrid('getPager');

    pager.pagination({
        //pageNumber:2,
        beforePageText: '第',//页数文本框前显示的汉字
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
        // onSelectPage:function(pageNum, pageSize){
        //     opts.pageNumber = pageNum;
        //     opts.pageSize = pageSize;
        //     // pager.pagination('refresh',{
        //     //     pageNumber:pageNum,
        //     //     pageSize:pageSize
        //     // });
        //     alert("pageNum:" + pageNum + ",pageSize:" + pageSize);
        // }
        /*onBeforeRefresh:function(){
            $(this).pagination('loading');
            alert('before refresh');
            $(this).pagination('loaded');
        }*/
    });
</script>
</html>
