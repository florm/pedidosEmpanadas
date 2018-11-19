var textEmail = $("#textEmail");
var textPassword = $("#textPassword");
var textRepetirPassword = $("#textRepetirPassword");
var btnAceptar = $("#btnAceptar");
var modalRegistro = $("#registro");
var modalLogin = $("#login");
var loginEmail = $("#loginEmail");
var loginPassword = $("#loginPassword");
var btnAceptarLogin = $("#btnAceptarLogin");


modalLogin.find(".btn-cancelar").click(function () {
    modalLogin.modal('hide');
    loginEmail.val("");
    loginPassword.val("");
    ocultarMensajeValidacionErronea(loginEmail);
    ocultarMensajeValidacionErronea(loginPassword);


});

modalRegistro.find(".btn-cancelar").click(function () {
    modalRegistro.modal('hide');
    textEmail.val("");
    textPassword.val("");
    ocultarMensajeValidacionErronea(textEmail);
    ocultarMensajeValidacionErronea(textPassword);
    ocultarMensajeValidacionErronea(textRepetirPassword);
});

btnAceptar.click(function () {
    if (!validar())
        return;

    var usuario = new Object();
    usuario.email = textEmail.val();
    usuario.password = textPassword.val();

    llamadaAjax(window.pathRegistro, JSON.stringify(usuario), false, "callback");
});

btnAceptarLogin.click(function () {
    if (!validarCampoEmail(loginEmail)) return;
    if (!validarCampoObligatorio(loginPassword)) return;

    var usuario = new Object();
    usuario.email = loginEmail.val();
    usuario.password = loginPassword.val();

    llamadaAjax(window.pathLogin, JSON.stringify(usuario), false, "loginOk", "loginError");
});

function callback(data) {
    mostrarMsgExito(data.mensaje);
    if(data.operacion ==="ok")
    modalRegistro.modal("hide");

}


function loginOk(data) {
    var url = data.split('/');
    if (data === "")
        window.location.href = window.pathListaPedidos;
    else if (url[1].toLowerCase() === 'pedido' && url[2].toLowerCase() === 'detalle'
        || url[1].toLowerCase() === 'pedido' && url[2].toLowerCase() === 'elegirGustos'
        || url[1].toLowerCase() === 'pedido' && url[2].toLowerCase() === 'elegir')
        window.location.href = data;
    else
    {
        window.location.href = window.pathListaPedidos;
    }

}

function loginError(data) {
    var mensaje = JSON.parse(data);
    mostrarMsgError(mensaje.ErrorMessage);

}


function validar() {
    if (!validarCampoEmail(textEmail)) return false;
    if (!validarCampoObligatorio(textPassword)) return false;
    if (!validarTextRepetir()) return false;
    return true;
}

function validarTextRepetir() {
    if (textPassword.val() != textRepetirPassword.val()) {
        mostrarMensajeValidacionErronea(textRepetirPassword, "Las contraseñas deben ser iguales");
        return false;
    }
    return validarCampoObligatorio(textRepetirPassword);
}

var btnCancelar = $("#btnCancelar");
btnCancelar.on('click', function () {
    if ('referrer' in document) {
        window.location = document.referrer;
    } else {
        window.history.back();
    }
});

toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "preventDuplicates": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "showDuration": "4000",
    "hideDuration": "10000",
    "timeOut": "7000",
    "extendedTimeOut": "10000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};
