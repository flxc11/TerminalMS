/**
 * Created with JetBrains WebStorm.
 * User: hd.work@163.com
 * Date: 13-8-23
 * Time: 下午3:37
 * To change this template use File | Settings | File Templates.
 */

function loadScript() {
    var script = document.createElement("script");
    script.src = "http://api.map.baidu.com/api?v=2.0&ak=CD6ba8c1f1a1796342ff6eeaf4b1f576&callback=initialize";
    document.body.appendChild(script);

}
window.onload = loadScript;

//创建和初始化地图函数：
function initialize() {
    var script = document.createElement("script");
    script.src = "http://api.map.baidu.com/library/SearchInfoWindow/1.5/src/SearchInfoWindow_min.js";
    document.body.appendChild(script);
    ajaxMap();
}
function ajaxMap(_ClassID,_AreaID,_ManuFacturer,_Keyword) {
    var classid = (_ClassID != "undefined" && _ClassID) || "";
    var area = (_AreaID != "undefined" && _AreaID) || "";
    var facturer = (_ManuFacturer != "undefined" && _ManuFacturer) || "";
    var keyword = (_Keyword != "undefined" && _Keyword) || "";
    $.ajax( {
        type: "post", //用POST方式传输
        dataType: "xml", //数据格式:JSON
        url: 'GetPoint.aspx?Time=' + (new Date().getTime()), //目标地址
        data: {
            ClassID: classid,
            AreaID: area,
            ManuFacturer: facturer,
            Keyword: keyword
        },
        beforeSend: function() {
            $('#allmap').html("<div style='text-align: center;padding-top: 200px;'><img src='/images/loading.gif'></div>");//等待图片
        },
        success: function(xml) {
            $('#allmap').html("");//等待图片
            var map = new BMap.Map("allmap");
            window.map = map;  //将map变量存储在全局
            addMapControl();
            map.clearOverlays();
            var stra = new Array();
            if($(xml).find("Rows").length > 0) {
                $(xml).find("Rows").each(function(i,v) {
                    var content = '<div style="margin:0;line-height:20px;padding:2px;" class="divcnt">' +
                        '放置地点：' + $(v).find("Location").text() +
                        '<br/>详细地址：' + $(v).find("Address").text() +
                        '<input type="hidden" value="' + $(v).find("Address").text() + '" id="curaddress" />' +
                        '<br/>联系人电话：' + $(v).find("ContentTel").text() + '</div>' +
                        '<div class="BMapLib_nav" id="BMapLib_nav0"><ul class="BMapLib_nav_tab" id="BMapLib_nav_tab0"><li class="BMapLib_current" id="BMapLib_tab_tohere0" style="display: block; width: 99px;"><span class="BMapLib_icon BMapLib_icon_tohere"></span>到这里去</li><li class="" id="BMapLib_tab_fromhere0" style="display: block; width: 100px;"><span class="BMapLib_icon BMapLib_icon_fromhere"></span>从这里出发</li></ul><ul class="BMapLib_nav_tab_content"><li id="BMapLib_transBox0"><table width="100%" align="center" border="0" cellpadding="0" cellspacing="0"><tbody><tr><td width="30" style="padding-left:8px;"><div id="BMapLib_stationText0">起点</div></td><td><input id="trans_text" class="BMapLib_trans_text" type="text" maxlength="100" autocomplete="off"></td><td width="106" style="padding-left:7px;"><input id="BMapLib_search_bus_btn0" type="button" value="公交" class="iw_bt" style="margin-right:5px;"><input id="BMapLib_search_drive_btn0" type="button" class="iw_bt" value="驾车"></td></tr></tbody></table></li></ul></div>';
                    var name = $(v).find("Location").text();
                    var strs = new Array();
                    var _point = $(v).find("Coordinate").text();
                    strs = _point.split(",");
                    var point = new BMap.Point(strs[0],strs[1]);
                    var icon = "http://api.map.baidu.com/img/markers.png";
                    addMarker(i,point,name,content,icon);
                    stra.push(point);
                })
                var points = stra;
                map.setViewport(points);
            }
            else {
                map.centerAndZoom(new BMap.Point(120.669847,28.019127), 12);
                map.enableScrollWheelZoom();
            }
        }
    })
}
function ajaxMap1(_AreaID,_SportID,_SiteID) {
    var area = (_AreaID != "undefined" && _AreaID) || "";
    var sport = (_SportID != "undefined" && _SportID) || "";
    var site = (_SiteID != "undefined" && _SiteID) || "";
    var ISInDoor = "";
    var ISOutDoor = "";
    var ISCharge = "";
    var ISFree = "";
    $.ajax( {
        type: "post", //用POST方式传输
        dataType: "xml", //数据格式:JSON
        url: 'mapajax.aspx?Time=' + (new Date().getTime()), //目标地址
        data: {
            AreaID: area,
            SportID: sport,
            SiteID: site,
            ISInDoor: ISInDoor,
            ISOutDoor: ISOutDoor,
            ISCharge: ISCharge,
            ISFree: ISFree
        },
        beforeSend: function() {
            $('#allmap').html("<div style='text-align: center;padding-top: 200px;'><img src='images/loading.gif'></div>");//等待图片
        },
        success: function(xml) {
            var map = new BMap.Map("allmap");
            window.map = map;  //将map变量存储在全局
            addMapControl();
            map.clearOverlays();
            map.enableScrollWheelZoom();
            var stra = new Array();

            if($(xml).find("DepartCoordi").length > 0) {     //判断是否是点击 区域选择
                var strs = new Array();
                var _point = $(xml).find("DepartCoordi").text();
                var _coorname = $(xml).find("DepartCoordiName").text();
                strs = _point.split(",");
                var point = new BMap.Point(strs[0],strs[1]);
                map.centerAndZoom(point, 18);
                //alert("当前地图缩放级别：" + map.getZoom());
                var _icon = "../images/2.png";
                var myIcon = new BMap.Icon(_icon, new BMap.Size(30,29));
                var marker = new BMap.Marker(point, {icon:myIcon}); //创建marker对象
                marker.setTitle(_coorname);  //
                //var marker = new BMap.Marker(point); //创建marker对象
                //marker.enableDragging(); //marker可拖拽
                //marker.addEventListener("click", function(e){
                    //ajaxMap1(AreaID,"","");
                //})
                map.addOverlay(marker); //在地图中添加marker
            }
            if($(xml).find("Rows").length > 0) {
                $(xml).find("Rows").each(function(i,v) {
                    var content = '<div style="margin:0;line-height:20px;padding:2px;" class="divcnt">' +
                        '<img src="' + $(v).find("Pic").text() + '" alt="" style="float:right;zoom:1;overflow:hidden;width:50px;height:50px;margin-left:3px;"/>' +
                        '地址：' + $(v).find("Address").text() +
                        '<input type="hidden" value="' + $(v).find("Address").text() + '" id="curaddress" />' +
                        '<br/>联系人（电话）：' + $(v).find("Tel").text() +
                        '<br/>社区体育指导员：' + $(v).find("Remark").text() +
                        '<br/>体育志愿者：' + $(v).find("Volunteer").text() + '</div>' +
                        '<div class="BMapLib_nav" id="BMapLib_nav0"><ul class="BMapLib_nav_tab" id="BMapLib_nav_tab0"><li class="BMapLib_current" id="BMapLib_tab_tohere0" style="display: block; width: 99px;"><span class="BMapLib_icon BMapLib_icon_tohere"></span>到这里去</li><li class="" id="BMapLib_tab_fromhere0" style="display: block; width: 100px;"><span class="BMapLib_icon BMapLib_icon_fromhere"></span>从这里出发</li></ul><ul class="BMapLib_nav_tab_content"><li id="BMapLib_transBox0"><table width="100%" align="center" border="0" cellpadding="0" cellspacing="0"><tbody><tr><td width="30" style="padding-left:8px;"><div id="BMapLib_stationText0">起点</div></td><td><input id="trans_text" class="BMapLib_trans_text" type="text" maxlength="100" autocomplete="off"></td><td width="106" style="padding-left:7px;"><input id="BMapLib_search_bus_btn0" type="button" value="公交" class="iw_bt" style="margin-right:5px;"><input id="BMapLib_search_drive_btn0" type="button" class="iw_bt" value="驾车"></td></tr></tbody></table></li></ul></div>';
                    var name = $(v).find("Name").text();
                    var strs = new Array();
                    var _point = $(v).find("Coordinate").text();
                    strs = _point.split(",");
                    var point = new BMap.Point(strs[0],strs[1]);
                    var icon = "";
                    addMarker(i,point,name,content,icon);
//                    if (site != "") {
//                        stra.push(point);
//                    }
                    stra.push(point);
                })
//                if (site != "") {
//                    var points = stra;
//                    map.setViewport(points);
//                }
                var points = stra;
                map.setViewport(points);

            }
            else {
                if($(xml).find("DepartCoordi").length > 0) {
                    var strs = new Array();
                    var _point = $(xml).find("DepartCoordi").text();
                    strs = _point.split(",");
                    var point = new BMap.Point(strs[0],strs[1]);
                    map.centerAndZoom(point, 18);
                    map.enableScrollWheelZoom();
                }
                else {
                    var centerpoint = "120.669847,28.019127";
                    map.centerAndZoom(centerpoint, 18);
                    map.enableScrollWheelZoom();
                }
            }
        }
    })
}

