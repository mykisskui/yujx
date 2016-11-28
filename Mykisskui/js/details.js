var DetailsMain = {
    /*
        @param a 数据
        @param b 载体
    */
    content: function (a,b) {
        if (a == '') { return false; }
        a = JSON.parse(a);
        var reg = /\+/g;
        while (reg.test(a.Text)) {
           a.Text =  a.Text.replace('+', '%20');
        }
        console.log(a.Text);
     b.innerHTML = decodeURIComponent(a.Text);
    }
}