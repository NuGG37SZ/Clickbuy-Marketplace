const addUpdProductBtn = document.getElementById('add-upd-productBtn');
const listProductBtn = document.getElementById('list-productBtn');
const formAddUpdProduct = document.getElementById('create-update-product');
const allProductsDiv = document.querySelector('.all-product-seller');
const createBtnForm = document.getElementById('create-product');
const updateBtnForm = document.getElementById('update-product');
let brandSelect = document.getElementById('brand');
const productSelect = document.getElementById('products');
const categorySelect = document.getElementById('category');
const subcategorySelect = document.getElementById('subcategory');
const productSellerDiv = document.querySelector('.products-seller');
let countClickAllProduct = 0;

document.addEventListener('DOMContentLoaded', () => {
    fillProductsSelect();
    fillBrandSelect();
    fillCategoriesSelect();
})

addUpdProductBtn.addEventListener('click', () => {
    hideElementsOnClickAddUpdBtn();
})

listProductBtn.addEventListener('click', () => {
    hideElementsOnClickAllProductsBtn();
})

function hideElementsOnClickAddUpdBtn() {
    formAddUpdProduct.style.display = 'block';
    allProductsDiv.style.display = 'none';
}

function hideElementsOnClickAllProductsBtn() {
    allProductsDiv.style.display = 'block';
    formAddUpdProduct.style.display = 'none';
}

createBtnForm.addEventListener('click', () => {
    let nameInpt = document.getElementById('name');
    let priceInpt = document.getElementById('price');
    let countInpt = document.getElementById('count');
    let descriptionArea = document.getElementById('description');
    let fileInpt = document.getElementById('image-product');

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
                    postRequest(`https://localhost:58841/api/v1/products/create`, productCreateModel);
                }
            })
    }
})

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

    if(result === 201) {
        alert('Вы успешно добавили новый товар!');
        return result;
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

function getImageBytes(file) {
    const reader = new FileReader();
    let result  = reader.readAsArrayBuffer(file);
    return result;
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
                            <p class="card-text" id="count-product">Количество: ${product.count}</p>
                            <div style="display: flex; justify-content: end;">
                                <button class="btn btn-outline-danger" id="delete-product"><i class="bi bi-x-circle-fill"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            `
    
}

listProductBtn.addEventListener('click', () => {
    getRequest('https://localhost:58841/api/v1/products')
        .then(listProduct => {
            listProduct.forEach(product => {
                if(productSellerDiv.children.length != listProduct.length) {
                    productSellerDiv.insertAdjacentHTML('beforeend', cardInsertHtml(product));
                }
            })
        })  
})


