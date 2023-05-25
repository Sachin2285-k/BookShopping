var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },

        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    <a href="/Admin/BookType/Upsert/${data}" class="btn btn-info"><i class="fas fa-edit"></i></a>
                    <a class="btn btn-danger" onclick = Delete("/Admin/BookType/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
                    </div>
                    `;
                }
            }
        ]
    })
}