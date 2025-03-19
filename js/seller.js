const addUpdProductBtn = document.getElementById('add-upd-productBtn');
const listProductBtn = document.getElementById('list-productBtn');
const listOrderBtn = document.getElementById('list-ordersBtn');
const allOrderSellerDiv = document.querySelector('.all-order-seller');
const formAddUpdProduct = document.getElementById('create-update-product');
const allProductsDiv = document.querySelector('.all-product-seller');
const createBtnForm = document.getElementById('create-product');
const updateBtnForm = document.getElementById('update-product');
let brandSelect = document.getElementById('brand');
const productSelect = document.getElementById('products');
const categorySelect = document.getElementById('category');
let subcategorySelect = document.getElementById('subcategory');
const productSellerDiv = document.querySelector('.products-seller');
const deleteProductBtn = document.getElementById('delete-product');
const orderSellerDiv = document.querySelector('.orders-seller');
const userId = localStorage.getItem('userId');
const searchItemsSeller = document.getElementById('search-item-seller');
const filterOrders = document.querySelector('.filter-orders');
const numberOrderFilterInpt = document.querySelector('.num-order-filter');

document.addEventListener('DOMContentLoaded', () => {
    fillProductsSelect();
    fillBrandSelect();
    fillCategoriesSelect();
    startElements();
})

function startElements() {
    formAddUpdProduct.style.display = 'block';
    allProductsDiv.style.display = 'none';
    allOrderSellerDiv.style.display = 'none';
}

addUpdProductBtn.addEventListener('click', () => {
    formAddUpdProduct.style.display = 'block';
    allProductsDiv.style.display = 'none';
    allOrderSellerDiv.style.display = 'none';
})

listProductBtn.addEventListener('click', () => {
    allProductsDiv.style.display = 'block';
    formAddUpdProduct.style.display = 'none';
    allOrderSellerDiv.style.display = 'none';
})

listOrderBtn.addEventListener('click', () => {
    allOrderSellerDiv.style.display = 'block';
    allProductsDiv.style.display = 'none';
    formAddUpdProduct.style.display = 'none';
})

async function getBrandSubcategoriesByBrandIdAndSubcategoriesId(brandId, subcategoriesId) {
    const brandsSubcategories = 
        await getRequest(
            `https://localhost:58841/api/v1/brandsSubcategories/getByBrandAndSubcategories/${brandId}/${subcategoriesId}`
        );
    return brandsSubcategories;
}

async function getProductByNameAndUserId(name, userId) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${name}/${userId}`);
    return product;
}

async function getProductSizesListByProductId(productId) {
    const productSizeList = await getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${productId}`);
    return productSizeList;
}

async function getBrandSubcategoriesById(id) {
    const brandSubcategories = await getRequest(`https://localhost:58841/api/v1/brandsSubcategories/getById/${id}`);
    return brandSubcategories;
}

async function getSubcategoriesById(id) {
    const subcategories = await getRequest(`https://localhost:58841/api/v1/subcategories/getById/${id}`);
    return subcategories;
}

async function getCategoryById(id) {
    const category = await getRequest(`https://localhost:58841/api/v1/categories/getById/${id}`);
    return category;
}

createBtnForm.addEventListener('click', async () => {
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let countInpt = document.getElementById('count');
    let descriptionArea = document.getElementById('description');
    let fileInpt = document.getElementById('image-product');
    let sizeInpt = document.getElementById('size');
    let confirmWindow = confirm('Вы точно хотите добавить новый товар?');

    if(confirmWindow) {
        const brandsSubcategories = await getBrandSubcategoriesByBrandIdAndSubcategoriesId(brandSelect.value, subcategorySelect.value);
        let file = fileInpt.files[0];
        let reader = new FileReader();
        reader.readAsDataURL(file);
                
        reader.onload = async (event) => {
            let imageUrl = event.target.result;

            let productCreateModel = {
                userId: parseInt(userId),
                brandsSubcategoriesId: brandsSubcategories.id,
                name: nameInpt.value,
                price: parseInt(priceInpt.value),
                count: parseInt(countInpt.value),
                description: descriptionArea.value,
                imageUrl: imageUrl
            }

            let code = await postRequest(`https://localhost:58841/api/v1/products/create`, productCreateModel);
            if(code === 201) {
                let product = await getProductByNameAndUserId(nameInpt.value, userId);
                let sizeText = sizeInpt.value;
                let sizes = sizeText.split(', ');
                let countText = countInpt.value;
                let counts = countText.split(', ');

                if(sizes.length == counts.length) {
                    for(let i = 0; i < sizes.length; i++) {
                        let currentSize = sizes[i];
                        let currentCount = parseInt(counts[i]);

                        if(currentCount <= 100) {
                            let productSizes = {
                                productId: parseInt(product.id),
                                size: currentSize,
                                count: currentCount,
                            }
                            await postRequest(`https://localhost:58841/api/v1/productSizes/create`, productSizes);
                        } else {
                            alert('Максимальное количество товаров 100!');
                        }
                    }
                }
                alert('Вы успешно добавили товар!');
                location.reload(); 
            }
        }            
    }
})

