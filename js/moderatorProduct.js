const productsDiv = document.querySelector('.cards-product-div');
const productUserInpt = document.querySelector('#product-user-inpt');
const productNameSelect = document.querySelector('#product-name-select');
const searchProductModerBtn = document.querySelector('#search-product-moder-btn');

insertAllCard();

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getAllProducts() {
    const productList = await getRequest(`https://localhost:58841/api/v1/products`);
    return productList;
}

async function getProductByNameAndUserId(name, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${name}/${userId}`);
    return product;
}

async function getProductListByUserId(userId) {
    const productList = await getRequest(`https://localhost:58841/api/v1/products/getByUserId/${userId}`);
    return productList;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

function insertCard(product, user) {
    return `
        <div class="card mb-3" style="max-width: 540px; margin-top: 30px;">
            <div class="row g-0">
                <div class="col-md-4">
                    <img src="${product.imageUrl}" class="img-fluid rounded-start" alt="...">
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title">${user.login}</h5>
                        <h5 class="card-title">${product.name}</h5>
                        <p class="card-text">${product.description}</p>
                        <p class="card-text" id="price-product">Цена: ${product.price}₽</p>  
                        <div style="display: flex; justify-content: end;">
                            <button class="btn btn-outline-danger" id="delete-product"><i class="bi bi-x-circle-fill"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
}

async function insertAllCard() {
    let productList = await getAllProducts();

    for (const product of productList) {
        let user = await getUserById(product.userId);
        productsDiv.insertAdjacentHTML('beforeend', insertCard(product, user));
    }
}

productUserInpt.addEventListener('change', async () => {
    let user = await getUserByLogin(productUserInpt.value);
    let productList = await getProductListByUserId(user.id);

    for (const product of productList) {
        fillProductSelect(product);
    }
})

function fillProductSelect(product) {
    productNameSelect.append(new Option(product.name, product.name));
}

searchProductModerBtn.addEventListener('click', async () => {
    productsDiv.innerHTML = '';
    let user = await getUserByLogin(productUserInpt.value);
    let productName = productNameSelect.value;
    let product = await getProductByNameAndUserId(productName, user.id);
    productsDiv.insertAdjacentHTML('beforeend', insertCard(product, user));
})

productsDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#delete-product')) {
        let confirmWindow = confirm('Вы точно хотите удалить товар?');
        
        if(confirmWindow) {
            let cardProduct = event.target.closest('#delete-product').parentNode.parentNode;
            let userLogin = cardProduct.children[0].textContent;
            let user = await getUserByLogin(userLogin);
            let productName = cardProduct.children[1].textContent;
            let product = await getProductByNameAndUserId(productName, user.id);

            await deleteRequest(`https://localhost:58841/api/v1/products/delete/${product.id}`);
            cardProduct.remove();
        }
    }
})


