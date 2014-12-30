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
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=CD6ba8c1f1a1796342ff6eeaf4b1f576"></script>
    <style>
    #l-map 
        {
            width:80%;
            float:left;
            height:650px;
            overflow: hidden;margin:0;
        }
        #r-map {
            width:20%;
            float:right;
            height:650px;
            overflow: hidden;margin:0;
        }
    </style>
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
                                    <asp:ListItem>杭州信颐</asp:ListItem>
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
                            <label for="in-out" class="control-label">屏幕：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="Screen" runat="server" CssClass="select1">
                                    <asp:ListItem>双屏</asp:ListItem>
                                    <asp:ListItem>单屏</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group">
                            
                            <label for="in-out" class="control-label">室内外：</label>
                            <div class="controls controls-inline">
                                <asp:RadioButtonList ID="OutIn" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">室内</asp:ListItem>
                                    <asp:ListItem Value="1">室外</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
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
                            <label for="in-out" class="control-label">时间：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="PostTime" runat="server" CssClass="easyui-datebox app-input" data-options="editable:false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">编号：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Numb" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
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
                            <label for="in-out" class="control-label">赞助商：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Sponsor" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">所属类别：</label>
                            <div class="controls controls-inline">
                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="select1"></asp:DropDownList>
                            </div>
                            <label for="in-out" class="control-label">安装状态：</label>
                            <div class="controls controls-inline">
                                <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">待安装</asp:ListItem>
                                    <asp:ListItem Value="1">已安装</asp:ListItem>
                                    <asp:ListItem Value="2">已搬回</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">转移记录：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Recores" runat="server" CssClass="app-input" Height="100px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">备注：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="Remark" runat="server" CssClass="app-input" Height="100px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label1"><span>图片上传</span></label>
                            <div class="controls controls-inline" id="MyFile0">
                                <input type="file" class="input fl easyui-validatebox" id ="mfile0_5"  name="mfile0_5" /><input type="button" value="增加" class="fl add1" onclick="addFile('MyFile0')" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="in-out" class="control-label">地址搜索：</label>
                            <div class="controls controls-inline">
                                <input type="text" name="sea1" id="sea1" class="app-input" style="width:220px;" /> <input type="button" class="btnSubmit" value="搜 索" onclick="javascript: search_name1()"/>
                            </div>
                            <label for="in-out" class="control-label">坐标：</label>
                            <div class="controls controls-inline">
                                <asp:TextBox ID="LocationCoordinate" runat="server" CssClass="app-input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <div id="l-map"></div>
                            <div id="r-result"></div>
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
<script type="text/javascript">
    function search_name1() {
        var _name = $("#sea1").val();
        search_name(_name);
    }
    var map = new BMap.Map("l-map");
    window.map = map;
    var point = new BMap.Point(120.669819, 28.019119);
    //var myGeo = new BMap.Geocoder();
    //myGeo.getPoint("瓯海区政府", function (point) {
    //if (point) {
    //map.centerAndZoom(point, 15);
    //map.enableScrollWheelZoom();
    //var marker = new BMap.Marker(point);  // 创建标注
    //map.addOverlay(marker);              // 将标注添加到地图中
    //marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
    //}
    //}, "温州市");
    map.centerAndZoom(point, 15);
    map.addControl(new BMap.NavigationControl());  //添加默认缩放平移控件
    map.enableScrollWheelZoom();   //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    //map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL }));  //右上角，仅包含平移和缩放按钮
    //map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT, type: BMAP_NAVIGATION_CONTROL_PAN }));  //左下角，仅包含平移按钮
    //map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //右下角，仅包含缩放按钮
    map.addEventListener("click", addCoordinate);
    function addCoordinate(e) {
        map.clearOverlays();
        var clickpoint = e.point.lng + "," + e.point.lat;
        $("#LocationCoordinate").val(clickpoint);
        var marker1 = new BMap.Marker(e.point);
        map.addOverlay(marker1);
    }
    window.openInfoWinFuns = null;
    function search_name(name) {
        var options = {
            pageCapacity: 10,
            onSearchComplete: function (results) {
                //返回当前页的结果数
                var cPNum = results.getCurrentNumPois();
                // 判断状态是否正确
                if (local.getStatus() == BMAP_STATUS_SUCCESS) {

                    openInfoWinFuns = [];
                    for (var i = 0; i < cPNum; i++) {
                        var marker = addMarker(results.getPoi(i).point, i);
                        var openInfoWinFun = addInfoWindow(marker, results.getPoi(i), i);
                        openInfoWinFuns.push(openInfoWinFun);
                        // 默认打开第一标注的信息窗口
                        var selected = "";
                        if (i == 0) {
                            //selected = "background-color:#f0f0f0;";
                            map.centerAndZoom(results.getPoi(i).point, 15);
                            openInfoWinFun();
                        }
                    }
                }
            }
        };
        //var local = new BMap.LocalSearch(map, options);
        var local = new BMap.LocalSearch(map, {
            renderOptions: { map: map, panel: "r-result" }
        });
        window.local = local;
        local.search(name);
        // 百度地图API功能
        //var map = new BMap.Map("allmap");            // 创建Map实例
        //map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
        //var local = new BMap.LocalSearch(map, {
        //    renderOptions: { map: map }
        //});
        //local.search(name);
    }
    // 添加标注
    function addMarker(point, index) {
        var myIcon = new BMap.Icon("http://api.map.baidu.com/img/markers.png", new BMap.Size(23, 25), {
            offset: new BMap.Size(10, 25),
            imageOffset: new BMap.Size(0, 0 - index * 25)
        });
        var marker = new BMap.Marker(point, { icon: myIcon });
        map.addOverlay(marker);
        return marker;
    }
    // 添加信息窗口
    function addInfoWindow(marker, poi, index) {
        var maxLen = 10;
        var name = null;
        if (poi.type == BMAP_POI_TYPE_NORMAL) {
            name = "地址：  "
        } else if (poi.type == BMAP_POI_TYPE_BUSSTOP) {
            name = "公交：  "
        } else if (poi.type == BMAP_POI_TYPE_SUBSTOP) {
            name = "地铁：  "
        }
        // infowindow的标题
        var infoWindowTitle = '<div style="font-weight:bold;color:#CE5521;font-size:14px">' + poi.title + '</div>';
        // infowindow的显示信息
        var infoWindowHtml = [];
        infoWindowHtml.push('<table cellspacing="0" style="table-layout:fixed;width:100%;font:12px arial,simsun,sans-serif"><tbody>');
        infoWindowHtml.push('<tr>');
        infoWindowHtml.push('<td style="vertical-align:top;line-height:16px;width:38px;white-space:nowrap;word-break:keep-all">' + name + '</td>');
        infoWindowHtml.push('<td style="vertical-align:top;line-height:16px">' + poi.address + ' </td>');
        infoWindowHtml.push('</tr>');
        infoWindowHtml.push('</tbody></table>');
        var infoWindow = new BMap.InfoWindow(infoWindowHtml.join(""), { title: infoWindowTitle, width: 200 });
        var openInfoWinFun = function () {
            marker.openInfoWindow(infoWindow);
            for (var cnt = 0; cnt < maxLen; cnt++) {
                if (!document.getElementById("list" + cnt)) { continue; }
                if (cnt == index) {
                    document.getElementById("list" + cnt).style.backgroundColor = "#f0f0f0";
                } else {
                    document.getElementById("list" + cnt).style.backgroundColor = "#fff";
                }
            }
        }
        marker.addEventListener("click", openInfoWinFun);
        return openInfoWinFun;
    }
</script>
</html>
