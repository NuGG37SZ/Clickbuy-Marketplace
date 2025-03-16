const currentUserId = localStorage.getItem('userId');
const ratingContainer = document.getElementById('rating-container');
const commentDiv = document.querySelectorAll('.card-right-panel')[2];
const modalWindowBody = document.querySelector('.modal-window-body');
let prodcutIdUpdate;
let productSizesIdUpdate;
let orderIdUpdate;
let starCount;

document.addEventListener('DOMContentLoaded', () => {
    insertAllCardRating();
})

function insertCardComment(product, productSizes, order) {
    return `
        <div class="card-rating">
            <div class="card-rating-body">
                <div style="position: relative;">
                    <img src="${product.imageUrl}" width="200px" height="200px">
                    <p style="margin-bottom: 0;" id="title-product-rating">${product.name}</p>
                    <p style="font-size: 14px; color: grey; margin-bottom: 0;" id="size-product-rating">Размер ${productSizes.size}</p>
                    <p style="font-size: 14px; color: grey; margin-bottom: 0;" id="num-order-rating">Заказ №${order.id}</p>
                    <p style="display: none;" id="product-id-rating">${product.id}</p>
                </div>
                <div style="width: 130px; height: 30px; position: absolute; top: 185px;  border-radius: 10px; 
                    display: flex; flex-direction: row; align-items: flex-start; justify-content: center;" id="star-div">
                        <a href="#" class="rating-icons openModalBtn" data-star="1"><i class="bi bi-star"></i></a>
                        <a href="#" class="rating-icons openModalBtn" data-star="2"><i class="bi bi-star"></i></a>
                        <a href="#" class="rating-icons openModalBtn" data-star="3"><i class="bi bi-star"></i></a>
                        <a href="#" class="rating-icons openModalBtn" data-star="4"><i class="bi bi-star"></i></a>
                        <a href="#" class="rating-icons openModalBtn" data-star="5"><i class="bi bi-star"></i></a>
                    </div>
                </div>
            </div>
        </div>
    `
}

function insertProductRating(product) {
    return `
        <img src="${product.imageUrl}" width="400px" style="margin: 0 auto;">
            <p style="font-size: 14px; color: grey; margin-bottom: 0;">${product.name}</p>
            <div>
                <label>Комментарии</label>
                <textarea style="width: 450px; height: 100px;" id="comment-text"></textarea>
            </div>
        <button type="button" class="btn btn-success" style="width: 450px; margin-top: 15px;" id="save-rating-product">Сохранить</button>
    `
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
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

async function getRatingProductByUserId(userId) {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct/getByUserId/${userId}`);
    return ratingProductList;
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;
}

async function getProductSizesByProductIdAndSize(productId, size) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getByProductIdAndSize/${productId}/${size}`);
    return productSizes;
}

async function getOrderById(id) {
    const order = await getRequest(`https://localhost:7049/api/v1/orders/getById/${id}`);
    return order;
}

async function getByProductIdAndProductSizesIdAndOrderId(productId, productSizesId, orderId) {
    const order = await 
        getRequest(`https://localhost:7029/api/v1/ratingProduct/getByProductIdAndProductSizesIdAndOrderId/${productId}/${productSizesId}/${orderId}`);
    return order;
}

async function insertAllCardRating() {
    let ratingProductList = await getRatingProductByUserId(parseInt(currentUserId));

    for (const ratingProduct of ratingProductList) {
        let product = await getProductById(ratingProduct.productId);
        let productSizes = await getProductSizesById(ratingProduct.productSizesId);
        let order = await getOrderById(ratingProduct.orderId);
        
        if(order.status == 'Получен' && ratingProduct.comment == '') {
            ratingContainer.insertAdjacentHTML('beforeend', insertCardComment(product, productSizes, order));
        }
    }
}

commentDiv.addEventListener('click', () => {
    const allCards = document.querySelectorAll('.card-rating');
    const modal = document.getElementById('modal');
    const openModalBtnList = document.querySelectorAll('.rating-icons.openModalBtn');
    const closeModalBtn = document.getElementById('closeModalButton');

    allCards.forEach(card => {
        const stars = card.querySelectorAll('.rating-icons');
    
        stars.forEach(star => {
            star.addEventListener('mouseenter', () => {
                const rating = star.getAttribute('data-star');
                highlightStars(stars, rating);
            });
    
            star.addEventListener('click', async () => {
                const rating = star.getAttribute('data-star');
                highlightStars(stars, rating);
    
                starCount = rating;
    
                let productId = parseInt(card.querySelector('#product-id-rating').textContent);
                let size = replaceAllLetters(card.querySelector('#size-product-rating').textContent);
                let orderId = replaceAllLetters(card.querySelector('#num-order-rating').textContent);
    
                let product = await getProductById(productId);
                let productSizes = await getProductSizesByProductIdAndSize(product.id, size.toString());
                modalWindowBody.insertAdjacentHTML('beforeend', insertProductRating(product));
    
                prodcutIdUpdate = product.id;
                orderIdUpdate = orderId;
                productSizesIdUpdate = productSizes.id;
            });
        });
    
        card.addEventListener('mouseleave', () => {
            resetStars(stars);
        });
    });
    
    function highlightStars(stars, rating) {
        stars.forEach(star => {
            const starRating = star.getAttribute('data-star');
            if (starRating <= rating) {
                star.classList.add('active'); 
            } else {
                star.classList.remove('active');
            }
        });
    }
    
    function resetStars(stars) {
        stars.forEach(star => {
            star.classList.remove('active');
        });
    }

    openModalBtnList.forEach(openModalBtn => {
        openModalBtn.addEventListener('click', () => {
            const modalContent = document.querySelector('.modal-content');
            modalContent.remove();
            modal.style.display = 'block';
        });
    })
 
    closeModalBtn.addEventListener('click', () => {
        location.reload();
    });
})

function replaceAllLetters(str) {
    return parseInt(str.replace(/\D+/g, ''));
}

modalWindowBody.addEventListener('click', async (event) => {
    if(event.target.closest('#save-rating-product')) {
        let commentText = document.getElementById('comment-text').value;

        let ratingProduct = await getByProductIdAndProductSizesIdAndOrderId(prodcutIdUpdate, productSizesIdUpdate, orderIdUpdate);
        ratingProduct.comment = commentText;
        ratingProduct.rating = parseFloat(starCount);

        let code = await putRequest(`https://localhost:7029/api/v1/ratingProduct/update/${ratingProduct.id}`, ratingProduct);

        if(code == 200) alert('Комментарии успешно добавлен!');
    }
})