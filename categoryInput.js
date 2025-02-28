const categoryItems = document.querySelectorAll('.category-item');
const categoryText = document.getElementById('selectedCategory');

categoryItems.forEach(item => {
    item.addEventListener('click', function() {
        const category = this.getAttribute('data-category');
        categoryText.textContent = category;
        categoryText.innerHTML += ' <span class="close-category">&times;</span>';

        const previousActive = document.querySelector('.category-item.active');
        if (previousActive) {
            previousActive.classList.remove('active');
        }
        this.classList.add('active');

        const modal = bootstrap.Modal.getInstance(document.getElementById('categoryModal'));
        modal.hide();
        categoryText.classList.add('active');
    });
    
});

categoryText.addEventListener('click', function(e) {
    if (e.target.classList.contains('close-category')) {
        categoryText.textContent = 'Везде';
        categoryText.innerHTML += ' <i class="bi bi-chevron-down"></i>';

        categoryText.classList.remove('active');

        const activeCategory = document.querySelector('.category-item.active');
        if (activeCategory) {
            activeCategory.classList.remove('active');
        }
        e.stopPropagation();
    }
});