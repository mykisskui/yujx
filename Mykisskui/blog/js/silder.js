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
 
var newsObj = {}, children, news_a, val = 0, news_url_div, Naviobject;
newsObj.news = document.getElementsByClassName('news_layout');
newsObj.newsdata;
newsObj.newsLayoutTopAndLeft = [];
newsObj.newsLayout = function (a, b, c,d) {
    children = a.children;
    children[b].style.cssText = 'background:' + newsObj.randomCol() + ';';
    news_a = children[b].getElementsByTagName('a')[0];
    news_a.className = 'news_url_a';
    //news_a.style.cssText = 'top:' + ((children[b].clientHeight - (news_a.clientHeight > 30 ? 30 : news_a.clientHeight)) / 2) + 'px;';
    return ((children.length - 1) == b ? 0 : newsObj.newsLayout(a, b + 1, c));

}

var news = document.getElementsByClassName('news_layout');
newsObj.randomCol = function() {
    var r = Math.floor(Math.random() * 255);
    var g = Math.floor(Math.random() * 255);
    var b = Math.floor(Math.random() * 255);
    return  'rgb(' + r + ',' + g + ',' + b + ')';
}
newsObj.News_ajax = function () {
    var result = '';
    $.ajax({
        url: '/blog/baiduNews',
        type: 'get',
        //   async:false,
        beforeSend: function () {
            
        },
        complete: function () {
            news_loading.style.cssText = '';
            news_loading.children[0].style.cssText = '';
        },
        success: function (data) {
            newsObj.newsdata = JSON.parse(data);
            news_url_div = document.createElement('div');
            news_url_div.id = 'div';
            var news_url_a = document.getElementsByClassName('news_url_a');
            for (var i = 0; i < news_url_a.length; i++) {
                news_url = 'http://news.baidu.com/ns?tn=news&word=' + newsObj.newsdata.data[i].query_word;
                news_url_a[i].href = news_url;
                news_url_a[i].innerText = newsObj.newsdata.data[i].query_word;
                news_url_a[i].target = '_blank';
                news_url_a[i].title = newsObj.newsdata.data[i].query_word;
                news_url_a[i].style.cssText = 'top:' + ((news_url_a[i].parentNode.clientHeight - news_url_a[i].clientHeight) / 2) + 'px;';
                news_url_a[i].parentNode.innerHTML += '<div class="news_url_div" >'+
                '<a style="height:' + (news_url_a[i].parentNode.clientHeight / 2) + 'px;padding:' + (news_url_a[i].parentNode.clientHeight / 2) + 'px 0 0 0;" href="' + news_url + '" target="_blank">' + (newsObj.newsdata.data[i].desc == '' ? newsObj.newsdata.data[i].query_word : newsObj.newsdata.data[i].desc) + '</a></div>';
            }
        }
    })
    return result;
}
newsObj.color = function () {
    var news_div, news_div_width, news_clientwidth, _indexof, news_div_width, TopAndLeftIndex = 0;
    for (var i = 0; i < news.length; i++) {
        a = newsObj.newsLayout(news.item(i),0,news.length);
    }
    news = document.getElementsByClassName('news').item(0);
    news_div = news.getElementsByTagName('div')[0];
    news_div_Layout = news_div.getElementsByTagName('div');
    news_div_width = news_div.clientWidth / 3;
    _indexof = news_div_width.toString().indexOf('.');
    news_div_width = _indexof > 0 ? news_div_width.toString().substring(0, _indexof) : news_div_width;
    for (var i = 0; i < news_div_Layout.length; i++) {
        if (news_div_Layout[i].className == 'news_layout') {
            //위치설정
            news_div_Layout[i].style.cssText = 'z-index:' + i + ';width:' + (news_div_width) + 'px;height:' + news_div_width + 'px;';
            news_div_Layout[i].style.cssText += 'top:' + news_div_Layout[i].offsetTop + 'px;left:' + news_div_Layout[i].offsetLeft + 'px;';
            newsObj.newsLayoutTopAndLeft[TopAndLeftIndex] = { name: news_div_Layout[i], top: news_div_Layout[i].offsetTop, left: news_div_Layout[i].offsetLeft };
            console.log(i + ":" + news_div_Layout[i].offsetTop);

            TopAndLeftIndex++;
        }
        switch (news_div_Layout[i].className) {
            case 'news_layout_1':
                news_div_Layout[i].style.width = '' + (news_div_width - 8) + 'px';
                news_div_Layout[i].style.height = '' + (news_div_width - 4) + 'px';
                break;
            case 'news_layout_2':
                news_div_Layout[i].style.width = '' + (news_div_width - 8) + 'px';
                news_div_Layout[i].style.height = '' + (news_div_width /2 -4)  + 'px';
                break;
            case 'news_layout_3':
                news_div_Layout[i].style.width = '' + (news_div_width /2 -8)  + 'px';
                news_div_Layout[i].style.height = '' + (news_div_width /2 -4)  + 'px';
                break;
        }
    }
    news.style.cssText = 'height:' + news.clientHeight + 'px;';
        newsObj.newsLayoutPosition();
}

newsObj.naviClick = function (e) {
    var _target = e.target, _f = true;
    while (_f) {
        if(_target.localName =='html'){
            _f = false;
        } else if (_target.localName != 'li') {
            _target = _target.parentNode;
        } else {
            _f = false;
        }
    }
    console.log(_target);
    if (Naviobject == _target) {
        //false
    } else {
        try {
            Naviobject.className = Naviobject.className.replace('news_navi_li_on', '');
        } catch (e) {

        }
        Naviobject = _target;

        _target.className = 'news_navi_li_on';
    }

    var RandomValue = 6, TopAndLeftValue, RandomExist = [];
    for (var i = 0; i < RandomValue; i++) {
        TopAndLeftValue = Math.floor(Math.random() * RandomValue);
        if (RandomExist.length >= 6) {
            break;
        } else if (RandomExist.indexOf(TopAndLeftValue) != -1) {
             i = -1;
        } else {
            RandomExist.push(TopAndLeftValue);
        }
    }
    TopAndLeftValue = 0;
    newsObj.newsLayoutTopAndLeft.forEach(function (a, b, c) {
        a.name.style.top = '' + c[RandomExist[TopAndLeftValue]].top + 'px';
        a.name.style.left = ''+c[RandomExist[TopAndLeftValue]].left+'px';
        TopAndLeftValue++;
    });
}
newsObj.newsLayoutPosition = function () {//위치 설정
    for (var i = 0; i < newsObj.news.length; i++) {
        //top,left 설정
      newsObj.news[i].style.position = 'absolute';
      newsObj.news[i].className += ' news_layout_transition';
    }
}
window.onresize = function () {
    newsObj.color();
}
window.onload = function () {
    document.getElementById('news_navi_ul').addEventListener('click', newsObj.naviClick, false);
    newsObj.News_ajax();
}
newsObj.color();
news_loading.style.cssText = 'height:' + ((news_loading.parentNode.clientWidth / 6)) + 'px;width:' + (news_loading.parentNode.clientWidth / 6) + 'px; padding:' + (news_loading.parentNode.clientHeight - 20 - (news_loading.parentNode.clientWidth / 6)) / 2 + 'px ' + (news_loading.parentNode.clientWidth - (news_loading.parentNode.clientWidth / 6)) / 2 + 'px;';
news_loading.children[0].style.cssText = 'opacity:.9;font-size:' + (news_loading.parentNode.clientWidth / 6) + 'px; ';