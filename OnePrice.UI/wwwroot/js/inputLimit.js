var counter = document.getElementById("counter");
var sendButton = document.getElementById("sendButton");
var input = document.getElementById("messageInput");

input.addEventListener("input", () => {
    var count = input.value.length;
    counter.innerText = count + "/4096";
    if (count > 4096) {
        counter.classList.add("text-danger");
        sendButton.disabled = true;
    } else {
        counter.classList.remove("text-danger");
        sendButton.disabled = false;
    }
});