//Al pulsar el boton de (btnLogin) mostrara la primera ventana del modal Login.
$(document).on("click", "#btnLogin", function () {
    $('#modalLoginMain').modal('show');
});

////////////////////////////////////////////////
//Login////////////////////////////////////////
///////////////////////////////////////////////
//Limpia el mensaje de error al hacer focus sobre el input
$("#inLoginEmail").focus(function () {
    $("#txtErrorEmail").removeClass("d-block");
});
$("#inRegistroUsuarioNombre").focus(function () {
    $("#txtErrorRegistroUsuarioNombre").removeClass("d-block");
});
$("#inRegistroUsuarioApellidos").focus(function () {
    $("#txtErrorRegistroUsuarioApellidos").removeClass("d-block");
});
$("#inRegistroUsuarioTelefono").focus(function () {
    $("#txtErrorRegistroUsuarioTelefono").removeClass("d-block");
});
$("#inRegistroUsuarioPassword").focus(function () {
    $("#txtErrorRegistroUsuarioPassword").removeClass("d-block");
});
//Comprueba si el formato email esta correctamente escrito.
//True == Email INCORRECTO.
//False == Email CORRECTO.
function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/;
    return pattern.test(emailAddress);
}
function isValidRegistroUsuarioNombre(nombre) {
    var pattern = /^[a-zA-Z-]{3,20}$/;
    return pattern.test(nombre);
}
function isValidRegistroUsuarioApellidos(apellidos) {
    var pattern = /^[a-zA-Z-]{3,100}$/;
    return pattern.test(apellidos);
}
function isValidRegistroUsuarioTelefono(telefono) {
    var pattern = /^[0-9]{9}$/;
    return pattern.test(telefono);
}
function isValidRegistroUsuarioPassword(password) {
    var pattern = /^[a-zA-Z0-9_-]{6,18}$/;
    return pattern.test(password);
}
function isValidRegistroUsuario(nombre, apellidos, telefono, password) {

    if (!isValidRegistroUsuarioNombre(nombre)) {
        $("#txtErrorRegistroUsuarioNombre").addClass("d-block");
        $("#inRegistroUsuarioNombre").effect("shake");
    } else if (!isValidRegistroUsuarioApellidos(apellidos)) {
        $("#txtErrorRegistroUsuarioApellidos").addClass("d-block");
        $("#inRegistroUsuarioApellidos").effect("shake");
    } else if (!isValidRegistroUsuarioTelefono(telefono)) {
        $("#txtErrorRegistroUsuarioTelefono").addClass("d-block");
        $("#inRegistroUsuarioTelefono").effect("shake");
    } else if (!isValidRegistroUsuarioPassword(password)) {
        $("#txtErrorRegistroUsuarioPassword").addClass("d-block");
        $("#inRegistroUsuarioPassword").effect("shake");
    }
    else
        return true;
    return false;
}
//Al pulsar sobre el boton SIGUIENTE en el LOGIN nos mandara a otro modal depende de lo que devuelva el email escrito.
//Email no existe => modal de creacion de usuario nuevo.
//Email existe => modal de usuario existente.
$(document).on("click", "#btnLoginNext", function () {
    if (!isValidEmailAddress($('#inLoginEmail').val().trim())) {
        $("#txtErrorEmail").addClass("d-block");
        $("#inLoginEmail").effect("shake");
    }
    else {
        $.ajax({
            type: "GET",
            url: "/Login/comprobarEmailExistente/",
            contentType: "application/json; charset=utf-8",
            data: { email: $("#inLoginEmail").val() },
            dataType: "json",
            success: function (result, status, xhr) {
                if (result) {
                    $("#tituloUser").text(result);
                    $('#modalLoginMain').modal('hide');
                    $('#loginUser').modal('show');
                }
                else {
                    $('#modalLoginMain').modal('hide');
                    $('#registroCuentaUsuario').modal('show');
                    $("#txtRegistroUsuarioEmail").text($("#inLoginEmail").val());
                }

            },
            error: function (xhr, status, error) { $("#dbData").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText) }
        });
        //codigo en el callback
    }


});

