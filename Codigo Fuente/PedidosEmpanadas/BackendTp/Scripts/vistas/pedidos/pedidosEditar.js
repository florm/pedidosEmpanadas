var selectAll = $("#selectAll");
var textValidacionGustos = $("#textValidacionGustos");
var btnIniciar = $("#btnIniciar");
var inputInvitados = $("#inputInvitados");
var divInvitado = $("#divInvitado");
var invitadosEditar = $(".invitadosEditar");

inicializaSelectTags(window.usuarioController, inputInvitados, "invitados");
HabilitarEnvioSoloANuevos();
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

    swal({
        title: "¿Seguro desea Confirmar el pedido?",
        text: "Una vez confirmado no se podrá modificar.",
        icon: "warning",
        buttons:  ["Cancelar", "Aceptar"],
        dangerMode: true,
    })
        .then((willConfirm) => {
        if (willConfirm) {
            buttonLoading($(this), true);
            $.post( "/Pedido/Confirmar", { "Pedido.IdPedido": $(this).data("pedido_id") })
                .done(function( data ) {
                    toastr.success("El pedido fue Confirmado exitosamente.");
                    $("#btnConfirmar").find("span").remove();
                });
        }
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


var acciones = $("#acciones");
acciones.click(function() {
    HabilitarEnvioSoloANuevos();
});

function HabilitarEnvioSoloANuevos() {
    if (inputInvitados.val() === "") {
        $("#acciones option[value='3']").attr('disabled', 'disabled');
    }
    else
        $("#acciones option[value='3']").removeAttr('disabled');
}

