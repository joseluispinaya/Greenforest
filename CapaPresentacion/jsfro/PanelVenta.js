

function ObtenerFecha() {

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + d.getFullYear();

    return output;
}

$(document).ready(function () {

    $("#txtSubTotal").val(ObtenerFecha());
    cargarClien();
    cargarPro();

})

function cargarClien() {

    $("#cboBuscarCliente").select2({
        ajax: {
            url: "PanelVenta.aspx/BuscarClie",
            dataType: 'json',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return JSON.stringify({ buscar: params.term });
            },
            processResults: function (data) {

                return {
                    results: data.d.Data.map((item) => ({
                        id: item.IdCliente,
                        text: item.RazonSocial,
                        Telefono: item.Telefono,
                        Correo: item.Correo,
                        cliente: item
                    }))
                };
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            }
        },
        language: "es",
        placeholder: 'Buscar Cliente',
        minimumInputLength: 1,
        templateResult: formatoRes
    });
}

function formatoRes(data) {

    var imagenes = "ImagePro/logogre.jpeg";
    // Esto es por defecto, ya que muestra el "buscando..."
    if (data.loading)
        return data.text;

    var contenedor = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:60px;width:60px;margin-right:10px" src="${imagenes}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.text}</p>
                    <p style="margin:2px">${data.Correo}</p>
                </td>
            </tr>
        </table>`
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();

});

// Evento para manejar la selección del cliente
$("#cboBuscarCliente").on("select2:select", function (e) {

    var data = e.params.data.cliente;
    $("#txtIdclienteAtec").val(data.IdCliente);
    $("#txtNombreClienteat").val(data.RazonSocial);

    $("#cboBuscarCliente").val("").trigger("change")
    //console.log(data);
});

function cargarPro() {

    $("#cboBuscarProductov").select2({
        ajax: {
            url: "PanelVenta.aspx/BuscarPro",
            dataType: 'json',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return JSON.stringify({ buscar: params.term });
            },
            processResults: function (data) {

                return {
                    results: data.d.Data.map((item) => ({
                        id: item.IdProducto,
                        text: item.Nombre,
                        TotalCadena: item.TotalCadena,
                        ImageFulP: item.ImageFulP,
                        PrecioUnidadVenta: parseFloat(item.PrecioUnidadVenta)
                    }))
                };
            },
        },
        language: "es",
        placeholder: 'Buscar Producto',
        minimumInputLength: 1,
        templateResult: formatoResultados
    });
}

function formatoResultados(data) {
    // Esto es por defecto, ya que muestra el "buscando..."
    if (data.loading)
        return data.text;

    var contenedor = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:60px;width:60px;margin-right:10px" src="${data.ImageFulP}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.TotalCadena}</p>
                    <p style="margin:2px">${data.text}</p>
                </td>
            </tr>
        </table>`
    );

    return contenedor;
}

let ProductosParaVentaC = [];

$("#cboBuscarProductov").on("select2:select", function (e) {
    const data = e.params.data;

    let producto_encontradov = ProductosParaVentaC.filter(p => p.IdProducto == data.id)
    if (producto_encontradov.length > 0) {
        $("#cboBuscarProductov").val("").trigger("change")
        toastr.warning("", "El producto ya fue agregado")
        return false;
    }

    swal({
        title: data.text,
        text: data.TotalCadena,
        imageUrl: data.ImageFulP,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Ingrese Cantidad"
    }, function (valor) {
        if (valor === false) {
            return false;
        }

        if (valor === "") {
            toastr.warning("", "Necesita ingresar la cantidad")
            return false;
        }
        if (isNaN(parseInt(valor))) {
            toastr.warning("", "Debe ser un valor numerico")
            return false;
        }

        if (parseInt(valor) <= 0) {
            toastr.warning("", "La cantidad debe ser mayor a cero");
            return false;
        }

        let productod = {
            IdProducto: data.id,
            NombreProducto: data.text,
            Cantidad: parseInt(valor),
            PrecioUnidad: parseFloat(data.PrecioUnidadVenta),
            ImporteTotal: (parseFloat(valor) * data.PrecioUnidadVenta)
        }

        ProductosParaVentaC.push(productod)
        //console.log(ProductosParaVentaC);

        mosProdr_Precio();
        $("#cboBuscarProductov").val("").trigger("change")
        swal.close();
    }
    )
})

