


var IndexMain = {
    dom:document,
    /*
       数据
       @param a 数据
       @param b 载体元素
    */
    list: function (a, b, c) {
        if (a == '') { return false; }
        var _result = '';
        a = JSON.parse(a);
        var time,text = '';
        //var test = /([^src="]*?\.jpg)/gi;
        //var img;
        //while (img = test.exec(str)) {
        //  console.log(img[1]);//这里的下标是匹配结果，跟你说的下标不是一回事
        //}
        for (var i = 0; i < a.length; i++) 
        {
            var reg = /\+/g;
            while(reg.test(a[i].Text)){
                a[i].Text = a[i].Text.replace('+', '%20');
            }
            text = decodeURIComponent(a[i].Text);
            reg = /src=[\'\"]?([^\'\"]*)[\'\"]?.[jpg|png]/gi;
            var img,imgs ='',image = '',imagelen = 0 ;
            while (img = reg.exec(text)) {
                imgs += img[0] + "|";
                imagelen += 1;
            }
            time = a[i].Time.replace('/Date(', '').replace(')/', '').trim();
            text = text.replace(/<[^>]+>/g, '');
            text = text.length > 300 ? text.substring(0, 300) : text;
            imagelen = imagelen > 5 && (imagelen = 5);
            for (var q = 0; q < imgs.split('|').length; q++) {
                if (imagelen > 0) {
                    image += '<img  ' + imgs.split('|')[q] + '" />';
                    imagelen -= 1;
                }
            }
            
            _result += template.IndexList.replace('#id', a[i].Id).replace('#title', a[i].Title).replace('#time', GetLocalTime(time)).replace('#Text',text).replace('#Image',image);//.replace('#Text',decodeURIComponent(a[i].Text));
            imgs = '',image = '',imagelen = 0;
        }
        b.innerHTML = _result;
    },
    /*
        导航列表
    */
    naviList:function(a,b,c){
        var result = '',js;
        try {
            a = JSON.parse(a);
            a.forEach(function (a, b, c) {
                if (a.URL === '' || a.URL == null) {
                    a.URL = 'javascript:alert(\'开发中\');';
                }
                result += template.NaviList.replace('#URL', a.URL).replace('#title', a.Name);
            });
        } catch (e) {
            result = '加载失败';
        }
        b.innerHTML = result;
    },
    /*
        @param a 数据
        @param b 载体 
    */
    search: function (a,b) {
        console.log(b);
        if (a == '') { return false; }
        var _result = '';
        a = JSON.parse(a);
        for (var i = 0; i < a.length; i++) {
            var reg = /\+/g;
            while (reg.test(a[i].Text)) {
                a[i].Text = a[i].Text.replace('+', '%20');
            }
            text = decodeURIComponent(a[i].Text);
            reg = /src=[\'\"]?([^\'\"]*)[\'\"]?.[jpg|png]/gi;
            var img, imgs = '', image = '', imagelen = 0;
            while (img = reg.exec(text)) {
                imgs += img[0] + "|";
                imagelen += 1;
            }
            time = a[i].Time.replace('/Date(', '').replace(')/', '').trim();
            text = text.replace(/<[^>]+>/g, '');
            text = text.length > 300 ? text.substring(0, 300) : text;
            imagelen = imagelen > 5 && (imagelen = 5);
            for (var q = 0; q < imgs.split('|').length; q++) {
                if (imagelen > 0) {
                    image += '<img  ' + imgs.split('|')[q] + '" />';
                    imagelen -= 1;
                }
            }

            _result += template.IndexList.replace('#id', a[i].Id).replace('#title', a[i].Title).replace('#time', GetLocalTime(time)).replace('#Text', text).replace('#Image', image);//.replace('#Text',decodeURIComponent(a[i].Text));
            imgs = '', image = '', imagelen = 0;
        }
        b.innerHTML = _result;
    }   
}
IndexMain.dom.getElementById('search').onclick = function (e) {
        var _self = this;
        var value = _self.parentNode.getElementsByTagName('div')[0].getElementsByTagName('input')[0].value;
        $.ajax({
            url: '/you/search',
            type:'post',
            data: { k:value },
            success: function (data) {
                data != '' && (IndexMain.search(data, document.getElementsByClassName('list').item(0).getElementsByTagName('ul')[0]));
            }
        })
}
IndexMain.dom.getElementsByClassName('search').item(0).onkeydown = function (e) {
    if (e.keyCode == 13) {
        IndexMain.dom.getElementById('search').click();
    }
}
///提示
function GetLocalTime(v) {
    if (/^(-)?\d{1,10}$/.test(v)) {
        v = v * 1000;
    } else if (/^(-)?\d{1,13}$/.test(v)) {
        v = v * 1;
    } else {
        alert("时间戳格式不正确");
        return;
    }
    var dateObj = new Date(v);
    var UnixTimeToDate = dateObj.getFullYear() + '/' + (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + ' ' + dateObj.getHours() + ':' + dateObj.getMinutes();//+ ':' + dateObj.getSeconds();
   return UnixTimeToDate;
}
(function ($) {
    $.ajax({
        url: '/you/BookMark',
        type: 'get',
        async:false,
        success: function (data) {
            var result = JSON.parse(data),templateresult ='';
            result.forEach(function (a,b,c) {
                templateresult += template.BookMarkList.replace('#URL', a.URL).replace('#title', a.Title);
            });
            $("#bookmarkList_ul").html(templateresult);
        }
    })
    $('div[class=IndexListContentFindImage]').live('click', function (e) {
        var target = e.target,imageurl,imageheight,imagewidth;
        if (target.localName == 'img') {
            imageurl = target.src;
            var popimage = $('.popIMG'), indexpop = $('.IndexPop');
            var windowHeight = $(window).innerHeight();
            windowHeight = (windowHeight - popimage.height()) / 2;
            popimage.css('margin-top', windowHeight + 'px').css('margin-bottom', windowHeight + 'px');
            imageheight = target.naturalHeight > popimage.height() ? (popimage.height() - 30) : target.naturalHeight;
            imagewidth = target.naturalWidth > popimage.width() ? (popimage.width() - 30) : target.naturalWidth;
            if (imageheight == 0 || imagewidth == 0) {
                alert('图片阵亡了');
                return false;
            }
            popimage.children('img').attr('src', imageurl).css('height', imageheight + 'px').css('width', imagewidth + 'px').css('display', 'block').css('margin', 'auto')
            .css('margin-top', (popimage.height() - imageheight) / 2 + 'px');
            indexpop.show();//true

        }
    });
    $('.IndexPop,.popIMG > div').live('click', function (e) {
        var target = e.target;
        console.log(target.className);
        if (target.className === 'IndexPop' || target.className.indexOf('IMGclose') != -1) {
            $('.IndexPop').hide();
        }
    });

    $(window).load(function () {
        var listHeight = $('.list').height();
        $('.right_content').css('height', listHeight);
    });
    


})(jQuery);

   
/*
    onmouseover 当鼠标移动到目标上时触发
    onmouseout 当鼠标离开目标时触发
*/
document.getElementById('indexListul').onmouseover = function (e) {
    var element,target;
    var whileTrueFalse = true;
    target = e.target;
        while (whileTrueFalse) {
            if (target.localName == 'li') { //只需要li
                whileTrueFalse = false;//停止循环
                element = target;
                element.style.cssText = 'cursor:pointer;padding:5px 10px;';//稍后修改为class
                element.onmouseout = function (e) {
                    this.style.cssText = 'border:none';//
                }
                //由于css样式影响会触碰到ul元素，导致循环无结果出现错误
            } if (target.localName == 'html') {
                whileTrueFalse = false;//停止循环
            } else {
                target = target.parentNode;//获取当前元素上级
            }
        }
}