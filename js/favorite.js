let userId = localStorage.getItem('userId');
let productContainer = document.querySelector('.row');

getAllFavoriteByUserId();

function insertCardProductFavorite(product, seller) {
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
                        <span class="star">⭐ 4.7</span>
                        <span class="reviews" id="reviews-1" data-count="120">💬</span>
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

function getAllFavoriteByUserId() {
    getRequest(`https://localhost:7073/api/v1/favorites/getByUserId/${userId}`)
        .then(favoriteList => {
            favoriteList.forEach(favorite => {
                getRequest(`https://localhost:58841/api/v1/products/getById/${favorite.productId}`)
                    .then(product => {
                        getRequest(`http://localhost:5098/api/v1/users/${product.userId}`)
                            .then(seller => {
                                productContainer.insertAdjacentHTML('beforeend', insertCardProductFavorite(product, seller.login));
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

        getRequest(`http://localhost:5098/api/v1/users/getByLogin/${seller}`)
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
