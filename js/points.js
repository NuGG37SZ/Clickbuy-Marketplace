const pickUpPoints = document.querySelector('.pick-up-points');
const saveActivePointBtn = document.getElementById('save-active-point-btn');
const addressPointBtn = document.getElementById('address-point-btn');
let point;

insertAllRadioPoints();
changeNameBtnToActivePoint();

function insertRadioPoint(address) {
    return `
        <div class="radio-point">
            <input class="form-check-input" type="radio" name="flexRadioDefault">
            <label class="form-check-label">${address}</label>
            <div class="dropdown">
                <a class="btn" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="bi bi-three-dots-vertical"></i>
                </a>

                <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                    <li><a class="dropdown-item" href="#" id="delete-point">Удалить</a></li>
                    <li><a class="dropdown-item" href="point.html">Показать на карте</a></li>
                </ul>
            </div>
        </div>
    `
}

async function getPointById(id) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getById/${id}`);
    return point
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

async function getListUserPointsByUserId(userId) {
    const userPoints = await getRequest(`https://localhost:7049/api/v1/userPoints/getByUserId/${userId}`);
    return userPoints;
}

async function getUserPointByUserIdAndPointId(userId, pointId) {
    const userPoint = await getRequest(`https://localhost:7049/api/v1/userPoints/getByUserIdAndPointsId/${userId}/${pointId}`);
    return userPoint;
}

async function getPointByAddress(address) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getByAddress/${address}`);
    return point;
}

async function getPointById(id) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getById/${id}`);
    return point;
}

async function getUserPointsByIsActiveAndUserId(isActive, userId) {
    const userPoint = await getRequest(`https://localhost:7049/api/v1/userPoints/getByIsActiveAndUserId/${isActive}/${userId}`);
    return userPoint;
}

async function insertAllRadioPoints() {
    let userPoints = await getListUserPointsByUserId(userId);

    userPoints.forEach(async up => {
        let point = await getPointById(up.pointsId);
        pickUpPoints.insertAdjacentHTML('beforeend', insertRadioPoint(point.address))
    })
} 

pickUpPoints.addEventListener('click', async (event) => {
    if(event.target.closest('.radio-point')) {
        let pointAddress = event.target.closest('.radio-point').children[1].textContent;
        point = await getPointByAddress(pointAddress);
    }

    if(event.target.closest('#delete-point')) {
        let currentBtn = event.target.closest('#delete-point');
        let currentPointDiv = currentBtn.parentNode.parentNode.parentNode.parentNode;
        let addressPoint = currentPointDiv.children[1].textContent;
        let point = await getPointByAddress(addressPoint);
        let userPoint = await getUserPointByUserIdAndPointId(parseInt(userId), point.id);

        let confirmWindow = confirm('Вы точно хотите удалить текущий пункт выдачи?');
        if(confirmWindow) {
            let code = await deleteRequest(`https://localhost:7049/api/v1/userPoints/delete/${userPoint.id}`);
            if(code == 204) {
                alert('Вы удалили пункт выдачи!');
            }
        }
    }
})

saveActivePointBtn.addEventListener('click', async () => {
    let currentPoint = await getPointById(point.id);
    let userPoint = await getUserPointByUserIdAndPointId(parseInt(userId), currentPoint.id);
    let activeUserPoint = await getUserPointsByIsActiveAndUserId(true, parseInt(userId));

    if(activeUserPoint.pointsId != userPoint.pointsId) {
        activeUserPoint.isActive = false;

        let userPointUpdateModel = {
            userId: userPoint.userId,
            pointsId: userPoint.pointsId,
            isActive: true
        }
    
        await putRequest(`https://localhost:7049/api/v1/userPoints/update/${activeUserPoint.id}`, activeUserPoint);
        await putRequest(`https://localhost:7049/api/v1/userPoints/update/${userPoint.id}`, userPointUpdateModel);
        alert('Активный пункт установлен');
    }
})

async function changeNameBtnToActivePoint() {
    let activeUserPoint = await getUserPointsByIsActiveAndUserId(true, parseInt(userId));
    let point = await getPointById(activeUserPoint.pointsId)
    addressPointBtn.textContent = point.address;
}


