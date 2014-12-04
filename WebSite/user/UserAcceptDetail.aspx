<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAcceptDetail.aspx.cs" Inherits="WebSite.user.UserAcceptDetail" %>

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
    <script src="/js/jQuery.query.js"></script>
    <script src="/js/cnvp.js"></script>
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
				当前位置：受理单管理 > 添加受理单信息
			</div>
			<div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="control-group">
                        <input type="hidden" name="pubNum" id="pubNum" value="" />
                        <table border="0" cellpadding="1" cellspacing="1" bgcolor="#999999" style="margin-top:20px;" width="965">
                            <tr>
                            <td height="60" colspan="11" align="center" bgcolor="#FFFFFF" style="font-size:20px;">温报传媒业务受理单</td>
                            </tr>
                            <tr>
  	                        <td height="30" bgcolor="#FFFFFF"></td>
                            <td align="center" bgcolor="#FFFFFF">日期</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:Label ID="PostTime" runat="server" Text="Label"></asp:Label></td>
                            <td width="84" align="center" bgcolor="#FFFFFF">合同编号</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="ContractNo" runat="server" Text=""></asp:Label></td>
                            <td align="center" bgcolor="#FFFFFF">编号</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="Num" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td width="21" rowspan="2" align="center" bgcolor="#FFFFFF">业务客户</td>
                            <td width="79" height="38" align="center" bgcolor="#FFFFFF">客户名称</td>
                            <td colspan="6" bgcolor="#FFFFFF">　<asp:Label ID="ClientName" runat="server" Text=""></asp:Label></td>
                            <td width="135" align="center" bgcolor="#FFFFFF">客户方经办人</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="Operator" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td align="center" bgcolor="#FFFFFF">代理公司</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:Label ID="AgencyCompany" runat="server" Text=""></asp:Label></td>
                            <td width="84" align="center" bgcolor="#FFFFFF">电话</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="Tel" runat="server" Text=""></asp:Label></td>
                            <td align="center" bgcolor="#FFFFFF">手机</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="Mobile" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">业务内容</td>
                            <td colspan="9" bgcolor="#FFFFFF">　<asp:Label ID="ADTitle" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">发布形式</td>
                            <td colspan="9" bgcolor="#FFFFFF">
                                <%=publishType %>
                            </td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">上屏时间</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="StartTime" runat="server" Text=""></asp:Label></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">下屏时间</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">　<asp:Label ID="EndTime" runat="server" Text=""></asp:Label></td>
                            <td colspan="3" bgcolor="#FFFFFF">　</td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">视频秒数</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="VideoTime" runat="server" Text=""></asp:Label></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">整屏幅数</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">　<asp:Label ID="PicNum" runat="server" Text=""></asp:Label></td>
                            <td align="center" bgcolor="#FFFFFF">投放时间合计</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:Label ID="CountTime" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">投放地域<br/>(已选 <span style="font-size:14px; color:red;"><%=ChooseCount %></span> 台)</td>
                            <td colspan="9" bgcolor="#FFFFFF">
                                <asp:Repeater ID="rptClass" runat="server" OnItemDataBound="rptClass_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="ClassTitle"><span class="span_open"></span><%#Eval("ClassName") %> （共 <%#Eval("rcount") %> 台，已选 <em class="checkCount"></em> 台）</div>
                                        <div class="divTerminal">
                                            <asp:Repeater ID="rptTerminal" runat="server">
                                                <ItemTemplate>
                                                    <a href='javascript:;' <%#IsChecked(Eval("ID").ToString(), adAreaId) %>><%#Eval("Location") %></a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ItemTemplate>                                    
                                </asp:Repeater>
                            </td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">业务承接人</td>
                            <td width="84" align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="SalesMan" runat="server" Text=""></asp:Label></td>
                            <td width="66" align="center" bgcolor="#FFFFFF">审查人</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="Verifier" runat="server" Text=""></asp:Label></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">部门主任审批</td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="Director" runat="server" Text=""></asp:Label></td>
                            <td width="135" align="center" bgcolor="#FFFFFF">报社领导审批</td>
                            <td width="163" align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="Lead" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                            <td width="21" align="center" bgcolor="#FFFFFF">备注</td>
                            <td colspan="10" bgcolor="#FFFFFF" style="padding-top:10px;">　<asp:Label ID="Remark" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div class="control-group">
                        <a href="javascript:void(0)" class="btn-submit" id="btn-submit">返　　回</a>
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
        var backurl = "UserAcceptList.aspx?page=" + _page + "&StartTime=" + _StartTime + "&EndTime=" + _EndTime + "&SelectType=" + _SelectType + "&Keyword=" + _Keyword;
        $("#btn-submit").attr('href', backurl);
        $(".checkCount").each(function(i, v) {
            var _count = $(this).parent().next().find("a.div-db").length;
            $(this).html(_count);
        });
    })
    $(".ClassTitle").on("click", function () {
        var divTermi = $(this).next("div");
        if (divTermi.is(":visible")) {
            divTermi.hide();
            $(this).find("span").attr("class", "span_open");
        } else {
            divTermi.slideDown();
            $(this).find("span").attr("class", "span_close");
        }
    });
    function CheckCount() {
        
    }
</script>
</html>
