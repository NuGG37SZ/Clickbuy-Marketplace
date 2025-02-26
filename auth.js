const redirectSignFormBtn = document.getElementById('redirect-sign');
const redirectRegFormBtn = document.getElementById('redirect-reg');
const authSection = document.querySelector('.auth-section');
const regSection = document.querySelector('.registration-section');

redirectSignFormBtn.addEventListener('click', () => {
    regSection.style.display = 'none';
    authSection.style.display = 'block';
})

redirectRegFormBtn.addEventListener('click', () => {
    authSection.style.display = 'none';
    regSection.style.display = 'block';
})

