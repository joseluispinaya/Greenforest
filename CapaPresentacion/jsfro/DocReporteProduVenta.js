


$(document).ready(function () {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const fechainicioEncoded = urlParams.get('fi');
    const fechafinEncoded = urlParams.get('ff');

    const fechainicio = decodeURIComponent(fechainicioEncoded);
    const fechafin = decodeURIComponent(fechafinEncoded);

    if (fechainicio.trim() === "" || fechafin.trim() === "") {
        alert("No hay un parámetro válido recibido. El formulario se cerrará.");
        window.close();
    } else {
        CargarDatosGroupFech(fechainicio, fechafin);
    }

});

function ObtenerFecha() {

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + d.getFullYear();

    return output;
}

function CargarDatosGroupFech($fechainicio, $fechafin) {

    $("#tbDetalles tbody").html("");

    var request = {
        fechainicio: $fechainicio,
        fechafin: $fechafin
    };
    $.ajax({
        type: "POST",
        url: "PanelRepoProducto.aspx/ObtenerReporteFechas",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            if (response.d.Estado) {
                $("#codi").text("Nro: 0125450");
                $("#fechar").text(ObtenerFecha());
                $("#razonsocial").text("De: " + $fechainicio);
                $("#ruc").text("Hasta: " + $fechafin);

                $("#tbDetalles tbody").html("");

                // Variables para acumular totales
                var totalCantidad = 0;
                var totalCosto = 0;

                // Recorrer la lista de productos y añadir cada uno al tbody
                $.each(response.d.Data, function (i, row) {
                    totalCantidad += row.CantidadTotal; // Sumar la cantidad al total
                    totalCosto += row.MontoTotal;

                    $("<tr>").append(
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.NombreProducto)),
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.Codigo)),
                        $("<td>").addClass("td-item").append(
                            $("<img>").attr("src", row.ImageFulRe)
                                .css("height", "30px")
                                .addClass("rounded mx-auto d-block")
                        ),
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.PrecioUnidadVenta.toFixed(2))),
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.CantidadTotal)),
                        $("<td>").css("background-color", "#EDF6F9").append($("<p>").addClass("item").text(row.TotalCadena)) // Mostrar con 2 decimales y color de fondo
                    ).appendTo("#tbDetalles tbody");
                });

                // Actualizar los totales en los campos correspondientes
                $("#cantTotal").text(totalCantidad + " Unds");
                $("#costoTotal").text(totalCosto.toFixed(2) + " /USD.");

            } else {
                alert("Ocurrió un error. El formulario se cerrará.");
                window.close();
            }

        }
    });
}

function imprSelec(nombre) {
    var printContents = document.getElementById(nombre).innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}
function hide() {
    document.getElementById('Imprimir').style.visibility = "hidden";
}