productSelect.addEventListener('change', async () => {
    let id = productSelect.value;
    let product = await getProductById(id);
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let descriptionArea = document.getElementById('description');
    let sizeInpt = document.getElementById('size');
    let countInpt = document.getElementById('count');

    nameInpt.value = product.name;
    priceInpt.value = product.price;
    descriptionArea.value = product.description;
    sizeInpt.value = '';
    countInpt.value = '';
    
    let productSizesList = await getProductSizesListByProductId(product.id);
    for (const productSizes of productSizesList) {
        sizeInpt.value += productSizes.size + ', ';
        countInpt.value += productSizes.count + ', ';
    }
    sizeInpt.value = sliceString(sizeInpt.value);
    countInpt.value = sliceString(countInpt.value);
    
    let brandsSubcategories = await getBrandSubcategoriesById(product.brandsSubcategoriesId)
    let brandId = brandsSubcategories.brandsId;
    let subcategoriesId = brandsSubcategories.subcategoriesId;
    let subcategories = await getSubcategoriesById(subcategoriesId);
    let category = await getCategoryById(subcategories.categoryId);

    categorySelect.value = category.id;
    subcategorySelect.options.length = 0;
    brandSelect.value = brandId;
    fillSubcategoriesSelect(category.id);            
})

updateBtnForm.addEventListener('click', async () => {
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let descriptionArea = document.getElementById('description');
    let fileInpt = document.getElementById('image-product');
    let userId = localStorage.getItem('userId');
    let brandsSubcategories = await getBrandSubcategoriesByBrandIdAndSubcategoriesId(brandSelect.value, subcategorySelect.value);
    let product = await getProductById(productSelect.value);

    if(fileInpt.files[0] == undefined) {
        let updateProductModel = {
            userId: parseInt(userId),
            brandsSubcategoriesId: brandsSubcategories.id,
            name: nameInpt.value,
            price: parseInt(priceInpt.value),
            description: descriptionArea.value,
            imageUrl: product.imageUrl
        }
        let code = await putRequest(`https://localhost:58841/api/v1/products/update/${productSelect.value}`, updateProductModel)
        if(code == 200) updateProductSizes(code, product)
                
    } else {
        let file = fileInpt.files[0];
        let reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = async (event) => {
            let imageUrl = event.target.result;

            let updateProductModel = {
                userId: parseInt(userId),
                brandsSubcategoriesId: bs.id,
                name: nameInpt.value,
                price: parseInt(priceInpt.value),
                description: descriptionArea.value,
                imageUrl: imageUrl
            }

            let code = await putRequest(`https://localhost:58841/api/v1/products/update/${productSelect.value}`, updateProductModel)
            if(code == 200) updateProductSizes(code, product);
            location.reload();
        }
                
    }
        
})

function sliceString(str) {
    let temp = str;
    temp = temp.slice(0, temp.length - 2);
    str = temp;
    return str;
}

async function updateProductSizes(code, product) {
    let sizeInpt = document.getElementById('size');
    let countInpt = document.getElementById('count');

    if(code == 200) {
        let sizeText = sizeInpt.value;
        let sizes = sizeText.split(', ');
        let countText = countInpt.value;
        let counts = countText.split(', ');

        if(sizes.length == counts.length) {
            let arrayProductSizes = [];

            for(let i = 0; i < sizes.length; i++) {
                let currentSize = sizes[i];
                let currentCount = parseInt(counts[i]);

                if(currentCount >= 100) {   
                    alert('Количество товара не может быть больше 100!');
                    return;
                } else {
                    let productSizes = {
                        productId: product.id,
                        size: currentSize,
                        count: currentCount
                    }
                    arrayProductSizes[i] = productSizes;
                }
            }
            let code = await putRequest(`https://localhost:58841/api/v1/productSizes/update/${product.id}`, arrayProductSizes);
            if(code == 200) alert('Вы успешно обновили товар!');  
        }
    }
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

async function putRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'PUT',
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

async function deleteRequest(url) {
    const response = await fetch(url, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
    });
    let result = await response.status;

    if(result == 204) {
        alert('Вы успешно удалили товар!')
    }
}
 
