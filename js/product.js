let sellerLogin = localStorage.getItem('userLogin');
let productName = localStorage.getItem('productName');
let userId = localStorage.getItem('userId');
const sizesProduct = document.querySelector('.sizes-product');
const descriptionButton = document.getElementById('description-product-btn');
const productDescription = document.getElementById('product-description-card');
const progressBar = document.querySelector('.progress-bar');
const countProduct = document.getElementById('product-count-card');
const addPrdouctToCartBtn = document.getElementById('add-product-cart');
let hideDescription = 0;

fillCardProduct();

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

    if (!response.ok) {
        const errorResponse = await response.json();
        console.error("Ошибка ответа:", errorResponse);
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    let result = await response.status;
    return result;
}

function fillProduct(product) {
    const productTitle = document.getElementById('product-name-card');
    const productPrice = document.getElementById('product-price-card');
    const seller = document.getElementById('seller-product');
    const imgProduct = document.getElementById('product-img-card');

    productTitle.textContent = product.name;
    productPrice.textContent = `${product.price} ₽`;
    seller.textContent = sellerLogin;
    productDescription.textContent = product.description;
    imgProduct.src = product.imageUrl;
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
                                        calculatePercentageOfProduct(sizeObj.count);
                                        countProduct.textContent = `Осталось ${sizeObj.count} шт.`;
                                    }
                                })
                            })
                    })
            })
    }
})

function calculatePercentageOfProduct(count) {
    let percent = count / 100;
    percent *= 100;
    progressBar.style.width = `${percent}%`;
}

addPrdouctToCartBtn.addEventListener('click', () => {
    getRequest(`https://localhost:5098/api/v1/users/getByLogin/${sellerLogin}`)
        .then(seller => {
            getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${productName}/${seller.id}`)
                .then(product => {
                    let activeSize = document.querySelector('.active-size');

                    if(activeSize != null) {
                        getRequest(`https://localhost:58841/api/v1/productSizes/getByProductIdAndSize/${product.id}/${activeSize.textContent}`)
                            .then(ps => {
                                let cartCreateModel = {
                                    productId: parseInt(product.id),
                                    userId: parseInt(seller.id),
                                    productSizesId: parseInt(ps.id)
                                };
                            
                                postRequest(`https://localhost:7073/api/v1/carts/create`, cartCreateModel)
                                .then(code => {
                                    if(code == 201) {
                                        localStorage.setItem('activeSize', activeSize.textContent);
                                        alert('Вы добавили товар в корзину');
                                    }
                                })
                            })
                    } else {
                        alert('Выберите размер, для того чтобы добавить товар в корзину!');
                    }    
                })
        })
})