const tbodyPayment = document.querySelector('.tbody-payment');
const currentUserId = localStorage.getItem('userId');
let sumPayment = document.querySelector('.sum-payment');
const continuePaymentBtn = document.getElementById('continue-btn');
const nextPaymentBtn = document.getElementById('next-payment-btn');
const payBtn = document.getElementById('pay-btn');

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
