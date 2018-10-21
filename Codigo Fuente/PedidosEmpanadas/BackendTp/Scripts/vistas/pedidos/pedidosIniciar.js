$(document).ready(function () {
    var selectAll = $("#selectAll");
    var textValidacionGustos = $("#textValidacionGustos");
    var btnIniciar = $("#btnIniciar");
    var inputInvitados = $("#inputInvitados");
    var divInvitado = $("#divInvitado");

    var btnIniciar = $("#btnIniciar");
    var invitadosEditar = $(".invitadosEditar");
    inicializaSelectTags(window.usuarioController, inputInvitados, "invitados");
    if(invitadosEditar != null || invitadosEditar != undefined)
        agregarTag(inputInvitados, invitadosEditar);

    selectAll.click(function () {
        seleccionarTodos(this);
    });

    btnIniciar.click(function (e) {
        validacionYEnvio(e);
    });
});




