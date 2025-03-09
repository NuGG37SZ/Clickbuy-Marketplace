const cartContainer = document.querySelector('.cart-items');
const cartBtnHeader = document.getElementById('cart-btn-header');
const sumOrder = document.getElementById('sum-order');
const countProductOnCart = document.getElementById('count-product-on-cart');

cartBtnHeader.addEventListener('click', () => {
    getAllProductCartOnCurrentUser();
})

function getAllProductCartOnCurrentUser() {
    cartContainer.innerHTML = '';
    let sum = 0;
    
    getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${userId}`)
        .then(cartList => {
            cartList.forEach(async cart => {
                if(cartContainer.children.length != cartList.length) {
                    let currentProduct = await getProductByCart(cart);
                    let userLogin = await getUserLoginByProduct(currentProduct);
                    let size = await getProductSizesSizeByCart(cart);
                    cartContainer.insertAdjacentHTML('beforeend' ,insertCardProduct(currentProduct, userLogin, size))
                    
                    sum +=currentProduct.price;
                    sumOrder.textContent = `Сумма заказа: ${sum}₽`;
                    countProductOnCart.textContent = `Количество товаров в заказе: ${cartList.length}`;
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

async function getAllCart() {
    const listCart = await getRequest(`https://localhost:7073/api/v1/carts`);
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

function insertCardProduct(product, seller, productSize) {
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

        let userId = await getUserIdByLogin(sellerLogin);
        let productId = await getProductIdByNameProductAndUserId(titleCart, userId);
        let productSizeId = await getProductSizeByProductIdAndSize(productId, currentSize);

        let listCart = await getAllCart();
        listCart.forEach(async cart => {
            if(cart.userId == userId && cart.productId == productId && 
                cart.productSizesId == productSizeId) 
            {
                let code = await deleteRequest(`https://localhost:7073/api/v1/carts/delete/${cart.id}`)
                if(code == 204) {
                    let currentCart = currentProductInfo.parentNode;
                    currentCart.remove();
                    getAllProductCartOnCurrentUser();
                    return;
                }
            }
        })
    }
})