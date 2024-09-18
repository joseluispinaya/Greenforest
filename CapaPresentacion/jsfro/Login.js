

//$(document).ready(function () {

//    obtenerIp();

//});



//function obtenerIp() {

//    fetch('https://api.ipify.org?format=json')
//        .then(response => response.json())
//        .then(data => {
//            console.log("Tu IP pública es:", data.ip);
//        })
//        .catch(error => {
//            console.error("Error al obtener la IP:", error);
//        });
//}

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