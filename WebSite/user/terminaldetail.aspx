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
    <style>
    	#Terminal-info {
			margin-left: 20px;
		}
		#Terminal-info tr:hover {
			background: #dddddd;
		}
		#Terminal-info tr td {
			font-size: 14px;
		}
		.tdpl10 {
			padding-left: 10px;
		}
    </style>
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
                        <table width="80%" border="0" cellpadding="1" cellspacing="1" bgcolor="#dbdbdb" id="Terminal-info">
                          <tbody>
                            <tr>
                              <td width="10%" height="50" align="center" bgcolor="#FFFFFF">放置地点：</td>
                              <td width="20%" height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Location" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td width="10%" height="50" align="center" bgcolor="#FFFFFF">详细地址：</td>
                              <td width="20%" height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Address" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td width="10%" height="50" align="center" bgcolor="#FFFFFF">安装时间：</td>
                              <td width="30%" height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="PostTime" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">联系人和电话：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="ContentTel" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">区　　域：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Area" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">设备厂商：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Manufacturer" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">签　　收：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="SignIn" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">开机时长：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="OpenTime" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">屏　　幕：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Screen" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">编　　号：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Numb" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">系　　统：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="System" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">使用状况：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Stituation" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">尺　　寸：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="MachineSize" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">室内室外：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="OutIn" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">赞助商：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Sponsor" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">所属类别：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="ClassID" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">安装状态：</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10">
                                  <asp:Label ID="Status" runat="server" Text="&nbsp;"></asp:Label></td>
                              <td height="50" align="center" bgcolor="#FFFFFF">&nbsp;</td>
                              <td height="50" bgcolor="#FFFFFF" class="tdpl10">&nbsp;</td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">转移记录：</td>
                              <td height="50" colspan="5" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Recores" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                            <tr>
                              <td height="50" align="center" bgcolor="#FFFFFF">备　　注：</td>
                              <td height="50" colspan="5" bgcolor="#FFFFFF" class="tdpl10"><asp:Label ID="Remark" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                          </tbody>
                        </table>
                        <div class="control-group" style="font-size: 16px; line-height:50px;padding-left: 20px;">图片列表</div>
                        <div style="overflow:hidden">
                        	<asp:Repeater ID="scwOthers" runat="server">
                                <ItemTemplate>
                                    <div class="control-group" style="width:200px; margin:0 15px; float:left;">
                                        <div><a href="<%#Eval("SourceUrl") %>" target="_blank"><img src="<%#Eval("SourceUrl") %>" width="200" /><span style="display:block; text-align:center;width:200px;">点击查看</span></a></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
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
