<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairDetail.aspx.cs" Inherits="WebSite.user.RepairDetail" %>

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
    <style type="text/css">
        .auto-style1 {
            height: 28px;
            width: 25%;
        }
        .auto-style2 {
            height: 28px;
            width: 8%;
        }
        .auto-style3 {
            height: 28px;
            width: 5%;
        }
    </style>
</head>
<body>
    <form id="ff" runat="server">
    <div class="wrap-header">
        <div class="header">
            <div class="lg-info">
                <h2>您好!欢迎使用本系统</h2>
                <a href="#">
                    <span class="manager"><%=UserLoginInfo.UserLoginName %></span>
                </a>
                <a href="login.aspx">
                    <span class="lg-out">退 出</span>
                </a>
            </div>
        </div>
    </div>
    <div class="main-l">
        <!--#include file="/control/userleft.html" -->
    </div>
    <div class="main-r">
        <div class="mainScroll">
			<div class="position">
				当前位置：报修单管理 > 查看报修详情
			</div>
			<div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="table-1">
                        <table style="width:90%; border-collapse: 0; border-spacing: 1px; background-color: #CCCCCC; margin: 20px auto;" aria-dropeffect="none">
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">所在位置：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="RepairTitle" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;"></td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"></td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">详细地址：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="Address" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;">联系人：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="ContentTel" runat="server" Text=" "></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">问题描述：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="RepairContent" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;"></td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"></td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">报修人：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="UserName" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;">报修时间：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="RepairTime" runat="server" Text=" "></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">维修反馈：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="ReplyContent" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;">维修时间：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="ReplyRepairTime" runat="server" Text=" "></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="background-color: #FFFFFF; padding-left: 10px;">维修人：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="ReplyName" runat="server" Text=" "></asp:Label></td>
                                <td class="auto-style3" style="background-color: #FFFFFF; padding-left: 10px;">报修单状态：</td>
                                <td class="auto-style1" style="background-color: #FFFFFF; padding-left: 10px;"><asp:Label ID="RepairStatus" runat="server" Text=" "></asp:Label></td>
                            </tr>
                        </table>
                        <div class="control-group">
                            <a href="javascript:void(0)" class="btn-submit" id="btn-submit" style="margin-left:120px;">返　　回</a>
                        </div>
                    </div>
                </div>
			</div>
		</div>
    </div>
    </form>
</body>
<script>
    $(function () {
        var _page = $.query.get("page");
        var _StartTime = $.query.get("StartTime");
        var _EndTime = $.query.get("EndTime");
        var _SelectType = $.query.get("SelectType");
        var _Keyword = $.query.get("Keyword");
        var backurl = "RepairList.aspx?page=" + _page + "&StartTime=" + _StartTime + "&EndTime=" + _EndTime + "&SelectType=" + _SelectType + "&Keyword=" + _Keyword;
        $("#btn-submit").attr('href', backurl);
    });

    function submitForm(action) {
        if ($("#ff").form("validate")) {
            if (/^s*$/.test($("input[name='TerminalID']").val()) || $("input[name='TerminalID']").val() == undefined) {
                alert("信息获取有误，请重新输入！");
                return false;
            } else {
                function doSubmit() {
                    $("#ff").attr("action", "RepairEdit.aspx?Action=Edit");
                    $("#ff").submit();
                }
                setTimeout(doSubmit, 0);
            }
        } else {
            $.messager.alert("信息", "信息填写不规范");
        }
    }
