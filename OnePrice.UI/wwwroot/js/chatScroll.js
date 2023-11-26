var messagesList = document.getElementById('messagesList');
var sendButton = document.getElementById("sendButton");

document.addEventListener("DOMContentLoaded", function () {
    messagesList.scrollTop = messagesList.scrollHeight;

    sendButton.addEventListener("click", function () {
        messagesList.scrollTop = messagesList.scrollHeight;
    });
});
