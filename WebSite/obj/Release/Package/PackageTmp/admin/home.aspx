<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="WebSite.admin.home" %>

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
    <script type="text/javascript">
        $(function () {
            //县市区百分比
            $('#container').highcharts({
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    }
                },
                title: {
                    text: '县市区百分比'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: '百分比',
                    data: [
                        <%=arrArea%>
                    ]
                }]
            });

            // Set up the chart
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container1',
                    type: 'column',
                    margin: 75,
                    options3d: {
                        enabled: true,
                        alpha: 0,
                        beta: 0,
                        depth: 0,
                        viewDistance: 0
                    }
                },
                title: {
                    text: '进度柱状图'
                },
                xAxis: {
                    type: 'datetime',
                    labels: {
                        formatter: function() {
                            //return Highcharts.dateFormat('%Y-%m', this.value);
                            return Highcharts.dateFormat('%Y-%m', this.value);
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: '台数'
                    }
                },
                subtitle: {
                    text: ''
                },
                tooltip: {
                    crosshairs: true,
                    shared: true
                },
                plotOptions: {
                    column : {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: '安装台数',
                    data: [<%=arrCount%>]
                }]
            });

            function showValues() {
                $('#R0-value').html(chart.options.chart.options3d.alpha);
                $('#R1-value').html(chart.options.chart.options3d.beta);
            }

            // Activate the sliders
            $('#R0').on('change', function () {
                chart.options.chart.options3d.alpha = this.value;
                showValues();
                chart.redraw(false);
            });
            $('#R1').on('change', function () {
                chart.options.chart.options3d.beta = this.value;
                showValues();
                chart.redraw(false);
            });

            showValues();
        });
    </script>
</head>
<body>
    <script src="/js/highcharts.js"></script>
    <script src="/js/highcharts-3d.js"></script>
    <script src="/js/modules/exporting.js"></script>
    <form id="form1" runat="server">
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
        <div class="terminal-title" style="margin-top: 30px;">汇总：</div>
        <div class="out-btn-info">
            <%=str%>
        </div>
        <div class="terminal-title">按所属分类：</div>
        <div class="out-btn-info">
            <asp:Repeater ID="rptClass" runat="server">
                <ItemTemplate>
                    <a href="terminallist.aspx?SelectType=ClassID&Keyword=<%#Eval("ClassName") %>"><div class="btn-info"><%#Eval("ClassName") %> (<%#Eval("acount") %>)</div></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="terminal-title">按县市区：</div>
        <div class="out-btn-info">
            <asp:Repeater ID="rptArea" runat="server">
                <ItemTemplate>
                    <a href="terminallist.aspx?SelectType=Area&Keyword=<%#Eval("Area") %>"><div class="btn-info"><%#Eval("Area") %> (<%#Eval("acount") %>)</div></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="terminal-title">按设备厂商：</div>
        <div class="out-btn-info">
            <asp:Repeater ID="rptFactory" runat="server">
                <ItemTemplate>
                    <a href="terminallist.aspx?SelectType=Manufacturer&Keyword=<%#Eval("ManuFacturer") %>"><div class="btn-info"><%#Eval("ManuFacturer") %> (<%#Eval("acount") %>)</div></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="container" style="height: 400px; width: 600px; float: left; margin-top: 30px;"></div>
        <div>
            <div id="container1" style="min-width: 800px;height: 400px; float: left;"></div>
            <div id="sliders" style="float: left;">
                <table>
                    <tr><td>垂直旋转</td><td><input id="R0" type="range" min="0" max="45" value="15"/> <span id="R0-value" class="value"></span></td></tr>
                    <tr><td>水平旋转</td><td><input id="R1" type="range" min="0" max="45" value="15"/> <span id="R1-value" class="value"></span></td></tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
