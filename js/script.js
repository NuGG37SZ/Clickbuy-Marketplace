const signUpBtn = document.getElementById('sign-up-btn');
const profile = document.getElementById('profile');
const exitBtn = document.getElementById('exit-btn');

document.addEventListener("DOMContentLoaded", ()  => {
    hideLinkForAuthorizedUsers();
})

exitBtn.addEventListener('click', () => {
    localStorage.removeItem('isAuth');
    localStorage.removeItem('userId');
    hideLinkForAuthorizedUsers();
    alert('Вы успешно вышли!');
    location.href='index.html';
})

function hideLinkForAuthorizedUsers() {
    if(localStorage.getItem('isAuth') === 'true') {
        profile.style.display = 'block';
        signUpBtn.style.display = 'none';
        exitBtn.style.display = 'block';
    } else {
        profile.style.display = 'none';
        signUpBtn.style.display = 'block';
        exitBtn.style.display = 'none';
    }
}