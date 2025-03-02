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
    let fileInpt = document.getElementById('file-path');
})

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
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


