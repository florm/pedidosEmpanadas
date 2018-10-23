var selectAll = $("#selectAll");
var textValidacionGustos = $("#textValidacionGustos");
var btnIniciar = $("#btnIniciar");
var inputInvitados = $("#inputInvitados");
var divInvitado = $("#divInvitado");
var invitadosEditar = $(".invitadosEditar");

inicializaSelectTags(window.usuarioController, inputInvitados, "invitados");
var inv = divInvitado.find("input");

var btnEditar = $("#btnEditar");
agregarTag(inputInvitados, invitadosEditar);



btnEditar.click(function (e) {
    validacionYEnvio(e);
});


