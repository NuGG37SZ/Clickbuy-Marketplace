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

                reader.onload = function() {
                    console.log(reader.result);

                    let productCreateModel = {
                        userId: userId,
                        brandSubcategoriesId: bs.id,
                        name: nameInpt.value,
                        price: priceInpt.value,
                        count: countInpt.value,
                        description: descriptionArea.value,
                        image: reader.result
                    }

                    console.log(JSON.stringify(productCreateModel));
                    
                    const response = postRequest(`https://localhost:58841/api/v1/products/create`, productCreateModel);
                    if(response == 201) {
                        alert('Вы успешно добавили новый товар!');
                    }
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
        const errorText = await response.text(); // Получаем текст ошибки
        throw new Error(`Ошибка ${response.status}: ${errorText}`); // Выводим статус и текст
    }

    let result = await response.status;
    return result;
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


