const cartContainer = document.querySelector('.cart-items');
const cartBtnHeader = document.getElementById('cart-btn-header');
const sumOrder = document.getElementById('sum-order');
const countProductOnCart = document.getElementById('count-product-on-cart');
const goPaymentBtn = document.getElementById('buy-order-btn');

cartBtnHeader.addEventListener('click', () => {
    getAllProductCartOnCurrentUser();
})

function getAllProductCartOnCurrentUser() {
    cartContainer.innerHTML = '';
    let sum = 0;
    let countInOrder = 0;
    
    getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${userId}`)
        .then(cartList => {
            cartList.forEach(async cart => {
                if(cartContainer.children.length != cartList.length) {
                    let currentProduct = await getProductByCart(cart);
                    let userLogin = await getUserLoginByProduct(currentProduct);
                    let size = await getProductSizesSizeByCart(cart);
                    let count = cart.count;
                    cartContainer.insertAdjacentHTML('beforeend' ,insertCardProduct(currentProduct, userLogin, size, count))
                    
                    sum += currentProduct.price * count;
                    countInOrder += count;
                    sumOrder.textContent = `Сумма заказа: ${sum}₽`;
                    countProductOnCart.textContent = `Количество товаров в заказе: ${countInOrder}`;
                }
            })
            
        });
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getProductByCart(cart) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${cart.productId}`);
    return product;  
}

async function getUserLoginByProduct(product) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${product.userId}`);
    return user.login;  
}

async function getProductSizesSizeByCart(cart) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${cart.productSizesId}`);
    return productSizes.size;  
}

async function getAllCartByCurrentUser() {
    let userId = localStorage.getItem('userId');
    const listCart = await getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${parseInt(userId)}`);
    return listCart;
}

async function getUserIdByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user.id;
}

async function getProductIdByNameProductAndUserId(nameProduct, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${nameProduct}/${userId}`);
    return product.id;
}

async function getProductSizeByProductIdAndSize(productId, size) {
    const productSize = await getRequest(`https://localhost:58841/api/v1/productSizes/getByProductIdAndSize/${productId}/${size}`);
    return productSize.id;
}

async function deleteRequest(url) {
    const response = await fetch(url, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
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

function insertCardProduct(product, seller, productSize, countProduct) {
    return `
            <div class="product-card" style="margin-top: 10px;">
                <div class="product-img">
                    <img src="${product.imageUrl}" draggable="false">
                </div>
                <div class="product-info">
                    <p class="product-title">${product.name}</p>

                    <div class="price-block">
                        <span class="current-price">${product.price} ₽</span>
                        <span class="price-badge discount">Выгодно</span>
                    </div>

                    <p class="product-creater">${seller}</p>

                    <ul style="margin-bottom: 0;">
                        <li class="product-size-cart">${productSize}</li>
                    </ul

                    <p class="product-description">${product.description}</p>

                    <p class="product-count-cart">Количество: ${countProduct}<p>

                    <div class="rating-reviews" style="display: flex; justify-content: space-between;">
                        <div style="display: flex; flex-direction: row;">
                            <span class="star">⭐ 4.7</span>
                            <span class="reviews" id="reviews-1" data-count="120" style="margin-left: 5px">💬</span>
                        </div> 
                        <div style="display: flex; justify-content: end;">
                            <button class="btn btn-outline-danger" id="delete-product-cart"><i class="bi bi-x-circle-fill"></i></button>
                        </div>
                    </div>  
                </div>
            </div>
    `;
}

cartContainer.addEventListener('click', async (event) => {
    if(event.target.closest('#delete-product-cart')) {
        let currentProductInfo = event.target.closest('#delete-product-cart').parentNode.parentNode.parentNode;
        let titleCart = currentProductInfo.querySelector('.product-title').textContent;
        let sellerLogin = currentProductInfo.querySelector('.product-creater').textContent;
        let currentSize = currentProductInfo.querySelector('.product-size-cart').textContent;
        let count = currentProductInfo.querySelector('.product-count-cart').textContent;
        let num = parseInt(count.match(/\d+/)[0]);

        let userId = await getUserIdByLogin(sellerLogin);
        let productId = await getProductIdByNameProductAndUserId(titleCart, userId);
        let productSizeId = await getProductSizeByProductIdAndSize(productId, currentSize);

        let listCart = await getAllCartByCurrentUser();
        listCart.forEach(async cart => {
            if(cart.userId == userId && cart.productId == productId && 
                cart.productSizesId == productSizeId) 
            {
                if(num > 1) {
                    --cart.count;
                    await putRequest(`https://localhost:7073/api/v1/carts/update/${cart.id}`, cart);
                    getAllProductCartOnCurrentUser();
                    return;
                } else {
                    let code = await deleteRequest(`https://localhost:7073/api/v1/carts/delete/${cart.id}`)
                    if(code == 204) {
                        let currentCart = currentProductInfo.parentNode;
                        currentCart.remove();
                        getAllProductCartOnCurrentUser();
                        return;
                    }
                }

            }
        })
    }

    if(event.target.closest('.product-img')) {
        console.log(event.target.closest('.product-img'))
        const productInfo = event.target.closest('.product-img').parentNode.children[1];
        const productName = productInfo.querySelector('.product-title').textContent;
        const userLogin = productInfo.querySelector('.product-creater').textContent;
        localStorage.setItem('userLogin', userLogin);
        localStorage.setItem('productName', productName);

        let confirmWindow = confirm('Вы точно хотите перейти на страницу товара?');
        
        if(confirmWindow) location.href = 'product.html';
    }
})

goPaymentBtn.addEventListener('click', () => {
    location.href = 'payment.html';
})