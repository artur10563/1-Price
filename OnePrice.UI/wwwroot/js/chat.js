"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var chat = document.getElementById("chatInput").value;
var sendButton = document.getElementById("sendButton");
var messagesList = document.getElementById("messagesList");
var input = document.getElementById("messageInput");
var userId = document.getElementById("senderId").value;

sendButton.disabled = true;



connection.on("ReceiveMessage", function (message, date, authorId) {
    var dt = new Date(date);
    var formattedDate = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;

    var li = document.createElement("li");
    messagesList.appendChild(li);
    li.className = "pb-1";
    li.innerHTML = `
    <div class="d-flex flex-wrap justify-content-${authorId == userId ? 'end' : 'start'}">
        <div class="${authorId == userId ? 'bg-sender' : 'bg-receiver'} text-white rounded p-2 max-width-75">
            <span>${message}</span>
        </div>
    </div>
    <div class="d-flex flex-wrap justify-content-${authorId == userId ? 'end' : 'start'} mb-2">
        <p class="small m-0 text-${authorId == userId ? 'right' : 'left'} text-muted">${formattedDate}</p>
    </div>
`;


    messagesList.appendChild(li);
    messagesList.scrollTop = messagesList.scrollHeight;
});


connection.start().then(function () {
    sendButton.disabled = false;

    connection.invoke("JoinChat", chat).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

sendButton.addEventListener("click", function (event) {
    var message = input.value;
    input.value = "";

    connection.invoke("SendMessage", chat, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

input.addEventListener('keypress', function (e) {
    if (e.key === 'Enter') {
        sendButton.click();
    }
});


