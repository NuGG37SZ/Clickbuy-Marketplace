 // Открытие и закрытие панели чата
 const openChatBtn = document.getElementById('openChatBtn');
 const closeChatBtn = document.getElementById('closeChatBtn');
 const chatPanel = document.getElementById('chatPanel');

 openChatBtn.addEventListener('click', () => {
     chatPanel.style.display = 'block';  
 });

 closeChatBtn.addEventListener('click', () => {
     chatPanel.style.display = 'none'; 
 });