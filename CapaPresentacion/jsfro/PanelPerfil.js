﻿

let opcion = false; // Variable global para validar la fortaleza

$(document).ready(function () {
    $.LoadingOverlay("show");
    obtenerDatosUsua();

    // Función para evaluar la fortaleza de la contraseña
    function strength(password) {
        let i = 0;

        if (password.length > 6) {
            i++;
        }

        if (password.length >= 10) {
            i++;
        }

        if (/[A-Z]/.test(password)) {
            i++;
        }

        if (/[0-9]/.test(password)) {
            i++;
        }

        if (/[A-Za-z0-8]/.test(password)) {
            i++;
        }

        return i;
    }
    // Evento keyup para monitorear los cambios en el campo de contraseña
    $('#txtClaveNueva').on('keyup', function () {
        let password = $(this).val();
        let strengthValue = strength(password);
        let container = $('.containerz');

        let gradientColor;
        let strengthText;
        let text = document.getElementById('texto');

        // Evaluar la fortaleza de la contraseña y actualizar las clases
        if (strengthValue <= 2) {
            container.addClass('weak');
            container.removeClass('medium');
            container.removeClass('strong');
            gradientColor = '#f00';
            strengthText = 'Muy débil';
            opcion = false;
        } else if (strengthValue >= 2 && strengthValue <= 4) {
            container.removeClass('weak');
            container.addClass('medium');
            container.removeClass('strong');
            gradientColor = '#ffa500';
            strengthText = 'Medio';
            opcion = true;
        } else {
            container.removeClass('weak');
            container.removeClass('medium');
            container.addClass('strong');
            gradientColor = '#0f0';
            strengthText = 'Fuerte';
            opcion = true;
        }
        text.textContent = strengthText;
        text.style.color = gradientColor;
    });


    // Seleccionar el campo de contraseña y el botón de mostrar/ocultar
    let pswrd = $('#txtClaveNueva');
    let show = $('.shown');

    // Añadir evento al hacer clic en el botón de mostrar/ocultar
    show.on('click', function () {
        if (pswrd.attr('type') === 'password') {
            pswrd.attr('type', 'text');  // Cambiar a texto
            show.addClass('hide');       // Añadir clase 'hide' para cambiar el contenido del botón
        } else {
            pswrd.attr('type', 'password');  // Volver a contraseña
            show.removeClass('hide');        // Remover la clase 'hide' para restaurar el contenido del botón
        }
    });

    // Campo de "Contraseña Actual" y su botón de mostrar/ocultar
    let pswrdActual = $('#txtClaveActual');
    let showActual = $('#showClaveActual');

    // Evento para mostrar/ocultar "Contraseña Actual"
    showActual.on('click', function () {
        if (pswrdActual.attr('type') === 'password') {
            pswrdActual.attr('type', 'text');
            showActual.addClass('hide');
        } else {
            pswrdActual.attr('type', 'password');
            showActual.removeClass('hide');
        }
    });

    // Campo de "Confirmar Contraseña" y su botón de mostrar/ocultar
    let pswrdConfirmar = $('#txtConfirmarClave');
    let showConfirmar = $('#showConfirmarClave');

    // Evento para mostrar/ocultar "Confirmar Contraseña"
    showConfirmar.on('click', function () {
        if (pswrdConfirmar.attr('type') === 'password') {
            pswrdConfirmar.attr('type', 'text');
            showConfirmar.addClass('hide');
        } else {
            pswrdConfirmar.attr('type', 'password');
            showConfirmar.removeClass('hide');
        }
    });
});
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
    //if (claveNueva.length < 8) {
    //    toastr.warning("", "La nueva contraseña debe tener al menos 8 caracteres");
    //    return;
    //}

    // Validar la fortaleza de la contraseña usando la variable global 'opcion'
    if (opcion === false) {
        swal("Mensaje", "La nueva contraseña no cumple con los requisitos mínimos de seguridad", "warning");
        //toastr.warning("", "La nueva contraseña no cumple con los requisitos mínimos de seguridad");
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