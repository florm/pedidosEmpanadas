var textEmail = $("#textEmail");
var textPassword = $("#textPassword");
var textRepetirPassword = $("#textRepetirPassword");
var btnAceptar = $("#btnAceptar");
var modalRegistro = $("#registro");

btnAceptar.click(function () {
    if (!validar())
        return;
    
    var usuario = new Object();
    usuario.email = textEmail.val();
    usuario.password = textPassword.val();
    
    llamadaAjax(window.pathRegistro, JSON.stringify(usuario), false, "registroOk", "registroError");
});

function registroOk() {
    mostrarMsgExito("El registro se realizó correctamente. Ahora puede iniciar sesión");
    modalRegistro.modal("hide");

}
function registroError(mensaje, params) {
    console.log(mensaje);
    console.log(params);
}

function validar() {
    if (!validarCampoEmail(textEmail)) return false;
    if(!validarCampoObligatorio(textPassword)) return false;
    if(!validarTextRepetir()) return false;
    return true;
}

function validarTextRepetir() {
    if (textPassword.val() != textRepetirPassword.val()) {
        mostrarMensajeValidacionErronea(textRepetirPassword, "Las contraseñas deben ser iguales");
        return false;
    }
    return validarCampoObligatorio(textRepetirPassword);
}