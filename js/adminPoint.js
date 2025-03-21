const pointAddressInpt = document.getElementById('point-address-inpt');
const createPointBtn = document.getElementById('create-point-btn');
const deletePointBtn = document.getElementById('delete-point-btn');

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