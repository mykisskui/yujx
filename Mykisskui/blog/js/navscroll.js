var ua = navigator.userAgent;
var nav = document.getElementById('topnav'), article = document.getElementsByTagName('article')[0];
var ipad = ua.match(/(iPad).*OS\s([\d_]+)/),
    isIphone = !ipad && ua.match(/(iPhone\sOS)\s([\d_]+)/),
    isAndroid = ua.match(/(Android)\s+([\d.]+)/),
    isMobile = isIphone || isAndroid;
if (isMobile) {
    document.getElementsByTagName('header')[0].style.cssText = 'height:' + (nav.clientHeight) + 'px;';
} else {


    if (document.getElementsByTagName('aside')[0].clientWidth > 0 ) {
    nav.style.cssText = 'width:' + (article.clientWidth - 80) + 'px;position:fixed;top:0;z-index:100;';
    } else {

        nav.style.cssText = 'width:' + (article.clientWidth ) + 'px;position:fixed;top:0;z-index:100;';
    }

}

