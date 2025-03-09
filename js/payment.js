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

cardLogo.style.display = "none";

insertAllProductForCurrentUser();

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

async function getAllCartByCurrentUser() {
    const listCart = await getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${parseInt(currentUserId)}`);
    return listCart;
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

async function insertAllProductForCurrentUser() {
    let sum = 0;
    const listCart = await getAllCartByCurrentUser();

    listCart.forEach(async cart => {
        let product = await getProductById(cart.productId);
        let seller = await getUserByProduct(product);
        let productSizes = await getProductSizesById(cart.productSizesId);
        sum += product.price * cart.count;

        tbodyPayment.insertAdjacentHTML('beforeend', insertTableProductPayment(product, seller, productSizes.size, cart.count));
        sumPayment.textContent = `Итого: ${sum} ₽`;
    })
}

continuePaymentBtn.addEventListener('click', () => {
    const personDataDiv = document.querySelector('.person-data');
    personDataDiv.style.display = 'block';
}) 

nextPaymentBtn.addEventListener('click', () => {
    const paymentMethodsDiv = document.querySelector('.payment-methods');
    paymentMethodsDiv.style.display = 'block';
})

payBtn.addEventListener('click', () => {
    location.href = 'index.html';
})


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
        document.querySelector("#pay-btn").style.display = "none";
        document.querySelector("#payment-details").style.display = "none";
    }
});

document.querySelector("#yandex-money-method").addEventListener("change", function() {
    if (this.checked) {
        document.querySelector("#pay-btn").style.display = "block";
        document.querySelector("#payment-details").style.display = "none";
        document.querySelector("#qr-code").style.display = "none";
    }
});