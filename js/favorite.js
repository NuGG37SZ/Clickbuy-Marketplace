let productContainer = document.querySelector('.row');

getAllFavoriteByUserId();

function insertCardProductFavorite(product, seller, rating, comment) {
    return `
        <div class="col-6 col-md-3">
            <div class="product-card">
                <div class="favorite">
                    <img src="source/heart.png" alt="Избранное" draggable="false" width="20px" height="20px" class="favorite-img">
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

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getRatingProductByProductId(productId) {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getByProdcutId/${productId}`);
    return ratingProductList;
}

async function getEmptyCommentByProductId(productId) {
    const countEmptyComment = await getRequest(`https://localhost:7029/api/v1/ratingProduct/countEmptyCommentByProductId/${productId}`);
    return countEmptyComment;
}

async function getAvgRatingByProductId(productId) {
    const ratingProductSum = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getAvgRatingByProductId/${productId}`);
    return ratingProductSum;
}

function getAllFavoriteByUserId() {
    getRequest(`https://localhost:7073/api/v1/favorites/getByUserId/${userId}`)
        .then(favoriteList => {
            favoriteList.forEach(favorite => {
                getRequest(`https://localhost:58841/api/v1/products/getById/${favorite.productId}`)
                    .then(product => {
                        getRequest(`https://localhost:5098/api/v1/users/${product.userId}`)
                            .then(async seller => {
                                let ratingProductList = await getRatingProductByProductId(product.id);

                                if(ratingProductList.length != 0) {
                                    let emptyCommentsCount = await getEmptyCommentByProductId(product.id);
                                    let countComment = ratingProductList.length - emptyCommentsCount;
                                    let ratingProductAvg = await getAvgRatingByProductId(product.id);

                                    productContainer.insertAdjacentHTML('beforeend', 
                                        insertCardProductFavorite(product, seller.login, ratingProductAvg, countComment)
                                    );
                                } else {
                                    productContainer.insertAdjacentHTML('beforeend', insertCardProductFavorite(product, seller.login, 0, 0));
                                }                                
                            })
                    })
            });
        })
}

productContainer.addEventListener('click', function(event) {
    if (event.target.closest('.favorite')) {
        const icon = event.target.closest('.favorite'); 
        const card = icon.parentElement; 
        const cardName = card.querySelector('.product-title').textContent;
        const seller = card.querySelector('.product-creater').textContent;

        getRequest(`https://localhost:5098/api/v1/users/getByLogin/${seller}`)
            .then(s => {
                getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${cardName}/${s.id}`)
                    .then(p => {
                        getRequest(`https://localhost:7073/api/v1/favorites/getByUserIdAndProductId/${userId}/${p.id}`)
                            .then(fp => {
                                deleteRequest(`https://localhost:7073/api/v1/favorites/delete/${fp.id}`)
                                    .then(code => {
                                        if(code == 204) {
                                            alert('Вы успешно удалили товар из избранного!');
                                            card.parentElement.remove();
                                        }
                                    })
                            })
                    });
            });
    }

    if (event.target.closest('.product-info')) {
        const productInfo = event.target.closest('.product-info');
        const productName = productInfo.querySelector('.product-title').textContent;
        const userLogin = productInfo.querySelector('.product-creater').textContent;
        localStorage.setItem('userLogin', userLogin);
        localStorage.setItem('productName', productName);
        location.href = 'product.html';
    }
});

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
