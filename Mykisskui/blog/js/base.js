var dom = document;
var c = {
    result: false,
    obj:null
}
//弹窗
function message(value, obj) {

    if (dom.getElementById('message') != null) { return false; }
    console.log(obj);
    var div = document.createElement("div");
    div.id = 'message';
    div.style.cssText = "transition:all 0.5s; opacity:1; width:300px;position:absolute;top:0;display:table;top:" + (obj.offsetTop + obj.offsetHeight) + "px;left:" + (obj.offsetLeft - 300 + obj.offsetWidth) + "px; background:rgb(29, 29, 29);border:1px solid #2f2f2f;box-shadow:0 0 5px 1px #000;";
    var div_1 = document.createElement('div');
    div_1.style.cssText = 'color:#fff;padding:10px 10px;text-align:center;font-weight:bold;';
    var div_2 = dom.createElement('div');
    div_2.id = 'div_2';
    div_2.style.cssText = 'display:flex;width:200px;margin:20px auto 10px;';
    var div_2_1 = dom.createElement('div');
    div_2_1.innerText = '确认';
    div_2_1.setAttribute('data-id', obj.children[0].getAttribute('data-id'));
    div_2_1.setAttribute('data-type', obj.children[0].getAttribute('data-type'));
    div_2_1.id = 'confirm' + obj.children[0].getAttribute('data-aim');
    div_2_1.style.cssText = 'float: left;color: #272626;padding: 0px 10px;background: #fff;font-size: 14px;line-height: 2;font-weight: bold;border-radius: 5px;box-shadow: 0 0 5px 1px #fff;width:75px;cursor:pointer;';
    var div_2_2 = div_2_1.cloneNode(true);
    div_2_1.style.margin = "0 25px 0 0 ";
    div_2_2.innerText = '取消';
    div_2_2.id = 'cancel';
    div_1.innerText = value;
    div_2.appendChild(div_2_1);
    div_2.appendChild(div_2_2);
    div_1.appendChild(div_2);
    div.appendChild(div_1);
    document.body.appendChild(div);
}
//close弹窗
function messageRemove() {
    dom.getElementById('message').style.left = '-400';
    dom.getElementById('message').style.opacity = '0';
    setTimeout(function () { dom.getElementById("message").remove()}, 500);
   
}
$('#div_2').live('click', function (e) {
    var target = e.target;
    var _url = '/' + target.getAttribute('data-id') + '/' + target.getAttribute('data-type') + '';
    console.log(_url);
    switch (target.id+"") {
        case 'confirmenable':
            c.result = true;
            $.ajax({
                url: '/Admin/enable' + _url,
                type: 'get',
                success: function (data) {
                    console.log(data);
                    if (Number(data) == 0) {
                        c.obj.getAttribute('data-status') == 'True' ? (c.obj.setAttribute('data-status', 'False')) : (c.obj.setAttribute('data-status', 'True'));
                    } else {
                        alert('error!');
                    }
                }
            });
            break;
        case 'confirmremove':
            $.ajax({
                url: '/Admin/remove' + _url,
                type: 'get',
                success: function (data) {
                    if (Number(data) > 0) {
                        var parent = c.obj.parentNode.parentNode;
                        var tr = parent.cloneNode(true);
                        console.log(parent.offsetTop);
                        tr.className = 'transition';
                        tr.style.cssText = 'position:absolute;top:' + (parent.offsetTop + 2) + 'px;left:' + parent.offsetLeft + 'px;border:none; transition:all 1s;';
                        dom.getElementsByTagName('table')[0].getElementsByTagName('tbody')[0].appendChild(tr);
                        parent.style.visibility = 'hidden';
                        setTimeout(function () { tr.style.left = '-2000px'; }, 50);
                        setTimeout(function () {
                            tr.remove();
                            parent.remove();
                        }, 550);

                    } else {
                        alert('error!');
                    }
                }
            });
            break;
        case 'cancel':
            c.result = false;
            break;
        default:
            console.log('error!');
            break;
    }
    messageRemove();
});
$('*[data-status]').click(function (e) {

    console.log(e.target.offsetParent.offsetTop);
    var self = this, messageText;
    c.obj = self;
    var status = self.getAttribute('data-status');
    switch (status) {
        case 'True':
            messageText = '确定隐藏吗?';
            break;
        case 'False':
            messageText = '确定显示吗?';
            break;
        case 'remove':
            messageText = '确定删除吗?';
            break;
    }
    //创建弹窗
    message(messageText, e.target.offsetParent);
    });
