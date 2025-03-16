const loginUserProfile = document.getElementById('user-login');
const panelRoles = document.querySelector('.panels-roles');
const delivery = document.querySelector('.deliveries');
const countFavorite = document.getElementById('count-goods');
const rightPanel = document.querySelectorAll('.card-right-panel');
const goodsNoCommentText = document.getElementById('goods-no-comment');
const favoriteDiv = rightPanel[0];
let userId = localStorage.getItem('userId');
const wordForms = ['товар', 'товара', 'товаров'];

document.addEventListener("DOMContentLoaded", async ()  => {
    checkAuthUser();
    insertDelivery();
    await getFavoriteProductsText();
    await getProductNoCommentText();
})

async function getFavoriteProductsText() {
    let favoriteCount = await getCountFavorites();
    let text = getPluralForm(favoriteCount, wordForms)
    countFavorite.textContent = `${favoriteCount} ${text}`;
}

async function getProductNoCommentText() {
    let count = 0;
    let ratingProductList = await getRatingProductListByUserId(parseInt(userId));

    for (const ratingProduct of ratingProductList) {
        let order = await getOrderById(ratingProduct.orderId);

        if(order.status == 'Получен' && ratingProduct.comment == '') {
            count++;
        }
    }  
    let text = getPluralForm(count, wordForms)
    goodsNoCommentText.textContent = `${count} ${text}`;
}

favoriteDiv.addEventListener('click', () => {
    location.href = 'favorite.html';
})

function checkAuthUser() {
    if(localStorage.getItem('isAuth') === 'true') {
        const id = localStorage.getItem('userId');

        getUserByIdRequest(id)
            .then(user => {
                loginUserProfile.textContent = user.login;
                hideElementForRoleUser(user.role);
            })
    }
}

async function getUserByIdRequest(id) {
    url = `https://localhost:5098/api/v1/users/${id}`;
    const response = await fetch(url);
    return await response.json();
}

function hideElementForRoleUser(role) {
    switch(role) {
        case 'user':
            panelRoles.style.display = 'none';
            break;
        case 'seller':
            panelRoles.children[1].style.display = 'none';
            panelRoles.children[2].style.display = 'none';
            break;
        case 'moderator': 
            panelRoles.children[0].style.display = 'none';
            panelRoles.children[2].style.display = 'none';
            break;
    }
}

function insertDeliveryCard(order, product, date) {
    return `
        <div class="delivery-card">
            <img src="${product.imageUrl}" width="150px" height="140px" style="margin-top: 5px">
            <div class="title-price-delivery">
                <p style="font-size: 20px" id="status-delivery"><b>Заказ №${order.id}</b></p>
                <p style="font-size: 20px" id="status-delivery">${order.status}</p>
                <p style="font-size: 16px" id="date-delivery">Приедет ${date}</p>
            </div>
        </div>   
    `
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getOrderListByUserId(userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByUserId/${userId}`);
    return orderList;  
}

async function getOrderProductListByOrderId(orderId) {
    const orderProductList = await getRequest(`https://localhost:7049/api/v1/orderProduct/getByOrderId/${orderId}`);
    return orderProductList;  
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;  
}

async function getFavoriteListByUserId(userId) {
    const favorites = await getRequest(`https://localhost:7073/api/v1/favorites/getByUserId/${userId}`);
    return favorites;  
}

async function getCountRatingProductByEmptyCommentAndUserId(userId) {
    const count = await getRequest(`https://localhost:7029/api/v1/ratingProduct/countRatingByUserIdAndEmptyComment/${userId}`);
    return count;
}

async function getRatingProductListByUserId(userId) {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getByUserId/${userId}`);
    return ratingProductList;
}

async function insertDelivery() {
    let orderList = await getOrderListByUserId(parseInt(userId));
    
    if(orderList.length <= 3) {
        for (const order of orderList) {
            insertCardDelivery(order);
        }
    } else {
        let order = orderList[0];
        insertCardDelivery(order);
    }
} 

async function insertCardDelivery(order) {
    if(order.status != 'Отменен' && order.status != 'Получен') {
        let orderProductList = await getOrderProductListByOrderId(order.id);
        let orderProduct = orderProductList[0];
        let date = getDate(order.createOrder, 11);
        let product = await getProductById(orderProduct.productId);
        delivery.insertAdjacentHTML('beforeend', insertDeliveryCard(order, product, date));
    }
}

function getDate(dateStr, days) {
    const dateFirst = moment(dateStr, "YYYY-MM-DD HH:mm:ss");
    dateFirst.add(days, 'days');
    return dateFirst.format('DD.MM.YYYY');
}

async function getCountFavorites() {
    const favoriteList = await getFavoriteListByUserId(parseInt(userId));
    const count = favoriteList.length;
    return count;
}

function getPluralForm(number, words) {
    const lastDigit = number % 10;
    const lastTwoDigits = number % 100;

    if (lastTwoDigits >= 11 && lastTwoDigits <= 19) {
        return words[2];
    }

    if (lastDigit === 1) {
        return words[0];
    } else if (lastDigit >= 2 && lastDigit <= 4) {
        return words[1];
    } else {
        return words[2];
    }
}



