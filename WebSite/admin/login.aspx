<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebSite.admin.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=HD.Config.UIConfig.SoftName %></title>
    <link rel="stylesheet" href="/css/Global.css">
    <link rel="stylesheet" href="/css/reg-login.css">
    <script src="/js/jquery.js"></script>
    <style>
    	html {
			width: 100%;
			height: 100%;
			overflow: hidden;
    	}
		body {
			width: 100%;
			height: 100%;
			background-image: url("/images/bglogin.jpg");
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
  background: url("/images/logos.png") no-repeat 50px top;
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
input[type="button"],input[type="submit"] {
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
  width: 110px;
  padding-top: 5px;
  text-align: right;
}
.form-horizontal .controls {
  margin-left: 120px;
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
    <div class="wrap-login">
    	<div class="login">
    		<form action="?Action=Login" name="form1" onsubmit="return CheckForm()" method="post" class="form-horizontal">
	    		<fieldset>
			    <div class="control-group">
			      <label for="email" class="control-label">登录账号：</label>
			      <div class="controls controls-inline">
			        <input type="text" id="UserName" name="UserName" />
			        <span class="help-inline">请输入登陆账号！</span>
			      </div>
			    </div>
			    <div class="control-group">
			      <label for="password" class="control-label">登录密码：</label>
			      <div class="controls controls-inline">
			        <input type="password" id="password" name="password" />
			        <span class="help-inline">请输入登陆密码</span>
			      </div>
			      
			    </div>
			    <div class="control-group" style="padding-left: 122px;">
			      	<input type="submit" value="登 录" />
			      </div>
			  </fieldset>
		    </form>
    	</div>
    </div>
</body>
    <script>
        function CheckForm() {
            var rslt = true;
            if (/^\s*$/.test($("#UserName").val())) {
                alert("请输入登陆账号！");
                $("#UserName").focus();
                rslt = false;
                return false;
            };
            if (/^\s*$/.test($("#password").val())) {
                alert("请输入登陆密码！");
                $("#password").focus();
                rslt = false;
                return false;
            };
            return rslt;
        }
    </script>
</html>
