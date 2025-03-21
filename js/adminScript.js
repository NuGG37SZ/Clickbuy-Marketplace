const rolesDiv = document.getElementById('roles-div');
const pointsDiv = document.getElementById('points-div');
const brandDiv = document.getElementById('brands-div');
const subcategoryDiv = document.getElementById('subcategory-div');
const categoryDiv = document.getElementById('category-div');
const changeRoleDivBtn = document.getElementById('change-role-div-btn');
const pointDivBtn = document.getElementById('point-div-btn');
const brandsDivBtn = document.getElementById('brands-div-btn');
const categoryDivBtn = document.getElementById('category-div-btn');
const subcategoryDivBtn = document.getElementById('subcategory-div-btn');
const divs = document.querySelector('#divs-admins');

changeRoleDivBtn.addEventListener('click', () => {
    openCurrentDiv(rolesDiv.id);
})

pointDivBtn.addEventListener('click', () => {
    openCurrentDiv(pointsDiv.id);
})

brandsDivBtn.addEventListener('click', () => {
    openCurrentDiv(brandDiv.id);
})

categoryDivBtn.addEventListener('click', () => {
    openCurrentDiv(categoryDiv.id);
})

subcategoryDivBtn.addEventListener('click', () => {
    openCurrentDiv(subcategoryDiv.id);
})

function openCurrentDiv(id) {
    for(let i = 0; i < divs.children.length; i++) {;
        if(divs.children[i].id == id) {
            divs.children[i].style.display = 'flex';
        } else {
            divs.children[i].style.display = 'none';
        }
    }
}