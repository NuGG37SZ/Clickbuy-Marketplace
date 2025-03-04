const productContainer = document.querySelector('.row');

insertCards();

function insertCardProduct(product) {
    return `
        <div class="col-6 col-md-3">
                <div class="product-card">
                    <div class="favorite">
                        <img src="source/love.png" alt="Избранное" draggable="false">
                    </div>
                    <div class="product-img">
                        <img src="${product.imageUrl}" alt="Товар 2" draggable="false">
                    </div>
                    <div class="product-info">
                        <p class="product-title">${product.name}</p>

                        <div class="price-block">
                            <span class="current-price">${product.price} ₽</span>
                            <span class="price-badge discount">Выгодно</span>
                        </div>

                        <p class="product-description">${product.description}</p>

                        <div class="rating-reviews">
                            <span class="star">⭐ 4.7</span>
                            <span class="reviews" id="reviews-1" data-count="10">💬</span>
                        </div>                    
                    </div>
                </div>
            </div>
    `;
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

function insertCards() {
    getRequest(`https://localhost:58841/api/v1/products`)
        .then(productsList => {
            productsList.forEach(product => {
                productContainer.insertAdjacentHTML('beforeend', insertCardProduct(product));
            });
        })
}