function addMarker_street(AreaID,point,_name){
    var _icon = "../images/bgstreet1.png";
    var myIcon = new BMap.Icon(_icon, new BMap.Size(26,36));
    var marker = new BMap.Marker(point, {icon:myIcon}); //创建marker对象
    marker.setTitle(_name);  //
    //var marker = new BMap.Marker(point); //创建marker对象
   // marker.enableDragging(); //marker可拖拽
    marker.addEventListener("click", function(e){
        ajaxMap1(AreaID,"","");

    })
    map.addOverlay(marker); //在地图中添加marker
    //addLabel1(point,_name,AreaID);
}
function addMarker_street1(AreaID,point,_name){
    var _icon = "../images/2.gif";
    var myIcon = new BMap.Icon(_icon, new BMap.Size(30,29));
    var marker = new BMap.Marker(point, {icon:myIcon}); //创建marker对象
    marker.setTitle(_name);  //
    //var marker = new BMap.Marker(point); //创建marker对象
    //marker.enableDragging(); //marker可拖拽
    marker.addEventListener("click", function(e){
        ajaxMap1(AreaID,"","");
    })
    map.addOverlay(marker); //在地图中添加marker
    //marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
    //addLabel1(point,_name,AreaID);
}
function addLabel1(point,_name,AreaID) {
    var opts = {
        position : point,    // 指定文本标注所在的地理位置
        offset   : new BMap.Size(-30, 0)    //设置文本偏移量
    }
    var label = new BMap.Label(_name, opts);  // 创建文本标注对象
    label.setStyle({
        color : "gray",
        fontSize : "10px",
        height : "20px",
        lineHeight : "20px",
        padding : "0",
        fontFamily:"微软雅黑",
        border:"0",
        background:"transparent"
    });
    map.addOverlay(label);
    label.addEventListener("click", function(e){
        ajaxMap1(AreaID,"","");
    })
}
//!function(){
//    var busSearch = {
//        params :{
//            "ID":"BusSearchSta"
//        },
//        init: function(){
//            //这里写方法
//
//        }
//    }
//}();