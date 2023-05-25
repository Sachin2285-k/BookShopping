var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/PendingOrders/GetAll"
        },

        "columns": [
            { "data": "id", "width": "30%" },
            { "data": "name", "width": "30%" },
            { "data": "phoneNumber", "width": "30%" },
            { "data": "orderTotal", "width": "30%" },
            { "data": "orderDate", "width": "30%" }
        ]
    })
}