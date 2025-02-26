const redirectSignFormBtn = document.getElementById('redirect-sign');
const redirectRegFormBtn = document.getElementById('redirect-reg');
const authSection = document.querySelector('.auth-section');
const regSection = document.querySelector('.registration-section');

redirectSignFormBtn.addEventListener('click', () => {
    swapDisplay(regSection, authSection);
})

redirectRegFormBtn.addEventListener('click', () => {
    swapDisplay(authSection, regSection);
})

function swapDisplay(first, second) {
    first.style.display = 'none';
    second.style.display = 'block';
}