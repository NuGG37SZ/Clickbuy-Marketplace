const userChangeInpt = document.querySelector('#user-change-inpt');
const roleSelect = document.querySelector('#role-select');
const saveChangesBtn = document.querySelector('#save-change-role-btn');

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function putRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(obj)
    });
    let result = await response.status;
    return result;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

userChangeInpt.addEventListener('change', async () => {
    let user = await getUserByLogin(userChangeInpt.value);
    if(user.role != 'admin') {
        roleSelect.value = user.role;
    }
})

saveChangesBtn.addEventListener('click', async() => {
    let confirmWindow = confirm('Вы точно хотите изменить права доступа?');

    if(confirmWindow) {
        let user = await getUserByLogin(userChangeInpt.value);
        let role = roleSelect.value;
        user.role = role;
        let code = await putRequest(`https://localhost:5098/api/v1/users/update/${user.id}`, user);

        if(code == 200) {
            alert('Права доступа успешно изменены!');
        }
    }
})