const tbodyPayment = document.querySelector('.tbody-payment');
const currentUserId = localStorage.getItem('userId');
let sumPayment = document.querySelector('.sum-payment');
const continuePaymentBtn = document.getElementById('continue-btn');
const nextPaymentBtn = document.getElementById('next-payment-btn');
const payBtn = document.getElementById("pay-btn");
const paymentMethods = document.querySelectorAll('input[name="payment-method"]');
const paymentDetails = document.getElementById('payment-details');
const cardDetails = document.getElementById('card-details');
const qrCode = document.getElementById('qr-code');
const cardNumberInput = document.getElementById("card-number");
const cardDateInput = document.getElementById("card-date");
const cardCVVInput = document.getElementById("card-cvv");
const cardLogo = document.getElementById("card-logo");
const addressOrder = document.querySelector('.address-order-payment');

cardLogo.style.display = "none";

insertAllProductForCurrentUser();
generateQrCode();

function insertTableProductPayment(product, seller, productSize, countProduct) {
  return `
        <tr>
            <td>
                <img src="${product.imageUrl}" draggable="false" width="100px">
            </td>
            <td>${product.name}</td>
            <td>${product.price} ₽</td>
            <td>${seller.login}</td>
            <td>${productSize}</td>
            <td>${product.description}</td>
            <td>Количество: ${countProduct}</td>
        </tr>`
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

async function getAllCartByCurrentUser() {
    const listCart = await getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${parseInt(currentUserId)}`);
    return listCart;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;  
}

async function getUserByProduct(product) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${product.userId}`);
    return user;  
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;  
}

async function getAllOrdersByUserId(userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByUserId/${userId}`);
    return orderList;
}

async function getAllOrderProductByOrderId(orderId) {
    const orderProductList = await getRequest(`https://localhost:7049/api/v1/orderProduct/getByOrderId/${orderId}`);
    return orderProductList;
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;
}

async function getActiveUserPoint(isActive, userId) {
    const userPoint = await getRequest(`https://localhost:7049/api/v1/userPoints/getByIsActiveAndUserId/${isActive}/${userId}`);
    return userPoint;
}

async function getPointById(id) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getById/${id}`);
    return point;
}

async function getActivePoint() {
    const activeUserPoint = await getActiveUserPoint(true, parseInt(userId));
    const point = await getPointById(activeUserPoint.pointsId);
    if(point != null) return point;
}

async function insertAllProductForCurrentUser() {
    let sum = 0;
    const listCart = await getAllCartByCurrentUser();
    const point = await getActivePoint();

    listCart.forEach(async cart => {
        let product = await getProductById(cart.productId);
        let seller = await getUserByProduct(product);
        let productSizes = await getProductSizesById(cart.productSizesId);
        sum += product.price * cart.count;

        tbodyPayment.insertAdjacentHTML('beforeend', insertTableProductPayment(product, seller, productSizes.size, cart.count));
        sumPayment.textContent = `Итого: ${sum} ₽`;
    })
    addressOrder.textContent = `Пункт выдачи: ${point.address}`;
}

continuePaymentBtn.addEventListener('click', () => {
    const paymentMethodsDiv = document.querySelector('.payment-methods');
    paymentMethodsDiv.style.display = 'block';
}) 

payBtn.addEventListener('click', async () => {
    let now = new Date();
    let dateStr = formatDate(now);
    let dateObj = parseDateFromString(dateStr);
    const point = await getActivePoint();

    let orderCreateModel = {
        userId: parseInt(currentUserId),
        pointId: point.id,
        createOrder: dateObj,
        updateOrder: dateObj,
        status: "В сборке"
    }
    let code = await postRequest(`https://localhost:7049/api/v1/orders/create`, orderCreateModel);

    if (code === 201) {
        let orderListByCurrentUser = await getAllOrdersByUserId(currentUserId);
        const listCart = await getAllCartByCurrentUser();

        for (const currentOrder of orderListByCurrentUser) {
            let orderProductListByCurrentOrder = await getAllOrderProductByOrderId(currentOrder.id);

            if (orderProductListByCurrentOrder.length === 0) {
               
                for (const cart of listCart) {
                    let product = await getProductById(cart.productId);

                    let orderProductCreateModel = {
                        orderId: currentOrder.id,
                        productId: product.id,
                        productSizesId: cart.productSizesId,
                        userId: product.userId,
                        count: cart.count
                    };
                    
                    await postRequest(`https://localhost:7049/api/v1/orderProduct/create`, orderProductCreateModel);
                    let productSizes = await getProductSizesById(cart.productSizesId);
                    productSizes.count -= orderProductCreateModel.count;
                    await putRequest(`https://localhost:58841/api/v1/productSizes/updateById/${productSizes.id}`, productSizes);
                    let date = new Date();

                    let ratingProductModel = {
                        productId: product.id,
                        productSizesId: cart.productSizesId,
                        userId: parseInt(currentUserId),
                        orderId: currentOrder.id,
                        rating: 0.0,
                        comment: '',
                        dateCreateComment: date
                    }

                    let user = await getUserById(parseInt(currentUserId));
                    let table = document.querySelector('.all-product-for-payment');
                    table.setAttribute("border", "1");
                    let checkBody = table.outerHTML;

                    let message = {
                        to: [user.email],
                        bcc: ["Anezer20@yandex.ru"],
                        cc: ["Anezer20@yandex.ru"],
                        from: "Anezer20@yandex.ru",
                        displayName: "ClickBuy",
                        replyTo: "",
                        replyToName: "ClickBuy",
                        subject: "Чек",
                        body: checkBody,
                    }

                    await postRequest(`https://localhost:7029/api/v1/ratingProduct/create`, ratingProductModel);
                    await postRequest(`https://localhost:7160/api/v1/notificationMail/sendmail`, message);
                    await deleteRequest(`https://localhost:7073/api/v1/carts/delete/${cart.id}`);
                }
            }
        }
    }
    location.href = 'check.html';
})

