

var table;

const MODELO_BASE = {
    IdUsuario: 0,
    Nombres: "",
    Apellidos: "",
    Correo: "",
    Clave: "",
    IdRol: 0,
    Estado: true,
    ImageFull: ""
}

$(document).ready(function () {
    //$('#data-table-buttons').DataTable({
    //    responsive: true
    //});
    cargarRoles();
    dtUsuarios();
})

function dtUsuarios() {
    // Verificar si el DataTable ya está inicializado
    if ($.fn.DataTable.isDataTable("#tbUsuario")) {
        // Destruir el DataTable existente
        $("#tbUsuario").DataTable().destroy();
        // Limpiar el contenedor del DataTable
        $('#tbUsuario tbody').empty();
    }

    table = $("#tbUsuario").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelUsuario.aspx/ObtenerUsuario',
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
            { "data": "IdUsuario", "visible": false, "searchable": false },
            {
                "data": "ImageFull", render: function (data) {
                    return `<img style="height:40px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "Nombres" },
            { "data": "Apellidos" },
            { "data": "Correo" },
            { "data": "Orol.NomRol" },
            {
                "data": "Estado", render: function (data) {
                    if (data == true)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-danger">No Activo</span>';
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>',
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
                filename: 'Informe Usuarios',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6] // Ajusta según las columnas que desees exportar
                }
            },
            'pageLength'
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function cargarRoles() {
    $("#cboRol").html("");

    $.ajax({
        type: "POST",
        url: "PanelUsuario.aspx/ObtenerRol",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            if (data.d.Estado) {
                $.each(data.d.Data, function (i, row) {
                    if (row.Activo == true) {
                        $("<option>").attr({ "value": row.Idrol }).text(row.NomRol).appendTo("#cboRol");
                    }

                })
            }

        }
    });
}

function mostrarImagenSeleccionada(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgUsuarioM').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    } else {
        $('#imgUsuarioM').attr('src', "Imagenes/sinimagen.png");
    }
}

$('#txtFoto').change(function () {
    mostrarImagenSeleccionada(this);
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

$("#txtNombre").inputFilter(function (value) {
    return /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$/u.test(value);
});
$("#txtapellido").inputFilter(function (value) {
    return /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$/u.test(value);
});


function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdUsuario").val(modelo.IdUsuario);
    $("#txtNombre").val(modelo.Nombres);
    $("#txtClave").val(modelo.Clave);
    $("#txtapellido").val(modelo.Apellidos);
    $("#txtCorreo").val(modelo.Correo);
    $("#cboRol").val(modelo.IdRol == 0 ? $("#cboRol option:first").val() : modelo.IdRol);
    $("#cboEstado").val(modelo.Estado == true ? 1 : 0);
    $("#imgUsuarioM").attr("src", modelo.ImageFull == "" ? "Imagenes/sinimagen.png" : modelo.ImageFull);

    // Configurar el estado de cboEstado según cboEstadoDeshabilitado jquery v 1.11.1
    $("#cboEstado").prop("disabled", cboEstadoDeshabilitado);

    //$("#txtCorreo").prop("disabled", !cboEstadoDeshabilitado);
    $("#txtFoto").val("");

    $("#modalData").modal("show");
}

$("#tbUsuario tbody").on("click", ".btn-editar", function (e) {
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

$('#btnNuevoUsu').on('click', function () {
    mostrarModal(null, true);
    //$("#modalData").modal("show");
})

function sendDataToServer(request) {
    $.ajax({
        type: "POST",
        url: "PanelUsuario.aspx/GuardarUsua",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtUsuarios();
                $('#modalData').modal('hide');
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
            $('#btnGuardarCambios').prop('disabled', false);
        }
    });
}


function registerDataAjax() {
    var fileInput = document.getElementById('txtFoto');
    var file = fileInput.files[0];

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdUsuario"] = parseInt($("#txtIdUsuario").val());
    modelo["Nombres"] = $("#txtNombre").val();
    modelo["Clave"] = $("#txtClave").val();
    modelo["Apellidos"] = $("#txtapellido").val();
    modelo["Correo"] = $("#txtCorreo").val();
    modelo["IdRol"] = $("#cboRol").val();

    if (file) {

        var maxSize = 2 * 1024 * 1024; // 2 MB en bytes
        if (file.size > maxSize) {
            swal("Mensaje", "La imagen seleccionada es demasiado grande max 1.5 Mb.", "warning");
            // Rehabilitar el botón si hay un error de validación
            $('#btnGuardarCambios').prop('disabled', false);
            return;
        }

        var reader = new FileReader();

        reader.onload = function (e) {
            var arrayBuffer = e.target.result;
            var bytes = new Uint8Array(arrayBuffer);

            var request = {
                oUsuario: modelo,
                imageBytes: Array.from(bytes)
            };

            sendDataToServer(request);
        };

        reader.readAsArrayBuffer(file);
    } else {
        // Si no se selecciona ningún archivo, envía un valor nulo o vacío para imageBytes
        var request = {
            oUsuario: modelo,
            imageBytes: null // o cualquier otro valor que indique que no se envió ningún archivo
        };

        sendDataToServer(request);
    }
}

$('#btnGuardarCambios').on('click', function () {

    // Deshabilitar el botón para evitar múltiples envíos
    $('#btnGuardarCambios').prop('disabled', true);

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()

        // Rehabilitar el botón si hay campos vacíos
        $('#btnGuardarCambios').prop('disabled', false);
        return;
    }

    //$('#btnGuardarCambios').prop('disabled', true);

    if (parseInt($("#txtIdUsuario").val()) === 0) {
        registerDataAjax();
    } else {
        swal("Mensaje", "Falta para Actualizar personal.", "warning")
        // Rehabilitar el botón en caso de advertencia
        $('#btnGuardarCambios').prop('disabled', false);
        //editarDataAjaxU();
    }
})