<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="WebSite.user.map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=HD.Config.UIConfig.SoftName %></title>
    <link rel="stylesheet" href="/css/Global.css" />
    <link rel="stylesheet" href="/themes/default/easyui.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="/themes/icon.css" />
    <link rel="stylesheet" href="/css/SearchInfoWindow_min.css" />
    <script src="/js/jquery.js"></script>
    <script src="/js/cnvp.js"></script>
    <script src="/js/map.js"></script>
    <script src="/js/Marker.js"></script>
    <style>
        #allmap {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <div class="map-search">
            <div class="out-btn-info" id="Class">
                <div class="terminal-title1">分类：</div>
                <asp:Repeater ID="rptClass" runat="server">
                    <ItemTemplate>
                        <div class="map-info" data="<%#Eval("ClassID") %>"><%#Eval("ClassName") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="out-btn-info" id="AreaID">
                <div class="terminal-title1">县市区：</div>
                <asp:Repeater ID="rptArea" runat="server">
                    <ItemTemplate>
                        <div class="map-info" data="<%#Eval("Area") %>"><%#Eval("Area") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="out-btn-info" id="Manufacturer">
                <div class="terminal-title1">设备厂商：</div>
                <asp:Repeater ID="rptFactory" runat="server">
                    <ItemTemplate>
                        <div class="map-info" data="<%#Eval("ManuFacturer") %>"><%#Eval("ManuFacturer") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="out-btn-info" id="Manufacturer">
                <div class="terminal-title1">放置地点：</div>
                <div class="terminal-title1" style="padding: 0;"><input type="text" name="keyword" id="keyword" class="map-ipt-search"><input type="button" value="搜索" style="height: 24px;width: 40px;margin-left: 10px; cursor: pointer;" id="btn-keyword"></div>
            </div>
            <div class="map-up">收起</div>
        </div>
        <div class="allmap" id="allmap"></div>
    </div>
    </form>
</body>
</html>
<script>
    $(function(){
        flg = 1;
        $(".map-up").on('click', function(event) {
            event.preventDefault();
            /* Act on the event */
            if (flg) {
                $(this).parent().animate({
                    top: '-184px'},
                    500, function() {
                    /* stuff to do after animation is complete */
                    $(".map-up").html("打开");
                    flg = 0;
                });
            } else {
                $(this).parent().animate({
                    top: '0'},
                    1000, function() {
                    /* stuff to do after animation is complete */
                    $(".map-up").html("收起");
                    flg = 1;
                });
            };
        });
    })
    $(".map-info").on("click", function(){
        if ($(this).attr("class") == "map-info map-click") {
            $(this).removeClass("map-click");
            GetInfo();
        } else {
            $(this).addClass("map-click").siblings().removeClass("map-click");
            GetInfo();
        };
    })
    $("#btn-keyword").on('click', function(event) {
        event.preventDefault();
        /* Act on the event */
        GetInfo();
    });
    function GetInfo() {
        var ClassID = $("#Class div.map-click").attr("data");
        var AreaID = $("#AreaID .map-click").attr("data");
        var Manufacturer = $("#Manufacturer .map-click").attr("data");
        var Keyword = $("#keyword").val();
        ajaxMap(ClassID,AreaID,Manufacturer,Keyword);
    }
</script>