function formatDate(date) {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${day}.${month}.${year} ${hours}:${minutes}`;
}

function parseDateFromString(dateString) {
    const [datePart, timePart] = dateString.split(' ');
    const [day, month, year] = datePart.split('.');
    const [hours, minutes] = timePart.split(':');
    return new Date(year, month - 1, day, hours, minutes);
}

paymentDetails.style.display = 'none';
cardDetails.style.display = 'none';
qrCode.style.display = 'none';
payBtn.style.display = 'none';

paymentMethods.forEach(method => {
    method.addEventListener('change', (event) => {
        paymentDetails.style.display = 'none';
        cardDetails.style.display = 'none';
        qrCode.style.display = 'none';
        payBtn.style.display = 'none';

        if (event.target.id === "card-method") {
            cardDetails.style.display = 'block';
            paymentDetails.style.display = 'block';
            payBtn.style.display = 'block';
        } else if (event.target.id === "sbp-method") { 
            qrCode.style.display = 'block';
            paymentDetails.style.display = 'block';
            payBtn.style.display = 'none';
        } else if (event.target.id === "yandex-money-method") {
            paymentDetails.style.display = 'none';
            qrCode.style.display = 'none';
            payBtn.style.display = 'block';
        }
    });
});

function generateQrCode() {
    const url = "check.html";

    new QRCode(document.getElementById("qrcode"), {
        text: url,
        width: 200, 
        height: 200
    });   
}

cardCVVInput.addEventListener("input", (e) => {
    cardCVVInput.value = "*".repeat(cardCVVInput.value.length);
});

cardNumberInput.addEventListener("input", (e) => {
    let value = e.target.value.replace(/\D/g, "");
    value = value.replace(/(.{4})/g, "$1 ").trim();
    e.target.value = value.slice(0, 19);

    if (value.length === 0) {
        cardLogo.style.display = "none";
    } else {
        cardLogo.style.display = "block";
    }

    if (/^2/.test(value)) {
        cardLogo.src = "source/icons8-mir-48.png";
    } else if (/^4/.test(value)) {
        cardLogo.src = "source/icons8-visa-48.png";
    } else if (/^5/.test(value)) {
        cardLogo.src = "source/icons8-mastercard-48.png";
    } else {
        cardLogo.style.display = "none";
    }
});

cardDateInput.addEventListener("input", (e) => {
    let value = e.target.value.replace(/\D/g, "");

    if (value.length >= 2) {
        value = value.slice(0, 2) + "/" + value.slice(2);
    }

    e.target.value = value.slice(0, 5);
});

document.querySelector("#card-method").addEventListener("change", function() {
    if (this.checked) {
        document.querySelector("#payment-details").style.display = "block"; 
        document.querySelector("#pay-btn").style.display = "block"; 
        document.querySelector("#qr-code").style.display = "none";
    }
});

document.querySelector("#sbp-method").addEventListener("change", function() {
    if (this.checked) {
        document.querySelector("#qr-code").style.display = "block";
        document.querySelector("#card-details").style.display = "none";
        document.querySelector("#pay-btn").style.display = "none";
    }
});

document.querySelector("#yandex-money-method").addEventListener("change", function() {
    if (this.checked) {
        document.querySelector("#pay-btn").style.display = "block";
        document.querySelector("#payment-details").style.display = "none";
        document.querySelector("#qr-code").style.display = "none";
    }
});