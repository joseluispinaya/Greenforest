
var table;
const MODELO_BASE = {
    IdCliente: 0,
    Ruc: "",
    RazonSocial: "",
    Direccion: "",
    Telefono: "",
    Correo: ""
}

$(document).ready(function () {
    
    //dtClen();

});
function dtClen() {
    // Verificar si el DataTable ya está inicializado
    if ($.fn.DataTable.isDataTable("#tbCliente")) {
        // Destruir el DataTable existente
        $("#tbCliente").DataTable().destroy();
        // Limpiar el contenedor del DataTable
        $('#tbCliente tbody').empty();
    }

    table = $("#tbCliente").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelCliente.aspx/Obtener',
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
            { "data": "IdCliente", "visible": false, "searchable": false },
            { "data": "Ruc" },
            { "data": "RazonSocial" },
            { "data": "Telefono" },
            { "data": "Correo" },
            { "data": "FechaRegistro" },
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
                filename: 'Informe Clientes',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5] // Ajusta según las columnas que desees exportar
                }
            },
            'pageLength'
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdCliente").val(modelo.IdCliente);
    $("#txtRuc").val(modelo.Ruc);
    $("#txtRazon").val(modelo.RazonSocial);
    $("#txtDireccion").val(modelo.Direccion);
    $("#txtTelefono").val(modelo.Telefono);
    $("#txtCorreo").val(modelo.Correo);

    if (cboEstadoDeshabilitado) {
        $("#myTitulop").text("Nuevo Cliente");
    } else {
        $("#myTitulop").text("Editar Cliente");
    }

    $("#modalclien").modal("show");
}

$("#tbCliente tbody").on("click", ".btn-editar", function (e) {
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

$('#btnAdd').on('click', function () {
    mostrarModal(null, true);
    //$("#modalclien").modal("show");
})

function dataRegistrar() {
    const modelo = structuredClone(MODELO_BASE);
    modelo["IdCliente"] = parseInt($("#txtIdCliente").val());
    modelo["Ruc"] = $("#txtRuc").val().trim();
    modelo["RazonSocial"] = $("#txtRazon").val().trim();
    modelo["Direccion"] = $("#txtDireccion").val().trim();
    modelo["Telefono"] = $("#txtTelefono").val().trim();
    modelo["Correo"] = $("#txtCorreo").val().trim();

    var request = {
        oCliente: modelo
    };

    $.ajax({
        type: "POST",
        url: "PanelCliente.aspx/GuardarPru",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            // Inicializa el progreso personalizado y muestra el LoadingOverlay
            var progress2 = new LoadingOverlayProgress({
                bar: {
                    "background": "#dd0000",
                    "top": "50px",
                    "height": "30px",
                    "border-radius": "15px"
                },
                text: {
                    "color": "#aa0000",
                    "font-family": "monospace",
                    "top": "25px"
                }
            });

            // Muestra el overlay con texto personalizado
            $(".modal-content").LoadingOverlay("show", {
                custom: progress2.Init("Encriptando...")
            });

            // Simular progreso de 0 a 100
            var count2 = 0;
            var iId2 = setInterval(function () {
                if (count2 >= 100) {
                    clearInterval(iId2);
                    delete progress2;
                    return;
                }
                count2++;
                progress2.Update(count2);
            }, 50);
        },
        success: function (response) {
            // Mantener el LoadingOverlay visible por un mínimo de 1 segundo
            setTimeout(function () {
                $(".modal-content").LoadingOverlay("hide");  // Ocultar el overlay después del éxito
                if (response.d.Estado) {
                    dtClen();
                    $('#modalclien').modal('hide');
                    swal("Mensaje", response.d.Valor, "success");
                } else {
                    swal("Mensaje", response.d.Valor, "warning");
                }
            }, 7000); // Tiempo de retraso en milisegundos (1 segundo)
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");  // Ocultar el overlay después del error
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCamClie').prop('disabled', false);
        }
    });
}

function dataActualizar() {
    const modelo = structuredClone(MODELO_BASE);
    modelo["IdCliente"] = parseInt($("#txtIdCliente").val());
    modelo["Ruc"] = $("#txtRuc").val().trim();
    modelo["RazonSocial"] = $("#txtRazon").val().trim();
    modelo["Direccion"] = $("#txtDireccion").val().trim();
    modelo["Telefono"] = $("#txtTelefono").val().trim();
    modelo["Correo"] = $("#txtCorreo").val().trim();

    var request = {
        oCliente: modelo
    };

    $.ajax({
        type: "POST",
        url: "PanelCliente.aspx/Editar",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            // Mostrar overlay de carga antes de enviar la solicitud modal-content
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                dtClen();
                $('#modalclien').modal('hide');
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
            $('#btnGuardarCamClie').prop('disabled', false);
        }
    });
}

