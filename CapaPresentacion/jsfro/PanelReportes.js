
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

});

function dtDatosConsulta() {
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
            "url": 'PanelReportes.aspx/ObtenerReporte',
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
            { "data": "FechaRegistro" },
            { "data": "Codigo" },
            { "data": "Cliente" },
            { "data": "Producto" },
            { "data": "TotalRepo" }
        ],
        "dom": "rt",
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#btnBuscar').on('click', function () {

    dtDatosConsulta();
    //swal("Mensaje", "Falta Implementar Este boton", "warning");

})

$('#btnImprimir').on('click', function () {

    $('#accordionSidebar').hide();
    $('#omitirep').hide();
    window.print();
    $('#accordionSidebar').show();
    $('#omitirep').show();

})