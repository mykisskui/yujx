

var template = {
 IndexList : [
         '<li>'+
         '<a target="_blank" href="/you/details/#id">#title</a>' +
         '<span>' +
            '#time'+
         '</span>' +
         '<div class="IndexListContent">' +
         '<div class="IndexListContentFindImage">' +
         //此处动态编写内容
         '#Image'+
         '</div>' +
         '#Text'+
         '</div>'+
         '</li>'
 ].join('/n')
    ,
 NaviList: [
            '<li>'+
                   '<a href="#URL" >#title</a>'+
                    '</li>'
 ].join('/n'),
 BookMarkList:['<li>'+
               '<div><a target="_blank" href="#URL">#title</a></div>',
               '</li>'
 ].join(''),
 phonedetail: ['<div class="detailTitle">' +
               '#title'+
                '</div>'+
                 '<div class="detailTime">'+
                  ' #time'+
                   '</div>'+
                    '<div class="detailcontent">'+
                     '#content'+
                      '</div>'].join('')
}