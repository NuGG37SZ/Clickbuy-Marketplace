const pointsContainer = document.querySelector('.points');
const userId = localStorage.getItem('userId');

insertAllPoints();

function insertPoint(points) {
    return `
        <div class="point">
            <p style="font-size: 24px; text-align: center;" class="address-point">${points.address}</p>
            <div style="font-size: 16px; color: gray;" class="footer-point">
                <label>5⭐,</label>
                <label style="margin-left: 5px;">Пункт выдачи</label>
            </div>
        </div>
    `
}

function insertRadioPoint(address) {
    return `
        <div>
            <input class="form-check-input" type="radio" name="flexRadioDefault">
            <label class="form-check-label">${address}</label> 
        </div>
    `
}

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

async function getAllPoints() {
    const points = await getRequest(`https://localhost:7049/api/v1/points`);
    return points;
}

async function getPointByAddress(address) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getByAddress/${address}`);
    return point;
}

async function getPointById(id) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getById/${id}`);
    return point
}

async function getUserPointByUserIdAndPointId(userId, pointId) {
    const userPoint = await getRequest(`https://localhost:7049/api/v1/userPoints/getByUserIdAndPointsId/${userId}/${pointId}`);
    return userPoint;
}

async function getListUserPointsByUserId(userId) {
    const userPoints = await getRequest(`https://localhost:7049/api/v1/userPoints/getByUserId/${userId}`);
    return userPoints;
}

async function insertAllPoints() {
    let points = await getAllPoints();

    points.forEach(point => {
        pointsContainer.insertAdjacentHTML('beforeend', insertPoint(point));
    })
}

pointsContainer.addEventListener('click', async (event) => {
    if(event.target.closest('.point')) {
        let currentPoint = event.target.closest('.point');
        let confirmWindow = confirm('Вы хотите добавить данный пункт выдачи?');

        if(confirmWindow) {
            let address = currentPoint.querySelector('.address-point').textContent;
            let point = await getPointByAddress(address);

            let userPoint = await getUserPointByUserIdAndPointId(parseInt(userId), point.id);

            if(userPoint.userId != 0 && userPoint.pointsId != 0) {
                alert('У вас же есть этот пункт выдачи!');
                return;
            } 
            
            let userPointCreateModel = {
                userId: parseInt(userId),
                pointsId: point.id,
                isActive: false
            };

            let code = await postRequest(`https://localhost:7049/api/v1/userPoints/create`, userPointCreateModel);

            if(code == 201) alert('Вы добавили пункт выдачи!');
        }
    }
})   