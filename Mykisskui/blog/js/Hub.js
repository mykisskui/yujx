$(function () {

    var MyName = initMyName(''),wsContentText;//name
    var chat = $.connection.ChatHub;
    init(chat);
    $.connection.hub.start().done(function () {
     

        $("#ws_text").bind("keyup", function (event) {

            if (event.keyCode == "13") {
                $("#ws_send").click();
            }
        });
        $("#ws_send").click(function () {
            var text = $("#ws_text").val();
         chat.server.SendMessag(text);
            $("#ws_text").val("");

        });
        $("#ws_UserNameValue").change(function () {
            var _self = this;
            console.log(_self);
            _self.value.trim() != "" && chat.server.Change(_self.value);
            
        });
      

    });

 

});

function initMyName(MyName) {
          for (var i = 0; i < 6; i++) {
        MyName += Math.floor(Math.random() * 10) + "";
          }
          return MyName;
}
//init the client method
function Append(data) {
    data = JSON.parse(data);
    wsContentText = "<div>" +
    "<div class=\"" + data.Hubs + "\">" +
     "   <span>" + data.Name + "</span>" +
    "    <span>" + data.Time + "</span>" +
   " </div>" +
  "  <div>" +
 "       <span >" + data.content + "</span>" +
"    </div>" +
" </div>";
    ws_content.innerHTML += wsContentText;
    ws_content.scrollTop = ws_content.scrollHeight;
}


  

function init(chat) {
    chat.client.talk = function (data) {
        Append(data);
    }
}

