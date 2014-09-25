<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fields.aspx.cs" Inherits="WebSite.user.fields" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=HD.Config.UIConfig.SoftName %></title>
    <link rel="stylesheet" href="/css/Global.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/js/jquery.js"></script>
    <script src="/js/cnvp.js"></script>
</head>
<body>
    <form id="ff" name="ff" runat="server" onsubmit="return checkform();">
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
				当前位置：个人中心 > 字段管理
			</div>
            <div style="padding:50px 0 0 100px;">
                <div class="control-group" style="font-size:16px; color:red;font-weight:bold;">根据需要选择合适的字段内容显示在终端管理列表中</div>
			    <div class="control-group" style="font-size:14px;">
                    <input type="checkbox" name="CheckBoxAll" class="single-fields" onclick="checkAll()" />全选
			    </div>
                <div class="control-group">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="single-fields-div">
                                <input type="checkbox" name="single-fields" class="single-fields" value="<%#Eval("field") %>" data-title="<%#Eval("explain") %>" <%# GetChecked(Eval("field").ToString()) %> /><%#Eval("explain") %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <input type="hidden" name="checkfields" />
			    </div>
                <div class="control-group">
                    <asp:Button ID="Button1" runat="server" Text="保 存" OnClick="Button1_Click" Width="80px" />
			    </div>
    	</div>
		</div>
	</div>
    
    </form>
</body>
<script>
    function checkAll() {
        if ($("input[type='checkbox'][name='CheckBoxAll']").is(":checked")) {
            $("input[type='checkbox'][name='single-fields']").prop("checked", true);
        } else {
            $("input[type='checkbox'][name='single-fields']").prop("checked", false);
        }
    }
    function checkform() {
        var rslt = true;
        var _fields = "";
        if ($("input[type='checkbox'][name='single-fields']:checked").length <= 0) {
            alert("请选择需要的字段");
            rslt = false;
        } else {
            $("input[type='checkbox'][name='single-fields']:checked").each(function (i, v) {
                _fields += $(v).attr("data-title") + ",";
            })
            $("input[name='checkfields']").val(_fields);
        }
        return rslt;
    }
</script>
</html>
