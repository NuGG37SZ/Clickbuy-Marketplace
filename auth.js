const redirectSignFormBtn = document.getElementById('redirect-sign');
const redirectRegFormBtn = document.getElementById('redirect-reg');
const authSection = document.querySelector('.auth-section');
const regSection = document.querySelector('.registration-section');
const authBtn = document.getElementById('auth-btn');

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

authBtn.addEventListener('click', () => {
    const loginInput = document.getElementById('loginInputAuth');
    const passwordInput = document.getElementById('passwordInputAuth');

    requestGetUserByLogin(loginInput.textContent);
})


function requestGetUserByLogin(login) {
    fetch(`http://localhost:5098/users/getByLogin/${login}`)
        .then(response => response.json())
        .then(user => console.log(user))
}