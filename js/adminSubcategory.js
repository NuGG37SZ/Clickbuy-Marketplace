const subcategoryInpt = document.getElementById('subcategory-name-inpt');
const categorySelect = document.getElementById('category-select');
const createSubcategoryBtn = document.getElementById('create-subcategory-btn');
const deleteSubcategoryBtn = document.getElementById('delete-subcategory-btn');

document.addEventListener('DOMContentLoaded', async () => {
    await fillCategorySelect();
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
    let result = await response.status;
    return result;
}

async function deleteRequest(url) {
    const response = await fetch(url, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }
    });
    let result = await response.status;
    return result;
}

async function getAllCategory() {
    const categoryList = await getRequest(`https://localhost:58841/api/v1/categories`);
    return categoryList;
}

async function getSubcategoryByName(name) {
    const subcategory = await getRequest(`https://localhost:58841/api/v1/subcategories/getSubcategoriesByName/${name}`);
    return subcategory;
}

async function fillCategorySelect() {
    let categoryList = await getAllCategory();

    for (const category of categoryList) {
        categorySelect.append(new Option(category.name, category.id));    
    }
}

createSubcategoryBtn.addEventListener('click', async () => {
    let confirmWindow = confirm('Вы точно хотите создать подкатегорию?');

    if(confirmWindow) {
        let subcategory = await getSubcategoryByName(subcategoryInpt.value);
    
        if(subcategory.id == 0) {
            let subcategoryCreateModel = {
                categoryId: categorySelect.value,
                name: subcategoryInpt.value
            } 
    
            let code = await postRequest(`https://localhost:58841/api/v1/subcategories/create`, subcategoryCreateModel);
    
            if(code == 200) {
                alert('Подкатегория успешно создана!');
            }
        }
    }
})

deleteSubcategoryBtn.addEventListener('click', async () => {
    let confirmWindow = confirm('Вы точно хотите удалить подкатегорию?');

    if(confirmWindow) {
        let subcategory = await getSubcategoryByName(subcategoryInpt.value);
    
        if(subcategory.id != 0) {
            let code = await deleteRequest(`https://localhost:58841/api/v1/subcategories/delete/${subcategory.id}`);
    
            if(code == 204) {
                alert('Подкатегория успешно удалена!');
            }
        }
    }
})