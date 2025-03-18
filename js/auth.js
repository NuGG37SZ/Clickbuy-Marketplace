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
            if(user.isBanned == false) {
                localStorage.setItem('userId', user.id);
                
                requestConfirmPassword(user.login, passwordInput.value)
                    .then(response => {
                        if(response == 200) {
                            localStorage.setItem('isAuth', 'true');
                            location.href='index.html';
                            alert('Вы успешно вошли!');
                        } else {
                            alert('Перепроверьте данные!');
                        }
                    });
            } else {
                alert('Ваш аккаунт заблокирован!');
                return;
            }
        });
})

async function requestGetUserByLogin(login) {
    url = `https://localhost:5098/api/v1/users/getByLogin/${login}`;
    const response = await fetch(url);
    return await response.json();
}

async function requestConfirmPassword(login, password) {
    url = `https://localhost:5098/api/v1/users/authenticate/${login}/${password}`;
    const response = await fetch(url);
    return await response.status;
}
