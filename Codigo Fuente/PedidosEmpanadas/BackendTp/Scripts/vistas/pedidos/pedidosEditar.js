var selectAll = $("#selectAll");
var textValidacionGustos = $("#textValidacionGustos");
var btnIniciar = $("#btnIniciar");
var inputInvitados = $("#inputInvitados");
var divInvitado = $("#divInvitado");
var invitadosEditar = $(".invitadosEditar");

inicializaSelectTags(window.usuarioController, inputInvitados, "invitados");
var inv = divInvitado.find("input");

var btnEditar = $("#btnEditar");

selectAll.click(function () {
    seleccionarTodos(this);
});


btnEditar.click(function (e) {
    validacionYEnvio(e, buscarInvitadosNoEliminados());
});

var iconoEliminarInvitado = $(".iconoEliminarInvitado:not(.deshabilitado)");
var cambio = false;

iconoEliminarInvitado.click(function () {
    if (cambio) {
        $(this).css("color", "black");
        cambio = false;
        $(this).attr("eliminado", "false");
    }
    else {
        $(this).css("color", "red");
        cambio = true;
        $(this).attr("eliminado", "true");
    }
});

$("#pruebainvitados").click(function () {
    buscarInvitadosNoEliminados();
});

$("#btnConfirmar").click(function () {
    
    buttonLoading($(this), true);
    $.post( "/Pedido/Confirmar", { "Pedido.IdPedido": $(this).data("pedido_id") })
    .done(function( data ) {
        toastr.success("El pedido fue Confirmado exitosamente.");
        $("#btnConfirmar").find("span").remove();
    });
});

function buscarInvitadosNoEliminados() {
    var eliminadosFalse = $(".iconoEliminarInvitado[eliminado=false]");
    var invitadosConfirmados = new Array();
    $.each(eliminadosFalse, function (i, item) {
        invitadosConfirmados.push({id: item.id, completoPedido: $(item).attr("completoPedido")});
    });
    return invitadosConfirmados;
}


