

$(document).ready(function () {

    totalesLabel();
    sendDetalleVentas();
    sendDataVentas();
});

function totalesLabel() {

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/ObtenerTotalesLabel",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            //console.log(response.d.objeto);
            if (response.d.Estado) {
                //$("#totalIngresos").text(response.d.objeto);
                $("#totalVenta").text(response.d.Data.NombreProducto + " Ventas");
                $("#totalIngresos").text(response.d.Data.Codigo + " /USD");
                $("#totalProductos").text(response.d.Data.Imagen + " Productos");
                $("#totalClientes").text(response.d.Data.Descripcion + " Clientes");
            }

        }
    });
}

function addDVenta(response) {
    const barchar_labele = [];
    const barchar_datae = [];

    var totall = 0;
    let iterations = 0;

    $.each(response.Data, function (i, row) {
        var tot = row.MontoTotal;
        totall = totall + tot;

        if (iterations < 4) {
            barchar_labele.push(row.NombreProducto);
            barchar_datae.push(row.CantidadTotal);
            iterations++;
        }
    });

    // Pie Chart Example
    let controlProducto = document.getElementById("pieProductos");
    let myPieChart = new Chart(controlProducto, {
        type: 'doughnut',
        data: {
            labels: barchar_labele,
            datasets: [{
                data: barchar_datae,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', "#FF785B"],
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', "#FF5733"],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
            },
            legend: {
                display: true
            },
            cutoutPercentage: 80,
        },
    });

    //$("#totalIngresos").text(totall);
}

function sendDetalleVentas() {

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/DetalleVentaDash",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            //console.log(response.d);
            if (response.d.Estado) {
                addDVenta(response.d);
            }

        }
    });
}

function addDVentaPorFecha(response) {
    const barchar_labels = [];
    const barchar_data = [];

    $.each(response.Data, function (i, row) {
        barchar_labels.push(row.FechaRegistro);
        barchar_data.push(row.IdVenta);
        //console.log(row.IdCategoria);
    })
    //console.log(barchar_labels);
    //console.log(barchar_data);

    // Bar Chart Example
    let controlVenta = document.getElementById("barVentas");
    let myBarChart = new Chart(controlVenta, {
        type: 'bar',
        data: {
            labels: barchar_labels,
            datasets: [{
                label: "Cantidad",
                backgroundColor: "#4e73df",
                hoverBackgroundColor: "#2e59d9",
                borderColor: "#4e73df",
                data: barchar_data,
            }],
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            },
        }
    });

}

function sendDataVentas() {

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/ObtenerListaVenta",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            //console.log(response.d);
            if (response.d.Estado) {
                addDVentaPorFecha(response.d);
                console.log(response.d.Data);
            }

        }
    });
}