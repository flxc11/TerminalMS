﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="WebSite.user.home" %>

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
        <div class="terminal-title" style="margin-top: 30px;">汇总：</div>
        <div class="out-btn-info">
            <%=str%>
        </div>
        <div class="terminal-title">按县市区：</div>
        <div class="out-btn-info">
            <asp:Repeater ID="rptArea" runat="server">
                <ItemTemplate>
                    <a href="terminallist.aspx?SelectType=Area&Keyword=<%#Eval("Area") %>"><div class="btn-info"><%#Eval("Area") %> (<%#Eval("acount") %>)</div></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div class="terminal-title">按设备厂商：</div>
        <div class="out-btn-info">
            <asp:Repeater ID="rptFactory" runat="server">
                <ItemTemplate>
                    <a href="terminallist.aspx?SelectType=Manufacturer&Keyword=<%#Eval("ManuFacturer") %>"><div class="btn-info"><%#Eval("ManuFacturer") %> (<%#Eval("acount") %>)</div></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
    </div>
    </form>
</body>
</html>
