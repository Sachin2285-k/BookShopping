var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },

        "columns": [
            { "data": "title", "width": "70%" },
            { "data": "description", "width": "70%" },
            { "data": "author", "width": "70%" },
            { "data": "isbn", "width": "70%" },
            { "data": "price", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    <a href="/Admin/Product/Upsert/${data}" class="btn btn-info"><i class="fas fa-edit"></i></a>
                    <a class="btn btn-danger" onclick = Delete("/Admin/Product/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
                    </div>
                    `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Delete Information",
        text: "Want to delete data?",
        icon: "warning",
        buttons: true,
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}