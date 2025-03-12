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

createBtnForm.addEventListener('click', () => {
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let countInpt = document.getElementById('count');
    let descriptionArea = document.getElementById('description');
    let fileInpt = document.getElementById('image-product');
    let sizeInpt = document.getElementById('size');

    let confirmWindow = confirm('Вы точно хотите добавить новый товар?');
    if(confirmWindow) {
        getRequest(`https://localhost:58841/api/v1/brandsSubcategories/getByBrandAndSubcategories/${brandSelect.value}/${subcategorySelect.value}`)
            .then(bs => {
                let userId = localStorage.getItem('userId');
                let file = fileInpt.files[0];
                let reader = new FileReader();
                reader.readAsDataURL(file);
                
                reader.onload = function(event) {
                    let imageUrl = event.target.result;

                    let productCreateModel = {
                        userId: parseInt(userId),
                        brandsSubcategoriesId: bs.id,
                        name: nameInpt.value,
                        price: parseInt(priceInpt.value),
                        count: parseInt(countInpt.value),
                        description: descriptionArea.value,
                        imageUrl: imageUrl
                    }
                    let result = postRequest(`https://localhost:58841/api/v1/products/create`, productCreateModel);
                    result.then(code => {
                        if(code === 201) {
                            getRequest(`https://localhost:58841/api/v1/products/getByNameAndUserId/${nameInpt.value}/${userId}`)
                            .then(product => {
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
                                            postRequest(`https://localhost:58841/api/v1/productSizes/create`, productSizes);
                                        } else {
                                            alert('Максимальное количество товаров 100!');
                                        }
                                    }
                                }
                                alert('Вы успешно добавили товар!');
                                location.reload();
                            })
                        }
                    })
                }
                        
            })
    }
})

productSelect.addEventListener('change', () => {
    let id = productSelect.value;
    getRequest(`https://localhost:58841/api/v1/products/getById/${id}`)
        .then(product => {
            let nameInpt = document.getElementById('name');
            nameInpt.value = product.name;
            let priceInpt = document.getElementById('price');
            priceInpt.value = product.price;
            let descriptionArea = document.getElementById('description');
            descriptionArea.value = product.description;
            let sizeInpt = document.getElementById('size');
            let countInpt = document.getElementById('count');
            sizeInpt.value = '';
            countInpt.value = '';
            
            getRequest(`https://localhost:58841/api/v1/productSizes/getAllByProductId/${product.id}`)
                .then(productSizesList => {
                    productSizesList.forEach(productSize => {
                        sizeInpt.value += productSize.size + ', ';
                        countInpt.value += productSize.count + ', ';
                    })
                    sizeInpt.value = sliceString(sizeInpt.value);
                    countInpt.value = sliceString(countInpt.value);
                }) 

            getRequest(`https://localhost:58841/api/v1/brandsSubcategories/getById/${product.brandsSubcategoriesId}`)
                .then(bs => {
                    let brandId = bs.brandsId;
                    let subcategoriesId = bs.subcategoriesId;

                    getRequest(`https://localhost:58841/api/v1/subcategories/getById/${subcategoriesId}`)
                        .then(sc => {
                            getRequest(`https://localhost:58841/api/v1/categories/getById/${sc.categoryId}`)
                                .then(c => {
                                    categorySelect.value = c.id;
                                    subcategorySelect.options.length = 0;
                                    brandSelect.value = brandId;
                                    fillSubcategoriesSelect(c.id);
                                })
                        })
                })    
        })
})

updateBtnForm.addEventListener('click', () => {
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let descriptionArea = document.getElementById('description');
    let fileInpt = document.getElementById('image-product');
    let userId = localStorage.getItem('userId');

    getRequest(`https://localhost:58841/api/v1/brandsSubcategories/getByBrandAndSubcategories/${brandSelect.value}/${subcategorySelect.value}`)
        .then(bs => {
            let file;

            if(fileInpt.files[0] == undefined) {
                getRequest(`https://localhost:58841/api/v1/products/getById/${productSelect.value}`)
                .then(product => {
                    file = product.imageUrl;

                    let updateProductModel = {
                        userId: parseInt(userId),
                        brandsSubcategoriesId: bs.id,
                        name: nameInpt.value,
                        price: parseInt(priceInpt.value),
                        description: descriptionArea.value,
                        imageUrl: file
                    }

                    putRequest(`https://localhost:58841/api/v1/products/update/${productSelect.value}`, updateProductModel)
                        .then(code => { updateProductSizes(code, product) })
                }) 
            } else {
                getRequest(`https://localhost:58841/api/v1/products/getById/${productSelect.value}`)
                .then(product => {
                    let file = fileInpt.files[0];
                    let reader = new FileReader();
                    reader.readAsDataURL(file);

                    reader.onload = function(event) {
                        let imageUrl = event.target.result;

                        let updateProductModel = {
                            userId: parseInt(userId),
                            brandsSubcategoriesId: bs.id,
                            name: nameInpt.value,
                            price: parseInt(priceInpt.value),
                            description: descriptionArea.value,
                            imageUrl: imageUrl
                        }
        
                        putRequest(`https://localhost:58841/api/v1/products/update/${productSelect.value}`, updateProductModel)
                            .then(code => { updateProductSizes(code, product) })
                        location.reload();
                    }
                })
            }
        })
})

function sliceString(str) {
    let temp = str;
    temp = temp.slice(0, temp.length - 2);
    str = temp;
    return str;
}

function updateProductSizes(code, product) {
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
            putRequest(`https://localhost:58841/api/v1/productSizes/update/${product.id}`, arrayProductSizes); 
            alert('Вы успешно обновили товар!');  
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
