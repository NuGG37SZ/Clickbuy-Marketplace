const productContainer = document.querySelector('.row');
let productCard = document.querySelector('.product-card');
const categoriesDiv = document.querySelector('#categoriesAccordion');

document.addEventListener('DOMContentLoaded', () => {
    insertCards();
    if(userId == null) addressPointBtn.value = 'Пункт выдачи';
})

insertAllCategories();

function insertCardProductIndex(product, seller, inFavorite, rating, comment) {
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

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getAllCategories() {
    const categoriesList = await getRequest(`https://localhost:58841/api/v1/categories`);
    return categoriesList;
}

async function getAllSubcategoriesByCategoryId(categoryId) {
    const subCategoriesList = await getRequest(`https://localhost:58841/api/v1/subcategories/getSubcategoryByCategoryId/${categoryId}`);
    return subCategoriesList;
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

async function getSubcategoriesByName(name) {
    const subCategories = await getRequest(`https://localhost:58841/api/v1/subcategories/getSubcategoriesByName/${name}`);
    return subCategories;
}

async function getBrandSubcategoriesListBySubcategoryId(subcategoryId) {
    const brandsSubcategoriesList = await getRequest(`https://localhost:58841/api/v1/brandsSubcategories/getBySubcategoriesId/${subcategoryId}`);
    return brandsSubcategoriesList;
}

async function getProductListByBrandSubcategoryId(brandSubcategoryId) {
    const productList = await getRequest(`https://localhost:58841/api/v1/products/getByBrandSubcategoryId/${brandSubcategoryId}`);
    return productList;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function postRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(obj)
    });

    if (!response.ok) {
        const errorResponse = await response.json();
        console.error("Ошибка ответа:", errorResponse);
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    let result = await response.status;
    return result;
}

function insertCards() {
    getRequest(`https://localhost:58841/api/v1/products`)
        .then(productsList => {
            productsList.forEach(product => {
                getRequest(`https://localhost:5098/api/v1/users/${product.userId}`)
                    .then(seller => {
                        checkFavoriteProduct(seller, product);
                    })
            });
        })
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
                        insertCardProductIndex(product, seller.login, true, ratingProductAvg, commentCount)
                    );
                } else {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductIndex(product, seller.login, false, ratingProductAvg, commentCount)
                    );
                }
                
            } else {
                if (fp && fp.productId === product.id) {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductIndex(product, seller.login, true, 0, 0)
                    );
                } else {
                    productContainer.insertAdjacentHTML('beforeend', 
                        insertCardProductIndex(product, seller.login, false, 0, 0)
                    );
                }
            }
        })
}

productContainer.addEventListener('click', function(event) {
    if (event.target.closest('.favorite')) {
        const icon = event.target.closest('.favorite'); 
        const card = icon.parentElement; 
        const cardName = card.querySelector('.product-title').textContent;
        const seller = card.querySelector('.product-creater').textContent;
        const imgFavorite = card.querySelector('.favorite-img');

        getRequest(`https://localhost:5098/api/v1/users/getByLogin/${seller}`)
            .then(s => {
                getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${cardName}/${s.id}`)
                    .then(p => {
                        checkAddFavoriteProduct(p, imgFavorite);
                    });
            });
    }

    if(event.target.closest('.product-info')) {
        const productInfo = event.target.closest('.product-info');
        const productName = productInfo.querySelector('.product-title').textContent;
        const userLogin = productInfo.querySelector('.product-creater').textContent;
        localStorage.setItem('userLogin', userLogin);
        localStorage.setItem('productName', productName);
        location.href = 'product.html';
    }
});

function checkAddFavoriteProduct(product, img) {
    getRequest(`https://localhost:7073/api/v1/favorites/getByUserId/${userId}`)
        .then(fpList => {
            const isAlreadyInFavorites = fpList.some(fp => fp.productId === product.id);
            if (isAlreadyInFavorites) {
                alert('Этот товар уже был добавлен в ваше избранное!');
            } else {
                img.src = 'source/heart.png';
                addProductToWishList(product);
            }
        })
}

function addProductToWishList(product) {
    if(userId == null) {
        alert('Войдите в аккаунт, чтобы добавить товар в избранное!');
    } else {
        const now = new Date();

        let cartModel = {
            userId: userId,
            productId: product.id,
            dateAdded: now
        }
        postRequest(`https://localhost:7073/api/v1/favorites/create`, cartModel);
    }
}

function prevImage(index) {
    let images = document.querySelectorAll('.product-images')[index].querySelectorAll('img');
    let activeIndex = [...images].findIndex(img => img.classList.contains('active'));
    images[activeIndex].classList.remove('active');
    let newIndex = activeIndex === 0 ? images.length - 1 : activeIndex - 1;
    images[newIndex].classList.add('active');
}

function nextImage(index) {
    let images = document.querySelectorAll('.product-images')[index].querySelectorAll('img');
    let activeIndex = [...images].findIndex(img => img.classList.contains('active'));
    images[activeIndex].classList.remove('active');
    let newIndex = activeIndex === images.length - 1 ? 0 : activeIndex + 1;
    images[newIndex].classList.add('active');
}

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".reviews").forEach((element) => {
        let count = parseInt(element.getAttribute("data-count"), 10);
        element.textContent = `💬 ${count} ${declineReviews(count)}`;
    });
});

function declineReviews(number) {
    if (number % 10 === 1 && number % 100 !== 11) {
        return "отзыв";
    } else if ([2, 3, 4].includes(number % 10) && ![12, 13, 14].includes(number % 100)) {
        return "отзыва";
    } else {
        return "отзывов";
    }
}

function insertCategory(category) {
    return `
                <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${category.id}">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" 
                            data-bs-target="#collapse${category.id}" aria-expanded="false" aria-controls="collapse${category.id}">
                            ${category.name}    
                        </button>
                    </h2>
                    <div id="collapse${category.id}" class="accordion-collapse collapse" aria-labelledby="heading${category.id}" 
                        data-bs-parent="#categoriesAccordion">
                        <div class="accordion-body">
                            
                        </div>
                    </div>
                </div>
    `
}

function insertSubCategory(subcategory) {
    return `<a href="#" class="subcategory-link">${subcategory.name}</a><br>`
}

async function insertAllCategories() {
    let categoriesList = await getAllCategories();

    for (const category of categoriesList) {
        categoriesDiv.insertAdjacentHTML('beforeend', insertCategory(category));
        let accordionBody = categoriesDiv.querySelectorAll('.accordion-body');
        let lastAccordionBody = accordionBody[accordionBody.length - 1];
        let subcategoryList = await getAllSubcategoriesByCategoryId(category.id);
        
        for (const subcategory of subcategoryList) {
            lastAccordionBody.insertAdjacentHTML('beforeend', insertSubCategory(subcategory));
        }
    }   
}

categoriesDiv.addEventListener('click', async (event) => {
    if(event.target.closest('.subcategory-link')) {
        productContainer.innerHTML = '';
        let subcategoryName = event.target.closest('.subcategory-link').textContent;
        let subcategories = await getSubcategoriesByName(subcategoryName);
        let brandsSubcategoriesList = await getBrandSubcategoriesListBySubcategoryId(subcategories.id);

        for (const brandSubcategory of brandsSubcategoriesList) {
            let productList = await getProductListByBrandSubcategoryId(brandSubcategory.id);
            console.log(productList[0]);
            
            for(const product of productList) {
                let user = await getUserById(product.userId);
                checkFavoriteProduct(user, product);
            }
        }
    }
})
