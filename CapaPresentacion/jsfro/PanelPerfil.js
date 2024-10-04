
$(document).ready(function () {
    $.LoadingOverlay("show");
    obtenerDatosUsua();
})
function obtenerDatosUsua() {

    var usuario = JSON.parse(sessionStorage.getItem('usuarioL'));
    $("#txtIdUsuarioP").val(usuario.IdUsuario);
    $("#imgFotop").attr("src", usuario.ImageFull);
    $("#txtNombre").val(usuario.Nombres);
    $("#txtapellido").val(usuario.Apellidos);
    $("#txtCorreo").val(usuario.Correo);
    $("#txtRol").val(usuario.Orol.NomRol);
    // Hacer que el LoadingOverlay dure unos segundos antes de desaparecer
    setTimeout(function () {
        $.LoadingOverlay("hide");
    }, 1000); // El overlay se ocultará después de 1 segundo (1000 milisegundos)
}


function mostrarImagenSeleccionad(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgFotop').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    } else {
        $('#imgFotop').attr('src', "Imagenes/sinimagen.png");
    }
}

$('#txtFotop').change(function () {
    mostrarImagenSeleccionad(this);
});

function sendDataToServerP(request) {
    $.ajax({
        type: "POST",
        url: "PanelPerfil.aspx/EditarPerfil",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loadPe").LoadingOverlay("show");
        },
        success: async function (response) { // Declaramos esta función como asíncrona
            $("#loadPe").LoadingOverlay("hide");
            if (response.d.Estado) {
                swal("Mensaje", response.d.Valor, "success");

                // Aguardamos a que la función cerrarSesion se complete
                await cerrarSesion();

            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadPe").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCambiosp').prop('disabled', false);
        }
    });
}

//sin usar
function sendDataToServerPOri(request) {
    $.ajax({
        type: "POST",
        url: "PanelPerfil.aspx/EditarPerfil",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loadPe").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loadPe").LoadingOverlay("hide");
            if (response.d.Estado) {
                swal("Mensaje", response.d.Valor, "success");

                cerrarSesion();

            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadPe").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCambiosp').prop('disabled', false);
        }
    });
}

function registerDataAjaxP() {
    var fileInput = document.getElementById('txtFotop');
    var file = fileInput.files[0];

    var modelo = {
        IdUsuario: parseInt($("#txtIdUsuarioP").val()),
        Nombres: $("#txtNombre").val(),
        Apellidos: $("#txtapellido").val(),
        Correo: $("#txtCorreo").val()
    }

    if (file) {

        var maxSize = 2 * 1024 * 1024; // 2 MB en bytes
        if (file.size > maxSize) {
            swal("Mensaje", "La imagen seleccionada es demasiado grande max 1.5 Mb.", "warning");
            // Rehabilitar el botón si hay un error de validación
            $('#btnGuardarCambiosp').prop('disabled', false);
            return;
        }

        var reader = new FileReader();

        reader.onload = function (e) {
            var arrayBuffer = e.target.result;
            var bytes = new Uint8Array(arrayBuffer);

            var request = {
                oUsuario: modelo,
                imageBytes: Array.from(bytes)
            };

            sendDataToServerP(request);
        };

        reader.readAsArrayBuffer(file);
    } else {
        // Si no se selecciona ningún archivo, envía un valor nulo o vacío para imageBytes
        var request = {
            oUsuario: modelo,
            imageBytes: null // o cualquier otro valor que indique que no se envió ningún archivo
        };

        sendDataToServerP(request);
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

$('#btnGuardarCambiosp').on('click', function () {

    $('#btnGuardarCambiosp').prop('disabled', true);

    if ($("#txtCorreo").val().trim() === "") {
        toastr.warning("", "Debe Ingresar correo")
        $("#txtCorreo").focus()
        $('#btnGuardarCambiosp').prop('disabled', false);
        return;
    }

    swal({
        title: "Mensaje de Confirmacion",
        text: "¿Desea guardar los cambios?",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-primary",
        confirmButtonText: "Si",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },

        function (respuesta) {
            if (respuesta) {
                registerDataAjaxP();
            } else {
                // Si el usuario cancela, vuelve a habilitar el botón
                $('#btnGuardarCambiosp').prop('disabled', false);
            }
            
        });


})

$('#btnCambiarClave').on('click', async function () { // Cambiar aquí para usar async

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const claveNueva = $("#txtClaveNueva").val().trim();
    const confirmarClave = $("#txtConfirmarClave").val().trim();

    // Validar que la nueva clave tenga al menos 8 caracteres
    if (claveNueva.length < 8) {
        toastr.warning("", "La nueva contraseña debe tener al menos 8 caracteres");
        return;
    }

    // Validar que ambas contraseñas sean iguales
    if (claveNueva != confirmarClave) {
        toastr.warning("", "Las contraseñas no son iguales");
        return;
    }

    var request = {
        IdUsuario: parseInt($("#txtIdUsuarioP").val()),
        claveActual: $("#txtClaveActual").val().trim(),
        claveNueva: claveNueva
    }

    $.ajax({
        type: "POST",
        url: "PanelPerfil.aspx/CambiarClave",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $("#loadis").LoadingOverlay("show");
        },
        success: async function (response) {  // Aquí añadimos async para usar await
            $("#loadis").LoadingOverlay("hide");
            if (response.d.Estado) {
                swal("Mensaje", response.d.Valor, "success");
                // Retraso de 2 segundos antes de cerrar sesión
                setTimeout(async function () {
                    await cerrarSesion();  // Llama a cerrarSesion después del retraso
                }, 2000);  // Retraso de 2000 ms (2 segundos)
                //await cerrarSesion();

            } else {
                swal("Mensaje", response.d.Valor, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadis").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
});