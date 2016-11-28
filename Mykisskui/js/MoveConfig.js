
var dom = document;


var config = {

    clone: null,
    viLayou: null,
    TopAddCount:0,
    moveLayout: dom.getElementById("moveLayout"),
    moveLayoutscrollTop: 0,
    LayoutContent: new Array(),
    LayoutLen:[],
    init: function () {
        config.moveLayout.innerHTML = "";
        config.viLayou = null;
        config.LayoutContent.forEach(function (a, b, c) {
            config.moveLayout.appendChild(a);
            //按照下标顺序记录所有模块坐标
            config.LayoutLen[b] = {top:a.offsetTop,left:a.offsetLeft,width:a.clientWidth,height:a.clientHeight};
        });
        config.moveLayout.scrollTop = config.moveLayoutscrollTop;
    }

}
window.onload = function () {
    config.moveLayout.onscroll = function () {
        config.moveLayoutscrollTop = this.scrollTop;
    }

    dom.getElementById('move_UL').onmousedown = function (e) {
        var target = e.target, modulename, self = this,qAction;
        if (target.getAttribute("data-type") == "module") {
            modulename = target.getAttribute("data-name");//模块名称
            var cloneTarget = target;
            AddModule(target.parentNode, self);
            //获取克隆的对象
            var clone = config.clone;
            clone = b(e, clone);
            config.moveLayout.style.position = "relative";
            clone.onmousemove = function (e) {//移动/////////////////////////////////////////////////////////////////////////////////////////////
                b(e, clone);
                var _top = [],result;
                if (MoveLayoutTopLeft(config.moveLayout, e.clientX, e.clientY)) {
                    if (config.viLayou === null) {
                        //创建虚拟模块添加到容器内部
                        result = htmlModule.search(modulename);
                        config.viLayou = result;
                    }
                    config.moveLayout.scrollTop = config.moveLayoutscrollTop;
                    result = d(e.clientX, e.clientY, result);
                    try {
                        config.LayoutContent.forEach(function (a, b, c) {
                            _top[b] = a.offsetTop;
                        });
                            qAction = q(result.a, result.site,config.viLayou);

                            if (qAction.exist) {
                                console.log(qAction.exist);
                                for (var i = 0; i < _top.length; i++) {
                                    var a = config.LayoutContent[i];
                                    a.style.cssText = "position:absolute;top:" + _top[i] + ";left:" + a.offsetLeft + ";";
                                    if (result.site == 1) {
                                        if (result.a.offsetTop < a.offsetTop) {
                                            a.style.top = _top[i] + result.a.offsetTop;
                                        }
                                    }
                                }
                            } else {
                                console.log(false);
                            }
                    } catch (e) {
                        console.log('error');
                    }
                   
                    try {
                        //根据提供的数据创建虚拟模块
                        //首先空出一个模块的区域

                    } catch (e) {
                            //error
                    }
                  
                }
            }
            dom.getElementsByClassName("Main").item(0).onmouseup = function (e) {//释放////////////////////////////////////////////////////////////////////////////

                if (config.clone === null) return false;
                RemoveModule();
                var result = MoveLayoutTopLeft(config.moveLayout, e.clientX, e.clientY);
                if (result) {
                   //result = htmlModule.search(modulename); //测试使用
                    //config.moveLayout.appendChild(result);
                                     ///////////////////////////
                                            config.moveLayout.style.position = "";
                                            config.LayoutContent.forEach(function (a, b, c) {
                                                a.style.cssText = "";
                                            });
                                     ///////////////////////////
                        if (config.LayoutContent.length > 0) {
                            result = d(e.clientX, e.clientY, config.viLayou);
                            if (Arraysite(result.a) < 0) {
                                c(result.data);
                            }
                            else {
                                config.LayoutContent.splice(result.site === 0 ? Arraysite(result.a) : (Arraysite(result.a) + 1), 0, result.data);
                            }
                        } else {
                            result = config.viLayou;
                            c(result);
                        }

                        //初始化
                        config.init();
                }
            }
        } else {
            return false;
        }
    }
}
/*
    创建虚拟模块
    param a 被上的模块
    param b 坐标
    param c 是否被上
*/
var q_obj =null, q_site, q_exist = false;
function q(a, b,c) {
    if (a == q_obj) {//是同一模块
        if (q_site == b) {
            //true
            q_exist = false;
        } else {
            //false
            q_site = b;
            q_exist = true;
        }
    } else if (typeof a != "undefined") {
        console.log('这里是' + a);
        q_obj = a;
        q_site = b;
        q_exist = true;
    }
    return { site: q_site, exist: q_exist, obj: q_obj };
}
/*
    param a 克隆的对象
    param b 克隆上级元素
    param c 克隆数量：1
*/
function AddModule(a, b, c) {
    if (config.clone === null) {
        var cloneNode = a.cloneNode(true);
        b.appendChild(cloneNode);
        config.clone = cloneNode;
    }
}
/*
    删除克隆
*/
function RemoveModule() {
    config.clone.remove();
    config.clone = null;
}
/*
    释放时计算是否在布局内
    param a 布局元素
    param b 鼠标当前位置x
    param c 鼠标当前位置y
*/
function MoveLayoutTopLeft(a, b, c) {
    var left = a.offsetLeft + a.clientWidth;
    var top = a.offsetTop + a.clientHeight;
    if (b > a.offsetLeft && b < left && c > a.offsetTop && c < top) {
        return true;
    } else {
        return false;
    }
}
/*
   param a 对象
   param b 克隆对象
*/
function b(a, b, c) {
    var x = a.clientX;
    var y = a.clientY;
    var cloneWidth = b.clientWidth;
    var cloneHeight = b.clientHeight;
    b.style.cssText = "  top:" + (y - cloneHeight / 2) + ";left:" + (x - cloneWidth / 2) + "; position:absolute; width:" + cloneWidth + "px; z-index:9999; border:1px solid #000; ";
    return b;
}
/*
    记录被拖拽的数量
*/
function c(a) {
    config.LayoutContent.push(a);
    return config.LayoutContent.length;
}
/*
    获取数组内指定下标
*/
function Arraysite(a) {
    var result = -1;
    for (var i = 0; i < config.LayoutContent.length; i++) {
        if (config.LayoutContent[i] === a) {
            result = i;
            break;
        }
    }
    return result;
}
/*
    计算容器内元素坐标
*/
function d(x, y, d) {
    if (config.moveLayout.scrollTop > 0) {
        y = y + config.moveLayout.scrollTop;
    }
    var result, top, clientheight,item,itemoffsetTop,itemoffsetLeft;
    for (var i = 0; i < config.LayoutContent.length; i++) {
        //查找符合鼠标坐标的元素
        item = config.LayoutContent[i];
        itemoffsetTop = config.LayoutLen[i].top;
        itemoffsetLeft = config.LayoutLen[i].left;
        top = config.moveLayout.scrollTop > 0 ? (config.moveLayout.scrollTop + itemoffsetTop) : itemoffsetTop;
        clientheight = config.moveLayout.scrollTop > 0 ? (config.moveLayout.scrollTop + item.clientHeight + itemoffsetTop) : (itemoffsetTop + item.clientHeight);
        if (x > itemoffsetLeft && x < (itemoffsetLeft + item.clientWidth) && y > itemoffsetTop && y < (itemoffsetTop + item.clientHeight)) {
            result = item;
        }
    }
    //计算是在上还是下
    try {
   
        var height = (result.offsetTop + (result.clientHeight + result.offsetTop)) / 2;
        var site;

        if (parseInt(y) > parseInt(height)) {
            console.log("next!");
            site = 1;
        } else {
            console.log("top!");
            site = 0;
        }
    } catch (e) {

    }
    return { a: result, site: site, data: d };
}
var daohanglanLen = 0;
var htmlModule = {
    search: function (a) {
        var b = eval("htmlModule." + a + "()");
        return b;
    },
    daohanglan: function () {
        daohanglanLen++;
        //创建导航栏
        var a = dom.createElement("div");
        a.className = "testdiv";
        a.innerText = "导航栏" + daohanglanLen;
        return a;
    }

}