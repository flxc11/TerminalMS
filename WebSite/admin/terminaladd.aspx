<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminaladd.aspx.cs" Inherits="WebSite.admin.terminaladd" %>

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
    <form id="ff" name="ff" action="?Action=Save" runat="server" enctype="multipart/form-data">
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
				当前位置：<a href="#">终端管理</a> > 添加终端信息
			</div>
			<div class="main-form">
                <div class="cnvp-tab-panle">
                    <div class="table-1">
                        <div class="control-group">
                            <label for="in-out" class="control-label">设备厂商：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="Manufacturer" runat="server" CssClass="select1">
                                    <asp:ListItem>温州智景</asp:ListItem>
                                    <asp:ListItem>杭州信怡</asp:ListItem>
                                    <asp:ListItem>上海高清</asp:ListItem>
                                    <asp:ListItem>顺泰</asp:ListItem>
                                    <asp:ListItem>冠众</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="in-out" class="control-label">尺寸：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="MachineSize" runat="server" CssClass="select1">
                                    <asp:ListItem>42寸</asp:ListItem>
                                    <asp:ListItem>55寸</asp:ListItem>
                                    <asp:ListItem>26寸</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">屏幕：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="Screen" runat="server" CssClass="select1">
                                    <asp:ListItem>双屏</asp:ListItem>
                                    <asp:ListItem>单屏</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="in-out" class="control-label">室内外：</label>
                            <div class="controls controls-inline">
                                <asp:RadioButtonList ID="OutIn" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">室内</asp:ListItem>
                                    <asp:ListItem Value="1">室外</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">区域：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="Area" runat="server" CssClass="select1">
                                    <asp:ListItem>鹿城区</asp:ListItem>
                                    <asp:ListItem>瓯海区</asp:ListItem>
                                    <asp:ListItem>龙湾区</asp:ListItem>
                                    <asp:ListItem>温州经济开发区</asp:ListItem>
                                    <asp:ListItem>乐清市</asp:ListItem>
                                    <asp:ListItem>瑞安市</asp:ListItem>
                                    <asp:ListItem>苍南县</asp:ListItem>
                                    <asp:ListItem>永嘉县</asp:ListItem>
                                    <asp:ListItem>洞头县</asp:ListItem>
                                    <asp:ListItem>平阳县</asp:ListItem>
                                    <asp:ListItem>文成县</asp:ListItem>
                                    <asp:ListItem>泰顺县</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="in-out" class="control-label">放置地点：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Location" runat="server" CssClass="easyui-validatebox app-input" data-options="required:true,missingMessage:'请输入放置地点'"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">签收：</label>
                            <div class="controls controls-inline">
                                <asp:RadioButtonList ID="SignIn" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">未签收</asp:ListItem>
                                    <asp:ListItem Value="1">已签收</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <label for="in-out" class="control-label">开机时长：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="OpenTime" runat="server" CssClass="select1">
                                    <asp:ListItem>10小时</asp:ListItem>
                                    <asp:ListItem>8小时</asp:ListItem>
                                    <asp:ListItem>9小时</asp:ListItem>
                                    <asp:ListItem>11小时</asp:ListItem>
                                    <asp:ListItem>12小时</asp:ListItem>
                                    <asp:ListItem>13小时</asp:ListItem>
                                    <asp:ListItem>14小时</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">时间：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="PostTime" runat="server" CssClass="easyui-datebox app-input" data-options="required:true,missingMessage:'请选择时间',editable:false"></asp:TextBox>
                            </div>
                            <label for="in-out" class="control-label">编号：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Numb" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">系统：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="System" runat="server" CssClass="select1">
                                    <asp:ListItem>Windows</asp:ListItem>
                                    <asp:ListItem>安卓</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="in-out" class="control-label">使用状况：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Stituation" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">详细地址：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Address" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                            <label for="in-out" class="control-label">联系人和电话：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="ContentTel" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">赞助商：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Sponsor" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">转移记录：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Recores" runat="server" CssClass="app-input" Height="200px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">备注：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Remark" runat="server" CssClass="app-input" Height="200px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label1"><span>图片上传</span></label>
                            <div class="controls controls-inline" id="MyFile0">
                                <input type="file" class="input fl easyui-validatebox" id ="mfile0_5"  name="mfile0_5" /><input type="button" value="增加" class="fl add1" onclick="addFile('MyFile0')" />
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
<script>
    function submitForm(action) {
        if ($("#ff").form("validate")) {
            function doSubmit() {
                $("#ff").attr("action", "terminaladd.aspx?Action=Save");
                $("#ff").submit();
            }
            setTimeout(doSubmit,0);
        } else {
            $.messager.alert("信息", "信息填写不规范");
        }
    }
    function addFile(id) {
        var inputstr = "";
        var i = 1;
        var index = id.substring(6);
        while (true) {
            if (document.getElementById("mfile" + index + "_" + i) == null) break;
            else i++;
        }
        var inputid = "mfile" + index + "_" + i;
        var str = "<br /><INPUT type=\"file\" class=\"input fl\" id=\"" + inputid + "\" name=\"" + inputid + "\" /><INPUT id=\"r" + inputid + "\" type=\"button\" class=\"fl add1\" value=\"删除\" onclick=removeFile(\"" + inputid + "\") />";
        document.getElementById(id).insertAdjacentHTML("beforeEnd", str);

    }
    function addFile1(id) {
        var inputstr = "";
        var i = 1;
        var index = id.substring(6);
        while (true) {
            if (document.getElementById("scwmfile" + index + "_" + i) == null) break;
            else i++;
        }
        var inputid = "scwmfile" + index + "_" + i;
        var str = "<div class='control-group'><div class='controls-inline'><label for='in-out' class='control-label1'>&nbsp;</label><div class='controls-inline'><input type=\"file\" class=\"input fl\" id=\"" + inputid + "\" name=\"" + inputid + "\" /><input id=\"r" + inputid + "\" type=\"button\" class=\"fl add1\" value=\"删除\" onclick=removeFile(\"" + inputid + "\") /></div></div></div>";
        document.getElementById(id).insertAdjacentHTML("beforeEnd", str);

    }
    function removeFile(id) {
        var obj = document.getElementById(id);
        var obj1 = document.getElementById("r" + id);
        if (obj != null && obj1 != null) {
            //obj.removeNode(true);
            //obj1.removeNode(true);
            obj.parentNode.removeChild(obj);
            obj1.parentNode.removeChild(obj1);
        }
    }

</script>
</html>
