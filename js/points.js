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

async function getUserPointsByIsActive(isActive) {
    const userPoint = await getRequest(`https://localhost:7049/api/v1/userPoints/getByIsActive/${isActive}`);
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
})

saveActivePointBtn.addEventListener('click', async () => {
    let currentPoint = await getPointById(point.id);
    let userPoint = await getUserPointByUserIdAndPointId(userId, currentPoint.id);
    let activeUserPoint = await getUserPointsByIsActive(true);

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
    let activeUserPoint = await getUserPointsByIsActive(true);
    let point = await getPointById(activeUserPoint.pointsId)
    addressPointBtn.textContent = point.address;
}


