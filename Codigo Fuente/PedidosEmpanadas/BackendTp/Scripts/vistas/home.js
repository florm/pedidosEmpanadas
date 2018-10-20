﻿var textEmail = $("#textEmail");
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

modalRegistro.find(".btn-cancelar").click(function() {
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
    
    llamadaAjax(window.pathRegistro, JSON.stringify(usuario), false, "registroOk", "registroError");
});

btnAceptarLogin.click(function () {
    if (!validarCampoEmail(loginEmail)) return;
    if (!validarCampoObligatorio(loginPassword)) return;

    var usuario = new Object();
    usuario.email = loginEmail.val();
    usuario.password = loginPassword.val();

    llamadaAjax(window.pathLogin, JSON.stringify(usuario), false, "loginOk", "loginError");
});

function registroOk() {
    mostrarMsgExito("El registro se realizó correctamente. Ahora puede iniciar sesión");
    modalRegistro.modal("hide");

}
function registroError(mensaje, params) {
    mensaje = JSON.parse(mensaje);
    mostrarMsgError(mensaje.message);
    console.log(mensaje);
    console.log(params);
}

function loginOk() {
    window.location.href = window.pathListaPedidos;

}
function loginError(data) {
    var mensaje = JSON.parse(data);
    mostrarMsgError(mensaje.ErrorMessage);
    
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



$("#testbtn").click(function () {
    probandoajax();

});




function probandoajax() {

    //var invocation = new XMLHttpRequest();
    //var url = 'http://localhost:56230/Api/Pedidos';

    //callOtherDomain();

    //function callOtherDomain() {
    //    if (invocation) {
    //        invocation.open('POST', url, true);
    //        invocation.onreadystatechange = handler;
    //        invocation.send();
    //    }
    //}

    var pedidoRequest = new Object();
    pedidoRequest.IdUsuario = 5;
    pedidoRequest.Token = "Ale";
    llamadaAjax("http://localhost:56230/api/pedidos", JSON.stringify(pedidoRequest), true, "pedidoOk", "mostrarMensajeDeError");
    //llamadaAjax("http://localhost:56230/Api/Pedidos", JSON.stringify(pedidoRequest), true, "pedidoOk", "mostrarMensajeDeError");

    //$.ajax({
    //    url: 'http://localhost:56230/Api/Pedidos',
    //    type: 'POST',
    //    data: JSON.stringify(pedidoRequest),
    //    contentType: "application/json;charset=utf-8",
    //    success: function (data) {
    //        alert("hola");
    //    }
    //});

}