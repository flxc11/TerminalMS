<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clientlist.aspx.cs" Inherits="WebSite.client.clientlist" %>

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
    <script src="/js/jquery.ba-hashchange.js"></script>
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
        <!--加载到datagrid的toolbar上 -->
        <div id="search-div">
			从: <input class="easyui-datebox" data-options="editable:false" name="sea_start" id="sea_start" style="width:100px" />
			到: <input class="easyui-datebox" data-options="editable:false" name="sea_end" id="sea_end" style="width:100px" />
			选项: 
			<select name="sea_select" id="sea_select" panelHeight="auto" style="width:100px">
				<option value="">全部</option>
				
			</select>
            <input type="text" name="sea_keyword" id="sea_keyword" class="pagination-num" style="width:100px;" />
			<a href="javascript:;"  onclick="dosearch();" id="search_a">搜索</a>
		</div>
        <div id="tb"></div>
    </div>
    </form>
</body>
<script>
    function getWidth(percent) {
        return $(".main-r").width() * percent;
    }
    var fields = "ClientId,ClientPGuid,ClientName,Tel,Operator,AgencyCompany,Mobile,ClientRemark,ClientPostTime";

    $('#tb').datagrid({
        title: '当前位置：客户管理 > 全部客户列表',
        width: 'auto',
        height: 'auto',
        nowrap: false,
        striped: true,  //是否显示斑马线效果
        url: 'clientjson.aspx',
        queryParams: {
            action: 'GetClientList',
            easyGrid_Sort: fields,
            Time: new Date().getTime()
        },
        idField: 'Id',
        fix: false,
        frozenColumns: [[

        ]],
        columns: [[
            { title: '序号', field: 'ooo',checkbox:true, width: getWidth(0.05), align: 'center',
                formatter: function (value, row, index) {
                    return index + 1;
                }
            },
            { title: '客户名称', field: 'ClientName', width: getWidth(0.1), align: 'center' },
            { title: '客户方经办人', field: 'Operator', width: getWidth(0.1), align: 'center' },
            { title: '代理公司', field: 'AgencyCompany', width: getWidth(0.1), align: 'center' },
            { title: '电话', field: 'Tel', width: getWidth(0.1), align: 'center' },
            { title: '手机', field: 'Mobile', width: getWidth(0.1), align: 'center' },
            { title: '备注', field: 'ClientRemark', width: getWidth(0.1), align: 'center' },
            {
                title: '添加时间', field: 'PostTime', width: getWidth(0.1), align: 'center',
                formatter: function (value, row, index) {
                    return Common.TimeFormatter(row.ClientPostTime, row, index);
                }
            },
            {
                title: '操作', field: 'xxx', width: getWidth(0.05), align: 'center',
                formatter: function (value, row, index) {
                    return "<a href='clientedit.aspx?ClientGuid=" + row.ClientPGuid + "'>编辑</a>";
                }
            }
        ]],
        pagination: true,
        pageSize: 20,
        pageNumber: parseInt($.query.get("page")) || 1,
        pageList: [10, 20, 30, 50],//可以设置每页记录条数的列表
        rownumbers: false,
        toolbar: [{
            id: 'btndelete',
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#btndelete').linkbutton('enable');
                //---
                var idsstr = "";
                var ids = new Array();
                var rows = $('#tb').datagrid('getSelections');
                if (rows.length < 1) {
                    $.messager.alert('信息窗口', '请选择要删除的数据!', 'info');
                } else {
                    var cand = true;
                    for (var i = 0; i < rows.length; i++) {
                        ids.push(rows[i].ClientGuid);
                    }
                    idsstr = ids.join(',');
                    if (cand) {
                        $.messager.confirm('删除窗口', '确定要删除吗?', function (r) {
                            if (r) {
                                //--s-执行删除操作
                                $.ajax({
                                    type: "POST",
                                    url: "/client/clientjson.aspx?Time=" + new Date().getTime(),
                                    data: {
                                        action: "Delete",
                                        ClientGuid: idsstr
                                    },
                                    dataType: "json",
                                    cache: false,
                                    success: function (msg) {
                                        if (msg.result == "1") {
                                            $.messager.alert('信息窗口', '删除成功!', 'info', function () {
                                                //重新加载当前页
                                                $('#tb').datagrid('reload');
                                            });
                                        } else {
                                            $.messager.alert('信息窗口', '删除失败!', 'info');
                                        }
                                    }
                                });
                                //--e-执行删除操作
                            }
                        })
                    } else {
                        $.messager.alert('信息窗口', '你选择的数据中有ID为0的数据，此数据为系统数据，不能删除!', 'info');
                    }
                }
                //---
            }
        }
        ],
        onLoadSuccess: function () {
            var grid = $(".datagrid-toolbar"); //datagrid
            var date = $("#search-div");
            grid.append(date);
        }
    });
    var p = $('#tb');
    var opts = p.datagrid('options');
    var pager = p.datagrid('getPager');

    pager.pagination({
        //pageNumber:2,
        beforePageText: '第',//页数文本框前显示的汉字
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
    });

    function dosearch() {
        //alert(1);
        window.location.hash = "StartTime=" + $("input[name='sea_start']").val() + "&EndTime=" + $("input[name='sea_end']").val() + "&SelectType=" + $("select[name='sea_select']").val() + "&Keyword=" + $("#sea_keyword").val();
        if (!/^\s*$/.test($("input[name='sea_start']").val())) {
            if (/^\s*$/.test($("input[name='sea_end']").val())) {
                alert("请输入结束时间");
                $("#sea_end").focus();
                return false;
            } else {
                //var _host = window.location.host;
                //history.pushState('','',_host);
                $("#tb").datagrid("load", {
                    Action: "TerminalSearch",
                    StartTime: $("input[name='sea_start']").val(),
                    EndTime: $("input[name='sea_end']").val(),
                    SelectType: $("select[name='sea_select']").val(),
                    Keyword: $("#sea_keyword").val(),
                    easyGrid_Sort: fields,
                    Time: new Date().getTime()
                });
            }
        } else {
            $("#tb").datagrid("load", {
                Action: "TerminalSearch",
                StartTime: $("input[name='sea_start']").val(),
                EndTime: $("input[name='sea_end']").val(),
                SelectType: $("select[name='sea_select']").val(),
                Keyword: $("#sea_keyword").val(),
                easyGrid_Sort: fields,
                Time: new Date().getTime()
            });
        }
    }
</script>
</html>
