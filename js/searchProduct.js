const searchInpt = document.querySelector('#search-inpt');
const searchBtn = document.querySelector('.search-btn');

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

function insertCardProductSearch(product, seller, inFavorite, rating, comment) {
    return `
        <div class="col-6 col-md-3">
            <div class="product-card">
                <div class="favorite">
                    <img ${inFavorite == true ? 'src="source/heart.png"' : 'src="source/love.png"'}
                        alt="Избранное" draggable="false" width="20px" height="20px" class="favorite-img">
                </div>
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

                    <p class="product-description">${product.description}</p>

                    <div class="rating-reviews">
                        <span class="star">⭐ ${rating}</span>
                        <span class="reviews" id="reviews-1" data-count="120">💬 ${comment}</span>
                    </div>                    
                </div>
            </div>
        </div>
    `;
}

function checkFavoriteProduct(seller, product) {
    getRequest(`https://localhost:7073/api/v1/favorites/getByUserIdAndProductId/${userId}/${product.id}`)
        .then(async fp => {
            let ratingProductList = await getRatingProductByProductId(product.id);
            let commentCount = 0;
            let rating = 0;
            let ratingProductAvg = 0;
            
            if(ratingProductList.length != 0) {
                for (const ratingProduct of ratingProductList) {
                    if(ratingProduct.comment != '' && ratingProduct.rating != 0.0) {
                        commentCount += 1;
                        rating += ratingProduct.rating;
                        ratingProductAvg = rating / commentCount;
                    }
                }
                
                if (fp && fp.productId === product.id) {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductSearch(product, seller.login, true, ratingProductAvg, commentCount)
                    );
                } else {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductSearch(product, seller.login, false, ratingProductAvg, commentCount)
                    );
                }
                
            } else {
                if (fp && fp.productId === product.id) {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductSearch(product, seller.login, true, 0, 0)
                    );
                } else {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductSearch(product, seller.login, false, 0, 0)
                    );
                }
            }
        })
}

searchBtn.addEventListener('click', () => {
    productContainer.innerHTML = '';
    getRequest(`https://localhost:58841/api/v1/products/getByName/${searchInpt.value}`)
    .then(productsList => {
        productsList.forEach(product => {
            getRequest(`https://localhost:5098/api/v1/users/${product.userId}`)
                .then(seller => {
                    checkFavoriteProduct(seller, product);
                })
        });
    })
})