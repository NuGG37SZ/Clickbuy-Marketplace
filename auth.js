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
    
    requestGetUserByLogin(loginInput.value)
        .then(user => {
            localStorage.setItem('userLogin', user.login);
            
            requestConfirmPassword(passwordInput.value, user.password)
                .then(response => {
                    if(response == 200) {
                        localStorage.setItem('isAuth', 'true');
                        location.href='index.html';
                        alert('Вы успешно вошли!');
                    } else {
                        alert('Перепроверьте данные!');
                    }
                });
        });
})

async function requestGetUserByLogin(login) {
    url = `http://localhost:5098/api/v1/users/getByLogin/${login}`;
    const response = await fetch(url);
    return await response.json();
}

async function requestConfirmPassword(password, hashPassword) {
    url = `http://localhost:5098/api/v1/users/passwordСomparison/${password}/${hashPassword}`;
    const response = await fetch(url);
    return await response.status;
}
