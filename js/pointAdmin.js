const ordersDiv = document.querySelector('.orders');
const searchBtn = document.querySelector('.search-button');

insertAllOrders();

function insertOrder(order, user) {
    return `
        <div class="order" style="margin-top: 15px;">
            <h4>Получатель: ${user.login}</h4>
            <h4>Заказ №${order.id}.</h4>
            <table class="all-product-for-payment">
                <thead>
                    <tr>
                        <th></th>
                        <th>Название</th>
                        <th>Цена</th>
                        <th>Продавец</th>
                        <th>Размер</th>
                        <th>Описание</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody class="tbody-orders">
                        
                </tbody>
            </table>

            <div style="display: flex; justify-content: space-between; width: 100%; align-items: baseline">
                <div style="display: flex; font-size: 20px; font-weight: bold; width: 470px;">
                    <p>Статус: </p>
                    <p style="color: #5ab84b; padding-left: 7px;">${order.status}</p>
                </div>
            </div>
        </div>
    `
}

function insertCancelOrder(order, user) {
    return `
        <div class="order" style="margin-top: 15px;">
            <h4>Получатель: ${user.login}</h4>
            <h4>Заказ №${order.id}.</h4>
            <table class="all-product-for-payment">
                <thead>
                    <tr>
                        <th></th>
                        <th>Название</th>
                        <th>Цена</th>
                        <th>Продавец</th>
                        <th>Размер</th>
                        <th>Описание</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody class="tbody-orders">
                    
                </tbody>
            </table>

            <div style="display: flex; justify-content: space-between; width: 100%; align-items: baseline">
                <div style="display: flex; justify-content: space-between; font-size: 20px; font-weight: bold; width: 160px;">
                    <p>Статус: </p>
                    <p style="color:rgb(252, 20, 63);">${order.status}</p>
                </div>
            </div>
        </div>
    `
}

function insertProduct(product, seller, productSize, countProduct) {
    return `
          <tr>
              <td>
                  <img src="${product.imageUrl}" draggable="false" width="100px">
              </td>
              <td>${product.name}</td>
              <td>${product.price} ₽</td>
              <td>${seller.login}</td>
              <td>${productSize}</td>
              <td>${product.description}</td>
              <td>Количество: ${countProduct}</td>
          </tr>
    `
}

function insertSaveRow() {
    return `
        <tr>
           <td colspan="6"></td>
            <td>
                <div style="display: flex; justify-content: center;">
                    <select style="width: 200px; height: 36px" id="order-status-select">
                        <option disabled selected>Выберите статус</option>
                        <option>Получен</option>
                        <option>Возврат</option>
                    </select>
                    <button type="button" class="btn btn-primary" style="margin-left: 10px" id="save-status-order-btn">Сохранить</button>
                </div>
            </td>
        </tr>
    `
}

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function putRequest(url, obj) {
    const response = await fetch(url, { 
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(obj)
    });
    let result = await response.status;
    return result;
}

async function getOrderList() {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders`);
    return orderList;
}

async function getOrderListByUserId(userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByUserId/${userId}`);
    return orderList;
}

async function getOrderListByStatusAndUserId(status, userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByOrderStatusAndUserId/${status}/${userId}`);
    return orderList;
}

async function getOrderById(id) {
    const order = await getRequest(`https://localhost:7049/api/v1/orders/getById/${id}`);
    return order;
}

async function getOrderProductByOrderId(orderId) {
    const orderProductList = await getRequest(`https://localhost:7049/api/v1/orderProduct/getByOrderId/${orderId}`);
    return orderProductList;
}

async function getProductById(id) {
    const product = await getRequest(`https://localhost:58841/api/v1/products/getById/${id}`);
    return product;
}

async function getProductSizesById(id) {
    const productSizes = await getRequest(`https://localhost:58841/api/v1/productSizes/getById/${id}`);
    return productSizes;
}

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}


async function insertAllOrders() {
    let orderList = await getOrderList();

    for (const order of orderList) {
        let user = await getUserById(order.userId);

        if(order.status == 'Возврат') {
            ordersDiv.insertAdjacentHTML('beforeend', insertCancelOrder(order, user));
        } else if (order.status != 'Отменен') {
            ordersDiv.insertAdjacentHTML('beforeend', insertOrder(order, user));
        }
        insertOrderProduct(ordersDiv, order);
    }
}

async function insertOrderProduct(ordersDiv, order) {
    let orderDivs = ordersDiv.querySelectorAll('.order');
    let lastOrderDiv = orderDivs[orderDivs.length - 1];
    let tbody = lastOrderDiv.querySelector('.tbody-orders');
    let orderProducts = await getOrderProductByOrderId(order.id);
    
    for (const orderProduct of orderProducts) {     
        let product = await getProductById(orderProduct.productId);
        let productSizes = await getProductSizesById(orderProduct.productSizesId);
        let seller = await getUserById(product.userId);
        tbody.insertAdjacentHTML('beforeend', insertProduct(product, seller, productSizes.size, orderProduct.count));
    }

    if(order.status != 'Возврат' && order.status != 'Получен')
        tbody.insertAdjacentHTML('beforeend', insertSaveRow());
}

searchBtn.addEventListener('click', async () => {
    ordersDiv.innerHTML = '';
    let userLogin = document.getElementById('search-inpt').value;
    let orderStatusValue = document.getElementById('order-status-select-adm').value;
    let user = await getUserByLogin(userLogin);
    let orderList;

    if(orderStatusValue == 'Все заказы') {
        orderList = await getOrderListByUserId(user.id);
    } else {
        orderList = await getOrderListByStatusAndUserId(orderStatusValue, user.id);
    }
    inserOrdersWithOrderList(orderList, user);
})

function inserOrdersWithOrderList(orderList, user) {
    for (const order of orderList) {
        if(order.status == 'Возврат') {
            ordersDiv.insertAdjacentHTML('beforeend', insertCancelOrder(order, user));
        } else if (order.status != 'Отменен') {
            ordersDiv.insertAdjacentHTML('beforeend', insertOrder(order, user));
        }
        insertOrderProduct(ordersDiv, order);
    }
}

ordersDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#save-status-order-btn')) {
        let orderDiv = event.target.closest('#save-status-order-btn')
        .parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.children[1];
        let orderStatusSelect = event.target.closest('#save-status-order-btn').parentNode.children[0].value;
        let orderId = parseInt(orderDiv.textContent.replace(/\D+/g, ''));
        let order = await getOrderById(orderId); 
        
        if(orderStatusSelect != 'Выберите статус') {
            let date = new Date();
            order.status = orderStatusSelect;
            order.updateOrder = date;
            
            let confirmWindow = confirm('Вы точно хотите изменить статус заказа?');

            if(confirmWindow) await putRequest(`https://localhost:7049/api/v1/orders/update/${order.id}`, order);
        }
    }
})

