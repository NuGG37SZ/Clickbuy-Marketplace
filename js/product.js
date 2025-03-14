let sellerLogin = localStorage.getItem('userLogin');
let productName = localStorage.getItem('productName');
let userId = localStorage.getItem('userId');
const sizesProduct = document.querySelector('.sizes-product');
const descriptionButton = document.getElementById('description-product-btn');
const productDescription = document.getElementById('product-description-card');
const progressBar = document.querySelector('.progress-bar');
const countProduct = document.getElementById('product-count-card');
const addPrdouctToCartBtn = document.getElementById('add-product-cart');
const allReviews = document.querySelector('.all-reviews');
let hideDescription = 0;

fillCardProduct();
insertAllComments();

function fillCardProduct() {
    getRequest(`https://localhost:5098/api/v1/users/getByLogin/${sellerLogin}`)
        .then(seller => {
            getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${productName}/${seller.id}`)
                .then(product => {
                    fillProduct(product);
        
                    getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${product.id}`)
                        .then(sizes => {
                            sizes.sort((a, b) => a.size - b.size);
                            sizes.forEach(size => {
                                sizesProduct.insertAdjacentHTML('beforeend', insertSizeProduct(size.size));
                            });
                        })
                })
        })
}

function insertSizeProduct(size) {
    return `
            <div class="size-product">
                <p>${size}</p>
            </div>
        ` 
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function postRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(obj)
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

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getProductByNameAndUserId(name, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${name}/${userId}`);
    return product;
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;
}

async function getRatingProductListByProductId(productId) {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getByProdcutId/${productId}`);
    return ratingProductList;
}

async function getAvgRatingByProductId(productId) {
    const ratingProductSum = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getAvgRatingByProductId/${productId}`);
    return ratingProductSum;
}

async function fillProduct(product) {
    const productTitle = document.getElementById('product-name-card');
    const productPrice = document.getElementById('product-price-card');
    const seller = document.getElementById('seller-product');
    const imgProduct = document.getElementById('product-img-card');
    const commentProduct = document.querySelector('.comments-product');
    const startProduct = document.querySelector('.star-product');
    let ratingProductList = await getRatingProductListByProductId(product.id);

    productTitle.textContent = product.name;
    productPrice.textContent = `${product.price} ₽`;
    seller.textContent = sellerLogin;
    productDescription.textContent = product.description;
    imgProduct.src = product.imageUrl;

    if(ratingProductList.length != 0) {
        let ratingProductAvg = await getAvgRatingByProductId(product.id);
        commentProduct.textContent = `💬 ${ratingProductList.length}`;
        startProduct.textContent = `⭐ ${ratingProductAvg}`;
    } else {
        commentProduct.textContent = `💬 ${0}`;
        startProduct.textContent = `⭐ ${0}`;
    }
}

descriptionButton.addEventListener('click', () => {
    hideDescription++;

    if(hideDescription == 1) {
        productDescription.style.display = 'block';
    } 

    if(hideDescription == 2) {
        productDescription.style.display = 'none';
        hideDescription = 0;
    }
})

sizesProduct.addEventListener('click', (event) => {
    if(event.target.closest('.size-product')) {
        let activeSize = document.querySelector('.active-size');
        let currentSize = event.target.closest('.size-product').children[0];
        let size = parseInt(currentSize.textContent);

        if(activeSize) activeSize.classList.remove('active-size');
        
        getRequest(`https://localhost:5098/api/v1/users/getByLogin/${sellerLogin}`)
            .then(seller => {
                getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${productName}/${seller.id}`)
                    .then(product => {
                        getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${product.id}`)
                            .then(productSizeList => {
                                productSizeList.forEach(sizeObj => {
                                    if(size == parseInt(sizeObj.size)) {
                                        currentSize.classList.add('active-size');
                                        progressBar.style.width = `${sizeObj.count}%`;
                                        countProduct.textContent = `Осталось ${sizeObj.count} шт.`;
                                    }
                                })
                            })
                    })
            })
    }
})

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getProductByNameAndUserId(productName, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${productName}/${userId}`);
    return product;
}

async function getProductSizesByProductIdAndSize(productId, size) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getByProductIdAndSize/${productId}/${size}`);
    return productSizes;
}

