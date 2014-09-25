<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminaldetail.aspx.cs" Inherits="WebSite.user.terminaldetail" %>

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
    <form id="ff" name="ff" action="?Action=Edit" runat="server" enctype="multipart/form-data">
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
                当前位置：<a href="#">终端管理</a> > 查看终端信息
            </div>
            <div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="table-1">
                        <input type="hidden" name="terPage" value="<%=terPage %>" />
                        <div class="control-group">
                            <label for="in-out" class="control-label">设备厂商：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Manufacturer" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">尺寸：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="MachineSize" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">屏幕：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Screen" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">室内外：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="OutIn" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">区域：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Area" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">放置地点：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Location" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">签收：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="SignIn" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">开机时长：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="OpenTime" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">时间：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="PostTime" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">编号：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Numb" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">系统：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="System" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">使用状况：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Stituation" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">详细地址：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Address" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">联系人和电话：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="ContentTel" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                            <label for="in-out" class="control-label">赞助商：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Sponsor" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">转移记录：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Recores" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">备注：</label>
                            <div class="controls controls-inline">
                                <asp:Label ID="Remark" runat="server" Text="&nbsp;"></asp:Label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label1"><span>图片列表</span></label>
                        </div>
                        <asp:Repeater ID="scwOthers" runat="server">
                            <ItemTemplate>
                                <div class="control-group">
                                    <div class="clear1"><img src="<%#Eval("SourceUrl") %>" width="80" height="60"  /><a href="<%#Eval("SourceUrl") %>" target="_blank">点击查看</a></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="control-group">
                        <a href="terminallist.aspx?page=<%=terPage %>&StatrTime=<%=startTime %>&EndTime=<%=endTime %>&SelectType=<%=selectType %>&Keyword=<%=keyWord %>" class="btn-submit">返　　回</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
