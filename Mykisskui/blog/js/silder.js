/*导航当前页高亮*/
var obj=null;
var As=document.getElementById('topnav').getElementsByTagName('a');
obj = As[0];
for(i=1;i<As.length;i++){if(window.location.href.indexOf(As[i].href)>=0)
obj=As[i];}
obj.id='topnav_current'

///*百度分享广告*/
//window._bd_share_config={"common":{"bdSnsKey":{},"bdText":"","bdMini":"1","bdMiniList":false,"bdPic":"","bdStyle":"1","bdSize":"32"},"share":{}};with(document)0[(getElementsByTagName('head')[0]||body).appendChild(createElement('script')).src='http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion='+~(-new Date()/36e5)];




//随机赋值颜色
 
var newsObj = {},children,news_a;
newsObj.newsLayout = function (a, b, c) {
    children = a.children;
    children[b].style.cssText = 'background:' + newsObj.randomCol() + ';';
    news_a = children[b].getElementsByTagName('a')[0];
    news_a.style.cssText = 'top:' + ((children[b].clientHeight - (news_a.clientHeight > 30 ? 30 : news_a.clientHeight)) / 2) + 'px;';
    console.log(news_a.clientHeight);
    return ((children.length -1) == b ? 0 :newsObj.newsLayout(a, b+1, c));
}

var news = document.getElementsByClassName('news_layout');
newsObj.randomCol = function() {
    var r = Math.floor(Math.random() * 255);
    var g = Math.floor(Math.random() * 255);
    var b = Math.floor(Math.random() * 255);
    return  'rgb(' + r + ',' + g + ',' + b + ')';
}
newsObj.color = function () {
    var news_div, news_div_width, news_clientwidth, _indexof
    for (var i = 0; i < news.length; i++) {
        a = newsObj.newsLayout(news.item(i),0,news.length);
    }
    news = document.getElementsByClassName('news').item(0);
      news_div = news.getElementsByTagName('div')[0];
      news_div_width = news_div.getElementsByTagName('div')[0].clientWidth;
      news_clientwidth = news.clientWidth / news_div_width;
      _indexof = news_clientwidth.toString().indexOf('.');
      news_div_width = _indexof > 0 ? (news_clientwidth.toString().substring(0, _indexof)) * news_div_width : (news_clientwidth * news_div_width);
      news_div.style.cssText = 'width:' + news_div_width + 'px;';
}
window.onresize = function () {
    newsObj.color();
}
newsObj.color();
