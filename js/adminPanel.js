const userChangeInpt = document.querySelector('#user-change-inpt');
const roleSelect = document.querySelector('#role-select');
const saveChangesBtn = document.querySelector('#save-change-role-btn');
const rolesDiv = document.getElementById('roles-div');
const pointsDiv = document.getElementById('points-div');
const changeRoleDivBtn = document.getElementById('change-role-div-btn');
const pointDivBtn = document.getElementById('point-div-btn');
const pointAddressInpt = document.getElementById('point-address-inpt');
const createPointBtn = document.getElementById('create-point-btn');
const deletePointBtn = document.getElementById('delete-point-btn');

changeRoleDivBtn.addEventListener('click', () => {
    rolesDiv.style.display = 'block';
    pointsDiv.style.display = 'none';
})

pointDivBtn.addEventListener('click', () => {
    rolesDiv.style.display = 'none';
    pointsDiv.style.display = 'flex';
})

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
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

async function deleteRequest(url) {
    const response = await fetch(url, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }
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

userChangeInpt.addEventListener('change', async () => {
    let user = await getUserByLogin(userChangeInpt.value);
    if(user.role != 'admin') {
        roleSelect.value = user.role;
    }
})

async function getPointByAddress(address) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getByAddress/${address}`);
    return point;
}

createPointBtn.addEventListener('click', async() => {
    let confirmWindow = confirm('Вы точно хотите создать пункт?');

    if(confirmWindow) {
        let point = await getPointByAddress(pointAddressInpt.value);
    
        if(point.id == 0 && point.token == null && point.address == null) {
            let point = {address: pointAddressInpt.value}
            let code = await postRequest(`https://localhost:7049/api/v1/points/create`, point);
    
            if(code == 201) {
                alert('Вы создали новый пункт выдачи!');
            }
        }
    }
})

deletePointBtn.addEventListener('click', async () => {
    let confirmWindow = confirm('Вы точно хотите удалить пункт?');

    if(confirmWindow) {
        let point = await getPointByAddress(pointAddressInpt.value);
    
        if(point.id != 0 && point.token != null && point.address != null) {
            let point = await getPointByAddress(pointAddressInpt.value);
            let code = await deleteRequest(`https://localhost:7049/api/v1/points/delete/${point.id}`);
    
            if(code == 204) {
                alert('Вы удалил пункт!!');
            }
        }
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

