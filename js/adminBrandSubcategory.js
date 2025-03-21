const brandSelect = document.getElementById('brand-select');
const subcategorySelect = document.getElementById('subcategory-select');
const createBrandSubcategoryBtn = document.getElementById('create-bs-btn');
const deleteBrandSubcategoryBtn = document.getElementById('delete-bs-btn');

document.addEventListener('DOMContentLoaded', async () => {
    await fillBrandsSelect();
    await fillSubcategorySelect();
})

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getAllBrands() {
    const brandsList = await getRequest(`https://localhost:58841/api/v1/brands`);
    return brandsList;
}

async function getAllSubcategory() {
    const subcategoriesList = await getRequest(`https://localhost:58841/api/v1/subcategories`);
    return subcategoriesList;
}

async function getBrandSubcategoryByBrandIdAndSubcategoriesId(brandId, subcategoryId) {
    const brandSubcategory = await getRequest(`https://localhost:58841/api/v1/subcategories/getByBrandAndSubcategories/${brandId}/${subcategoryId}`);
    return brandSubcategory;
}

async function fillBrandsSelect() {
    let brandsList = await getAllBrands();

    for (const brands of brandsList) {
        brandSelect.append(new Option(brands.name, brands.id));    
    }
}

async function fillSubcategorySelect() {
    let subcategoriesList = await getAllSubcategory();

    for (const subcategory of subcategoriesList) {
        subcategorySelect.append(new Option(subcategory.name, subcategory.id));
    }
}

createBrandSubcategoryBtn.addEventListener('click', async () => {
    let brandSubcategory = await getBrandSubcategoryByBrandIdAndSubcategoriesId(brandSelect.value, subcategorySelect.value);

    
})

