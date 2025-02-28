document.addEventListener("DOMContentLoaded", function() {
    const openChatBtn = document.getElementById("openChatBtn");
    const closeChatBtn = document.getElementById("closeChatBtn");
    const chatPanel = document.getElementById("chatPanel");
    const chatbox = document.getElementById("chatbox");

    // Открытие чата
    openChatBtn.addEventListener("click", function() {
        chatbox.classList.add("hidden");
        chatPanel.classList.add("open");
    });

    // Закрытие чата
    closeChatBtn.addEventListener("click", function() {
        chatPanel.classList.remove("open");
        setTimeout(() => chatbox.classList.remove("hidden"), 400);
    });

    // Есть ли оператор?
    const operatorPhoto = document.querySelector(".support-photo img");
    const operatorName = document.getElementById("operatorName");
    const operatorRole = document.getElementById("operatorRole");

    let isOperatorOnline = false;

    if (!isOperatorOnline) {
        operatorPhoto.src = "source/robot.png";
        operatorName.textContent = "Бот";
        operatorRole.textContent = "";
    }
});
