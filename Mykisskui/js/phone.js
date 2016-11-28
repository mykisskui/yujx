window.onload = function () {
   
    var table = document.getElementsByClassName('table').item(0), ul = document.getElementsByClassName('classTop').item(0).getElementsByTagName('ul')[0], floatparent = document.getElementById('floatparent'), floatsub = document.getElementById('floatsub');
    navi = JSON.parse(navi);
    var ulHTML = "",tableHTML = "";
    for (var i = 0; i < navi.length; i++) {
    
        ulHTML += '<li class="'+(i==0 ? "classTopLi_on" : "")+'">' + navi[i].Name + '</li>';
    }
    data = JSON.parse(data);
    for (var i = 0; i < data.length; i++) {
        var reg = /\+/g;
        while (reg.test(data[i].Text)) {
            data[i].Text = data[i].Text.replace('+', '%20');
        }
        text = decodeURIComponent(data[i].Text);
        text = text.replace(/<[^>]+>/g, '');
        text = text.length > 100 ? text.substring(0, 100) : text;
        tableHTML += '';
        tableHTML += '        <div class="list" data-id="'+data[i].Id+'"><div class="title">'+data[i].Title+'</div>' +
                     '<div class="content">' + text + '</div>  </div>';
    }
    ul.innerHTML = ulHTML;
    table.innerHTML = tableHTML;
    document.getElementsByClassName('table').item(0).onclick = function (e) {
        var target = e.target
        var whileTF = true;
        while (whileTF) {
            if (target.className == 'list') {
                whileTF = false;
            } else {
                target = target.parentNode;
            }
            if (target == null) {
                return false;
            }
        }
        detailShow(true,target.getAttribute('data-id'));
    }
    document.getElementsByClassName('detailback').item(0).onclick = function () {
        detailShow(false);
    }
    function detailShow(a, b) {
        var detailtemp = wrapper1.getElementsByClassName('detailDIV').item(0),loadingMargin;
        switch (a) {
            case true:
                //detailtemp.style.cssText = 'height:' + (window.innerHeight - 45) + 'px;';
                setTimeout(function () {
                    detail.style.cssText = 'position:fixed;z-index:99;top:0;left:' + window.innerWidth + 'px;width:100%;height:' + (window.innerHeight) + 'px;-webkit-transform:translate3d(-' + window.innerWidth + 'px,0,0); -webkit-transition:all 500ms cubic-bezier(0.22,0.61,0.36,1)';
                }, 100);

                loadingMargin = window.innerHeight - 45 - 49;
                detailtemp.innerHTML = '<i class="icon-spinner icon-spin icon-3x" style="display:block;text-align:center;margin:'+loadingMargin /2 +'px 0;"></i>';
                setTimeout(function () {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].Id === ~~b) {
                            var reg = /\+/g;
                            while (reg.test(data[i].Text)) {
                                data[i].Text = data[i].Text.replace('+', '#20');
                            }
                            text = decodeURIComponent(data[i].Text);
                            text = text.replace(/<[^>]+>/g, '');
                            detailtemp.innerHTML = template.phonedetail.replace('#title', data[i].Title).replace('#time', GetLocalTime(data[i].Time.replace('/Date(', '').replace(')/', '').trim())).replace('#content', text);
                            detailscroll.refresh();
                        }
                    }
                }, 800);
                //if (detailtemp.clientHeight < window.innerHeight - 40) {
                //    detailtemp.style.height = window.innerHeight - 45 + 'px';
                //}
                break;
            case false:
                detail.style.cssText = 'position:fixed;z-index:99;top:0;left:1000px;-webkit-transform:translate3s(0,0,0);-webkit-transition:all 1000ms cubic-bezier(0.22,0.61,0.36,1)';
                setTimeout(function () {
                    detailtemp.innerHTML = '';
                }, 500);
                break;
        }
    }

    //float js start
    var floatparentTop = floatparent.offsetTop,
        floatparentLeft = floatparent.offsetLeft,
        floatsubArray = [];
    floatsub.childNodes.forEach(function (a, b, c) {
        if (b % 2) {
            floatsubArray.push(a);
        }
    
    });
   

    //float js end
  



    function GetLocalTime(v) {
        if (/^(-)?\d{1,10}$/.test(v)) {
            v = v * 1000;
        } else if (/^(-)?\d{1,13}$/.test(v)) {
            v = v * 1;
        } else {
            alert("error");
            return;
        }
        var dateObj = new Date(v);
        var UnixTimeToDate = dateObj.getFullYear() + '-' + (dateObj.getMonth() + 1) + '-' + dateObj.getDate() + ' ' + dateObj.getHours() + ':' + dateObj.getMinutes();//+ ':' + dateObj.getSeconds();
        return UnixTimeToDate;
    }
}
