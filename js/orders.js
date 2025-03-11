const ordersDiv = document.querySelector('.orders');
const userId = localStorage.getItem('userId');

insertAllOrders();

function insertOrder(order, firstDate, secondDate) {
    return `
        <div class="order">
            <h3>Заказ №${order.id}. Приедет примерно ${firstDate} - ${secondDate}
           </h3>
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

async function insertAllOrders() {
    let orderList = await getOrdersByUserId(parseInt(userId));

    for (const order of orderList) {
        if(order.status != 'Получен' || order.status != 'Отменен') {
            let firstDate = getDate(order.createOrder, 10);
            let secondDate = getDate(order.createOrder, 12);
            ordersDiv.insertAdjacentHTML('beforeend', insertOrder(order, firstDate, secondDate));
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
}

function getDate(dateStr, days) {
    const dateFirst = moment(dateStr, "YYYY-MM-DD HH:mm:ss");
    dateFirst.add(days, 'days');
    return dateFirst.format('DD.MM.YYYY');
}

