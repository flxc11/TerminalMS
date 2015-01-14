<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="WebSite.user.home" %>

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

            firstCharts();

            

            function SecondChart(yearMonth) {

                var chart1 = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container1',
                        type: 'column',
                        margin: 75,
                        options3d: {
                            enabled: true,
                            alpha: 10,
                            beta: 10,
                            depth: 25,
                            viewDistance: 50
                        }
                    },
                    title: {
                        text: yearMonth + ' 进度柱状图'
                    },
                    xAxis: {
                        allowDecimals: false,
                        labels: {
                            formatter: function () {
                                return this.value; // clean, unformatted number for year
                            }
                        }
                    },
                    yAxis: {
                        title: {
                            text: '台数'
                        }
                    },
                    // tooltip: {
                    //     crosshairs: true,
                    //     shared: true
                    // },
                    plotOptions: {
                        column : {
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            pointStart: 1,
                            events: {
                                click: function(e) {
                                    //console.log(Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', e.xAxis))
                                    //console.log()
                                    firstCharts();
                                }
                            }
                        }
                    },
                    series: [{
                        name: '安装台数',
                        //data: [10, null, null, 15,null,null,null,null,null,null,1,2,null,null,1,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null]
                        data: []
                    }]
                });
                $.ajax({
                        url: '/user/userjson.aspx',
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            Action: 'GetMonthTer',
                            YearMonth: yearMonth,
                            Time: new Date().getTime()
                        }/*,
                beforeSend: function() {
                    $("#container1").html("asdf");
                }*/
                    })
                    .done(function(d) {
                        var arrdate = d.date.split(',');
                        for (var i = 0; i < arrdate.length; i++) {
                            if (arrdate[i] == "null") {
                                chart1.series[0].addPoint("");
                            } else {
                                chart1.series[0].addPoint({y: parseInt(arrdate[i]), name:"点击柱体返回<br>" + yearMonth + "-" + (i + 1)});
                            };
                        };
                    })
                // Activate the sliders
                $('#R0').off();
                $('#R1').off();
                $('#R0').on('change', function () {
                    chart1.options.chart.options3d.alpha = this.value;
                    showValues(chart1);
                    chart1.redraw(false);
                });
                $('#R1').on('change', function () {
                    chart1.options.chart.options3d.beta = this.value;
                    showValues(chart1);
                    chart1.redraw(false);
                });

                showValues(chart1);
            }
            function firstCharts() {
                // Set up the chart
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container1',
                        type: 'column',
                        margin: 75,
                        options3d: {
                            enabled: true,
                            alpha: 15,
                            beta: 15,
                            depth: 25,
                            viewDistance: 50
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
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true
                            },
                            events: {
                                click: function(e) {
                                    //console.log(Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', e.xAxis))
                                    //console.log()
                                    SecondChart(Highcharts.dateFormat('%Y-%m', e.point.options['x']));
                                }
                            }
                        }
                    },
                    series: [{
                        name: '安装台数',
                        data: [<%=arrCount%>]
                    }]
                });

                // Activate the sliders
                $('#R0').off();
                $('#R1').off();
                $('#R0').on('change', function () {
                    chart.options.chart.options3d.alpha = this.value;
                    showValues(chart);
                    chart.redraw(false);
                });
                $('#R1').on('change', function () {
                    chart.options.chart.options3d.beta = this.value;
                    showValues(chart);
                    chart.redraw(false);
                });

                showValues(chart);
            }

        });
        function showValues(obj) {
            $('#R0-value').html(obj.options.chart.options3d.alpha);
            $('#R1-value').html(obj.options.chart.options3d.beta);
        }
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
            <div id="container1" style="min-width: 800px; height: 400px; float: left;"></div>
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