function mosProdr_Precio() {
    let total = 0;
    let subtotal = 0;
    let porcentaje = 0.18;

    $("#tbVentaca tbody").html("");

    ProductosParaVentaC.forEach((item) => {

        total = total + parseFloat(item.ImporteTotal)

        $("#tbVentaca tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idProductoa", item.IdProducto)
                ),
                $("<td>").text(item.NombreProducto),
                $("<td>").text(item.Cantidad),
                $("<td>").text(item.PrecioUnidad),
                $("<td>").text(item.ImporteTotal)
            )
        )
    })
    //subtotal = total / (1 + porcentaje);
    //$("#txtSubTotal").val(subtotal.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));
}

$(document).on('click', 'button.btn-eliminar', function () {
    const _idProducto = $(this).data("idProductoa")
    ProductosParaVentaC = ProductosParaVentaC.filter(p => p.IdProducto != _idProducto);

    mosProdr_Precio();
});

function dataGuardarVentaCliente() {


    //$("#btnTermiCaja").LoadingOverlay("show");

    var totallprodu = 0;

    var DETALLE = "";
    var VENTA = "";
    var DETALLE_VENTA = "";
    var DATOS_VENTA = "";

    ProductosParaVentaC.forEach((item) => {

        totallprodu = totallprodu + parseInt(item.Cantidad)

        DATOS_VENTA = DATOS_VENTA + "<DATOS>" +
            "<IdVenta>0</IdVenta>" +
            "<IdProducto>" + item.IdProducto + "</IdProducto>" +
            "<Cantidad>" + item.Cantidad + "</Cantidad>" +
            "<PrecioUnidad>" + item.PrecioUnidad + "</PrecioUnidad>" +
            "<ImporteTotal>" + item.ImporteTotal + "</ImporteTotal>" +
            "</DATOS>"
    });

    VENTA = "<VENTA>" +
        "<IdCliente>" + $("#txtIdclienteAtec").val() + "</IdCliente>" +
        "<CantidadProducto>" + ProductosParaVentaC.length + "</CantidadProducto>" +
        "<CantidadTotal>" + totallprodu + "</CantidadTotal>" +
        "<TotalCosto>" + $("#txtTotal").val() + "</TotalCosto>" +
        "</VENTA>";

    DETALLE_VENTA = "<DETALLE_VENTA>" + DATOS_VENTA + "</DETALLE_VENTA>";
    DETALLE = "<DETALLE>" + VENTA + DETALLE_VENTA + "</DETALLE>"

    var request = { xml: DETALLE };

    $.ajax({
        type: "POST",
        url: "PanelVenta.aspx/GuardarVentaIdCliente",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $("#loaaaV").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loaaaV").LoadingOverlay("hide");
            if (response.d.Estado) {
                // Reseteo de campos y tabla después de un éxito
                ProductosParaVentaC = [];
                mosProdr_Precio();

                $("#txtIdclienteAtec").val("0");
                $("#txtNombreClienteat").val("");

                //swal("Mensaje", response.d.Valor, "success");

                var url = 'frmDocVenta.aspx?id=' + response.d.Valor;

                $("#overlayc").LoadingOverlay("show");
                var popup = window.open(url, '', 'height=600,width=800,scrollbars=0,location=1,toolbar=0');

                var timer = setInterval(function () {
                    if (popup.closed) {
                        clearInterval(timer);
                        $("#overlayc").LoadingOverlay("hide");
                    }
                }, 500);

            } else {
                swal("Mensaje", response.d.Valor, "error");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loaaaV").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnTermiCaja').prop('disabled', false);
        }
    });
}

//$('#btnTermiCaja').on('click', function () {
    
//    var url = 'frmDocVenta.aspx?id=' + 1;
//    window.open(url, '', 'height=600,width=800,scrollbars=0,location=1,toolbar=0');

//})

$('#btnTermiCaja').on('click', function () {
    
    $('#btnTermiCaja').prop('disabled', true);

    if (ProductosParaVentaC.length < 1) {
        swal("Mensaje", "Debe registrar minimo un producto en la venta", "warning");
        $('#btnTermiCaja').prop('disabled', false);
        return;
    }
    
    if (parseInt($("#txtIdclienteAtec").val()) === 0) {
        swal("Mensaje", "Debe Seleccionar un Cliente para la venta", "warning");
        $('#btnTermiCaja').prop('disabled', false);
        return;
    }
    dataGuardarVentaCliente();

})