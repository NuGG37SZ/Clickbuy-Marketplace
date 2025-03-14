const ordersDiv = document.querySelector('.orders');
const userId = localStorage.getItem('userId');
const orderStatusSelect = document.getElementById('order-status-select');

insertAllOrders();

function insertOrder(order, firstDate, secondDate, point) {
    return `
        <div class="order" style="margin-top: 15px;">
            <h4>Заказ №${order.id}. ${ order.status == 'Получен' ?
                `Заказ получен в пункте выдачи по адресу ${point.address}` :
                 `Приедет примерно ${firstDate} - ${secondDate}, в пункт выдачи по адресу ${point.address}` }</h4>
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

                <div style="display: flex;">
                    ${order.status == 'Получен' ? '' : '<button type="button" class="btn btn-danger" id="cancel-order-btn">Отменить заказ</button>' }
                </div>
            </div>
        </div>
    `
}

function insertCancelOrder(order) {
    return `
        <div class="order" style="margin-top: 15px;">
            <h4>Заказ №${order.id}. ${order.status}</h4>
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
          </tr>`
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

async function getRequest(url) {
    const response = await fetch(url);
    return await response.json();
}

async function getOrdersByUserId(userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByUserId/${userId}`);
    return orderList;
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

async function getPointById(id) {
    const point = await getRequest(`https://localhost:7049/api/v1/points/getById/${id}`);
    return point;
}

async function getOrderById(id) {
    const order = await getRequest(`https://localhost:7049/api/v1/orders/getById/${id}`);
    return order;
}

async function getOrderListByStatusAndUserId(status, userId) {
    const orderList = await getRequest(`https://localhost:7049/api/v1/orders/getByOrderStatusAndUserId/${status}/${userId}`);
    return orderList;
}

async function insertAllOrders() {
    let orderList = await getOrdersByUserId(parseInt(userId));

    for (const order of orderList) {
        let firstDate = getDate(order.createOrder, 10);
        let secondDate = getDate(order.createOrder, 12);
        let point = await getPointById(order.pointId);

        if(order.status != 'Отменен') {
            ordersDiv.insertAdjacentHTML('beforeend', insertOrder(order, firstDate, secondDate, point));
        } else {
            ordersDiv.insertAdjacentHTML('beforeend', insertCancelOrder(order));
        }

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
    }
}

async function insertOrderCancelList(orderList) {
    for (const order of orderList) {
        ordersDiv.insertAdjacentHTML('beforeend', insertCancelOrder(order));
       
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
    }
}

async function insertOrderList(orderList) {
    for (const order of orderList) {
        let firstDate = getDate(order.createOrder, 10);
        let secondDate = getDate(order.createOrder, 12);
        let point = await getPointById(order.pointId);

        ordersDiv.insertAdjacentHTML('beforeend', insertOrder(order, firstDate, secondDate, point));
       
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
    }
}

function getDate(dateStr, days) {
    const dateFirst = moment(dateStr, "YYYY-MM-DD HH:mm:ss");
    dateFirst.add(days, 'days');
    return dateFirst.format('DD.MM.YYYY');
}

ordersDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#cancel-order-btn')) {
        let currentOrder = event.target.closest('#cancel-order-btn').parentNode.parentNode.parentNode;
        let titleOrder = currentOrder.children[0].textContent;
        let arrayTitle = titleOrder.split('.');
        let orderId = arrayTitle[0].replace(/\D+/g, '');
        let order = await getOrderById(orderId);
        order.status = 'Отменен';
        let code = await putRequest(`https://localhost:7049/api/v1/orders/update/${order.id}`, order);
        
        if(code == 200) {
            let orderProducts = await getOrderProductByOrderId(order.id);

            for (const orderProduct of orderProducts) {
                let productSizes = await getProductSizesById(orderProduct.productSizesId);
                productSizes.count += orderProduct.count;
                await putRequest(`https://localhost:58841/api/v1/productSizes/updateById/${productSizes.id}`, productSizes);
            }

            alert('Заказ отменен :с');
            location.reload();
        }
    }
})

orderStatusSelect.addEventListener('change', async () => {
    ordersDiv.innerHTML = '';
    let valueSelect = orderStatusSelect.value;
    console.log(valueSelect);
    let orderList = await getOrderListByStatusAndUserId(valueSelect, parseInt(userId));
    console.log()

    if(valueSelect == 'Отменено' &&  valueSelect == 'Возврат') {
        insertOrderCancelList(orderList);
    } else if(valueSelect == 'Все заказы') {
        await insertAllOrders();
    }else {
        insertOrderList(orderList);
    }
})
