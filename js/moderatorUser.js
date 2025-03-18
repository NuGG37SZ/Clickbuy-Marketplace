const usersDiv = document.querySelector('#users-div');

insertAllUsers();

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

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getAllUsers() {
    const userList = await getRequest(`https://localhost:5098/api/v1/users`);
    return userList;
}

function insertUserCard(user, role) {
    return `
        <div style="border: 1px solid black; padding: 15px; border-radius: 20px; margin-top: 15px;">
            <p>Пользователь: ${user.login}</p>
            <p>Роль: ${role}</p>
            <button type="button" class="btn btn-success" id="banned-user-btn">Заблокировать</button>
            <button type="button" class="btn btn-danger" id="unbanned-user-btn">Разблокировать</button>
        </div>
    `
}

async function insertAllUsers() {
    let userList = await getAllUsers();
    
    for (const user of userList) {
        if(user.role != 'admin' || user.role != 'moderator') {
            let role = '';
    
            switch(user.role) {
                case 'user':
                    role = 'Пользователь';
                    break;
                case 'seller':
                    role = 'Продавец';
                    break;
            }
            usersDiv.insertAdjacentHTML('beforeend', insertUserCard(user, role));
        }
    }
}

usersDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#banned-user-btn')) {
        let confirmWindow = confirm('Вы хотите заблокировать пользователя?');
        if(confirmWindow) {
            let cardUser = event.target.closest('#banned-user-btn').parentNode;
            await updateUser(cardUser, true);
        } 
    }

    if(event.target.closest('#unbanned-user-btn')) {
        let confirmWindow = confirm('Вы хотите заблокировать пользователя?');
        if(confirmWindow) {
            let cardUser = event.target.closest('#unbanned-user-btn').parentNode;
            await updateUser(cardUser, false);
        }
    }
})

async function updateUser(cardUser, isBanned) {
    let userLogin = cardUser.children[0].textContent.split(': ')[1];
    let user = await getUserByLogin(userLogin);
    user.isBanned = isBanned;
    let code = await putRequest(`https://localhost:5098/api/v1/users/update/${user.id}`, user);
    if(code == 200) {
        if(isBanned == false) {
            alert('Пользователь успешно разбанен');
        } else {
            alert('Пользователь успешно забанен');
        }
    }
        
}
 