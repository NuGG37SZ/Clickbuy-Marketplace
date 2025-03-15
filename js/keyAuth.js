const inputKey = document.querySelector('#inputKey2');
const orderContainer = document.querySelector('.orders-container');
const loginWrapper = document.querySelector('.login-wrapper');
const authBtn = document.querySelector('#auth-with-token-btn');
const pointTitle = document.querySelector('#point-title');

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getPointByToken(token) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getByToken/${token}`);
    return point;
}

async function checkToken() {
    const point = await getPointByToken(inputKey.value);
    
    if(point != null) {
        orderContainer.style.display = 'block';
        loginWrapper.style.display = 'none';
        pointTitle.textContent = `ПВЗ - ${point.address}`;
    }
}

authBtn.addEventListener('click', async () => {
    checkToken();
})

