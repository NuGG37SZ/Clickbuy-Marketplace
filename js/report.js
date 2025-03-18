const modal = document.getElementById("myModalWindow");
const btn = document.getElementById("openModalButton");
const span = document.getElementsByClassName("close-second")[0];
const categoryReportSelect = document.querySelector('#category-report-select');
const subjectReportInpt = document.querySelector('#subject-report-inpt');
const descriptionTextArea = document.querySelector('#description-report-area');
const sendReportBtn = document.querySelector('#send-report-btn');

document.addEventListener('DOMContentLoaded', async() => {
    await fillCategoryReportSelect();
})

btn.onclick = function() {
    modal.style.display = "block";
}

span.onclick = function() {
    modal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

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

async function getAllCategoryReport(id) {
    const categoryReportList = await getRequest(`https://localhost:7199/api/v1/categoryReport`);
    return categoryReportList;
}

async function fillCategoryReportSelect() {
    const categoryReportList = await getAllCategoryReport();

    for (const categoryReport of categoryReportList) {
        categoryReportSelect.append(new Option(categoryReport.name, categoryReport.id));
    }
}

sendReportBtn.addEventListener('click', async () => {
    let reportModel = {
        userId: parseInt(userId),
        categoryReportId: parseInt(categoryReportSelect.value),
        subject: subjectReportInpt.value,
        status: 'Открыт',
        description: descriptionTextArea.value
    }

    let confirmWindow = confirm('Вы точно хотите создать репорт?');
    if(confirmWindow) {
        let code = await postRequest(`https://localhost:7199/api/v1/report/create`, reportModel);

        if(code == 201) {
            alert('Вы успешно создали репорт! В скором времени вам ответят.')
        }
    }
})

