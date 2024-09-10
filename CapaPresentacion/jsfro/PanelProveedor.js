

var table;
function ObtenerFecha() {

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + d.getFullYear();

    return output;
}

$(document).ready(function () {
    
    $("#txtPrecio").val("0");
    $("#txtCantidad").val("0");
    $("#txtFechaIni").val(ObtenerFecha());
    dtProvee();

});

function dtProvee() {
    // Verificar si el DataTable ya está inicializado
    if ($.fn.DataTable.isDataTable("#tbProveedor")) {
        // Destruir el DataTable existente
        $("#tbProveedor").DataTable().destroy();
        // Limpiar el contenedor del DataTable
        $('#tbProveedor tbody').empty();
    }

    table = $("#tbProveedor").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelProveedor.aspx/ObtenerProvee',
            "type": "POST", // Cambiado a POST
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                //console.log("Response from server:", json.d.objeto);
                if (json.d.Estado) {
                    return json.d.Data; // Asegúrate de que esto apunta al array de datos
                } else {
                    return [];
                }
            }
        },
        "columns": [
            { "data": "IdProveedor", "visible": false, "searchable": false },
            { "data": "Precio" },
            { "data": "Cantidad" },
            { "data": "VFechaRegistro" },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "50px"
            }
        ],
        "order": [[0, "desc"]],
        "dom": "Bfrtip",
        "buttons": [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Informe Compras',
                exportOptions: {
                    columns: [1, 2, 3] // Ajusta según las columnas que desees exportar
                }
            },
            'pageLength'
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$("#tbProveedor tbody").on("click", ".btn-editar", function (e) {
    e.preventDefault();
    let filaSeleccionada;

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const model = table.row(filaSeleccionada).data();

    $("#txtIdProveedor").val(model.IdProveedor);
    $("#txtPrecio").val(model.Precio);
    $("#txtCantidad").val(model.Cantidad);
    $("#txtFechaIni").val(model.VFechaRegistro);
    //mostrarModal(model, false);
})

function registerDataAjax() {
    //parseFloat($("#txtPresupuesto").val())

    var request = {
        oProvee: {
            Precio: parseFloat($("#txtPrecio").val()),
            Cantidad: parseFloat($("#txtCantidad").val())
        }
    }

    $.ajax({
        type: "POST",
        url: "PanelProveedor.aspx/Guardar",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            // Mostrar overlay de carga antes de enviar la solicitud modal-content
            $("#loaddd").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loaddd").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtProvee();

                $("#txtIdProveedor").val("0");
                $("#txtPrecio").val("0");
                $("#txtCantidad").val("0");

                swal("Mensaje", response.d.Valor, "success");

            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loaddd").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarProv').prop('disabled', false);
        }
    });
}

function editarDataAjax() {
    //parseFloat($("#txtPresupuesto").val())

    var request = {
        oProvee: {
            IdProveedor: parseInt($("#txtIdProveedor").val()),
            Precio: parseFloat($("#txtPrecio").val()),
            Cantidad: parseFloat($("#txtCantidad").val())
        }
    }

    $.ajax({
        type: "POST",
        url: "PanelProveedor.aspx/Editar",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            // Mostrar overlay de carga antes de enviar la solicitud modal-content
            $("#loaddd").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loaddd").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtProvee();

                $("#txtIdProveedor").val("0");
                $("#txtPrecio").val("0");
                $("#txtCantidad").val("0");

                swal("Mensaje", response.d.Valor, "success");

            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loaddd").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarProv').prop('disabled', false);
        }
    });
}

$('#btnGuardarProv').on('click', function () {

    $('#btnGuardarProv').prop('disabled', true);

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()

        $('#btnGuardarProv').prop('disabled', false);
        return;
    }

    var montoo = parseFloat($("#txtPrecio").val().trim());
    if (isNaN(montoo) || montoo === 0) {
        toastr.warning("", "Debe ingresar un Precio válido");
        $("#txtPrecio").focus();

        $('#btnGuardarProv').prop('disabled', false);
        return;
    }

    var cantid = parseFloat($("#txtCantidad").val().trim());
    if (isNaN(cantid) || cantid === 0) {
        toastr.warning("", "Debe ingresar una cantidad válido");
        $("#txtCantidad").focus();

        $('#btnGuardarProv').prop('disabled', false);
        return;
    }

    if (parseInt($("#txtIdProveedor").val()) === 0) {
        registerDataAjax();
    } else {
        editarDataAjax();
    }
})