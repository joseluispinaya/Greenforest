

$(document).ready(function () {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const IdVenta = urlParams.get('id')

    if (IdVenta !== null) {
        CargarDatos(IdVenta);
        //swal("Mensaje", "Llego bien.", "success");
    } else {
        swal("Mensaje", "No hay parámetro recibido. El formulario se cerrará.", "warning");
        window.close();
    }
});

function CargarDatos($IdVenta) {
    $('#tbDetalles tbody').html(''); // Limpiar el tbody antes de llenarlo

    var request = {
        IdVenta: $IdVenta
    };

    $.ajax({
        type: "POST",
        url: "PanelVenta.aspx/DetalleVenta",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            if (response.d.Estado) {
                // Actualizar los detalles de la venta
                $("#codi").text("Nro: " + response.d.Data.Codigo);
                $("#fechar").text(response.d.Data.FechaRegistro);
                $("#razonsocial").text(response.d.Data.Cliente.RazonSocial);
                $("#ruc").text(response.d.Data.Cliente.Ruc);

                // Limpiar el tbody de la tabla de productos
                $("#tbDetalles tbody").html("");

                // Variables para acumular totales
                var totalCantidad = 0;
                var totalCosto = response.d.Data.TotalCosto;

                // Recorrer la lista de productos y añadir cada uno al tbody
                $.each(response.d.Data.ListaDetalleVenta, function (i, row) {
                    totalCantidad += row.Cantidad; // Sumar la cantidad al total

                    $("<tr>").append(
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.NombreProducto)),
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.Cantidad)),
                        $("<td>").addClass("td-item").append($("<p>").addClass("item").text(row.PrecioUnidad.toFixed(2))), // Mostrar con 2 decimales
                        $("<td>").css("background-color", "#EDF6F9").append($("<p>").addClass("item").text(row.ImporteTotal.toFixed(2))) // Mostrar con 2 decimales y color de fondo
                    ).appendTo("#tbDetalles tbody");
                });

                // Actualizar los totales en los campos correspondientes
                $("#cantTotal").text(totalCantidad + " Items");
                $("#costoTotal").text(totalCosto.toFixed(2) + " /Bs.");
            } else {
                swal("Mensaje", "Ocurrió un error. El formulario se cerrará.", "warning");
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

window.addEventListener('beforeunload', function (e) {
    var confirmationMessage = '¿Seguro que quieres salir?';
    
    e.preventDefault();
    e.returnValue = confirmationMessage;
    return confirmationMessage;
});

window.addEventListener('unload', function (e) {
    setTimeout(function () {
        window.close();
    }, 3000);
});