function fillBrandSelect() {
    getRequest(`https://localhost:58841/api/v1/brands`)
        .then(listBrands => {
            listBrands.forEach(brand => {
                brandSelect.append(new Option(brand.name, brand.id));
            });
        })
}

function fillCategoriesSelect() {
    getRequest(`https://localhost:58841/api/v1/categories`)
        .then(listCategories => {
            listCategories.forEach(category => {
                categorySelect.append(new Option(category.name, category.id));
            })
        })
}

function fillSubcategoriesSelect(id) {
    getRequest(`https://localhost:58841/api/v1/subcategories/getSubcategoryByCategoryId/${id}`)
    .then(listSubcategories => {
        listSubcategories.forEach(subcategories => {
            subcategorySelect.append(new Option(subcategories.name, subcategories.id));
        })
    })
}

function fillProductsSelect() {
    getRequest(`https://localhost:58841/api/v1/products`)
        .then(listProduct => {
            listProduct.forEach(product => {
                productSelect.append(new Option(product.id, product.id));
            })
        })
}

categorySelect.addEventListener('change', (e) => {
    subcategorySelect.options.length = 0;
    fillSubcategoriesSelect(e.target.value);
})

function cardInsertHtml(product) {   
    return `
                <div class="card mb-3" style="max-width: 540px; margin-top: 30px;">
                    <div class="row g-0">
                        <div class="col-md-4">
                            <img src="${product.imageUrl}" class="img-fluid rounded-start" alt="...">
                        </div>
                        <div class="col-md-8">
                            <div class="card-body">
                            <h5 class="card-title">${product.name}</h5>
                            <p class="card-text">${product.description}</p>
                            <p class="card-text" id="price-product">Цена: ${product.price}₽</p>  
                            <div class="sizes-cards">
                            </div>
                            <div style="display: flex; justify-content: end;">
                                <button class="btn btn-outline-danger" id="delete-product"><i class="bi bi-x-circle-fill"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            `
    
}

function sizeCardInsert(productSizes) {
    return `<ul>
                <li>${productSizes.size} - ${productSizes.count} шт.</li>
            <ul>`
}

