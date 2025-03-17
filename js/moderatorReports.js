const searchReportBtn = document.querySelector('#search-report-btn');
const reportsDiv = document.querySelector('.reports-div');
const categoryReportSelect = document.querySelector('#category-report-select');

document.addEventListener('DOMContentLoaded', async () => {
    await insertAllReports();
    await fillCategoryReportSelect();
})

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

async function getUserById(id) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/${id}`);
    return user;
}

async function getUserByLogin(login) {
    const user = await getRequest(`https://localhost:5098/api/v1/users/getByLogin/${login}`);
    return user;
}

async function getCategoryReportById(id) {
    const categoryReport = await getRequest(`https://localhost:7199/api/v1/categoryReport/getById/${id}`);
    return categoryReport;
}

async function getAllCategoryReport() {
    const categoryReport = await getRequest(`https://localhost:7199/api/v1/categoryReport`);
    return categoryReport;
}

async function getByCategoryReportAndStatusAndUserId(categoryId, userId, status) {
    const reportList = 
        await getRequest(`https://localhost:7199/api/v1/report/getByCategoryReportAndStatusAndUserId/${categoryId}/${userId}/${status}`);
    return reportList;
}

async function getReportListByCategoryReportId(categoryId) {
    const reportList = await getRequest(`https://localhost:7199/api/v1/report/getByCategoryReportId/${categoryId}`);
    return reportList;
}

async function getReportListByStatus(status) {
    const reportList = await getRequest(`https://localhost:7199/api/v1/report/getByStatus/${status}`);
    return reportList;
}

async function getReportListByUserId(userId) {
    const reportList = await getRequest(`https://localhost:7199/api/v1/report/getByUserId/${userId}`);
    return reportList;
}

async function getAllReport() {
    const reportList = await getRequest(`https://localhost:7199/api/v1/report`);
    return reportList;
}

async function getReportById(id) {
    const report = await getRequest(`https://localhost:7199/api/v1/report/getById/${id}`);
    return report;
}

async function fillCategoryReportSelect() {
    let categoryReportList = await getAllCategoryReport();
    for (const categoryReport of categoryReportList) {
        categoryReportSelect.append(new Option(categoryReport.name, categoryReport.id));
    }
}

function insertReport(report, categoryReport, user) {
    return `
        <div style="display: flex; flex-direction: column; margin-top: 50px; border: 1px solid; padding: 20px; border-radius: 20px;" 
        class="report">
            <h3 style="text-align: center; border-bottom: 1px solid black;">Репорт №${report.id}</h3>
            <label><b>Тема: </b>${report.subject}</label>
            <label><b>Пользователь: </b>${user.login}</label>
            <label><b>Категория: </b>${categoryReport.name}</label>
            <div style="display: flex;">
                <label><b>Статус: </b></label>
                <label ${report.status == 'Открыт' ? 'style="color: green; margin-left: 5px"' : 
                    'style="color: red; margin-left: 5px"'}><b>${report.status}</b></label>  
            </div>
            <label><b>Описание:</b></label>
            <p style="height: 200px; width: 410px;">${report.description}</p>
            <div style="display: flex; justify-content: end; margin-top: 15px;">
                <button ${report.status == 'Закрыт' ? 'style="display: none;"' : 'style="display: block;"'}
                type="button" class="btn btn-danger" id="close-report-btn">Закрыть</button>
            </div>
        </div>
    `
}

searchReportBtn.addEventListener('click', async () => {
    reportsDiv.innerHTML = '';
    const usersInpt = document.querySelector('#users-report-inpt');
    const categoryReportSelect = document.querySelector('#category-report-select');
    const statusSelect = document.querySelector('#status-report-select');
    let reportList;

    if(usersInpt.value != '' && categoryReportSelect.value != '' && statusSelect.value != '') {
        user = await getUserByLogin(usersInpt.value);
        reportList = await getByCategoryReportAndStatusAndUserId(
            parseInt(categoryReportSelect.value), user.id, statusSelect.value, 
        );
    } else if(usersInpt.value != '' && categoryReportSelect.value == '' && statusSelect.value == '') {
        user = await getUserByLogin(usersInpt.value);
        reportList = await getReportListByUserId(user.id);
    } else if(usersInpt.value == '' && categoryReportSelect.value != '' && statusSelect.value == '') {
        reportList = await getReportListByCategoryReportId(parseInt(categoryReportSelect.value));
    } else if(usersInpt.value == '' && categoryReportSelect.value == '' && statusSelect.value != '') {
        reportList = await getReportListByStatus(statusSelect.value);
    } else {
        return;
    }

    for (const report of reportList) {
        const categoryReport = await getCategoryReportById(report.categoryReportId);
        const user = await getUserById(report.userId);
        reportsDiv.insertAdjacentHTML('beforeend', insertReport(report, categoryReport, user));
    }
})

async function insertAllReports() {
    const reportList = await getAllReport();

    for (const report of reportList) {
        const categoryReport = await getCategoryReportById(report.categoryReportId);
        const user = await getUserById(report.userId);
        reportsDiv.insertAdjacentHTML('beforeend', insertReport(report, categoryReport, user));
    }
}

reportsDiv.addEventListener('click', async (event) => {
    if(event.target.closest('#close-report-btn')) {
        let confirmWindow = confirm('Вы точно хотите закрыть репорт?');

        if(confirmWindow) {
            let reportIdText = event.target.closest('#close-report-btn')
            .parentNode.parentNode.children[0].textContent;
            let reportId = parseInt(reportIdText.replace(/\D+/g, '')); 
            let report = await getReportById(reportId);
            report.status = 'Закрыт';
            await putRequest(`https://localhost:7199/api/v1/report/update/${report.id}`, report);
        }
    }
})