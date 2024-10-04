

$(document).ready(function () {
    // Validar si existen tanto el token como el usuario en sessionStorage
    const tokenSesion = sessionStorage.getItem('tokenSesionOrt');
    const usuarioL = sessionStorage.getItem('usuarioL');

    if (tokenSesion && usuarioL) {
        // Parsear el usuario almacenado
        var usuParaenviar = JSON.parse(usuarioL);
        var idUsu = usuParaenviar.IdUsuario; // Obtener IdUsuario

        // Llamar a obtenerDetalleUsuarioR pasando el IdUsuario
        obtenerDetalleUsuarioR(idUsu);
    } else {
        // Si no hay sesión, redirigir al login
        window.location.href = 'Login.aspx';
    }

    //obtenerDetalleUsuarioR();
});

$('#salirsis').on('click', async function (e) {
    e.preventDefault(); // Evita que el <a> navegue a otra página
    await cerrarSesion(); // Llama a la función asíncrona y espera su finalización
});


async function obtenerDetalleUsuarioR(idUsu) {
    try {
        const response = await $.ajax({
            type: "POST",
            url: "Inicio.aspx/ObtenerToken",
            data: JSON.stringify({ IdUsu: idUsu }), // Puedes dejarlo como JSON vacío
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        });

        if (response.d.Estado) {
            const tokenSession = sessionStorage.getItem('tokenSesionOrt');
            if (tokenSession !== response.d.Valor) {
                await cerrarSesion(); // Llama a la función para cerrar sesión
            } else {
                // Actualiza la información del usuario en la interfaz nuevo
                const usuarioL = sessionStorage.getItem('usuarioL');

                if (usuarioL) {
                    var usuario = JSON.parse(usuarioL);
                    $("#nomUserg").text(usuario.Apellidos);
                    $("#imgUsumast").attr("src", usuario.ImageFull);
                    if (usuario.IdRol === 1) {
                        $(".adminic").show();
                    } else {
                        $(".adminic").hide();
                    }
                } else {
                    console.error('No se encontró información del usuario en sessionStorage.');
                    window.location.href = 'Login.aspx'; // Redirigir si no hay usuario válido
                }
                //var usuario = JSON.parse(sessionStorage.getItem('usuarioL'));
                //$("#nomUserg").text(usuario.Apellidos);
                //$("#imgUsumast").attr("src", usuario.ImageFull);

                //if (usuario.IdRol === 1) {
                //    $(".adminic").show();
                //} else {
                //    $(".adminic").hide();
                //}
            }
        } else {
            window.location.href = 'Login.aspx';
        }
    } catch (error) {
        // Manejo de error de la llamada AJAX
        console.error('Error al obtener los datos del usuario:', error);
        window.location.href = 'Login.aspx'; // Redirigir si hay error grave
    }
}

async function cerrarSesion() {
    try {
        const response = await $.ajax({
            type: "POST",
            url: "Inicio.aspx/CerrarSesion",
            contentType: 'application/json; charset=utf-8',
            dataType: "json"
        });

        if (response.d.Estado) {
            sessionStorage.clear(); // Limpia el almacenamiento de sesión
            window.location.replace('Login.aspx'); // Redirige al login
        }
    } catch (error) {
        console.error('Error al cerrar la sesión:', error);
    }
}



//$(document).ready(function () {
//    oBtenerDetalleUsuarioR();
//});

//function oBtenerDetalleUsuarioR() {

//    $.ajax({
//        type: "POST",
//        url: "Inicio.aspx/ObtenerDatos",
//        data: {},
//        contentType: 'application/json; charset=utf-8',
//        dataType: "json",
//        error: function (xhr, ajaxOptions, thrownError) {
//            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
//        },
//        success: function (response) {

//            if (response.d.Estado) {
//                if (sessionStorage.getItem('tokenSesionOrt') !== response.d.Valor) {
//                    CerrarSesion();
//                }
//                //$("#nomUserg").append(response.d.Objeto.Apellidos); salirsis
//                $("#nomUserg").text(response.d.Data.Apellidos);
//                $("#imgUsumast").attr("src", response.d.Data.ImageFull);
//                //$("#rolusuario").html("<i class='fa fa-circle text-success'></i> " + response.d.objeto.oRol.Descripcion);
//            } else {
//                window.location.href = 'Login.aspx';
//            }

//        }
//    });
//}

//function CerrarSesion() {

//    $.ajax({
//        type: "POST",
//        url: "Inicio.aspx/CerrarSesion",
//        dataType: "json",
//        contentType: 'application/json; charset=utf-8',
//        error: function (xhr, ajaxOptions, thrownError) {
//            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
//        },
//        success: function (response) {
//            if (response.d.Estado) {
//                //sessionStorage.removeItem('usuario');
//                sessionStorage.clear();
//                //window.location.href = 'Login.aspx';
//                // Limpiar el historial antes de redirigir
//                window.location.replace('Login.aspx');
//            }
//        }
//    });
//}