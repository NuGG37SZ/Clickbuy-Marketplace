const redirectSignFormBtn = document.getElementById('redirect-sign');
const redirectRegFormBtn = document.getElementById('redirect-reg');
const authSection = document.querySelector('.auth-section');
const regSection = document.querySelector('.registration-section');
const authBtn = document.getElementById('auth-btn');
const enterBtn = document.getElementById('enter-btn');
const codeMail = document.getElementById('code-mail');
let code;

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

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function postRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(obj)
    });
    let result = await response.status;
    return result;
}

function generateCode() {
    let randomCode = Math.round((Math.random() * 10) * 10000000);
    if(randomCode.toString().length < 8) {
        randomCode *= 10;
    }
    return randomCode.toString();
}

enterBtn.addEventListener('click', async () => {
    const loginInput = document.getElementById('loginInputAuth');
    let user = await getUserByLogin(loginInput.value);
    code = generateCode();

    let message = {
        to: [user.email],
        bcc: ["Anezer20@yandex.ru"],
        cc: ["Anezer20@yandex.ru"],
        from: "Anezer20@yandex.ru",
        displayName: "ClickBuy",
        replyTo: "",
        replyToName: "ClickBuy",
        subject: "Код для входа в аккаунт",
        body: `<p style="font-size: 20px"><b>Ваш код входа - ${code}</b></p>`,
    }

    await postRequest(`https://localhost:7160/api/v1/notificationMail/sendmail`, message);
})

authBtn.addEventListener('click', async () => {
    const loginInput = document.getElementById('loginInputAuth');
    const passwordInput = document.getElementById('passwordInputAuth');
    let user = await getUserByLogin(loginInput.value);

    if(user.isBanned == false) {
        let codeRequest = await requestConfirmPassword(user.login, passwordInput.value);

        if(codeRequest == 200) {
            if(codeMail.value == code) {
                localStorage.setItem('userId', user.id);
                localStorage.setItem('isAuth', 'true');
                location.href='index.html';
                alert('Вы успешно вошли!');
            } else {
                alert('Вы ввели неверный код!');
            }
        } else {
            alert('Перепроверьте данные!');
        }
    } else {
        alert('Ваш аккаунт заблокирован!');
        return;
    }
        
})

async function requestConfirmPassword(login, password) {
    url = `https://localhost:5098/api/v1/users/authenticate/${login}/${password}`;
    const response = await fetch(url);
    return await response.status;
}
