const loginInpt = document.getElementById('loginInputReg');
const passwordInpt = document.getElementById('passwordInputReg');
const emailInpt = document.getElementById('emailInputReg');
const confirmPasswordInpt = document.getElementById('confirmPasswordInput');
const registerBtn = document.getElementById('registation-btn');

registerBtn.addEventListener('click', async () => {
    if(passwordInpt.value === confirmPasswordInpt.value) {
        if(!emailInpt.value.includes('@yandex.ru')) {
            alert('Пожалуйста, введите почту yandex для того, чтобы получать информацию о покупках на почту!');
            return;
        } else {
            let user = {
                login: loginInpt.value,
                password: passwordInpt.value,
                email: emailInpt.value,
                role: 'user',
                isBanned: false
            }
            const result = await createUserRequest(user);
    
            if(result == 201) {
                alert('Вы успешно зарегистрировались!');
            }
        }
    } else {
        alert('Пароли не совпадают!');
    }
})

async function createUserRequest(user) {
    const response = await fetch('https://localhost:5098/api/v1/users/create', { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(user)
    });
    let result = await response.status;
    return result;
}