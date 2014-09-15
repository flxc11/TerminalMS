<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg.aspx.cs" Inherits="WebSite.user.reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户注册</title>
	<link rel="stylesheet" href="/css/Global.css">
    <link rel="stylesheet" href="/css/reg-login.css">
    <link rel="stylesheet" href="/themes/default/easyui.css">
    <link rel="stylesheet" href="/themes/icon.css">
    <style>
    	html {
			width: 100%;
			height: 100%;
			overflow: hidden;
    	}
		body {
			width: 100%;
			height: 100%;
			background-image: url("../images/bglogin.jpg");
			background-repeat: no-repeat;
			-webkit-background-size: cover;
			background-size: cover;
		}
		form {
		  margin: 0;
		}
		fieldset {
		  padding: 0;
		  margin: 0;
		  border: 0;
		  padding-top: 146px;
		  background: url("/images/logos.png") no-repeat center top;
		}
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
    <script src="/js/jquery.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script src="/js/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <div class="wrap-reg">
    	<div class="reg">
    		<form name="ff" method="post" id="ff" class="form-horizontal" runat="server">
	    		<fieldset>
			    <div class="control-group">
			      <label for="email" class="control-label">登录账号：</label>
			      <div class="controls controls-inline">
                      <asp:TextBox ID="UserName" CssClass="easyui-validatebox textbox" runat="server" data-options="required:true,missingMessage:'请最少输入5个数字和字母组合或者2-6个汉字'" validType="Composite_validation[/^[\u4E00-\u9FA5]{2,6}$|^[A-Za-z0-9]{5,15}$/,'用户名不合法（请最少输入5个数字和字母组合或者2-6个汉字）','该用户名已存在！']" ></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="password" class="control-label">登录密码：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserPass" CssClass="easyui-validatebox textbox" runat="server" data-options="required:true,missingMessage:'请输入登录密码'" TextMode="Password"></asp:TextBox>
			      </div>

			    </div>
			    <div class="control-group">
			      <label for="checkpassword" class="control-label">确认密码：</label>
			      <div class="controls controls-inline">
			        <input type="password" class="easyui-validatebox textbox" id="checkpassword" data-options="required:true,missingMessage:'请再次输入密码'" validType="equals['#UserPass']" />
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="TrueName" class="control-label">真实姓名：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="TrueName" CssClass="easyui-validatebox textbox" runat="server" data-options="required:true,missingMessage:'请输入真实姓名'"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserTel" class="control-label">联系电话：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserTel" CssClass="easyui-validatebox textbox" runat="server" data-options="required:true,missingMessage:'请输入联系电话'" validType="telphone"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserEmail" class="control-label">邮箱地址：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserEmail" CssClass="easyui-validatebox textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="UserUnit" class="control-label">单位名称：</label>
			      <div class="controls controls-inline">
			        <asp:TextBox ID="UserUnit" CssClass="easyui-validatebox textbox" runat="server"></asp:TextBox>
			      </div>
			    </div>
			    <div class="control-group" style="padding-left: 222px;">
			      	<a href="javascript:void(0)" onclick="submitForm()">注 册</a>
	    			<a href="javascript:void(0)" onclick="clearForm()">重 置</a>
			      </div>
			  </fieldset>
		    </form>
    	</div>
    </div>
</body>
<script>
    $.extend($.fn.validatebox.defaults.rules, {
        Composite_validation: {  //验证用户名是否存在
            validator: function (value, param) {
                var M_reg = new RegExp(param[0]);
                value = value.toLowerCase();
                if (!M_reg.test(value)) {
                    $.fn.validatebox.defaults.rules.Composite_validation.message = param[1];
                    return false;
                };
                len = value.length;
                if (len >= 2) {
                    var m_result = $.ajax({
                        type: "post",
                        dataType: "type",
                        url: "/ajax.aspx",
                        data: "Action=IsUserExists&UserName=" + value + "&Time=" + new Date().getTime(),
                        async: false
                    }).responseText;
                    if (m_result == "True") {
                        $.fn.validatebox.defaults.rules.Composite_validation.message = param[2];
                        return false;
                    };
                    return true;
                };
            },
            message: ''
        },
        equals: {  //检查密码和确认密码是否相同
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: '两次密码输入不一致'
        },
        telphone: {
            validator: function (value) {
                return /^(\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$/i.test(value);
            },
            message: '手机或固话号码格式不正确'
        }
    })
    function submitForm() {
        if ($("#ff").form("validate")) {
            $("#ff").attr("action", "Reg.aspx?Action=reg");
            $("#ff").submit();
            /*function doSubmit() {
		        
		        $("#ff").action = "reg.aspx?Action=reg";
		        $("#ff").submit();
            }*/
            //setTimeout(doSubmit,0);
        } else {
            $.messager.alert("信息", "输入不合法，请检查后再提交！")
        }
    }
    function clearForm() {
        $('#ff').form('clear');
    }
    $(function () {
        $(".wrap-reg").css("padding-top", ($(window).height() - 550) / 2);
    })
</script>
</html>
