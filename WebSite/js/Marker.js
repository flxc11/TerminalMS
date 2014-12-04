/**
 * Created with JetBrains WebStorm.
 * User: Administrator
 * Date: 13-9-10
 * Time: 下午3:10
 * To change this template use File | Settings | File Templates.
 */
function addMarker(_index,point,_name,content,_icon){
    var _height;
    if (ieVersion() < 100) {
        _height = 100;
    }else {
        _height = "auto";
    }
    var searchInfoWindow = null;
    searchInfoWindow = new BMapLib.SearchInfoWindow(map, content, {
        title  : _name,      //标题
        width  : 290,             //宽度
        height : _height,              //高度
        panel  : "panel",         //检索结果面板
        enableAutoPan : true,     //自动平移
        searchTypes   :[

        ]
    });
    //var myIcon = new BMap.Icon(_icon, new BMap.Size(23,25));
    var marker = new BMap.Marker(point); //创建marker对象
    //var marker = new BMap.Marker(point); //创建marker对象
    //marker.enableDragging(); //marker可拖拽
    marker.setTitle(_name);
    marker.addEventListener("click", function(e){
        searchInfoWindow.open(marker);
        clickchoose();
        showbpic();
    })
    map.addOverlay(marker); //在地图中添加marker
//    if(_index == 0) {
//        map.centerAndZoom(point, 15);
//        searchInfoWindow.open(marker); //在marker上打开检索信息串口
//    }
    //addLabel(point,_name);
}
function addLabel(point,_name) {
    var opts = {
        position : point,    // 指定文本标注所在的地理位置
        offset   : new BMap.Size(20, -30)    //设置文本偏移量

    }
    var label = new BMap.Label(_name, opts);  // 创建文本标注对象
    label.setStyle({
        color : "black",
        fontSize : "12px",
        height : "20px",
        lineHeight : "20px",
        padding : "5px",
        fontFamily:"微软雅黑"
    });
    map.addOverlay(label);
}
function addMapControl() {
    //向地图中添加默认缩放平移控件
    var ctrl_nav = new BMap.NavigationControl();
    map.addControl(ctrl_nav);
    //右上角，仅包含平移和缩放按钮
    var ctrl_nav_tr = new BMap.NavigationControl( {
        anchor : BMAP_ANCHOR_TOP_RIGHT,
        type : BMAP_NAVIGATION_CONTROL_ZOOM
    });
    map.addControl(ctrl_nav_tr);
    //左下角，仅包含平移按钮
    var ctrl_nav_bl = new BMap.NavigationControl( {
        anchor : BMAP_ANCHOR_BOTTOM_LEFT,
        type : BMAP_NAVIGATION_CONTROL_PAN
    });
    map.addControl(ctrl_nav_bl);
    //向地图中左下角添加比例尺控件
    var ctrl_sca = new BMap.ScaleControl( {
        anchor : BMAP_ANCHOR_BOTTOM_LEFT
    });
    map.addControl(ctrl_sca);
    map.enableScrollWheelZoom();   //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
}



//公交查询
function bus_search(word_from,word_to) {
    var myGeo = new BMap.Geocoder();
    map.clearOverlays();
    var transit = new BMap.TransitRoute(map, {
        renderOptions: {map: map, panel: "r-result"},
        pageCapacity: 3
    });
    transit.search(word_from, word_to);
}
//汽车路线查询
function car_search(word_from,word_to) {
    map.clearOverlays();

    var transit = new BMap.DrivingRoute(map, {
        renderOptions: {map: map, panel: "r-result", autoViewport: true},
        pageCapacity: 3
    });
    transit.search(word_from, word_to);
}

//根据地址获取坐标
function getCoordinate(_Address) {
    var myGeo = new BMap.Geocoder();
    myGeo.getPoint(_Address, function(point){
        if (point) {
            return point;
        }
    },"温州市")
}

function showbpic() {
    var x = 10;
    var y = 20;
    $( ".divcnt img" ).mouseenter( function( e ){
        var tooltip = '<div id="tooltip"><img src="' + this.src + '" width="400" height="300" /></div>';
        $("body").append(tooltip);
        $("#tooltip")
            .css({
                "top": (e.pageY + y) + "px",
                "left": (e.pageX + x) + "px"
            }).show("fast");
    }).mouseout(function(){
            $("#tooltip").remove();
        }).mousemove(function(e){
            $("#tooltip")
                .css({
                    "top": (e.pageY+y) + "px",
                    "left":  (e.pageX+x)  + "px"
                });
        })
}