//Al pulsar sobre el boton REGISTRAR crea una cuenta nueva recogiendo 
// los parametros dados en los inputs
$(document).on("click", "#btnRegistrarUsuario", function () {
    console.log("pulsado");
    if (isValidRegistroUsuario($("#inRegistroUsuarioNombre").val(), $("#inRegistroUsuarioApellidos").val(),
        $("#inRegistroUsuarioTelefono").val(), $("#inRegistroUsuarioPassword").val())) {
        $.ajax({
            type: "GET",
            url: "/Login/crearCuentaUsuario/",
            contentType: "application/json; charset=utf-8",
            data: {
                nombre: $("#inRegistroUsuarioNombre").val(),
                apellido: $("#inRegistroUsuarioApellidos").val(),
                telefono: $("#inRegistroUsuarioTelefono").val(),
                password: $("#inRegistroUsuarioPassword").val(),
                email: $("#txtRegistroUsuarioEmail").text()
            },
            dataType: "json",
            success: function (result, status, xhr) {
                if (result != null) {
                    $('#registroCuentaUsuario').modal('hide');
                    location.reload();
                }
            },
        });
    } else console.log("error");
});

//usuario modal
$(document).on("click", "#btnLoginUser", function () {
        $.ajax({
            type: "GET",
            url: "/Login/login/",
            contentType: "application/json; charset=utf-8",
            data: {
                email: $("#inLoginEmail").val(),
                password: $("#inInicioSesionPassword").val()
            },
            dataType: "json",
            success: function (result, status, xhr) {
                if (result) {
                    $('#loginUser').modal('hide');
                    location.reload();
                }
                else {
                    $('#txtErrorPassword').addClass("d-block");
                    $('#inInicioSesionPassword').effect('shake');
                }
            }, error: function (xhr, status, error) { $("#dbData").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText) }
        });
});

$("#atrasLoginUser").click(function () {
    $('#loginUser').modal('hide');
    $('#modalLoginMain').modal('show');
});
$("#atrasLoginCreate").click(function () {
    $('#registroCuentaUsuario').modal('hide');
    $('#modalLoginMain').modal('show');
});

$('a[href="#logout"]').click(function () {
    $.ajax({
        url: '/Login/borrarCookies/',
        type: 'GET',
        success: function (data) { if (data == true) location.reload(); }
    })
});

//Autocompleta el input de BUSCAR CIUDAD
$("#inBuscarCiudad").focus(function () {
    $("#inBuscarCiudad").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/autoCompleteCiudades/',
                type: 'GET',
                cache: false,
                data: request,
                dataType: 'json',
                success: function (data) {
                    response($.map(data.slice(0,5), function (item) {
                        return {
                            label: item,
                            value: item
                        }
                    }))
                }
            });
        },
        select: function (event, ui) {
            window.location.href = "/Distrito/B/"+ui.item.label
        }
    });
});

$("#inBuscarRestaurante").focus(function () {
    $("#inBuscarRestaurante").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/autoCompleteBusqueda/',
                type: 'GET',
                cache: false,
                data: request,
                dataType: 'json',
                success: function (data) {
                    response($.map(data.slice(0,5), function (item) {
                        return {
                            label: JSON.stringify(item.nombre).replace(/"/g, ''),
                            value2: JSON.stringify(item.idlocal)
                        }
                    }))
                }
            });
        },
        select: function (event, ui) {
            window.location.href = "/Local/Id/" + ui.item.value2
        }
    });
});



$(".card-content").on({
    "mouseover": function () {
        var image = $(this).find("img");

        var src = $(image).attr("src");
        var newfile = src.substr(0, src.length - 4) + "2.png";
        $(image).attr("src", newfile);
    }
});
$(".card-content").on({
    "mouseout": function () {
        var image = $(this).find("img");

        var src = $(image).attr("src");
        var newfile = src.substr(0, src.length - 5) + ".png";
        $(image).attr("src", newfile);
    }
});
//BOTON NAVBAR MOVIL
$("#btn-abajo").on({
    "click": function () {
        $("#btn-abajo").css("display", "none");
        $("#btn-arriba").css("display", "block");
    }
});
$("#btn-arriba").on({
    "click": function () {
        $("#btn-arriba").css("display", "none");
        $("#btn-abajo").css("display", "block");
    }
});



