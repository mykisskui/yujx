var nav = document.getElementById('topnav'), article = document.getElementsByTagName('article')[0];
nav.style.cssText = 'width:'+(article.clientWidth - 80 )+'px;position:fixed;top:0;z-index:100;';
