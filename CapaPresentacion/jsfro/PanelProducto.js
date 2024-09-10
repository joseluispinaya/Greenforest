

var table;

const MODELO_BASE = {
    IdProducto: 0,
    //Codigo: "",
    Nombre: "",
    Descripcion: "",
    PrecioUnidadVenta: 0.0, // Inicializado como float
    Activo: true,
    ImageFulP: ""
}

$(document).ready(function () {
    dtProduc();
})

function dtProduc() {
    // Verificar si el DataTable ya está inicializado
    if ($.fn.DataTable.isDataTable("#tbProduct")) {
        // Destruir el DataTable existente
        $("#tbProduct").DataTable().destroy();
        // Limpiar el contenedor del DataTable
        $('#tbProduct tbody').empty();
    }

    table = $("#tbProduct").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelProducto.aspx/ObtenerProductos',
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
            { "data": "IdProducto", "visible": false, "searchable": false },
            {
                "data": "ImageFulP", render: function (data) {
                    return `<img style="height:40px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "Codigo" },
            { "data": "Nombre" },
            { "data": "TotalCadena" },
            {
                "data": "Activo", render: function (data) {
                    if (data === true)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-danger">No Activo</span>';
                }
            },
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
                filename: 'Informe Productos',
                exportOptions: {
                    columns: [2, 3, 4, 5] // Ajusta según las columnas que desees exportar
                }
            },
            'pageLength'
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function mostrarImagenSeleccionadaP(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgUsuarioP').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);

        // Actualiza el nombre del archivo en el label
        var fileName = input.files[0].name;
        var nextSibling = $(input).next('.custom-file-label');
        nextSibling.text(fileName);
    } else {
        $('#imgUsuarioP').attr('src', "ImagePro/produ.jpg");

        // Restablece el texto del label
        var nextSibling = $(input).next('.custom-file-label');
        nextSibling.text('Ningún archivo seleccionado');
    }
}

$('#txtFotoP').change(function () {
    mostrarImagenSeleccionadaP(this);
});

$.fn.inputFilter = function (inputFilter) {
    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function (e) { // Captura el evento como 'e'
        if (inputFilter(this.value) || e.key === "Backspace" || e.key === " ") { // Usa 'e' en lugar de 'event'
            this.oldValue = this.value;
            this.oldSelectionStart = this.selectionStart;
            this.oldSelectionEnd = this.selectionEnd;
        } else if (this.hasOwnProperty("oldValue")) {
            this.value = this.oldValue;
            this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
        } else {
            this.value = "";
        }
    });
};

$("#txtNombrePr").inputFilter(function (value) {
    return /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$/u.test(value);
});

function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdProducto").val(modelo.IdProducto);
    $("#txtNombrePr").val(modelo.Nombre);
    $("#txtDescripcionPr").val(modelo.Descripcion);
    $("#txtPrecioPr").val(modelo.PrecioUnidadVenta);
    $("#cboEstadoPr").val(modelo.Activo == true ? 1 : 0);
    $("#imgUsuarioP").attr("src", modelo.ImageFulP == "" ? "ImagePro/produ.jpg" : modelo.ImageFulP);

    // Configurar el estado de cboEstado según cboEstadoDeshabilitado jquery v 1.11.1
    $("#cboEstadoPr").prop("disabled", cboEstadoDeshabilitado);
    $("#txtFotoP").val("");
    $(".custom-file-label").text('Ningún archivo seleccionado');

    // Actualizar el título del modal
    if (cboEstadoDeshabilitado) {
        $("#myTitulop").text("Nuevo Producto");
    } else {
        $("#myTitulop").text("Editar Producto");
    }

    $("#modalrolp").modal("show");
}

$("#tbProduct tbody").on("click", ".btn-editar", function (e) {
    e.preventDefault();
    let filaSeleccionada;

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const model = table.row(filaSeleccionada).data();
    mostrarModal(model, false);
})

$('#btnNuevoProd').on('click', function () {
    mostrarModal(null, true);
    //$("#modalrolp").modal("show");
})

function sendDataToServer(request) {
    $.ajax({
        type: "POST",
        url: "PanelProducto.aspx/GuardarProductoNu",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtProduc();
                $('#modalrolp').modal('hide');
                swal("Mensaje", response.d.Valor, "success");
            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCambiosP').prop('disabled', false);
        }
    });
}


function registerDataAjax() {
    var fileInput = document.getElementById('txtFotoP');
    var file = fileInput.files[0];

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdProducto"] = parseInt($("#txtIdProducto").val());
    modelo["Nombre"] = $("#txtNombrePr").val();
    modelo["Descripcion"] = $("#txtDescripcionPr").val();
    modelo["PrecioUnidadVenta"] = parseFloat($("#txtPrecioPr").val()); // Convertir a float

    if (file) {

        var maxSize = 2 * 1024 * 1024; // 2 MB en bytes
        if (file.size > maxSize) {
            swal("Mensaje", "La imagen seleccionada es demasiado grande max 1.5 Mb.", "warning");
            // Rehabilitar el botón si hay un error de validación
            $('#btnGuardarCambiosP').prop('disabled', false);
            return;
        }

        var reader = new FileReader();

        reader.onload = function (e) {
            var arrayBuffer = e.target.result;
            var bytes = new Uint8Array(arrayBuffer);

            var request = {
                oProducto: modelo,
                imageBytes: Array.from(bytes)
            };

            sendDataToServer(request);
        };

        reader.readAsArrayBuffer(file);
    } else {
        // Si no se selecciona ningún archivo, envía un valor nulo o vacío para imageBytes
        var request = {
            oProducto: modelo,
            imageBytes: null // o cualquier otro valor que indique que no se envió ningún archivo
        };

        sendDataToServer(request);
    }
}

function sendDataToServerEditU(request) {
    $.ajax({
        type: "POST",
        url: "PanelProducto.aspx/EditarProducto",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtProduc();
                $('#modalrolp').modal('hide');
                swal("Mensaje", response.d.Valor, "success");
            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCambiosP').prop('disabled', false);
        }
    });
}


function editarDataAjaxU() {
    var fileInput = document.getElementById('txtFotoP');
    var file = fileInput.files[0];

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdProducto"] = parseInt($("#txtIdProducto").val());
    modelo["Nombre"] = $("#txtNombrePr").val();
    modelo["Descripcion"] = $("#txtDescripcionPr").val();
    modelo["PrecioUnidadVenta"] = parseFloat($("#txtPrecioPr").val()); // Convertir a float
    modelo["Activo"] = ($("#cboEstadoPr").val() == "1" ? true : false);

    if (file) {

        var maxSize = 2 * 1024 * 1024; // 2 MB en bytes
        if (file.size > maxSize) {
            swal("Mensaje", "La imagen seleccionada es demasiado grande max 1.5 Mb.", "warning");
            // Rehabilitar el botón si hay un error de validación
            $('#btnGuardarCambiosP').prop('disabled', false);
            return;
        }

        var reader = new FileReader();

        reader.onload = function (e) {
            var arrayBuffer = e.target.result;
            var bytes = new Uint8Array(arrayBuffer);

            var request = {
                oProducto: modelo,
                imageBytes: Array.from(bytes)
            };

            sendDataToServerEditU(request);
        };

        reader.readAsArrayBuffer(file);
    } else {
        // Si no se selecciona ningún archivo, envía un valor nulo o vacío para imageBytes
        var request = {
            oProducto: modelo,
            imageBytes: null // o cualquier otro valor que indique que no se envió ningún archivo
        };

        sendDataToServerEditU(request);
    }
}
$('#btnGuardarCambiosP').on('click', function () {

    // Deshabilitar el botón para evitar múltiples envíos
    $('#btnGuardarCambiosP').prop('disabled', true);

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()

        // Rehabilitar el botón si hay campos vacíos
        $('#btnGuardarCambiosP').prop('disabled', false);
        return;
    }

    var preciot = parseFloat($("#txtPrecioPr").val().trim());
    if (isNaN(preciot) || preciot === 0) {
        toastr.warning("", "Debe ingresar un Monto válido");
        $("#txtPrecioPr").focus();
        // Rehabilitar el botón si es 0 o letra
        $('#btnGuardarCambiosP').prop('disabled', false);
        return;
    }

    //$('#btnGuardarCambios').prop('disabled', true);

    if (parseInt($("#txtIdProducto").val()) === 0) {
        registerDataAjax();
    } else {
        //swal("Mensaje", "Falta para Actualizar personal.", "warning")
        // Rehabilitar el botón en caso de advertencia
        //$('#btnGuardarCambios').prop('disabled', false);
        editarDataAjaxU();
    }
})