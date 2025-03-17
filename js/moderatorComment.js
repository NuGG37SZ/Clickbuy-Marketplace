const commentDiv = document.querySelector('.comment-div');

insertAllComments();

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getAllRatignProduct() {
    const ratingProductList = await getRequest(`https://localhost:7029/api/v1/ratingProduct`);
    return ratingProductList;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;
}

function insertComment(user, ratingProduct, product) {
    return `
        <div style="border: 1px solid; padding: 15px; border-radius: 20px; margin-top: 20px;" class="comment">
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

