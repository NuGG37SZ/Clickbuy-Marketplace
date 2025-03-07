const cartContainer = document.querySelector('.cart-items');
const cartBtnHeader = document.getElementById('cart-btn-header');
const sumOrder = document.getElementById('sum-order');
const countProductOnCart = document.getElementById('count-product-on-cart');

cartBtnHeader.addEventListener('click', () => {
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
})

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getProductByCart(cart) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${cart.productId}`);
    return product;  
}

async function getUserLoginByProduct(product) {
    const user = await getRequest(`http://localhost:5098/api/v1/users/${product.userId}`);
    return user.login;  
}

async function getProductSizesSizeByCart(cart) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${cart.productSizesId}`);
    return productSizes.size;  
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
                        <li>${productSize}</li>
                    </ul

                    <p class="product-description">${product.description}</p>

                    <div class="rating-reviews">
                        <span class="star">⭐ 4.7</span>
                        <span class="reviews" id="reviews-1" data-count="120">💬</span>
                    </div>                    
                </div>
            </div>
    `;
}
