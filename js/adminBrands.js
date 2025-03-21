const createBrandBtn = document.getElementById('create-brand-btn');
const deleteBrandBtn = document.getElementById('delete-brand-btn');
const brandNameInpt = document.getElementById('brand-name-inpt');

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

async function getBrandByName(name) {
    const point = await getRequest(`https://localhost:58841/api/v1/brands/getByName/${name}`);
    return point;
}

createBrandBtn.addEventListener('click', async () => {
    let confirmWindow = confirm('Вы точно хотите создать новый бренд?');

    if(confirmWindow) {
        let brand = await getBrandByName(brandNameInpt.value);
        console.log(brand);
    
        if(brand.id == 0 && brand.name == null) {
            let brand = { name: brandNameInpt.value }
            let code = await postRequest(`https://localhost:58841/api/v1/brands/create`, brand);
    
            if(code == 201) {
                alert('Вы успешно создали бренд!');
            }
        }
    }
})

deleteBrandBtn.addEventListener('click', async () => {
    let confirmWindow = confirm('Вы точно хотите удалить бренд?');
    
    if(confirmWindow) {
        let brand = await getBrandByName(brandNameInpt.value);
    
        if(brand.id != 0 && brand.name != '') {
            let code = await deleteRequest(`https://localhost:58841/api/v1/brands/delete/${brand.id}`);
    
            if(code == 204) {
                alert('Вы успешно удалили бренд!');
            }
        }
    }
})