listProductBtn.addEventListener('click', () => { 
    getRequest(`https://localhost:58841/api/v1/products`)
        .then(listProduct => {
            listProduct.forEach(product => {
                if(productSellerDiv.children.length != listProduct.length) {
                    productSellerDiv.insertAdjacentHTML('beforeend', cardInsertHtml(product)); 
                    const lastCard = productSellerDiv.lastElementChild;

                    getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${product.id}`)
                        .then(prodcutSizesList => {
                            prodcutSizesList.sort((a, b) => a.size - b.size);
                            prodcutSizesList.forEach(productSize => {
                                const sizesCardContainer = lastCard.querySelector('.sizes-cards');
                                sizesCardContainer.insertAdjacentHTML('beforeend', sizeCardInsert(productSize));
                            })  
                        })
                }
            })
        })  
})

productSellerDiv.addEventListener('click', (event) => {
    if (event.target.closest('#delete-product')) {
        let confirmWindow = confirm('Вы точно хотите удалить данный товар?');

        if(confirmWindow) {
            let userId = localStorage.getItem('userId')
            const card = event.target.closest('.card');
            const productNameElement = card.querySelector('.card-title');
            const productName = productNameElement.textContent;
            deleteRequest(`https://localhost:58841/api/v1/products/deleteByNameAndUserId/${productName}/${userId}`);
            card.remove();
        }
    }
})

function insertOrder(orderId) {
    return `
        <div class="order-seller">
            <p style="font-size: 20px; margin-bottom: 0; font-weight: bold;">Заказ: №${orderId}</p>

            <div class="card-order"> 

            </div>

            <div style="display: flex; justify-content: end;">
                <button class="btn btn-success" id="confirm-order-btn">Передал в службу доствки</button>
            </div>
        </div>
    `
}

function insertCardProduct(product, productSizes, count) {
    return `
                <div class="card mb-3" style="max-width: 540px; margin-top: 30px;">
                    <div class="row g-0">
                        <div class="col-md-4">
                            <img src="${product.imageUrl}" class="img-fluid rounded-start" alt="...">
                        </div>
                        <div class="col-md-8">
                            <div class="card-body">
                            <h5 class="card-title">${product.name}</h5>
                            <p class="card-text">${product.description}</p>
                            <p class="card-text" id="price-product">Цена: ${product.price}₽</p>  
                            <ul>
                                <li>${productSizes.size} - ${count} шт.</li>
                            </ul>
                        </div>
                    </div>
                </div>
    `
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getOrderList() {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders`);
    return orderList;  
}

async function getOrderProductListByOrderId(orderId) {
    const orderProductList = await getRequest(`https://localhost:7049/api/v1/orderProduct/getByOrderId/${orderId}`);
    return orderProductList;  
}

async function getOrderById(id) {
    const order = await getRequest(`https://localhost:7049/api/v1/orders/getById/${id}`);
    return order;  
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;  
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;  
}

async function insertAllOrderProduct() {
    let orderList = await getOrderList();

    for (const order of orderList) {
        if(order.status == 'В сборке') {
            orderSellerDiv.insertAdjacentHTML('beforeend', insertOrder(order.id));
            let orderProductList = await getOrderProductListByOrderId(order.id);
    
            for (const orderProduct of orderProductList) {
                let cardOrderArray = orderSellerDiv.querySelectorAll('.card-order');
                let lastOrderCard = cardOrderArray[cardOrderArray.length - 1];
                let product = await getProductById(orderProduct.productId);
                let productSizes = await getProductSizesById(orderProduct.productSizesId);
                lastOrderCard.insertAdjacentHTML('beforeend', insertCardProduct(product, productSizes, orderProduct.count));
            }
        }
    }
}

listOrderBtn.addEventListener('click', () => {
    orderSellerDiv.innerHTML = '';
    insertAllOrderProduct();
})

orderSellerDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#confirm-order-btn')) {
        let ordetTitle = event.target.closest('#confirm-order-btn').parentNode.parentNode.children[0];
        let orderTitleStr = ordetTitle.textContent;
        let orderId = parseInt(orderTitleStr.replace(/\D+/g, ''));
        let order = await getOrderById(orderId);
        let now = new Date();

        let orderUpdateModel = {
            userId: order.userId,
            pointId: order.pointId,
            createOrder: order.createOrder,
            updateOrder: now,
            status: "Собрано и передано в службу доставки"
        }

        let code = await putRequest(`https://localhost:7049/api/v1/orders/update/${order.id}`, orderUpdateModel);

        if(code == 200) {
            alert('Вы успешно изменили статус заказа!');
        }
    }
})

async function insertProductByList(productList) {
    for (const product of productList) {
        if(productSellerDiv.children.length != productList.length) {
            productSellerDiv.insertAdjacentHTML('beforeend', cardInsertHtml(product)); 
            const lastCard = productSellerDiv.lastElementChild;
    
            getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${product.id}`)
                .then(prodcutSizesList => {
                    prodcutSizesList.sort((a, b) => a.size - b.size);
                    prodcutSizesList.forEach(productSize => {
                        const sizesCardContainer = lastCard.querySelector('.sizes-cards');
                        sizesCardContainer.insertAdjacentHTML('beforeend', sizeCardInsert(productSize));
                    })  
                })
        }
    }
}

async function getProductListByNameAndUserId(name, userId) {
    const productList = await getRequest(`https://localhost:58841/api/v1/products/getProductListByNameAndUserId/${name}/${userId}`)
    return productList;
}

searchItemsSeller.addEventListener('change', async () => {
    productSellerDiv.innerHTML = '';
    let valueSelect = searchItemsSeller.value;
    let productList = await getProductListByNameAndUserId(valueSelect, parseInt(userId));
    await insertProductByList(productList);
})

async function insertOrderProductByOrderId(orderId) {
    let order = await getOrderById(parseInt(orderId));

        if(order.status == 'В сборке') {
            orderSellerDiv.insertAdjacentHTML('beforeend', insertOrder(order.id));
            let orderProductList = await getOrderProductListByOrderId(order.id);

            for (const orderProduct of orderProductList) {
                let cardOrderArray = orderSellerDiv.querySelectorAll('.card-order');
                let lastOrderCard = cardOrderArray[cardOrderArray.length - 1];
                let product = await getProductById(orderProduct.productId);
                let productSizes = await getProductSizesById(orderProduct.productSizesId);
                lastOrderCard.insertAdjacentHTML('beforeend', insertCardProduct(product, productSizes, orderProduct.count));
            }
        }
}

numberOrderFilterInpt.addEventListener('change', async () => {
    orderSellerDiv.innerHTML = '';
    let valueSelect = numberOrderFilterInpt.value;

    if(!isNaN(valueSelect) && valueSelect.length != 0) {
        await insertOrderProductByOrderId(parseInt(valueSelect));
    } else if (valueSelect.length == 0) {
        await insertAllOrderProduct();
    } else {
        alert('Введите число!');
        return;
    }
})


