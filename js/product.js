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

addPrdouctToCartBtn.addEventListener('click', async () => {
    const seller = await getUserByLogin(sellerLogin);
    const product = await getProductByNameAndUserId(productName, seller.id);   
    let activeSize = document.querySelector('.active-size');

    if(activeSize != null) {
        const productSizes = await getProductSizesByProductIdAndSize(product.id, activeSize.textContent);
            let cartCreateModel = {
                productId: product.id,
                userId: seller.id,
                productSizesId: productSizes.id
            };

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