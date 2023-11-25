"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var chat = document.getElementById("chatInput").value;
var sendButton = document.getElementById("sendButton");
var messagesList = document.getElementById("messagesList");
var input = document.getElementById("messageInput");

sendButton.disabled = true;

connection.on("ReceiveMessage", function (nickname, message, date) {
    var dt = new Date(date);
    var formattedDate = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;

    var li = document.createElement("li");
    messagesList.appendChild(li);
    li.className = "pb-1 list-group-item";
    li.innerHTML = `
    <div class="d-flex justify-content-between align-items-center">
        <span>${nickname}: ${message}</span>
        <p class="small">${formattedDate}</p>
    </div>
`;
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