</script>
<script type="text/javascript">

    //实现搜索输入框的输入提示js类
    function oSearchSuggest(searchFuc) {
        var input = $('#RepairTitle');
        var suggestWrap = $('#gov_search_suggest');
        var key = "";
        var init = function () {
            input.on('keyup', sendKeyWord);
            input.on('focus', sendKeyWord);
            //input.focus(sendKeyWord);
            input.on('blur', function () { setTimeout(hideSuggest, 100); });
        }
        var hideSuggest = function () {
            suggestWrap.hide();
        }

        //发送请求，根据关键字到后台查询
        var sendKeyWord = function (event) {
            event = event || window.event;
            //键盘选择下拉项
            if (suggestWrap.css('display') == 'block' && event.keyCode == 38 || event.keyCode == 40) {
                var current = suggestWrap.find('li.hover');
                if (event.keyCode == 38) {
                    if (current.length > 0) {
                        var prevLi = current.removeClass('hover').prev();
                        if (prevLi.length > 0) {
                            prevLi.addClass('hover');
                            input.val(prevLi.html());
                        }
                    } else {
                        var last = suggestWrap.find('li:last');
                        last.addClass('hover');
                        input.val(last.html());
                    }

                } else if (event.keyCode == 40) {
                    if (current.length > 0) {
                        var nextLi = current.removeClass('hover').next();
                        if (nextLi.length > 0) {
                            nextLi.addClass('hover');
                            input.val(nextLi.html());
                        }
                    } else {
                        var first = suggestWrap.find('li:first');
                        first.addClass('hover');
                        input.val(first.html());
                    }
                }

                //输入字符
            } else {
                var valText = $.trim(input.val());
                if (valText == '') {
                    return;
                }
                searchFuc(valText);
                key = valText;
            }
            //为下拉选项绑定回车事件
            if (input.is(":focus") && event.keyCode == 13) {
                GetTerminal(input.val());
                suggestWrap.hide();
            }

        }
        //input.on("change", function () {
        //    GetTerminal(input.val());
        //});
        //请求返回后，执行数据展示
        this.dataDisplay = function (data) {
            if (data.length <= 0) {
                suggestWrap.hide();
                return;
            }

            //往搜索框下拉建议显示栏中添加条目并显示
            var li;
            var tmpFrag = document.createDocumentFragment();
            suggestWrap.find('ul').html('');
            for (var i = 0; i < data.length; i++) {
                li = document.createElement('LI');
                li.innerHTML = data[i];
                tmpFrag.appendChild(li);
            }
            suggestWrap.find('ul').append(tmpFrag);
            suggestWrap.show();

            //为下拉选项绑定鼠标事件
            suggestWrap.find('li')
                .hover(function () {
                    suggestWrap.find('li').removeClass('hover');
                    $(this).addClass('hover');
                }, function () {
                    $(this).removeClass('hover');
                }).on('click', function () {
                    input.val(this.innerHTML);
                    GetTerminal(this.innerHTML);
                    suggestWrap.hide();
                });
        }
        init();
    };

    //实例化输入提示的JS,参数为进行查询操作时要调用的函数名
    var searchSuggest = new oSearchSuggest(sendKeyWordToBack);

    //这是一个模似函数，实现向后台发送ajax查询请求，并返回一个查询结果数据，传递给前台的JS,再由前台JS来展示数据。本函数由程序员进行修改实现查询的请求
    //参数为一个字符串，是搜索输入框中当前的内容
    function sendKeyWordToBack(keyword) {
        var obj = {
            action: "SearchTitle",
            "keyword": keyword
        };
        $.ajax({
            type: "POST",
            url: "/Repair/RepairJson.aspx",
            async: false,
            data: obj,
            dataType: "html",
            success: function (data) {
                //var json = eval("("+data+")");
                var key = data.split(",");
                var aData = [];
                for (var i = 0; i < key.length; i++) {
                    //以下为根据输入返回搜索结果的模拟效果代码,实际数据由后台返回
                    if (key[i] != "") {
                        aData.push(key[i]);
                    }
                }
                //将返回的数据传递给实现搜索输入框的输入提示js类
                searchSuggest.dataDisplay(aData);
            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
            }
        });

        //以下为根据输入返回搜索结果的模拟效果代码,实际数据由后台返回
        //var aData = [];
        //aData.push(keyword + '返回数据1');
        //aData.push(keyword + '返回数据2');
        //aData.push(keyword + '不是有的人天生吃素的');
        //aData.push(keyword + '不是有的人天生吃素的');
        //aData.push(keyword + '2012是真的');
        //aData.push(keyword + '2012是假的');
        ////将返回的数据传递给实现搜索输入框的输入提示js类
        //searchSuggest.dataDisplay(aData);

    }
    function GetTerminal(key) {
        var divAddress = "";
        var obj = {
            action: "GetTerminal",
            "keyword": key
        };
        $.ajax({
            type: "POST",
            url: "/Repair/RepairJson.aspx",
            async: false,
            data: obj,
            dataType: "json",
            beforeSend: function () {
                $("body").append("<div class='over'></div>");
            },
            success: function (data) {
                if (data.return == "1") {
                    $("div.over").remove();
                    if ($("#addAddress")) {
                        $("#addAddress").remove();
                    }
                    divAddress =
                        '<div class="control-group" id="addAddress">' +
                        '<label class="control-label">详细地址：</label>' +
                        '<div class="controls controls-inline">' +
                        data.Address +
                        '</div>' +
                        '<label class="control-label">联系人：</label>' +
                        '<div class="controls controls-inline" id="dd">' +
                        data.ContentTel +
                        '<input type="hidden" name="TerminalID" value="' + data.TerminalID + '" /></div>' +
                        '</div>';
                    $("#newPosition").append(divAddress);
                } else {
                    alert('找不到对应的信息，请重新输入所在位置！');
                    $("div.over").remove();
                    $('#RepairTitle').focus();
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrow) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
            }
        });
    }
</script>
</html>
