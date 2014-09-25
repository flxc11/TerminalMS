<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cliendadd.aspx.cs" Inherits="WebSite.client.cliendadd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
        <div class="mainScroll">
			<div class="position">
				当前位置：客户管理 > 添加客户信息
			</div>
			<div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="table-1">
                        <div class="control-group">
                            <label for="in-out" class="control-label">客户名称：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="ClientName" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                            <label for="in-out" class="control-label">客户方经办人：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Operator" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">代理公司：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="AgencyCompany" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                            <label for="in-out" class="control-label">电话：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Tel" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                            <label for="in-out" class="control-label">手机：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Mobile" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">备注：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Remark" runat="server" CssClass="app-input" Height="100px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="control-group">
                        <a href="javascript:void(0)" class="btn-submit" onclick="submitForm()">保存</a>
                        <a href="javascript:void(0)" class="btn-submit" onclick="javascript:history.go(-1)">返　　回</a>
                    </div>
                </div>
			</div>
		</div>
    </div>
    </form>
</body>
</html>
