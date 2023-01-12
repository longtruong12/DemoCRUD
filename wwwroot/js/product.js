// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var dataTableProduct;
$(document).ready(function () {
    dataTableProduct = $("#tableProduct").DataTable({
        ajax: {
            url: "/Product/GetAllProduct",
            type: "GET",
            datatype: "json",
            dataSrc: ""
        },
        columnDefs: [
            { targets: 0, data: "id", visible: false },
            { targets: 1, data: "name", sortable: false },
            { targets: 2, data: "descreption", sortable: false },
            { targets: 3, data: "categoryId", sortable: false },
            //{ targets: 4, data: "categoryId", sortable: false },
            {
                targets: 4,
                data: "id",
                render: (data, type, row, meta) => {
                    console.log(data)
                    var listActions = [];
                    listActions.push('<a class="btn btn-danger btn-sm " onclick="deleteProduct(' + data + ')">Delete</a> ');
                    listActions.push('<a class="btn btn-success  btn-sm " onclick="getProduct(' + data + ')" >Edit</a>');

                    return listActions.join('');
                }
            }
        ]
    })
})

function getProduct(id) {
    $.get("/Product/GetProductPartial", { id: id }, function (res) {
        $("#contentModalProduct").html(res);
        $("#modalProduct").modal("show");
    })
}

function createOrUpdateProduct() {
    var modal = $("#modalProduct");
    var form = $("#formProduct")
    console.log(form);
    //form.validate();
    //if (!form.valid()) {
    //    return;
    //} else {
    var data = form.serialize();
    console.log(data)
    $.post("/Product/CreateOrUpdateProduct", data, function (res) {
        console.log(res)
        if (res == true) {
            modal.modal("hide");
            dataTableProduct.ajax.reload();
        } else {
            $("#contentModalProduct").html(res);
        }
    })
    //}
}

function deleteProduct(id) {
    $.get("/Product/DeleteProduct", { id: id }, function (res) {
        $("#contentDeleteModalProduct").html(res);
        $("#modalDeleteProduct").modal("show");
    })
}

function deleteProductPost(id) {
    $.get("/Product/DeleteProductPost", { id: id }, function (res) {
        if (res) {
            $("#modalDeleteProduct").modal("hide");
            dataTableProduct.ajax.reload();
        }
    })
}