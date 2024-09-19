


$('#ingrsarLo').on('click', function () {

    //VALIDACIONES DE USUARIO
    if ($("#txtcorreo").val().trim() === "" || $("#txtpassword").val().trim() === "") {
        swal("Mensaje", "Complete los datos para iniciar sesion", "warning");
        return;
    }
    
    loginSistema();
})

function loginSistema() {

    $.ajax({
        type: "POST",
        url: "Login.aspx/Logeo",
        data: JSON.stringify({ Usuario: $("#txtcorreo").val().trim(), Clave: $("#txtpassword").val().trim() }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {

            $.LoadingOverlay("show");
        },
        success: function (response) {
            $.LoadingOverlay("hide");
            if (response.d.Estado) {

                sessionStorage.setItem('tokenSesionOrt', response.d.Valor);
                // Almacenar el objeto usuario completo en sessionStorage
                sessionStorage.setItem('usuarioL', JSON.stringify(response.d.Data));
                window.location.href = 'Inicio.aspx';
            } else {
                swal("Mensaje", "No se encontro el usuario", "warning")
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $.LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}


//etiqueta <a> no es boton
$('#btncorreo').on('click', function (e) {
    e.preventDefault(); // Evita que el enlace siga el href
    $("#modalcorr").modal("show");
});

function enviarRecCorreo() {

    $.ajax({
        type: "POST",
        url: "Login.aspx/RecuperacionCl",
        data: JSON.stringify({ correo: $("#txtcxorreo").val().trim() }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                $('#modalcorr').modal('hide');
                swal("Mensaje", response.d.Valor, "success");
            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}



function esCorreoValido(correo) {
    // Expresión regular mejorada para validar correos electrónicos
    var emailRegex = /^[a-zA-Z0-9._%+-ñÑáéíóúÁÉÍÓÚ]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return correo !== "" && emailRegex.test(correo);
}

$('#btnEnviarC').on('click', function () {

    var correo = $("#txtcxorreo").val().trim();
    if (!esCorreoValido(correo)) {
        swal("Mensaje", "Ingrese un correo válido", "warning");
        //mostrarMensaje("Mensaje", "Ingrese un correo válido", "warning");
        return;
    }

    // Deshabilitar el botón para evitar múltiples envíos
    $('#btnEnviarC').text('Enviando...').prop('disabled', true);

    enviarRecCorreo().always(function () {
        // Rehabilitar el botón después de que se complete el envío
        $('#btnEnviarC').text('Enviar').prop('disabled', false);
    });
})