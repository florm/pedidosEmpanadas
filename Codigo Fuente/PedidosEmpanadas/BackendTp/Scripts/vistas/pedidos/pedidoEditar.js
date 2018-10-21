$(document).ready(function () {
    var eliminarInvitado = $(".eliminarInvitado");
    var cambio = false;

    eliminarInvitado.click(function () {
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
});