// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var dataTable;
$(document).ready(function () {
   dataTable = $("#tableCategory").DataTable({
        ajax: {
            url: "/Category/GetAllCategory",
            type: "GET",
            datatype: "json",
            dataSrc: ""
        },
        columnDefs: [
            { targets: 0, data: "id", visible: false },
            { targets: 1, data: "name", sortable: false },
            {
                targets: 2,
                data: "status",
                render: (data, type, row, meta) => {
                    if (data == 0) {
                        return "Ennable";
                    } else {
                        return "Disible";
                    }
                    //return '<a class="btn btn-success onclick="getCategory(' + row.id + ')">Edit</a>'
                }
            },
            {
                targets: 3,
                data: null,
                render: (data, type, row, meta) => {
                    var listActions = [];
                    listActions.push('<a class="btn btn-danger btn-sm " onclick="deleteCategory(' + row.id +')">Delete</a> ');
                    listActions.push('<a class="btn btn-success  btn-sm " onclick="getCategory(' + row.id+')" >Edit</a>');
                    
                    return listActions.join('');
                }
            }
        ]
    })
})

function getCategory(id) {
    $.get("/Category/GetCategoryPartial", { id: id }, function (res) {
        $("#contentModalCategory").html(res);
        $("#modalCategory").modal("show");
    })
}

function createOrUpdateCategory() {
    var modal = $("#modalCategory");
    var form = $("#formCategory")
    console.log(form);
    //form.validate();
    //if (!form.valid()) {
    //    return;
    //} else {
        var data = form.serialize();
    $.post("/Category/CreateOrUpdateCategory", data, function (res) {
            console.log(res)
            if (res == true) {
                modal.modal("hide");
                dataTable.ajax.reload();
            } else {
                $("#contentModalCategory").html(res);
            }
        })
    //}
}

function deleteCategory(id) {
    $.get("/Category/DeleteCategory", { id: id }, function (res) {
        $("#contentDeleteModalCategory").html(res);
        $("#modalDeleteCategory").modal("show");
    })
}

function deleteCategoryPost(id) {
    $.get("/Category/DeleteCategoryPost", { id: id }, function (res) {
        if (res) {
            $("#modalDeleteCategory").modal("hide");
            dataTable.ajax.reload();
        }
    })
}