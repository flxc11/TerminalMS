<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminallist.aspx.cs" Inherits="WebSite.admin.terminallist" %>

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
        //return document.body.clientWidth * percent ;
        return $(".main-r").width() * percent;
    }
    //var fields = "Id,Guid,IO,ShipName,Saillings,Operator,StartPort,ArrivedTime,WorkBerth,AppState";
    var fields = "";
    var explains = "";

    //获取个人字段配置信息
    $.ajax({
        type: "POST",
        url: "/user/userjson.aspx?Time=" + new Date().getTime(),
        data: {
            action: "GetFields",
            UserId: "<%=LoginInfo.LoginID%>",
            UserType: "admin"
        },
        dataType: "json",
        cache: false,
        success: function (msg) {
            if (msg.result == "1") {
                fields = "Guid," + msg.relfields;
                explains = "xx," + msg.relexplain;
            } else {
                fields = "Guid,Manufacturer,MachineSize,Screen,Area,Location,SignIn,Numb,System,Stituation";
                explains = "xx,设备厂商,尺寸,屏幕,区域,放置地点,签收,编号,系统,使用状况";
            }
            var flag = true;
            arrfields = new Array();
            arrexplains = new Array();
            arrfields = fields.split(",");
            arrexplains = explains.split(",");
            var columnArray = [];
            columnArray.push({ field: 'ck', checkbox: true, width: getWidth(0.05) });
            columnArray.push(
                    {
                        title: '序号', field: 'ooo', width: getWidth(0.05), align: 'center',
                        formatter: function (value, row, index) {
                            return index + 1;
                        }
                    });
            for (var i = 1; i < arrfields.length; i++) {
                $("#sea_select").append("<option value='" + arrfields[i] + "'>" + arrexplains[i] + "</option>");
                if (arrfields[i] == "OutIn") {
                    columnArray.push(
                        { title: arrexplains[i], field: arrfields[i], width: getWidth(0.05), align: 'center',
                            formatter: function (value, row, index) {
                                if (row.OutIn == "0") {
                                    return "室内";
                                } else {
                                    return "室外";
                                };
                            }
                        }
                    );
                } else if(arrfields[i] == "SignIn") {
                    columnArray.push(
                        { title: arrexplains[i], field: arrfields[i], width: getWidth(0.05), align: 'center',
                            formatter: function (value, row, index) {
                                if (row.SignIn == "0") {
                                    return "<span style='color:red'>未签收</span>";
                                } else if (row.SignIn == "1") {
                                    return "已签收";
                                };
                            }
                        }
                    );
                } else if(arrfields[i] == "PostTime") {
                    columnArray.push(
                        { title: arrexplains[i], field: arrfields[i], width: getWidth(0.05), align: 'center',
                            formatter: function (value, row, index) {
                                return Common.TimeFormatter(row.PostTime,row,index);
                            }
                        }
                    );
                } else {
                    columnArray.push(
                        {
                            title: arrexplains[i], field: arrfields[i], width: getWidth(0.05), align: 'center',
                            formatter: function (value, row, index) {
                                return "<a href='terminaledit.aspx?guid=" + row.Guid + "&page=" + $('#tb').datagrid('getPager').data("pagination").options.pageNumber + "'>" + value + "</a>";
                            }
                        }
                    );
                };
            };
            if ($.query.get("StartTime") != true && $.query.get("StartTime") != "true") {
                $("#sea_start").datebox('setValue', $.query.get("StartTime"));
            };
            if ($.query.get("EndTime") != true && $.query.get("EndTime") != "true") {
                $("#sea_end").datebox('setValue', $.query.get("EndTime"));
            };
            if ($.query.get("SelectType") != true && $.query.get("SelectType") != "true") {
                $("#sea_select").val($.query.get("SelectType"));
            };
            if ($.query.get("Keyword") != true && $.query.get("Keyword") != "true") {
                $("#sea_keyword").val($.query.get("Keyword"));
            };
            columnArray.push(
                    {
                        title: '操作', field: 'xxx', width: getWidth(0.05), align: 'center',
                        formatter: function (value, row, index) {
                            return "<a href='terminaledit.aspx?guid=" + row.Guid +
                            "&page=" + $('#tb').datagrid('getPager').data("pagination").options.pageNumber +
                            "&StartTime=" + $("input[name='sea_start']").val() +
                            "&EndTime=" + $("input[name='sea_end']").val() +
                            "&SelectType=" + $("select[name='sea_select']").val() +
                            "&Keyword=" + $("#sea_keyword").val() +
                             "'>编辑</a>";
                        }
                });
            $('#tb').datagrid({
                title: '当前位置：终端管理 > 全部终端列表',
                width: 'auto',
                height: 'auto',
                nowrap: false,
                striped: true,  //是否显示斑马线效果
                url: '/user/userjson.aspx',
                queryParams: {
                    action: 'GetTerminalList',
                    easyGrid_Sort: fields,
                    // StartTime: $.query.get("StartTime") == true && "true" ? "" : $.query.get("StartTime"),
                    // EndTime: $.query.get("EndTime") == true && "true"  ? "" : $.query.get("EndTime"),
                    // SelectType: $.query.get("SelectType") == true && "true"  ? "" : $.query.get("SelectType"),
                    // Keyword: $.query.get("Keyword") == true && "true"  ? "" : $.query.get("Keyword"),
                    StartTime: String($.query.get("StartTime")) == "true" ? "" : $.query.get("StartTime"),
                    EndTime: String($.query.get("EndTime")) == "true"  ? "" : $.query.get("EndTime"),
                    SelectType: String($.query.get("SelectType")) == "true"  ? "" : $.query.get("SelectType"),
                    Keyword: String($.query.get("Keyword")) == "true"  ? "" : $.query.get("Keyword"),
                    Time: new Date().getTime()
                },
                idField: 'Id',
                fix: false,
                frozenColumns: [[

                ]], 
                columns: [columnArray],
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
                                ids.push(rows[i].Guid);
                            }
                            idsstr = ids.join(',');
                            if (cand) {
                                $.messager.confirm('删除窗口', '确定要删除吗?', function (r) {
                                    if (r) {
                                        //--s-执行删除操作
                                        $.ajax({
                                            type: "POST",
                                            url: "/user/userjson.aspx?Time=" + new Date().getTime(),
                                            data: {
                                                action: "Delete",
                                                Guid: idsstr
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
                }, '-',
                {
                    id: 'btnexport',
                    text: '导出',
                    iconCls: 'icon-large-smartart',
                    handler: function () {
                        window.open("Export.aspx?action=Export&StartTime=" + $("input[name='sea_start']").val() + "&EndTime=" + $("input[name='sea_end']").val() + "&SelectType=" + $("select[name='sea_select']").val() + "&Keyword=" + $("#sea_keyword").val());
                    }
                }
                ],
                onLoadSuccess: function () {
                    //alert($.query.get("Keyword"));
                    var grid = $(".datagrid-toolbar"); //datagrid
                    var date = $("#search-div");
                    grid.append(date);
                    // if ($.query.get("StartTime") != true && $.query.get("StartTime") != "true") {
                    //     $("#sea_start").datebox('setValue', $.query.get("StartTime"));
                    // };
                    // if ($.query.get("EndTime") != true && $.query.get("EndTime") != "true") {
                    //     $("#sea_end").datebox('setValue', $.query.get("EndTime"));
                    // };
                    // if ($.query.get("SelectType") != true && $.query.get("SelectType") != "true") {
                    //     $("#sea_select").val($.query.get("SelectType"));
                    // };
                    // if ($.query.get("Keyword") != true && $.query.get("Keyword") != "true") {
                    //     $("#sea_keyword").val($.query.get("Keyword"));
                    // };
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
        }
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