async function getAllCardsByCurrentUserId() {
    let userId = localStorage.getItem('userId');
    const cartList = await getRequest(`https://localhost:7073/api/v1/carts/getByUserId/${parseInt(userId)}`);
    return cartList;
}

async function getRatingProductByProductId(productId) {
    const ratingProductList = await getRequest(`https://localhost:58841/api/v1/ratingProduct/getByProdcutId/${productId}`);
    return ratingProductList;
}

addPrdouctToCartBtn.addEventListener('click', async () => {
    const seller = await getUserByLogin(sellerLogin);
    const product = await getProductByNameAndUserId(productName, seller.id);   
    let activeSize = document.querySelector('.active-size');

    if(activeSize != null) {
        const productSizes = await getProductSizesByProductIdAndSize(product.id, activeSize.textContent);
        const cartList = await getAllCardsByCurrentUserId();

        let cartCreateModel = {
            productId: product.id,
            userId: seller.id,
            productSizesId: productSizes.id,
            count: 1,
        };

        for (const cart of cartList) {
            if(cart.productId == cartCreateModel.productId && 
               cart.userId == cartCreateModel.userId &&
               cart.productSizesId == cartCreateModel.productSizesId) 
            {
                ++cartCreateModel.count;
                await putRequest(`https://localhost:7073/api/v1/carts/update/${cart.id}`, cartCreateModel);
                return;  
            }
        }

        postRequest(`https://localhost:7073/api/v1/carts/create`, cartCreateModel)
            .then(code => {
                if(code == 201) {
                    alert('Вы добавили товар в корзину');
                }
            })
    } else {
        alert('Выберите размер, для того чтобы добавить товар в корзину!');
    }    
})

function insertComment(productSizes, ratingProduct, user) {
    return `
            <div class="review-div">
                <div class="header-review">
                    <div class="user-review">
                        <img src="source/profile-user.png" width="64px" height="64px">
                        <div class="info-user-review">
                            <p style="font-size: 20px;">${user.login}</p>
                            <p style="font-size: 14px; color: gray;">Выкупили, Размер: ${productSizes.size}</p>
                        </div>
                    </div>
                    <div>
                        <div class="date-and-rating" style="display: flex;">
                            
                        </div>
                        <p style="font-size: 18px; color: gray; text-align: end;">${getDate(ratingProduct.dateCreateComment)}</p>
                    </div>
                </div>
                <div class="text-review">
                    <p>${ratingProduct.comment}</p>
                </div>
            </div>
    `
}

function insertFillStar() {
    return `<img src="source/starYellow.png" class="rating-product" width="24px" style="margin-left: 5px">`
}

function insertEmptyStar() {
    return `<img src="source/starEmpty.png" class="rating-product" width="24px" style="margin-left: 5px">`
}

function getDate(dateStr) {
    const dateFirst = moment(dateStr, "YYYY-MM-DD HH:mm:ss");
    return dateFirst.format('DD.MM.YYYY');
}

async function insertAllComments() {
    let user = await getUserByLogin(sellerLogin);
    let product = await getProductByNameAndUserId(productName, user.id);
    let ratingProductList = await getRatingProductListByProductId(product.id);

    for (const ratingProduct of ratingProductList) {
        if(ratingProduct.comment != '' && ratingProduct.rating != 0.0) {
            let productSizes = await getProductSizesById(ratingProduct.productSizesId);
            let user = await getUserById(ratingProduct.userId);
            allReviews.insertAdjacentHTML('beforeend', insertComment(productSizes, ratingProduct, user));
            let dateAndRatingDivAll = allReviews.querySelectorAll('.date-and-rating');
            let dateAndRatingDivLast = dateAndRatingDivAll[dateAndRatingDivAll.length - 1];
            
            for(let i = 0; i < ratingProduct.rating; i++) {
                dateAndRatingDivLast.insertAdjacentHTML('beforeend', insertFillStar())
            }
    
            for(let j = ratingProduct.rating; j < 5; j++) {
                dateAndRatingDivLast.insertAdjacentHTML('beforeend', insertEmptyStar())
            }
        }
    }
}

