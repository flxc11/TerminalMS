<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptEdit.aspx.cs" Inherits="WebSite.Accept.AcceptEdit" %>

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
				当前位置：受理单管理 > 添加受理单信息
			</div>
			<div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="control-group">
                        <input type="hidden" name="acceptGuid" value="<%=acceptGuid %>" />
                        <input type="hidden" name="pubNum" id="pubNum" value="" />
                        <table border="0" cellpadding="1" cellspacing="1" bgcolor="#999999" style="margin-top:20px;">
                            <tr>
                            <td height="60" colspan="11" align="center" bgcolor="#FFFFFF" style="font-size:20px;">温报传媒业务受理单</td>
                            </tr>
                            <tr>
  	                        <td height="30" bgcolor="#FFFFFF"></td>
                            <td align="center" bgcolor="#FFFFFF">日期</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:TextBox ID="PostTime" runat="server" CssClass="easyui-datebox accept-input-item accept-input2"  data-options="required:true,missingMessage:'请选择时间',editable:false"></asp:TextBox></td>
                            <td width="84" align="center" bgcolor="#FFFFFF">合同编号</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="ContractNo" runat="server" CssClass="easyui-validatebox accept-input-item accept-input1"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">编号</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="Num" runat="server" CssClass="easyui-validatebox accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td width="21" rowspan="2" align="center" bgcolor="#FFFFFF">业务客户</td>
                            <td width="79" height="38" align="center" bgcolor="#FFFFFF">客户名称</td>
                            <td colspan="6" bgcolor="#FFFFFF">　<asp:TextBox ID="ClientName" runat="server" CssClass="easyui-validatebox accept-input-item accept-input4" data-options="required:true,missingMessage:'请输入客户名称'"></asp:TextBox></td>
                            <td width="135" align="center" bgcolor="#FFFFFF">客户方经办人</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="Operator" runat="server" CssClass="accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td align="center" bgcolor="#FFFFFF">代理公司</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:TextBox ID="AgencyCompany" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td width="84" align="center" bgcolor="#FFFFFF">电话</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="Tel" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">手机</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="Mobile" runat="server" CssClass="accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">业务内容</td>
                            <td colspan="9" bgcolor="#FFFFFF">　<asp:TextBox ID="ADTitle" runat="server" CssClass="easyui-validatebox accept-input-item accept-input7"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">发布形式</td>
                            <td colspan="9" bgcolor="#FFFFFF">
                                <%=publishType %>
                            </td>
                            </tr>
                            <tr>
                            <td height="40" colspan="2" align="center" bgcolor="#FFFFFF">上屏时间</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="StartTime" runat="server" CssClass="easyui-datebox accept-input-item accept-input1"  data-options="required:true,missingMessage:'请选择时间',editable:false"></asp:TextBox></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">下屏时间</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">　<asp:TextBox ID="EndTime" runat="server" CssClass="easyui-datebox accept-input-item accept-input1"  data-options="required:true,missingMessage:'请选择时间',editable:false"></asp:TextBox></td>
                            <td colspan="3" bgcolor="#FFFFFF">　</td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">视频秒数</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="VideoTime" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">整屏幅数</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">　<asp:TextBox ID="PicNum" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">投放时间合计</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="CountTime" runat="server" CssClass="accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">投放地域</td>
                            <td colspan="9" bgcolor="#FFFFFF">
                                <asp:Repeater ID="rptClass" runat="server" OnItemDataBound="rptClass_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="ClassTitle"><span class="span_open"></span><%#Eval("ClassName") %> （共 <%#Eval("rcount") %> 台，已选择 <em class="checkCount"></em> 台）</div>
                                        <div class="divTerminal">
                                            <div style="font-size: 14px;color: red;">
                                                <input type="checkbox" id="check_all1<%#Eval("ID") %>" name="checkall" />
                                                <label for="check_all1<%#Eval("ID") %>">全选/取消全选</label>
                                                <input type="checkbox" id="check_all2<%#Eval("ID") %>" name="checkinvert" />
                                                <label for="check_all2<%#Eval("ID") %>">反选</label>
                                            </div>
                                        <asp:Repeater ID="rptTerminal" runat="server">
                                            <ItemTemplate>
                                                <a href='javascript:;'><input type="checkbox" name="checkTermi" value="<%#Eval("ID") %>" <%#IsChecked(Eval("ID").ToString(), adAreaId) %> /> <%#Eval("Location") %></a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            </tr>
                            <tr>
                            <td width="21" rowspan="3" align="center" bgcolor="#FFFFFF">费用合计</td>
                            <td height="30" align="center" bgcolor="#FFFFFF">合计价</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="TotalPrice" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td width="21" rowspan="3" align="center" bgcolor="#FFFFFF">结算方式</td>
                            <td align="center" bgcolor="#FFFFFF">现金</td>
                            <td width="82" align="center" bgcolor="#FFFFFF"><asp:TextBox ID="Cash" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            <td width="61" align="center" bgcolor="#FFFFFF">发票号码</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:TextBox ID="InvoiceNum" runat="server" CssClass="accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">折扣</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="Discount" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">转账</td>
                            <td bgcolor="#FFFFFF" align="center"><asp:TextBox ID="Transfer" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">支票号码</td>
                            <td colspan="3" bgcolor="#FFFFFF">　<asp:TextBox ID="ChequeNum" runat="server" CssClass="accept-input-item accept-input3"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">实收价</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="RealPrice" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">支付时间</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="PayTime" runat="server" CssClass="easyui-datebox accept-input-item accept-input1"  data-options="required:false,missingMessage:'请选择时间',editable:false"></asp:TextBox></td>
                            <td align="center" bgcolor="#FFFFFF">类别</td>
                            <td colspan="2" bgcolor="#FFFFFF">　<asp:TextBox ID="PayClass" runat="server" CssClass="accept-input-item accept-input1"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td height="30" colspan="2" align="center" bgcolor="#FFFFFF">业务承接人</td>
                            <td width="84" align="center" bgcolor="#FFFFFF"><asp:TextBox ID="SalesMan" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            <td width="66" align="center" bgcolor="#FFFFFF">审查人</td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Verifier" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            <td colspan="2" align="center" bgcolor="#FFFFFF">部门主任审批</td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Director" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            <td width="135" align="center" bgcolor="#FFFFFF">报社领导审批</td>
                            <td width="163" align="center" bgcolor="#FFFFFF">
                                <asp:TextBox ID="Lead" runat="server" CssClass="accept-input-item accept-input5"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td width="21" align="center" bgcolor="#FFFFFF">备注</td>
                            <td colspan="10" bgcolor="#FFFFFF" style="padding-top:10px;">　<asp:TextBox ID="Remark" runat="server" TextMode="MultiLine" Width="600" Height="50"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                    <div class="control-group">
                        <a href="javascript:void(0)" class="btn-submit" onclick="submitForm()" style="margin-left:120px;">提交修改</a>
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
        var backurl = "AcceptList.aspx?page=" + _page + "&StartTime=" + _StartTime + "&EndTime=" + _EndTime + "&SelectType=" + _SelectType + "&Keyword=" + _Keyword;
        $("#btn-submit").attr('href', backurl);
        $("input[type='checkbox'][name='CheckBoxList2']").on("click", function () {
            if ($(this).is(":checked")) {
                $(this).parent().next().find("input").removeAttr("readOnly");
                $(this).parent().next().find("input").attr("class", "accept-input6");
                $(this).parent().next().find("input").val(1);
            } else {

                $(this).parent().next().find("input").val('');
                $(this).parent().next().find("input").attr("readOnly", "readOnly");
                $(this).parent().next().find("input").attr("class", "readonly");
            };
        })
        $(".checkCount").each(function (i, v) {
            var _count = $(this).parent().next().find("input[name='checkTermi']:checked").length;
            $(this).html(_count);
        })
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
    $(".divTerminal a").on("click",
    function (event) {
        $(this).find(":checkbox").click(function (event) { event.stopPropagation(); }).trigger("click");
    });
    function submitForm(action) {
        if ($("#ff").form("validate")) {
            function doSubmit() {
                var _num = "";
                $("input[type='checkbox'][name='CheckBoxList2']:checked").each(function (i, v) {
                    _num += $(this).parent().next().find("input").val() + ",";
                });
                $("#pubNum").val(_num);
                //alert($("#pubNum").val());
                $("#ff").attr("action", "AcceptEdit.aspx?Action=Edit");
                $("#ff").submit();
            }
            setTimeout(doSubmit, 0);
        } else {
            $.messager.alert("信息", "信息填写不规范");
        }
    }
</script>
</html>
