$(".gustosUsuario").numeric({ decimal: false, negative: false, min: 1, max: 5000 });
$("#btnElegirGustos").click(function () {
    ElegirGustos();
});

function ElegirGustos() {

    var pedidoRequest = new Object();
    pedidoRequest.IdUsuario = $("#idUsuario").val();
    pedidoRequest.IdPedido = $("#idPedido").val();
    pedidoRequest.Token = $("#tokenUsuario").val();

    var GustoEmpanadasCantidad = $("#listaGustos input.gustosUsuario").map(function () {
        var GustoEmpanadasCantidad = new Object();
        GustoEmpanadasCantidad.Cantidad = $(this).val();
        GustoEmpanadasCantidad.IdGustoEmpanada = $(this).attr('id');
        return GustoEmpanadasCantidad;
    }).get();

    pedidoRequest.GustoEmpanadasCantidad = GustoEmpanadasCantidad;

    llamadaAjax("/api/pedidos/confirmargustos", JSON.stringify(pedidoRequest), true, "gustosOk", "mostrarMensajeDeError");
}

function gustosOk(data) {
    mostrarMsgExito(data);
    setTimeout(function () {
        window.location.href = window.pathPedidos;
    }, 3000);
}