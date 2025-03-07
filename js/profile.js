const loginUserProfile = document.getElementById('user-login');
const panelRoles = document.querySelector('.panels-roles');
let userId = localStorage.getItem('userId');

document.addEventListener("DOMContentLoaded", ()  => {
    checkAuthUser();
})

function checkAuthUser() {
    if(localStorage.getItem('isAuth') === 'true') {
        const id = localStorage.getItem('userId');

        getUserByIdRequest(id)
            .then(user => {
                loginUserProfile.textContent = user.login;
                hideElementForRoleUser(user.role);
            })
    }
}

async function getUserByIdRequest(id) {
    url = `https://localhost:5098/api/v1/users/${id}`;
    const response = await fetch(url);
    return await response.json();
}

function hideElementForRoleUser(role) {
    switch(role) {
        case 'user':
            panelRoles.style.display = 'none';
            break;
        case 'seller':
            panelRoles.children[1].style.display = 'none';
            panelRoles.children[2].style.display = 'none';
            break;
        case 'moderator': 
            panelRoles.children[0].style.display = 'none';
            panelRoles.children[2].style.display = 'none';
            break;
    }
}