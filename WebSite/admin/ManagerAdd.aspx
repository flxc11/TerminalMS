<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerAdd.aspx.cs" Inherits="WebSite.admin.ManagerAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=HD.Config.UIConfig.SoftName %></title>
    <link rel="stylesheet" href="/css/Global.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/js/jquery.js"></script>
    <script src="/js/cnvp.js"></script>
    <style>
		label,
		input,
		button{
		  font-size: 14px;
		  font-weight: normal;
		  line-height: 18px;
		}
		input,
		button{
		  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
		}
		label {
		  display: block;
		  margin-bottom: 5px;
		  color: #333333;
		}
		input{
		  display: inline-block;
		  width: 210px;
		  height: 18px;
		  padding: 4px;
		  margin-bottom: 9px;
		  font-size: 13px;
		  line-height: 18px;
		  color: #555555;
		  border: 1px solid #cccccc;
		  -webkit-border-radius: 3px;
		  -moz-border-radius: 3px;
		  border-radius: 3px;
		}
		input[type="button"],
		input[type="reset"],
		input[type="submit"] {
		  width: auto;
		  height: auto;
		}
		input {
		  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  -webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -moz-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -ms-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -o-transition: border linear 0.2s, box-shadow linear 0.2s;
		  transition: border linear 0.2s, box-shadow linear 0.2s;
		}
		.control-group a {
			display: inline-block;
			*zoom:1;
			*display: inline;
			-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
		  -webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -moz-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -ms-transition: border linear 0.2s, box-shadow linear 0.2s;
		  -o-transition: border linear 0.2s, box-shadow linear 0.2s;
		  transition: border linear 0.2s, box-shadow linear 0.2s;
			-webkit-border-radius: 5px;
			-moz-border-radius: 5px;
			border-radius: 5px;
			border: 1px solid #2584bb;
			width: 97px;
			text-align: center;
			cursor: pointer;
			color: #fff;
			margin-right: 14px;
			background: #6cbbe6;
			/* Firefox 3.6+ */
			background: -moz-linear-gradient(top, #6cbbe6, #1a7db5);
			/* Safari 5.1+, Chrome 10+ */
			background: -webkit-linear-gradient(top, #6cbbe6, #1a7db5);
			/* Opera 11.10+ */
			background: -o-linear-gradient(top, #6cbbe6, #1a7db5);
		}
		input:focus,
		textarea:focus {
		  border-color: rgba(82, 168, 236, 0.8);
		  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
		  -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
		  box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
		  outline: 0;
		  outline: thin dotted \9;
		  /* IE6-9 */

		}
		input{
		  margin-left: 0;
		}
		input:focus:required:invalid,
		textarea:focus:required:invalid,
		select:focus:required:invalid {
		  color: #b94a48;
		  border-color: #ee5f5b;
		}
		input:focus:required:invalid:focus,
		textarea:focus:required:invalid:focus,
		select:focus:required:invalid:focus {
		  border-color: #e9322d;
		  -webkit-box-shadow: 0 0 6px #f8b9b7;
		  -moz-box-shadow: 0 0 6px #f8b9b7;
		  box-shadow: 0 0 6px #f8b9b7;
		}
		:-moz-placeholder {
		  color: #999999;
		}
		::-webkit-input-placeholder {
		  color: #999999;
		}
		.help-block,
		.help-inline {
		  color: #555555;
		}
		.help-block {
		  display: block;
		  margin-bottom: 9px;
		}
		.help-inline {
		  display: inline-block;
		  *display: inline;
		  /* IE7 inline-block hack */

		  *zoom: 1;
		  vertical-align: middle;
		  padding-left: 5px;
		}
		.form-horizontal input,
		.form-horizontal .help-inline {
		  display: inline-block;
		  margin-bottom: 0;
		}

		.control-group {
		  margin-bottom: 9px;
		}
		.form-horizontal .control-group {
		  margin-bottom: 18px;
		  *zoom: 1;
		}
		.form-horizontal .control-group:before,
		.form-horizontal .control-group:after {
		  display: table;
		  content: "";
		}
		.form-horizontal .control-group:after {
		  clear: both;
		}
		.form-horizontal .control-label {
		  float: left;
		  width: 210px;
		  padding-top: 5px;
		  text-align: right;
		}
		.form-horizontal .controls {
		  margin-left: 220px;
		  /* Super jank IE7 fix to ensure the inputs in .input-append and input-prepend don't inherit the margin of the parent, in this case .controls */

		  *display: inline-block;
		  *margin-left: 0;
		  *padding-left: 20px;
		}
		.form-horizontal .help-block {
		  margin-top: 9px;
		  margin-bottom: 0;
		}
		.form-horizontal .form-actions {
		  padding-left: 160px;
		}
		/*helps info*/

		.form-horizontal .help-inline {
			position: relative;
			padding: 3px 6px;
			background: #444;
			color: #fff;
			z-index: 9;
			border-radius: 3px;
			-webkit-transition:all 0.5s;
		  -moz-transition:all 0.5s;
		  -o-transition:all 0.5s;
		  -ms-transition:all 0.5s;
		  transition:all 0.5s;
		  position: relative;
		  margin-left: -500px;
		}	
		.form-horizontal .help-inline::before {
			content:"";
			display: block;
			height: 0;
			width: 0;
			position: absolute;
			top: 7px;
			left: -12px;
			border: 6px solid transparent;
			border-right-color: #444;
			z-index: 2;
		}
		.controls {
			position: relative;
			overflow: hidden;
		}	
		.form-horizontal .help-block {
			background: #444;
			border-radius: 3px;
			padding: 3px 6px;
			position: absolute;
			top: 28px;
			left: 0;
			z-index: -2;
			color: #fff;
			-webkit-transition:all 0.5s;
		  -moz-transition:all 0.5s;
		  -o-transition:all 0.5s;
		  -ms-transition:all 0.5s;
		  transition:all 0.5s;
		}
		.form-horizontal .help-block::after {
			border: 6px solid transparent;
			border-bottom-color: #444;
			content:"";
			display: block;
			height:0;
			width: 0;
			left: 5px;
			top: -12px;
			z-index: 2;
			position: absolute;
		} 
		.form-horizontal input:active + .help-inline,
		.form-horizontal input:focus + .help-inline {
			margin-left: 8px;
		}
		.form-horizontal .controls-block input:active,
		.form-horizontal .controls-block input:focus {
			margin-bottom: 35px;
		}
    </style>
</head>
<body>
    <form id="ff" name="ff" runat="server" action="ManagerAdd.aspx" onsubmit="return checkform()">
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
				当前位置：账号管理 > 新建管理员账号
			</div>
            <div style="padding:50px 0 0 100px;">
			    <div class="control-group">
			      <label for="email" class="control-label">登陆账号：</label>
			      <div class="controls controls-inline">
                      <asp:TextBox ID="UserName" CssClass="textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
                <div class="control-group">
			      <label for="email" class="control-label">登录密码：</label>
			      <div class="controls controls-inline">
                      <asp:TextBox ID="UserPass" CssClass="textbox" TextMode="Password" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="TrueName" class="control-label">真实姓名：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="TrueName" CssClass="textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserTel" class="control-label">联系电话：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserTel" CssClass="textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserEmail" class="control-label">邮箱地址：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserEmail" CssClass="textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserUnit" class="control-label">单位名称：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserUnit" CssClass="textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
                <div class="control-group" style="padding-left: 122px;">
                    <asp:Button ID="Button1" runat="server" Text="新 增" OnClick="Button1_Click" Width="80px" />
			      </div>
    	</div>
		</div>
	</div>
    
    </form>
</body>
<script>
    function checkform() {
        var rslt = true;
        if ($("#UserName").val() == "") {
            alert("请输入登陆账号");
            $("#UserName").focus();
            rslt = false;
            return false;
        }
        if ($("#UserPass").val() == "") {
            alert("请输入登陆密码");
            $("#UserPass").focus();
            rslt = false;
            return false;
        }
        return rslt;
    }
</script>
</html>
