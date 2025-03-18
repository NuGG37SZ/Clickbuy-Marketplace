const commentDiv = document.querySelector('.comment-div');
const searchCommentBtn = document.querySelector('#search-comment-btn');
const productSelectComment = document.querySelector('#product-select-comment');
const dateCreateCommentInpt = document.querySelector('#date-create-comment-inpt');
const userCommentInpt = document.querySelector('#users-comment-inpt');

insertAllComments();

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

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

async function getAllRatignProduct() {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct`);
    return ratingProductList;
}

async function getRatingProductByUserIdAndProductId(userId, productId) {
    const ratingProduct = 
        await getRequest(
            `https://localhost:7029/api/v1/ratingProduct/getByUserIdAndProductId/${userId}/${productId}`
        );
    return ratingProduct;
}


async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;
}

async function getProductByNameAndUserId(name, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${name}/${userId}`);
    return product;
}

async function getProductByUserId(userId) {
    const productList = await getRequest(`https://localhost:58841/api/v1/products/getByUserId/${userId}`);
    return productList;
}

function insertComment(user, ratingProduct, product) {
    return `
        <div style="border: 1px solid; padding: 15px; border-radius: 20px; margin-top: 20px;" class="comment">
            <p><b>Товар №${product.id}</b></p>
            <div style="display: flex; justify-content: space-between;">
                <p><b>${user.login}</b></p>
                <p>${ratingProduct.dateCreateComment}</p>
            </div>
            <p style="font-size: 14px; color: gray;">${product.name}<p>
            <div style="display: flex; max-width: 400px;">
                <p>${ratingProduct.comment}</p>
            </div>
            <div style="display: flex; justify-content: end;">
                <button class="btn btn-outline-danger" id="delete-comment-moderator"><i class="bi bi-x-circle-fill"></i></button>
            </div>
        </div>
    `
}

async function insertAllComments() {
    const ratingProductList = await getAllRatignProduct();

    for (const ratingProduct of ratingProductList) {
        if(ratingProduct.comment != '') {
            const user = await getUserById(ratingProduct.userId);
            const product = await getProductById(ratingProduct.productId);
            commentDiv.insertAdjacentHTML('beforeend', insertComment(user, ratingProduct, product));
        }
    }
}

async function fillProductSelect(product) {
    productSelectComment.append(new Option(product.name, product.id));
}

userCommentInpt.addEventListener('change', async () => {
    productSelectComment.options.length = 0;
    let user = await getUserByLogin(userCommentInpt.value);
    let productList = await getProductByUserId(user.id);

    for (const product of productList) {
        fillProductSelect(product);
    }
})

searchCommentBtn.addEventListener('click', async () => {
    commentDiv.innerHTML = '';
    let productId = parseInt(productSelectComment.value);
    let userLogin = userCommentInpt.value;

    let user = await getUserByLogin(userLogin);
    let product = await getProductById(productId);
    let ratingProductList = await getRatingProductByUserIdAndProductId(user.id, product.id);

    for (const ratingProduct of ratingProductList) {
        if(ratingProduct.comment != '')
            commentDiv.insertAdjacentHTML('beforeend', insertComment(user, ratingProduct, product));
    }
})

commentDiv.addEventListener('click', async (event) => {
    if(event.target.closest("#delete-comment-moderator")) {
        let confirmWindow = confirm('Вы точно хотите удалить комментарии?');
        
        if(confirmWindow) {
            let ratingProductCard = event.target.closest("#delete-comment-moderator")
            .parentNode.parentNode;
            let productId = parseInt(ratingProductCard.children[0].textContent.replace(/\D+/g, ''));
            let userLogin = ratingProductCard.children[1].children[0].textContent;
            let description = ratingProductCard.children[4].children[0].textContent;
            let user = await getUserByLogin(userLogin);
            let ratingProductList = await getRatingProductByUserIdAndProductId(user.id, productId);
    
            for (const ratingProduct of ratingProductList) {
                if(ratingProduct.comment == description && 
                    ratingProduct.userId == user.id &&
                    ratingProduct.productId == productId) {
                    await deleteRequest(`https://localhost:7029/api/v1/ratingProduct/delete/${ratingProduct.id}`);
                    ratingProductCard.remove();
                    return;
                }
            }
        }
    }
})