$('#btnGuardarCamClie').on('click', function () {

    // Deshabilitar el botón para evitar múltiples envíos
    $('#btnGuardarCamClie').prop('disabled', true);

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()

        // Rehabilitar el botón si hay campos vacíos
        $('#btnGuardarCamClie').prop('disabled', false);
        return;
    }
    var emailRegex = /^[a-zA-Z0-9._%+-ñÑáéíóúÁÉÍÓÚ]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    var correo = $("#txtCorreo").val().trim();

    if (correo === "" || !emailRegex.test(correo)) {
        toastr.warning("", "Debe ingresar un Correo válido");
        $("#txtCorreo").focus();
        // Rehabilitar el botón si hay campos vacíos
        $('#btnGuardarCamClie').prop('disabled', false);
        return;
    }
    

    if (parseInt($("#txtIdCliente").val()) === 0) {
        dataRegistrar();
    } else {
        //dataActualizar();
        swal("Mensaje", "Error al registrar", "warning");
        // Rehabilitar el botón si hay campos vacíos
        $('#btnGuardarCamClie').prop('disabled', false);
    }
})

$('#btnListar').on('click', function () {
    //loaddd
    //loaddd
    // Initialize Progress and show LoadingOverlay
    var progress2 = new LoadingOverlayProgress({
        bar: {
            "background": "#dd0000",
            "top": "50px",
            "height": "30px",
            "border-radius": "15px"
        },
        text: {
            "color": "#aa0000",
            "font-family": "monospace",
            "top": "25px"
        }
    });
    $.LoadingOverlay("show", {
        custom: progress2.Init("Desencriptando...")  // Texto personalizado
    });

    // Simulate some other action:
    var count2 = 0;
    var iId2 = setInterval(function () {
        if (count2 >= 100) {
            clearInterval(iId2);
            delete progress2;
            $.LoadingOverlay("hide");

            // Llamar a la función dtClen() una vez que el overlay se oculta
            dtClen();
            return;
        }
        count2++;
        progress2.Update(count2);
    }, 50);

})

$('#btnDetallee').on('click', function () {
    //loaddd
    // Initialize Progress and show LoadingOverlay
    var progress2 = new LoadingOverlayProgress({
        bar: {
            "background": "#dd0000",
            "top": "50px",
            "height": "30px",
            "border-radius": "15px"
        },
        text: {
            "color": "#aa0000",
            "font-family": "monospace",
            "top": "25px"
        }
    });
    $.LoadingOverlay("show", {
        custom: progress2.Init("Desencriptando...")  // Texto personalizado
    });

    // Simulate some other action:
    var count2 = 0;
    var iId2 = setInterval(function () {
        if (count2 >= 100) {
            clearInterval(iId2);
            delete progress2;
            $.LoadingOverlay("hide");

            // Llamar a la función dtClen() una vez que el overlay se oculta
            dtClen(); 
            return;
        }
        count2++;
        progress2.Update(count2);
    }, 50);

})