const categoryInpt = document.getElementById('category-name-inpt');
const createCategoryBtn = document.getElementById('create-category-btn');
const deleteCategoryBtn = document.getElementById('delete-category-btn');

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

async function getCategoryByName(name) {
    const category = await getRequest(`https://localhost:58841/api/v1/categories/getByName/${name}`);
    return category;
}

createCategoryBtn.addEventListener('click', async () => {
    let category = await getCategoryByName(categoryInpt.value);

    if(category.id == 0 && category.name == null) {
        let category = { name: categoryInpt.value }
        let code = await postRequest(`https://localhost:58841/api/v1/categories/create`, category);
        
        if(code == 201) {
            alert('Вы успешно создали категорию!');
        }
    }
})

deleteCategoryBtn.addEventListener('click', async () => {
    let category = await getCategoryByName(categoryInpt.value);

    if(category.id != 0 && category.name != null) {
        let code = await deleteRequest(`https://localhost:58841/api/v1/categories/delete/${category.id}`);

        if(code == 204) {
            alert('Вы успешно удалили категорию');
        }
    }
})