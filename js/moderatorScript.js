const reportsModeratorBtn = document.getElementById('reports-moderator-btn');
const commentsModeratorBtn = document.getElementById('comments-moderator-btn');
const productsModeratorBtn = document.getElementById('products-moderator-btn');
const usersModeratorBtn = document.getElementById('users-moderator-btn');
const reportsModeratorDiv = document.querySelector('.reports-moderator');
const commentsModeratorDiv = document.querySelector('.comments-moderator');
const productssModeratorDiv = document.querySelector('.products-moderator');
const usersModeratorDiv = document.querySelector('.users-moderator');

reportsModeratorBtn.addEventListener('click', () => {
    reportsModeratorDiv.style.display = 'block';
    commentsModeratorDiv.style.display = 'none';
    productssModeratorDiv.style.display = 'none';
    usersModeratorDiv.style.display = 'none';
})

commentsModeratorBtn.addEventListener('click', () => {
    reportsModeratorDiv.style.display = 'none';
    commentsModeratorDiv.style.display = 'block';
    productssModeratorDiv.style.display = 'none';
    usersModeratorDiv.style.display = 'none';
})

productsModeratorBtn.addEventListener('click', () => {
    reportsModeratorDiv.style.display = 'none';
    commentsModeratorDiv.style.display = 'none';
    productssModeratorDiv.style.display = 'block';
    usersModeratorDiv.style.display = 'none';
})

usersModeratorBtn.addEventListener('click', () => {
    reportsModeratorDiv.style.display = 'none';
    commentsModeratorDiv.style.display = 'none';
    productssModeratorDiv.style.display = 'none';
    usersModeratorDiv.style.display = 'block';
})