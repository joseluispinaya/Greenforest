


var table;
function ObtenerFechaA() {

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + month).length < 2 ? '0' : '') + month + '/' + (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    return output;
}

$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: "mm/dd/yy" });
    $("#txtFechaFin").datepicker({ dateFormat: "mm/dd/yy" });

    $("#txtFechaInicio").val(ObtenerFechaA());
    $("#txtFechaFin").val(ObtenerFechaA());
    dtDatosConsulta();
});

function dtDatosConsulta() {
    if ($.fn.DataTable.isDataTable("#tbdatare")) {
        $("#tbdatare").DataTable().destroy();
        $('#tbdatare tbody').empty();
    }

    table = $("#tbdatare").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelRepoProducto.aspx/ObtenerReporteTodo',
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            },
            "beforeSend": function () {
                $.LoadingOverlay("show"); // Mostrar el overlay de carga antes de enviar la solicitud
            },
            "complete": function () {
                $.LoadingOverlay("hide"); // Ocultar el overlay de carga una vez que la solicitud ha terminado
            }
        },
        "columns": [
            { "data": "NombreProducto" },
            { "data": "Descripcion" },
            { "data": "Codigo" },
            {
                "data": "ImageFulRe", render: function (data) {
                    return `<img style="height:40px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "PrecioUnidadVenta" },
            { "data": "CantidadTotal" },
            { "data": "TotalCadena" }
        ],
        "dom": "rt",
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function dtDatosConsultaFecha() {
    if ($.fn.DataTable.isDataTable("#tbdatare")) {
        $("#tbdatare").DataTable().destroy();
        $('#tbdatare tbody').empty();
    }

    var request = {
        fechainicio: $("#txtFechaInicio").val(),
        fechafin: $("#txtFechaFin").val()
    };

    table = $("#tbdatare").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PanelRepoProducto.aspx/ObtenerReporteFechas',
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function () {
                return JSON.stringify(request);
            },
            "dataSrc": function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            },
            "beforeSend": function () {
                $.LoadingOverlay("show"); // Mostrar el overlay de carga antes de enviar la solicitud
            },
            "complete": function () {
                $.LoadingOverlay("hide"); // Ocultar el overlay de carga una vez que la solicitud ha terminado
            }
        },
        "columns": [
            { "data": "NombreProducto" },
            { "data": "Descripcion" },
            { "data": "Codigo" },
            {
                "data": "ImageFulRe", render: function (data) {
                    return `<img style="height:40px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "PrecioUnidadVenta" },
            { "data": "CantidadTotal" },
            { "data": "TotalCadena" }
        ],
        "dom": "rt",
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#btnBuscar').on('click', function () {

    dtDatosConsultaFecha();
    //swal("Mensaje", "Falta Implementar Este boton", "warning");

})